#include "lm92.h"

#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>
#include <fcntl.h>
#include <linux/i2c-dev.h>
#include <sys/ioctl.h>
#include <stdexcept>

using namespace std;

LM92::LM92( const string& device, short address )
{
    fd = open( device.c_str(), O_RDWR );
    if ( fd < 0 )
    {
        throw runtime_error( "Can't open I2C device: " + device + "\n" );
    }

    if ( ioctl( fd, I2C_SLAVE, address ) < 0 )
    {
        throw runtime_error( "Can't set I2C address: " + to_string( address ) + "\n" );
    }

    current_page = PAGE_TEMPERATURE;
}

LM92::~LM92()
{
    close( fd );
}

bool LM92::IsSensorSupported()
{
    return GetChipId() == MANUFACTURER_ID;
}

unsigned short LM92::Read16()
{
    if ( read( fd, buffer, 2 ) != 2 )
    {
        throw runtime_error( "Unable to read from device\n" );
    }
    return ((unsigned short)buffer[ 0 ] << 8 | buffer[ 1 ]);
}

void LM92::SwitchToPage( Page new_page )
{
    if ( current_page == new_page ) return;

    buffer[ 0 ] = new_page;
    if ( write( fd, buffer, 1 ) != 1 ) 
    {
        throw runtime_error( "Error setting page\n" );
    }
    current_page = new_page;
}

int LM92::GetChipId()
{
    SwitchToPage( PAGE_ID );

    return Read16();
}

double LM92::GetTemperature()
{
    SwitchToPage( PAGE_TEMPERATURE );

    unsigned short data = Read16();
    data >>= 3;
    if ( data & (1 << 12) ) // sign bit is 1
    {
        data -= 8192;
    }
    double temperature = data / 16.0;
    return temperature;
}
