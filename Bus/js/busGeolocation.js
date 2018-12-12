const position = {};
let map;
const busStopUrl = './Bus/Stop';
const estimatedUrl = './Bus/EstimatedTimeOfArrival';
const routeUrl = './Bus/Route';
let nearbyBusStopData;
let busStopEstimateTimeData;
let routeNameData;

// 獲取目前定位、初始化Google Maps，並獲取附近公車站牌
function getBusGeolocation() {
    navigator.geolocation.getCurrentPosition(function (pos) {
        position.latitude = pos.coords.latitude;
        position.longitude = pos.coords.longitude;
        console.log(position);
        initMap();
        getNearbyBusStop();
    });
}

function initMap() {
    map = new google.maps.Map(document.getElementById('map'), {
        center: { lat: position.latitude, lng: position.longitude },
        zoom: 15
    });
    let marker = new google.maps.Marker({
        position: { lat: position.latitude, lng: position.longitude },
        map: map,
        label: '你的位置'
    });
}

function getNearbyBusStop() {
    if (position === null) return;

    let distance = 1000;
    let data = { query: `$spatialFilter=nearby(StopPosition,${position.latitude},${position.longitude},${distance})`};
    $.ajax({
        type: 'POST',
        url: busStopUrl,
        data: data,
        dataType: 'json',
        success: function (data) {
            nearbyBusStopData = data;
            console.log(nearbyBusStopData);
            addBusMarker();
        }
    });
}

function addBusMarker() {
    for (const item of nearbyBusStopData) {
        let marker = new google.maps.Marker({
            position: { lat: item.StopPosition.PositionLat, lng: item.StopPosition.PositionLon },
            icon: '../Content/Images/bus.png',
            map: map
        });

        marker.addListener('click', function () {
            console.log(item.StopName.Zh_tw);
            const lat = item.StopPosition.PositionLat;
            const lng = item.StopPosition.PositionLon;
            let busStopFilter = nearbyBusStopData.filter(function (busStop) {
                return busStop.StopPosition.PositionLat === lat
                    && busStop.StopPosition.PositionLon === lng;
            });
            console.log(busStopFilter);
            getBusStopData(busStopFilter);

        });
    }
}

$('#btnGeolocation').click(function () {
    getBusGeolocation();
});

function getBusStopData(busStops) {
    let queryString = '';
    for (let index = 0; index < busStops.length; index++) {
        if (busStops.length === 1 || index === 0) {
            queryString += `StopUID eq '${busStops[index].StopUID}'`;
        }else{
            queryString += ` or StopUID eq '${busStops[index].StopUID}'`;
        }
    }

    let data = { query: `$filter=${queryString}&$orderby=EstimateTime,RouteID,Direction` };
    $.ajax({
        type: 'POST',
        url: estimatedUrl,
        data: data,
        dataType: 'json',
        success: function (data) {
            console.log(`${estimatedUrl}?$filter=${queryString}&$orderby=EstimateTime,RouteID,Direction`);
            // console.log(data);
            busStopEstimateTimeData = data;
            getRouteNameData(busStopEstimateTimeData);
        }
    });
}

function getRouteNameData(busStopTime) {
    let queryString = '';
    for (let index = 0; index < busStopTime.length; index++) {
        if (busStopTime.length === 1 || index === 0) {
            queryString += `RouteUID eq '${busStopTime[index].RouteUID}'`;
        }else{
            queryString += ` or RouteUID eq '${busStopTime[index].RouteUID}'`;
        }
    }

    let data = { query: `$filter=${queryString}`};
    $.ajax({
        type: 'POST',
        url: `routeUrl`,
        data: data,
        dataType: 'json',
        success: function (data) {
            console.log(data);
            routeNameData = data;
            showBusStopData();
        }
    });
}

function showBusStopData() {
    let busStopList = '';
    let busTimeNullOrBelowZero = '';
    let isNullOrBelowZero = false;
    let temp = '';
    for (const item of busStopEstimateTimeData) {
        const time = item.EstimateTime;
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

        temp =
            `<a class="list-group-item list-group-item-action" href="./busRouteInfo.html?RouteUID=${item.RouteUID}">
                <div class="row">
                    <div class="col-10">
                        <h3>${item.RouteName.Zh_tw}</h3>
                        <p>${item.RouteUID} - ${item.Direction}</p>
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