#include <stdio.h>
#include <stdlib.h>
#include <string.h> 

#include "Thread.h"
#include "Socket.h"
#include "Connection/ConnectConnectConnection.h"
#include "Connection/ListenListenConnection.h"
#include "Connection/ConnectListenConnection.h"
#include "Connection/ListenConnectConnection.h"
#include "Connection/ConnectConnectToServerConnection.h"
#include "Connection/ListenConnectToServerConnection.h"
#include "Util.h"

unsigned short GetPort( const char* number_string )
{
    return (unsigned short)strtol( number_string, nullptr, 10 );
}

int main( int argc, const char* argv[] )
{
    if ( argc < 6 )
    {
        printf( "Usage: %s <mode> <client address> <client port> <server address> <server port>\n", argv[ 0 ] );
        return -1;
    }
    
    Socket::StartUp();

    Connection* connection = nullptr;
    if ( !stricompare( argv[ 1 ], "cc" ) )
    {
        connection = new ConnectConnectConnection( argv[ 2 ], GetPort( argv[ 3 ] ), argv[ 4 ], GetPort( argv[ 5 ] ) );
    }
    else if ( !stricompare( argv[ 1 ], "ll" ) )
    {
        connection = new ListenListenConnection( argv[ 2 ], GetPort( argv[ 3 ] ), argv[ 4 ], GetPort( argv[ 5 ] ) );
    }
    else if ( !stricompare( argv[ 1 ], "cl" ) )
    {
        connection = new ConnectListenConnection( argv[ 2 ], GetPort( argv[ 3 ] ), argv[ 4 ], GetPort( argv[ 5 ] ) );
    }
    else if ( !stricompare( argv[ 1 ], "lc" ) )
    {
        connection = new ListenConnectConnection( argv[ 2 ], GetPort( argv[ 3 ] ), argv[ 4 ], GetPort( argv[ 5 ] ) );
    }
    else if ( !stricompare( argv[ 1 ], "ccs" ) )
    {
        connection = new ConnectConnectToServerConnection( argv[ 2 ], GetPort( argv[ 3 ] ), argv[ 4 ], GetPort( argv[ 5 ] ) );
    }
    else if ( !stricompare( argv[ 1 ], "lcs" ) )
    {
        connection = new ListenConnectToServerConnection( argv[ 2 ], GetPort( argv[ 3 ] ), argv[ 4 ], GetPort( argv[ 5 ] ) );
    }
    if ( connection )
    {
        connection->Manage();
    }

    Socket::ShutDown();
    return 0;
}

