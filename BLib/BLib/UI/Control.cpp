#include "Control.h"

namespace BLib
{

void Control::SetText( const String& text )
{
    Font* current_font = (font != NULL)? font: default_font;

    HDC dc = GetDC( window_handle );
    SelectObject( dc, *current_font );
    SIZE size = { 0 };
    GetTextExtentPoint32( dc, text, text.GetLength(), &size );
    SetSize( size.cx + 4, size.cy );
    ReleaseDC( window_handle, dc );

    Window::SetText( text );
}

void Control::CreateNewWindow(  Window* parent, const String& title, int x, int y, int width, int height,
                                DWORD window_styles, DWORD window_ex_styles, const String& class_name )
{
    Window::CreateNewWindow( parent, title, x, y, width, height, window_styles, window_ex_styles, class_name );
    SetWindowLongPtr( window_handle, GWLP_USERDATA, (LONG)this );
    default_wndproc = (WNDPROC)SetWindowLongPtr( window_handle, GWL_WNDPROC, (LONG)Window::WindowProcedure );
}


}