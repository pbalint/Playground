#include "ImageHelper.h"
#include <memory.h>

namespace BLib
{

void CopyBmpData( Image* image, const BMPHeader& bmp_header )
{
    int src_scanline = bmp_header.GetScanLineSize();
    int dst_scanline = image->GetScanLineInBytes();
    int dst_line_offset = dst_scanline;
    unsigned char* src_ptr = bmp_header.GetBMPData();
    unsigned char* dst_ptr = image->GetPtr();
    if ( bmp_header.IsFlipped() )
    {
        dst_ptr += ( image->GetHeight() - 1 ) * dst_scanline;
        dst_line_offset *= -1;
    }

    for ( unsigned int y=0; y < image->GetHeight(); y++ )
    {
        unsigned char* line_src_ptr = src_ptr;
        unsigned char* line_dst_ptr = dst_ptr;
        for ( unsigned int x=0; x < image->GetWidth(); x++ )
        {
            line_dst_ptr[ 0 ] = line_src_ptr[ 2 ];
            line_dst_ptr[ 1 ] = line_src_ptr[ 1 ];
            line_dst_ptr[ 2 ] = line_src_ptr[ 0 ];
            line_dst_ptr += image->GetPixelSize();
            line_src_ptr += image->GetPixelSize();
        }
        src_ptr += src_scanline;
        dst_ptr += dst_line_offset;
    }
}

void CopyBmpDataUsingPalette( Image* image, const BMPHeader& bmp_header )
{
    int src_scanline = bmp_header.GetScanLineSize();
    int dst_scanline = image->GetScanLineInBytes();
    unsigned char* src_ptr = bmp_header.GetBMPData();
    unsigned char* dst_ptr = image->GetPtr();
    if ( bmp_header.IsFlipped() )
    {
        dst_ptr += ( image->GetHeight() - 1 ) * dst_scanline;
        dst_scanline *= -1;
    }

    const int palette_entry_size = ( bmp_header.GetBMPData() - bmp_header.GetPalette() ) / bmp_header.GetPaletteEntryCount();
    unsigned char* palette = bmp_header.GetPalette();
    for ( unsigned int y=0; y < image->GetHeight(); y++ )
    {
        unsigned char* line_src_ptr = src_ptr;
        unsigned char* line_dst_ptr = dst_ptr;
        for ( unsigned int x=0; x < image->GetWidth(); x++ )
        {
            line_dst_ptr[ 2 ] = palette[ *line_src_ptr * palette_entry_size     ];
            line_dst_ptr[ 1 ] = palette[ *line_src_ptr * palette_entry_size + 1 ];
            line_dst_ptr[ 0 ] = palette[ *line_src_ptr * palette_entry_size + 2 ];
            line_src_ptr ++;
            line_dst_ptr += image->GetPixelSize();
        }
        dst_ptr += dst_scanline;
        src_ptr += src_scanline;
    }
}

void ShuffleRGBToBGR( Image* image )
{
    unsigned char* ptr = image->GetPtr();
    for ( unsigned int y=0; y < image->GetHeight(); y++)
    {
        for ( unsigned int x=0; x < image->GetWidth(); x++ )
        {
            ptr[ 0 ] = ptr[ 2 ];
            ptr[ 1 ] = ptr[ 1 ];
            ptr[ 2 ] = ptr[ 0 ];
            ptr += image->GetPixelSize();
        }
    }
}

void ConvertGrayToRGB( unsigned char* src_ptr, unsigned char* dst_ptr, unsigned int width, unsigned int height, unsigned int src_padding, unsigned int dst_padding )
{
    for ( unsigned int y=0; y < height; y++ )
    {
        for ( unsigned int x=0; x < width; x++ )
        {
            dst_ptr[ 0 ] = *src_ptr;
            dst_ptr[ 1 ] = *src_ptr;
            dst_ptr[ 2 ] = *src_ptr;

            src_ptr += 1;
            dst_ptr += 3;
        }
        src_ptr += src_padding;
        dst_ptr += dst_padding;
    }
}

void ConvertGrayToRGBA( unsigned char* src_ptr, unsigned char* dst_ptr, unsigned int width, unsigned int height, unsigned int src_padding, unsigned int dst_padding )
{
    for ( unsigned int y=0; y < height; y++ )
    {
        for ( unsigned int x=0; x < width; x++ )
        {
            dst_ptr[ 0 ] = *src_ptr;
            dst_ptr[ 1 ] = *src_ptr;
            dst_ptr[ 2 ] = *src_ptr;
            dst_ptr[ 3 ] = 255;

            src_ptr += 1;
            dst_ptr += 4;
        }
        src_ptr += src_padding;
        dst_ptr += dst_padding;
    }
}

void ConvertRGBToGray( unsigned char* src_ptr, unsigned char* dst_ptr, unsigned int width, unsigned int height, unsigned int src_padding, unsigned int dst_padding )
{
    for ( unsigned int y=0; y < height; y++ )
    {
        for ( unsigned int x=0; x < width; x++ )
        {
            dst_ptr[ 0 ] = ( src_ptr[ 0 ] + src_ptr[ 1 ] + src_ptr[ 2 ] ) / 3;

            src_ptr += 3;
            dst_ptr += 1;
        }
        src_ptr += src_padding;
        dst_ptr += dst_padding;
    }
}

void ConvertRGBToRGBA( unsigned char* src_ptr, unsigned char* dst_ptr, unsigned int width, unsigned int height, unsigned int src_padding, unsigned int dst_padding )
{
    for ( unsigned int y=0; y < height; y++ )
    {
        for ( unsigned int x=0; x < width; x++ )
        {
            dst_ptr[ 0 ] = src_ptr[ 0 ];
            dst_ptr[ 1 ] = src_ptr[ 1 ];
            dst_ptr[ 2 ] = src_ptr[ 2 ];
            dst_ptr[ 3 ] = 255;

            src_ptr += 3;
            dst_ptr += 4;
        }
        src_ptr += src_padding;
        dst_ptr += dst_padding;
    }
}

void ConvertRGBAToGray( unsigned char* src_ptr, unsigned char* dst_ptr, unsigned int width, unsigned int height, unsigned int src_padding, unsigned int dst_padding )
{
    for ( unsigned int y=0; y < height; y++ )
    {
        for ( unsigned int x=0; x < width; x++ )
        {
            dst_ptr[ 0 ] = ( src_ptr[ 0 ] + src_ptr[ 1 ] + src_ptr[ 2 ] ) * 255 / ( src_ptr[ 3 ] * 3 );

            src_ptr += 4;
            dst_ptr += 1;
        }
        src_ptr += src_padding;
        dst_ptr += dst_padding;
    }
}

void ConvertRGBAToRGB( unsigned char* src_ptr, unsigned char* dst_ptr, unsigned int width, unsigned int height, unsigned int src_padding, unsigned int dst_padding )
{
    for ( unsigned int y=0; y < height; y++ )
    {
        for ( unsigned int x=0; x < width; x++ )
        {
            dst_ptr[ 0 ] = src_ptr[ 0 ] * 255 / src_ptr[ 3 ];
            dst_ptr[ 1 ] = src_ptr[ 1 ] * 255 / src_ptr[ 3 ];
            dst_ptr[ 2 ] = src_ptr[ 2 ] * 255 / src_ptr[ 3 ];

            src_ptr += 4;
            dst_ptr += 3;
        }
        src_ptr += src_padding;
        dst_ptr += dst_padding;
    }
}

}