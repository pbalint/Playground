#include <stdio.h>

#include "Connection.h"
#include "../Util.h"
#include "../Events.h"

Socket* Connection::WaitForNewClientSideConnection( Socket& client_side_socket )
{
    Socket* new_client_side_connection = client_side_socket.Accept();
    if ( new_client_side_connection == nullptr ) {
        printf( "Error accepting new client connection!\n" );
        return nullptr;
    }
    printf( "Accepted new client connection!\n" );
    return new_client_side_connection;
}

Socket* Connection::WaitForNewClientSideConnectionEvent( Socket& client_side_connection )
{
    int control_event;
    long long rc = client_side_connection.Receive( (char*)&control_event, sizeof( control_event ) );
    if ( rc == sizeof( control_event ) && control_event == ControlEvent::NEW_CONNECTION )
    {
        return Socket::CreateAndConnect( client_side_connection.GetRemoteAddress(), client_side_connection.GetRemotePort() );
    }
    return nullptr;
}

Socket* Connection::RequestNewServerSideConnection( Socket* server_side_connection,
                                                    Socket& server_side_socket,
                                                    Socket*& new_client_side_connection )
{
    int new_connection_event = ControlEvent::NEW_CONNECTION;
    long long rc = server_side_connection->Send( (char*)&new_connection_event, sizeof( new_connection_event ) );
    if ( rc < 0 ) {
        printf( "Error sending new connection event!" );
        delete new_client_side_connection;
        new_client_side_connection = nullptr;
        return nullptr;
    }
    Socket* new_server_side_socket = server_side_socket.Accept();
    if ( new_server_side_socket == nullptr ) {
        printf( "Error accepting new server connection!" );
        delete new_client_side_connection;
        new_client_side_connection = nullptr;
        return nullptr;
    }
    printf( "Accepted new server connection!\n" );
    return new_server_side_socket;
}

Connection::Connection()
{
}

Connection::~Connection()
{
}
