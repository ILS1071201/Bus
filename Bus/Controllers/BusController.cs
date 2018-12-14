using Bus.Models;
using Bus.Services;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Bus.Controllers
{
    public class BusController : Controller
    {
        private BusService _busService = new BusService();

        // POST: Bus/Routes
        /// <summary>
        /// 獲取所有公車路線
        /// </summary>
        /// <returns>Json格式的公車路線資料</returns>
        [HttpPost]
        public ActionResult Routes()
        {
            var routesData = _busService.GetRoutes("");

            return Json(routesData);
        }

        // POST: Bus/RouteTime
        /// <summary>
        /// 獲取RouteUID所對應的路線方向之公車預計到達時間
        /// </summary>
        /// <param name="routeUID">路線UID</param>
        /// <param name="direction">方向(['0: 去程', '1: 返程', '2: 迴圈', '255: 未知'])</param>
        /// <returns>Json格式的該路線資訊與公車預計到達時間資料</returns>
        [HttpPost]
        public ActionResult RouteTime(string routeUID, int direction)
        {
            string routeQuery = $"$filter=RouteUID eq '{routeUID}'";
            var routeInfo = _busService.GetRoutes(routeQuery);

            string routeTimeQuery = $"$filter=RouteUID eq '{routeUID}' and Direction eq '{direction}'&$orderby=StopSequence";
            var routeTimeData = _busService.GetEstimatedTimeOfArrival(routeTimeQuery);

            var routeInfoAndRouteTimeData = new
            {
                routeInfo = routeInfo[0],
                routeTimeData
            };

            return Json(routeInfoAndRouteTimeData);
        }

        // POST: Bus/NearbyStops
        /// <summary>
        /// 取得附近站牌資訊
        /// </summary>
        /// <param name="lat">緯度</param>
        /// <param name="lng">經度</param>
        /// <param name="distance">搜尋範圍(公尺)</param>
        /// <returns>Json格式的附近站牌資訊</returns>
        [HttpPost]
        public ActionResult NearbyStops(float lat, float lng, int distance)
        {
            string nearbyStopsQuery = $"$spatialFilter=nearby(StopPosition,{lat},{lng},{distance})";
            var nearbyStopsData = _busService.GetStops(nearbyStopsQuery);

            return Json(nearbyStopsData);
        }

        // POST: Bus/StopsTime
        /// <summary>
        /// 獲取stopUIDs的所有對應站牌之公車預計到達時間與站牌路線資訊
        /// </summary>
        /// <param name="stopUIDs">欲獲取的stopUIDs Array</param>
        /// <returns>Json格式的所有站牌之公車預計到達時間與站牌路線資訊</returns>
        public ActionResult StopsTime(List<string> stopUIDs)
        {
            // 獲取公車預計到達時間
            var stopUIDsQuery = new List<string>();
            foreach (var stopUID in stopUIDs)
            {
                stopUIDsQuery.Add($"StopUID eq '{stopUID}'");
            }
            string stopsTimeQuery = $"$filter={string.Join(" or ", stopUIDsQuery)}&$orderby=EstimateTime,RouteID,Direction";
            var stopsTimeData = _busService.GetEstimatedTimeOfArrival(stopsTimeQuery);

            // 獲取路線資訊
            var routeUIDs = stopsTimeData.Select(s => s.RouteUID).Distinct().ToList();
            var routeUIDsQuery = new List<string>();
            foreach (var routeUID in routeUIDs)
            {
                routeUIDsQuery.Add($"RouteUID eq '{routeUID}'");
            }
            var routesQuery = $"$filter={string.Join(" or ", routeUIDsQuery)}";
            var routesData = _busService.GetRoutes(routesQuery);

            // join到達時間與路線資訊
            var stopTimeAndRouteData = from t in stopsTimeData
                                       join r in routesData on t.RouteUID equals r.RouteUID
                                       select new
                                       {
                                           stopTimeData = t,
                                           routeData = r
                                       };

            return Json(stopTimeAndRouteData);
        }
    }
}