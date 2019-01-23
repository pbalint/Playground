#pragma once

#include "Connection.h"
#include "../Socket.h"

class ListenConnectConnection : public Connection
{
private:
    Socket client_side_socket;
    Socket server_side_connection;

public:
    ListenConnectConnection( const char* client_address, unsigned short client_port,
                             const char* server_address, unsigned short server_port );
    virtual ~ListenConnectConnection();

    virtual void Manage();
};

