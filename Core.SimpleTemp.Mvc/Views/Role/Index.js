var $table = $('#table'), $btnScreen = $("#btnScreen"), $btnDele = $("#btnDele");


var selectedRole = 0;
var ajaxCount = 0;
var delOjb = new Delete($table);

//基础页面需要用到的Url
var defajaxRequestUrl = "/Role/GetAllPageList";
var editUrl = "edit";
var detailsUrl = "details";
var deleteMultiUrl = "/Role/DeleteMuti";
var deleteSingleUrl = "/Role/Delete";









//生成查询条件
function GetQueryData(offset, limit) {
    var filterList = PagingQuery($("#searchForm")[0]);
    //var filterOjb = {};
    //filterOjb.Field = "ParentId";
    //filterOjb.Action = "=";
    //filterOjb.Logic = "and";
    //filterOjb.Value = selectedId;
    //filterOjb.DataType = "guid";
    //filterList.push(filterOjb);
    var filterListJson = JSON.stringify(filterList);
    var pagingQueryData = { "filterConditionList": filterListJson, "offset": offset, limit: limit };
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
        EditWindow('edit?id=' + row.id);
    },
    'click .delete': function (e, value, row, index) {
        delOjb.deleteSingle(deleteSingleUrl, row.id, function () {
            initTree();
        });
    },
    'click .details': function (e, value, row, index) {
        var url = detailsUrl + '?id=' + row.id + '&isDetails=true';
        EditWindow(url, "确定");
    }
};

//行内样式
function operateFormatter(value, row, index) {
    var btnList = GetBaseOperateHtml('Role_Edit', 'Role_details', 'Role_Delete')
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
            field: 'name',
            title: '角色编号',
            align: 'center',
        }, {
            field: 'code',
            title: '角色名称',
            align: 'center',
        }, {
            field: 'remarks',
            title: '角色描述',
            align: 'center',
        }, {
            field: 'operate',
            title: '操作',
            align: 'center',
            events: operateEvents,
            formatter: operateFormatter
        }
    ]
    , onClickRow: function (row, $element) {
        $('.info').removeClass('info');
        $($element).addClass('info');
        selectedRole = row.id;
        loadPermissionByRole(selectedRole);
    }
});










//加载树
function initTree() {
    $.jstree.destroy();
    $.ajax({
        type: "Get",
        url: "/Menu/GetMenuTreeData?_t=" + new Date().getTime(),    //获取数据的ajax请求地址
        success: function (data) {
            $('#treeDiv').jstree({       //创建JsTtree
                'core': {
                    'data': data,        //绑定JsTree数据
                    "multiple": true    //是否多选
                },
                "plugins": ["types", "wholerow", "checkbox",],  //配置信息
                "checkbox": {
                    "keep_selected_style": false
                }
            })
            $("#treeDiv").on("ready.jstree", function (e, data) {   //树创建完成事件
                data.instance.open_all();    //展开所有节点
            });
        }
    });

}


//保存角色权限关联关系
function savePermission() {
    if (selectedRole == 0) {
        layer.alert("请选择角色。");
        return;
    }
    var checkedMenu = $('#treeDiv').jstree().get_checked(true);
    var permissions = [];
    $.each(checkedMenu, function (i, item) {
        permissions.push({ "SysRoleId": selectedRole, "SysMenuId": item.id });
    })
    //获取半选状态ID
    $(".jstree-undetermined").each(function () {
        var id = $(this).parent().parent().attr('id');
        permissions.push({ "SysRoleId": selectedRole, "SysMenuId": id });
    });
    $.ajax({
        type: "POST",
        url: "/Role/SavePermission",
        data: { "roleId": selectedRole, "roleMenus": permissions },
        success: function (data) {
            if (data.result = true) {
                layer.alert("保存成功！");
            }
            else {
                layer.alert("保存失败！");
            }
        }
    })
};
//根据选中角色加载功能权限
function loadPermissionByRole(selectedRole) {
    $.ajax({
        type: "Get",
        url: "/Role/GetMenusByRole?roleId=" + selectedRole + "&_t=" + new Date().getTime(),
        success: function (data) {
            $("#treeDiv").find("li").each(function () {
                $("#treeDiv").jstree("uncheck_node", $(this));
                if (data.indexOf($(this).attr("id")) != -1) {
                    $("#treeDiv").jstree("check_node", $(this));
                }
            })
        }
    });
};



$(function () {

    $(document).ajaxStart(function () {
        if (ajaxCount != 0)
            layer.load(1);
        ajaxCount++;
    }).ajaxStop(function () {
        layer.closeAll('loading');
    })
    initTree();


    $("#btnSavePermission").click(function () { savePermission(); });


    //页面基础按钮
    $("#btnAdd").click(function () {
        EditWindow(editUrl);
    });
    $("#btnDelete").click(function () {
        delOjb.deleteMulti(deleteMultiUrl, function () {
            initTree();
        });
    });
    $btnScreen.click(function () {
        $table.bootstrapTable('refresh');
    });
});
