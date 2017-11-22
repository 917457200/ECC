
//var isheadteacher = 0;
$(function () {
    if (!loadHead('taskmanage', 'taskrelease')) {
        return;
    }
    //加载头部
    getClassHead(loginUser.userCode, loginUser.rootCode);
    //布局类型
    GetDictsByType("DisplayModelID", "DisplayModelID", true, "1");
    if (loginUser.roleCode == "1072") {
        //操作类型
        GetDictsByType("OperateTypeID", "OperateTypeID", false, "1", "itemkey not in(3,12)");
    }
    else {
        //操作类型
        GetDictsByType("OperateTypeID", "OperateTypeID", false, "1", "itemkey in(1,2,10,18)");
    }
    //任务类型
    GetDictsByType("TaskTypeID", "TaskTypeID");
    //图片显示效果类型
    GetDictsByType("ImageEffectID", "ImageEffectID");
    $('#TaskTypeID').change(function () {
        if ($.toString($(this).val()) == "1") {
            $('#taskdatetime').hide();
        }
        else if ($.toString($(this).val()) == "2") {

            $('#taskdatetime').show();

        }
        else if ($.toString($(this).val()) == "3") {

            $('#taskdatetime').show();
        }
    });

    $.Upload("file_uploadImage", "uploadImageQueue", UploadTypeEnum.image, true, function (data) {
        $('#table_image').show();
        var index = $('#id_table_list_image').find('tr').length > 0 ? $('#id_table_list_image').find('tr').length + 1 : 1;
        var htmlstr = '<tr style="width:150px;" id="' + index + '">';
        htmlstr += '<td  style="width:50px;" data-url="' + data.url + '">' + data.name + '</td>';
        htmlstr += '<td  style="width:50px;"><img src="' + data.thumnailUrl + '"/></td>';
        htmlstr += '<td  style="width:50px;">' + $.toString(data.width) + '*' + $.toString(data.height) + '</td>';
        htmlstr += '<td  style="width:50px;"><a href="javascript:deletedata(&quot;id_table_list_image&quot;,' + index + ');">删除</a></td>';
        htmlstr += '</tr>';
        $('#id_table_list_image').append(htmlstr);

    });
    $.Upload("file_uploadVideo", "uploadVideoQueue", UploadTypeEnum.video, false, function (data) {
        $('#table_video').show();
        $('#id_table_list_video').find('tr').remove();
        var index = $('#id_table_list_video').find('tr').length > 0 ? $('#id_table_list_video').find('tr').length + 1 : 1;
        var htmlstr = '<tr id="' + index + '">';
        htmlstr += '<td data-url="' + data.url + '">' + data.name + '</td>';
        htmlstr += '<td>' + data.size + '</td>';
        htmlstr += '<td><a href="javascript:deletedata(&quot;id_table_list_video&quot;,' + index + ');">删除</a></td>';
        htmlstr += '</tr>';
        $('#id_table_list_video').append(htmlstr);
    });

    //校区事件
    $('#campus').change(function () {
        //加载所有学科类型
        getUserSubjectTypeList(loginUser.userCode, $.toString($(this).val()));
        //加载所有班级类型
        getUserClassTypeList(loginUser.userCode, $.toString($(this).val()));
        //加载所有班级
        getUserClassList(loginUser.userCode, $.toString($(this).val()));
    });
    //年级事件
    $('#classlist').delegate(':checkbox[name="grade"]', 'change', function () {
        $('#div_grade_' + $.toString($(this).val()) + '_classlist :checkbox').prop("checked", $(this).prop("checked"));

    });

    //班级事件
    $('#classlist').delegate(':checkbox[name="class"]', 'change', function () {
        if ($(this).is(":checked")) {
            if ($.toString($(this).val()) == "") {
                $(this).parent().css("color", "red");
            }
            else {
                $(this).parent().css("color", "blue");
            }
        }
        else {
            $(this).parent().css("color", "");
        }
        //判断是有未选中的
        if ($('#div_grade_' + $.toString($(this).attr("data-grade")) + '_classlist :checkbox:not(:checked)').length > 0) {

            $('#grade_' + $.toString($(this).attr("data-grade"))).removeAttr("checked");

        }
        else {
            //alert($.toString($(this).attr("data-grade")));
            //$('#grade_' + $.toString($(this).attr("data-grade"))).attr("checked", 'true');
            $('#grade_' + $.toString($(this).attr("data-grade"))).prop("checked", 'checked');
        }

    });
    $('#IsCheckReBindClass').click(function () {
        if ($.toInt($('#OperateTypeID').val()) == 17) {
            if ($(this).is(":checked")) {
                GetFailBindClassInfo();
            }
            else {
                var obj = $('#classlist').find(':checkbox');
                obj.prop("checked", false);
                obj.parent().css("color", "");
            }

        }

    })
    $('#OperateTypeID').change(function () {
        if ($.toString($('#OperateTypeID').val()) == "8") {
            $('#NavigationSet').hide();
            $('#StartProgramSwitchSet').hide();
            $('#CheckReBindClass').hide();
            $('#Timingtime').show();
        }
        else if ($.toString($('#OperateTypeID').val()) == "9") {
            $('#Timingtime').hide();
            $('#StartProgramSwitchSet').hide();
            $('#CheckReBindClass').hide();
            $('#NavigationSet').show();

        }
        else if ($.toString($('#OperateTypeID').val()) == "16") {
            $('#Timingtime').hide();
            $('#NavigationSet').hide();
            $('#CheckReBindClass').hide();
            $('#StartProgramSwitchSet').show();

        }
        else if ($.toString($('#OperateTypeID').val()) == "17") {
            $('#Timingtime').hide();
            $('#NavigationSet').hide();
            $('#StartProgramSwitchSet').hide();
            $('#CheckReBindClass').show();
            //getUserClassList1(loginUser.userCode, $.toString($('#campus').val()));
        }
        else {
            $('#NavigationSet').hide();
            $('#StartProgramSwitchSet').hide();
            $('#Timingtime').hide();
            $('#CheckReBindClass').hide();
        }
        $('#msgContentText').val('');
    });

    $("#IsCheckVersion").click(function () {
        if ($(this).is(":checked")) {
            if ($.toInt($('#OperateTypeID').val()) >= 14) {

                GetUnCurrentInstallerVersionECC();
            }
            else {
                GetUnCurrentVersionECC();
            }

        }
        else {
            var obj = $('#classlist').find(':checkbox');
            obj.prop("checked", false);
            obj.parent().css("color", "");

        }
    });

    //全选事件
    $('#classlist').delegate('#allclass', 'change', function () {
        $('#classlist :checkbox').prop("checked", $(this).prop("checked"));
        if ($(this).is(":checked")) {
            $('#classlist :checkbox[name="class"]').each(function (i, o) {
                if ($.toString($(o).val()) == "") {
                    $(this).parent().css("color", "red");
                }
                else {
                    $(this).parent().css("color", "blue");
                }
            })
        }
        else {
            $('#classlist :checkbox[name="class"]').each(function (i, o) {
                $(this).parent().css("color", "");
            });
        }

    });
    $('#classlist').delegate(':checkbox[name="class"]', 'change', function () {
        if ($('#allclass').length > 0) {
            if ($('#classlist :checkbox[name="class"]:checked').length > 0) {
                $('#allclass').prop("checked", true);
            }
            else {
                $('#allclass').prop("checked", false);
            }
        }




    });
  
    
});
function GetFailBindClassInfo() {
    var url = "/Service.ashx?actionname=01-38";
    var par = {};
    EDUCAjax(par, function () { }, function (res) {
        if (res.status == 0) {

            if (res.data.length > 0) {

                $(res.data).each(function (i, o) {

                    var obj = $('#classlist [value="' + $.toString(o.JPushID) + '"]').eq(0);

                    obj.prop("checked", "checked");
                    obj.parent().css("color", "blue");

                });

            }
            else {

            }
        }
    }, url);
}
function GetUnCurrentVersionECC() {
    var url = "/Service.ashx?actionname=01-27";
    var par = {};
    EDUCAjax(par, function () { }, function (res) {
        if (res.status == 0) {

            if (res.data.length > 0) {

                $(res.data).each(function (i, o) {

                    var obj = $('#classlist').find('#class_' + o.ClassCode);

                    if ($.toString(obj.val()) == "") {
                        obj.parent().css("color", "red");
                    }
                    else {
                        obj.prop("checked", "checked");
                        obj.parent().css("color", "blue");
                    }

                });

            }
            else {

            }
        }
    }, url);

}

function GetUnCurrentInstallerVersionECC() {
    var url = "/Service.ashx?actionname=01-34";
    var par = {};
    EDUCAjax(par, function () { }, function (res) {
        if (res.status == 0) {

            if (res.data.length > 0) {

                $(res.data).each(function (i, o) {

                    var obj = $('#classlist').find('#class_' + o.ClassCode);

                    if ($.toString(obj.val()) == "") {
                        obj.parent().css("color", "red");
                    }
                    else {
                        obj.prop("checked", "checked");
                        obj.parent().css("color", "blue");
                    }

                });

            }
            else {

            }
        }
    }, url);

}
//删除上传的文件
function deletedata(control, index) {
    $('#' + control + ' #' + index).remove();
}


//获取用户学科类型
function getUserSubjectTypeList(userCode, rootCode) {
    var url = "/Service.ashx?actionname=01-06";
    var par = { "userCode": userCode, "rootCode": rootCode };
    EDUCAjax(par, function () { }, function (res) {
        if (res.status == 0) {
            $('#SubjectType').empty();
            if (res.data.length > 0) {
                $('#SubjectType').append('<option value=""></option>');
                $(res.data).each(function (i, o) {

                    $('#SubjectType').append('<option value="' + $.toString(o.SubjectTypeID) + '">' + $.toString(o.SubjectTypeIDText) + '</option>');
                });
                $('#SubjectType').show();
                $('#lbSubjectType').show();
            }
            else {
                $('#SubjectType').hide();
                $('#lbSubjectType').hide();
            }
        }
    }, url);

}
//获取用户班级类型
function getUserClassTypeList(userCode, rootCode) {
    var url = "/Service.ashx?actionname=01-07";
    var par = { "userCode": userCode, "rootCode": rootCode };
    EDUCAjax(par, function () { }, function (res) {
        if (res.status == 0) {
            $('#ClassType').empty();
            if (res.data.length > 0) {
                $('#ClassType').append('<option value=""></option>');
                $(res.data).each(function (i, o) {

                    $('#ClassType').append('<option value="' + $.toString(o.ClassTypeID) + '">' + $.toString(o.ClassTypeIDText) + '</option>');
                });
                $('#lbClassType').show();
                $('#ClassType').show();
            }
            else {
                $('#lbClassType').hide();
                $('#ClassType').hide();
            }

        }
    }, url);

}



///班主任加载班级
function getUserSingleClass(userCode) {

    //加载班级
    var url = "/Service.ashx?actionname=01-04";
    var par = { "userCode": userCode };
    EDUCAjax(par, function () { }, function (res) {
        if (res.status == 0) {

            //循环添加年级
            $(res.data).each(function (i, o) {
                var htmlCalss = '<label class="checkbox-inline" style="width: 60px;">';
                htmlCalss += '<input type="checkbox" name="class" id="class_{0}" value="{0}""> {1}</label>';
                htmlCalss = htmlCalss.format($.toString(o.ClassCode), $.toString(o.ClassName));
                $('#classHeadList').append(htmlCalss);

            });


        }
    }, url);
}
//加载班级头部列表
function getClassHead(userCode, rootCode) {
    var url = "/Service.ashx?actionname=01-04";
    var par = { "userCode": userCode, "rootCode": rootCode };
    EDUCAjax(par, function () { }, function (res) {
        if (res.status == 0) {


            //
            if (loginUser.roleCode == "1072") {
                //加载校区
                if ($(loginUser.campus).length > 0) {
                    $(loginUser.campus).each(function (i, o) {
                        $('#campus').append('<option value="' + o.code + '">' + o.name + '</option>');
                    });
                }
                if (res.data.length == 0) {
                    layer.confirm('当前用户权限下没有班级，请进行在系统设置里同步设备和班级', function () {
                        gotourl("systemSeting.html")//跳转系统设置
                    }, function () {
                        $('#lbTaskTypeID').hide();
                        $('#TaskTypeID').hide();
                        $('#lbDisplayModelID').hide();
                        $('#DisplayModelID').hide();
                        $('#CheckVersion').hide();


                        $('#push').remove();

                    });
                    return;
                }


                //加载所有学科类型
                getUserSubjectTypeList(loginUser.userCode, $.toString($('#campus').val()));
                //加载所有班级类型
                getUserClassTypeList(loginUser.userCode, $.toString($('#campus').val()));
                //加载所有班级
                getUserClassList(loginUser.userCode, $.toString($('#campus').val()));


            }
            else if (loginUser.roleCode == "1078") {
                if (res.data.length == 0) {
                    layer.confirm('当前用户权限下没有班级，请联系管理员', function () {
                        gotourl("login.html")//跳转
                    }, function () {
                        $('#lbTaskTypeID').hide();
                        $('#TaskTypeID').hide();
                        $('#lbDisplayModelID').hide();
                        $('#DisplayModelID').hide();
                        $('#CheckVersion').hide();


                        $('#push').remove();

                    });
                    return;
                }
                $('#TaskTypeID').attr("disabled", "disabled");
                //$('#OperateTypeID').attr("disabled", "disabled");
                $('#DisplayModelID').removeAttr("disabled");
                $('#lbTaskTypeID').hide();
                $('#TaskTypeID').hide();
                $('#CheckVersion').hide();
                //$('#lbOperateTypeID').hide();
                //$('#OperateTypeID').hide();

                $('#lbDisplayModelID').show();
                $('#DisplayModelID').show();
                $('#lbMessageTitle').css("margin-left", "0");
                //加载一个班级
                $('#classHeadList').empty();
                if (res.data != null && res.data.length > 1)//添加全选按钮
                {
                    var htmlAllClass = '<label class="checkbox-inline" style="width: 120px;"><input type="checkbox" id="allclass"  name="allclass" > 全选</label></br>';
                    $('#classHeadList').append(htmlAllClass);
                }
                //循环添加班级
                $(res.data).each(function (i, o) {
                    var htmlCalss = '<label class="checkbox-inline" style="width: 120px;">';
                    htmlCalss += '<input type="checkbox"  name="class" id="class_{0}" value="{1}" title="{3}" data-Grade="{2}"> {3}</label>';
                    htmlCalss = htmlCalss.format($.toString(o.ClassCode), $.toString(o.JPushID), $.toString(o.gradeCode).substr(11, 16), $.toString(o.ClassName));
                    $('#classHeadList').append(htmlCalss);
                });


            }
            else {
                if (res.data.length == 0) {
                    layer.confirm('当前用户权限下没有班级，请联系管理员', function () {
                        gotourl("login.html")//跳转系统设置
                    }, function () {
                        $('#lbTaskTypeID').hide();
                        $('#TaskTypeID').hide();
                        $('#lbDisplayModelID').hide();
                        $('#DisplayModelID').hide();
                        $('#CheckVersion').hide();


                        $('#push').remove();

                    });
                    return;
                }
                //isheadteacher = 1;//班主任
                $('#TaskTypeID').attr("disabled", "disabled");
                //$('#OperateTypeID').attr("disabled", "disabled");
                $('#DisplayModelID').removeAttr("disabled");
                $('#lbTaskTypeID').hide();
                $('#TaskTypeID').hide();
                $('#CheckVersion').hide();
                //$('#lbOperateTypeID').hide();
                //$('#OperateTypeID').hide();

                $('#lbDisplayModelID').show();
                $('#DisplayModelID').show();
                $('#lbMessageTitle').css("margin-left", "0");
                //加载一个班级
                $('#classHeadList').empty();
                //循环添加班级
                $(res.data).each(function (i, o) {
                    var htmlCalss = '<label class="checkbox-inline" style="width: 120px;">';
                    htmlCalss += '<input type="checkbox" checked="checked" name="class" id="class_{0}" value="{1}" title="{3}" data-Grade="{2}"> {3}</label>';
                    htmlCalss = htmlCalss.format($.toString(o.ClassCode), $.toString(o.JPushID), $.toString(o.gradeCode).substr(11, 16), $.toString(o.ClassName));
                    $('#classHeadList').append(htmlCalss);
                });
            }

            //

        }
    }, url);

}
//给重新初始化参数添加班级
function getUserClassList1(userCode, rootCode) {
    var url = "/Service.ashx?actionname=01-04";
    var par = { "userCode": userCode, "rootCode": rootCode };
    EDUCAjax(par, function () { }, function (res) {
        if (res.status == 0) {
            $('#classlist div:not("#classHeadList")').remove();
            //给重新初始化参数添加班级
            if ($.toString($('#OperateTypeID').val()) == "17") {
                var htmlstr = '<option value=""></option>';
                $(res.data).each(function (i, o) {

                    htmlstr += '<option value="' + $.toString(o.ClassCode) + '">' + $.toString(o.ClassName) + '</option>';

                });
                $('#SelectClassCode').html(htmlstr);
            }
        }
    }, url);

}
//管理员获取用户年级
function getUserClassList(userCode, rootCode) {
    var url = "/Service.ashx?actionname=01-04";
    var par = { "userCode": userCode, "rootCode": rootCode };
    EDUCAjax(par, function () { }, function (res) {
        if (res.status == 0) {
            $('#classlist div:not("#classHeadList")').remove();
            var Calss = "";
            //循环添加年级
            $(res.data).each(function (i, o) {
                var htmlstr = '';
                if ($('#classlist #div_grade_' + $.toString(o.gradeCode).substr(11, 16) + '').length > 0) {

                }
                else {
                    htmlstr += '<div class="padder" id="div_grade_{0}">';
                    htmlstr += ' <label class="checkbox-inline" style="width: 90px; ">';
                    htmlstr += ' <input type="checkbox" name="grade" id="grade_{0}" title="{1}"  value="{0}">{1} </label>';
                    htmlstr += ' <div id="div_grade_{0}_classlist"></div></div>';

                    htmlstr = htmlstr.format($.toString(o.gradeCode).substr(11, 16), $.toString(o.gradeName));
                }
                $('#classlist').append(htmlstr);
            });

            //循环添加班级
            $(res.data).each(function (i, o) {
                if (!(Calss.indexOf($.toString(o.ClassCode))>-1)) {
                    var htmlCalss = '<label class="checkbox-inline" style="width: 72px;"><input type="checkbox" name="class" id="class_{0}" value="{1}"  data-Grade="{2}" title="{4}"> {3}</label>';

                    htmlCalss = htmlCalss.format($.toString(o.ClassCode), $.toString(o.JPushID), $.toString(o.gradeCode).substr(11, 16), $.toString(o.ClassName).replace($.toString(o.gradeName), ""), $.toString(o.ClassName));
                    if ($('#classlist #div_grade_' + $.toString(o.gradeCode).substr(11, 16) + ' #div_grade_' + $.toString(o.gradeCode).substr(11, 16) + '_classlist').length > 0) {
                        $('#classlist #div_grade_' + $.toString(o.gradeCode).substr(11, 16) + ' #div_grade_' + $.toString(o.gradeCode).substr(11, 16) + '_classlist').append(htmlCalss);
                        Calss += $.toString(o.ClassCode)+",";
                    }
                }
            });

            //给重新初始化参数添加班级
            if ($.toString($('#OperateTypeID').val()) == "17") {
                var htmlstr = '<option value=""></option>';
                $(res.data).each(function (i, o) {

                    htmlstr += '<option value="' + $.toString(o.ClassCode) + '">' + $.toString(o.ClassName) + '</option>';

                });
                $('#SelectClassCode').html(htmlstr);
            }
        }
    }, url);

}
var userCalssList = new Array();

//获取发送图片
function getAllImage() {
    if ($('#table_image').is(':visible') && $('#id_table_list_image tr').length > 0) {
        var imageArray = new Array();
        $('#id_table_list_image tr').each(function (i, o) {
            imageArray.push($(o).find('td').eq(0).attr("data-url"));
        });
        return JSON.stringify(imageArray);
    }
    else {
        return null;
    }
}
//获取发送图片名称
function getAllImageName() {
    if ($('#table_image').is(':visible') && $('#id_table_list_image tr').length > 0) {
        var imageNameArray = new Array();
        $('#id_table_list_image tr').each(function (i, o) {
            imageNameArray.push($(o).find('td').eq(0).html());
        });
        return JSON.stringify(imageNameArray);
    }
    else {
        return null;
    }
}
//获取发送视频
function getAllVideo() {
    if ($('#table_video').is(':visible') && $('#id_table_list_video tr').length > 0) {
        var videoArray = new Array();
        $('#id_table_list_video tr').each(function (i, o) {
            videoArray.push($(o).find('td').eq(0).attr("data-url"));
        });
        return JSON.stringify(videoArray);
    }
    else {
        return null;
    }
}
//获取发送视频名称
function getAllVideoName() {
    if ($('#table_video').is(':visible') && $('#id_table_list_video tr').length > 0) {
        var videoArray = new Array();
        $('#id_table_list_video tr').each(function (i, o) {
            videoArray.push($(o).find('td').eq(0).html());
        });
        return JSON.stringify(videoArray);
    }
    else {
        return null;
    }
}