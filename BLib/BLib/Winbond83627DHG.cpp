#include "Winbond83627DHG.h"

namespace BLib
{

const unsigned short Winbond83627DHG::chip_addresses[] = { 0x2e, 0x4e };

const Winbond83627DHG::Address Winbond83627DHG::temp_sensor[] =
{
    { 1, 0x50 }, // CPUTIN
    { 2, 0x50 }, // AUXTIN
    { 0, 0x27 }  // SYSTIN
};

const Winbond83627DHG::Address Winbond83627DHG::temp_offset[] =
{
    { 4, 0x55 }, // CPUTIN
    { 4, 0x56 }, // AUXTIN
    { 4, 0x54 }  // SYSTIN
};

const Winbond83627DHG::Address Winbond83627DHG::fan_out[] =
{
    { 0, 0x01 }, // SYSFANOUT
    { 0, 0x03 }, // CPUFANOUT0
    { 0, 0x11 }, // AUXFANOUT
    { 0, 0x61 }  // CPUFANOUT1
};

const Winbond83627DHG::Address Winbond83627DHG::fan_in[] =
{
    { 0, 0x28 }, // SYSFANIN
    { 0, 0x29 }, // CPUFANIN0
    { 0, 0x2a }, // AUXFANIN
    { 0, 0x3f }  // CPUFANIN1
};

Winbond83627DHG::Winbond83627DHG( const String& driver )
{
    port.Open( driver );
    index_reg   = chip_addresses[ 0 ];
    data_reg    = index_reg + 1;
    int i       = 0;
    while ( i < sizeof( chip_addresses ) / sizeof( chip_addresses[ 0 ] ) )
    {
        if ( ChipPresent() )
        {
            ConfigMode( true );
            SelectDevice( 0x0b );
            hwm_base_address = ReadConfigRegister( 0x60 ) << 8;
            hwm_base_address |= ReadConfigRegister( 0x61 );
            ConfigMode( false );

            hwm_address_port = hwm_base_address + 5;
            hwm_data_port    = hwm_address_port + 1;

            /*WriteHWMRegister( 0, 0x04, 3 );     // CPUFANOUT0, SYSFANOUT manual, DC
            WriteHWMRegister( 0, 0x12, 1 );     // AUXFANOUT manual, DC
            WriteHWMRegister( 0, 0x62, 0x40 );  // CPUFANOUT1 manual, DC*/
            break;
        }
        else
        {
            i++;
            index_reg = chip_addresses[ i ];
            data_reg = index_reg + 1;
        }
    }
}

bool Winbond83627DHG::ChipPresent()
{
    const unsigned int revision = GetRevision()&(~0x0f);
    return  revision == 0xa020 ||  // 83627DHG
            revision == 0xb070;    // 83627DHG-P
}

unsigned short Winbond83627DHG::GetRevision()
{
    unsigned short revision;
    ConfigMode( true );

    port.Write1( index_reg, 0x20 );
    revision = port.Read1( data_reg ) << 8;

    port.Write1( index_reg, 0x21 );
    revision |= port.Read1( data_reg );

    ConfigMode( false );

    return revision;
}

void Winbond83627DHG::ConfigMode( bool on )
{
    if ( on )
    {
        port.Write1( index_reg, 0x87 );
        port.Write1( index_reg, 0x87 );
    }
    else
    {
        port.Write1( index_reg, 0xaa );
    }
}

void Winbond83627DHG::SelectDevice( unsigned char device_number )
{
    port.Write1( index_reg, 0x07 );
    port.Write1( data_reg, device_number );
}

void Winbond83627DHG::WriteConfigRegister( unsigned char reg, unsigned char value )
{
    port.Write1( index_reg, reg );
    port.Write1( data_reg, value );
}

unsigned char Winbond83627DHG::ReadConfigRegister( unsigned char reg )
{
    port.Write1( index_reg, reg );
    return port.Read1( data_reg );
}

void Winbond83627DHG::WriteHWMRegister( unsigned char bank, unsigned char reg, unsigned char value )
{
    port.Write1( hwm_address_port, 0x4e );
    port.Write1( hwm_data_port, 0x80 + bank );

    port.Write1( hwm_address_port, reg );
    port.Write1( hwm_data_port, value );
}

unsigned char Winbond83627DHG::ReadHWMRegister( unsigned char bank, unsigned char reg )
{
    port.Write1( hwm_address_port, 0x4e );
    port.Write1( hwm_data_port, 0x80 + bank );

    port.Write1( hwm_address_port, reg );
    return port.Read1( hwm_data_port );
}

const unsigned char Winbond83627DHG::GetTemperatureSensorCount()
{
    return sizeof( temp_sensor ) / sizeof( temp_sensor[0] );
}

const unsigned char Winbond83627DHG::GetFanCount()
{
    return sizeof( fan_out ) / sizeof( fan_out[0] );
}

char Winbond83627DHG::GetTemperature( Sensor sensor )
{
    return ReadHWMRegister( temp_sensor[ sensor ].bank, temp_sensor[ sensor ].reg );
}

void Winbond83627DHG::SetFan( Fan fan, unsigned char percent )
{
    WriteHWMRegister( fan_out[ fan ].bank, fan_out[ fan ].reg, min( percent, 100 ) * 255 / 100 );
}

void Winbond83627DHG::SetTemperatureOffset( Sensor sensor, char offset )
{
    WriteHWMRegister( temp_offset[ sensor ].bank, temp_offset[ sensor ].reg, offset );
}

void Winbond83627DHG::SetFanMode( Fan fan, FanMode mode )
{
    int reg = 0;
    switch ( fan )
    {
        case FAN_CPU0:
            reg = ( ReadHWMRegister( 0, 4 ) & 0xfd ) + ( mode << 1);
            WriteHWMRegister( 0, 4, (unsigned char)reg );
            break;
        case FAN_CPU1:
            reg = ( ReadHWMRegister( 0, 0x62 ) & 0xbf ) + ( mode << 6);
            WriteHWMRegister( 0, 0x62, (unsigned char)reg );
            break;
        case FAN_SYS:
            reg = ( ReadHWMRegister( 0, 4 ) & 0xfe ) + mode;
            WriteHWMRegister( 0, 4, (unsigned char)reg );
            break;
        case FAN_AUX:
            reg = ( ReadHWMRegister( 0, 0x12 ) & 0xfe ) + mode;
            WriteHWMRegister( 0, 0x12, (unsigned char)reg );
            break;
    }
}

int Winbond83627DHG::GetFanSpeed( Fan fan )
{
    const int count = max( 1, ReadHWMRegister( fan_in[ fan ].bank, fan_in[ fan ].reg ) );
    if ( count == 255 ) return 0;

    int divisor = 1;
    switch ( fan )
    {
        case FAN_CPU0:
            divisor <<=
            (((ReadHWMRegister( 0, 0x47 )>>6)&3) + 
             ((ReadHWMRegister( 0, 0x5d )>>4)&4));
            break;

        case FAN_CPU1:
            divisor <<=
            ((ReadHWMRegister( 0, 0x49 )&3) + 
            ((ReadHWMRegister( 0, 0x4c )>>5)&4));
            break;

        case FAN_SYS:
            divisor <<=
            (((ReadHWMRegister( 0, 0x47 )>>4)&3) + 
             ((ReadHWMRegister( 0, 0x5d )>>3)&4));
            break;

        case FAN_AUX:
            divisor <<=
            (((ReadHWMRegister( 0, 0x4b )>>6)&3) + 
             ((ReadHWMRegister( 0, 0x5d )>>5)&4));
            break;
    }

    return 1350000 / ( count * divisor );
}

}