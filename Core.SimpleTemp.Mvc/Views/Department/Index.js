var $table = $('#table'), $btnScreen = $("#btnScreen"), $btnDele = $("#btnDele");
var selectedId = "00000000-0000-0000-0000-000000000000";
var ajaxCount = 0;


//新增
function add(type) {
    if (type === 1) {
        if (selectedId === "00000000-0000-0000-0000-000000000000") {
            layer.alert("请选择部门。");
            return;
        }
        $("#ParentId").val(selectedId);
    }
    else {
        $("#ParentId").val("00000000-0000-0000-0000-000000000000");
    }
    $("#Id").val("00000000-0000-0000-0000-000000000000");
    $("#Code").val("");
    $("#Name").val("");
    $("#Manager").val("");
    $("#ContactNumber").val("");
    $("#Remarks").val("");
    $("#Title").text("新增顶级");
    //弹出新增窗体
    $("#addRootModal").modal("show");
};
//编辑
function edit(id) {
    $.ajax({
        type: "Get",
        url: "/Department/Get?id=" + id + "&_t=" + new Date().getTime(),
        success: function (data) {
            $("#Id").val(data.id);
            $("#ParentId").val(data.parentId);
            $("#addForm [name='name']").val(data.name);
            $("#Code").val(data.code);
            $("#Manager").val(data.manager);
            $("#ContactNumber").val(data.contactNumber);
            $("#Remarks").val(data.remarks);
            $("#Title").text("编辑功能")
            $("#addRootModal").modal("show");
        }
    })
};
//保存
function save() {
    //var postData = { "dto": { "Id": $("#Id").val(), "ParentId": $("#ParentId").val(), "Name": $("#Name").val(), "Code": $("#Code").val(), "Manager": $("#Manager").val(), "ContactNumber": $("#ContactNumber").val(), "Remarks": $("#Remarks").val() } };
    var postData = $("#addForm").serializeArray();
    $.ajax({
        type: "Post",
        url: "/Department/Edit",
        data: postData,
        success: function (data) {
            debugger
            if (data.result == "Success") {
                initTree();
                $("#addRootModal").modal("hide");
            } else {
                layer.tips(data.message, "#btnSave");
            };
        }
    });
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
    if ($("#isTopQuery").get(0).checked) {
        selectedId = "00000000-0000-0000-0000-000000000000";
    } else {
        if ($('#treeDiv').jstree()) {
            console.log($('#treeDiv').jstree());

        }
    }
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
        alert('You click like action, row: ' + JSON.stringify(row));
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
            title: 'Item Operate',
            align: 'center',
            events: operateEvents,
            formatter: operateFormatter
        }
    ]
});

function operateFormatter(value, row, index) {
    var v = false ? 'disabled =disabled' : '';
    var editBtnHtml = '<button class="btn btn-info btn-xs Role_Edit edit" href="javascript:" ' + v + '><i class="fa fa-edit"></i> 编辑 </button>'
    var delBtnHtml = '<button class="btn btn-danger btn-xs Menu_Delete" href="javascript:" ' + v + '><i class="fa fa-edit"></i> 删除 </button>'
    return [editBtnHtml, delBtnHtml
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
                var inst = data.instance;
                var obj = inst.get_node(e.target.firstChild.firstChild.lastChild);
                inst.select_node(obj);
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
    $("#btnSave").click(function () { save(); });
    $("#btnDelete").click(function () { deleteMulti(); });
    $("#btnLoadRoot").click(function () {
        selectedId = "00000000-0000-0000-0000-000000000000";
        $table.bootstrapTable('refresh');
    });
    $btnScreen.click(function () { $table.bootstrapTable('refresh'); });
    //$btnDele.click(function () {
    //    deleteMulti
    //});

    $("#checkAll").click(function () { checkAll(this) });
    initTree();
});