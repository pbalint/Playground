#include "Window.h"

namespace BLib
{

const String    Window::window_class_name   = "BLibWindow";
WNDCLASSEX      Window::window_class        = { 0 };
int             Window::window_count        = 0;
Font*           Window::default_font        = NULL;

void Window::Init( HINSTANCE instance )
{
    default_font                = new Font();

    window_class.cbSize         = sizeof( window_class );
    window_class.style          = CS_HREDRAW | CS_VREDRAW | CS_OWNDC;
    window_class.lpfnWndProc    = Window::WindowProcedure;
    window_class.hInstance      = ( instance != NULL )? instance: GetModuleHandle( NULL );
    window_class.hbrBackground  = HBRUSH ( 5 );
    window_class.lpszClassName  = window_class_name;
    window_class.hCursor        = LoadCursor( NULL, IDC_ARROW );

    if ( !RegisterClassEx( &window_class ) ) throw;
}

void Window::Finish()
{
    UnregisterClass( window_class_name, window_class.hInstance );
    delete default_font;
}

static Mouse AssembleMouseFromParams( WPARAM wparam, LPARAM lparam )
{
    return Mouse(   LOWORD( lparam ),
                    HIWORD( lparam ),
                    ( wparam & MK_LBUTTON ) != 0,
                    ( wparam & MK_MBUTTON ) != 0,
                    ( wparam & MK_RBUTTON ) != 0,
                    ( wparam & MK_XBUTTON1 ) != 0,
                    ( wparam & MK_XBUTTON2 ) != 0 );
}

LRESULT CALLBACK Window::WindowProcedure( HWND hwnd, UINT msg, WPARAM wparam, LPARAM lparam )
{
    Window* window = NULL;

    switch ( msg )
    {
        case WM_NCCREATE:
        {
            LPCREATESTRUCT creation_parameters = (LPCREATESTRUCT)lparam;
            ++window_count;
            SetWindowLongPtr( hwnd, GWLP_USERDATA, (LONG)creation_parameters->lpCreateParams );
            break;
        }

        case WM_PARENTNOTIFY:
        {
            if ( LOWORD( wparam ) == WM_CREATE ) ++window_count;
            break;
        }
        case WM_DESTROY:
        {
            if ( --window_count <= 0 ) PostQuitMessage( 0 );
            break;
        }

        default:
            window = (Window*)GetWindowLongPtr( hwnd, GWLP_USERDATA );
    }

    if ( window )
    {
        window->HandleMessagePreDefault( hwnd, msg, wparam, lparam );
        LRESULT default_result = window->default_wndproc( hwnd, msg, wparam, lparam );
        window->HandleMessagePostDefault( hwnd, msg, wparam, lparam );
        return default_result;
    }
    else
    {
        return DefWindowProc( hwnd, msg, wparam, lparam );
    }
}

void Window::DoMessageLoop()
{
    MSG msg;
    while ( GetMessage( &msg, NULL, 0, 0 ) )
    {
        TranslateMessage( &msg );
        DispatchMessage( &msg );
    }
}

void Window::HandleMessagePreDefault( HWND /*hwnd*/, UINT msg, WPARAM /*wparam*/, LPARAM /*lparam*/ )
{
    switch ( msg )
    {
        case WM_CLOSE:
            for ( size_t i=0; i<controls.GetSize(); i++ )
            {
                controls[i]->OnClosed();
            }
            OnClosed();
            break;
    }
}

void Window::HandleMessagePostDefault( HWND hwnd, UINT msg, WPARAM wparam, LPARAM lparam )
{
    switch ( msg )
    {
        case WM_COMMAND:
        {
            String str = "hwnd: ";
            str += (long long)hwnd;
            str += " lparam:";
            str += lparam;
            str += "\n";
            OutputDebugString( str );
            Window* window = (Window*)GetWindowLongPtr( (HWND)lparam, GWLP_USERDATA );
            window->OnClick();
            break;
        }

        case WM_MOUSEMOVE:
        {
            Mouse mouse = AssembleMouseFromParams( wparam, lparam );
            OnMouseMoved( mouse );
            break;
        }

        case WM_LBUTTONDOWN:
        case WM_MBUTTONDOWN:
        case WM_RBUTTONDOWN:
        case WM_XBUTTONDOWN:
        {
            Mouse mouse = AssembleMouseFromParams( wparam, lparam );
            OnMouseDown( mouse );
            break;
        }

        case WM_LBUTTONUP:
        case WM_MBUTTONUP:
        case WM_RBUTTONUP:
        case WM_XBUTTONUP:
        {
            Mouse mouse = AssembleMouseFromParams( wparam, lparam );
            OnMouseUp( mouse );
            break;
        }

        case WM_LBUTTONDBLCLK:
        case WM_MBUTTONDBLCLK:
        case WM_RBUTTONDBLCLK:
        case WM_XBUTTONDBLCLK:
        {
            Mouse mouse = AssembleMouseFromParams( wparam, lparam );
            OnMouseDoubleClick( mouse );
            break;
        }

        case WM_KEYDOWN:
            break;

        case WM_KEYUP:
            break;

    }
}

}