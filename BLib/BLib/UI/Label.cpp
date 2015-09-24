#include "Label.h"

namespace BLib
{

const String     Label::default_window_class_name   = "Static";

Label::Label(   Window*         parent,
                const String&   text,
                int             x,
                int             y )
{
    CreateNewWindow( parent, "", x, y, 1, 1, WS_CHILD | WS_VISIBLE | SS_NOTIFY, 0, default_window_class_name );
    SetText( text );
    ShowWindow( window_handle, SW_SHOW );
}

}