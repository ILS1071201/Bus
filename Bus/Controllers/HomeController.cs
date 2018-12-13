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
            return Redirect("./index.html");
        }

        /// <summary>
        /// HMAC 取得API
        /// </summary>
        /// <returns></returns>
        //private ActionResult CallAPIByHMAC()
        //{
        //    //申請的APPID
        //    //（FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF 為 Guest 帳號，以IP作為API呼叫限制，請替換為註冊的APPID & APPKey）
        //    string APPID = "FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF";
        //    //申請的APPKey
        //    string APPKey = "FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF";

        //    //取得當下UTC時間
        //    string xdate = DateTime.Now.ToUniversalTime().ToString("r");
        //    string SignDate = "x-date: " + xdate;
        //    //取得加密簽章
        //    string Signature = HMAC_SHA1.Signature(SignDate, APPKey);
        //    string sAuth = "hmac username=\"" + APPID + "\", algorithm=\"hmac-sha1\", headers=\"x-date\", signature=\"" + Signature + "\"";

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
        //    ViewBag.ticket = Signature;
        //    return View(Data);

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