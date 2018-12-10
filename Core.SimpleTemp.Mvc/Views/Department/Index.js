var $table = $('#table'), $btnScreen = $("#btnScreen"), $btnDele = $("#btnDele");
var guidEmpty = "00000000-0000-0000-0000-000000000000";
var selectedId = guidEmpty;
var ajaxCount = 0;
//新增
function add(type) {
    var parentId = guidEmpty;
    if (type === 1) {
        if (selectedId === guidEmpty) {
            layer.alert("请选择部门。");
            return;
        }
        parentId = selectedId;
    } else {
        SetTreeSelectEmpty()
        parentId = guidEmpty;
    }
    EditWindow('edit?ParentId=' + parentId);

};

function EditWindow(Url) {
    var index = layer.open({
        type: 2,
        area: ['550px', '550px'],
        fixed: false, //不固定
        maxmin: true,
        content: [Url,'no'],//禁止滚动条
        btn: ['保存', '取消'],
        btn1: function (index, layero) {
            $(layero).find("iframe")[0].contentWindow.submit(index);
        }
    });
    //layer.full(index);
}

//编辑
function edit(id) {
    EditWindow('edit?id=' + id);
};


//删除单条数据
function deleteSingle(id) {
    layer.confirm("您确认删除选定的记录吗？", {
        btn: ["确定", "取消"]
    }, function () {
        $.ajax({
            type: "POST",
            url: "/Department/Delete",
            data: { "id": id },
            success: function (data) {
                if (data.result == "Success") {
                    initTree();
                    layer.closeAll();
                }
                else {
                    layer.alert(data.message);
                }
            }
        })
    });
};

function ajaxRequest(params) {
    $.ajax({
        type: "post",
        dataType: "json",
        url: "/Department/GetChildrenByParent?_t=" + new Date().getTime(),
        data: GetQueryData(params.data.offset, params.data.limit),
        success: function (ret) {
            console.log(ret.data);
            params.success({
                total: ret.data.rowCount,
                rows: ret.data.pageData
            });
        }
    })
}

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

window.operateEvents = {

    'click .edit': function (e, value, row, index) {
        edit(row.id);
        //alert('You click like action, row: ' + JSON.stringify(row));
    },
    'click .delete': function (e, value, row, index) {
        deleteSingle(row.id);
        //$table.bootstrapTable('remove', {
        //    field: 'sysRoleId',
        //    values: [row.sysRoleId]
        //});
    }
};

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

function operateFormatter(value, row, index) {
    var v = false ? 'disabled =disabled' : '';
    var editBtnHtml = '<button class="btn btn-info btn-xs  edit" title="编辑" href="javascript:" ' + v + '><i class="glyphicon glyphicon-pencil"></i>  </button> '
    var delBtnHtml = '<button class="btn btn-danger btn-xs delete" title="删除"  href="javascript:" ' + v + '><i class="glyphicon glyphicon-remove"></i>  </button>'
    var detailsBtnHtml = "";
    return [editBtnHtml, delBtnHtml, detailsBtnHtml
    ].join('');
}

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
                    selectedId = node.id;
                    $table.bootstrapTable('refresh');
                };
            });
        }
    });
}

//批量删除
function deleteMulti() {
    var ids = "";
    var list = $table.bootstrapTable('getSelections');
    $.each(list, function () {
        ids += this.id + ","
    })
    ids = ids.substring(0, ids.length - 1);
    if (ids.length == 0) {
        layer.alert("请选择要删除的记录。");
        return;
    };
    //询问框
    layer.confirm("您确认删除选定的记录吗？", {
        btn: ["确定", "取消"]
    }, function () {
        var sendData = { "ids": ids };
        $.ajax({
            type: "Post",
            url: "/Department/DeleteMuti",
            data: sendData,
            success: function (data) {
                if (data.result == "Success") {
                    initTree();
                    layer.closeAll();
                }
                else {
                    layer.alert(data.message);
                }
            }
        });
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
    $("#btnAddRoot").click(function () { add(0); });
    $("#btnAdd").click(function () { add(1); });
    $("#btnDelete").click(function () { deleteMulti(); });
    $btnScreen.click(function () {
        if (selectedId == guidEmpty)
        {
            layer.alert("请选择部门。");
            return;
        }
        $table.bootstrapTable('refresh');
    });
    $("#btnScreenTop").click(function () {
        SetTreeSelectEmpty();
        $table.bootstrapTable('refresh');
    });
    initTree();
});

var SetTreeSelectEmpty = function () {
    //取消当前选择
    $('#treeDiv').jstree('deselect_node', selectedId);
    selectedId = guidEmpty;
}