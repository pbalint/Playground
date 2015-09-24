package com.pb;

import javax.swing.UIManager;
import javax.xml.bind.annotation.XmlRootElement;

@XmlRootElement
public class Config {
    private String apiKey;
    private String lookAndFeel;

    public String getLookAndFeel() {
        return lookAndFeel;
    }

    public void setLookAndFeel( String lookAndFeel ) {
        this.lookAndFeel = lookAndFeel;
    }

    public String getApiKey() {
        return apiKey;
    }

    public void setApiKey( String apiKey ) {
        this.apiKey = apiKey;
    }

    public static Config getDefault() {
        Config defaultConfig = new Config();
        defaultConfig.setLookAndFeel( UIManager.getSystemLookAndFeelClassName() );
        return defaultConfig;
    }
}
