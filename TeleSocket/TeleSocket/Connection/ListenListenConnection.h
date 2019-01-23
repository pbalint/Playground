#pragma once

#include "Connection.h"
#include "../Socket.h"

class ListenListenConnection : public Connection
{
private:
    Socket client_side_socket;
    Socket server_side_socket;

public:
    ListenListenConnection( const char* client_address, unsigned short client_port,
                            const char* server_address, unsigned short server_port );
    virtual ~ListenListenConnection();

    virtual void Manage();
};

