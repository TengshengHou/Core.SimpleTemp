$(function () {
    //禁用表单
    new EditTool().detailsDomDisabled('addForm');
})

function EditTool() {
    //详情页面禁用指定表单（ID）下控件为disabled
    this.detailsDomDisabled = function (FormId) {
        if (isDetails()) {
            var formElem = document.getElementById(FormId);
            if (formElem) {
                $(formElem).find('input,textarea,select').attr("disabled", "disabled");
            }
        }
    }

}
