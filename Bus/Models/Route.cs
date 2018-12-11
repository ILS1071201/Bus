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
        public Operator[] Operators { get; set; }
        public string AuthorityID { get; set; }
        public string ProviderID { get; set; }
        public Subroute[] SubRoutes { get; set; }
        public int BusRouteType { get; set; }
        public Routename RouteName { get; set; }
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

    public class Routename
    {
        public string Zh_tw { get; set; }
        public string En { get; set; }
    }

    public class Operator
    {
        public string OperatorID { get; set; }
        public Operatorname OperatorName { get; set; }
        public string OperatorCode { get; set; }
        public string OperatorNo { get; set; }
    }

    public class Operatorname
    {
        public string Zh_tw { get; set; }
        public string En { get; set; }
    }

    public class Subroute
    {
        public string SubRouteUID { get; set; }
        public string SubRouteID { get; set; }
        public string[] OperatorIDs { get; set; }
        public Subroutename SubRouteName { get; set; }
        public string Headsign { get; set; }
        public int Direction { get; set; }
    }

    public class Subroutename
    {
        public string Zh_tw { get; set; }
        public string En { get; set; }
    }
}