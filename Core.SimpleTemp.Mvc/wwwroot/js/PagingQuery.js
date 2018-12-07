//组装查询分页条件
var PagingQuery = function (form) {
    var filterObjList = [];
    $form = $(form);
    filterDomList = $form.find("[data-filter=true]");
    $.each(filterDomList, function () {
        var filterOjb = {};
        filterOjb = GetFilterObj(this);
        filterObjList.push(filterOjb);
    });

    return filterObjList;
}


var GetFilterObj = function (dom) {
    $dom = $(dom);
    var filterOjb = {};
    filterOjb.Field = $dom.attr("name") ? $dom.attr("name") : "";
    filterOjb.Action = $dom.attr("data-action") ? $dom.attr("data-action") : "=";
    filterOjb.Logic = $dom.attr("data-logic") ? $dom.attr("data-logic") : "and";
    filterOjb.Value = $dom.val() ? $dom.val() : "";
    filterOjb.DataType = $dom.attr("data-datatype") ? $dom.attr("data-datatype") : "string";
    return filterOjb;
}