<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm7.aspx.cs" Inherits="EastElite.ECC.WebForm7" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
     <script src="js/jquery-1.7.2.min.js"></script>
    <script src="js/WebJS.js"></script>
    <title></title>
   
    <script>
        $(function () {
            $('#login').click(function () {
                var url = "/Service.ashx?actionname=03-01";
                $.toString($('#userCode').val())//"090001"   a1s1d1
                var par = { "userCode": $.toString($('#userCode').val()), "userType": $.toInt($('#userType').val()), "password":  $.toString($('#password').val()) }
                //  var par = { "pageSize": "100", "currentPage": "1" }
                EDUCAjax(par, function () { }, function (re) {
                    if ($.toString(re.ret[0]).indexOf("用户登录成功") > 0) {
                        var user = re.data.result[0];
                        setCookie("loginUser", JSON.stringify(user));
                     
                        gotourl("/WebForm6.aspx");
                    }
                  
                
                }, url);
            });

            $('#delete').click(function () {
                delCookie("loginUser");
            });
           
        });

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
            <label for="">userCode：</label>   <input type="text" id="userCode" value="090001" placeholder="请输入账号" />
         <label for="">userType：</label> <input type="text" id="userType" value="1" placeholder="请输入用户类型" />
                 
         <label for="">password：</label>  <input type="text" id="password" value="a1s1d1" placeholder="请输入密码" />
                   
                  
          <input type="button" id="login" value="登陆" />
          <input type="button" id="delete" value="删除cookie" />
    </div>
    </form>
</body>
</html>
