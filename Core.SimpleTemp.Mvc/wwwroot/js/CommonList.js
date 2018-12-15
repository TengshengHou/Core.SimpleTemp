function baseEditBtnHtml(functionCode) {
    return '<button class="btn btn-info btn-xs  edit ' + functionCode + '" title="编辑" href="javascript:" ><i class="fa fa-edit"></i>  </button> '
}
function baseDetailsBtnHtml(functionCode) {
    return '<button class="btn btn-info btn-xs details ' + functionCode + '" title="详情"  href="javascript:"><i class="glyphicon glyphicon-eye-open"></i>  </button> '
}

function baseDelBtnHtml(functionCode) {
    return '<button class="btn btn-danger btn-xs delete ' + functionCode + '" title="删除"  href="javascript:"><i class="fa fa-trash-o"></i>  </button> '
}

function GetBaseOperateHtml(eidtCode, detailCode, delCode) {
    var eidtBtnHtml = ""
    if (!isEmpty(eidtCode))
        eidtBtnHtml = baseEditBtnHtml(eidtCode);

    var detailBtnHtml = ""
    if (!isEmpty(detailCode))
        detailBtnHtml = baseDetailsBtnHtml(detailCode);

    var delBtnHtml = ""
    if (!isEmpty(delCode))
        delBtnHtml = baseDelBtnHtml(delCode);
    return [eidtBtnHtml, detailBtnHtml, delBtnHtml]
}


//新增/编辑 弹窗
function EditWindow(Url, btn1Text) {
    var btntext = btn1Text ? btn1Text : "保存";
    var index = layer.open({
        type: 2,
        area: ['550px', '550px'],
        fixed: false, //不固定
        maxmin: true,
        content: [Url, 'no'],//禁止滚动条
        btn: [btntext, '取消'],
        btn1: function (index, layero) {
            $(layero).find("iframe")[0].contentWindow.submit(index);
        }
    });
    //layer.full(index);
}


function Delete($table) {

    //批量删除
    this.deleteMulti = function (url, successFunc) {
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
                url: url,
                data: sendData,
                success: function (data) {
                    if (data.result == "Success") {
                        $table.bootstrapTable('refresh');
                        if (successFunc) {
                            successFunc();
                        }
                        layer.closeAll();
                    }
                    else {
                        layer.alert(data.message);
                    }
                }
            });
        });
    };



    //删除单条数据
    this.deleteSingle = function (url, id, successFunc) {
        layer.confirm("您确认删除选定的记录吗？", {
            btn: ["确定", "取消"]
        }, function () {
            $.ajax({
                type: "POST",
                url: url,
                data: { "id": id },
                success: function (data) {
                    if (data.result == "Success") {
                        $table.bootstrapTable('refresh');
                        if (successFunc) {
                            successFunc();
                        }
                        layer.closeAll();
                    }
                    else {
                        layer.alert(data.message);
                    }
                }
            })
        });
    };


}

