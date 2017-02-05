package com.pb.controllers;

import java.time.LocalDateTime;
import java.time.ZoneId;
import java.util.ArrayList;
import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.client.RestTemplate;

import com.pb.entities.RestLogEntry;
import com.pb.services.RestLogService;

@RestController
public class RestLogController {
    @Autowired
    private RestLogService restLogService;

    @RequestMapping( path = "add-rest-log", method = RequestMethod.PUT )
    public void putRestLog( @RequestBody RestLogEntry restLogEntry ) {
        restLogService.saveLogEntry( restLogEntry );
    }
    
    @RequestMapping( path = "add-rest-logs", method = RequestMethod.PUT )
    public void putRestLogs( @RequestBody List<RestLogEntry> restLogEntries ) {
        restLogService.saveLogEntries( restLogEntries );
    }
    
    @RequestMapping( path = "test", method = RequestMethod.GET )
    public void testRestLog() {
        RestTemplate restTemplate = new RestTemplate();
        RestLogEntry entry = new RestLogEntry();
        entry.setTimeStamp( LocalDateTime.now().atZone( ZoneId.systemDefault() ).toEpochSecond() );
        entry.setHostName( "myhostname" );
        entry.setIpAddress( "1.1.1.1" );
        entry.setMethod( com.pb.entities.HttpMethod.PUT );
        entry.setRequestPayload( "asdasd" );
        entry.setRequestPayloadSize( 6 );
        entry.setResponseCode( 200 );
        entry.setResponsePayload( "okok" );
        entry.setResponsePayloadSize( 4 );
        entry.setRttMs( (int)(Math.random() * 1000));
        entry.setUrl( "localhost" );
        
        List<RestLogEntry> entries = new ArrayList<>();
        entries.add( entry );
        entries.add( entry );

        restTemplate.put( "http://localhost:8080/add-rest-logs", entries );
    }
    

}
