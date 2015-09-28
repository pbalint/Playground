#include <Windows.h>
#include <PowrProf.h>
#include <stdio.h>

int main( int argc, const char* argv )
{
    GUID* active_power_scheme;
    HRESULT return_value = PowerGetActiveScheme( NULL, &active_power_scheme );
    if ( return_value != 0 )
    {
        printf( "Error getting active power scheme! (%i)\n", return_value );
        return -1;
    }

    DWORD old_ac_throttle_min;
    DWORD old_dc_throttle_min;
    DWORD old_ac_throttle_max;
    DWORD old_dc_throttle_max;
    PowerReadACValueIndex( NULL, active_power_scheme, &GUID_PROCESSOR_SETTINGS_SUBGROUP, &GUID_PROCESSOR_THROTTLE_MINIMUM, &old_ac_throttle_min );
    PowerReadACValueIndex( NULL, active_power_scheme, &GUID_PROCESSOR_SETTINGS_SUBGROUP, &GUID_PROCESSOR_THROTTLE_MAXIMUM, &old_ac_throttle_max );
    PowerReadDCValueIndex( NULL, active_power_scheme, &GUID_PROCESSOR_SETTINGS_SUBGROUP, &GUID_PROCESSOR_THROTTLE_MINIMUM, &old_dc_throttle_min );
    PowerReadDCValueIndex( NULL, active_power_scheme, &GUID_PROCESSOR_SETTINGS_SUBGROUP, &GUID_PROCESSOR_THROTTLE_MAXIMUM, &old_dc_throttle_max );

    PowerWriteACValueIndex( NULL, active_power_scheme, &GUID_PROCESSOR_SETTINGS_SUBGROUP, &GUID_PROCESSOR_THROTTLE_MINIMUM, 0 );
    PowerWriteACValueIndex( NULL, active_power_scheme, &GUID_PROCESSOR_SETTINGS_SUBGROUP, &GUID_PROCESSOR_THROTTLE_MAXIMUM, 0 );
    PowerWriteDCValueIndex( NULL, active_power_scheme, &GUID_PROCESSOR_SETTINGS_SUBGROUP, &GUID_PROCESSOR_THROTTLE_MINIMUM, 0 );
    PowerWriteDCValueIndex( NULL, active_power_scheme, &GUID_PROCESSOR_SETTINGS_SUBGROUP, &GUID_PROCESSOR_THROTTLE_MAXIMUM, 0 );

    system( "pause" );

    PowerWriteACValueIndex( NULL, active_power_scheme, &GUID_PROCESSOR_SETTINGS_SUBGROUP, &GUID_PROCESSOR_THROTTLE_MINIMUM, old_ac_throttle_min );
    PowerWriteACValueIndex( NULL, active_power_scheme, &GUID_PROCESSOR_SETTINGS_SUBGROUP, &GUID_PROCESSOR_THROTTLE_MAXIMUM, old_ac_throttle_max );
    PowerWriteDCValueIndex( NULL, active_power_scheme, &GUID_PROCESSOR_SETTINGS_SUBGROUP, &GUID_PROCESSOR_THROTTLE_MINIMUM, old_dc_throttle_min );
    PowerWriteDCValueIndex( NULL, active_power_scheme, &GUID_PROCESSOR_SETTINGS_SUBGROUP, &GUID_PROCESSOR_THROTTLE_MAXIMUM, old_dc_throttle_max );

    LocalFree( active_power_scheme );
    return 0;
}