var _index;
function save() {
    var postData = $("#addForm").serializeArray();
    $.ajax({
        type: "Post",
        url: "/Role/Save",
        data: postData,
        success: function (data) {
            if (data.result == "Success") {
                parent.layer.msg("数据保存成功");
                parent.$table.bootstrapTable('refresh');
                parent.layer.close(_index);//需要手动关闭窗口

            } else {
                parent.layer.msg(data.message);
            };
        }
    });
};
$.validator.setDefaults({
    submitHandler: function (form) {
        save();
    },
});

var submit = function (index) {
    _index = index;
    if (!isDetails()) {
        $("#btnSave").click();
    } else {
        parent.layer.close(_index);//需要手动关闭窗口
    }
}

