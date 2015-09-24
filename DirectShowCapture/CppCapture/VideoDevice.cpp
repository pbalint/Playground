#include <dshow.h>
#include <dvdmedia.h>

#include "VideoDevice.h"
#include "WinUtils.h"

using namespace std;

VideoDevice::VideoDevice( const std::wstring& name, IBaseFilter* device )
:
    Device( name, device )
{
    QueryPins();
}

VideoDevice::~VideoDevice()
{
}

shared_ptr< Device::Pin > VideoDevice::AddPin( IPin* raw_pin, Pin::Direction direction )
{
    shared_ptr< VideoPin > pin = make_shared< VideoPin >( raw_pin, direction );
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

void VideoDevice::AddPinConfiguration( shared_ptr< Pin >& pin, int id, _AMMediaType* media_type )
{
    if ( media_type->majortype == MEDIATYPE_Video )
    {
        if ( media_type->formattype == FORMAT_VideoInfo && media_type->cbFormat == sizeof( VIDEOINFOHEADER ) )
        {
            VIDEOINFOHEADER* header = (VIDEOINFOHEADER*)media_type->pbFormat;
            if ( header->bmiHeader.biCompression == BI_RGB )
            {
                static_pointer_cast< VideoPin >( pin )->AddConfiguration( id, header->bmiHeader.biWidth, header->bmiHeader.biHeight, 10000000.0 / header->AvgTimePerFrame );
            }
            else
            {
                //printf( "Ignoring unsupported pin: %i, %i, %i, %x\n", header->bmiHeader.biWidth, header->bmiHeader.biHeight, header->bmiHeader.biBitCount, header->bmiHeader.biCompression );
            }
        }
        else if ( media_type->formattype == FORMAT_VideoInfo2 && media_type->cbFormat == sizeof( VIDEOINFOHEADER2 ) )
        {
            VIDEOINFOHEADER2* header = (VIDEOINFOHEADER2*)media_type->pbFormat;
            if ( header->bmiHeader.biCompression == BI_RGB )
            {
                static_pointer_cast< VideoPin >( pin )->AddConfiguration( id, header->bmiHeader.biWidth, header->bmiHeader.biHeight, 10000000.0 / header->AvgTimePerFrame );
            }
            else
            {
                //printf( "Ignoring unsupported pin: %i, %i, %i, %x\n", header->bmiHeader.biWidth, header->bmiHeader.biHeight, header->bmiHeader.biBitCount, header->bmiHeader.biCompression );
            }
        }
        else
        {
            printf( "Format type not supported:\n" );
            PrintGuid( media_type->formattype );
        }
    }
    else
    {
        printf( "Format type not supported:\n" );
        PrintGuid( media_type->formattype );
    }
}

VideoDevice::VideoPin::VideoPin( IPin* pin, Direction direction )
:
    Pin( pin, direction )
{
}

void VideoDevice::VideoPin::AddConfiguration( int id, int width, int height, double fps )
{
    configurations.push_back( make_shared< Configuration >( id, width, height, fps ) );
}


VideoDevice::VideoPin::~VideoPin()
{
}
