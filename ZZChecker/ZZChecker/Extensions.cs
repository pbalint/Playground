using System;
using System.Windows.Forms;

namespace PMCFiller
{
    static class Extensions
    {
        public static HtmlElement GetElementByName( this HtmlDocument document, string name, int index = 0 )
        {
            try
            {
                return document.All.GetElementsByName( name )[ index ];
            }
            catch ( Exception )
            {
                return null;
            }
        }
    }
}
