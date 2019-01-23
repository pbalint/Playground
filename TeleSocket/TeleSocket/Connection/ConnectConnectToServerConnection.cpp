#include <stdio.h>

#include "ConnectConnectToServerConnection.h"
#include "../Events.h"
#include "../DataTransferThread.h"
#include "../Util.h"

ConnectConnectToServerConnection::ConnectConnectToServerConnection( const char* client_address, unsigned short client_port,
                                                                    const char* server_address, unsigned short server_port )
{
    printf( "Connecting to control channel: %s:%i\n", client_address, client_port );
    client_side_connection.Connect( client_address, client_port );
    printf( "Connected control channel: %s:%i\n", client_side_connection.GetRemoteAddress(), client_side_connection.GetRemotePort() );
    this->server_address = server_address;
    this->server_port = server_port;
}

ConnectConnectToServerConnection::~ConnectConnectToServerConnection()
{
}

void ConnectConnectToServerConnection::Manage()
{
    while ( true )
    {
        Socket* new_client_side_connection = WaitForNewClientSideConnectionEvent( client_side_connection );
        if ( !new_client_side_connection ) break;
        Socket* new_server_side_connection = Socket::CreateAndConnect( server_address, server_port );

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

