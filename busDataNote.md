# 市區公車之路線資料

    https://ptx.transportdata.tw/MOTC/v2/Bus/Route/City/Taichung

---

## 會使用到的資料
* 路線UID - RouteUID 
* 營運業者 - Operators[].OperatorName.Zh_tw
* 路線名稱 - RouteName.Zh_tw
* 附屬路線名稱 - SubRoutes[].SubRouteName.Zh_tw
* 附屬路線描述 - SubRoutes[].Headsign
* 起站名稱 - DepartureStopNameZh
* 終站名稱 - DestinationStopNameZh
* 路線簡圖網址 - RouteMapImageUrl
* 所屬縣市 - City

---

## 查詢
    查詢 RouteUID == TXG1
    ?$filter RouteUID eq 'TXG1'

---

## 範例
```json
{
    "RouteUID": "TXG1",
    "RouteID": "1",
    "HasSubRoutes": true, 
    "Operators": [
      {
        "OperatorID": "15",
        "OperatorName": {
          "Zh_tw": "中台灣客運",
          "En": "Ct Bus Co., Ltd."
        },
        "OperatorCode": "CenterTaiwanBus",
        "OperatorNo": "0401"
      },
      {
        "OperatorID": "3",
        "OperatorName": {
          "Zh_tw": "統聯客運",
          "En": "United Highway Bus Co., Ltd."
        },
        "OperatorCode": "UnitedHighwayBus",
        "OperatorNo": "1201"
      }
    ],
    "AuthorityID": "007",
    "ProviderID": "007",
    "SubRoutes": [
      {
        "SubRouteUID": "TXG1",
        "SubRouteID": "1",
        "OperatorIDs": [
          "15",
          "3"
        ],
        "SubRouteName": {
          "Zh_tw": "1",
          "En": "1"
        },
        "Headsign": "臺中刑務所演武場-中臺科技大學校區",
        "Direction": 1
      },
      {
        "SubRouteUID": "TXG1",
        "SubRouteID": "1",
        "OperatorIDs": [
          "15",
          "3"
        ],
        "SubRouteName": {
          "Zh_tw": "1",
          "En": "1"
        },
        "Headsign": "臺中刑務所演武場-中臺科技大學校區",
        "Direction": 0
      }
    ],
    "BusRouteType": 11,
    "RouteName": {
      "Zh_tw": "1",
      "En": "1"
    },
    "DepartureStopNameZh": "臺中刑務所演武場",
    "DepartureStopNameEn": "Budokan Martial Arts Hall",
    "DestinationStopNameZh": "中臺科技大學校區",
    "DestinationStopNameEn": "Central Taiwan University",
    "RouteMapImageUrl": "http://www.traffic.taichung.gov.tw/form/index-1.asp?Parser=3,7,161,52,,,2343,102,,,,,1",
    "City": "Taichung",
    "CityCode": "TXG",
    "UpdateTime": "2018-12-06T12:13:08+08:00",
    "VersionID": 322
}
```

---

# 市區公車之預估到站資料

    https://ptx.transportdata.tw/MOTC/v2/Bus/EstimatedTimeOfArrival/City/Taichung

---

## 會使用到的資料
* 站牌UID - StopUID
* 站牌名稱 - StopName.Zh_tw
* 路線UID - RouteUID
* 路線名稱 - RouteName.Zh_tw
* 去返程方向 - Direction ('0: 去程', '1: 返程', '2: 迴圈')
* 到站預估時間 - EstimateTime (秒)
* 路線站牌順序 - StopSequence

---

## 查詢
    查詢TXG1路線，其方向為去程(0)的所有站點資料，並依據StopSequence排序
    ?$filter RouteUID eq 'TXG1' and Direction eq '0'&$orderby StopSequence

    查詢TXG20035站點的所有預估到站資料
    ?$filter StopUID eq 'TXG20035'

    查詢站點名的所有預估到站資料
    ?$filter=StopName/any(s:(contains(s/Zh_tw,'中科管理局(科雅西路)') eq true))

---

```json
{
    "PlateNumb": "050-U8",
    "StopUID": "TXG20035",
    "StopID": "20035",
    "StopName": {
      "Zh_tw": "臺中刑務所演武場",
      "En": "Budokan Martial Arts Hall"
    },
    "RouteUID": "TXG1",
    "RouteID": "1",
    "RouteName": {
      "Zh_tw": "1",
      "En": "1"
    },
    "SubRouteUID": "TXG1",
    "SubRouteID": "1",
    "SubRouteName": {
      "Zh_tw": "1",
      "En": "1"
    },
    "Direction": 1,
    "EstimateTime": 1620,
    "StopSequence": 42,
    "MessageType": 0,
    "NextBusTime": "2018-12-06T15:31:00+08:00",
    "SrcUpdateTime": "2018-12-06T14:42:33+08:00",
    "UpdateTime": "2018-12-06T14:42:57+08:00"
}
```

---

# 市區公車之站牌資料

    https://ptx.transportdata.tw/MOTC/v2/Bus/Stop/City/Taichung

---

## 會使用到的資料
* 站牌UID - StopUID
* 站牌名稱 - StopName.Zh_tw
* 站牌位置 - StopPosition.PositionLat / StopPosition.PositionLon
* 所屬縣市 - City

---

## 查詢

    查詢距離 位置(24.134276, 120.67317) 1000公尺的所有站點
    ?$spatialFilter=nearby(StopPosition, 24.134276, 120.67317, 1000)

---

```json
{
    "StopUID": "TXG20035",
    "StopID": "20035",
    "AuthorityID": "007",
    "StopName": {
      "Zh_tw": "臺中刑務所演武場",
      "En": "Budokan Martial Arts Hall"
    },
    "StopPosition": {
      "PositionLat": 24.134276,
      "PositionLon": 120.67317
    },
    "City": "Taichung",
    "CityCode": "TXG",
    "LocationCityCode": "TXG",
    "UpdateTime": "2018-12-06T12:13:08+08:00",
    "VersionID": 322
}
```
