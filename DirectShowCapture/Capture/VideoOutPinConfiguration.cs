using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DirectShowLib;
using System.Runtime.InteropServices;

namespace Capture
{
    class VideoOutPinConfiguration
    {
        private int width;
        private int height;
        private long fps;

        private IBaseFilter filter;
        private IPin pin;
        private int format_id;

        public IBaseFilter Filter { get { return filter; } }
        public IPin Pin      { get { return pin; } }
        public int Width { get { return width; } }
        public int Height { get { return height; } }

        public VideoOutPinConfiguration( IBaseFilter filter, IPin pin, int format_id, VideoInfoHeader header )
        {
            this.filter = filter;
            this.pin = pin;
            this.width = header.BmiHeader.Width;
            this.height = header.BmiHeader.Height;
            this.fps = 10000000 / header.AvgTimePerFrame;
            this.format_id = format_id;
        }

        public override string ToString()
        {
            return width + " x " + height + " @ " + fps;
        }

        public bool ApplyConfiguration()
        {
            IAMStreamConfig config = (IAMStreamConfig)pin;
            int caps_count;
            int caps_size;
            config.GetNumberOfCapabilities( out caps_count, out caps_size );
            AMMediaType type = null;
            IntPtr buffer = Marshal.AllocCoTaskMem( caps_size ); 

            config.GetStreamCaps( format_id, out type, buffer );
            int r = config.SetFormat( type );

            Marshal.FreeCoTaskMem( buffer );
            DsUtils.FreeAMMediaType( type );

            return r == 0;
        }
    }
}
