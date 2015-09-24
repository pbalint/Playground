#include <windows.h>

#include "SampleGrabber.h"
#include "qedit.h"

using namespace std;

class SampleGrabberCallBack : public ISampleGrabberCB {
private:
    const SampleGrabber* sample_grabber;

public:
    SampleGrabberCallBack( const SampleGrabber* sample_grabber )
    {
        this->sample_grabber = sample_grabber;
    }

    virtual HRESULT STDMETHODCALLTYPE QueryInterface( REFIID riid, _COM_Outptr_ void __RPC_FAR *__RPC_FAR *ppvObject ) { return 0; }
    virtual ULONG STDMETHODCALLTYPE AddRef( void ) { return 1; }
    virtual ULONG STDMETHODCALLTYPE Release( void ) { return 1; }

    virtual STDMETHODIMP SampleCB( double sample_time, IMediaSample *sample )
    {
        if ( sample_grabber != nullptr && sample_grabber->callback_function != nullptr )
        {
            unsigned char* buffer;
            sample->GetPointer( &buffer );
            ( *( sample_grabber->callback_function ) )( sample_grabber->callback_context, sample_time, buffer, sample->GetSize() );
        }
        return 0;
    }

    virtual STDMETHODIMP BufferCB( double sample_time, BYTE* buffer, long buffer_length )
    {
        printf( "%f, %i\n", sample_time, buffer_length );
        if ( sample_grabber != nullptr && sample_grabber->callback_function != nullptr )
        {
            ( *( sample_grabber->callback_function ) )( sample_grabber->callback_context, sample_time, buffer, buffer_length );
        }
        return 0;
    }
};


SampleGrabber::SampleGrabber( const std::wstring& name, IBaseFilter* device )
:
    Device( name, device ),
    callback_context( nullptr ),
    callback_function( nullptr ),
    grabber( nullptr )
{
    if ( device->QueryInterface( IID_ISampleGrabber, (void**)&grabber ) == S_OK )
    {
        QueryPins();
        grabber->SetBufferSamples( FALSE );
        grabber_callback = make_unique< SampleGrabberCallBack >( this );
        grabber->SetCallback( grabber_callback.get(), 1 ); // use BufferCB
    }
}

SampleGrabber::~SampleGrabber()
{
    if ( grabber != nullptr )
    {
        grabber->SetCallback( NULL, 0 );
        grabber->Release();
    }
}

shared_ptr< Device::Pin > SampleGrabber::AddPin( IPin* raw_pin, Pin::Direction direction )
{
    shared_ptr< Device::Pin > pin = make_shared< Device::Pin >( raw_pin, direction );
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

void SampleGrabber::AddPinConfiguration( std::shared_ptr< Pin >& pin, int id, _AMMediaType* media_type )
{

}
