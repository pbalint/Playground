#pragma once

#include "String.h"

namespace BLib
{

class Image
{
public:
    enum PixelFormat
    {
        Invalid = 0, Gray, RGB, RGBA
    };

private:
    unsigned char*  data;
    bool            free_buffer;
    unsigned char   pixel_size;
    unsigned int    width;
    unsigned int    padding;
    unsigned int    height;
    PixelFormat     pixel_format;

public:
    Image( unsigned int width,  unsigned int height, PixelFormat pixel_format, unsigned char* data = 0, bool free_buffer = true, unsigned int padding = 0 );
    Image( const Image& other );
    Image& operator =( const Image& other );
    ~Image();
    unsigned char* GetPtr() const;
    unsigned char& GetPixel( unsigned char x, unsigned char y ) const;
    unsigned int GetWidth() const { return width; }
    unsigned int GetHeight() const { return height; }
    unsigned int GetPadding() const { return padding; }
    unsigned int GetScanLine() const { return width + padding; }
    unsigned int GetScanLineInBytes() const { return ( width + padding ) * GetPixelSize(); }
    unsigned int GetImageSizeInBytes() const { return GetScanLineInBytes() * GetHeight(); }
    unsigned char GetPixelSize () const { return pixel_size; }
    unsigned char GetBpp() const  { return pixel_size * 8; };
    PixelFormat GetPixelFormat() const  { return pixel_format; }
    Image* Convert( PixelFormat pixel_format ) const;

    static unsigned char GetBppForPixelFormat( PixelFormat pixel_format );
    static Image* LoadPNG( const String& file_name );
    static Image* LoadBMP( const String& file_name );
    void SavePNG( const String& file_name ) const ;
};

}