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