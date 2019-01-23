#pragma once

#include "Connection.h"
#include "../Socket.h"

class ConnectConnectConnection : public Connection
{
private:
    Socket client_side_connection;
    Socket server_side_connection;

public:
    ConnectConnectConnection( const char* client_address, unsigned short client_port,
                              const char* server_address, unsigned short server_port );
    virtual ~ConnectConnectConnection();

    virtual void Manage();
};

