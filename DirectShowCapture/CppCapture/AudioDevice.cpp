#include "AudioDevice.h"
#include "WinUtils.h"

using namespace std;

AudioDevice::AudioDevice( const std::wstring& name, IBaseFilter* device )
:
    Device( name, device )
{
    QueryPins();
}

AudioDevice::~AudioDevice()
{
}

shared_ptr< Device::Pin > AudioDevice::AddPin( IPin* raw_pin, Pin::Direction direction )
{
    shared_ptr< AudioPin > pin = make_shared< AudioPin >( raw_pin, direction );
    if ( direction == Pin::In )
    {
        pins_in.push_back( pin );
    }
    else
    {
        pins_out.push_back( pin );
    }

    return pin;

}

void AudioDevice::AddPinConfiguration( shared_ptr< Pin >& pin, int id, _AMMediaType* media_type )
{
    if ( media_type->majortype == MEDIATYPE_Audio )
    {
        if ( media_type->formattype == FORMAT_WaveFormatEx && media_type->cbFormat == sizeof( WAVEFORMATEX ) )
        {
            WAVEFORMATEX* header = (WAVEFORMATEX*)media_type->pbFormat;
            static_pointer_cast< AudioPin >( pin )->AddConfiguration( id, header->wFormatTag, header->nChannels, header->nSamplesPerSec, header->wBitsPerSample );
        }
        else
        {
            printf( "Format type not supported:\n" );
            PrintGuid( media_type->formattype );
        }
    }
    else
    {
        printf( "Major type not supported:\n" );
        PrintGuid( media_type->majortype );
    }
}

AudioDevice::AudioPin::AudioPin( IPin* pin, Direction direction )
:
    Pin( pin, direction )
{

}

void AudioDevice::AudioPin::AddConfiguration( int id, int format_type, int channels, double frequency, int bits )
{
    configurations.push_back( make_shared< Configuration >( id, format_type, channels, frequency, bits ) );
}

AudioDevice::AudioPin::~AudioPin()
{
}

