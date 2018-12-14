const routeTimeUrl = './Bus/RouteTime';
const url = new URL(window.location.href);
const params = url.searchParams;
console.log(params.get('RouteUID'));
console.log(params.get('Direction'));
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

// 網頁初次載入時執行
$(function () {
    let paramDirection = parseInt(params.get('Direction'));
    if (paramDirection === 0) { direction.setDirection(direction.go); }
    if (paramDirection === 1) { direction.setDirection(direction.back); }

    getBusEstimateTimeData();

    setInterval(function () {
        if (time >= refreshTime) { getBusEstimateTimeData(); }
        if (time === -1) {
            $('#time').text('資料更新中...');
            return;
        }
        time += 1;
        $('#time').text(`於${time}秒前更新`);
    }, 1000);
});

// 取得該路線的公車預測時間
function getBusEstimateTimeData() {

    // 資料更新中
    time = -1;

    let data = {
        routeUID: params.get('RouteUID'),
        direction: direction.now
    };
    $.ajax({
        type: 'POST',
        url: routeTimeUrl,
        data: data,
        dataType: 'json',
        success: function (data) {
            console.log(data);
            time = 0;
            showBusEstimateTimeData(data);
            setDirectionButtonAttr();
        }
    });
}

// 切換公車行進方向 
$('#btnGo').click(function () {
    direction.setDirection(direction.go);
    setDirectionButtonAttr();
    getBusEstimateTimeData();
});

$('#btnBack').click(function () {
    direction.setDirection(direction.back);
    setDirectionButtonAttr();
    getBusEstimateTimeData();
});

function setDirectionButtonAttr() {
    if (direction.now === 0) {
        $('#btnGo').attr('class', 'col-5 btn btn-primary');
        $('#btnBack').attr('class', 'col-5 btn btn-outline-primary');
    }
    if (direction.now === 1) {
        $('#btnGo').attr('class', 'col-5 btn btn-outline-primary');
        $('#btnBack').attr('class', 'col-5 btn btn-primary');
    }
}

// 手動更新公車預測時間
$('#refresh').click(function () {
    getBusEstimateTimeData();
});

function showBusEstimateTimeData(data) {
    let busRouteInfoList = '';
    for (const item of data.routeTimeData) {
        const time = item.EstimateTime;
        let timeState = '';
        if (time >= 60) {
            timeState = `約${time / 60}分鐘`;
        }
        else if (time > 0) {
            timeState = `約${time}秒`;
        }
        else if (time >= 0) {
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

    $('#busRoute').text(data.routeInfo.RouteName.Zh_tw);
    $('#btnGo').text(`往 ${data.routeInfo.DestinationStopNameZh}`);
    $('#btnBack').text(`往 ${data.routeInfo.DepartureStopNameZh}`);
    console.log(data.routeInfo.RouteName.Zh_tw);
}

