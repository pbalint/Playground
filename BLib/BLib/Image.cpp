#include "Image.h"
#include "External/lodepng.h"
#include "Buffer.h"
#include "ImageHelper.h"
#include <malloc.h>

namespace BLib
{

Image::Image( unsigned int width, unsigned int height, PixelFormat pixel_format, unsigned char* data, bool free_buffer, unsigned int padding )
{
    this->width         = width;
    this->height        = height;
    this->pixel_format  = pixel_format;
    this->padding       = padding;
    this->pixel_size    = GetBppForPixelFormat( pixel_format ) / 8;
    this->free_buffer   = free_buffer;

    if ( data != NULL && !free_buffer )
    {
        this->data = data;
    }
    else
    {
        this->data = new unsigned char[ GetImageSizeInBytes() ];
        if ( data != NULL )
        {
            memcpy( this->data, data, GetImageSizeInBytes() );
        }
    }
}

Image::Image( const Image& other )
{
    this->width         = other.width;
    this->height        = other.height;
    this->pixel_format  = other.pixel_format;
    this->padding       = other.padding;
    this->pixel_size    = other.pixel_size;
    this->free_buffer   = true;
    this->data          = new unsigned char[ GetImageSizeInBytes() ];
    memcpy( this->data, other.data, GetImageSizeInBytes() );
}

Image& Image::operator =( const Image& other )
{
    if ( this == &other ) return *this;

    this->width         = other.width;
    this->height        = other.height;
    this->pixel_format  = other.pixel_format;
    this->padding       = other.padding;
    this->pixel_size    = other.pixel_size;
    this->free_buffer   = true;
    this->data          = new unsigned char[ GetImageSizeInBytes() ];
    memcpy( this->data, other.data, GetImageSizeInBytes() );

    return *this;
}

Image::~Image()
{
    if ( free_buffer ) delete[] data;
}

unsigned char Image::GetBppForPixelFormat( PixelFormat pixel_format )
{
    switch ( pixel_format )
    {
        case Gray: return 8;
        case RGB:  return 24;
        case RGBA: return 32;
    }
    return 0;
}

unsigned char* Image::GetPtr() const
{
    return data;
}

unsigned char& Image::GetPixel( unsigned char x, unsigned char y ) const
{
    return data[ ( y * GetScanLineInBytes() + x ) * pixel_size ];
}

Image* Image::LoadPNG( const String& file_name )
{
    Buffer buffer;
    buffer.Load( file_name );
    Image* image = 0;
    unsigned char* data;
    unsigned int width;
    unsigned int height;
    int error = lodepng_decode32( &data, &width, &height, buffer, buffer.GetSize() );
    if ( !error )
    {
        image = new Image( width, height, Image::RGBA, data, true );
        free( data );
    }
    return image;
}

Image* Image::Convert( PixelFormat pixel_format ) const
{
    Image* image = NULL;

    if ( this->pixel_format == pixel_format )
    {
        image = new Image( *this );
    }
    else 
    {
        image = new Image( GetWidth(), GetHeight(), pixel_format );

        if ( this->pixel_format == Gray && pixel_format == RGB )         ConvertGrayToRGB( GetPtr(), image->GetPtr(), GetWidth(), GetHeight(), GetPadding(), image->GetPadding() );
        else if ( this->pixel_format == Gray && pixel_format == RGBA )   ConvertGrayToRGBA( GetPtr(), image->GetPtr(), GetWidth(), GetHeight(), GetPadding(), image->GetPadding() );
        else if ( this->pixel_format == RGB && pixel_format == Gray )    ConvertRGBToGray( GetPtr(), image->GetPtr(), GetWidth(), GetHeight(), GetPadding(), image->GetPadding() );
        else if ( this->pixel_format == RGB && pixel_format == RGBA )    ConvertRGBToRGBA( GetPtr(), image->GetPtr(), GetWidth(), GetHeight(), GetPadding(), image->GetPadding() );
        else if ( this->pixel_format == RGBA && pixel_format == Gray )   ConvertRGBAToGray( GetPtr(), image->GetPtr(), GetWidth(), GetHeight(), GetPadding(), image->GetPadding() );
        else if ( this->pixel_format == RGBA && pixel_format == RGB )    ConvertRGBAToRGB( GetPtr(), image->GetPtr(), GetWidth(), GetHeight(), GetPadding(), image->GetPadding() );
    }

    return image;
}

Image* Image::LoadBMP( const String& file_name )
{
    Buffer buffer;
    buffer.Load( file_name );

    Image* image = NULL;
    BMPHeader bmp_header( buffer );

    if ( bmp_header.IsValid() && bmp_header.GetCompressionMethod() == 0  )
    {
        PixelFormat pixel_format = Invalid;
        bool has_palette         = false;
        bool supported           = true;
        switch ( bmp_header.GetBpp() )
        {
            case 32:
                pixel_format = RGBA;
                break;

            case 24:
                pixel_format = RGB;
                break;

            case 8:
                pixel_format = RGB;
                has_palette  = true;
                break;

            default: 
                supported = false;
        }

        if ( supported )
        {
            image = new Image( bmp_header.GetWidth(), bmp_header.GetHeight(), pixel_format );
            if ( has_palette )
            {
                CopyBmpDataUsingPalette( image, bmp_header );
            }
            else
            {
                CopyBmpData( image, bmp_header );
            }
        }
    }

    return image;
}

void Image::SavePNG( const String& file_name ) const
{
    unsigned char* png_data;
    size_t png_size;
    LodePNGColorType color_type;
    switch ( pixel_format )
    {
        case RGBA:  color_type = LCT_RGBA;   break;
        case RGB:   color_type = LCT_RGB;    break;
        case Gray:  color_type = LCT_GREY;   break;
        default:
            return;
    }
    lodepng_encode_memory( &png_data, &png_size, data, width, height, color_type, 8 );
    Buffer png( png_data, png_size );
    png.Save( file_name );

}

}