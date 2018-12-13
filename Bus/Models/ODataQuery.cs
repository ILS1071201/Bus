using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bus.Models
{
    public class ODataQuery
    {
        public string Select { get; set; }
        public string Filter { get; set; }
        public string Orderby { get; set; }
        public string Top { get; set; }
        public string Skip { get; set; }
        public string SpatialFilter { get; set; }
    }
}