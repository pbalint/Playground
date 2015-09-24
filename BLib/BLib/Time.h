#pragma once

namespace BLib
{

class Time
{
protected:
    long long         ms;       // milliseconds since january 1, 1601 
    mutable void*     cache;    // ms -> actual time components is done only once, and cached

    void CalculateTime() const;

public:
    Time();
    Time( long long ms );
    Time(   int year,
            int month,
            int day,
            int hour    = 0,
            int minute  = 0,
            int second  = 0,
            int ms      = 0 );
    ~Time();

    long long TotalMs() const;

    int Year() const;
    int Month() const;
    int Day() const;
    int Hour() const;
    int Minute() const;
    int Second() const;
    int MilliSecond() const;

    const Time operator +( const Time& other );
    const Time operator -( const Time& other );

    static const Time GetUTCTime();
    static const Time GetLocalTime();
};

}