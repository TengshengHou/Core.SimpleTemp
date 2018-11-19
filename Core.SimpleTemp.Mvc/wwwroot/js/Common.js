$(function () {
    //$(document).ajaxStart(function () {
    //    if (ajaxCount != 0)
    //        layer.load(1);
    //    ajaxCount++;
    //}).ajaxStop(function () {
    //    layer.closeAll('loading');
    //})

    $(document).ajaxComplete(function (event, request, settings) {
        $(document).ajaxComplete(function (event, request, settings) {
            console.log(request.status);
            if (request.status === 401) {//Unauthorized
                layer.alert(request.status + '登录信息过期请重新登录', 20000,
                    Unauthorized());
            }
            if (request.status === 403) {
                layer.alert(request.status + "无权限进行此操作");
            }
        });

        var Unauthorized = function () {
            var currHref = window.location.href;
            var port = location.port == 80 ? "" : ":" + location.port
            window.location.href = location.protocol + "//" + location.hostname + port + '/account/login?ReturnUrl=' + currHref;
        }

    });
});