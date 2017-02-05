package com.pb.services;

import java.util.Collection;

import com.pb.entities.RestLogEntry;

public interface RestLogService {
    void saveLogEntry( RestLogEntry restLogEntry );
    void saveLogEntries( Collection< RestLogEntry > restLogEntries);
}