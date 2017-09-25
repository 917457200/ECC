<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm4.aspx.cs" Inherits="WebAPI.WebForm4" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="js/jquery-1.7.2.min.js"></script>
    <script src="/js/WebJS.js"></script>
    <title></title>
    <script>
        $(function () {
            $('#push').click(function () {
                CurrentDate();
                var DisplayModelID = 2;
                var OperateTypeID = 1;
                var MessageTitle = "text";
                var MessageTypeID = 1;
                var MessageSourceID = 1;
                var MessageContent = "";
                //var TargetRange = {};
                var RargetAlias = "";
                var TaskBeginTime = "";
                var TaskEndTime = "";
                var TaskPriorityID = 1;
                var ImageSpanSecond = 5;
                var ImageEffectID = 1;
                var TaskStatusID = 1;
                var Note = "";
                var CreatedID = 1;
                var CreatedName = 'hxx';
                var CreatedDate = "";
                var ModifiedID = 1;
                var ModifiedName = 'hxx';
                var ModifiedDate = "";
                var TaskResultID = 0;
                var TaskTypeID = 1;
                var url = "/Service.ashx?actionname=01-01";
                var strimage = Array();
                strimage.push("image1.png");
                strimage.push("image2.png");
                var strvideo = Array();
                strvideo.push("video1.mp4");
                strvideo.push("video2.mp4");
                var customMessageContent = CustomMessageContent().TextString("text1").ImageArray(strimage).VideoArray(strvideo);
                MessageContent = customMessageContent;

                var strtag = Array();
                strtag.push("001");
                strtag.push("tag2");
                var strtag_and = Array();
                strtag_and.push("tag_and1");
                strtag_and.push("tag_and2");
                var audience = Audience().TagArray(strtag);
                var TargetRange = audience.audience;
                var deviceTaskInfo = DeviceTaskInfo(DisplayModelID, OperateTypeID, MessageTitle, MessageTypeID, MessageSourceID,
                   MessageContent, TargetRange, RargetAlias, TaskBeginTime, TaskEndTime, TaskPriorityID, ImageSpanSecond,
                    ImageEffectID, TaskStatusID, Note, CreatedID, CreatedName, CreatedDate, ModifiedID, ModifiedName, ModifiedDate, TaskResultID, TaskTypeID);
                var message = Message(deviceTaskInfo).Content_type("text").Title("a");
                var pushPayload = PushPayload(PlatformEnum.android_ios, audience).Message(message);
                var pushData = { "pushpayload": JSON.stringify(pushPayload), "devicetaskinfo": JSON.stringify(deviceTaskInfo) };

                //EDUCAjax(JSON.stringify(aaa), function () { }, function (data) { }, url);
                EDUCAjax(pushData, function () { }, function (data) {
                    alert(data.errormsg);
                }, url);

            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Text="推送内容"></asp:Label>
            <asp:TextBox ID="content" runat="server"></asp:TextBox>
            <input type="button" id="push" value="推送紧急模式" />
        </div>
    </form>
</body>
</html>
