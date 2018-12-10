var _index;

function save() {
    console.log("1111");
    console.log("12312312");
    var postData = $("#addForm").serializeArray();
    console.log(postData);
    $.ajax({
        type: "Post",
        url: "/Department/Edit",
        data: postData,
        success: function (data) {
            if (data.result == "Success") {
                submitOk = true;
                parent.layer.msg("数据保存成功");
                parent.initTree();
                parent.layer.close(_index);//需要手动关闭窗口

            } else {
                parent.layer.msg(data.message);
            };
        }
        //});
    });
};
$.validator.setDefaults({
    submitHandler: function (form) {
        save();

    },
});

var submit = function (index) {
    _index = index;
    $("#btnSave").click();

}
