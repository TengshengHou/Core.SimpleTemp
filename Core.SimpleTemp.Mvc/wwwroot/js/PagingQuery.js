//组装查询分页条件
var PagingQuery = function (form) {
    var filterObjList = [];
    $form = $(form);
    filterDomList = $form.find("[data-filter=true]");

    UrlFilter();

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


//UrlJumpQuery 
function UrlFilter() {
    //处理通过Url传来的查询条件 
    var filterNames = GetUrlFilterArray();
    if (filterNames) {
        for (var i = 0; i < filterNames.length; i++) {
            $dom = $form.find("[name='" + filterNames[i].key + "']");
            $dom.val(filterNames[i].value).attr("disabled", "disabled");
            if ($dom.attr("data-domType") == "CoreSelect2") {
                $dom.html('<option value="' + filterNames[i].value + '">' + filterNames[i].text + '</option>')
            }
        }
    }
}

//获取Url查询参数//key value
var GetUrlFilterArray = function () {
    //[{ key:"domName" ,value:"123123" }]
    var qfc = GetQueryString("qfc");
    qfc = decodeURI(qfc);
    qfc = eval(qfc)
    return qfc;
}

//创建一个UrlFilter
//strUrl：页面相对路径 ，filterList：[{ key:"domName" ,value:"123123" }]
function CreateUrlFilter(strUrl, filterList) {
    var strQfc = JSON.stringify(filterList);
    strQfc = encodeURI(strQfc);
    var strUrl = strUrl + "?qfc=" + strQfc
    return strUrl;
}

function CreateUrlFilterOne(strUrl, oneKey, oneValue, oneText) {
    var filterList = [];
    var filterOjb = {};
    filterOjb.key = oneKey;
    filterOjb.value = oneValue;
    filterOjb.text = oneText;
    filterList.push(filterOjb);
    var strUrl = CreateUrlFilter(strUrl, filterList)
    return strUrl;
}