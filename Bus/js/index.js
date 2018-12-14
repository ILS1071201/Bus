const baseUrl = './Bus/Routes';
let busRouteData;

//取回所有的busRouteData(靜態資料)
$(function () {
    $.ajax({
        type: 'POST',
        url: baseUrl,
        dataType: 'json',
        success: function (data) {
            busRouteData = data;
            console.log(busRouteData);
            showBusRoute(busRouteData);
        }
    });
});

//將資料渲染至list
function showBusRoute(data) {
    let busRouteList = '';
    for (const item of data) {
        busRouteList +=
            `<a class="list-group-item list-group-item-action" href="./busRouteInfo.html?RouteUID=${item.RouteUID}">
                <h3>${item.SubRoutes[0].SubRouteName.Zh_tw}</h3>
                <p>${item.SubRoutes[0].Headsign}</p>
            </a>`;
    }
    $('#list').html(busRouteList);
}

//監聽輸入事件
$('#busSearch').keyup(function () {
    const searchValue = $('#busSearch').val();
    // console.log(searchValue);
    searchBusRoute(searchValue);
});

//監聽搜尋按鈕
$('#btnBusSearch').click(function () {
    const searchValue = $('#busSearch').val();
    // console.log(searchValue);
    searchBusRoute(searchValue);
});

//搜尋資料
function searchBusRoute(searchValue) {
    if (!busRouteData) return;
    let tempData = busRouteData.filter(function (item) {
        return item.SubRoutes[0].SubRouteName.Zh_tw.indexOf(searchValue) >= 0
            || item.SubRoutes[0].Headsign.indexOf(searchValue) >= 0;
    });
    // console.log(tempData);
    return showBusRoute(tempData);
}

