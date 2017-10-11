<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm5.aspx.cs" Inherits="WebAPI.WebForm5" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title> 
       <link href="js/Uploadify/uploadify.css" rel="stylesheet" />
    <script src="js/jquery-1.7.2.min.js"></script>
   
    <script src="js/Uploadify/swfobject.js"></script>
     <script src="js/Uploadify/jquery.uploadify.min.js"></script>

   <script type="text/javascript">
       $(function () {
           $("#file_upload").uploadify({
               //开启调试
               'debug': false,
               //是否自动上传
               'auto': false,
               'buttonText': '选择文件',
               //flash
               'swf': "js/Uploadify/uploadify.swf",
               //文件选择后的容器ID
               'queueID': 'uploadfileQueue',
               'uploader': 'AjaxUpload.ashx',
               'width': '75',
               'height': '24',
               'multi': true,
               'fileTypeDesc': '支持的格式：',
               'fileTypeExts': '*.jpg;*.jpge;*.gif;*.png;*.mp4;*.rmvb;',
               'fileSizeLimit': '100MB',
               "removeCompleted":false,
               'removeTimeout': 10,
               "progressData": "percentage",
               "simUploadLimit": 5,
               //返回一个错误，选择文件的时候触发
               'onSelectError': function (file, errorCode, errorMsg) {
                   switch (errorCode) {
                       case -100:
                           alert("上传的文件数量已经超出系统限制的" + $('#file_upload').uploadify('settings', 'queueSizeLimit') + "个文件！");
                           break;
                       case -110:
                           alert("文件 [" + file.name + "] 大小超出系统限制的" + $('#file_upload').uploadify('settings', 'fileSizeLimit') + "大小！");
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
                   alert("您未安装FLASH控件，无法上传图片！请安装FLASH控件后再试。");
               },
               //上传到服务器，服务器返回相应信息到data里
               'onUploadSuccess': function (file, data, response) {
                 var str=  eval("(" + data + ")");
                  // alert(data);
                   $("#uploadFile").append(str.url);
               }
           });
       });

       function doUplaod() {
           $('#file_upload').uploadify('upload', '*');
       }

       function closeLoad() {
           $('#file_upload').uploadify('cancel', '*');
       }


   </script>
</head>
<body>
    <table width="704" border="0" align="center" cellpadding="0" cellspacing="0" id="__01">
        <tr>
            <td align="center" valign="middle">
                <div id="uploadfileQueue" style="padding: 3px;">
                </div>
                <div id="file_upload">
                </div>
            </td>
        </tr>
        <tr>
            <td height="50" align="center" valign="middle">
                <img alt="" src="images/BeginUpload.gif" width="77" height="23" onclick="doUplaod()" style="cursor: hand" />
                <img alt="" src="images/CancelUpload.gif" width="77" height="23" onclick="closeLoad()" style="cursor: hand" />
                <div id="uploadFile"></div>
            </td>
        </tr>
    </table>
</body>
</html>
