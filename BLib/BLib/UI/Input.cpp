#include "Input.h"
#include <windows.h>

namespace BLib
{

bool Mouse::hidden = false;

Mouse::Mouse()
    :
    position( Mouse::GetCurrentPosition() )
{
    button_down[ LEFT ]     = GetAsyncKeyState( VK_LBUTTON ) != 0;
    button_down[ MIDDLE ]   = GetAsyncKeyState( VK_MBUTTON ) != 0;
    button_down[ RIGHT ]    = GetAsyncKeyState( VK_RBUTTON ) != 0;
    button_down[ X1 ]       = GetAsyncKeyState( VK_XBUTTON1 ) != 0;
    button_down[ X2 ]       = GetAsyncKeyState( VK_XBUTTON2 ) != 0;
}

Mouse::Mouse( int x, int y, bool left, bool middle, bool right, bool x1, bool x2 )
    :
    position( x, y )
{
    button_down[ LEFT ]     = left;
    button_down[ MIDDLE ]   = middle;
    button_down[ RIGHT ]    = right;
    button_down[ X1 ]       = x1;
    button_down[ X2 ]       = x2;
}

Point Mouse::GetPosition()
{
    return position;
}

bool Mouse::IsDown( MouseButton button )
{
    return button_down[ button ];
}

bool Mouse::Move( int x, int y, bool absolute_movement )
{
    INPUT input = { INPUT_MOUSE, { 0 } };

    // for absolute movement: 0 = top/left, 65535 = bottom/right
    input.mi.dwFlags = MOUSEEVENTF_MOVE | ( absolute_movement? MOUSEEVENTF_ABSOLUTE: 0 );
    input.mi.dx      = x;
    input.mi.dy      = y;

    if ( SendInput( 1, &input, sizeof( INPUT ) ) == 0 ) return false;

    return true;
}

bool Mouse::Click( MouseButton button, ActionType action )
{
    INPUT input = { INPUT_MOUSE, { 0 } };

    if ( button == LEFT )
    {
        if ( action & DOWN )
        {
            input.mi.dwFlags = MOUSEEVENTF_LEFTDOWN;
            if ( SendInput( 1, &input, sizeof( INPUT ) ) == 0 ) return false;
        }
        if ( action & UP )
        {
            input.mi.dwFlags = MOUSEEVENTF_LEFTDOWN;
            if ( SendInput( 1, &input, sizeof( INPUT ) ) == 0 ) return false;
        }
    }

    if ( button == MIDDLE )
    {
        if ( action & DOWN )
        {
            input.mi.dwFlags = MOUSEEVENTF_MIDDLEDOWN;
            if ( SendInput( 1, &input, sizeof( INPUT ) ) == 0 ) return false;
        }
        if ( action & UP )
        {
            input.mi.dwFlags = MOUSEEVENTF_MIDDLEUP;
            if ( SendInput( 1, &input, sizeof( INPUT ) ) == 0 ) return false;
        }
    }

    if ( button == RIGHT )
    {
        if ( action & DOWN )
        {
            input.mi.dwFlags = MOUSEEVENTF_RIGHTDOWN;
            if ( SendInput( 1, &input, sizeof( INPUT ) ) == 0 ) return false;
        }
        if ( action & UP )
        {
            input.mi.dwFlags = MOUSEEVENTF_RIGHTUP;
            if ( SendInput( 1, &input, sizeof( INPUT ) ) == 0 ) return false;
        }
    }

    if ( button == X1 || button == X2 )
    {
        if ( button == X1 ) input.mi.mouseData = XBUTTON1;
        if ( button == X2 ) input.mi.mouseData = XBUTTON2;

        if ( action & DOWN )
        {
            input.mi.dwFlags = MOUSEEVENTF_XDOWN;
            if ( SendInput( 1, &input, sizeof( INPUT ) ) == 0 ) return false;
        }

        if ( action & UP )
        {
            input.mi.dwFlags = MOUSEEVENTF_XUP;
            if ( SendInput( 1, &input, sizeof( INPUT ) ) == 0 ) return false;
        }
    }

    return true;
}

bool Mouse::Scroll( int amount )
{
    INPUT input = { INPUT_MOUSE, { 0 } };

    input.mi.dwFlags    = MOUSEEVENTF_WHEEL;
    input.mi.mouseData  = amount;

    if ( SendInput( 1, &input, sizeof( INPUT ) ) == 0 ) return false;

    return true;
}

void Mouse::Hide()
{
    if ( !hidden )
    {
        ShowCursor( false );
        hidden = true;
    }
}

void Mouse::Show()
{
    if ( hidden )
    {
        ShowCursor( true );
        hidden = true;
    }
}

Point Mouse::GetCurrentPosition()
{
    Point point( 0, 0 );
    POINT p;
    if (::GetCursorPos( &p ) != 0)
    {
        point.X() = p.x;
        point.Y() = p.y;
    }

    return point;
}

bool Mouse::IsCurrentlyDown( MouseButton button )
{
    bool down = false;
    switch ( button )
    {
        case LEFT:
            down = GetAsyncKeyState( VK_LBUTTON ) != 0;
            break;
        case MIDDLE:
            down = GetAsyncKeyState( VK_MBUTTON ) != 0;
            break;
        case RIGHT:
            down = GetAsyncKeyState( VK_RBUTTON ) != 0;
            break;
        case X1:
            down = GetAsyncKeyState( VK_XBUTTON1 ) != 0;
            break;
        case X2:
            down = GetAsyncKeyState( VK_XBUTTON2 ) != 0;
            break;
    }
    return down;
}

bool Keyboard::SendKey( int key, ActionType type, bool shift, bool ctrl, bool alt, bool direct )
{
    INPUT input = { INPUT_KEYBOARD, { 0 } };

    if ( direct )
    {
        input.ki.wVk    = (WORD)key;
    }
    else
    {
        input.ki.wVk    = VkKeyScan( (TCHAR)key );
        shift           = ( input.ki.wVk & 0x0100 ) != 0;
        ctrl            = ( input.ki.wVk & 0x0200 ) != 0;
        alt             = ( input.ki.wVk & 0x0400 ) != 0;
        input.ki.wVk   &= 0xFF;
        if ( input.ki.wVk == 0xFF ) return false;
    }

    if ( shift && key != VK_SHIFT )   SendKey( VK_SHIFT, DOWN, false, false, false, true );
    if ( ctrl  && key != VK_CONTROL ) SendKey( VK_CONTROL, DOWN, false, false, false, true  );
    if ( alt   && key != VK_MENU )    SendKey( VK_MENU, DOWN, false, false, false, true  );

    if ( type & DOWN )
    {
        if ( SendInput( 1, &input, sizeof( INPUT ) ) == 0 ) return false;
    }

    if ( type & UP )
    {
        input.ki.dwFlags = KEYEVENTF_KEYUP;
        if ( SendInput( 1, &input, sizeof( INPUT ) ) == 0 ) return false;
    }

    if ( alt   && key != VK_MENU )    SendKey( VK_MENU, UP, false, false, false, true  );
    if ( ctrl  && key != VK_CONTROL ) SendKey( VK_CONTROL, UP, false, false, false, true  );
    if ( shift && key != VK_SHIFT )   SendKey( VK_SHIFT, UP, false, false, false, true  );

    return true;
}

bool Keyboard::SendText( const wchar_t text[], int chars )
{
    for ( int i=0; i<chars; i++ )
    {
        if ( !SendKey( text[i], PRESS ) ) return false;
    }
    return true;
}

}