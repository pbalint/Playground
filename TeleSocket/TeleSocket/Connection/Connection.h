#pragma once

#include "../Socket.h"

class Connection
{
protected:
    Socket* WaitForNewClientSideConnection( Socket&  client_side_socket );
    Socket* WaitForNewClientSideConnectionEvent( Socket&  client_side_connection );
    Socket* RequestNewServerSideConnection( Socket*  server_side_connection,
                                            Socket&  server_side_socket,
                                            Socket*& new_client_side_connection );

public:
    Connection();
    virtual ~Connection();

    virtual void Manage() = 0;
};

