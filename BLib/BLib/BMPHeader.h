#pragma once

#include <math.h>

namespace BLib
{

class BMPHeader
{
private:
    static const int FILE_HEADER_MAGIC          = 0x4d42;

    static const int FILE_HEADER_BMP_DATA       = 0x0a;
    static const int FILE_HEADER_SIZE           = 0x0e;

    static const int INFO_HEADER_SIZE           = 0;
    static const int INFO_HEADER_WIDTH          = 0x04;
    static const int INFO_HEADER_HEIGHT         = 0x08;
    static const int INFO_HEADER_COLOR_PLANES   = 0x0c;
    static const int INFO_HEADER_BPP            = 0x0e;
    static const int INFO_HEADER_COMPRESSION    = 0x10;
    static const int INFO_HEADER_PALETTE_SIZE   = 0x20;

    unsigned char* file_header;
    unsigned char* info_header;
    unsigned char* bmp_data;

    int ReadLeInt( unsigned char* ptr, unsigned int offset ) const
    {
        return ( ptr[ offset + 3 ] << 24 ) + ( ptr[ offset + 2 ] << 16 ) + ( ptr[ offset + 1 ] << 8 ) + ptr[ offset ];
    }

    short ReadLeShort( unsigned char* ptr, unsigned int offset ) const
    {
        return ( ptr[ offset + 1 ] << 8 ) + ptr[ offset ];
    }
public:
    BMPHeader( unsigned char* ptr )
    {
        this->file_header   = ptr;
        if ( !IsValid() ) return;

        this->bmp_data      = file_header + ReadLeInt( file_header, FILE_HEADER_BMP_DATA );
        this->info_header   = file_header + FILE_HEADER_SIZE;
    }

    bool IsValid() const { return file_header != 0 && ReadLeShort( file_header, 0 ) == FILE_HEADER_MAGIC; }

    int GetWidth() const { return ReadLeInt( info_header, INFO_HEADER_WIDTH ); }
    int GetScanLineSize() const { return ( ( GetWidth() * GetBpp() / 8 ) + 3 ) & (~3); } // BMP rows are 4 byte-aligned
    int GetHeight() const { return abs( ReadLeInt( info_header, INFO_HEADER_HEIGHT ) ); }
    unsigned int GetBpp() const { return ReadLeInt( info_header, INFO_HEADER_BPP ); }
    unsigned int GetCompressionMethod() const { return ReadLeInt( info_header, INFO_HEADER_COMPRESSION ); }
    bool IsFlipped() const { return ReadLeInt( info_header, INFO_HEADER_HEIGHT ) >= 0; }
    unsigned int GetPaletteEntryCount() const
    {
        int palette_size = ReadLeInt( info_header, INFO_HEADER_PALETTE_SIZE );
        if ( palette_size == 0 )
        {
            palette_size = ( 1 << GetBpp() );
        }
        return palette_size;
    }
    unsigned int GetInfoHeaderSize() const { return ReadLeInt( info_header, INFO_HEADER_SIZE ); }

    unsigned char* GetBMPData() const { return bmp_data; }
    unsigned char* GetPalette() const { return info_header + GetInfoHeaderSize(); }
};

}