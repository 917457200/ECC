/// <reference path="../../jquery.min.js" />
/// <reference path="../../jquery.min.js" />

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
//随机数
function random(min, max) {
    return String(Math.floor(min + Math.random() * (max - min)));
}

function setCookie(name, value, time) {
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

function getCookie(name) {
    var arr, reg = new RegExp("(^| )" + name + "=([^;]*)(;|$)");
    if (arr = document.cookie.match(reg))
        return unescape(arr[2]);
    else
        return null;
}

function delCookie(name) {
    var exp = new Date();
    exp.setTime(exp.getTime() - 1);
    var cval = getCookie(name);
    if (cval != null)
        document.cookie = name + "=" + cval + ";expires=" + exp.toGMTString();
}
var loginUser;

$(document).ready(function () {

    var animating = false,
        submitPhase1 = 1100,
        submitPhase2 = 400,
        logoutPhase1 = 800,
        $login = $(".login"),
        $app = $(".app");

    function ripple(elem, e) {
        $(".ripple").remove();
        var elTop = elem.offset().top,
            elLeft = elem.offset().left,
            x = e.pageX - elLeft,
            y = e.pageY - elTop;
        var $ripple = $("<div class='ripple'></div>");
        $ripple.css({ top: y, left: x });
        elem.append($ripple);
    };

    $(document).on("click", ".login__submit", function (e) {
      
        if (animating) return;
        animating = true;
        var that = this;
       
     
        var usercode = $.toString($('#usercode').val());
        var usertype = $.toString($('#usertype').val());
        var password = $.toString($('#password').val());
        var url = "../../Service.ashx?actionname=03-01";
        var par = { "userCode": usercode, "userType": usertype, "password": password };
        $.ajax({
            type: "POST",
            dataType: "JSON",
            url: url,
            async: true,
            beforeSend: function () {
              
                $(that).addClass("processing");
                //beforeback();
            },
            data: par,
            success: function (res) {
            
                if (undefined != res.ret && res.ret.length > 0 && $.toString(res.ret[0]).indexOf("SUCC_01") > -1) {
                    //写入cookie
                    setCookie("loginUser", res.ret[0]);
                    gotourl('live.html');
                }
                else {
                  
                    animating = false;
                    //$(that).removeClass("processing");
                    location.reload(true);
                }
               
                //callback(res);
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                animating = false;
              
                $(that).removeClass("processing");
            }
        });
        //setTimeout(function () {
        //  //  $(that).addClass("success");
        //    setTimeout(function () {
        //        //$(".ripple").remove();
        //        this.location.reload();
        //        //$app.show();
        //        //$app.css("top");
        //        //$app.addClass("active");
        //    }, submitPhase2 - 70);
        //    //setTimeout(function () {
        //    //    //$login.hide();
        //    //    $login.addClass("inactive");
        //    //    animating = false;
        //    //    $(that).removeClass("success processing");
        //    //}, submitPhase2);
        //}, submitPhase1);
    });

    $(document).on("click", ".app__logout", function (e) {
        if (animating) return;
        $(".ripple").remove();
        animating = true;
        var that = this;
        $(that).addClass("clicked");
        setTimeout(function () {
            $app.removeClass("active");
            $login.show();
            $login.css("top");
            $login.removeClass("inactive");
        }, logoutPhase1 - 120);
        setTimeout(function () {
            $app.hide();
            animating = false;
            $(that).removeClass("clicked");
        }, logoutPhase1);
    });
    $.toString = function (str) {
        if (typeof str == "undefined" || (str + "") == "" || str == null || str == "") {
            return "";
        } else {
            return str + "";
        }
    }
  
});
