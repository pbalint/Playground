#pragma once

#include <windows.h>
#include "String.h"

namespace BLib
{

class Service
{
friend class ServiceControlManager;

private:
    String          name;
    String          path;
    SC_HANDLE       handle;
    DWORD           start_type;
    SERVICE_STATUS  status;

public:
    Service( const String& name, const String& path = "", DWORD service_type = SERVICE_KERNEL_DRIVER, DWORD start_type = SERVICE_DEMAND_START )
    {
        this->name          = name;
        this->path          = path;
        this->handle        = NULL;
        this->start_type    = start_type;
        memset( &status, 0, sizeof( status ) );
        status.dwServiceType = service_type;

        this->name.ConvertToUnicode();
        this->path.ConvertToUnicode();
    }

    String GetName() { return name; }
    String GetPath() { return path; }
    HANDLE GetHandle() { return handle; }
    DWORD GetStartType() { return start_type; }
    SERVICE_STATUS GetStatus() { return status; }
};


class ServiceControlManager
{
private:
    SC_HANDLE handle;

public:
    enum ServiceCommand
    {
        Continue = SERVICE_CONTROL_CONTINUE,
        Refresh  = SERVICE_CONTROL_INTERROGATE,
        Pause    = SERVICE_CONTROL_PAUSE,
        Stop     = SERVICE_CONTROL_STOP
    };

    ServiceControlManager( const String& machine = L"" );
    bool Open( Service& service );
    bool Create( Service& service );
    bool Start( Service& service );
    bool Control( Service& service, ServiceCommand command );
    bool Delete( Service& service );
    bool Close( Service& service );
    ~ServiceControlManager();
};

}