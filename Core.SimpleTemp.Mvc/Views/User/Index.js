﻿var $table = $('#table'), $btnScreen = $("#btnScreen"), $btnDele = $("#btnDele");
var ajaxCount = 0;
var delOjb = new Delete($table);

var defajaxRequestUrl = "/User/GetList";
var editUrl = "edit";
var detailsUrl = "details";
var deleteMultiUrl = "/User/DeleteMuti";
var deleteSingleUrl = "/User/Delete";


//生成查询条件
function GetQueryData(offset, limit) {
    var filterList = PagingQuery($("#searchForm")[0]);
    var filterListJson = JSON.stringify(filterList);
    var pagingQueryData = { "filterConditionList": filterListJson, "offset": offset, limit: limit };
    if ($("#roles").val())
        pagingQueryData.roles = $("#roles").val();
    return pagingQueryData;
}
//获取数据
function ajaxRequest(params) {
    $.ajax({
        type: "post",
        dataType: "json",
        url: defajaxRequestUrl + "?_t=" + new Date().getTime(),
        data: GetQueryData(params.data.offset, params.data.limit),
        success: function (ret) {
            params.success({
                total: ret.data.rowCount,
                rows: ret.data.pageData
            });
        }
    })
}

//Table行内事件
window.operateEvents = {
    'click .edit': function (e, value, row, index) {
        EditWindow(editUrl + '?id=' + row.id);
    },
    'click .delete': function (e, value, row, index) {
        delOjb.deleteSingle(deleteSingleUrl, row.id, function () {
            $table.bootstrapTable('refresh');
        });
    },
    'click .details': function (e, value, row, index) {
        var url = detailsUrl + '?id=' + row.id + '&isDetails=true';
        EditWindow(url, "确定");
    }
};

//操作列行内样式
function operateFormatter(value, row, index) {
    var btnList = GetBaseOperateHtml('UserController_Edit', 'UserController_details', 'UserController_Delete')
    return btnList.join('');
}

//设置Table
$table.bootstrapTable({

    columns: [
        {
            field: 'state',
            align: 'center',
            checkbox: true
        },
        {
            field: 'loginName',
            title: '账号',
            align: 'center',
        }, {
            field: 'name',
            title: '名字',
            align: 'center',
        }, {
            field: 'operate',
            title: '操作',
            align: 'center',
            events: operateEvents,
            formatter: operateFormatter
        }
    ]
});


$(function () {
    $("#SysDepartmentId").CoreSelect2("/Department/SelcetTwoAsync", function (responseData) {
        var pageModel = responseData.data;
        console.log(pageModel);
        var selectList = [];
        $.each(pageModel.pageData, function () {
            console.log(this);
            selectList.push({ "id": this.id, "text": this.name });
        });
        return selectList;
    });


    $("#roles").select2({
        multiple: false,
        placeholder: "请选择",
        allowClear: true//可以清除选项
    });

    $(document).ajaxStart(function () {
        if (ajaxCount != 0)
            layer.load(1);
        ajaxCount++;
    }).ajaxStop(function () {
        layer.closeAll('loading');
        layer.closeAll('loading');
    })

    $("#btnAdd").click(function () {
        EditWindow(editUrl);
    });
    $("#btnDelete").click(function () {
        delOjb.deleteMulti(deleteMultiUrl, function () {
            $table.bootstrapTable('refresh');
        });
    });
    $btnScreen.click(function () {
        $table.bootstrapTable('refresh');
    });

});

