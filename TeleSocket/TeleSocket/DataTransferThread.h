#pragma once

#include "Thread.h"
#include "Socket.h"

class DataTransferThread : public Thread
{
private:
    char*           buffer;
    unsigned int    buffer_size;
    Socket*         input;
    Socket*         output;
    char*           thread_name;

public:
    DataTransferThread( Socket* input, Socket* output, unsigned int buffer_size = 4096 );
    virtual void Execute();
    virtual ~DataTransferThread();
};

