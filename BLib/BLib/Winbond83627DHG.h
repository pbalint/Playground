#pragma once

#include "Port.h"

namespace BLib
{

class Winbond83627DHG
{
private:
    static const unsigned short chip_addresses[];
    unsigned short              index_reg;
    unsigned short              data_reg;

    unsigned short              hwm_base_address;
    unsigned short              hwm_address_port;
    unsigned short              hwm_data_port;

    Port                        port;

    void ConfigMode( bool on );
    void SelectDevice( unsigned char device_number );
    void WriteConfigRegister( unsigned char reg, unsigned char value );
    unsigned char ReadConfigRegister( unsigned char reg );
    void WriteHWMRegister( unsigned char bank, unsigned char reg, unsigned char value );
    unsigned char ReadHWMRegister( unsigned char bank, unsigned char reg );

    struct Address
    {
        unsigned char bank;
        unsigned char reg;
    };

    static const Address temp_sensor[];
    static const Address temp_offset[];
    static const Address fan_out[];
    static const Address fan_in[];

public:
    Winbond83627DHG( const String& driver = L"\\\\.\\BDrv" );

    bool ChipPresent();
    unsigned short GetRevision();

    enum Sensor { TEMP_CPU = 0, TEMP_AUX, TEMP_SYS };
    enum Fan { FAN_SYS = 0, FAN_CPU0, FAN_AUX, FAN_CPU1 };
    enum FanMode { FAN_MODE_PWM = 0, FAN_MODE_DC = 1 };

    char GetTemperature( Sensor sensor );
    void SetTemperatureOffset( Sensor sensor, char offset );
    void SetFan( Fan fan, unsigned char percent );
    void SetFanMode( Fan fan, FanMode mode );
    int GetFanSpeed( Fan fan );

    const unsigned char GetTemperatureSensorCount();
    const unsigned char GetFanCount();
};

}