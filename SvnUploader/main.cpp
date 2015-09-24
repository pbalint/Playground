#include <time.h>
#include <stdio.h>
#include <WinSock2.h>
#include <ws2tcpip.h>

bool SourceforgeIsReachable()
{
    ADDRINFOA hints     = { 0 };
    hints.ai_family     = AF_INET;
    hints.ai_protocol   = IPPROTO_TCP;
    hints.ai_socktype   = SOCK_STREAM;
    ADDRINFOA* result;
    if ( getaddrinfo( "sourceforge.net", "80", &hints, &result ) != 0 ) return false;

    SOCKET s = socket( AF_INET, SOCK_STREAM, IPPROTO_TCP );
    if ( s == INVALID_SOCKET )
    {
        freeaddrinfo( result );
        return false;
    }
    if ( connect( s, result->ai_addr, sizeof( *result->ai_addr ) ) != 0 )
    {
        freeaddrinfo( result );
        closesocket( s );
        return false;
    }

    freeaddrinfo( result );
    closesocket( s );
    return true;
}

void WaitForSourceforgeToBeReachable()
{
    WSADATA wsa_data = { 0 };
    WSAStartup( MAKEWORD( 2, 2 ), &wsa_data );
    while ( !SourceforgeIsReachable() )
    {
        Sleep( 10000 );
    }
    WSACleanup();
}

int CALLBACK WinMain( HINSTANCE hInstance, HINSTANCE hPrevInstance, LPSTR lpCmdLine, int nCmdShow )
{
    time_t ts = time( 0 );

    WaitForSourceforgeToBeReachable();

    FILE* file;
    if ( fopen_s( &file, "log.txt", "w+" ) == 0 )
    {
        char buffer[ 4096 ];
        tm t;
        localtime_s( &t, &ts );
        int bytes_to_write = strftime( buffer, 4096, "%Y.%m.%d. %H:%M", &t );
        fwrite( buffer, bytes_to_write, 1, file );
        fclose( file );
        wchar_t command[ 4096 ];
        _snwprintf_s( command, 4096, L"svn commit -m \"%S\" --username apateszt --password apateszt", buffer );
        STARTUPINFO startup_info = { 0 };
        PROCESS_INFORMATION process_info = { 0 };
        if ( CreateProcess( NULL, command, NULL, NULL, false, CREATE_NO_WINDOW, NULL, NULL, &startup_info, &process_info ) != 0 )
        {
            WaitForSingleObject( process_info.hProcess, INFINITE );
            CloseHandle( process_info.hProcess );
            CloseHandle( process_info.hThread );
        }
    }

    return 0;
}