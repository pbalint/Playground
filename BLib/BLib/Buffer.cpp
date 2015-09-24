#include <windows.h>
#include "String.h"

namespace BLib
{

Buffer::Buffer() { size = 0; }

Buffer::Buffer( size_t size )
:
ptr( new unsigned char[ size ] )
{
    this->size      = size;
}

Buffer::Buffer( const void* src, size_t size )
{
    if ( size == 0 )
    {
        size = strlen( (char*)src );
    }

    this->size      = size;
    ptr             = new unsigned char[ size ];
    CopyFrom( src, size );
}

Buffer::Buffer( const Buffer& other )
{
    size        = other.size;
    ptr         = new unsigned char[ size ];
    CopyFrom( other );
}

Buffer& Buffer::operator =( const Buffer& other )
{
    if ( this != &other )
    {
        SetSize( other.size );
        CopyFrom( other );
    }

    return *this;
}

Buffer& Buffer::operator =( const void* src )
{
    if ( ptr != src )
    {
        size = strlen( (char*)src );

        SetSize( size );
        CopyFrom( src, size );
    }

    return *this;
}

void Buffer::CopyFrom( const void* src, size_t size, ptrdiff_t src_offset, ptrdiff_t dst_offset )
{
    memcpy( ptr + dst_offset, (unsigned char*)src + src_offset, size );
}

void Buffer::CopyFrom( const Buffer& other, size_t size, ptrdiff_t src_offset, ptrdiff_t dst_offset )
{
    if ( size == 0 ) size = other.size;

    memcpy( ptr + dst_offset, other.ptr + src_offset, size );
}

void Buffer::CopyTo( const void* dst, size_t size, ptrdiff_t src_offset, ptrdiff_t dst_offset ) const
{
    if ( size == 0 ) size = this->size;

    memcpy( (unsigned char*)dst + dst_offset, ptr + src_offset, size );
}

void Buffer::CopyTo( Buffer& other, size_t size, ptrdiff_t src_offset, ptrdiff_t dst_offset ) const
{
    if ( size == 0 ) size = this->size;

    memcpy( other.ptr + dst_offset, ptr + src_offset, size );
}

void Buffer::Map( void* ptr, size_t size )
{
    if ( this->ptr == ptr ) return;

    this->ptr   = (unsigned char*)ptr;
    this->size  = (size == 0)?strlen( (char*)ptr ) : size;
}

void Buffer::Map( const void* ptr, size_t size )
{
    if ( this->ptr == ptr ) return;

    this->ptr   = (const unsigned char*)ptr;
    this->size  = (size == 0)? strlen( (const char*)ptr ) : size;
}

void Buffer::Map( const Buffer& other )
{
    size = other.size;
    ptr  = other.ptr;
}

void Buffer::SetSize( size_t size )
{
    this->size = size;
    ptr = new unsigned char[ size ];
}

bool Buffer::Load( const String& file_name )
{
    HANDLE file = INVALID_HANDLE_VALUE;
    if ( file_name.IsUnicode() )
    {
        file = CreateFileW( file_name, GENERIC_READ, FILE_SHARE_READ, 0, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, 0 );
    }
    else
    {
        file = CreateFileA( file_name, GENERIC_READ, FILE_SHARE_READ, 0, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, 0 );
    }
    if ( file == INVALID_HANDLE_VALUE ) return false;

    LARGE_INTEGER file_size;
    if ( !GetFileSizeEx( file, &file_size ) )
    {
        CloseHandle( file );
        return false;
    }

    SetSize( (size_t)file_size.QuadPart );

    LARGE_INTEGER read_offset = { 0 };
    DWORD bytes_read;
    while ( ReadFile( file,
                      ptr + read_offset.QuadPart,
                      (file_size.HighPart == 0)? file_size.LowPart: 0xFFFFFFFF,
                      &bytes_read,
                      0 )
            &&
            file_size.QuadPart != 0 )
    {
        file_size.QuadPart   -= bytes_read;
        read_offset.QuadPart += bytes_read;
        if ( !SetFilePointerEx( file, read_offset, 0, FILE_BEGIN ) ) break;
    }

    CloseHandle( file );
    return ( file_size.QuadPart == 0 );
}

bool Buffer::Save( const String& file_name )
{
    HANDLE file = INVALID_HANDLE_VALUE;
    if ( file_name.IsUnicode() )
    {
        file = CreateFileW( file_name, GENERIC_WRITE, 0, 0, CREATE_ALWAYS, FILE_ATTRIBUTE_NORMAL, 0 );
    }
    else
    {
        file = CreateFileA( file_name, GENERIC_WRITE, 0, 0, CREATE_ALWAYS, FILE_ATTRIBUTE_NORMAL, 0 );
    }
    if ( file == INVALID_HANDLE_VALUE ) return false;
    
    LARGE_INTEGER write_offset = { 0 };
    LARGE_INTEGER file_size;
    file_size.QuadPart = size;
    DWORD bytes_written;
    while ( WriteFile( file,
                       ptr + write_offset.QuadPart,
                       (file_size.HighPart == 0)? file_size.LowPart : 0xFFFFFFFF,
                       &bytes_written,
                       0 )
            &&
            file_size.QuadPart != 0 )
    {
        file_size.QuadPart    -= bytes_written;
        write_offset.QuadPart += bytes_written;
        if ( !SetFilePointerEx( file, write_offset, 0, FILE_BEGIN ) ) break;
    }

    CloseHandle( file );
    return ( file_size.QuadPart == 0 );
}

void Buffer::SHA1( Buffer& sha1 )
{
    unsigned int h0 = 0x67452301;
    unsigned int h1 = 0xEFCDAB89;
    unsigned int h2 = 0x98BADCFE;
    unsigned int h3 = 0x10325476;
    unsigned int h4 = 0xC3D2E1F0;

    size_t buffer_length = ((size + 1 + 8 + 63 )/64)*64;
    unsigned char* input = new unsigned char[ buffer_length ];
    memcpy( input, ptr, size );
    input[ size ] = 0x80;
    memset( input + size + 1, 0, buffer_length - size - 1 - 8 );
    const unsigned long long encoded_length = size * 8;
    for ( int i=0; i<8; i++ ) 
    {
        input[ buffer_length - (i+1) ] = ((unsigned char*)&encoded_length)[ i ];
    }
    unsigned int w[ 80 ];
    unsigned int a, b, c, d, e, temp;
    for ( size_t block_start = 0; block_start < buffer_length; block_start += 64 )
    {
        unsigned int* w_ptr = w;
        for ( int i=0; i<64; i+=4 )
        {
            *w_ptr++ = (input[ block_start + i     ] << 24 ) +
                       (input[ block_start + i + 1 ] << 16 ) +
                       (input[ block_start + i + 2 ] << 8 ) +
                        input[ block_start + i + 3 ];

        }

        for ( int i=16; i<80; i++ )
        {
             w[ i ] = _rotl( ( w[ i-3 ] ^ w[ i-8 ] ^ w[ i-14 ] ^ w[ i-16 ] ), 1 ); 
        }

        a = h0;
        b = h1;
        c = h2;
        d = h3;
        e = h4;

        for ( int i=0; i<20; i++ )
        {
            temp = _rotl( a, 5 ) + ( ( b & c ) | ( (~b) & d ) ) + e + w[ i ] + 0x5A827999;
            e = d;
            d = c;
            c = _rotl( b, 30 );
            b = a;
            a = temp;
        }
        for ( int i=20; i<40; i++ )
        {
            temp = _rotl( a, 5 ) + ( b ^ c ^ d ) + e + w[ i ] + 0x6ED9EBA1;
            e = d;
            d = c;
            c = _rotl( b, 30 );
            b = a;
            a = temp;
        }
        for ( int i=40; i<60; i++ )
        {
            temp = _rotl( a, 5 ) + ( ( b & c ) | ( b & d ) | ( c & d ) ) + e + w[ i ] + 0x8F1BBCDC;
            e = d;
            d = c;
            c = _rotl( b, 30 );
            b = a;
            a = temp;
        }
        for ( int i=60; i<80; i++ )
        {
            temp = _rotl( a, 5 ) + ( b ^ c ^ d ) + e + w[ i ] + 0xCA62C1D6;
            e = d;
            d = c;
            c = _rotl( b, 30 );
            b = a;
            a = temp;
        }

        h0 += a;
        h1 += b;
        h2 += c;
        h3 += d;
        h4 += e;

    }

    sha1.SetSize( 20 );
    for ( int i=0; i<4; i++ ) sha1.ptr[ i      ] = ((char*)&h0)[ 3 - i ];
    for ( int i=0; i<4; i++ ) sha1.ptr[ i + 4  ] = ((char*)&h1)[ 3 - i ];
    for ( int i=0; i<4; i++ ) sha1.ptr[ i + 8  ] = ((char*)&h2)[ 3 - i ];
    for ( int i=0; i<4; i++ ) sha1.ptr[ i + 12 ] = ((char*)&h3)[ 3 - i ];
    for ( int i=0; i<4; i++ ) sha1.ptr[ i + 16 ] = ((char*)&h4)[ 3 - i ];
    delete[] input;
}

}