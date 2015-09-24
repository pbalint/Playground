#include "ServiceControlManager.h"

namespace BLib
{

ServiceControlManager::ServiceControlManager( const String& machine )
{
    if ( machine.IsUnicode() )
    {
        handle = OpenSCManagerW( machine, NULL, SC_MANAGER_CREATE_SERVICE );
    }
    else
    {
        handle = OpenSCManagerA( machine, NULL, SC_MANAGER_CREATE_SERVICE );
    }
}

ServiceControlManager::~ServiceControlManager()
{
    if ( handle != NULL ) CloseServiceHandle( handle );
}

bool ServiceControlManager::Create( Service &service )
{
    service.handle = CreateServiceW( handle,
                                     service.name, service.name,
                                     SERVICE_ALL_ACCESS,
                                     service.status.dwServiceType, service.start_type,
                                     SERVICE_ERROR_NORMAL,
                                     service.path,
                                     NULL, NULL, NULL, NULL, NULL );
    return ( service.handle != NULL );
}

bool ServiceControlManager::Open( Service& service )
{
    service.handle = OpenService( handle, service.name, SERVICE_ALL_ACCESS );
    return ( service.handle != NULL );
}

bool ServiceControlManager::Start( Service &service )
{
    return ( StartServiceW( service.handle, 0, NULL ) != NULL );
}

bool ServiceControlManager::Control( Service &service, ServiceCommand command )
{
    return ( ControlService( service.handle, command, &service.status ) != NULL );
}

bool ServiceControlManager::Close( Service& service )
{
    return ( CloseServiceHandle( service.handle ) != NULL );
}

bool ServiceControlManager::Delete( Service& service )
{
    return ( DeleteService( service.handle ) != NULL );
}

}