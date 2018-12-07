var $table = $('#table')
    , $btnScreen = $("#btnScreen")
    , $btnDele = $("#btnDele");

function ajaxRequest(params) {
    $.ajax({
        type: "post",
        dataType: "json",
        url: "/Department/GetChildrenByParent?parentId=" + selectedId + "&_t=" + new Date().getTime(),
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
    //var filterObj = GetFilterObj($("sysRole_Id")[0])
    //filterObj.Value = $("#Role").val() ? $("#Role").val().toString() : "";
    //filterList.push()
    var filterListJson = JSON.stringify(filterList);
    var pagingQueryData = { "filterConditionList": filterListJson, "offset": offset, limit: limit };
    return pagingQueryData;
}



window.operateEvents = {

    'click .like': function (e, value, row, index) {
        alert('You click like action, row: ' + JSON.stringify(row));
    },
    'click .remove': function (e, value, row, index) {
        console.log(row.sysRoleId);
        console.log(row);
        $table.bootstrapTable('remove', {
            field: 'sysRoleId',
            values: [row.sysRoleId]
        });
    }
};


$btnScreen.click(function () {
    $table.bootstrapTable('refresh');
});


$btnDele.click(function () {
    console.log($table.bootstrapTable('getSelections'));
});

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
    var btnhml = '<button class="btn btn-info btn-xs Role_Edit like" href="javascript:" ' + v + '><i class="fa fa-edit"></i> 编辑 </button>'
    return [btnhml,
        '<a class="like" href="javascript:void(0)" ' + v + '  title="Like">',
        '<i class="fa fa-heart-o"></i>收藏',
        '</a>  ',
        '<a class="remove" href="javascript:void(0)" title="Remove">',
        '<i class="fa fa-trash"></i>',
        '</a>'
    ].join('');
}
