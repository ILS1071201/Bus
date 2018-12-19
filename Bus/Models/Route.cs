using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bus.Models
{
    public class Route
    {
        public string RouteUID { get; set; }
        public string RouteID { get; set; }
        public bool HasSubRoutes { get; set; }
        public List<Operator> Operators { get; set; }
        public string AuthorityID { get; set; }
        public string ProviderID { get; set; }
        public List<Subroute> SubRoutes { get; set; }
        public int BusRouteType { get; set; }
        public RouteName RouteName { get; set; }
        public string DepartureStopNameZh { get; set; }
        public string DepartureStopNameEn { get; set; }
        public string DestinationStopNameZh { get; set; }
        public string DestinationStopNameEn { get; set; }
        public string RouteMapImageUrl { get; set; }
        public string City { get; set; }
        public string CityCode { get; set; }
        public DateTime UpdateTime { get; set; }
        public int VersionID { get; set; }
    }
}