#pragma once

#include "../Point.h"

namespace BLib
{

class Mouse
{
private:
    static bool hidden;

    Point       position;
    bool        button_down[5];

public:
    Mouse();
    Mouse( int x, int y, bool left, bool middle, bool right, bool x1, bool x2 );

    enum ActionType { DOWN = 1, UP = 2, CLICK = 3 };
    enum MouseButton { LEFT = 0, MIDDLE, RIGHT, X1, X2 };

    Point GetPosition();
    bool IsDown( MouseButton button );

    static bool Move( int x, int y, bool absolute_movement = false );
    static bool Click( MouseButton button, ActionType action );
    static bool Scroll( int amount );
    static void Hide();
    static void Show();
    static Point GetCurrentPosition();
    static bool IsCurrentlyDown( MouseButton button );
};

class Keyboard
{
private:
public:
    enum ActionType { DOWN = 1, UP = 2 , PRESS = 3 };

    static bool IsDown( int key );
    static bool SendKey( int        key,
                         ActionType type,
                         bool       shift = false,
                         bool       ctrl = false,
                         bool       alt = false,
                         bool       direct = false );
    static bool SendText( const wchar_t text[], int chars );
};

}