#include <stdio.h>

#include "ListenConnectConnection.h"
#include "../Events.h"
#include "../DataTransferThread.h"
#include "../Util.h"

ListenConnectConnection::ListenConnectConnection( const char* client_address, unsigned short client_port,
                                                  const char* server_address, unsigned short server_port )
{
    client_side_socket.Listen( client_address, client_port );
    printf( "Connecting to server side control channel: %s:%i\n", server_address, server_port );
    server_side_connection.Connect( server_address, server_port );
    printf( "Connected to server side control channel: %s:%i\n", server_side_connection.GetRemoteAddress(), server_side_connection.GetRemotePort() );
}

ListenConnectConnection::~ListenConnectConnection()
{
}

void ListenConnectConnection::Manage()
{
    while ( true )
    {
        Socket* new_client_side_connection = WaitForNewClientSideConnection( client_side_socket );
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
