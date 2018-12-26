$(function () {

    $.fn.extend({
        CoreSelect2: function (url, resultsFun) {
            this.attr("data-domType","CoreSelect2")
            this.select2({
                ajax: {
                    url: url,//"/Department/SelcetTwoAsync",
                    dataType: 'json',
                    delay: 250,
                    data: function (params) {
                        return {
                            q: params.term, // search term
                            page: params.page || 1,
                            pageSize: 10
                        };
                    },
                    processResults: function (responseData, params) {
                        var pageModel = responseData.data;
                        //console.log(pageModel);
                        var selectList = [];
                        selectList = resultsFun(responseData);
                        //$.each(pageModel.pageData, function () {
                        //});
                        //console.log(selectList);
                        params.page = params.page || 1;
                        return {
                            results: selectList,
                            pagination: {
                                more: (params.page * 10) < pageModel.rowCount
                                //more: true
                            }
                        };
                    },
                    cache: true
                },
                escapeMarkup: function (markup) { return markup; }, // let our custom formatter work
                minimumInputLength: 0,
                templateResult: formatRepoProvince, // omitted for brevity, see the source of this page
                templateSelection: formatRepoProvince, // omitted for brevity, see the source of this page\
                placeholder: "请选择",
                allowClear: true//可以清除选项
            });
        }
    });

})
function formatRepoProvince(repo) {
    return repo.text;
}