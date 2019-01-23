#pragma once

#include "Connection.h"
#include "../Socket.h"

class ConnectListenConnection : public Connection
{
private:
    Socket client_side_connection;
    Socket server_side_socket;

public:
    ConnectListenConnection( const char* client_address, unsigned short client_port,
                             const char* server_address, unsigned short server_port );
    virtual ~ConnectListenConnection();

    virtual void Manage();
};

