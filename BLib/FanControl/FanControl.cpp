#include "BLib\ServiceControlManager.h"
#include "BLib\Winbond83627DHG.h"
#include "BLib\NVApi\nvapi.h"
#include <stdio.h>

using namespace BLib;

#if 0
int WINAPI WinMain( HINSTANCE, HINSTANCE, LPSTR, int )
{
    bool                    gpu_present = false;
    NvPhysicalGpuHandle     gpu_handle;
    NV_GPU_THERMAL_SETTINGS settings;
    NvAPI_Status            status;
    if ( NvAPI_Initialize() == NVAPI_OK )
    {
        NvU32 gpu_count;
        NvPhysicalGpuHandle gpu_handles[64];
        NvAPI_EnumPhysicalGPUs( gpu_handles, &gpu_count );
        if ( gpu_count > 0 )
        {
            settings.version = NV_GPU_THERMAL_SETTINGS_VER;
            gpu_present = true;
            gpu_handle = gpu_handles[0];
        }
    }

    
	/*ServiceControlManager sccm( "" );
    Service bdrv( "BDrv", "C:\\BDrv.sys" );
    if ( !sccm.Open( bdrv ) )
    {
        sccm.Create( bdrv );
    }

    sccm.Control( bdrv, sccm.Refresh );
    if ( bdrv.GetStatus().dwCurrentState != SERVICE_RUNNING )
    {
        sccm.Start( bdrv );
    }*/

    Winbond83627DHG lpcio;
    if ( lpcio.ChipPresent() )
    {
        int cpu_temp = 0;
        int gpu_temp = 0;
        int fan_percent = 100;
        const int max_cpu_temp = 45;
        const int max_gpu_temp = 65;

        while ( true )
        {
            cpu_temp = ( cpu_temp + lpcio.GetTemperature( lpcio.TEMP_CPU ) ) / 2;
            if ( gpu_present )
            {
                status = NvAPI_GPU_GetThermalSettings( gpu_handle, 0, &settings );
                gpu_temp = ( gpu_temp + settings.sensor[0].currentTemp ) / 2;
                if ( status != NVAPI_OK )
                {
                    MessageBox( NULL, L"Error: Couldn't get GPU temperature!", L"Error", MB_OK );
                }
            }
            if ( cpu_temp > max_cpu_temp || gpu_temp > max_gpu_temp )
            {
                if ( fan_percent < 100 )
                {
                    fan_percent = 100;
                    lpcio.SetFan( lpcio.FAN_CPU0, fan_percent );
                }
            }
            else if ( cpu_temp < max_cpu_temp * 0.9 && gpu_temp < max_gpu_temp * 0.9 )
            {
                if ( fan_percent == 100 )
                {
                    fan_percent = 50;
                    lpcio.SetFan( lpcio.FAN_CPU0, fan_percent );
                }
            }
            Sleep( 1000 );
        }
    }
    else
    {
        MessageBox( NULL, L"Error: Couldn't find BDrv!", L"Error", MB_OK );
    }

    if ( gpu_present )
    {
        NvAPI_Unload();
    }

    /*sccm.Control( bdrv, ServiceControlManager::Stop );
    sccm.Delete( bdrv );
    sccm.Close( bdrv );*/

	return 0;
}
#endif

int WINAPI WinMain( HINSTANCE, HINSTANCE, LPSTR, int )
{
    Winbond83627DHG lpcio;
    if ( lpcio.ChipPresent() )
    {
        int cpu_temp = 0;
        unsigned char fan_percent = 100;
        const int max_cpu_temp = 45;
        while ( true )
        {
            cpu_temp = ( cpu_temp + lpcio.GetTemperature( lpcio.TEMP_CPU ) ) / 2;
            if ( cpu_temp > max_cpu_temp )
            {
                if ( fan_percent < 100 )
                {
                    fan_percent = 100;
                    lpcio.SetFan( lpcio.FAN_CPU0, fan_percent );
                }
            }
            else if ( cpu_temp < max_cpu_temp * 0.9 )
            {
                if ( fan_percent == 100 )
                {
                    fan_percent = 0;
                    lpcio.SetFan( lpcio.FAN_CPU0, fan_percent );
                }
            }
            Sleep( 1000 );
        }
    }
    else
    {
        MessageBox( NULL, L"Error: Couldn't detect LPCIO!", L"Error", MB_OK );
    }

	return 0;
}
