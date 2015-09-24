#include "TrayIcon.h"

namespace BLib
{

TrayIcon::TrayIcon( Window* parent, const Icon& icon )
{
    memset( &data, 0, sizeof( data ) );
    data.cbSize         = sizeof( data );
    data.uID            = parent->GetControls().GetSize() + 1;
    data.uFlags         = NIF_ICON;
    data.hIcon          = icon;
    data.uVersion       = NOTIFYICON_VERSION_4;
    data.hWnd           = parent->GetHandle();

    if ( Shell_NotifyIcon( NIM_ADD, &data ) && parent != NULL )
    {
        parent->GetControls().AddLast( this );
    }
}

TrayIcon::~TrayIcon()
{
    Shell_NotifyIcon( NIM_DELETE, &data );
}

}