var AuthorizeList = [];
$(function () {

    //Ajax 授权/认证失败后处理
    $(document).ajaxComplete(function (event, request, settings) {
        $(document).ajaxComplete(function (event, request, settings) {
            console.log(request.status);
            if (request.status === 401) {//Unauthorized
                layer.alert(request.status + '登录信息过期请重新登录', 20000,
                    Unauthorized());
            }
            if (request.status === 403) {
                layer.msg(request.status + "无权限进行此操作");
            }

            var Unauthorized = function () {
                var currHref = window.location.href;
                var port = location.port == 80 ? "" : ":" + location.port
                window.location.href = location.protocol + "//" + location.hostname + port + '/account/login?ReturnUrl=' + currHref;
            }
            Authorize();
        });

        Authorize();

    })


});

//未授权按钮处理
var Authorize = function () {
    try {
        jQuery.each(AuthorizeList, function (key, val) {
            var classSelector = "." + key;

            if ($(classSelector)) {
                //$(classSelector).hide(!val);
                if (!val) {
                    $(classSelector).attr("disabled", "disabled");
                }

            }
        });
    } catch (err) { }
};


//判断是否是详情页面 

function isDetails() {
    return window.frames.location.search.indexOf("isDetails") > 0;
}



//判断字符是否为空的方法
function isEmpty(obj) {
    if (typeof obj == "undefined" || obj == null || obj == "") {
        return true;
    } else {
        return false;
    }
}