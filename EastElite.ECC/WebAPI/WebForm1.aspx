<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebAPI.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <script src="js/jquery-1.7.2.min.js"></script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Label1" runat="server" Text="推送内容"></asp:Label>  <asp:TextBox ID="content" runat="server"></asp:TextBox>
        <input type="button" id="push" value="推送紧急模式" />

    </div>
    </form>

    <script>

        $(function () {
            /*Ajax方法
 data：需要上传的数据
 beforeback：Ajax执行之前(回调函数)
 callback：Ajax获取请求后的数据(回调函数)
 url：请求地址
 datatype：数据类型
 lodingState：是否显示loding
 lodingMsg：loding 提示语
 async:：是否异步，默认异步
*/
            function Ajax(parameters) {
                try
                {
                $.ajax({
                    type: "POST",
                    dataType: "json",
                    url: "/Service.ashx?actionname=01-01" + "&" + Math.random(),
                    async: "true",
                    timeout: 5000,
                    beforeSend: function () {

                    },
                    data: parameters,
                    success: function (data) {
                        var strdata = eval(data);
                        alert(data.errormsg);
                       
                    },
                    error: function (e) {
                    
                    }
                });
            }
                catch (dd) { alert("exce0tiomn");};
            }
            $('#push').click(function () {
                var jsonContent = '{"modeltype":"1","platform": "all","audience":"all","content":"' + $("#content").val() + '"}';
                var message = '{ "msg_content": ' + jsonContent + ',"content_type": "text", "title": "msg","extras": {"key": "value"}}';
                var jsondata = { "pushpayload": '{ "platform": "all","audience":"all", "message":' + message + '}', "modelType": "1" };
                Ajax(jsondata);
            });
        });
    </script>
</body>
  
</html>