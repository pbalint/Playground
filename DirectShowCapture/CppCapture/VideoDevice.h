#pragma once

#include "Device.h"

class VideoDevice : public Device
{
public:
    class VideoPin : public Pin
    {
        friend class VideoDevice;

        struct Configuration
        {
            int     id;
            int     width;
            int     height;
            double  fps;

            Configuration( int id, int width, int height, double fps ) { this->id = id; this->width = width; this->height = height; this->fps = fps; }
        };

    private:
        std::vector< std::shared_ptr< Configuration > > configurations;

        void AddConfiguration( int id, int width, int height, double fps );

    public:
        VideoPin( IPin* pin, Direction direction );
        const std::vector< std::shared_ptr< Configuration > >& GetConfigurations() { return configurations; }
        virtual ~VideoPin();
    };

private:
    std::vector< std::shared_ptr< VideoPin > >   pins_in;
    std::vector< std::shared_ptr< VideoPin > >   pins_out;

protected:
    virtual std::shared_ptr< Pin > AddPin( IPin* raw_pin, Pin::Direction direction );
    virtual void AddPinConfiguration( std::shared_ptr< Pin >& pin, int id, _AMMediaType* media_type );

public:
    VideoDevice( const std::wstring& name, IBaseFilter* device );
    virtual ~VideoDevice();

    const std::vector< std::shared_ptr< VideoPin > >& GetInputPins() { return pins_in; }
    const std::vector< std::shared_ptr< VideoPin > >& GetOutputPins() { return pins_out; }
};