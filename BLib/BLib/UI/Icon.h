#pragma once

#include <windows.h>
#include "../String.h"

namespace BLib
{

class Icon
{
private:
    HICON handle;

public:
    Icon( const String& file_name );
    Icon( HICON handle );
    ~Icon();
    operator HICON() const;
};

}