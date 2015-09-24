#pragma once

#include "Device.h"

class AudioDevice : public Device
{
public:
    class AudioPin : public Pin
    {
        friend class AudioDevice;

        struct Configuration
        {
            int     id;
            int     format_type;
            int     channels;
            double  frequency;
            int     bits;

            Configuration( int id, int format_type, int channels, double frequency, int bits ) { this->id = id; this->format_type = format_type; this->channels = channels; this->frequency = frequency; this->bits; }
        };

    private:
        std::vector< std::shared_ptr< Configuration > > configurations;

        void AddConfiguration( int id, int format_type, int channels, double frequency, int bits );

    public:
        AudioPin( IPin* pin, Direction direction );
        const std::vector< std::shared_ptr< Configuration > >& GetConfigurations() { return configurations; }
        virtual ~AudioPin();
    };


private:
    std::vector< std::shared_ptr< AudioPin > >   pins_in;
    std::vector< std::shared_ptr< AudioPin > >   pins_out;

protected:
    virtual std::shared_ptr< Pin > AddPin( IPin* raw_pin, Pin::Direction direction );
    virtual void AddPinConfiguration( std::shared_ptr< Pin >& pin, int id, _AMMediaType* media_type );

public:
    AudioDevice( const std::wstring& name, IBaseFilter* device );
    virtual ~AudioDevice();

    const std::vector< std::shared_ptr< AudioPin > >& GetInputPins() { return pins_in; }
    const std::vector< std::shared_ptr< AudioPin > >& GetOutputPins() { return pins_out; }
};