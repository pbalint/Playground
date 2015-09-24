#pragma once

#include "Control.h"

namespace BLib
{

class Label : public Control
{
protected:
    static const String    default_window_class_name;

public:
    Label( Window*         parent,
           const String&   text               = L"",
           int             x                  = 0,
           int             y                  = 0 );
};

}