package com.pb.entities;

import javax.persistence.Entity;
import javax.persistence.EnumType;
import javax.persistence.Enumerated;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.Table;

@Entity
@Table(name="rest_log")
public class RestLogEntry {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long       id;
    
    private long       timeStamp;

    private String     hostName;

    private String     ipAddress;

    private String     url;

    @Enumerated( EnumType.STRING )
    private HttpMethod method;
    
    private int        rttMs;

    private String     requestPayload;

    private int        requestPayloadSize;

    private int        responseCode;

    private String     responsePayload;

    private int        responsePayloadSize;

    public Long getId() {
        return id;
    }

    public void setId( Long id ) {
        this.id = id;
    }

    public String getHostName() {
        return hostName;
    }

    public void setHostName( String hostName ) {
        this.hostName = hostName;
    }

    public String getIpAddress() {
        return ipAddress;
    }

    public void setIpAddress( String ipAddress ) {
        this.ipAddress = ipAddress;
    }

    public String getUrl() {
        return url;
    }

    public void setUrl( String url ) {
        this.url = url;
    }

    public HttpMethod getMethod() {
        return method;
    }

    public void setMethod( HttpMethod method ) {
        this.method = method;
    }

    public String getRequestPayload() {
        return requestPayload;
    }

    public void setRequestPayload( String requestPayload ) {
        this.requestPayload = requestPayload;
    }

    public int getRequestPayloadSize() {
        return requestPayloadSize;
    }

    public void setRequestPayloadSize( int requestPayloadSize ) {
        this.requestPayloadSize = requestPayloadSize;
    }

    public int getResponseCode() {
        return responseCode;
    }

    public void setResponseCode( int responseCode ) {
        this.responseCode = responseCode;
    }

    public String getResponsePayload() {
        return responsePayload;
    }

    public void setResponsePayload( String responsePayload ) {
        this.responsePayload = responsePayload;
    }

    public int getResponsePayloadSize() {
        return responsePayloadSize;
    }

    public void setResponsePayloadSize( int responsePayloadSize ) {
        this.responsePayloadSize = responsePayloadSize;
    }

    public long getTimeStamp() {
        return timeStamp;
    }

    public void setTimeStamp( long timeStamp ) {
        this.timeStamp = timeStamp;
    }

    public int getRttMs() {
        return rttMs;
    }

    public void setRttMs( int rttMs ) {
        this.rttMs = rttMs;
    }
}
