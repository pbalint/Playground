package com.pb.models;

public class Measurement {
    private long   timeStamp;
    private double temperature;

    public Measurement( long timeStamp, double temperature ) {
        this.timeStamp = timeStamp;
        this.temperature = temperature;
    }

    public long getTimeStamp() {
        return timeStamp;
    }

    public void setTimeStamp( long timeStamp ) {
        this.timeStamp = timeStamp;
    }

    public double getTemperature() {
        return temperature;
    }

    public void setTemperature( double temperature ) {
        this.temperature = temperature;
    }
}
