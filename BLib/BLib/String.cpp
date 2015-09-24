#include "String.h"
#include <string.h>
#include <stdio.h>


#pragma warning( push )
#pragma warning( disable: 4996 ) // consider using the _s version instead

namespace BLib
{

bool   String::null_terminated_by_default = true;
size_t String::growth_limit               = 4096;
bool   String::store_only_unicode         = true;

String::String()
{
    unicode         = store_only_unicode;
    null_terminated = null_terminated_by_default;
    length          = 0;
}

String::String( const char str[], bool null_terminated )
{
    unicode                 = false;
    this->null_terminated   = null_terminated;
    length                  = strlen( str );
    buffer.SetSize( GetTotalLengthInBytes() );
    buffer.CopyFrom( str, GetTotalLengthInBytes() );

    if ( store_only_unicode )
    {
        ConvertToUnicode();
    }
}

String::String( const wchar_t str[], bool null_terminated )
{
    unicode                 = true;
    this->null_terminated   = null_terminated;
    length                  = wcslen( str );
    buffer.SetSize( GetTotalLengthInBytes() );
    buffer.CopyFrom( str, GetTotalLengthInBytes() );
}

String::String( const String& other )
{
    unicode             = other.unicode;
    null_terminated     = other.null_terminated;
    length              = other.length;
    buffer.SetSize( other.buffer.GetSize() );
    buffer.CopyFrom( other.buffer );
}

String::String( Buffer& buffer, bool null_terminated, bool unicode )
{
    this->unicode           = unicode;
    this->null_terminated   = null_terminated;
    length                  = buffer.GetSize() / ( unicode? sizeof( wchar_t ): 1 ) - ( null_terminated? 1: 0 );
    this->buffer            = buffer;
}

String& String::operator =( const char str[] )
{
    if ( buffer != (void*)str )
    {
        unicode           = false;
        null_terminated   = null_terminated_by_default;
        length            = strlen( str );
        buffer.SetSize( GetTotalLengthInBytes() );
        buffer.CopyFrom( str, GetTotalLengthInBytes() );

        if ( store_only_unicode )
        {
            ConvertToUnicode();
        }
    }

    return *this;
}

String& String::operator =( const wchar_t str[] )
{
    if ( buffer != (void*)str )
    {
        unicode           = true;
        null_terminated   = null_terminated_by_default;
        length            = wcslen( str );
        buffer.SetSize( GetTotalLengthInBytes() );
        buffer.CopyFrom( str, GetTotalLengthInBytes() );
    }

    return *this;
}

String& String::operator =( const String& other )
{
    if ( this != &other )
    {
        unicode             = other.unicode;
        null_terminated     = other.null_terminated;
        length              = other.length;
        buffer.SetSize( GetTotalLengthInBytes() );
        buffer.CopyFrom( other.buffer, GetTotalLengthInBytes() );
    }

    return *this;
}

void String::CharToWChar( const char input[], wchar_t output[], size_t chars )
{
    for ( size_t i = 0; i < chars; i++ ) output[ i ] = (wchar_t)input[ i ];
}

void String::WCharToChar( const wchar_t input[], char output[], size_t chars )
{
    for ( size_t i = 0; i < chars; i++ ) output[ i ] = (char)input[ i ];
}

void String::ConvertToAscii()
{
    if ( !unicode ) return;

    unicode = false;
    Buffer new_buffer( GetTotalLengthInBytes() );
    WCharToChar( buffer.ptr.As< wchar_t >(), new_buffer.ptr.As< char >(), GetTotalLength() ); 
    buffer = new_buffer;
}

void String::ConvertToUnicode()
{
    if ( unicode ) return;

    unicode = true;
    Buffer new_buffer( GetTotalLengthInBytes() );
    CharToWChar( buffer.ptr.As< char >(), new_buffer.ptr.As< wchar_t >(), GetTotalLength() ); 
    buffer = new_buffer;
}

void String::NullTerminate()
{
    if ( null_terminated ) return;

    null_terminated = true;
    Buffer new_buffer( GetTotalLengthInBytes() );
    if ( unicode ) ((wchar_t*)new_buffer.GetPtr( buffer.size ))[0] = 0;
    else (new_buffer.GetPtr( buffer.size ))[0] = 0;
    buffer = new_buffer;
}

bool String::operator ==( const char str[] ) const
{
    if ( unicode ) return false;

    return ( memcmp( buffer, str, GetLengthInBytes() ) == 0 );
}

bool String::operator ==( const wchar_t str[] ) const
{
    if ( !unicode ) return false;

    return ( memcmp( buffer, str, GetLengthInBytes() ) == 0 );
}

bool String::operator ==( const String& str ) const
{
    if (    unicode != str.unicode || 
            length  != str.length ) return false;
    return ( memcmp( buffer, str.buffer, GetLengthInBytes() ) == 0 );
}

bool String::operator !=( const char str[] ) const
{
    return !( *this == str );
}

bool String::operator !=( const wchar_t str[] ) const
{
    return !( *this == str );
}

bool String::operator !=( const String& str ) const
{
    return !( *this == str );
}

String& String::operator +=( const String& str )
{
    size_t other_length          = str.length;
    size_t other_length_in_bytes = str.GetLengthInBytes();

    if ( buffer.size - GetTotalLengthInBytes() < other_length_in_bytes )
    {
        size_t exact_size = buffer.size + other_length_in_bytes;
        size_t new_size = 1;
        if ( exact_size < growth_limit )
        {
            while ( ( exact_size >>= 1 ) != 0 ) new_size <<= 1;
            new_size <<= 1;
        }
        else
        {
            exact_size = ( exact_size + growth_limit - 1 ) / growth_limit;
        }
        Buffer new_buffer( new_size );
        buffer.CopyTo( new_buffer );
        buffer = new_buffer;
    }

    memcpy( buffer.GetPtr( GetLengthInBytes() ), str.buffer, other_length_in_bytes );
    length += other_length;
    if ( null_terminated )
    {
        if ( unicode )
        {
            buffer.ptr.As< wchar_t >()[ GetLength() ] = 0;
        }
        else
        {
            buffer[ GetLength() ] = 0;
        }
    }

    return *this;
}

String& String::operator +=( const char c )
{
    const size_t buffer_size = 4;
    char temp[ buffer_size ];
    if ( store_only_unicode )
    {
        _snprintf( temp, buffer_size, "%c\0", c );
    }
    else
    {
        _snprintf( temp, buffer_size, "%c", c );
    }
    temp[ buffer_size-1 ] = 0;
    return *this += temp;
}

String& String::operator +=( const wchar_t c )
{
    const size_t buffer_size = 4;
    wchar_t temp[ buffer_size ];
    _snwprintf( temp, buffer_size, L"%c", c );
    temp[ buffer_size-1 ] = 0;
    return *this += temp;
}

String& String::operator +=( const int num )
{
    const size_t buffer_size = 64;
    if ( !unicode )
    {
        char temp[ buffer_size ];
        _snprintf( temp, buffer_size, "%i", num );
        temp[ buffer_size-1 ] = 0;
        return *this += temp;
    }
    else
    {
        wchar_t temp[ buffer_size ];
        _snwprintf( temp, buffer_size, L"%i", num );
        temp[ buffer_size-1 ] = 0;
        return *this += temp;
    }
}

String& String::operator +=( const unsigned int num )
{
    const size_t buffer_size = 64;
    if ( !unicode )
    {
        char temp[ buffer_size ];
        _snprintf( temp, buffer_size, "%u", num );
        temp[ buffer_size-1 ] = 0;
        return *this += temp;
    }
    else
    {
        wchar_t temp[ buffer_size ];
        _snwprintf( temp, buffer_size, L"%u", num );
        temp[ buffer_size-1 ] = 0;
        return *this += temp;
    }
}

String& String::operator +=( const double num )
{
    const size_t buffer_size = 128;
    if ( !unicode )
    {
        char temp[ buffer_size ];
        _snprintf( temp, buffer_size, "%f", num );
        temp[ buffer_size-1 ] = 0;
        return *this += temp;
    }
    else
    {
        wchar_t temp[ buffer_size ];
        _snwprintf( temp, buffer_size, L"%f", num );
        temp[ buffer_size-1 ] = 0;
        return *this += temp;
    }
}

String& String::operator +=( const long double num )
{
    const size_t buffer_size = 128;
    if ( !unicode )
    {
        char temp[ buffer_size ];
        _snprintf( temp, buffer_size, "%Lf", num );
        temp[ buffer_size-1 ] = 0;
        return *this += temp;
    }
    else
    {
        wchar_t temp[ buffer_size ];
        _snwprintf( temp, buffer_size, L"%Lf", num );
        temp[ buffer_size-1 ] = 0;
        return *this += temp;
    }
}

String& String::operator +=( const long long num )
{
    const size_t buffer_size = 64;
    if ( !unicode )
    {
        char temp[ buffer_size ];
        _snprintf( temp, buffer_size, "%lli", num );
        temp[ buffer_size-1 ] = 0;
        return *this += temp;
    }
    else
    {
        wchar_t temp[ buffer_size ];
        _snwprintf( temp, buffer_size, L"%lli", num );
        temp[ buffer_size-1 ] = 0;
        return *this += temp;
    }
}

String& String::operator +=( const unsigned long long num )
{
    const size_t buffer_size = 64;
    if ( !unicode )
    {
        char temp[ buffer_size ];
        _snprintf( temp, buffer_size, "%llu", num );
        temp[ buffer_size-1 ] = 0;
        return *this += temp;
    }
    else
    {
        wchar_t temp[ buffer_size ];
        _snwprintf( temp, buffer_size, L"%llu", num );
        temp[ buffer_size-1 ] = 0;
        return *this += temp;
    }
}

}

#pragma warning( pop )
