#pragma once

#include "Device.h"

class NullRenderer : public Device
{
private:
    friend class DeviceEnumerator;

    std::vector< std::shared_ptr< Device::Pin > >   pins_in;
    std::vector< std::shared_ptr< Device::Pin > >   pins_out;

    NullRenderer( const std::wstring& name, IBaseFilter* device );

protected:
    virtual std::shared_ptr< Pin > AddPin( IPin* raw_pin, Pin::Direction direction );
    virtual void AddPinConfiguration( std::shared_ptr< Pin >& pin, int id, _AMMediaType* media_type );

public:
    virtual ~NullRenderer();

    const std::vector< std::shared_ptr< Device::Pin > >& GetInputPins() { return pins_in; }
    const std::vector< std::shared_ptr< Device::Pin > >& GetOutputPins() { return pins_out; }
};

