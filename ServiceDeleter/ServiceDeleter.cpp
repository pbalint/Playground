#define WIN32_LEAN_AND_MEAN             // Exclude rarely-used stuff from Windows headers

#include <windows.h>

void print_error( HRESULT result_code )
{
    LPWSTR buffer = NULL;
    DWORD length = FormatMessage(
        FORMAT_MESSAGE_ALLOCATE_BUFFER |
        FORMAT_MESSAGE_FROM_SYSTEM |
        FORMAT_MESSAGE_IGNORE_INSERTS,
        NULL,
        result_code,
        MAKELANGID( LANG_NEUTRAL, SUBLANG_DEFAULT ),
        (LPWSTR)&buffer,
        0, NULL );
    HANDLE std_err = GetStdHandle( STD_ERROR_HANDLE );
    WriteConsole( std_err, buffer, length, NULL, NULL );
    LocalFree( buffer );
}

void print_last_error()
{
    DWORD last_error = GetLastError();
    print_error( last_error );
}

bool delete_key(LPCWSTR key)
{
    HRESULT result_code;
    HKEY key_to_delete;
    result_code = RegOpenKeyEx( HKEY_LOCAL_MACHINE, key, NULL, DELETE | KEY_ENUMERATE_SUB_KEYS | KEY_QUERY_VALUE | KEY_SET_VALUE, &key_to_delete );
    if ( result_code != ERROR_SUCCESS )
    {
        print_error( result_code );
        return false;
    }
    result_code = RegDeleteTree( key_to_delete, nullptr );
    if ( result_code != ERROR_SUCCESS )
    {
        print_error( result_code );
        return false;
    }
    result_code = RegDeleteKey( HKEY_LOCAL_MACHINE, key );
    {
        print_error( result_code );
        return false;
    }
    RegCloseKey( key_to_delete );
    return true;
}

int APIENTRY wWinMain( _In_ HINSTANCE hInstance, _In_opt_ HINSTANCE hPrevInstance, _In_ LPWSTR lpCmdLine, _In_ int nShowCmd )
{
    /*if ( !AllocConsole() )
    {
        print_last_error();
    }*/
    delete_key( L"SYSTEM\\CurrentControlSet\\Services\\AsusUpdateCheck" );
    delete_key( L"SYSTEM\\CurrentControlSet\\Services\\UsoSvc" );
    delete_key( L"SYSTEM\\CurrentControlSet\\Services\\wuauserv" );
    delete_key( L"SYSTEM\\CurrentControlSet\\Services\\WaaSMedicSvc" );
    return 0;
}

