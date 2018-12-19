using Bus.Security;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace Bus.Services
{
    public class PtxService
    {
        public List<T> GetJsonData<T>(string apiUrl)
        {
            string xdate = DateTime.Now.ToUniversalTime().ToString("r");
            string sAuth = PtxAuth.GetAuth(xdate);

            string result = string.Empty;
            using (HttpClient client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip }))
            {
                client.DefaultRequestHeaders.Add("Authorization", sAuth);
                client.DefaultRequestHeaders.Add("x-date", xdate);
                result = client.GetStringAsync(apiUrl).Result;
            }

            var data = JsonConvert.DeserializeObject<List<T>>(result);

            return data;
        }
    }
}