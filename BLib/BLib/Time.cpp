#include "Time.h"
#include <windows.h>

namespace BLib
{

static inline long long FileTimeToMs( FILETIME time )
{
    return (( (long long)time.dwHighDateTime << 32 ) +
                         time.dwLowDateTime) / 10000;
}

Time::Time()
    :
    ms( 0 ),
    cache( NULL )
{
}

Time::Time( long long ms )
    :
    ms( ms ),
    cache( NULL )
{
}

Time::~Time()
{
    delete cache;
}

Time::Time( int year, int month, int day, int hour, int minute, int second, int ms )
{
    SYSTEMTIME* time = new SYSTEMTIME;
    cache = time;
    time->wYear          = (WORD)year;
    time->wMonth         = (WORD)month;
    time->wDay           = (WORD)day;
    time->wHour          = (WORD)hour;
    time->wMinute        = (WORD)minute;
    time->wSecond        = (WORD)second;
    time->wMilliseconds  = (WORD)ms;

    FILETIME file_time;
    if ( !SystemTimeToFileTime( time, &file_time ) ) throw;

    this->ms = FileTimeToMs( file_time );
}

const Time Time::GetUTCTime()
{
    FILETIME file_time;
    GetSystemTimeAsFileTime( &file_time );
    return (const Time)FileTimeToMs( file_time );
}

const Time Time::GetLocalTime()
{
    SYSTEMTIME sys_time;
    ::GetLocalTime( &sys_time );
    return Time( sys_time.wYear,
                 sys_time.wMonth,
                 sys_time.wDay,
                 sys_time.wHour,
                 sys_time.wMinute,
                 sys_time.wSecond,
                 sys_time.wMilliseconds );
}

void Time::CalculateTime() const
{
    if ( cache != NULL ) return;

    cache = new SYSTEMTIME;
    FILETIME file_time;
    const long long t = ms * 10000;
    file_time.dwHighDateTime = ( t >> 32 );
    file_time.dwLowDateTime  = ( t & 0xFFFFFFFF );
    FileTimeToSystemTime( &file_time, (SYSTEMTIME*)cache );
}

int Time::Year() const
{
    CalculateTime();
    return ((SYSTEMTIME*)cache)->wYear;
}

int Time::Month() const
{
    CalculateTime();
    return ((SYSTEMTIME*)cache)->wMonth;
}

int Time::Day() const
{
    CalculateTime();
    return ((SYSTEMTIME*)cache)->wDay;
}

int Time::Hour() const
{
    CalculateTime();
    return ((SYSTEMTIME*)cache)->wHour;
}

int Time::Minute() const
{
    CalculateTime();
    return ((SYSTEMTIME*)cache)->wMinute;
}

int Time::Second() const
{
    CalculateTime();
    return ((SYSTEMTIME*)cache)->wSecond;
}

int Time::MilliSecond() const
{
    CalculateTime();
    return ((SYSTEMTIME*)cache)->wMilliseconds;
}

const Time Time::operator +( const Time& other )
{
    return (const Time)( ms + other.ms );
}

const Time Time::operator -( const Time& other )
{
    return (const Time)( ms - other.ms );
}



}