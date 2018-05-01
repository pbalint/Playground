#include <stdexcept>
#include <string>
#include <sstream>
#include <chrono>
#include <stdio.h>
#include <unistd.h>
#include <sys/time.h>

#include "zmq.h"

#include "proto/measurement.pb.h"
#include "lm92.h"

using namespace std::chrono;

int main(int argc, char *argv[])
{
    LM92* lm92 = NULL;
    try
    {
        lm92 = new LM92( "/dev/i2c-1", 0x4b );
    }
    catch (std::runtime_error& exception)
    {
        printf( "%s\n", exception.what() );
        exit( -1 );
    }

    printf( "Chip id: %x\n", lm92->GetChipId() );

    void* context = zmq_ctx_new();
    void* cmd_socket = zmq_socket( context, ZMQ_PUB );
    std::stringstream commandUrl;
    commandUrl << "tcp://*:5050";

    int rc = zmq_bind( cmd_socket, commandUrl.str().c_str() );

    bool keep_running = true;
    int buffer_size = 4096;
    char* buffer = new char[ buffer_size ];
    memset( buffer, 0, buffer_size );
    messages::Measurement measurement;
    measurement.set_value( 0 );

    double temperature = lm92->GetTemperature();
    while ( keep_running )
    {
        temperature = (temperature + lm92->GetTemperature()) / 2;
        milliseconds ms = duration_cast< milliseconds >( system_clock::now().time_since_epoch() );
        measurement.set_timestamp( ms.count() );
        measurement.set_input( 0 );
        measurement.set_value( temperature );
        bool success = measurement.SerializeToArray( buffer, buffer_size );

        const int sent_size = zmq_send( cmd_socket, buffer, measurement.GetCachedSize(), 0 );
        usleep( 1000000 );
    }
    delete[] buffer;
    zmq_close( cmd_socket );
    zmq_ctx_destroy( context );
    delete lm92;

    return 0;
}