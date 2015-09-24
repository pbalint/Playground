#pragma once

#include <vector>
#include <memory>

#include "AudioDevice.h"
#include "VideoDevice.h"
#include "SampleGrabber.h"
#include "NullRenderer.h"

struct ICreateDevEnum;

class DeviceEnumerator
{
private:
    ICreateDevEnum* enumerator;

public:
    DeviceEnumerator();
    virtual ~DeviceEnumerator();

    std::vector< std::shared_ptr< AudioDevice > > GetAudioDevices();
    std::vector< std::shared_ptr< VideoDevice > > GetVideoDevices();

    static std::shared_ptr< SampleGrabber > CreateSampleGrabber( const std::wstring& name = L"Grabber" );
    static std::shared_ptr< NullRenderer > CreateNullRenderer( const std::wstring& name = L"NullRenderer" );
};