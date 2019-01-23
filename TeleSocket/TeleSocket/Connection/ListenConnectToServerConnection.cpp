#include <stdio.h>

#include "ListenConnectToServerConnection.h"
#include "../Events.h"
#include "../DataTransferThread.h"
#include "../Util.h"

ListenConnectToServerConnection::ListenConnectToServerConnection( const char* client_address, unsigned short client_port,
                                                                  const char* server_address, unsigned short server_port )
{
    client_side_socket.Listen( client_address, client_port );
    this->server_address = server_address;
    this->server_port = server_port;
}

ListenConnectToServerConnection::~ListenConnectToServerConnection()
{
}

void ListenConnectToServerConnection::Manage()
{
    while ( true )
    {
        Socket* new_client_side_connection = WaitForNewClientSideConnection( client_side_socket );
        if ( !new_client_side_connection ) break;
        Socket* new_server_side_connection = Socket::CreateAndConnect( server_address, server_port );

        if ( new_client_side_connection && new_client_side_connection )
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
