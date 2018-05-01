package com.pb.measurement.controller;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.ResponseBody;

import com.pb.measurement.domain.Config;
import com.pb.measurement.service.MeasurementService;

@Controller
public class MeasurementController {
    @Autowired
    private MeasurementService measurementService;
    
    @Autowired
    private Config config;

    @RequestMapping( value = "/measurements", method = RequestMethod.GET )
    public String temperature() {
        return "measurements";
    }

    @RequestMapping( value = "/points", method = RequestMethod.GET )
    @ResponseBody
    public List< Double[] > getPoints( @RequestParam() double startTs ) {
        return measurementService.getMeasurementsFrom( startTs );
    }
    
    @RequestMapping( value = "/configuration", method = RequestMethod.GET )
    @ResponseBody
    public Config getConfig() {
        return config;
    }

    @RequestMapping( value = "/compact", method = RequestMethod.POST )
    @ResponseBody
    public void compactData( @RequestParam int pointsToAverage,
                             @RequestParam(required = false, defaultValue = "0") double startTs,
                             @RequestParam(required = false, defaultValue = "0") double endTs ) {
        measurementService.compactData( pointsToAverage, startTs, endTs );
    }
}
