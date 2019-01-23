#include <thread>
#include "Thread.h"

void Thread::EntryPoint( Thread* instance )
{
    instance->state = STARTED;
    instance->Execute();
    instance->state = STOPPED;

    if ( instance->daemon ) delete instance;
}

Thread::Thread( bool daemon )
{
    this->daemon = daemon;
}

Thread::~Thread()
{
    Join();
}

void Thread::Start()
{
    state = STARTING;
    thread = new std::thread( Thread::EntryPoint, this );
    if ( daemon ) ((std::thread*)thread)->detach();
    while ( state == STARTING );
}

void Thread::Join()
{
    if ( thread != nullptr )
    {
        std::thread* t = (std::thread*)thread;
        if ( t->joinable() ) t->join();
        delete t;
        thread = nullptr;
    }
}
