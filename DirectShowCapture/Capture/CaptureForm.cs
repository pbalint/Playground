using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DirectShowLib;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.InteropServices;

namespace Capture
{
    public partial class CaptureForm : Form, ISampleGrabberCB
    {
        private IGraphBuilder graph_builder;
        private IMediaControl media_control;
        private IMediaEventEx events;
        private ISampleGrabber grabber;

        private VideoOutPinConfiguration active_config = null;

        public CaptureForm()
        {
            InitializeComponent();

            graph_builder = (IGraphBuilder)new FilterGraph();
            media_control = (IMediaControl)graph_builder;
            events = (IMediaEventEx)graph_builder;
            grabber = (ISampleGrabber)new SampleGrabber();
 
            AMMediaType media_type = new AMMediaType();
            media_type.majorType = MediaType.Video;
            media_type.subType = MediaSubType.RGB24;
            grabber.SetMediaType( media_type );
            grabber.SetCallback( this, 1 );

            cbDevices.Items.AddRange( GetDevices( FilterCategory.VideoInputDevice ) );
        }

        private void Play()
        {
            int r;
            String selected_device_name = cbDevices.SelectedItem.ToString();
            if ( selected_device_name.Length <= 0 ) return;

            IBaseFilter input = ( (Device)cbDevices.SelectedItem ).Filter;
            VideoOutPinConfiguration pin_config = (VideoOutPinConfiguration)cbFormat.SelectedItem;
            if ( input == null || pin_config == null || !pin_config.ApplyConfiguration() ) return;

            r = graph_builder.AddFilter( input, cbDevices.SelectedText );
            r = graph_builder.AddFilter( (IBaseFilter)grabber, "grabber" );
            IPin input_out = pin_config.Pin;
            IPin grabber_in = GetFirstPin( (IBaseFilter)grabber, PinDirection.Input );
            if ( input_out != null && grabber_in != null )
            {
                r = graph_builder.Connect( input_out, grabber_in );
            }
            r = media_control.Run();

            active_config = pin_config;
            bPlay.Text = "Stop";
        }

        private void Stop()
        {
            media_control.Stop();
            if ( active_config != null )
            {
                graph_builder.RemoveFilter( active_config.Filter );
            }
            graph_builder.RemoveFilter( (IBaseFilter)grabber ); 
            active_config = null;

            bPlay.Text = "Play";
        }

        private void bPlay_Click( object sender, EventArgs e )
        {
            if ( bPlay.Text == "Play" )
            {
                Play();
            }
            else
            {
                Stop();
            }
        }

        private IPin GetFirstPin( IBaseFilter filter, PinDirection direction )
        {
            IEnumPins iterator;
            IPin[] pins = new IPin[1];
            filter.EnumPins( out iterator );
            while ( iterator.Next( 1, pins, IntPtr.Zero ) == 0 )
            {
                PinDirection pin_direction;
                pins[0].QueryDirection( out pin_direction );
                if ( pin_direction == direction )
                {
                    return pins[0];
                }
            }
            return null;
        }

        private VideoOutPinConfiguration[] GetVideoOutPins( IBaseFilter filter )
        {
            List<VideoOutPinConfiguration> video_out_pins = new List<VideoOutPinConfiguration>();

            IEnumPins iterator;
            IPin[] pins = new IPin[1];
            filter.EnumPins( out iterator );
            while ( iterator.Next( 1, pins, IntPtr.Zero ) == 0 )
            {
                PinDirection pin_direction;
                pins[0].QueryDirection( out pin_direction );
                if ( pin_direction == PinDirection.Output )
                {
                    int caps_count;
                    int caps_size;
                    IAMStreamConfig config = (IAMStreamConfig)pins[0];
                    config.GetNumberOfCapabilities( out caps_count, out caps_size );
                    AMMediaType type = null;
                    IntPtr buffer = Marshal.AllocCoTaskMem( caps_size ); 
                    for ( int i = 0; i < caps_count; i++ )
                    {
                        config.GetStreamCaps( i, out type, buffer );
                        VideoInfoHeader header = (VideoInfoHeader)Marshal.PtrToStructure( type.formatPtr, typeof( VideoInfoHeader ) );
                        if ( header.BmiHeader.Width  > 0 )
                        {
                            video_out_pins.Add( new VideoOutPinConfiguration( filter, pins[0], i, header ) );
                        }
                    }
                    Marshal.FreeCoTaskMem( buffer );
                    DsUtils.FreeAMMediaType( type );
                }
            }
            return video_out_pins.ToArray();
        }

        private Device[] GetDevices( Guid category )
        {
            List< Device > devices = new List< Device >();
            ICreateDevEnum enumerator = (ICreateDevEnum)new CreateDevEnum();
            IEnumMoniker iterator;
            enumerator.CreateClassEnumerator( category, out iterator, 0 );
            IMoniker[] moniker = new IMoniker[1];
            while ( iterator.Next( 1, moniker, IntPtr.Zero ) == 0 )
            {
                String friendly_name = "";
                String description = "";

                object obj;
                moniker[0].BindToStorage( null, null, typeof( IPropertyBag ).GUID, out obj );
                IPropertyBag bag =(IPropertyBag)obj;
                obj = null;
                bag.Read( "FriendlyName", out obj, null );
                if ( obj != null )
                {
                    friendly_name = obj.ToString();
                }
                bag.Read( "Description", out obj, null );
                if ( obj != null )
                {
                    description = obj.ToString();
                }
                moniker[0].BindToObject( null, null, typeof( IBaseFilter ).GUID, out obj );
                if ( obj != null )
                {
                    devices.Add( new Device( (IBaseFilter)obj, friendly_name, description ) );
                }
            }
            return devices.ToArray();
        }

        private void cbDevices_SelectedValueChanged( object sender, EventArgs e )
        {
            cbFormat.Items.Clear();
            IBaseFilter input = ((Device)cbDevices.SelectedItem).Filter;
            cbFormat.Items.AddRange( GetVideoOutPins( input ) );
        }

        public int SampleCB( double SampleTime, IMediaSample pSample )
        {
            return 0;
        }

        public int BufferCB( double SampleTime, IntPtr pBuffer, int BufferLen )
        {
            if ( active_config == null ) return 0;

            int width = active_config.Width;
            int height = active_config.Height;
            int scan_line = width * 3;
            Image bmp = new Bitmap( width, height, -scan_line, 
                                    System.Drawing.Imaging.PixelFormat.Format24bppRgb, 
                                    new IntPtr( pBuffer.ToInt64() + (height - 1) * scan_line) );
            if ( InvokeRequired )
            {
                BeginInvoke( new MethodInvoker( delegate { BufferCB( SampleTime, pBuffer, BufferLen ); } ) );
            }
            else
            {
                pbImage.Image = bmp;
            }

            return 0;
        }

        private void Form1_FormClosing( object sender, FormClosingEventArgs e )
        {
            Stop();
        }

        private void pbImage_DoubleClick( object sender, EventArgs e )
        {
            if ( active_config == null ) return;

            ISpecifyPropertyPages property_pages = (ISpecifyPropertyPages)active_config.Filter;
            DsCAUUID pages_uuid;
            property_pages.GetPages( out pages_uuid );
            OleHelper.OleCreatePropertyFrame(
                IntPtr.Zero,
                0, 0,
                "",
                1, new object[] { active_config.Filter },
                pages_uuid.cElems, pages_uuid.pElems,
                0, 0, IntPtr.Zero );
        }
    }
}
