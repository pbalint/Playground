using System;
using System.Globalization;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace HTTPStressTest
{
    public class Config : IXmlSerializable
    {
        private List< string >  request_templates      = new List< string >();
        private Random          random                 = new Random( DateTime.Now.Millisecond );
        
        public string           URL                     { get; set; }
        public int              SearchDateBeginInterval { get; set; }
        public int              SearchDateEndInterval   { get; set; }
        public bool             ShowResponse            { get; set; }

        public void UseDefaults()
        {
            ShowResponse            = true;
            URL                     = "http://agence.voyages-sncf.com";
            SearchDateBeginInterval = 90;
            SearchDateEndInterval   = 21;
            ShowResponse            = true;
            request_templates.Clear();
            request_templates.Add( "/pub/agent.dll?qscr=htwv&from=m&stat=1&khst=1&locn=MARSEILLE%2CFRANCE&ploc=MARSEILLE%2CFRANCE&loid=&ofmt=1&date1=<start_date>&date2=<end_date>&crom=1&cadu1=<adult_count>&rdct=1|htfv|420|7|2|2|1036|0|28|0|0|0|0|||09/05|1|178710|0|1|1|0|0|0|0|0|0" );
            request_templates.Add( "/pub/agent.dll?qscr=htwv&from=m&stat=1&khst=1&locn=PARIS%2CFRANCE&ploc=PARIS%2CFRANCE&loid=&ofmt=1&date1=<start_date>&date2=<end_date>&crom=1&cadu1=<adult_count>&rdct=1" );
            request_templates.Add( "/pub/agent.dll?qscr=htwv&from=m&stat=1&khst=1&locn=PARIS%2CFRANCE&ploc=PARIS%2CFRANCE&loid=&ofmt=1&date1=<start_date>&date2=<end_date>&crom=1&cadu1=<adult_count>&rdct=1|htfv|420|7|2|2|1036|0|28|0|0|0|0|||01/01|0|179898|0|1|2|0|0|0|0|0|0" );
            request_templates.Add( "/daily/home/process_super.aspx?Product=car&searchType=PLACE&carArr=NANTES&carDate1=<start_date>&carDate2=<end_date>&carTime1=11AM&carTime2=10AM&crse=basket_cola_voit_expedia&rfrr=basket_cola_voit_expedia&rel=xsellVoiture" );
            request_templates.Add( "/pub/agent.dll?qscr=cars&dagv=1&subm=1&fdrp=1&styp=2&locn=NANTES&date1=<start_date>&date2=<end_date>&vend=&kind=&time1=660&time2=600&ttyp=2&acop=2&rdct=1&rfrr=basket_cola_voit_expedia&crse=basket_cola_voit_expedia" );
        }

        public String Request
        {
            get
            {
                DateTime search_begin = DateTime.Now.AddDays( random.Next( 1, SearchDateBeginInterval ) );
                DateTime search_end = search_begin.AddDays( random.Next( 1, SearchDateEndInterval ) );
                int request = random.Next( 0, request_templates.Count );
                return request_templates[ request ].
                        Replace( "<start_date>", search_begin.ToString( "dd/MM/yyyy", new CultureInfo( 1033 ) ) ).
                        Replace( "<end_date>", search_end.ToString( "dd/MM/yyyy", new CultureInfo( 1033 ) ) ).
                        Replace( "<adult_count>", random.Next( 1, 6 ).ToString() );
            }
        }
    
        public System.Xml.Schema.XmlSchema  GetSchema() { return null; }

        public void ReadXml(System.Xml.XmlReader reader)
        {
            reader.Read();
            reader.Read();
            request_templates.Clear();
            while ( reader.Name.Equals( "Request" ) )
            {
                request_templates.Add( reader.ReadElementString() );
            }
            reader.Read();

            URL = reader.ReadElementString();
            SearchDateBeginInterval = Convert.ToInt32( reader.ReadElementString() );
            SearchDateEndInterval = Convert.ToInt32( reader.ReadElementString() );
            ShowResponse = Convert.ToBoolean( reader.ReadElementString() );
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
 	        writer.WriteStartElement( "RequestTemplates" );
            foreach ( string request in request_templates )
            {
                writer.WriteStartElement( "Request" );
                writer.WriteCData( request );
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
            writer.WriteElementString( "URL", URL );
            writer.WriteElementString( "SearchDateBeginInterval", SearchDateBeginInterval.ToString() );
            writer.WriteElementString( "SearchDateEndInterval", SearchDateEndInterval.ToString() );
            writer.WriteElementString( "ShowResponse", ShowResponse.ToString() );
        }
    }
}
