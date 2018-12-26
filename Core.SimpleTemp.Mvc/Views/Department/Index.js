var $table = $('#table'), $btnScreen = $("#btnScreen"), $btnDele = $("#btnDele");
var selectedId = guidEmpty;
var ajaxCount = 0;
var delOjb = new Delete($table);



//生成查询条件
function GetQueryData(offset, limit) {
    var filterList = PagingQuery($("#searchForm")[0]);
    var filterOjb = {};
    filterOjb.Field = "ParentId";
    filterOjb.Action = "=";
    filterOjb.Logic = "and";
    filterOjb.Value = selectedId;
    filterOjb.DataType = "guid";
    filterList.push(filterOjb);
    var filterListJson = JSON.stringify(filterList);
    var pagingQueryData = { "filterConditionList": filterListJson, "offset": offset, limit: limit };
    return pagingQueryData;
}
//获取数据
function ajaxRequest(params) {
    $.ajax({
        type: "post",
        dataType: "json",
        url: "/Department/GetChildrenByParent?_t=" + new Date().getTime(),
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
        delOjb.deleteSingle("/Department/Delete", row.id, function () {
            initTree();
        });
    },
    'click .details': function (e, value, row, index) {
        var url = 'details?id=' + row.id + '&isDetails=true';
        EditWindow(url, "确定");
    }
};

//行内样式
function operateFormatter(value, row, index) {
    var btnList = GetBaseOperateHtml('Department_Edit', 'Department_details', 'Department_Delete')
    return btnList.join('');
}
function nameFormatter(value, row, index) {
    var strUrl = CreateUrlFilterOne("/user/index", "SysDepartmentId", row.id, row.name)
    return "<a  title='点击查看【" + row.name + "】下所有用户' href=" + strUrl + "> " + row.name + "<a>";
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
            title: '部门名称',
            align: 'center',
            formatter: nameFormatter
        }, {
            field: 'code',
            title: '部门编号',
            align: 'center',
        }, {
            field: 'manager',
            title: '负责人',
            align: 'center',
        }, {
            field: 'contactNumber',
            title: '联系电话',
            align: 'center',
        }, {
            field: 'manager',
            title: '部门描述',
            align: 'remarks',
        }, {
            field: 'operate',
            title: '操作',
            align: 'center',
            events: operateEvents,
            formatter: operateFormatter
        }
    ]
});

//加载功能树
function initTree() {
    $.jstree.destroy();
    $.ajax({
        type: "Get",
        url: "/Department/GetTreeData?_t=" + new Date().getTime(),    //获取数据的ajax请求地址
        success: function (data) {
            $('#treeDiv').jstree({       //创建JsTtree
                'core': {
                    'data': data,        //绑定JsTree数据
                    "multiple": false    //是否多选
                },
                "plugins": ["types", "wholerow"]  //配置信息
            })
            $("#treeDiv").on("ready.jstree", function (e, data) {   //树创建完成事件
                data.instance.open_all();    //展开所有节点
                //默认选中根节点
                if (selectedId == guidEmpty) {
                    //var inst = data.instance;
                    //var obj = inst.get_node(e.target.firstChild.firstChild.lastChild);
                    //inst.select_node(obj);
                } else {

                    $('#treeDiv').jstree('select_node', selectedId);
                }
            });
            $("#treeDiv").on('changed.jstree', function (e, data) {   //选中节点改变事件

                var node = data.instance.get_node(data.selected[0]);  //获取选中的节点
                if (node) {
                    if (selectedId == node.id && data.event)
                        SetTreeSelectEmpty();
                    else {
                        selectedId = node.id;

                    }
                    $table.bootstrapTable('refresh');
                };
            });
        }
    });
}

$(function () {
    $(document).ajaxStart(function () {
        if (ajaxCount != 0)
            layer.load(1);
        ajaxCount++;
    }).ajaxStop(function () {
        layer.closeAll('loading');
    })

    $("#btnAdd").click(function () {
        EditWindow('edit?ParentId=' + selectedId);
    });
    $("#btnDelete").click(function () {
        delOjb.deleteMulti("/Department/DeleteMuti", function () {
            initTree();
        });
    });
    $btnScreen.click(function () {
        $table.bootstrapTable('refresh');
    });

    initTree();
});

//取消当前选中，并设置选中节点为GuidEmpty
var SetTreeSelectEmpty = function () {
    //取消当前选择
    $('#treeDiv').jstree('deselect_node', selectedId);
    selectedId = guidEmpty;
}