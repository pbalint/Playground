#include "Device.h"

#include <Windows.h>
#include "WinUtils.h"


using namespace std;

Device::Device( const std::wstring& name, IBaseFilter* device )
    :
    name( name ),
    device( device )
{
}

void Device::QueryPins()
{
    IEnumPins* pin_enumerator;
    if ( SUCCEEDED( device->EnumPins( &pin_enumerator ) ) )
    {
        IPin* raw_pin;
        while ( pin_enumerator->Next( 1, &raw_pin, 0 ) == 0 )
        {
            PIN_DIRECTION pin_direction;
            if ( SUCCEEDED( raw_pin->QueryDirection( &pin_direction ) ) )
            {
                shared_ptr< Pin > pin = AddPin( raw_pin, pin_direction == PINDIR_INPUT ? Pin::In : Pin::Out );

                IAMStreamConfig* stream_config;
                if ( SUCCEEDED( raw_pin->QueryInterface( IID_PPV_ARGS( &stream_config ) ) ) )
                {
                    int caps_count;
                    int caps_size;
                    stream_config->GetNumberOfCapabilities( &caps_count, &caps_size );
                    unsigned char* buffer = new unsigned char[ caps_size ];
                    for ( int i = 0; i < caps_count; i++ )
                    {
                        AM_MEDIA_TYPE* media_type;
                        if ( SUCCEEDED( stream_config->GetStreamCaps( i, &media_type, buffer ) ) )
                        {
                            AddPinConfiguration( pin, i, media_type );

                            DeleteMediaType( media_type );
                        }
                    }
                    delete[] buffer;
                    stream_config->Release();
                }
            }
            else
            {
                raw_pin->Release();
            }
        }
        pin_enumerator->Release();
    }
}

Device::~Device()
{
    if ( device != nullptr )
    {
        device->Release();
    }
}

Device::Pin::Pin( IPin* pin, Direction direction )
    :
    pin( pin ),
    direction( direction )
{
}

Device::Pin::~Pin()
{
    if ( pin != nullptr ) pin->Release();
}

bool Device::Pin::SetActiveConfiguration( int config_id )
{
    bool success = false;
    IAMStreamConfig* stream_config;
    if ( SUCCEEDED( pin->QueryInterface( IID_PPV_ARGS( &stream_config ) ) ) )
    {
        int caps_count;
        int caps_size;
        stream_config->GetNumberOfCapabilities( &caps_count, &caps_size );
        unsigned char* buffer = new unsigned char[ caps_size ];
        _AMMediaType* media_type;
        if ( SUCCEEDED( stream_config->GetStreamCaps( config_id, &media_type, buffer ) ) )
        {
            success = SUCCEEDED( stream_config->SetFormat( media_type ) );
            DeleteMediaType( media_type );
        }
        delete[] buffer;
        stream_config->Release();

        return true;
    }
    return false;
}
