using System;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Win32;

namespace Shared
{
    public struct EventEntry
    {
        public DateTime                 date;
        public SessionSwitchReason      event_type;

        public EventEntry( DateTime date, SessionSwitchReason event_type ) { this.date = date; this.event_type = event_type; }
    }

    public class Utils
    {
        public static void Serialize<T>( T obj, string file_name )
        {
            XmlSerializer serializer = new XmlSerializer( obj.GetType() );
            StreamWriter stream = null;
            try
            {
                stream = new StreamWriter( file_name );
                serializer.Serialize( stream, obj );
            }
            catch ( Exception ) { }
            finally
            {
                if ( stream != null )
                {
                    stream.Close();
                }
            }
        }

        public static void Deserialize<T>( ref T obj, string file_name )
        {
            XmlSerializer serializer = new XmlSerializer( obj.GetType() );
            StreamReader stream = null;
            try
            {
                stream = new StreamReader( file_name );
                obj = (T)serializer.Deserialize( stream );
            }
            catch ( Exception ) { }
            finally
            {
                if ( stream != null )
                {
                    stream.Close();
                }
            }
        }
    }

    public static class Extensions
    {
        // sun...mon = 0...6 -> mon...sun = 0...6
        public static int MondayFirstDoW( this DateTime date )
        {
            return ( date.DayOfWeek == DayOfWeek.Sunday )? 6 : (int)date.DayOfWeek - 1;
        }

        // 1.5 -> 1:30
        public static string ToHourMinuteString( this double hour )
        {
            return ( (int)hour ).ToString() + ":" + ( (int)( ( hour - (int)hour )*60 ) ).ToString( "D2" );
        }
    }

}
