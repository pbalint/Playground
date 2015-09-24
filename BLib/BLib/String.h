#pragma once

#include "Buffer.h"

namespace BLib
{

class String 
{
protected:
    Buffer      buffer;             
    size_t      length;
    bool        unicode;
    bool        null_terminated;

public:
    static bool   null_terminated_by_default;  // initializing from char* / wchar_t* will/will not be null terminated by default
    static bool   store_only_unicode;          
    static size_t growth_limit;                // appending strings will double their size until they reach this limit

    static void CharToWChar( const char    input[], wchar_t output[], size_t chars );
    static void WCharToChar( const wchar_t input[], char    output[], size_t chars );

    String();
    String( const char str[], bool null_terminated = null_terminated_by_default );
    String( const wchar_t str[], bool null_terminated = null_terminated_by_default );
    String( const String& other );
    String( Buffer& buffer, bool null_terminated, bool unicode );

    String& operator =( const char str[] );
    String& operator =( const wchar_t str[] );
    String& operator =( const String& other );

    bool operator ==( const char str[] ) const;
    bool operator ==( const wchar_t str[] ) const;
    bool operator ==( const String& other ) const;
    bool operator !=( const char str[] ) const;
    bool operator !=( const wchar_t str[] ) const;
    bool operator !=( const String& other ) const;

    String& operator +=( const String& str );
    String& operator +=( const char str );
    String& operator +=( const wchar_t str );
    String& operator +=( const int num );
    String& operator +=( const unsigned int num );
    String& operator +=( const double num );
    String& operator +=( const long double num );
    String& operator +=( const long long num );
    String& operator +=( const unsigned long long num );

    size_t GetLength() const { return length; }
    size_t GetTotalLength() const { return length + ( null_terminated? 1 : 0 ); }
    size_t GetLengthInBytes() const { return unicode? length * sizeof( wchar_t ): length; }
    size_t GetTotalLengthInBytes() const
    { 
        if ( unicode )
        {
            if ( null_terminated ) return (length+1) * sizeof( wchar_t );
            else                   return length * sizeof( wchar_t );
        }
        else
        {
            if ( null_terminated ) return length+1;
            else                   return length;
        }
    }

    void ConvertToUnicode();
    void ConvertToAscii();
    void NullTerminate();

    bool IsUnicode() const { return unicode; }
    bool IsNullTerminated() const { return null_terminated; }

    char& Char( size_t index )           { return buffer.ptr.As< char >()[ index ]; }
    char  Char( size_t index ) const     { return buffer.ptr.As< char >()[ index ]; }
    wchar_t& WChar( size_t index )       { return buffer.ptr.As< wchar_t >()[ index ]; }
    wchar_t  WChar( size_t index ) const { return buffer.ptr.As< wchar_t >()[ index ]; }

    operator char* ()                { return buffer.ptr.As< char >(); }
    operator wchar_t *()             { return buffer.ptr.As< wchar_t >(); }
    operator const char* () const    { return buffer.ptr.As< char >(); }
    operator const wchar_t *() const { return buffer.ptr.As< wchar_t >(); }
};

}