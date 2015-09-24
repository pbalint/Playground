#pragma once

#include <Windows.h>
#include "../String.h"

namespace BLib
{

class Font
{
protected:
    HFONT   font_handle;
    int     size;
    bool    bold;
    bool    italic;
    bool    underline;
    bool    strikeout;
    String  font_name;

    void InitWithDefaultFont();
public:
    Font();
    Font( String font_name, int size = 0, bool bold = false, bool italic = false, bool underline = false, bool strikeout = false );
    Font& operator =( const Font& other );
    operator HFONT();
    virtual ~Font();
};

}
