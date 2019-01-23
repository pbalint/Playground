#include <stdio.h>

#include "ConnectListenConnection.h"
#include "../Events.h"
#include "../DataTransferThread.h"
#include "../Util.h"

ConnectListenConnection::ConnectListenConnection( const char* client_address, unsigned short client_port,
                                                  const char* server_address, unsigned short server_port )
{
    printf( "Connecting to control channel: %s:%i\n", client_address, client_port );
    client_side_connection.Connect( client_address, client_port );
    printf( "Connected control channel: %s:%i\n", client_side_connection.GetRemoteAddress(), client_side_connection.GetRemotePort() );
    server_side_socket.Listen( server_address, server_port );
}

ConnectListenConnection::~ConnectListenConnection()
{
}

void ConnectListenConnection::Manage()
{
    printf( "Waiting for server side control connection on: %s:%i\n", server_side_socket.GetLocalAddress(), server_side_socket.GetLocalPort() );
    Socket* server_side_connection = server_side_socket.Accept();
    printf( "Accepted server side control connection: %s:%i\n", server_side_connection->GetRemoteAddress(), server_side_connection->GetRemotePort() );
    while ( true )
    {
        Socket* new_client_side_connection = WaitForNewClientSideConnectionEvent( client_side_connection );
        if ( !new_client_side_connection ) break;
        Socket* new_server_side_connection = RequestNewServerSideConnection( server_side_connection, server_side_socket, new_client_side_connection );

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
    delete server_side_connection;
}
