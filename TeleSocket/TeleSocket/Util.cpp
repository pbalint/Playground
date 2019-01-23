#include <stdarg.h>
#include <stdio.h>
#ifdef _WIN32
    #include <string.h>
#else
    #include <strings.h>
#endif

char* AllocString( const char* format, ... )
{
    va_list args;
    va_start( args, format );

    int size_needed = 1 + vsnprintf( nullptr, 0, format, args );
    char* string = new char[ size_needed ];
    vsnprintf( string, size_needed, format, args );

    va_end( args );

    return string;
}

int stricompare( char const* string1, char const* string2 )
{
#ifdef _WIN32
    return _stricmp( string1, string2 );
#else
    return strcasecmp( string1, string2 );
#endif

}
