#include "Port.h"
#include <WinIoCtl.h>
#include "Bdrv\BDrv.h"

namespace BLib
{

Port::Port() : handle( INVALID_HANDLE_VALUE ) {}

Port::~Port()
{
    Close();
}

bool Port::Open( const String& driver )
{
    handle = CreateFileW( driver, GENERIC_READ, FILE_SHARE_READ | FILE_SHARE_WRITE, 0, OPEN_EXISTING, 0, 0 );
    return IsOpen();
}

void Port::Close()
{
    if ( handle != INVALID_HANDLE_VALUE )
    {
        CloseHandle( handle );
        handle = INVALID_HANDLE_VALUE;
    }
}

bool Port::IsOpen()
{
    return handle != INVALID_HANDLE_VALUE;
}

unsigned char Port::Read1( unsigned short port )
{
    DWORD bytes_returned;

    BDRV::Call call;
    call.type                = BDRV::PortRead1;
    call.params.port.address = port;
    DeviceIoControl( handle, BDRV_IOCTL_CALL, &call, sizeof( call ), &call, sizeof( call ), &bytes_returned, NULL );
    return (unsigned char)call.params.port.value;
}

unsigned short Port::Read2( unsigned short port )
{
    DWORD bytes_returned;

    BDRV::Call call;
    call.type       = BDRV::PortRead4;
    call.params.port.address    = port;
    DeviceIoControl( handle, BDRV_IOCTL_CALL, &call, sizeof( call ), &call, sizeof( call ), &bytes_returned, NULL );
    return (unsigned short)call.params.port.value;
}

unsigned int Port::Read4( unsigned short port )
{
    DWORD bytes_returned;

    BDRV::Call call;
    call.type       = BDRV::PortRead4;
    call.params.port.address    = port;
    DeviceIoControl( handle, BDRV_IOCTL_CALL, &call, sizeof( call ), &call, sizeof( call ), &bytes_returned, NULL );
    return call.params.port.value;
}


void Port::Write1( unsigned short port, unsigned char value )
{
    DWORD bytes_returned;

    BDRV::Call call;
    call.type       = BDRV::PortWrite1;
    call.params.port.address    = port;
    call.params.port.value      = value;
    DeviceIoControl( handle, BDRV_IOCTL_CALL, &call, sizeof( call ), &call, sizeof( call ), &bytes_returned, NULL );
}

void Port::Write2( unsigned short port, unsigned short value )
{
    DWORD bytes_returned;

    BDRV::Call call;
    call.type       = BDRV::PortWrite2;
    call.params.port.address    = port;
    call.params.port.value      = value;
    DeviceIoControl( handle, BDRV_IOCTL_CALL, &call, sizeof( call ), &call, sizeof( call ), &bytes_returned, NULL );
}

void Port::Write4( unsigned short port, unsigned int value )
{
    DWORD bytes_returned;

    BDRV::Call call;
    call.type       = BDRV::PortWrite4;
    call.params.port.address    = port;
    call.params.port.value      = value;
    DeviceIoControl( handle, BDRV_IOCTL_CALL, &call, sizeof( call ), &call, sizeof( call ), &bytes_returned, NULL );
}

}
