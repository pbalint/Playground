#include "Button.h"

namespace BLib
{

const String     Button::default_window_class_name   = "Button";

Button::Button( Window*         parent,
                const String&   text,
                int             x,
                int             y )
{
    CreateNewWindow( parent, "", x, y, 1, 1, WS_CHILD | WS_VISIBLE, 0, default_window_class_name );

    SetText( text );
    ShowWindow( window_handle, SW_SHOW );
}

}