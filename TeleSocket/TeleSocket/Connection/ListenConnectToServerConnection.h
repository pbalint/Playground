#pragma once

#include "Connection.h"
#include "../Socket.h"

class ListenConnectToServerConnection : public Connection
{
private:
    Socket          client_side_socket;
    const char*     server_address;
    unsigned short  server_port;

public:
    ListenConnectToServerConnection( const char* client_address, unsigned short client_port,
                                     const char* server_address, unsigned short server_port );
    virtual ~ListenConnectToServerConnection();

    virtual void Manage();
};

