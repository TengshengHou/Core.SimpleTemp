var _index;
var saveUrl = "/User/Save";

function save() {
    var postData = $("#addForm").serializeArray();
    $.ajax({
        type: "Post",
        url: saveUrl,
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

$(function () {
    $("#roles").select2({ multiple: true });
    if (userRoleList)
        $("#roles").val(userRoleList).trigger("change");

    $("#SysDepartmentId").CoreSelect2( "/Department/SelcetTwoAsync", function (responseData) {
        var pageModel = responseData.data;
        console.log(pageModel);
        var selectList = [];
        $.each(pageModel.pageData, function () {
            console.log(this);
            selectList.push({ "id": this.id, "text": this.name });
        });
        return selectList;
    });
  
});


