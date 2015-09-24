#pragma once

#include "Device.h"

struct ISampleGrabberCB;
struct ISampleGrabber;

class SampleGrabber : public Device
{
public:
    typedef void( *CallBackFunction )( void* context, double time, unsigned char* buffer, long length );

private:
    friend class DeviceEnumerator;
    friend class SampleGrabberCallBack;

    void*                                           callback_context;
    CallBackFunction                               callback_function;
    std::unique_ptr< ISampleGrabberCB >             grabber_callback;
    ISampleGrabber*                                 grabber;
    std::vector< std::shared_ptr< Device::Pin > >   pins_in;
    std::vector< std::shared_ptr< Device::Pin > >   pins_out;

    SampleGrabber( const std::wstring& name, IBaseFilter* device );

protected:
    virtual std::shared_ptr< Pin > AddPin( IPin* raw_pin, Pin::Direction direction );
    virtual void AddPinConfiguration( std::shared_ptr< Pin >& pin, int id, _AMMediaType* media_type );

public:
    virtual ~SampleGrabber();
    void SetCallBack( void* context, CallBackFunction callback_function ) { this->callback_context = context; this->callback_function = callback_function; }

    const std::vector< std::shared_ptr< Device::Pin > >& GetInputPins() { return pins_in; }
    const std::vector< std::shared_ptr< Device::Pin > >& GetOutputPins() { return pins_out; }
};

