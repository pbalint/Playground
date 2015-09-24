#include <windows.h>

#include "qedit.h"
#include "NullRenderer.h"

using namespace std;

NullRenderer::NullRenderer( const std::wstring& name, IBaseFilter* device )
    :
    Device( name, device )
{
    QueryPins();
}


NullRenderer::~NullRenderer()
{
}

shared_ptr< Device::Pin > NullRenderer::AddPin( IPin* raw_pin, Pin::Direction direction )
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

void NullRenderer::AddPinConfiguration( std::shared_ptr< Pin >& pin, int id, _AMMediaType* media_type )
{
}
