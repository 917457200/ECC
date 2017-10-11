//上传类型定义
if (typeof UploadTypeEnum == "undefined") {
    var UploadTypeEnum = {};
    UploadTypeEnum.image = { "type": "image", "exts": "*.jpg;*.jpge;*.gif;*.png;", "buttonText": "选择图片" };
    UploadTypeEnum.video = { "type": "video", "exts": "*.mp4;*.rmvb;*.avi;*.wmv;*.flv;", "buttonText": "选择视频" };
    UploadTypeEnum.office = { "type": "office", "exts": "*.xls;", "buttonText": "选择文件" };
}

//容量换算单位
function bytesToSize(bytes) {
    if (bytes == 0) return '0 B';
    var k = 1024, // or 1024
        sizes = ['B', 'KB', 'MB', 'GB', 'TB', 'PB', 'EB', 'ZB', 'YB'],
        i = Math.floor(Math.log(bytes) / Math.log(k));

    return (bytes / Math.pow(k, i)).toPrecision(3) + ' ' + sizes[i];
}
$(function () {
    //上传按钮id，上传队列按钮id,上传文件类型，上传成功回调
    $.Upload = function Upload(control, queueID, uploadtype,multi, successCallback) {
        $("#" + control).uploadify({
          
            'onDialogClose': function (queueData, uploadQueue) {
                $('#uploadImageQueue .uploadify-queue-item').hide();
            },
            'onUploadStart': function (file) {
                $('#'+file.id).show();
            },
            //'onUploadProgress': function (file, bytesUploaded, bytesTotal, totalBytesUploaded, totalBytesTotal) {
            //    $('#' + file.id + ' .uploadify-progress').css('background-color', 'red');
            //    $('#'+file.id+' .uploadify-progress-bar').html("正在上传" + file.name + "--" + bytesUploaded / bytesTotal);
            //},
            //开启调试
            'debug': false,
            //是否自动上传
            'auto': true,
            'buttonText': uploadtype.buttonText,
            //flash
            'swf': "js/Uploadify/uploadify.swf",
            //文件选择后的容器ID
            'queueID': queueID,
            //'buttonImage':'js/Uploadify/SelectUpload.png',
            'uploader': 'AjaxUpload.ashx?uploadtype=' + uploadtype.type,
            'width': '80',
            'height': '25',
            'multi': multi,
            'fileTypeDesc': '支持的格式：',
            'fileTypeExts': uploadtype.exts,
            'fileSizeLimit': '1024MB',
            "removeCompleted": false,
            'removeTimeout': 1,
            "progressData": "percentage",
            //"simUploadLimit": 5,
            //返回一个错误，选择文件的时候触发
            'onSelectError': function (file, errorCode, errorMsg) {
                switch (errorCode) {
                    case -100:
                        alert("上传的文件数量已经超出系统限制的" + $('#' + control).uploadify('settings', 'queueSizeLimit') + "个文件！");
                        break;
                    case -110:
                        alert("文件 [" + file.name + "] 大小超出系统限制的" + $('#' + control).uploadify('settings', 'fileSizeLimit') + "大小！");
                        break;
                    case -120:
                        alert("文件 [" + file.name + "] 大小异常！");
                        break;
                    case -130:
                        alert("文件 [" + file.name + "] 类型不正确！");
                        break;
                }
            },
            //检测FLASH失败调用
            'onFallback': function () {
                alert("您未安装FLASH控件，无法上传！请安装FLASH控件后再试。");
            },
            //上传到服务器，服务器返回相应信息到data里
            'onUploadSuccess': function (file, res, response) {
             
                var result = eval("(" + res + ")");
                if (result.status == "0")
                {
                  
                    if (typeof (successCallback) === "function") {
                        $('#' + file.id).hide();
                     
                        successCallback({ "name": result.name, "url": result.url, "url1": result.url1, "thumnailUrl": result.thumnailUrl, "size": bytesToSize(file.size), "width": result.width, "height": result.height, "control": control });
                    }
                }
            }
        });
    };

 
});

