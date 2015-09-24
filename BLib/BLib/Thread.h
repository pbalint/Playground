/*#pragma once

#include <Windows.h>

class Thread
{
public:
    enum ThreadState { Invalid = -1, Running, Suspended, Stopped };

protected:
    HANDLE          thread_handle;
    unsigned long   thread_id;
    ThreadState     thread_state;

    static unsigned long __stdcall DispatchThread( void* context );
    Thread( bool ) {}

public:
    Thread();
    virtual ~Thread();
    virtual void Run() {}
    void Start();
    void Pause();

    ThreadState GetThreadState() { return thread_state; }
    Thread GetCurrentThread();
};

*/