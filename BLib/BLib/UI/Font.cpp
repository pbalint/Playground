#include "Font.h"

namespace BLib
{

Font::Font()
{
    InitWithDefaultFont();
}

Font::Font( String font_name, int size, bool bold, bool italic, bool underline, bool strikeout )
{
    this->bold        = bold;
    this->italic      = italic;
    this->underline   = underline;
    this->strikeout   = strikeout;
    this->size        = size;
    this->font_name   = font_name;

    font_handle = CreateFont(   size, 0, 0, 0, 
                                bold? FW_BOLD: 0,
                                italic, underline, strikeout,
                                DEFAULT_CHARSET, OUT_DEFAULT_PRECIS, CLIP_DEFAULT_PRECIS, DEFAULT_QUALITY, DEFAULT_PITCH,
                                font_name );

    if ( font_handle == NULL )
    {
        InitWithDefaultFont();
    }
}

Font::~Font()
{
    if ( font_handle != NULL )
    {
        DeleteObject( font_handle );
    }
}

Font& Font::operator =( const Font& other )
{
    DeleteObject( font_handle );

    this->bold        = other.bold;
    this->italic      = other.italic;
    this->underline   = other.underline;
    this->strikeout   = other.strikeout;
    this->size        = other.size;
    this->font_name   = other.font_name;

    font_handle = CreateFont(   size, 0, 0, 0, 
                                bold? FW_BOLD: 0,
                                italic, underline, strikeout,
                                DEFAULT_CHARSET, OUT_DEFAULT_PRECIS, CLIP_DEFAULT_PRECIS, DEFAULT_QUALITY, DEFAULT_PITCH,
                                font_name );

    if ( font_handle == NULL )
    {
        InitWithDefaultFont();
    }

    return *this;
}

void Font::InitWithDefaultFont()
{
    NONCLIENTMETRICS non_client_metrics;
    non_client_metrics.cbSize = sizeof( non_client_metrics );
    SystemParametersInfo( SPI_GETNONCLIENTMETRICS, sizeof( non_client_metrics ), &non_client_metrics, 0 );

    this->bold        = non_client_metrics.lfMessageFont.lfWeight > 600;
    this->strikeout   = non_client_metrics.lfMessageFont.lfStrikeOut != 0;
    this->italic      = non_client_metrics.lfMessageFont.lfItalic != 0;
    this->underline   = non_client_metrics.lfMessageFont.lfUnderline != 0;
    this->size        = non_client_metrics.lfMessageFont.lfHeight;
    this->font_name   = non_client_metrics.lfMessageFont.lfFaceName;

    font_handle = CreateFontIndirect( &non_client_metrics.lfMessageFont );
}

Font::operator HFONT()
{
    return font_handle;
}

}