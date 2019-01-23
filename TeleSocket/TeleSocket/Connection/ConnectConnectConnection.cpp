#include <stdio.h>

#include "ConnectConnectConnection.h"
#include "../Events.h"
#include "../DataTransferThread.h"
#include "../Util.h"

ConnectConnectConnection::ConnectConnectConnection( const char* client_address, unsigned short client_port,
                                                    const char* server_address, unsigned short server_port )
{
    printf( "Connecting to client side control channel: %s:%i\n", client_address, client_port );
    client_side_connection.Connect( client_address, client_port );
    printf( "Connected to client side control channel: %s:%i\n", client_side_connection.GetRemoteAddress(), client_side_connection.GetRemotePort() );
    printf( "Connecting to server side control channel: %s:%i\n", server_address, server_port );
    server_side_connection.Connect( server_address, server_port );
    printf( "Connected to server side control channel: %s:%i\n", server_side_connection.GetRemoteAddress(), server_side_connection.GetRemotePort() );
}

ConnectConnectConnection::~ConnectConnectConnection()
{
}

void ConnectConnectConnection::Manage()
{
    while ( true )
    {
        Socket* new_client_side_connection = WaitForNewClientSideConnectionEvent( client_side_connection );
        if ( !new_client_side_connection ) break;
        Socket* new_server_side_connection = Socket::CreateAndConnect( server_side_connection.GetRemoteAddress(), server_side_connection.GetRemotePort() );

        if ( new_client_side_connection && new_server_side_connection )
        {
            (new DataTransferThread( new_client_side_connection, new_server_side_connection ))->Start();
            (new DataTransferThread( new_server_side_connection, new_client_side_connection ))->Start();
        }
        else
        {
            break;
        }
    }
}

