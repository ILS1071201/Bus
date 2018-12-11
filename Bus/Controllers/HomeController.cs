using Bus.Security;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Bus.Models;

namespace Bus.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string query)
        {
            //string xdate = DateTime.Now.ToUniversalTime().ToString("r");
            //string sAuth = HMAC_SHA1.GetAuth(xdate);

            //List<Route> Data = new List<Route>();
            ////欲呼叫之API網址(此範例為台鐵車站資料)

            //var APIUrl = $"https://ptx.transportdata.tw/MOTC/v2/Bus/Route/City/Taichung?{query}";
            //string Result = string.Empty;
            //using (HttpClient Client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip }))
            //{
            //    Client.DefaultRequestHeaders.Add("Authorization", sAuth);
            //    Client.DefaultRequestHeaders.Add("x-date", xdate);
            //    Result = Client.GetStringAsync(APIUrl).Result;
            //}

            //Data = JsonConvert.DeserializeObject<List<Route>>(Result);
            //return Json(Data);
            //return CallAPIByHMAC();
            //return View();
            return Redirect("./index.html");
        }
       
        //private ActionResult CallAPIByHMAC()
        //{
        //    string xdate = DateTime.Now.ToUniversalTime().ToString("r");
        //    string sAuth = HMAC_SHA1.GetAuth(xdate);

        //    List<RailStation> Data = new List<RailStation>();
        //    //欲呼叫之API網址(此範例為台鐵車站資料)

        //    var APIUrl = "http://ptx.transportdata.tw/MOTC/v2/Rail/TRA/Station?$top=10&$format=JSON";
        //    string Result = string.Empty;

        //    using (HttpClient Client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip }))
        //    {
        //        Client.DefaultRequestHeaders.Add("Authorization", sAuth);
        //        Client.DefaultRequestHeaders.Add("x-date", xdate);
        //        Result = Client.GetStringAsync(APIUrl).Result;
        //    }

        //    Data = JsonConvert.DeserializeObject<List<RailStation>>(Result);
        //    return Json(Data, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}