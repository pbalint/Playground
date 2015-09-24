#pragma once

#include "Control.h"

namespace BLib
{

class Button : public Control
{
protected:
    static const String    default_window_class_name;

public:
    Button( Window*         parent,
            const String&   text               = L"",
            int             x                  = 0,
            int             y                  = 0 );
};

}