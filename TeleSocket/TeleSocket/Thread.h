#pragma once

class Thread
{
public:
    enum State { STOPPED, STARTING, STARTED };

private:
    static void EntryPoint( Thread* instance );

protected:
    void* thread = nullptr;
    volatile State state = STOPPED;
    bool daemon;

public:
    Thread( bool daemon = false );
    virtual ~Thread();

    void Start();
    virtual void Execute() = 0;
    void Join();
};
