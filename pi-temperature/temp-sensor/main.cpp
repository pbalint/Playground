#include <stdio.h>
#include <unistd.h>
#include <sys/time.h>

#include "lm92.h"

using namespace std;

int main(int argc, char *argv[])
{
    LM92 lm92( "/dev/i2c-1", 0x4b );

    printf( "Chip id: %x\n", lm92.GetChipId() );
    
    FILE* out = fopen( "temperatures.csv", "aw" );
    timeval tp;
    while (true)
    {
        gettimeofday( &tp, NULL );
        unsigned long long time_stamp = (unsigned long long)tp.tv_sec * 1000 + tp.tv_usec / 1000;
        fprintf( out, "%llu;%.2f\n", time_stamp, lm92.GetTemperature() );
        fflush( out );
        sleep( 1 );
    }
    fclose( out );
    return 0;
}