#pragma once

#include "String.h"
#include <windows.h>

namespace BLib
{

class Port
{
protected:
    HANDLE handle;

public:
    Port();
    ~Port();
    bool Open( const String& driver = L"\\\\.\\BDrv" );
    bool IsOpen();
    void Close();

    unsigned char  Read1( unsigned short port );
    unsigned short Read2( unsigned short port );
    unsigned int   Read4( unsigned short port );

    void Write1( unsigned short port, unsigned char value );
    void Write2( unsigned short port, unsigned short value );
    void Write4( unsigned short port, unsigned int value );
};

}

