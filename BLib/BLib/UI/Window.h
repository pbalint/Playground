#pragma once

#include <windows.h>
#include "Font.h"
#include "../String.h"
#include "../Array.h"
#include "Input.h"

namespace BLib
{

class Window
{
protected:
    struct CallbackBase
    {
        virtual void operator()() = 0;
    };

    template< typename PARAM >
    struct CallbackBase1
    {
        virtual void operator()( PARAM param ) = 0;
    };

    static const String     window_class_name;
    static WNDCLASSEX       window_class;
    static Font*            default_font;
    static int              window_count;

    static LRESULT CALLBACK WindowProcedure( HWND hwnd, UINT msg, WPARAM wparam, LPARAM lparam );
    virtual void HandleMessagePreDefault( HWND hwnd, UINT msg, WPARAM wparam, LPARAM lparam );
    virtual void HandleMessagePostDefault( HWND hwnd, UINT msg, WPARAM wparam, LPARAM lparam );

    HWND                    window_handle;
    Font*                   font;
    WNDPROC                 default_wndproc;
    Array< Window* >        controls;
    int                     x;
    int                     y;
    int                     width;
    int                     height;

    Window();
    virtual void CreateNewWindow(  Window* parent, const String& title, int x, int y, int width, int height,
                                   DWORD window_styles, DWORD window_ex_styles, const String& class_name );
public:
    CallbackBase*           click;
    CallbackBase1<int>*     key_down;
    CallbackBase1<int>*     key_up;
    CallbackBase1<int>*     key_pressed;
    CallbackBase1<Mouse&>*  mouse_down;
    CallbackBase1<Mouse&>*  mouse_up;
    CallbackBase1<Mouse&>*  mouse_double_click;
    CallbackBase1<Mouse&>*  mouse_moved;
    CallbackBase*           closed;

    virtual void            OnClick();
    virtual void            OnKeyDown( int key_code );
    virtual void            OnKeyUp( int key_code );
    virtual void            OnKeyPressed( int key_code );
    virtual void            OnMouseDown( Mouse& mouse_event );
    virtual void            OnMouseUp( Mouse& mouse_event );
    virtual void            OnMouseDoubleClick( Mouse& mouse_event );
    virtual void            OnMouseMoved( Mouse& mouse_event );
    virtual void            OnClosed();

    template<typename CLASS>
    struct Callback: public CallbackBase
    {
        typedef void (CLASS::*Method)();
        Method method;
        CLASS* instance;

        Callback( CLASS* instance, Method method )
        {
            this->instance = instance;
            this->method = method;
        }

        virtual void operator()()
        {
            (instance->*method)();
        }
    };

    template<typename CLASS, typename PARAM>
    struct Callback1: public CallbackBase1<PARAM>
    {
        typedef void (CLASS::*Method)( PARAM param );
        Method method;
        CLASS* instance;

        Callback1( CLASS* instance, Method method )
        {
            this->instance = instance;
            this->method = method;
        }

        virtual void operator()( PARAM param )
        {
            (instance->*method)( param );
        }
    };

    Window( const String&   title,
            int             x                   = 0,
            int             y                   = 0,
            int             width               = 320,
            int             height              = 240 );
    virtual ~Window();
    HWND GetHandle() { return window_handle; }
    Array< Window* >& GetControls() { return controls; }

    virtual String GetText();
    virtual void SetText( const String& text );
    virtual void SetPosition( int x, int y );
    virtual void SetSize( int width, int height );
    int GetX() { return x; }
    int GetY() { return y; }
    int GetWidth() { return width; }
    int GetHeight() { return height; }

    static void Init( HINSTANCE instance = NULL );
    static void Finish();
    static void DoMessageLoop();
};

}