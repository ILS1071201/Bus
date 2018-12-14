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
        private PtxService _ptxService = new PtxService();

        public List<Route> GetRoutes(string query)
        {
            string apiUrl = $"https://ptx.transportdata.tw/MOTC/v2/Bus/Route/City/Taichung?{query}";

            var data = _ptxService.GetJsonData<Route>(apiUrl);

            return data;
        }

        public List<EstimatedTimeOfArrival> GetEstimatedTimeOfArrival(string query)
        {
            string apiUrl = $"https://ptx.transportdata.tw/MOTC/v2/Bus/EstimatedTimeOfArrival/City/Taichung?{query}";

            var data = _ptxService.GetJsonData<EstimatedTimeOfArrival>(apiUrl);

            return data;
        }

        public List<Stop> GetStops(string query)
        {
            string apiUrl = $"https://ptx.transportdata.tw/MOTC/v2/Bus/Stop/City/Taichung?{query}";

            var data = _ptxService.GetJsonData<Stop>(apiUrl);

            return data;
        }

    }
}