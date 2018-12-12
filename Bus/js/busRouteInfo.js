const estimatedUrl = './Bus/EstimatedTimeOfArrival';
const routeUrl = './Bus/Route';
const url = new URL(window.location.href);
const params = url.searchParams;
console.log(params.get('RouteUID'));
const direction = {
    go: 0,
    back: 1,
    loop: 2,
    now: 0,
    setDirection(direction) {
        this.now = direction;
    }
};
let time = 0;
const refreshTime = 60;
let busEstimateTimeData;

// 網頁初次載入時執行
$(getRouteData());
$(getBusEstimateTimeData());
$(setInterval(function () {
    time += 1;
    if (time >= refreshTime) { getBusEstimateTimeData(); }
    $('#time').text(`於${time}秒前更新`);
}, 1000));

// 取得該路線的資訊
function getRouteData() {
    let data = { query: `$filter=RouteUID eq '${params.get('RouteUID')}'` };
    $.ajax({
        type: 'POST',
        url: routeUrl,
        data: data,
        dataType: 'json',
        success: function (data) {
            $('#busRoute').text(data[0].RouteName.Zh_tw);
            $('#btnGo').text(`往 ${data[0].DestinationStopNameZh}`);
            $('#btnBack').text(`往 ${data[0].DepartureStopNameZh}`);
            console.log(data[0].RouteName.Zh_tw);
        }
    });
}

// 取得該路線的公車預測時間
function getBusEstimateTimeData() {
    let data = { query: `$filter=RouteUID eq '${params.get('RouteUID')}' and Direction eq '${direction.now}'&$orderby=StopSequence`};
    $.ajax({
        type: 'POST',
        url: estimatedUrl,
        data: data,
        dataType: 'json',
        success: function (data) {
            busEstimateTimeData = data;
            console.log(busEstimateTimeData);
            time = 0;
            showBusEstimateTimeData();
        }
    });
}

// 切換公車行進方向 
$('#btnGo').click(function () {
    direction.setDirection(direction.go);
    getBusEstimateTimeData();
});

$('#btnBack').click(function () {
    direction.setDirection(direction.back);
    getBusEstimateTimeData();
});

// 手動更新公車預測時間
$('#refresh').click(function () {
    getBusEstimateTimeData();
});

function showBusEstimateTimeData() {
    let busRouteInfoList = '';
    for (const item of busEstimateTimeData) {
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
        }

        busRouteInfoList +=
            `<a class="list-group-item list-group-item-action">
                <div class="row">
                    <div class="col-10">
                        <h3>${item.StopName.Zh_tw}</h3>
                    </div>
                    <div class="col">
                        <h4>${timeState}</h4>
                    </div>
                </div>
            </a>`;
    }
    $('#list').html(busRouteInfoList);
}

