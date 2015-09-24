#pragma once

#include "Window.h"
#include "Icon.h"
#include <shellapi.h>

namespace BLib
{

class TrayIcon : public Window
{
private:
    NOTIFYICONDATA data;

public:
    TrayIcon( Window* parent, const Icon& icon );
    virtual ~TrayIcon();
};

}
