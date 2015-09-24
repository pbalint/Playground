using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DirectShowLib;

namespace Capture
{
    class Device
    {
        private String name;
        private String description;
        private IBaseFilter filter;

        public IBaseFilter Filter
        {
            get{ return filter; }
        }

        public Device( IBaseFilter filter, string name, string description )
        {
            this.filter         = filter;
            this.name           = name;
            this.description    = description;
        }

        public override String ToString()
        {
            if ( description.Length > 0 ) return name + " (" + description + ")";
            else return name;
        }

    }
}
