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

//function LayerEditPage() {

   

//    this._index = 0;

//    this.addFromSelector = "#addFrom";
//    this.addSaveBtnSelector = "#btnSave";
//    this.saveSuccess = null;
//    this.saveUrl = "";
//    this.save = function () {
        
//        var postData = $(addFromSelector).serializeArray();
//        $.ajax({
//            type: "Post",
//            url: this.saveUrl,
//            data: postData,
//            success: function (data) {
//                if (data.result == "Success") {
//                    parent.layer.msg("数据保存成功");
//                    if (!this.saveSuccess) {
//                        this.saveSuccess(parent);
//                    }
//                    parent.layer.close(_index);//需要手动关闭窗口

//                } else {
//                    parent.layer.msg(data.message);
//                };
//            }
//        });
//    };

//    this.submit = function (index) {
//        _index = index;
//        if (!isDetails()) {
//            $(this.addSaveBtnSelector).click();
//        } else {
//            parent.layer.close(_index);//需要手动关闭窗口
//        }
//    }

//}