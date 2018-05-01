package com.pb.measurement.domain;

import org.springframework.boot.context.properties.ConfigurationProperties;
import org.springframework.stereotype.Component;

@Component
@ConfigurationProperties( prefix = "config" )
public class Config {
    private int    refreshInterval;
    private int    historySize;
    private String measurementFileName;

    public int getRefreshInterval() {
        return refreshInterval;
    }

    public void setRefreshInterval( int refreshInterval ) {
        this.refreshInterval = refreshInterval;
    }

    public int getHistorySize() {
        return historySize;
    }

    public void setHistorySize( int historySize ) {
        this.historySize = historySize;
    }

    public String getMeasurementFileName() {
        return measurementFileName;
    }

    public void setMeasurementFileName( String measurementFileName ) {
        this.measurementFileName = measurementFileName;
    }
}
