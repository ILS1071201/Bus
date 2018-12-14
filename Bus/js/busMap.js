// Google Map 起始位置
const position = { lat: 24.215, lng: 120.614 };
let map;
let infowindow;
const nearbyStopsUrl = './Bus/NearbyStops';
const stopsTimeUrl = './Bus/StopsTime';
let nearbyStopData;
let selectedBusStopsData;
let time = 0;
const refreshTime = 60;

// 獲取目前定位、初始化Google Maps，並獲取附近公車站牌
// function getBusGeolocation() {
//     navigator.geolocation.getCurrentPosition(function (pos) {
//         position.latitude = pos.coords.latitude;
//         position.longitude = pos.coords.longitude;
//         console.log(position);
//         initMap();
//         getNearbyBusStop();
//     });
// }

// 初始Google Map並增加手動定位監聽事件
function initMap() {
    map = new google.maps.Map(document.getElementById('map'), {
        center: { lat: position.lat, lng: position.lng },
        zoom: 15
    });
    map.addListener('click', function (e) {
        getUserGeolocation(e.latLng, map);
    });

    infowindow = new google.maps.InfoWindow();

}

// 手動定位事件觸發，移除手動定位事件，並將該定位置中，再透過該定位取得附近站牌
function getUserGeolocation(latLng, map) {
    let marker = new google.maps.Marker({
        position: latLng,
        map: map,
        label: '你的位置'
    });
    map.panTo(latLng);
    google.maps.event.clearListeners(map, 'click');
    position.lat = latLng.lat();
    position.lng = latLng.lng();
    getNearbyBusStop();
}

function getNearbyBusStop() {
    let data = {
        lat: position.lat,
        lng: position.lng,
        distance: 1000
    };

    $.ajax({
        type: 'POST',
        url: nearbyStopsUrl,
        data: data,
        dataType: 'json',
        success: function (data) {
            nearbyStopData = data;
            console.log(nearbyStopData);
            addBusMarker();
        }
    });
}

// 在Google Map上添加bus marker
function addBusMarker() {
    for (const item of nearbyStopData) {
        let marker = new google.maps.Marker({
            position: { lat: item.StopPosition.PositionLat, lng: item.StopPosition.PositionLon },
            icon: '../Content/Images/bus.png',
            title: item.StopName.Zh_tw,
            map: map
        });

        marker.addListener('click', function () {
            console.log(item.StopName.Zh_tw);
            $('#stopName').text(item.StopName.Zh_tw);
            const lat = item.StopPosition.PositionLat;
            const lng = item.StopPosition.PositionLon;

            map.zoom = 18;
            map.panTo({ lat: lat, lng: lng });

            let busStopFilter = nearbyStopData.filter(function (busStop) {
                return busStop.StopPosition.PositionLat === lat
                    && busStop.StopPosition.PositionLon === lng;
            });

            infowindow.close();
            infowindow.setContent(item.StopName.Zh_tw);
            infowindow.open(map, marker);

            console.log(busStopFilter);
            selectedBusStopsData = busStopFilter;

            getStopsTimeData();
        });
    }
}

$('#btnGeolocation').click(function () {
    initMap();
});

$(setInterval(function () {
    if (selectedBusStopsData) {
        time += 1;
        if (time >= refreshTime) { getStopsTimeData(); }
        $('#time').text(`於${time}秒前更新`);
    }
}, 1000));

// 手動更新公車預測時間
$('#refresh').click(function () {
    getStopsTimeData();
});

function getStopsTimeData() {
    let busStops = [];
    for (const busStopData of selectedBusStopsData) {
        busStops.push(busStopData.StopUID);
    }
    console.log(busStops);

    let data = {
        stopUIDs: busStops
    };
    console.log(data);

    $.ajax({
        type: 'POST',
        url: stopsTimeUrl,
        data: data,
        dataType: 'json',
        success: function (data) {
            console.log(data);
            time = 0;
            $('#timeBlock').show();
            showBusStopData(data);
        }
    });
}

function showBusStopData(data) {
    let busStopList = '';
    let busTimeNullOrBelowZero = '';
    let isNullOrBelowZero = false;
    let temp = '';
    for (const item of data) {
        const time = item.stopTimeData.EstimateTime;
        let timeState = '';
        if (time >= 60) {
            timeState = `約${time / 60}分鐘`;
        }
        else if (time > 0) {
            timeState = `約${time}秒`;
        }
        else if (time === 0) {
            timeState = `公車進站中`;
        }

        else {
            timeState = `未發車`;
            isNullOrBelowZero = true;
        }

        let routeDirectionName = '';
        if (item.stopTimeData.Direction === 0) {
            routeDirectionName = item.routeData.DestinationStopNameZh;
        }
        if (item.stopTimeData.Direction === 1) {
            routeDirectionName = item.routeData.DepartureStopNameZh;
        }

        temp =
            `<a class="list-group-item list-group-item-action" href="./busRouteInfo.html?RouteUID=${item.stopTimeData.RouteUID}&Direction=${item.stopTimeData.Direction}">
                <div class="row">
                    <div class="col-10">
                        <h3>${item.stopTimeData.RouteName.Zh_tw}</h3>
                        <p>往 ${routeDirectionName}</p>
                    </div>
                    <div class="col">
                        <h4>${timeState}</h4>
                    </div>
                </div>
            </a>`;

        if (isNullOrBelowZero) {
            busTimeNullOrBelowZero += temp;
            isNullOrBelowZero = false;
            continue;
        }
        busStopList += temp;
    }
    $('#list').html(busStopList + busTimeNullOrBelowZero);
}