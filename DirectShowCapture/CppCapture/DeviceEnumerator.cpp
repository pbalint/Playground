#include <dshow.h>

#include "DeviceEnumerator.h"
#include "qedit.h"

using namespace std;

template< typename DeviceClass >
static vector< shared_ptr< DeviceClass > > GetDevices( ICreateDevEnum* enumerator, GUID device_category )
{
    vector< shared_ptr< DeviceClass > > devices;

    IEnumMoniker* device_iterator = nullptr;
    if ( enumerator->CreateClassEnumerator( device_category, &device_iterator, 0 ) == S_OK )
    {
        IMoniker* moniker = nullptr;
        while ( device_iterator != nullptr &&
                device_iterator->Next( 1, &moniker, 0 ) == S_OK )
        {
            IPropertyBag* properties;
            std::wstring name;
            if ( moniker->BindToStorage( 0, 0, IID_PPV_ARGS( &properties ) ) == S_OK )
            {
                VARIANT variant;
                VariantInit( &variant );

                if ( SUCCEEDED( properties->Read( L"FriendlyName", &variant, 0 ) ) )
                {
                    name = variant.bstrVal;
                    VariantClear( &variant );
                }

                properties->Release();
            }

            IBaseFilter* filter;
            if ( SUCCEEDED( moniker->BindToObject( 0, 0, IID_IBaseFilter, (void**)&filter ) ) )
            {
                devices.push_back( make_shared< DeviceClass >( name, filter ) );
            }

            moniker->Release();
        }
        if ( device_iterator != nullptr )    device_iterator->Release();
    }

    return devices;
}

DeviceEnumerator::DeviceEnumerator()
{
    CoCreateInstance( CLSID_SystemDeviceEnum, 0, CLSCTX_INPROC_SERVER, IID_ICreateDevEnum, (void**)&enumerator );
}

DeviceEnumerator::~DeviceEnumerator()
{
    if ( enumerator != nullptr )	enumerator->Release();
}

vector< shared_ptr< AudioDevice > > DeviceEnumerator::GetAudioDevices()
{
    return GetDevices< AudioDevice >( enumerator, CLSID_AudioInputDeviceCategory );
}

vector< shared_ptr< VideoDevice > > DeviceEnumerator::GetVideoDevices()
{
    return GetDevices< VideoDevice >( enumerator, CLSID_VideoInputDeviceCategory );
}

shared_ptr< SampleGrabber > DeviceEnumerator::CreateSampleGrabber( const std::wstring& name )
{
    IBaseFilter* sample_grabber_filter;
    CoCreateInstance( CLSID_SampleGrabber, nullptr, CLSCTX_INPROC_SERVER, IID_IBaseFilter, (void**)&sample_grabber_filter );
    return shared_ptr< SampleGrabber >( new SampleGrabber( name, sample_grabber_filter ) );
}

std::shared_ptr< NullRenderer > DeviceEnumerator::CreateNullRenderer( const std::wstring& name )
{
    IBaseFilter* null_renderer;
    CoCreateInstance( CLSID_nullptrRenderer, NULL, CLSCTX_INPROC_SERVER, IID_PPV_ARGS( &null_renderer ) );
    return shared_ptr< NullRenderer >( new NullRenderer( name, null_renderer ) );
}

