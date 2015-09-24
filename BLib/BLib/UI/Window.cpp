#include "Window.h"
#include "../Time.h"

namespace BLib
{

Window::Window()
:
x( 0 ),
y( 0 ),
width( 0 ),
height( 0 ),
font( NULL ),
default_wndproc( DefWindowProc ),

click( NULL ),
key_down( NULL ),
key_up( NULL ),
key_pressed( NULL ),
mouse_down( NULL ),
mouse_up( NULL ),
mouse_double_click( NULL ),
mouse_moved( NULL ),
closed( NULL )
{
}

Window::Window( const String&   title,
                int             x,
                int             y,
                int             width,
                int             height )
:
font( NULL ),
default_wndproc( DefWindowProc ),

click( NULL ),
key_down( NULL ),
key_up( NULL ),
key_pressed( NULL ),
mouse_down( NULL ),
mouse_up( NULL ),
mouse_double_click( NULL ),
mouse_moved( NULL ),
closed( NULL )
{
    CreateNewWindow( NULL, title, x, y, width, height, 
                     WS_OVERLAPPEDWINDOW, WS_EX_COMPOSITED, window_class.lpszClassName );

    ShowWindow( window_handle, SW_SHOW );
}

Window::~Window()
{
    while ( controls.GetSize() > 0 )
    {
        delete controls.RemoveLast();
    }

    delete font;
    delete click;
    delete key_down;
    delete key_up;
    delete key_pressed;
    delete mouse_down;
    delete mouse_up;
    delete mouse_double_click;
    delete mouse_moved;
    delete closed;
}

void Window::CreateNewWindow(  Window* parent, const String& title, int x, int y, int width, int height,
                               DWORD window_styles, DWORD window_ex_styles, const String& class_name )
{
    if ( parent != NULL )
    {
        parent->GetControls().AddLast( this );
    }

    this->x       = x;
    this->y       = y;
    this->width   = width;
    this->height  = height;
    window_handle = CreateWindowEx( window_ex_styles, class_name,
                                    title,
                                    window_styles,
                                    x, y, width, height,
                                    ( parent != NULL )? parent->window_handle: NULL,
                                    NULL,
                                    window_class.hInstance,
                                    this );
    String str = "New window: ";
    str += (ptrdiff_t)window_handle;
    str += "\n";
    OutputDebugString( str );
    if ( !window_handle ) throw;

    SendMessage( window_handle, WM_SETFONT, (WPARAM)(HFONT)*default_font, true );
}

String Window::GetText()
{
    const size_t text_length = 1 + SendMessage( window_handle, WM_GETTEXTLENGTH, 0, 0 );
    Buffer buff( text_length * sizeof( TCHAR ) );
    SendMessage( window_handle, WM_GETTEXT, text_length, (LPARAM)buff.GetPtr() );
    return String( buff, true, _UNICODE == 1 );
}

void Window::SetText( const String& text )
{
    SendMessage( window_handle, WM_SETTEXT, 0, (LPARAM)(const char*)text );
}

void Window::SetPosition( int x, int y )
{
    this->x = x;
    this->y = y;
    MoveWindow( window_handle, x, y, width, height, TRUE );
}

void Window::SetSize( int width, int height )
{
    this->width  = width;
    this->height = height;
    MoveWindow( window_handle, x, y, width, height, TRUE );
}

void Window::OnClick()
{
    if ( click != NULL ) (*click)();
}

void Window::OnKeyDown( int key_code )
{
    if ( key_down != NULL ) (*key_down)( key_code );
}

void Window::OnKeyUp( int key_code )
{
    if ( key_up != NULL ) (*key_up)( key_code );
}

void Window::OnKeyPressed( int key_code )
{
    if ( key_pressed != NULL ) (*key_pressed)( key_code );
}

void Window::OnMouseDown( Mouse& mouse_event )
{
    if ( mouse_down != NULL ) (*mouse_down)( mouse_event );
}

void Window::OnMouseUp( Mouse& mouse_event )
{
    if ( mouse_up != NULL ) (*mouse_up)( mouse_event );
}

void Window::OnMouseDoubleClick( Mouse& mouse_event )
{
    if ( mouse_double_click != NULL ) (*mouse_double_click)( mouse_event );
}

void Window::OnMouseMoved( Mouse& mouse_event )
{
    if ( mouse_moved != NULL ) (*mouse_moved)( mouse_event );
}

void Window::OnClosed()
{
    if ( closed != NULL ) (*closed)();
}

}