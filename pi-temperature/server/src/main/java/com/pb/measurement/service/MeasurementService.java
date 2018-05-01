package com.pb.measurement.service;

import java.io.BufferedWriter;
import java.io.FileWriter;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.util.Deque;
import java.util.Iterator;
import java.util.LinkedList;
import java.util.List;
import java.util.concurrent.ConcurrentLinkedDeque;
import java.util.stream.Stream;

import javax.annotation.PostConstruct;
import javax.annotation.PreDestroy;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Component;

import com.pb.measurement.MeasurementMessage.Measurement;
import com.pb.measurement.domain.Config;

@Component
public class MeasurementService {
    @SuppressWarnings( "unused" )
    private static final Logger LOGGER = LoggerFactory.getLogger( MeasurementService.class );


    @Value( "${data.endpoint}" )
    private String               dataEndpoint;
    
    @Autowired
    private Config               config;

    private Thread               workerThread;
    private MeasurementWorker    worker;

    @PostConstruct
    public void init() {
        Deque< Measurement > measurements = new ConcurrentLinkedDeque<>();
        try ( Stream< String > lines = Files.lines( Paths.get( config.getMeasurementFileName() ) ) ) {
            lines.forEachOrdered( line -> {
                String[] values = line.split( ";" );
                measurements.addLast( Measurement.newBuilder()
                                                 .setInput(Integer.parseInt( values[ 0 ]) )
                                                 .setTimeStamp(Long.parseLong( values[ 1 ]) )
                                                 .setValue(Double.parseDouble( values[ 2 ]) )
                                                 .build() );
            });
        }
        catch (IOException e) {
            e.printStackTrace();
        }
        
        worker = new MeasurementWorker( dataEndpoint, measurements, config.getHistorySize() );
        workerThread = new Thread( worker, MeasurementWorker.class.getSimpleName() );
        workerThread.setDaemon( true );
        workerThread.start();
    }

    @PreDestroy
    public void shutDown() throws InterruptedException {
        worker.stop();
        workerThread.join();
        
        Deque< Measurement > measurements = worker.getMeasurements();
        try (BufferedWriter writer = new BufferedWriter(new FileWriter(config.getMeasurementFileName()))) {
            StringBuilder builder = new StringBuilder();
            for ( Measurement measurement: measurements ) {
                builder.append( measurement.getInput() ).append( ';' ).append( measurement.getTimeStamp() ).append( ';' ).append( measurement.getValue() ).append( System.lineSeparator() );
                writer.write(builder.toString());
                builder = new StringBuilder( builder.length() );
            }
        } catch ( IOException e ) {
            e.printStackTrace();
        }
    }

    public List< Double[] > getMeasurementsFrom( double startTs ) {
        LinkedList< Double[] > points = new LinkedList<>();
        
        Iterator< Measurement > iterator = worker.getMeasurements().descendingIterator();
        while ( iterator.hasNext() ) {
            Measurement measurement = iterator.next();
            if ( measurement.getTimeStamp() > startTs ) {
                Double[] point = new Double[ 2 ];
                point[ 0 ] = (double)measurement.getTimeStamp();
                point[ 1 ] =  measurement.getValue();
                points.addFirst( point );
            }
            else {
                break;
            }
        }
        return points;
    }

    public List< Double[] > getMeasurementsFromBeginning() {
        LinkedList< Double[] > points = new LinkedList<>();

        Iterator< Measurement > iterator = worker.getMeasurements().iterator();
        while ( iterator.hasNext() ) {
            Measurement measurement = iterator.next();
            Double[] point = new Double[ 2 ];
            point[ 0 ] = (double)measurement.getTimeStamp();
            point[ 1 ] =  measurement.getValue();
            points.addFirst( point );
        }
        return points;
    }

    public void compactData( int pointsToAverage, double startTs, double endTs ) {
        Deque< Measurement > measurements = worker.getMeasurements();
        synchronized ( measurements ) {
            if ( startTs == 0 && !measurements.isEmpty() ) {
                startTs = measurements.peekFirst().getTimeStamp();
            }
            if ( endTs == 0 && !measurements.isEmpty() ) {
                endTs = measurements.peekLast().getTimeStamp();
            }
            
            ConcurrentLinkedDeque< Measurement > newMeasurements = new ConcurrentLinkedDeque<>();
            long averageTs = 0;
            double averageValue = 0.0;
            int pointsAveraged = 0;
            int input = !measurements.isEmpty()? measurements.peekFirst().getInput(): 0;
            for ( Measurement measurement: measurements ) {
                if ( measurement.getTimeStamp() >= startTs && measurement.getTimeStamp() <= endTs ) {
                    averageTs += measurement.getTimeStamp();
                    averageValue += measurement.getValue();
                    pointsAveraged++;
                    if ( pointsAveraged >= pointsToAverage ) {
                        newMeasurements.addLast( Measurement.newBuilder().setInput( input )
                                                                         .setTimeStamp( averageTs / pointsAveraged )
                                                                         .setValue( averageValue / pointsAveraged )
                                                                         .build() );
                        averageTs = 0;
                        averageValue = 0;
                        pointsAveraged = 0;
                    }
                }
                else if ( measurement.getTimeStamp() > endTs &&
                          pointsAveraged != 0 ) {
                    newMeasurements.addLast( Measurement.newBuilder().setInput( input )
                                                                     .setTimeStamp( averageTs / pointsAveraged )
                                                                     .setValue( averageValue / pointsAveraged )
                                                                     .build() );
                    pointsAveraged = 0;
                    newMeasurements.addLast( measurement );
                }
                else {
                    newMeasurements.addLast( measurement );
                }
            }
            worker.setMeasurements( newMeasurements );
        }
    }
}
