#pragma once

#include <string>
#include <vector>
#include <memory>

struct IBaseFilter;
struct _AMMediaType;
struct IPin;

class Device
{
public:
    class Pin
    {
    public:
        enum Direction { In, Out };

    private:
        IPin*       pin;
        Direction   direction;

    public:
        Pin( IPin* pin, Direction direction );
        virtual ~Pin();

        bool SetActiveConfiguration( int config_id );
        IPin* GetPin() { return pin; }
        Direction GetDirection() { return direction; }
    };

private:
    std::wstring                            name;
    IBaseFilter*                            device;

protected:
    virtual std::shared_ptr< Pin > AddPin( IPin* pin, Pin::Direction direction ) = 0;
    virtual void AddPinConfiguration( std::shared_ptr< Pin >& Pin, int id, _AMMediaType* media_type ) = 0;
    void QueryPins();

public:
    Device( const std::wstring& name, IBaseFilter* device );
    virtual ~Device();

    std::wstring GetName() { return name; }
    IBaseFilter* GetDevice() { return device; }
};

