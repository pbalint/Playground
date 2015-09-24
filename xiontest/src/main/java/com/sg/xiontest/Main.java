package com.sg.xiontest;

import java.io.File;

import org.openqa.selenium.OutputType;
import org.openqa.selenium.firefox.FirefoxDriver;

public class Main {

    public static void main( String[] args ) {
        FirefoxDriver driver1 = new FirefoxDriver();
        driver1.navigate().to( "http://index.hu" );
        
        FirefoxDriver driver = new FirefoxDriver();
        driver.navigate().to( "http://index.hu" );
        File f = driver.getScreenshotAs( OutputType.FILE );
        System.out.printf( "%s\n", f.getAbsolutePath() );
    }

}
