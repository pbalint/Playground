package com.pb.controller;

import java.io.FileReader;
import java.io.IOException;
import java.io.Reader;
import java.util.ArrayList;
import java.util.List;

import org.apache.commons.csv.CSVFormat;
import org.apache.commons.csv.CSVRecord;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Controller;
import org.springframework.ui.ModelMap;
import org.springframework.web.bind.annotation.ModelAttribute;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;

import com.pb.models.Measurement;

@Controller
public class TemperatureController {
    @Value( "${measurement.csv.location:./temperatures.csv}" )
    private String measurementCsvLocation;

    @RequestMapping( value = "/temperature", method = RequestMethod.GET )
    public String temperature( @ModelAttribute( "model" ) ModelMap model ) {
        List< Measurement > measurements = readMeasurements( measurementCsvLocation );
        model.addAttribute( "measurements", measurements );
        return "temperature";
    }

    private List< Measurement > readMeasurements( String measurementCsvLocation ) {
        List< Measurement > measurements = new ArrayList<>();
        try ( Reader in = new FileReader( measurementCsvLocation ) ) {
            Iterable< CSVRecord > records = CSVFormat.DEFAULT.withDelimiter( ';' ).parse( in );
            for ( CSVRecord record : records ) {
                Long timeStamp = Long.parseLong( record.get( 0 ) );
                Double temperature = Double.parseDouble( record.get( 1 ) );
                measurements.add( new Measurement( timeStamp, temperature ) );
            }
        }
        catch ( IOException e ) {
            e.printStackTrace();
        }
        return measurements;
    }
}
