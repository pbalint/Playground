#pragma once

#include <dshow.h>
#include "Device.h"

class FilterGraph
{
private:
    IGraphBuilder* graph_builder;
    IMediaEventEx* media_events;
    IMediaControl* media_control;

public:
    FilterGraph();
    bool Add( const std::shared_ptr< Device >& device );
    bool Connect( const std::shared_ptr< Device::Pin >& pin1, const std::shared_ptr< Device::Pin >& pin2 );
    bool Run();
    bool Stop();
    virtual ~FilterGraph();
};

