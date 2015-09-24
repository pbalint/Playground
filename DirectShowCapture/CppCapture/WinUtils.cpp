#include <stdio.h>
#include "WinUtils.h"


void PrintGuid( GUID guid )
{
    printf( "{%08lX-%04hX-%04hX-%02hhX%02hhX-%02hhX%02hhX%02hhX%02hhX%02hhX%02hhX}",
        guid.Data1, guid.Data2, guid.Data3,
        guid.Data4[ 0 ], guid.Data4[ 1 ], guid.Data4[ 2 ], guid.Data4[ 3 ],
        guid.Data4[ 4 ], guid.Data4[ 5 ], guid.Data4[ 6 ], guid.Data4[ 7 ] );
}

void DeleteMediaType( _AMMediaType *pmt )
{
    if ( pmt->cbFormat != 0 )
    {
        CoTaskMemFree( (PVOID)pmt->pbFormat );
        pmt->cbFormat = 0;
        pmt->pbFormat = nullptr;
    }
    if ( pmt->pUnk != nullptr )
    {
        pmt->pUnk->Release();
        pmt->pUnk = nullptr;
    }
    if ( pmt != nullptr )
    {
        CoTaskMemFree( pmt );
    }
}

