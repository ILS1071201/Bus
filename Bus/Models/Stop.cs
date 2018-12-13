using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bus.Models
{
    public class Stop
    {
        public string StopUID { get; set; }
        public string StopID { get; set; }
        public string AuthorityID { get; set; }
        public StopName StopName { get; set; }
        public StopPosition StopPosition { get; set; }
        public string City { get; set; }
        public string CityCode { get; set; }
        public string LocationCityCode { get; set; }
        public DateTime UpdateTime { get; set; }
        public int VersionID { get; set; }
    }
}