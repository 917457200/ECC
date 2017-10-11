<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="WebAPI.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
        <script src="js/jquery-1.7.2.min.js"></script>
    <script src="/js/WebJS.js"></script>
    <title></title>
    <script>
        $(function () {
            $('#push').click(function () {
                var text = "text1";
                var images = "image1.png";
                var videos = "video1.mp4";
                var modeltype = 1;
                var deviceSerial = "";
                var msgTitle = "title1";
                var msgType = 1;
                var roomNum = "";
                var alias = "";
                var tags = "";
                var sender = "";
                var sourceType = 0;
                var receiverType = 0;
                var receiverIds = "";
                var receiver = "";
                var content_type = "text";
                var msgExtras = null;
                var url = "/Service.ashx?actionname=01-01";
               
                var pushMessageContentFile = PushMessageContentTypeCheck(text, images, videos);
                if (pushMessageContentFile != null) {
                    var pushMessageContent = PushMessageContentCheck(modeltype, pushMessageContentFile, msgTitle);
                    if (pushMessageContent != null) {
                        var PushMessage = PushMessageCheck(pushMessageContent, content_type,msgTitle, msgExtras);
                        if (PushMessage != null) {
                            var PushData = PushDataCheck(modeltype, pushMessageContent,PushMessage);
                            if (PushData != null) {
                              
                                EDUCAjax(PushData, function () { }, function (data) { }, url);
                            }
                        }
                    }
                }
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <asp:Label ID="Label1" runat="server" Text="推送内容"></asp:Label>  <asp:TextBox ID="content" runat="server"></asp:TextBox>
        <input type="button" id="push" value="推送紧急模式" />
    </div>
    </form>
</body>
</html>
