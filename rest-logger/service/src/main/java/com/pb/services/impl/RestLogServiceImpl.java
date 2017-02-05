package com.pb.services.impl;

import java.util.Collection;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.pb.entities.RestLogEntry;
import com.pb.repositories.RestLogRepository;
import com.pb.services.RestLogService;

@Service
public class RestLogServiceImpl implements RestLogService {
    @Autowired
    private RestLogRepository restLogRepository;

    @Override
    public void saveLogEntry( RestLogEntry restLogEntry ) {
        restLogRepository.save( restLogEntry );
    }

    @Override
    public void saveLogEntries( Collection< RestLogEntry > restLogEntries ) {
        restLogRepository.save( restLogEntries );
    }
}
