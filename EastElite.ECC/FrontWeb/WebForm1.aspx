<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="FrontWeb.WebForm1" %>

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
        <asp:Button ID="push" runat="server" Text="推送紧急模式" />  
    </div>
    </form>

    <script>
    
        $(function () {
            $('#push').click(function () {
                var jsondata = '测试';
           
                $.post("/Service.ashx?actionname=01-01", jsondata, function (data, status) {
                    if (status == 0) {
                        alert("消息推送成功");
                    }
                })
            });
        });
    </script>
</body>
  
</html>
