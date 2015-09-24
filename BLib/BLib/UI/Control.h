#pragma once

#include "Window.h"

namespace BLib
{

class Control : public Window
{
protected:
    virtual void CreateNewWindow(   Window* parent, const String& title, int x, int y, int width, int height,
                                    DWORD window_styles, DWORD window_ex_styles, const String& class_name );

public:
    virtual void SetText( const String& text );
};

}

