using System;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32.SafeHandles;

namespace Extplorer
{
    class Utils
    {
        [DllImport( "Kernel32.dll", EntryPoint="CreateFile", SetLastError=true, CharSet=CharSet.Auto )]
        public static extern
        SafeFileHandle CreateFile( string filename,
                                   UInt32 accessmask,
                                   UInt32 sharemode,
                                   IntPtr securityattributes,
                                   UInt32 creationdisposition,
                                   UInt32 flagsandattributes,
                                   IntPtr tempfile );

        public const UInt32 GENERIC_READ             = 0x80000000;
        public const UInt32 GENERIC_WRITE            = 0x40000000;
        public const UInt32 GENERIC_EXECUTE          = 0x20000000;
        public const UInt32 GENERIC_ALL              = 0x10000000;

        public const UInt32 FILE_SHARE_READ          = 0x00000001;
        public const UInt32 FILE_SHARE_WRITE         = 0x00000002;

        public const UInt32 CREATE_NEW               = 1;
        public const UInt32 CREATE_ALWAYS            = 2;
        public const UInt32 OPEN_EXISTING            = 3;
        public const UInt32 OPEN_ALWAYS              = 4;

        public const UInt32 FILE_ATTRIBUTE_NORMAL    = 0x00000080;

        public static
        SafeFileHandle OpenRawDevice( string name )
        {
            // Read-only access
            return CreateFile( name,
                                GENERIC_READ,
                                FILE_SHARE_READ,
                                IntPtr.Zero,
                                OPEN_EXISTING,
                                FILE_ATTRIBUTE_NORMAL,
                                IntPtr.Zero );
            // Read-Write access
            /*return CreateFile(  name,
                                GENERIC_ALL,
                                GENERIC_READ,
                                FILE_SHARE_READ | FILE_SHARE_WRITE,
                                IntPtr.Zero,
                                OPEN_EXISTING,
                                FILE_ATTRIBUTE_NORMAL,
                                IntPtr.Zero );*/
        }

        static public void LoadLE32( out UInt32 value, Byte[] buffer, UInt32 offset )
        {
            value = ( UInt32 ) ( buffer[offset] + ( buffer[offset+1] << 8 ) + ( buffer[offset+2] << 16 ) + ( buffer[offset+3] << 24 ) );
        }

        static public void LoadLE32To64( out Int64 value, Byte[] buffer, UInt32 offset )
        {
            value = ( Int64 ) ( buffer[offset] + ( buffer[offset+1] << 8 ) + ( buffer[offset+2] << 16 ) + ( buffer[offset+3] << 24 ) );
        }

        static public void LoadLE16( out UInt32 value, Byte[] buffer, UInt32 offset )
        {
            value = ( UInt32 ) ( buffer[offset] + ( buffer[offset+1] << 8 ) );
        }

        static public void LoadLE16To64( out Int64 value, Byte[] buffer, UInt32 offset )
        {
            value = (UInt32)( buffer[ offset ] + ( buffer[ offset+1 ] << 8 ) );
        }

        static public void LoadString( out string str, int length, Byte[] buffer, UInt32 offset )
        {
            str = Encoding.ASCII.GetString( buffer, (int)offset, length ); ;
        }

        static public void LoadByteArrayFromUInt32Array( Byte[] byte_array, UInt32[] int_array, int int_count )
        {
            for ( int i=0; i<int_count; i++ )
            {
                byte_array[ i*4 ]   = (Byte)( ( int_array[ i ] & 0x000000ff ) );
                byte_array[ i*4+1 ] = (Byte)( ( int_array[ i ] & 0x0000ff00 ) >> 8 );
                byte_array[ i*4+2 ] = (Byte)( ( int_array[ i ] & 0x00ff0000 ) >> 16 );
                byte_array[ i*4+3 ] = (Byte)( ( int_array[ i ] & 0xff000000 ) >> 24 );
            }
        }
    }
}
