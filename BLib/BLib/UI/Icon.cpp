#include "Icon.h"

namespace BLib
{

Icon::Icon( const String& file_name )
{
    handle = (HICON)LoadImage( NULL, file_name, IMAGE_ICON, 0, 0, LR_LOADFROMFILE );
}

Icon::Icon( HICON handle )
{
    this->handle = handle;
}

Icon::~Icon()
{
    DestroyIcon( handle );
}

Icon::operator HICON() const
{
    return handle;
}

}