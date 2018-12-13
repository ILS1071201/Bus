using Bus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace Bus.Services
{
    public class BusService
    {
        public List<Route> GetRoutes(string query)
        {
            string apiUrl = $"https://ptx.transportdata.tw/MOTC/v2/Bus/Route/City/Taichung?{query}";

            var ptxService = new PtxService();
            var data = ptxService.GetJsonData<Route>(apiUrl);

            return data;
        }

        public List<EstimatedTimeOfArrival> GetEstimatedTimeOfArrival(string query)
        {
            string apiUrl = $"https://ptx.transportdata.tw/MOTC/v2/Bus/EstimatedTimeOfArrival/City/Taichung?{query}";

            var ptxService = new PtxService();
            var data = ptxService.GetJsonData<EstimatedTimeOfArrival>(apiUrl);

            return data;
        }

        public List<Stop> GetStops(string query)
        {
            string apiUrl = $"https://ptx.transportdata.tw/MOTC/v2/Bus/Stop/City/Taichung?{query}";

            var ptxService = new PtxService();
            var data = ptxService.GetJsonData<Stop>(apiUrl);

            return data;
        }

    }
}