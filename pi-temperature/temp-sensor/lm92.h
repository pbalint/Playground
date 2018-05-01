#pragma once

#include <string>

class LM92
{
private:
    static const int MANUFACTURER_ID = 0x8001;
    enum Page{ PAGE_TEMPERATURE = 0,
               PAGE_CONFIGURATION,
               PAGE_TEMP_HYSTERESIS,
               PAGE_TEMP_CRITICAL,
               PAGE_TEMP_HIGH,
               PAGE_TEMP_LOW,
               PAGE_ID = 7 };

    Page    current_page;
    int     fd;
    char    buffer[2];

    void SwitchToPage( Page new_page );
    unsigned short Read16();

public:
    LM92( const std::string& device, short address );
    virtual ~LM92();
    bool IsSensorSupported();
    int GetChipId();
    double GetTemperature();
};