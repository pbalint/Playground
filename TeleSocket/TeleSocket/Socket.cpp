#include <stdio.h>
#include <memory.h>
#include <math.h>
#include <algorithm>

#include "Socket.h"
#include "Util.h"

#ifdef _WIN32
    #define WIN32_LEAN_AND_MEAN 
    #define NOMINMAX
    #include <WinSock2.h>
    #include <WS2tcpip.h>

    #define SHUT_WR SD_BOTH
#else
    #include <errno.h>
    #include <sys/types.h>
    #include <sys/socket.h>
    #include <arpa/inet.h>
    #include <netdb.h>
    #include <unistd.h>
#endif

static int GetLastErrorCode()
{
#ifdef _WIN32
    return ::GetLastError();
#else
    return errno;
#endif
}

static char* GetGaiErrorString( int error_code )
{
    char* buffer;
#ifdef _WIN32
    const char* message = gai_strerrorA( error_code );
    size_t message_length = 1 + strlen( message );
    buffer = new char[ message_length ];
    memcpy( buffer, message, message_length );
#else
    const char* message = gai_strerror( error_code );
    size_t message_length = 1 + strlen( message );
    buffer = new char[ message_length ];
    memcpy( buffer, message, message_length );
#endif
    return buffer;
}

static char* GetErrorString( int error_code )
{
    char* buffer;
#ifdef _WIN32
    char* message;
    size_t message_length = 1 + FormatMessageA(
        FORMAT_MESSAGE_ALLOCATE_BUFFER |
        FORMAT_MESSAGE_FROM_SYSTEM |
        FORMAT_MESSAGE_IGNORE_INSERTS,
        NULL,
        error_code,
        MAKELANGID( LANG_NEUTRAL, SUBLANG_DEFAULT ),
        (LPSTR)&message,
        0,
        NULL );
    buffer = new char[ message_length ];
    memcpy( buffer, message, message_length );
    LocalFree( message );
#else
    char* message = strerror_r( error_code, nullptr, 0 );
    size_t message_length = 1 + strlen( message );
    buffer = new char[ message_length ];
    memcpy( buffer, message, message_length );
#endif
    return buffer;
}

static void CloseSocket( unsigned long long fd )
{
#ifdef _WIN32 
        closesocket( fd );
#else
        ::close( fd );
#endif
}

static void PrintError( const char* function_name, int error_code )
{
    char* error_string = GetErrorString( error_code );
    printf( "%s error: %i %s\n", function_name, error_code, error_string );
    delete[] error_string;
}

static void PrintGaiError( int error_code )
{
    char* error_string = GetGaiErrorString( error_code );
    printf( "Getaddressinfo error: %i %s\n", error_code, error_string );
    delete[] error_string;
}

static char* GetAddressString( sockaddr* address )
{
    char* address_string = nullptr;
    if ( address->sa_family == AF_INET )
    {
        sockaddr_in* ipv4_address = (sockaddr_in*)address;
        unsigned char* address_ptr = (unsigned char*)&(ipv4_address->sin_addr);
        address_string = AllocString( "%u.%u.%u.%u", address_ptr[ 0 ], address_ptr[ 1 ], address_ptr[ 2 ], address_ptr[ 3 ] );
    }
    else if ( address->sa_family == AF_INET6 )
    {
        sockaddr_in6* ipv6_address = (sockaddr_in6*)address;
        unsigned char* address_ptr = (unsigned char*)&(ipv6_address->sin6_addr);
        address_string = AllocString( "%02x%02x:%02x%02x:%02x%02x:%02x%02x:%02x%02x:%02x%02x:%02x%02x:%02x%02x",
            address_ptr[ 0 ], address_ptr[ 1 ], address_ptr[ 2 ], address_ptr[ 3 ],
            address_ptr[ 4 ], address_ptr[ 5 ], address_ptr[ 6 ], address_ptr[ 7 ],
            address_ptr[ 8 ], address_ptr[ 9 ], address_ptr[ 10 ], address_ptr[ 11 ],
            address_ptr[ 12 ], address_ptr[ 13 ], address_ptr[ 14 ], address_ptr[ 15 ] );
    }
    return address_string;
}

static unsigned short GetPort( sockaddr* address )
{
    if ( address->sa_family == AF_INET )
    {
        sockaddr_in* ipv4_address = (sockaddr_in*)address;
        return ntohs( ipv4_address->sin_port );
    }
    else if ( address->sa_family == AF_INET6 )
    {
        sockaddr_in6* ipv6_address = (sockaddr_in6*)address;
        return ntohs( ipv6_address->sin6_port );
    }
    return 0;
}

static void PopulateLocalConnectionInfo( unsigned long long fd, char*& address, unsigned short& port )
{
    sockaddr info;
    socklen_t addr_size = sizeof( info );

    int rc = getsockname( fd, &info, &addr_size );
    if ( rc )
    {
        PrintError( "Getsockname", GetLastErrorCode() );
    }
    address = GetAddressString( &info );
    port = GetPort( &info );
}

static void PopulateRemoteConnectionInfo( unsigned long long fd, char*& address, unsigned short& port )
{
    sockaddr info;
    socklen_t addr_size = sizeof( info );

    int rc = getpeername( fd, &info, &addr_size );
    if ( rc )
    {
        PrintError( "Getpeername", GetLastErrorCode() );
    }
    address = GetAddressString( &info );
    port = GetPort( &info );
}

static addrinfo* get_address_info( const char* host_name, unsigned short port, int address_family )
{
    const int buffer_size = 16;
    char port_string[ buffer_size ];
    snprintf( port_string, buffer_size, "%i", port );

    addrinfo hints;
    memset( &hints, 0, sizeof( hints ) );
    hints.ai_family = address_family;
    hints.ai_socktype = SOCK_STREAM;
    hints.ai_protocol = IPPROTO_TCP;

    addrinfo *addresses = nullptr;
    int rc = getaddrinfo( host_name, port_string, &hints, &addresses );
    if ( rc )
    {
        PrintGaiError( GetLastErrorCode() );
    }
    return addresses;
}

void Socket::StartUp()
{
#ifdef _WIN32
    WSADATA data;
    int rc = WSAStartup( MAKEWORD( 2, 2 ), &data );
    if ( rc )
    {
        printf( "WSAStartup failed: %i\n", rc );
    }
#endif
}

void Socket::ShutDown()
{
#ifdef _WIN32 
    WSACleanup();
#endif
}

Socket::Socket( Type type )
{
    this->type = type;
}

Socket::Socket( unsigned long long fd, Type type )
{
    this->fd = fd;
    this->type = type;
}

Socket::~Socket()
{
    Close();
}

Socket* Socket::CreateAndConnect( const char* host_name, unsigned short port, Type type )
{
    Socket* socket = new Socket( type );
    socket->Connect( host_name, port );
    return socket;
}

const char* Socket::GetLocalAddress()
{
    return local_address;
}

unsigned short Socket::GetLocalPort()
{
    return local_port;
}

const char* Socket::GetRemoteAddress()
{
    return remote_address;
}

unsigned short Socket::GetRemotePort()
{
    return remote_port;
}

void Socket::Close()
{
    if ( fd != -1 )
    {
        unsigned long long temp_fd = fd;
        fd = (unsigned long long) - 1;

        delete[] local_address;
        local_address = nullptr;
        delete[] remote_address;
        remote_address = nullptr;

        shutdown( temp_fd, SHUT_WR );
        CloseSocket( temp_fd );
    }
}

void Socket::Connect( const char* host_name, unsigned short port )
{
    addrinfo* addresses = get_address_info( host_name, port, type );
    for ( addrinfo* ptr = addresses; ptr != nullptr; ptr = ptr->ai_next )
    {

        fd = socket( ptr->ai_family, ptr->ai_socktype, ptr->ai_protocol );
        if ( fd == -1 )
        {
            PrintError( "Socket", GetLastErrorCode() );
            continue;
        }
        int rc = ::connect( fd, ptr->ai_addr, (int)ptr->ai_addrlen );
        if ( rc )
        {
            PrintError( "Connect", GetLastErrorCode() );
            CloseSocket( fd );
            continue;
        }

        PopulateLocalConnectionInfo( fd, local_address, local_port );
        PopulateRemoteConnectionInfo( fd, remote_address, remote_port );
        break;
    }

    freeaddrinfo( addresses );
}

void Socket::Listen( const char* interface_name, unsigned short port )
{
    addrinfo* addresses = get_address_info( interface_name, port, type );
    for ( addrinfo* ptr = addresses; ptr != nullptr; ptr = ptr->ai_next )
    {
        fd = socket( ptr->ai_family, ptr->ai_socktype, ptr->ai_protocol );
        if ( fd == -1 )
        {
            PrintError( "Socket", GetLastErrorCode() );
            continue;
        }

        int rc = bind( fd, ptr->ai_addr, (int)ptr->ai_addrlen );
        if ( rc == -1 ) {
            PrintError( "Bind", GetLastErrorCode() );
            CloseSocket( fd );
            continue;
        }

        rc = ::listen( fd, 100 );
        if ( rc == -1 ) {
            PrintError( "Listen", GetLastErrorCode() );
            CloseSocket( fd );
            continue;
        }

        PopulateLocalConnectionInfo( fd, local_address, local_port );
        break;
    }

    freeaddrinfo( addresses );
    connection_type = SERVER;
}

void Socket::SetKeepalive( bool enabled )
{
    int value = enabled ? 1 : 0;
    int rc = setsockopt( fd, SOL_SOCKET, SO_KEEPALIVE, (char*)&value, sizeof( value ) );
    if ( rc < 0 )
    {
        PrintError( "Set keepalive", GetLastErrorCode() );
    }
}

Socket* Socket::Accept()
{
    connection_type = SERVER;

    unsigned long long client_fd = ::accept( fd, nullptr, nullptr );
    if ( client_fd == -1 )
    {
        PrintError( "Accept", GetLastErrorCode() );
        return nullptr;
    }
    Socket* client = new Socket( client_fd, type );
    client->connection_type = CLIENT;
    PopulateLocalConnectionInfo( client_fd, client->local_address, client->local_port );
    PopulateRemoteConnectionInfo( client_fd, client->remote_address, client->remote_port );
    return client;
}

long long Socket::Send( char* buffer, unsigned int buffer_size, unsigned int send_size )
{
    long long bytes_sent = 0;
    while ( buffer_size  > 0 )
    {
        send_size = std::min( buffer_size, send_size );
        int rc = ::send( fd, buffer + bytes_sent, send_size, 0 );

        if ( rc == 0 ) return bytes_sent;
        if ( rc < 0 )
        {
            PrintError( "Send", GetLastErrorCode() );
            return -1;
        }

        bytes_sent += rc;
        buffer_size -= rc;
    }
    return bytes_sent;
}

long long Socket::Receive( char* buffer, unsigned int buffer_size )
{
    int rc = ::recv( fd, buffer, buffer_size, 0 );

    if ( rc < 0 )
    {
        PrintError( "Receive", GetLastErrorCode() );
        return -1;
    }
    return rc;
}

long long Socket::ReceiveFullBuffer( char* buffer, unsigned int buffer_size, unsigned int receive_size )
{
    long long bytes_received = 0;
    while ( buffer_size > 0 )
    {
        receive_size = std::min( buffer_size, receive_size );
        int rc = ::recv( fd, buffer + bytes_received, receive_size, 0 );

        if ( rc == 0 ) return bytes_received;
        if ( rc < 0 )
        {
            PrintError( "Receive", GetLastErrorCode() );
            return -1;
        }

        bytes_received += rc;
        buffer_size -= rc;
    }
    return bytes_received;
}

