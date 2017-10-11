$(function () {
       String.prototype.trim = function () {
            return this.replace(/(^\s*)|(\s*$)/g, "");
        }
    //loading 提示
    $("body").append('<div id="id_loding" style="display: none;"><div class="background"></div><div id="id_lodingMsg" class="progressBar" style="font-size: 12px; font-family: 微软雅黑;">2222</div></div>');

    //获取URL参数:jQuery 参数获取url已解密
    $.getUrlParam = function (name) {
        /*
        encodeURI();加密url
        decodeURI();解密url
        */
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
        var r = window.location.search.substr(1).match(reg);
        if (r != null) return decodeURI(r[2]); return null;
    }
    //转换字符串，错误数据显示空白
    $.toString = function (str) {
        
        if (typeof str == "undefined" || (str + "") == "" || str == null || str == "") {
            return "";
        } else {
            return str + "";
        }
    }

    //转换int，错误数据显示0
    $.toInt = function (str) {
        if (typeof str == "undefined" || str == null || str == "" || isNaN(parseInt(str))) {
            return 0;
        } else {
            return parseInt(str);
        }
    }

    //转换浮点数，错误数据显示0
    $.toFloat = function (str) {
        if (typeof str == "undefined" || str == null || str == "" || isNaN(parseFloat(str))) {
            return 0;
        } else {
            return $.toFixed(str, 2);
        }
    }

    //转换成XX.XX为小数，默认两位
    $.toFixed = function (str, length) {
        length = arguments.length > 1 ? length : 2;
        if (typeof str == "undefined" || str == null || str == "" || isNaN(parseFloat(str))) {
            return 0;
        } else {
            return parseFloat(parseFloat(str).toFixed(length));
        }
    }

    ///删除首位逗号
    $.toTrimComma = function (str) {
        return str.replace(/,$/g, "").replace(/^,/g, "")

    }
    $.currentDate = function () {
        /// <summary>日期格式化</summary>     
        /// <returns type="currentDate">The area.</returns>
        var myDate = new Date();

        var currentDate = {};
        currentDate.Year = myDate.getFullYear().toString();
        currentDate.Month = (myDate.getMonth() + 1).toString().length < 2 ? "0" + (myDate.getMonth() + 1).toString() : (myDate.getMonth() + 1).toString();
        currentDate.Day = myDate.getDate().toString().length < 2 ? "0" + myDate.getDate().toString() : myDate.getDate().toString();
        currentDate.Hours = myDate.getHours().toString().length < 2 ? "0" + myDate.getHours().toString() : myDate.getHours().toString();
        currentDate.Minutes = myDate.getMinutes().toString().length < 2 ? "0" + myDate.getMinutes().toString() : myDate.getMinutes().toString();
        currentDate.Seconds = myDate.getSeconds().toString().length < 2 ? myDate.getSeconds().toString() : myDate.getSeconds().toString();
        currentDate.ShortDate = currentDate.Year + "-" + currentDate.Month + "-" + currentDate.Day;
        currentDate.ShortTime = currentDate.Hours + ":" + currentDate.Minutes + ":" + currentDate.Seconds;
        currentDate.DateTime = currentDate.ShortDate + " " + currentDate.ShortTime;
        return currentDate;
    }
});

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
function EDUCAjax() {
    var data = arguments[0];
    var beforeback = arguments[1];
    var callback = arguments[2];
    var url = arguments[3];
    var datatype = arguments.length > 4 ? arguments[4] : "json";
    var lodingState = arguments.length > 5 ? arguments[5] : false;
    var lodingMsg = arguments.length > 6 ? arguments[6] : "数据加载中，请耐心等待...";
    var async = arguments.length > 7 ? arguments[7] : true;

    var loding = $("#id_loding");//loading提示
    $.ajax({
        type: "POST",
        dataType: datatype.toLowerCase(),

        url: url,
        async: async,
        beforeSend: function () {
            //显示loding提示
            if (lodingState) {
                $("#id_lodingMsg").html(lodingMsg);
                loding.show();
            }
            beforeback();
        },
        data: data,
        success: function (res) {
            if (lodingState) {
                loding.hide();
            }
            callback(res);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("服务器异常，请稍后重试！");
            //var XMLHttpRequestStr = XMLHttpRequest.readyState + "  " + XMLHttpRequest.status + "  " +
            //    XMLHttpRequest.statusText + "  " + XMLHttpRequest.responseText;
            //alert("XMLHttpRequest：" + XMLHttpRequestStr + "</br>textStatus：" + textStatus + "</br>errorThrown：" + errorThrown);
        }
    });
}


//根据数据字典类型获取常用字典数据
function GetDictsByType(controlId, typename, checkDefault, selectValue, filter) {
    /// <summary>根据数据字典类型获取常用字典数据</summary>   
    /// <param name="controlId" type="string">显示标签ID</param>
    /// <param name="typename" type="string">字典类型</param>
    /// <param name="checkDefault" type="bool">是否添加默认全部</param>
    /// <param name="selectValue" type="string">默认选中的value</param>

    var par = {
        "itemName": typename,
        "filter": filter
    };
    var url = "/Service.ashx?actionname=02-01";
    EDUCAjax(par, function () { }, function (rs) {
        if (rs.status = "0") {
            $('#' + controlId).html("");
            if (checkDefault) {
                $('#' + controlId).append('<option value="0"></option>');
            }

            $(rs.data).each(function (i, o) {
                $('#' + controlId).append('<option value="' + o.ItemKey + '" note ="' + o.Note + '">' + o.ItemValue + '</option>');
            });

            if (selectValue != "") {
                $('#' + controlId).find("option[value='" + selectValue + "']").attr("selected", true);
                $('#' + controlId).trigger("change");
            }

        }
    }, url);


}

var getCookie = function (name) {
    var arr, reg = new RegExp("(^| )" + name + "=([^;]*)(;|$)");
    if (arr = document.cookie.match(reg))
        return unescape(arr[2]);
    else
        return null;
}
var delCookie = function (name) {
    var exp = new Date();
    exp.setTime(exp.getTime() - 1);
    var cval = getCookie(name);
    if (cval != null)
        document.cookie = name + "=" + cval + ";expires=" + exp.toGMTString();
}
///time=d30--30天，默认值为回话cookie
var setCookie = function (name, value, time) {
    var strsec = arguments.length > 2 ? getsec(arguments[2]) : "";
    var exp = new Date();
    exp.setTime(exp.getTime() + strsec * 1);
    if (strsec != "") {
        document.cookie = name + "=" + escape(value) + ";expires=" + exp.toGMTString();
    }
    else {
        document.cookie = name + "=" + escape(value) + ";";
    }
}

function getsec(str) {

    var str1 = str.substring(1, str.length) * 1;
    var str2 = str.substring(0, 1);
    if (str2 == "s") {
        return str1 * 1000;
    }
    else if (str2 == "h") {
        return str1 * 60 * 60 * 1000;
    }
    else if (str2 == "d") {
        return str1 * 24 * 60 * 60 * 1000;
    }
}
//随机数
function random(min, max) {
    return String(Math.floor(min + Math.random() * (max - min)));
}
//跳转地址，加随机数url:跳转路径，isparent是否从父窗体打开，默认false
function gotourl(url, isparent) {
    /*
     encodeURI();加密url
     decodeURI();解密url
     */
    isparent = arguments.length > 1 ? isparent : false;
    var par = '';
    if (url.indexOf('?') < 0) {
        par = '?';
    }
    else {
        par = '&';
    }
    if (isparent) {
        //window.parent.location.href = encodeURI(url + par + 'v=' + String(random(1111, 999999)));
        window.open(encodeURI(url + par + 'v=' + String(random(1111, 999999))));
    } else {
        location.href = encodeURI(url + par + 'v=' + String(random(1111, 999999)));
    }
}

//String.prototype.format = function (args) {
//    if (arguments.length > 0) {
//        var result = this;
//        if (arguments.length == 1 && typeof (args) == "object") {
//            for (var key in args) {
//                var reg = new RegExp("({" + key + "})", "g");
//                result = result.replace(reg, args[key]);
//            }
//        }
//        else {
//            for (var i = 0; i < arguments.length; i++) {
//                if (arguments[i] == undefined) {
//                    return "";
//                }
//                else {
//                    var reg = new RegExp("({[" + i + "]})", "g");
//                    result = result.replace(reg, arguments[i]);
//                }
//            }
//        }
//        return result;
//    }
//    else {
//        return this;
//    }
//}
String.prototype.format = function (args) {
    var result = this;
    if (arguments.length > 0) {
        if (arguments.length == 1 && typeof (args) == "object") {
            for (var key in args) {
                if (args[key] != undefined) {
                    var reg = new RegExp("({" + key + "})", "g");
                    result = result.replace(reg, args[key]);
                }
            }
        }
        else {
            for (var i = 0; i < arguments.length; i++) {
                if (arguments[i] != undefined) {
                    var reg = new RegExp("({)" + i + "(})", "g");
                    result = result.replace(reg, arguments[i]);
                }
            }
        }
    }
    return result;
}
Array.prototype.indexOf = function (val)
{ for (var i = 0; i < this.length; i++) { if (this[i] == val) return i; } return -1; };
Array.prototype.remove = function (val) { var index = this.indexOf(val); if (index > -1) { this.splice(index, 1); } };
/* 
    *  删除数组元素:Array.removeArr(index) 
    */
Array.prototype.removeArr = function (index) {
    if (isNaN(index) || index >= this.length) { return false; }
    this.splice(index, 1);
}
/* 
*  插入数组元素:Array.insertArr(dx) 
*/
Array.prototype.insertArr = function (index, item) {
    this.splice(index, 0, item);
};
function DateFormat(DateString) {

}


//格式化日期使用示例：
//alert(new Date().Format("yyyy年MM月dd日"));
//alert(new Date().Format("MM/dd/yyyy"));
//alert(new Date().Format("yyyyMMdd"));
//alert(new Date().Format("yyyy-MM-dd hh:mm:ss"));
Date.prototype.Format = function (fmt) { //author: meizz 
    var o = {
        "M+": this.getMonth() + 1, //月份 
        "d+": this.getDate(), //日 
        "h+": this.getHours(), //小时 
        "m+": this.getMinutes(), //分 
        "s+": this.getSeconds(), //秒 
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度 
        "S": this.getMilliseconds() //毫秒 
    };
    if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
}
//管理员拥有的菜单；, { value: "classmanage", desc: "班级管理" }
var adminmenumlist = [
    {
        value: "taskmanage",
        desc: "任务管理",
        submenu: [
               { value: "devicemanage", desc: "设备管理", cssClass: "glyphicon glyphicon-th-list" },
            { value: "taskmanage", desc: "任务查询", cssClass: "glyphicon glyphicon-th-list" },
            { value: "taskrelease", desc: "任务发布", cssClass: "glyphicon glyphicon-edit" },
             { value: "examtionimport", desc: "考试数据导入", cssClass: "glyphicon glyphicon-import" },
             { value: "startupcontrol", desc: "启动程序控制", cssClass: "glyphicon glyphicon-cog" }
           
        ]
    },
    {
        value: "backmanage", desc: "后台管理",
        submenu: [
            { value: "backmanage", desc: "电子班牌任务管理", cssClass: "glyphicon glyphicon-th-list" },
              { value: "SPbackmanage", desc: "启动程序任务管理", cssClass: "glyphicon glyphicon-th-list" },
             { value: "classmaintenance", desc: "班级绑定", cssClass: "glyphicon glyphicon-th-list" },
             { value: "userclassmanage", desc: "班级授权", cssClass: "glyphicon glyphicon-play" },
               { value: "loginfolist", desc: "日志管理", cssClass: "glyphicon glyphicon-th-list" },
             { value: "deviceterminalscreenshotlist", desc: "设备监控", cssClass: "glyphicon glyphicon-th-list" },
            { value: "systemseting", desc: "系统设置", cssClass: "glyphicon glyphicon-cog" }
           
        ]
    }];
//当前用户拥有的菜单
var usermenumlist = [
{
    value: "taskmanage",
    desc: "任务管理",
    submenu: [
        { value: "taskmanage", desc: "任务查询", cssClass: "glyphicon glyphicon-th-list" },
        { value: "taskrelease", desc: "任务发布", cssClass: "glyphicon glyphicon-edit" }
    ]
},
    {
        value: "classmanage", desc: "班级管理",
        submenu: [
            { value: "classmanage", desc: "班级管理", cssClass: "glyphicon glyphicon-wrench" }
        ]
    }];
var loginUser = {};//登陆用户

///当前主菜单，子菜单
function loadHead(mainMenu, subMenu) {

    if (getCookie("loginUser") == null) {

        gotourl("login.html");
        return false;
    }
    else {
        loginUser = eval("(" + getCookie("loginUser") + ")");
        //检查权限
        if (checkaccess(mainMenu, subMenu)) {

            //加载login
            var strhtml = "";
            strhtml += '<div class="bg-light b-b navbar-top">';
            strhtml += '  <div class="container clearfix">';
            strhtml += '  <span>欢迎使用</span>';
            strhtml += '   <span class="text-primary">壹键通电子班牌管理系统</span>';
            strhtml += '     <div id="logoutForm" class="pull-right">';
            strhtml += '<ul class="reset login-wrapper">';
            strhtml += ' <li><span id="loginuser">您好 , {0} 老师</span> </li>';

            strhtml += '   <li class="divider">   <span>&nbsp;</span> </li>';

            strhtml += ' <li><a href="javascript:loginout();" id="loginout">退出</a></li>';
            strhtml += '  </ul>';
            strhtml += '  </div> </div> </div>';

            //检索项
            strhtml += '<div class="container m-b-n">';
            strhtml += ' <div class="row">';
            strhtml += '   <div class="col-md-8 col-xs-8"><a href="#"> <img src="Css/img/logo.png" style="height: 60px;" /> <span class="text-black text-big"></span>  </a></div>';
            strhtml += '<div class="col-md-4 col-xs-4">';
            strhtml += '<div class="input-group m-t-l" action="#" id="searchForm">';
            strhtml += '<input type="text" name="keyword" class="form-control" placeholder="请输入关键字" autocomplete="off">';
            strhtml += '  <div class="input-group-btn dropdown"><button class="btn btn-default" type="submit"><span class="glyphicon glyphicon-search"></span><span>搜索</span> </button> </div>';
            strhtml += '</div> ';
            strhtml += '   </div> ';
            strhtml += '  </div>  ';
            strhtml += '  </div>';

            strhtml += '  <div class="bg-primary">';
            strhtml += '<div class="container nav-main" id="navMain">';
            //加载菜单
            strhtml += loadMainmenu(mainMenu);
            strhtml += '  </div>';
            strhtml += '  </div>';



            strhtml = strhtml.format(loginUser.userName, "display")
            $('#head').append(strhtml);
            loadSubmenu(mainMenu,subMenu);
            return true;
        }
        else {//没有权限进入该页面
            gotourl("login.html");
            return false;
        }

    }
}
//检查权限
function checkaccess(currentMainMenumValue, currentSubMenumValue) {
    if (loginUser.roleCode == '1072') {

        if (undefined != adminmenumlist && null != adminmenumlist && adminmenumlist.length > 0) {
            for (var i = 0; i < adminmenumlist.length; i++) {
                if (adminmenumlist[i].value.toLowerCase() == currentMainMenumValue.toLowerCase()) {
                    if (null != adminmenumlist[i].submenu)
                    {
                        for (var j = 0; j < adminmenumlist[i].submenu.length; j++) {
                            if (adminmenumlist[i].submenu[j].value.toLowerCase() == currentSubMenumValue.toLowerCase()) {
                                return true;
                            }
                        }
                    }
                   
                }
            }
        }
    }
    else {
        if (undefined != usermenumlist && null != usermenumlist && usermenumlist.length > 0) {
            for (var i = 0; i < usermenumlist.length; i++) {
                if (usermenumlist[i].value.toLowerCase() == currentMainMenumValue.toLowerCase()) {
                    if (null != usermenumlist[i].submenu)
                    {
                        for (var j = 0; j < usermenumlist[i].submenu.length; j++) {
                            if (usermenumlist[i].submenu[j].value.toLowerCase() == currentSubMenumValue.toLowerCase()) {
                                return true;
                            }
                        }
                    }
                }
            }
        }
    }
    return false;
}
//加载菜单
function loadMainmenu(currentMainMenumValue) {

    var strhtml = "";
    if (loginUser.roleCode == '1072') {
        if (undefined != adminmenumlist && null != adminmenumlist && adminmenumlist.length > 0) {
            for (var i = 0; i < adminmenumlist.length; i++) {
                if (adminmenumlist[i].value.toLowerCase() == currentMainMenumValue.toLowerCase()) {
                    strhtml += ' <a href="' + adminmenumlist[i].value + '.html" class="active"> <span>' + adminmenumlist[i].desc + '</span></a>';
                }
                else {
                    strhtml += ' <a href="' + adminmenumlist[i].value + '.html" <span>' + adminmenumlist[i].desc + '</span></a>';
                }
            }
        }
    }
    else {
        if (undefined != usermenumlist && null != usermenumlist && usermenumlist.length > 0) {
            for (var i = 0; i < usermenumlist.length; i++) {
                if (usermenumlist[i].value.toLowerCase() == currentMainMenumValue.toLowerCase()) {
                    strhtml += ' <a href="' + usermenumlist[i].value + '.html" class="active"> <span>' + usermenumlist[i].desc + '</span></a>';
                }
                else {
                    strhtml += ' <a href="' + usermenumlist[i].value + '.html" <span>' + usermenumlist[i].desc + '</span></a>';
                }
            }
        }
    }
    return strhtml;
}
//加载菜单
function loadSubmenu(currentMainMenumValue,currentSubMenumValue) {

    var strhtml = "";
    if (loginUser.roleCode == '1072') {
        if (undefined != adminmenumlist && null != adminmenumlist && adminmenumlist.length > 0) {
            for (var i = 0; i < adminmenumlist.length; i++) {
                if (adminmenumlist[i].value.toLowerCase() == currentMainMenumValue.toLowerCase()) {
                    if (null != adminmenumlist[i].submenu) {
                        for (var j = 0; j < adminmenumlist[i].submenu.length; j++) {
                            if (adminmenumlist[i].submenu[j].value.toLowerCase() == currentSubMenumValue.toLowerCase()) {
                                var strhtml = '<li class="active"><a href="' + adminmenumlist[i].submenu[j].value + '.html"><i class="' + adminmenumlist[i].submenu[j].cssClass + '"></i><span>' + adminmenumlist[i].submenu[j].desc + '</span></a></li>';
                                $('#subMenuList').append(strhtml);

                            }
                            else {
                                var strhtml = '<li><a href="' + adminmenumlist[i].submenu[j].value + '.html"><i class="' + adminmenumlist[i].submenu[j].cssClass + '"></i><span>' + adminmenumlist[i].submenu[j].desc + '</span></a></li>';
                                $('#subMenuList').append(strhtml);
                            }
                        }
                    }

                }
            }
        }
    }
    else {
        if (undefined != usermenumlist && null != usermenumlist && usermenumlist.length > 0) {
            for (var i = 0; i < usermenumlist.length; i++) {
                if (usermenumlist[i].value.toLowerCase() == currentMainMenumValue.toLowerCase()) {
                    if (null != usermenumlist[i].submenu) {
                        for (var j = 0; j < usermenumlist[i].submenu.length; j++) {
                            if (usermenumlist[i].submenu[j].value.toLowerCase() == currentSubMenumValue.toLowerCase()) {
                                var strhtml = '<li class="active"><a href="' + usermenumlist[i].submenu[j].value + '.html"><i class="' + usermenumlist[i].submenu[j].cssClass + '"></i><span>' + usermenumlist[i].submenu[j].desc + '</span></a></li>';
                                $('#subMenuList').append(strhtml);

                            }
                            else {
                                var strhtml = '<li><a href="' + usermenumlist[i].submenu[j].value + '.html"><i class="' + usermenumlist[i].submenu[j].cssClass + '"></i><span>' + usermenumlist[i].submenu[j].desc + '</span></a></li>';
                                $('#subMenuList').append(strhtml);
                            }
                        }
                    }

                }
            }
        }
    }
    return strhtml;
}
//退出登录
function loginout() {
    delCookie("loginUser");
    gotourl("login.html");
}

function login(usercode, userType, password) {
    var url = "/Service.ashx?actionname=03-01";
    var par = { "userCode": usercode, "userType": userType, "password": password }

    EDUCAjax(par, function () {
        $('#login').html("登陆中...");
    }, function (re) {
        $('#login').html("登陆");

        if (undefined != re.ret && re.ret.length > 0) {

            if ($.toString(re.ret[0]).indexOf("用户登录成功") > 0) {
                var currentCalss = new Array();//当前用户下的班级
                if (re.data.result[0].roleCode != '1072') {

                    var url1 = "/Service.ashx?actionname=01-04";
                    var par1 = { "userCode": re.data.result[0].userCode, "rootCode": re.data.result[0].rootCode };
                    EDUCAjax(par1, function () { }, function (res) {
                        if (res.status == 0) {
                            $(res.data).each(function (i, o) {
                                currentCalss[i] = o.ClassName;
                            });
                            var user = $.extend({}, re.data.result[0], { "campus": re.data.campus }, { "currentCalss": currentCalss });
                            setCookie("loginUser", JSON.stringify(user),'d1');

                            gotourl("/taskRelease.html");
                        }
                        else {
                            $('.bg-danger .text-danger').html("网络异常,请重试!");
                            $('.bg-danger').show();
                        }
                    }, url1);

                }
                else {
                    var user = $.extend({}, re.data.result[0], { "campus": re.data.campus }, { "currentCalss": currentCalss });
                    setCookie("loginUser", JSON.stringify(user),'d1');

                    gotourl("/taskRelease.html");
                }
            }
            else {
                $('.bg-danger .text-danger').html(re.ret[0]);
                $('.bg-danger').show();
            }
            //re.data.result[0].userRole = 2;
            //re.data.result[0].userRole = 1;

            //if (user.roleCode == 1003 || user.roleCode == 1003)




        }
        else {
            $('.bg-danger .text-danger').html("网络异常,请重试!");
            $('.bg-danger').show();

        }


    }, url);
}

function getQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}
function loadjscssfile(filename, filetype) {
    if (filetype == "js") {
        var fileref = document.createElement('script')
        fileref.setAttribute("type", "text/javascript")
        fileref.setAttribute("src", filename)
    }
    else if (filetype == "css") {
        var fileref = document.createElement("link")
        fileref.setAttribute("rel", "stylesheet")
        fileref.setAttribute("type", "text/css")
        fileref.setAttribute("href", filename)
    }
    if (typeof fileref != "undefined")
        document.getElementsByTagName("head")[0].appendChild(fileref)
}

function removejscssfile(filename, filetype) {
    var targetelement = (filetype == "js") ? "script" : (filetype == "css") ? "link" : "none"
    var targetattr = (filetype == "js") ? "src" : (filetype == "css") ? "href" : "none"
    var allsuspects = document.getElementsByTagName(targetelement)
    for (var i = allsuspects.length; i >= 0; i--) {
        if (allsuspects[i] && allsuspects[i].getAttribute(targetattr) != null && allsuspects[i].getAttribute(targetattr).indexOf(filename) != -1)
            allsuspects[i].parentNode.removeChild(allsuspects[i])
    }
}

function myBrowser() {
    var userAgent = navigator.userAgent; //取得浏览器的userAgent字符串
    var isOpera = userAgent.indexOf("Opera") > -1; //判断是否Opera浏览器
    var isIE = userAgent.indexOf("compatible") > -1 && userAgent.indexOf("MSIE") > -1 && !isOpera; //判断是否IE浏览器
    var isFF = userAgent.indexOf("Firefox") > -1; //判断是否Firefox浏览器
    var isSafari = userAgent.indexOf("Safari") > -1; //判断是否Safari浏览器
    if (isIE) {
        var IE5 = IE55 = IE6 = IE7 = IE8 = IE9 = false;
        var reIE = new RegExp("MSIE (\\d+\\.\\d+);");
        reIE.test(userAgent);
        var fIEVersion = parseFloat(RegExp["$1"]);
        IE55 = fIEVersion == 5.5;
        IE6 = fIEVersion == 6.0;
        IE7 = fIEVersion == 7.0;
        IE8 = fIEVersion == 8.0;
        IE9 = fIEVersion == 9.0;
        if (IE5) {
            return "IE5";
        }
        if (IE55) {
            return "IE55";
        }
        if (IE6) {
            return "IE6";
        }
        if (IE7) {
            return "IE7";
        }
        if (IE8) {
            return "IE8";
        }
        if (IE9) {
            return "IE9";
        }
    }//isIE end
    if (isFF) {
        return "FF";
    }
    if (isOpera) {
        return "Opera";
    }
}