package com.jlogviewer;

import java.util.Set;
import java.util.TreeSet;

import javax.swing.UIManager;
import javax.xml.bind.annotation.XmlRootElement;

@XmlRootElement
public class Config {
    private int             maxLogEntryLength = 400;
    private Set< String >   filters           = new TreeSet<>();
    private String          lookAndFeel       = UIManager.getSystemLookAndFeelClassName();
    private int             dividerLocation   = 40;
    private int             windowX           = 10;
    private int             windowY           = 10;
    private int             windowWidth       = 640;
    private int             windowHeight      = 480;

    public int getMaxLogEntryLength() {
        return maxLogEntryLength;
    }

    public void setMaxLogEntryLength( int maxLogEntryLength ) {
        this.maxLogEntryLength = maxLogEntryLength;
    }

    public Set< String > getFilters() {
        return filters;
    }

    public void setFilters( Set< String > filters ) {
        this.filters = filters;
    }

    public String getLookAndFeel() {
        return lookAndFeel;
    }

    public void setLookAndFeel( String lookAndFeel ) {
        this.lookAndFeel = lookAndFeel;
    }
    
    public int getDividerLocation() {
        return dividerLocation;
    }

    public void setDividerLocation( int dividerLocation ) {
        this.dividerLocation = dividerLocation;
    }
    
    public int getWindowX() {
        return windowX;
    }

    public void setWindowX( int windowX ) {
        this.windowX = windowX;
    }

    public int getWindowY() {
        return windowY;
    }

    public void setWindowY( int windowY ) {
        this.windowY = windowY;
    }
    public int getWindowWidth() {
        return windowWidth;
    }

    public void setWindowWidth( int windowWidth ) {
        this.windowWidth = windowWidth;
    }

    public int getWindowHeight() {
        return windowHeight;
    }

    public void setWindowHeight( int windowHeight ) {
        this.windowHeight = windowHeight;
    }
}
