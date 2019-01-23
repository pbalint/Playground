#include <stdio.h>
#include "DataTransferThread.h"
#include "Util.h"

DataTransferThread::DataTransferThread( Socket* input, Socket* output, unsigned int buffer_size )
    :
    Thread( true ),
    input( input ),
    output( output ),
    thread_name( AllocString( "%s:%i -> %s:%i", input->GetLocalAddress(), input->GetLocalPort(), output->GetRemoteAddress(), output->GetRemotePort() ) ),
    buffer_size( buffer_size )
{
    buffer = new char[ buffer_size ];
}

DataTransferThread::~DataTransferThread()
{
    delete[] thread_name;
    delete[] buffer;
}

void DataTransferThread::Execute()
{
    long long data_size;
    printf( "Starting data transfer thread: %s\n", thread_name );
    while ( true )
    {
        data_size = input->Receive( buffer, buffer_size );
        if ( data_size <= 0 )
        {
            printf( "Stopping data transfer thread: %s\n", thread_name );
            output->Close();
            break;
        }

        data_size = output->Send( buffer, (unsigned int)data_size );
        if ( data_size <= 0 )
        {
            printf( "Stopping data transfer thread: %s\n", thread_name );
            input->Close();
            break;
        }
    }
}
