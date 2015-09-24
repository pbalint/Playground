using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Capture
{
    class OleHelper
    {
        [DllImport( "oleaut32.dll", CharSet = CharSet.Auto )]
        internal static extern int OleCreatePropertyFrame(
            IntPtr hwndOwner,
            uint x,
            uint y,
            [MarshalAs( UnmanagedType.LPWStr )] string caption,
            uint objectCount,
            [MarshalAs( UnmanagedType.LPArray, ArraySubType = UnmanagedType.IUnknown )] object[] lplpUnk,
            int cPages,
            IntPtr pageClsID,
            uint lcid,
            uint dwReserved,
            IntPtr lpvReserved );
    }
}
