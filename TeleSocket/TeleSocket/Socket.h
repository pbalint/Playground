#pragma once

class Socket
{
public:
    enum Type { IPV4 = 2, IPV6 = 23 };
    enum ConnectionType { UNDEFINED = 0, CLIENT, SERVER };

private:
    Type                type;
    ConnectionType      connection_type     = UNDEFINED;
    unsigned long long  fd                  = (unsigned int)-1;
    char*               local_address       = nullptr;
    char*               remote_address      = nullptr;
    unsigned short      local_port          = 0;
    unsigned short      remote_port         = 0;

    Socket( unsigned long long fd, Type type = IPV4 );

public:
    static void StartUp();
    static void ShutDown();
    static Socket* CreateAndConnect( const char* host_name, unsigned short port, Type type = IPV4 );

    Socket( Type type = IPV4 );
    void Connect( const char* host_name, unsigned short port );
    void Listen( const char* interface_name, unsigned short port );
    void SetKeepalive( bool enabled );
    Socket* Accept();
    long long Send( char* buffer, unsigned int buffer_size, unsigned int send_size = 65536 );
    long long Receive( char* buffer, unsigned int buffer_size );
    long long ReceiveFullBuffer( char* buffer, unsigned int buffer_size, unsigned int receive_size = 65536 );
    void Close();
    virtual ~Socket();

    const char* GetLocalAddress();
    unsigned short GetLocalPort();
    const char* GetRemoteAddress();
    unsigned short GetRemotePort();
};

