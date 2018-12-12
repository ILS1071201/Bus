using Bus.Models;
using Bus.Security;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Bus.Controllers
{
    public class BusController : Controller
    {
        // POST: Bus/Route
        [HttpPost]
        public ActionResult Route(string query)
        {
            string xdate = DateTime.Now.ToUniversalTime().ToString("r");
            string sAuth = HMAC_SHA1.GetAuth(xdate);

            List<Route> Data = new List<Route>();

            var APIUrl = $"https://ptx.transportdata.tw/MOTC/v2/Bus/Route/City/Taichung?{query}";
            string Result = string.Empty;
            using (HttpClient Client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip }))
            {
                Client.DefaultRequestHeaders.Add("Authorization", sAuth);
                Client.DefaultRequestHeaders.Add("x-date", xdate);
                Result = Client.GetStringAsync(APIUrl).Result;
            }

            Data = JsonConvert.DeserializeObject<List<Route>>(Result);
            return Json(Data);
        }

        // POST: Bus/EstimatedTimeOfArrival
        [HttpPost]
        public ActionResult EstimatedTimeOfArrival(string query)
        {
            string xdate = DateTime.Now.ToUniversalTime().ToString("r");
            string sAuth = HMAC_SHA1.GetAuth(xdate);

            List<EstimatedTimeOfArrival> Data = new List<EstimatedTimeOfArrival>();

            var APIUrl = $"https://ptx.transportdata.tw/MOTC/v2/Bus/EstimatedTimeOfArrival/City/Taichung?{query}";
            string Result = string.Empty;
            using (HttpClient Client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip }))
            {
                Client.DefaultRequestHeaders.Add("Authorization", sAuth);
                Client.DefaultRequestHeaders.Add("x-date", xdate);
                Result = Client.GetStringAsync(APIUrl).Result;
            }

            Data = JsonConvert.DeserializeObject<List<EstimatedTimeOfArrival>>(Result);
            return Json(Data);
        }

        // POST: Bus/Stop
        [HttpPost]
        public ActionResult Stop(string query)
        {
            string xdate = DateTime.Now.ToUniversalTime().ToString("r");
            string sAuth = HMAC_SHA1.GetAuth(xdate);

            List<Stop> Data = new List<Stop>();

            var APIUrl = $"https://ptx.transportdata.tw/MOTC/v2/Bus/Stop/City/Taichung?{query}";
            string Result = string.Empty;
            using (HttpClient Client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip }))
            {
                Client.DefaultRequestHeaders.Add("Authorization", sAuth);
                Client.DefaultRequestHeaders.Add("x-date", xdate);
                Result = Client.GetStringAsync(APIUrl).Result;
            }

            Data = JsonConvert.DeserializeObject<List<Stop>>(Result);
            return Json(Data);
        }
    }
}