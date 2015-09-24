using System;
using System.IO;
using System.Xml.Serialization;

namespace PMCFiller
{
    class Utils
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
}
