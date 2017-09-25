$(function () {
    //loading 提示
    $("body").append('<div id="id_loding" style="display: none;"><div class="background"></div><div id="id_lodingMsg" class="progressBar" style="font-size: 12px; font-family: 微软雅黑;">2222</div></div>');


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


    $.currentDate = function () {
        /// <summary>日期格式化</summary>     
        /// <returns type="currentDate">The area.</returns>
        var myDate = new Date();

        var currentDate = {};
        currentDate.Year = myDate.getFullYear().toString();
        currentDate.Month = (myDate.getMonth() + 1).toString();
        currentDate.Day = myDate.getDate().toString();
        currentDate.Hours = myDate.getHours().toString();
        currentDate.Minutes = myDate.getMinutes().toString();
        currentDate.Seconds = myDate.getSeconds().toString();
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
    var url = arguments[3] + "&" + Math.random();
    var datatype = arguments.length > 4 ? arguments[4] : "json";
    var lodingState = arguments.length > 5 ? arguments[5] : false;
    var lodingMsg = arguments.length > 6 ? arguments[6] : "数据加载中，请耐心等待...";
    var async = arguments.length > 7 ? arguments[7] : true;

   // var loding = $("#id_loding");//loading提示
    $.ajax({
        type: "POST",
        dataType: datatype.toLowerCase(),
        //contentType: "application/json;charset=utf-8",
        url: url + "?" + Math.random(),
        async: async,
        beforeSend: function () {
            //显示loding提示
            if (lodingState) {
                //$("#id_lodingMsg").html(lodingMsg);
                //loding.show();
            }
            beforeback();
        },
        data: data,
        success: function (res) {
            //if (lodingState) {
            //    loding.hide();
            //}
            callback(res);
        },
        error: function(){}
     
    });
}


//根据数据字典类型获取常用字典数据
function GetDictsByType(controlId, typename, checkDefault, selectValue) {
    /// <summary>根据数据字典类型获取常用字典数据</summary>   
    /// <param name="controlId" type="string">显示标签ID</param>
    /// <param name="typename" type="string">字典类型</param>
    /// <param name="checkDefault" type="bool">是否添加默认全部</param>
    /// <param name="selectValue" type="string">默认选中的value</param>

    var par = {
        "itemName": typename
    };
    var url = "/Service.ashx?actionname=02-01";
    EDUCAjax(par, function () { }, function (rs) {
        if (rs.status = "0") {
            $('#' + controlId).html("");
            if (checkDefault) {
                $('#' + controlId).append('<option value="">---全部---</option>');
            }

            $(rs.data).each(function (i, o) {
                $('#' + controlId).append('<option value="' + o.ItemKey + '">' + o.ItemValue + '</option>');
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
var setCookie = function (name, value, time) {
    var strsec = arguments.length > 2 ? getsec(arguments[2]) : getsec('d30');
    var exp = new Date();
    exp.setTime(exp.getTime() + strsec * 1);
    document.cookie = name + "=" + escape(value) + ";expires=" + exp.toGMTString();
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

String.prototype.format = function (args) {
    if (arguments.length > 0) {
        var result = this;
        if (arguments.length == 1 && typeof (args) == "object") {
            for (var key in args) {
                var reg = new RegExp("({" + key + "})", "g");
                result = result.replace(reg, args[key]);
            }
        }
        else {
            for (var i = 0; i < arguments.length; i++) {
                if (arguments[i] == undefined) {
                    return "";
                }
                else {
                    var reg = new RegExp("({[" + i + "]})", "g");
                    result = result.replace(reg, arguments[i]);
                }
            }
        }
        return result;
    }
    else {
        return this;
    }
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


