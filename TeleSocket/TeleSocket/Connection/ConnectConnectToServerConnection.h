#pragma once

#include "Connection.h"
#include "../Socket.h"

class ConnectConnectToServerConnection : public Connection
{
private:
    Socket          client_side_connection;
    const char*     server_address;
    unsigned short  server_port;

public:
    ConnectConnectToServerConnection( const char* client_address, unsigned short client_port,
                                      const char* server_address, unsigned short server_port );
    virtual ~ConnectConnectToServerConnection();

    virtual void Manage();
};

