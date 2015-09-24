#include "FilterGraph.h"


FilterGraph::FilterGraph()
    :
    graph_builder( nullptr ),
    media_events( nullptr ),
    media_control( nullptr )
{
    CoCreateInstance( CLSID_FilterGraph, nullptr, CLSCTX_INPROC_SERVER, IID_IGraphBuilder, (void**)&graph_builder );
    graph_builder->QueryInterface( IID_PPV_ARGS( &media_events ) );
    graph_builder->QueryInterface( IID_PPV_ARGS( &media_control ) );
}


FilterGraph::~FilterGraph()
{
    if ( media_control != nullptr )
    {
        media_control->Stop();
        media_control->Release();
    }
    if ( media_events != nullptr ) media_events->Release();
    if ( graph_builder != nullptr ) graph_builder->Release();
}

bool FilterGraph::Add( const std::shared_ptr< Device >& device )
{
    return ( graph_builder->AddFilter( device->GetDevice(), device->GetName().c_str() ) == S_OK );
}

bool FilterGraph::Connect( const std::shared_ptr< Device::Pin >& pin1, const std::shared_ptr< Device::Pin >& pin2 )
{
    if ( ( pin1->GetDirection() == Device::Pin::In ) && ( pin2->GetDirection() == Device::Pin::Out ) ) {
        return graph_builder->Connect( pin2->GetPin(), pin1->GetPin() ) == S_OK;
    }
    else if ( ( pin1->GetDirection() == Device::Pin::Out ) && ( pin2->GetDirection() == Device::Pin::In ) ) {
        return graph_builder->Connect( pin1->GetPin(), pin2->GetPin() ) == S_OK;
    }
    else {
        return false;
    }
}

bool FilterGraph::Run()
{
    return media_control->Run() == S_OK;
}

bool FilterGraph::Stop()
{
    return media_control->Stop() == S_OK;
}


