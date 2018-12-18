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
    //$("#SysDepartmentId").val(SysDepartmentId).trigger("change");
    //远程筛选
    $("#SysDepartmentId").select2({
        ajax: {
            url: "/Department/SelcetTwoAsync",
            dataType: 'json',
            delay: 250,
            data: function (params) {
                return {
                    q: params.term, // search term
                    page: params.page || 1,
                    pageSize: 10
                };
            },
            processResults: function (responseData, params) {
                var pageModel = responseData.data;
                console.log(pageModel);
                var selectList = [];
                $.each(pageModel.pageData, function () {
                    console.log(this);
                    selectList.push({ "id": this.id, "text": this.name });
                });
                console.log(selectList);
                params.page = params.page || 1;
                return {
                    results: selectList,
                    pagination: {
                        more: (params.page * 10) < pageModel.rowCount
                        //more: true
                    }
                };
            },
            cache: true
        },
        escapeMarkup: function (markup) { return markup; }, // let our custom formatter work
        minimumInputLength: 0,
        templateResult: formatRepoProvince, // omitted for brevity, see the source of this page
        templateSelection: formatRepoProvince, // omitted for brevity, see the source of this page
        allowClear: true,//可以清除选项
    });


});


function formatRepoProvince(repo) {
    return repo.text;
}