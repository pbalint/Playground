package com.pb.measurement.service;

import java.util.Deque;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.zeromq.ZMQ;
import org.zeromq.ZMQ.Context;
import org.zeromq.ZMQ.Socket;

import com.google.protobuf.InvalidProtocolBufferException;
import com.pb.measurement.MeasurementMessage.Measurement;

class MeasurementWorker implements Runnable {
    private static final Logger  LOGGER  = LoggerFactory.getLogger( MeasurementWorker.class );

    private volatile boolean     running = true;
    private String               dataEndpoint;
    private Deque< Measurement > measurements;
    private int                  itemCount;
    private int                  maxItemCount;

    public MeasurementWorker( String dataEndpoint, Deque< Measurement > measurements, int maxItemCount ) {
        this.dataEndpoint = dataEndpoint;
        this.measurements = measurements;
        this.maxItemCount = maxItemCount;
        this.itemCount = measurements.size();
    }

    @Override
    public void run() {
        Context context = ZMQ.context( 1 );
        Socket socket = context.socket( ZMQ.SUB );
        socket.connect( dataEndpoint );
        socket.subscribe( "" );

        try {
            while ( running ) {
                Measurement measurement = Measurement.parseFrom( socket.recv() );
                synchronized ( measurements ) {
                    if ( itemCount >= maxItemCount ) {
                        measurements.removeFirst();
                        itemCount--;
                    }
                    measurements.add( measurement );
                    itemCount++;
                }
            }
        }
        catch ( InvalidProtocolBufferException e ) {
            LOGGER.error( "Exception when deserializing message: ", e );
        }
        socket.close();
        context.close();
    }

    public void stop() {
        running = false;
    }

    public Deque<Measurement> getMeasurements() {
        return measurements;
    }

    public void setMeasurements( Deque<Measurement> measurements ) {
        this.measurements = measurements;
        this.itemCount = measurements.size();
    }
}
