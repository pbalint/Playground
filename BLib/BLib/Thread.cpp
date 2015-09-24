/*#include "Thread.h"

unsigned long __stdcall Thread::DispatchThread( void* context )
{
    Thread* thread = ((Thread*)context);

    thread->Run();
    thread->thread_state = Stopped;
}


Thread::Thread()
{
    thread_handle = CreateThread( 0, 0, &Thread::DispatchThread, this, CREATE_SUSPENDED, &thread_id );
    if ( thread_handle == NULL )
    {
        thread_state = Invalid;
    }
    else
    {
        thread_state = Suspended;
    }
}

Thread::~Thread()
{
    if ( thread_state != Invalid )
    {
        CloseHandle( thread_handle );
    }
}

void Thread::Start()
{
    if ( thread_state == Suspended )
    {
        int suspend_count = ResumeThread( thread_handle );
        if ( suspend_count == 0 || suspend_count == 1 )
        {
            thread_state = Running;
        }
    }
}

void Thread::Pause()
{
    if ( thread_state == Running )
    {
        if ( SuspendThread( thread_handle ) > 0 )
        {
            thread_state = Suspended;
        }
    }
}

Thread Thread::GetCurrentThread()
{
    Thread thread( true );
    thread.thread_handle = ::GetCurrentThread();
    thread.thread_state  = Running;
    return thread;
}
*/