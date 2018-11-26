$(function () {
    $.ajax({
        type: "Get",
        url: "/home/GetReadme?_t=" + new Date().getTime(),    //获取数据的ajax请求地址
        success: function (response) {
            $(".readme").html(response.data);
        }
    });
     
});