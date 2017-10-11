
/// <reference path="jquery-1.11.3.js" />
var layerState = false;
$(function () {
    $('#push').click(function () {
        var OperateTypeID = $.toInt($('#OperateTypeID').val());//操作类型
        var DisplayModelID = 0;
        if (OperateTypeID == 1 || OperateTypeID == 2) {
            DisplayModelID = $.toInt($('#DisplayModelID').val());//布局
        }
        else if (OperateTypeID == 4) {
            DisplayModelID = $.toInt($('#DisplayModelID').val());//布局
        }
       
        if (OperateTypeID == 2) {
            if (!layerState) {
                //信息框-例2
                layer.msg('创建会清空原有数据，确定要发布吗？', {
                    time: 0 //不自动关闭
                  , btn: ['确定', '取消']
                  , yes: function (index) {
                      layerState = true;
                      layer.close(index);
                      $('#push').click();
                  }
                });
                return;
            } else {
                layerState = false;
            }
        }
        var MessageTypeID = 1;//消息类型--新闻通知
        var MessageSourceID = 1;//消息来源电子班牌


        var TaskPriorityID = 1;//优先级
        var ImageSpanSecond = $.toInt($('#ImageSpanSecond').val());//图片显示时间
        var ImageEffectID = $.toInt($('#ImageEffectID').val());//图片显示效果
        var TaskStatusID = 1;//任务状态1--正常，2--删除
        var Title = $.toString($('#MessageTitle').val());//标题
        var Messagecontent_text = $('#msgContentText').val();//内容



        var Messagecontent_image, Messagecontent_video, Messagecontent_imagealias, Messagecontent_videoalias;
        var Messagecontent_textalias = '';//内容
        var ClsActive, ClsCheckStu;///zhc 改
        var ClsHonorList = new Array(), ClsHomeWkList = new Array(), ClsCheckItemList = new Array(), ClsNoticeList = new Array();
        Messagecontent_image = null;
        Messagecontent_video = null;
        Messagecontent_imagealias = null;

        switch ($.toInt($('#DisplayModelID').val())) {
            case 1:
                //Messagecontent_video = getAllVideo();
                //Messagecontent_imagealias = getAllImageName();
                //Messagecontent_image = getAllImage();
                ClsActive = getClsActive(ClsActive);
                ClsNoticeList = getClsNoticeList(ClsNoticeList);
                break;
            case 4:
                ClsActive = getClsActive(ClsActive);
                ClsNoticeList = getClsNoticeList(ClsNoticeList);
                break;
            case 3:
                ClsNoticeList = getClsNoticeList(ClsNoticeList);
                break;
            case 5:
                ClsActive = getClsActive(ClsActive);
                ClsNoticeList = getClsNoticeList(ClsNoticeList);
                break;
            case 9: case 10: case 11: case 12:
                ClsActive = getClsActive(ClsActive);
                ClsCheckStu = getClsCheckStu(ClsCheckStu);
                ClsHonorList = getClsHonorList(ClsHonorList);
                ClsHomeWkList = getClsHomeWkList(ClsHomeWkList);
                ClsCheckItemList = getClsCheckItemList(ClsCheckItemList);
                ClsNoticeList = getClsNoticeList(ClsNoticeList);
                break;
            default:
                Messagecontent_image = null;
                Messagecontent_video = null;
                Messagecontent_imagealias = null;
                break;
        }
        if (ClsActive == "1") {
            layer.msg("请选择活动展示!");
            return;
        }

        var TaskTypeID = $.toInt($('#TaskTypeID').val());//任务类型1--普通，2--定时

        var TaskBeginTime = $.toString($('#stime').val()) == "" ? $.currentDate().DateTime : $.toString($('#stime').val());
        var TaskEndTime = $.toString($('#etime').val()) == "" ? $.currentDate().DateTime : $.toString($('#etime').val());
        var RargetAlias = "";
        var Note = "";
        var tag_and = null;
        var registration_id = null;
        var Alias = null;//电子班牌
        var Tag = null;
        if (loginUser.roleCode != '1072')//班主任
        {
            //需要修改
            //如果选中了班级
            //if ($(':checkbox[name="class"]').is(":checked")) {
            //    if ($.toString($(':checkbox[name="class"]').eq(0).val()) != "") {
            //        tag_and = ["SC" + loginUser.schoolCode];
            //        Alias = ['U4'];
            //        registration_id = [$(':checkbox[name="class"]').eq(0).val()]


            //        RargetAlias += "" + $(':checkbox[name="class"]').eq(0).attr("title");
            //    }
            //    else {

            //        layer.msg("该班级设备未初始化");
            //        return;
            //    }
            //}
            //else {
            //    layer.msg("请选择班级");
            //    return;
            //}


            if ($(':checkbox[name="class"]:checked').length > 0) {
                registration_id = new Array();
                var isinitClass = true;
                $(':checkbox[name="class"]:checked').each(function (i, o) {
                    if ($.toString($(o).val()) != "") {
                        tag_and = ["SC" + loginUser.schoolCode];
                        Alias = ['U4'];
                        registration_id.push($.toString($(o).val()));
                        RargetAlias += "," + $(o).attr("title");
                    }
                    else {
                        isinitClass = false;
                        layer.msg($.toString($(o).attr("title")) + "设备未初始化");

                    }
                });

                if (!isinitClass) {
                    return;
                }

            }
            else {
                layer.msg("请选择班级");
                return;
            }


        }
        else//管理员
        {

            tag_and = ["SC" + loginUser.schoolCode];//--["SC1100009000", "RC1006"]学校+校区+班级类型+学科类型
            if ($('#campus').is(":visible"))//校区可见
            {
                tag_and.push("RC" + $.toString($('#campus').val()).substr(6, 9));
                RargetAlias += "," + $("#campus").find("option:selected").text();//校区名称
            }
            else {
                tag_and.push("RC" + $.toString(loginUser.rootCode).substr(6, 9));
            }
            if ($('#ClassType').is(":visible"))//班级类型可见
            {
                if ($.toString($('#ClassType').val()) != "") {
                    tag_and.push("L" + $.toString($('#ClassType').val()));
                    RargetAlias += "," + $('#ClassType').find("option:selected").text();//班级类型
                }


            }
            if ($('#SubjectType').is(":visible"))//学科类型可见
            {
                if ($.toString($('#SubjectType').val()) != "") {
                    tag_and.push("S" + $.toString($('#SubjectType').val()));
                    RargetAlias += "," + $('#SubjectType').find("option:selected").text();//班级类型
                }
            }
            //if (OperateTypeID != 13 && OperateTypeID != 4) {
            var isclass = 0;//是否按班级发送
            if ($(':checkbox[name="class"]:checked').each(function (i, o) {//班级选中
                if (!$('#grade_' + $(o).attr("data-grade")).is(":checked")) {//年级未选中
                isclass = 1;
                 return false
            }
            }))

                Alias = ['U4'];
            //RargetAlias += ",电子班牌";
            if (isclass == 1)//班级发送
            {
                registration_id = new Array();
                $(':checkbox[name="class"]:checked').each(function (i, o) {
                    if ($.toString($(o).val()) != "") {
                        registration_id.push($.toString($(o).val()));
                    }

                    RargetAlias += "," + $(o).attr("title");
                });
            }
            else {//不按班级发送，查询是否按年级发送
                if ($(':checkbox[name="grade"]:checked').length > 0) {
                    Tag = new Array();
                    $(':checkbox[name="grade"]:checked').each(function (i, o) {
                        Tag.push("G0" + $.toString($(o).val()));
                        RargetAlias += "," + $(o).attr("title");
                    });
                }
            }
            //if ($.toString($('#OperateTypeID').val()) != "17") {

            // }
            // else {
            //     registration_id = new Array();
            //     $(':checkbox[name="class"]:checked').each(function (i, o) {
            //         if ($.toString($(o).val()) != "") {
            //             registration_id.push($.toString($(o).val()));
            //             RargetAlias += "," + $(o).attr("title");
            //         }
            //     });
            // }
        }


        if ($.toString($('#OperateTypeID').val()) == "8") {
            if ($.toString($('#Timingstime').val()) != "" && $.toString($('#Timingetime').val()) != "") {
                Messagecontent_text = "boot:" + $.toString($('#Timingstime').val()) + ",shutdown:" + $.toString($('#Timingetime').val());
            }
            else {
                layer.msg("请检查开机关机时间");
                return;
            }
        }
        else if ($.toString($('#OperateTypeID').val()) == "9") {
            Messagecontent_text = 'navigationshow:' + $.toString($('#Navigationshow').val());
        }
        else if ($.toString($('#OperateTypeID').val()) == "16") {
            Messagecontent_text = 'startprogramswitchstaus:' + $.toString($('#StartProgramSwitchShow').val());
        }
        else if ($.toString($('#OperateTypeID').val()) == "17") {
            $(':checkbox[name="class"]:checked').each(function (i, o) {
                if ($.toString($(o).val()) != "") {
                    Messagecontent_text += ",'" + $.toString($(o).val()) + "'";
                }
            });
        }

        Messagecontent_text = $.toTrimComma(Messagecontent_text);
        var url = "/Service.ashx?actionname=01-44";
        var par = {
            "MessageSourceID": MessageSourceID,
            "MessageTypeID": MessageTypeID,
            "OperateTypeID": OperateTypeID,
            "DisplayModelID": DisplayModelID,
            "CreatedID": loginUser.userCode,
            "CreatedName": loginUser.userName,
            "Rootcode": loginUser.rootCode,
            "TaskBeginTime": TaskBeginTime,
            "TaskEndTime": TaskEndTime,
            "TaskPriorityID": TaskPriorityID,
            "ImageSpanSecond": ImageSpanSecond,
            "ImageEffectID": ImageEffectID,
            "TaskStatusID": TaskStatusID,
            "Title": Title,
            "Messagecontent_text": Messagecontent_text,
            "Messagecontent_image": Messagecontent_image,
            "Messagecontent_video": Messagecontent_video,
            "Messagecontent_textalias": Messagecontent_textalias,
            "Messagecontent_imagealias": Messagecontent_imagealias,
            "Messagecontent_videoalias": Messagecontent_videoalias,
            "RargetAlias": $.toTrimComma(RargetAlias),
            "tag_and": tag_and != null ? JSON.stringify(tag_and) : null,
            "Tag": Tag != null ? JSON.stringify(Tag) : null,
            "Alias": Alias != null ? JSON.stringify(Alias) : null,
            "registration_id": registration_id != null ? JSON.stringify(registration_id) : null,
            "TaskTypeID": TaskTypeID,
            "ClsActive": JSON.stringify(ClsActive),
            "ClsCheckStu": JSON.stringify(ClsCheckStu),
            "ClsHonor": JSON.stringify(ClsHonorList),
            "ClsHomeWk": JSON.stringify(ClsHomeWkList),
            "ClsCheckItem": JSON.stringify(ClsCheckItemList),
            "ClsNotice": JSON.stringify(ClsNoticeList)
        }
        EDUCAjax(par, function () { }, function (res) {
            if (res.status == "0") {
                var Note = $('#DisplayModelID option:selected').attr("note");
                MpTab(Note);
                layer.msg("发布成功");
            }
            else {
                layer.msg("发布失败");
            }

        }, url);
    });
    $(".mt20 .btn-info").click(function () {
        $(this).addClass("btn-primary").siblings().removeClass("btn-primary");
        $(".MpTab").eq($(this).index()).show().siblings().hide()
    });

    $('#DisplayModelID').change(function () {
        switch ($.toInt($(this).val())) {
            case 1: case 3: case 9: case 10: case 11: case 12: case 4: case 5:
                $('#resourse').hide(); $('#idvideo').hide(); $('#divimage').hide(); $('#divimageectf').hide();
                $('.template').show(); $(".MessageContentText").hide();
                break;
            case 2: case 6: case 7: case 8:
            default:
                $('#resourse').hide(); $('#idvideo').hide(); $('#divimage').hide(); $('#divimageectf').hide(); $('.template').hide(); $(".MessageContentText").show();
                break;
        }
        var Note = $('#DisplayModelID option:selected').attr("note");
        MpTab(Note);
    });

})
function MpTab(templateId) {
    if (templateId != "") {
        var Ids = templateId.split(",");
        $(".mt20 .btn-info").hide();
        $(".MpTab").hide();
        var par = {
            "Note": templateId
        };
        var url = "/Service.ashx?actionname=02-03";
        EDUCAjax(par, function () { }, function (rs) {
            if (rs.status == "0") {
                var show = true;
                for (var i = 0; i < rs.Data.length; i++) {
                    $(".mt20 .btn-info").eq(parseInt(rs.Data[i].ItemKey)).html(rs.Data[i].ItemValue).css("display", "inline-block");
                    var MpTab = $(".MpTab" + (parseInt(rs.Data[i].ItemKey) + 1));
                    if (show) {
                        MpTab.show();
                        show = false;
                    }
                    switch (rs.Data[i].Note) {
                        case "ClsActive":
                            MpTab1Onload(MpTab);
                            break;
                        case "ClsHonor":
                            MpTab2Onload(MpTab);
                            break;
                        case "ClsHomeWk":
                            MpTab3Onload();
                            break;
                        case "ClsCheckItem":
                            MpTab4Onload();
                            break;
                        case "ClsCheckStu":
                            MpTab5Onload();
                            break;
                        case "ClsNotice":
                            MpTab6Onload();
                            break;
                        default:
                            break;
                    }
                }
            }
        }, url);
    }
}
function MpTab1Onload(obj) {

    var Html = "";
    Html += "<div class=\"padder\">";
    Html += "<label id=\"lbClsActiveKind\">活动展示</label>";
    Html += "<select id=\"ClsActiveKind\">";
    Html += "<div class=\"padder\">";
    Html += " <option value=\"\">请选择</option>";
    if ($('#DisplayModelID').val() != "5") {
        Html += " <option value=\"1\">图片</option>";
    }
    if ($('#DisplayModelID').val() != "4") {
        Html += "<option value=\"2\">视频</option>";
    }
    Html += "</select>";
    Html += "<span style=\"display:none;\" id=\"taskdatetime\"></span>";
    Html += "</div>";
    //Html += "<div class=\"padder\">";
    //Html += "<label id=\"lbClsActivedate\" for=\"\">活动时间</label>";
    //Html += "<input id=\"ClsActivedate\" class=\"inputText\" type=\"text\" readonly placeholder=\"请选择活动时间\" onfocus=\"WdatePicker({ skin: 'whyGreen', dateFmt: 'yyyy-MM-dd' })\" />";
    //Html += "</div>";
    Html += "<div class=\"padder\">";
    Html += " <label id=\"lbClsActiveUrl\" for=\"\">活动上传</label>";
    Html += " <label id=\"ClsActiveUrl\" class=\"dn\" clsactiveurl=\"\"></label>";
    Html += "<div class=\"Image\" style=\"width:590px\">";
    Html += "<div id=\"file_ClsActiveImageUrl\" style=\"margin:0px;display:inline-block\"></div>";
    Html += "<div id=\"uploadClsActiveImageUrl\" style=\"margin-top: 0;float:left;width:250px;\"></div>";
    Html += "</div>";
    Html += "<div class=\"Vido dn\">";
    Html += "<div id=\"file_ClsActiveVidoUrl\" style=\"margin:0px;display:inline-block\"></div>";
    Html += " <div id=\"uploadClsActiveVidoUrl\" style=\"margin-top: 0;float:left;width:250px;\"></div>";
    Html += "</div>";
    Html += "</div>";
    Html += "<div class=\"padder\">";
    Html += "<label id=\"lbClsActiveContext\" class=\"fl\" for=\"\">活动内容</label>";
    Html += "<textarea id=\"ClsActiveContext\" rows=\"4\" cols=\"80\" placeholder=\"请输入消息文本内容\"></textarea>";
    Html += "<span style=\"display:none;\" id=\"taskdatetime\"></span>";
    Html += "</div>";
    obj.html(Html);

    $.Upload("file_ClsActiveImageUrl", "uploadClsActiveImageUrl", UploadTypeEnum.image, true, function (data) {
        $('#file_ClsActiveImageUrl').before("<img  data-url=\"" + data.url + "\" src=\"" + data.thumnailUrl + "\"/>");
        var clsactiveurl = $('#ClsActiveUrl').attr("clsactiveurl");
        if (clsactiveurl == "") {
            var clsactiveurl = data.url;
        } else {
            var clsactiveurl = clsactiveurl + "@&&@" + data.url;
        }
        $('#ClsActiveUrl').attr("clsactiveurl", clsactiveurl);
    });
    $.Upload("file_ClsActiveVidoUrl", "uploadClsActiveVidoUrl", UploadTypeEnum.video, false, function (data) {
        $('.Vido').html("<span data-url=\"" + data.url + "\">" + data.name + "</span>");
        $('#ClsActiveUrl').attr("clsactiveurl", data.url);
    });
    $("#ClsActiveKind").change(function () {
        $(".Image").hide();
        $(".Vido").hide();
        switch ($(this).val()) {
            case "1":
                var url = "";
                var img = $('.Image img');
                for (var i = 0; i < img.length; i++) {
                    url += $(img[i]).attr("data-url") + "@&&@";
                }
                $('#ClsActiveUrl').attr("clsactiveurl", url.length > 0 ? url.substring(0, url.length - 4) : "");
                $(".Image").show();
                break;
            case "2":
                var url = $('.Vido span') == undefined ? "" : $('.Vido span').attr("data-url");
                $('#ClsActiveUrl').attr("clsactiveurl", url);
                $(".Vido").css("display", "inline-block");
                break;
            default:
                break;
        }
    })
}
function MpTab2Onload(obj) {
    var Html = "";
    Html += "<div class=\"padder\">";
    Html += "<table width=\"100%\" class=\"Honor\" style=\"margin-top:5px;\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">";
    Html += "<thead>";
    Html += "<tr>";
    Html += "<th align=\"center\">获奖类型</th>";
    Html += "<th align=\"center\">获奖名称</th>";
    Html += "<th align=\"center\">获奖等级</th>";
    Html += "<th align=\"center\">获奖时间</th>";
    Html += "<th align=\"center\">获奖感言</th>";
    Html += "<th align=\"center\">奖章图片</th>";
    Html += "<th align=\"center\">操作</th>";
    Html += "</tr>";
    Html += "</thead>";
    Html += "<tbody></tbody>";
    Html += "</table>";
    Html += "<div style=\"margin-top: 10px; height:20px; \">";
    Html += " <div class=\" btn btn-info btn-sm \" onclick=\"AddHonor()\" style=\"padding: 0px 5px; float: right; \">添加荣誉</div>";
    Html += "</div>";
    obj.html(Html);

    AddHonor();

}
function MpTab3Onload() {
    $(".HomeWk tbody").html("");
    AddHomeWk();
}
function MpTab4Onload() {
    $(".CheckItem tbody").html("");
    AddCheckItem();
}
function MpTab5Onload() {
    var MpTab = $(".MpTab5");
    var Html = "";
    Html += " <div class=\"padder\">";
    Html += "<label id=\"lbClsCheckStudate\" for=\"\">考勤时间</label>";
    Html += "<input id=\"ClsCheckStudate\" class=\"inputText\" type=\"text\" readonly placeholder=\"请选择考勤时间\" onfocus=\"WdatePicker({skin:'whyGreen', datefmt :'yyyy-MM-dd'})\" />";
    Html += "</div>";
    Html += "<div class=\"padder\">";
    Html += "<label id=\"lbClsCheckStuSumNum\" for=\"\">全体人数</label>";
    Html += "<input id=\"ClsCheckStuSumNum\" class=\"inputText\" onkeyup=\"isInteger(this)\" type=\"text\" placeholder=\"请输入全体人数\" />";
    Html += "</div>";
    Html += "<div class=\"padder\">";
    Html += "<label id=\"lbClsCheckStuActualNum\" for=\"\">今日实到</label>";
    Html += "<input id=\"ClsCheckStuActualNum\" class=\"inputText\" onkeyup=\"isInteger(this)\" type=\"text\" placeholder=\"请输入今日实到\" />";
    Html += "</div>";
    Html += "<div class=\"padder\">";
    Html += "<label id=\"lbClsCheckStuMonAbsentNum\" for=\"\">早操缺勤</label>";
    Html += "<input id=\"ClsCheckStuMonAbsentNum\" class=\"inputText\" onkeyup=\"isInteger(this)\" type=\"text\" placeholder=\"请输入早操缺勤\" />";
    Html += "</div>";
    Html += "<div class=\"padder\">";
    Html += "<label id=\"lbClsCheckStuaftAbsentNum\" for=\"\">午操缺勤</label>";
    Html += "<input id=\"ClsCheckStuaftAbsentNum\" class=\"inputText\" onkeyup=\"isInteger(this)\" type=\"text\" placeholder=\"请输入午操缺勤\" />";
    Html += "</div>";
    Html += "<div class=\"padder\">";
    Html += "<label id=\" lbClsCheckStuLateNum\" for=\"\">今日迟到</label>";
    Html += "<input id=\"ClsCheckStuLateNum\" class=\"inputText\" onkeyup=\"isInteger(this)\" type=\"text\" placeholder=\"请输入今日迟到\" />";
    Html += "</div>";
    Html += "<div class=\"padder\">";
    Html += "<label id=\"lbClsCheckStuAbsentNum\" for=\"\">今日旷课</label>";
    Html += "<input id=\"ClsCheckStuAbsentNum\" class=\"inputText\" onkeyup=\"isInteger(this)\" type=\"text\" placeholder=\"请输入今日旷课\" />";
    Html += "</div>";
    MpTab.html(Html)
}
function MpTab6Onload() {
    $(".MpTabOh").html("");
    AddNotice();
}
//验证数字 num
function isInteger(objid) {
    reg = /[^\-?\d]/g;
    var obj = $(objid);
    if (reg.test(obj)) {
        obj.val((obj.val().replace(reg, "")));

    }
}
//班级荣誉列表操作
var TableCont = 0;
function AddHonor() {
    TableCont++;
    var TableTr = "";
    TableTr += "<tr>";
    TableTr += "<td align=\"center\">";
    TableTr += "<select class=\"ClsHonorKind\" style=\"width:68px;\">";
    TableTr += "<option value=\"\">请选择</option>";

    TableTr += "<option value=\"1\">个人</option>";
    TableTr += "<option value=\"2\">班级</option>";
    TableTr += "</select>";
    TableTr += "</td>";
    TableTr += "<td align=\"center\">";
    TableTr += "<input class=\"inputText ClsHonorPrise\" type=\"text\" placeholder=\"请输入获奖名称\" style=\"width:110px;\" />";
    TableTr += "</td>";
    TableTr += "<td align =\"center\">";
    TableTr += "<input class=\"inputText ClsHonorRank\" type=\"text\" placeholder=\"请输入获奖等级\" style=\"width:110px;\" />";
    TableTr += "</td>";
    TableTr += "<td align=\"center\">";
    TableTr += "<input class=\"inputText ClsHonorDate\" type=\"text\" style=\"width:100px;\" readonly placeholder=\"请选择获奖时间\" onfocus=\"WdatePicker({skin:'whyGreen', datefmt :'yyyy-mm-dd'})\" />";
    TableTr += "</td>";
    TableTr += "<td align=\"center\">";
    TableTr += "<textarea class=\"ClsHonorSummary\" maxlength=\"500\" style=\"width:120px;height: 50px;\" placeholder=\"请输入获奖感言\"></textarea>";
    TableTr += "<div class=\"ClsHonorUrl dn\" clshonorurl=\"\">";
    TableTr += "</div>";
    TableTr += "</td>";
    TableTr += "<td align=\"center\">";

    TableTr += "<div id=\"file_ClsHonorImageUrl" + TableCont + "\" class=\"uploadify\" style=\"height: 25px; width: 80px;\">";
    TableTr += "</div>";
    TableTr += "</td>";
    TableTr += "<td align=\"center\">";
    TableTr += " <span class=\" btn btn-info btn-sm \" onclick=\"Nexttable(this)\" style=\"padding: 0px 5px;\">上移</span>";
    TableTr += " <span class=\" btn btn-info btn-sm \" onclick=\"Uptable(this)\" style=\"padding: 0px 5px;\">下移</span>";
    TableTr += "<span class=\" btn btn-info btn-sm \" onclick=\"Deltable(this)\" style=\"padding: 0px 5px;\">删除</span>";

    TableTr += "</td>";
    TableTr += "</tr>";
    $(".Honor tbody").append(TableTr);


    $.Upload("file_ClsHonorImageUrl" + TableCont, "uploadClsHonorImageUrl", UploadTypeEnum.image, false, function (data) {
        $("#" + data.control).parent().prev().children("div").attr("clsactiveurl", data.url);
        $("#" + data.control).parent().html("<img data-url=\"" + data.url + "\" src=\"" + data.thumnailUrl + "\"/>");
    });
}
//作业布置列表操作
function AddHomeWk() {
    var TableTr = "";
    TableTr += "<tr>";
    TableTr += "<td align=\"center\">";
    TableTr += "<input type=\"text\" onfocus=\"WdatePicker({skin:'whyGreen', datefmt :'yyyy-mm-dd'})\" placeholder=\"请选择布置时间\" readonly=\"\" style=\"width:110px;\" class=\"inputText ClsHomeWkdate\">";
    TableTr += "</td>";
    TableTr += "<td align=\"center\">";
    TableTr += "<input type=\"text\" placeholder=\"请输入作业类别\" style=\"width:110px;\" class=\"inputText ClsHomekind\">";
    TableTr += "</td>";
    TableTr += "<td align=\"center\">";
    TableTr += "<input type=\"text\" placeholder=\"请输入作业学科\" style=\"width:110px;\" class=\"inputText ClsHomeSubject\">";
    TableTr += "</td>";
    TableTr += "<td align=\"center\">";
    TableTr += "<textarea placeholder=\"请输入作业内容\" style=\"width:325px;height: 50px;\" maxlength=\"1000\" class=\"ClsHomecontext\"></textarea>";
    TableTr += "</td>";
    TableTr += "<td align=\"center\">";
    TableTr += "<span class=\" btn btn-info btn-sm \" onclick=\"Nexttable(this)\" style=\"padding: 0px 5px;\">上移</span> <span class=\" btn btn-info btn-sm \" onclick=\"Uptable(this)\" style=\"padding: 0px 5px;\">下移</span>";
    TableTr += "<span class=\" btn btn-info btn-sm \" onclick=\"Deltable(this)\" style=\"padding: 0px 5px;\">删除</span> ";

    TableTr += "</td>";
    TableTr += "</tr>";
    $(".HomeWk tbody").append(TableTr);
}
//指标检查考勤列表操作
function AddCheckItem() {
    var TableTr = "";
    TableTr += "<tr>";
    TableTr += "<td align=\"center\">";
    TableTr += "<input type=\"text\" class=\"inputText ClsCheckItemdate\" style=\"width:150px;\" readonly=\"\" placeholder=\"请选择检查日期\" onfocus=\"WdatePicker({skin:'whyGreen', datefmt :'yyyy-mm-dd'})\">";
    TableTr += "</td>";
    TableTr += "<td align=\"center\">";
    TableTr += "<input type=\"text\" class=\"inputText ClsCheckItemcheck\" style=\"width:310px;\" placeholder=\"请输入检查项目\">";
    TableTr += "</td>";
    TableTr += "<td align=\"center\">";
    TableTr += "<input type=\"text\" placeholder=\"请输入分数\" style=\"width:150px;\" onkeyup=\"isInteger(this)\" class=\"inputText ClsCheckItemScore\">";
    TableTr += "</td>";
    TableTr += "<td align=\"center\">";
    TableTr += " <span style=\"padding: 0px 5px;\" onclick=\"Nexttable(this)\" class=\" btn btn-info btn-sm \">上移</span>";
    TableTr += " <span style=\"padding: 0px 5px;\" onclick=\"Uptable(this)\" class=\" btn btn-info btn-sm \">下移</span>";
    TableTr += "<span style=\"padding: 0px 5px;\" onclick=\"Deltable(this)\" class=\" btn btn-info btn-sm \">删除</span>";

    TableTr += "</td>";
    TableTr += "</tr>";
    $(".CheckItem tbody").append(TableTr);
}
//公告列表操作
var Notice = 0;
function AddNotice() {
    var TableTr = "";
    TableTr += "<table width=\"100%\" class=\"ClsNotice\" style=\"margin-top:5px;\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">";
    TableTr += "<tbody>";
    TableTr += "<tr>";
    TableTr += "<td align=\"center\" width=\"80\">公告标题</td>";
    TableTr += "<td align=\"left\"><input type=\"text\" placeholder=\"请输入公告标题\" style=\"width: 550px;\" class=\"inputText ClsNoticeTitle\"></td>";
    TableTr += "</tr>";
    TableTr += "<tr>";
    TableTr += "<td align=\"center\">公告时间</td>";
    TableTr += "<td align=\"left\"><input type=\"text\" onfocus=\"WdatePicker({skin:'whyGreen', datefmt :'yyyy-mm-dd'})\"  readonly=\"\" placeholder=\"请选择公告时间\" style=\"width:210px;\" class=\"inputText ClsNoticDate\"></td>";
    TableTr += "</tr>";
    TableTr += "<tr>";
    TableTr += "<td align=\"center\">公告内容</td>";
    TableTr += "<td align=\"left\">";
    TableTr += "<textarea id=\"d_content" + Notice + "\" placeholder=\"请输入公告内容\" style=\"width:550px;height:200px\" class=\"ClsNoticContext\" > </textarea>";
    //TableTr += "<iframe id=\"EWebEditor" + Notice + "\" src=\"/ewebeditor/ewebeditor.htm?id=d_content" + Notice + "&style=coolblue\" frameborder=\"0\" scrolling=\"no\" width=\"550\" height=\"380px\"></iframe>";
    TableTr += "</td>";
    TableTr += "</tr>";
    TableTr += "</tbody>";
    TableTr += "</table>";
    Notice++;
    $(".MpTabOh").append(TableTr);
}
//删除
function Deltable(obj) {
    $(obj).parent().parent().remove();
}
//下移
function Nexttable(obj) {
    var Down = $(obj).parent().parent();
    Down.after(Down.prev());
}
//上移
function Uptable(obj) {
    var Up = $(obj).parent().parent();
    Up.before(Up.next());
}

//班级活动加载
function getClsActive(ClsActive) {
    var Kind = $.toString($("#ClsActiveKind").val());
    //var date = $.toString($("#ClsActivedate").val());
    var context = $.toString($("#ClsActiveContext").val());
    var url = $("#ClsActiveUrl").attr("clsactiveurl");
    if (Kind == "" && (url != "" || context != "")) {
        return "1";
    }
    if (Kind != "" || context != "" || url != "") {
        ClsActive = {
            kind: Kind,
            //date: date,
            context: context,
            url: url,
        }
    }
    return ClsActive;
}
//班级荣誉加载
function getClsHonorList(ClsHonorList) {
    var ClsHonorListTable = $(".Honor tbody tr");
    for (var i = 0; i < ClsHonorListTable.length; i++) {
        var date = $.toString($(".ClsHonorDate")[i].value);
        var kind = $.toString($(".ClsHonorKind")[i].value);
        var prise = $.toString($(".ClsHonorPrise")[i].value);
        var rank = $.toString($(".ClsHonorRank")[i].value);
        var summary = $.toString($(".ClsHonorSummary")[i].value);
        var url = $.toString($($(".ClsHonorUrl")[i]).attr("clsactiveurl"));
        if (date != "" || kind != "" || prise != "" || rank != "" || summary != "" || url != "") {
            var ClsHonor = {
                "date": date,
                "kind": kind,
                "prise": prise,
                "rank": rank,
                "summary": summary,
                "url": url
            }
            ClsHonorList.push(ClsHonor);
        }
    }
    return ClsHonorList;
}
//作业布置加载
function getClsHomeWkList(ClsHomeWkList) {
    var ClsHomeWkListTable = $(".HomeWk tbody tr");
    for (var i = 0; i < ClsHomeWkListTable.length; i++) {
        var date = $.toString($(".ClsHomeWkdate")[i].value);
        var kind = $.toString($(".ClsHomekind")[i].value);
        var subject = $.toString($(".ClsHomeSubject")[i].value);
        var context = $.toString($(".ClsHomecontext")[i].value);
        if (date != "" || kind != "" || subject != "" || context != "") {
            var ClsHomeWk = {
                "date": date,
                "kind": kind,
                "subject": subject,
                "context": context
            }
            ClsHomeWkList.push(ClsHomeWk);
        }
    }
    return ClsHomeWkList;
}
//指标检查考勤加载
function getClsCheckItemList(ClsCheckItemList) {
    var ClsCheckItemListTable = $(".CheckItem tbody tr");
    for (var i = 0; i < ClsCheckItemListTable.length; i++) {
        var date = $.toString($(".ClsCheckItemdate")[i].value);
        var checkItem = $.toString($(".ClsCheckItemcheck")[i].value);
        var itemScore = $.toString($(".ClsCheckItemScore")[i].value);
        if (date != "" || checkItem != "" || itemScore != "") {
            var ClsCheckItem = {
                "date": date,
                "checkItem": checkItem,
                "itemScore": itemScore
            }
            ClsCheckItemList.push(ClsCheckItem);
        }
    }
    return ClsCheckItemList;
}
//学生出勤考勤
function getClsCheckStu(ClsCheckStu) {
    var date = $.toString($("#ClsCheckStudate").val());
    var sumNum = $.toString($("#ClsCheckStuSumNum").val());
    var absentNum = $.toString($("#ClsCheckStuAbsentNum").val());
    var lateNum = $.toString($("#ClsCheckStuLateNum").val());
    var actualNum = $.toString($("#ClsCheckStuActualNum").val());
    var monAbsentNum = $.toString($("#ClsCheckStuMonAbsentNum").val());
    var aftAbsentNum = $.toString($("#ClsCheckStuaftAbsentNum").val());

    if (date != "" || sumNum != "" || absentNum != "" || lateNum != "" || actualNum != "" || monAbsentNum != "" || aftAbsentNum != "") {
        ClsCheckStu = {
            date: date,
            sumNum: sumNum,
            absentNum: absentNum,
            lateNum: lateNum,
            actualNum: actualNum,
            monAbsentNum: monAbsentNum,
            aftAbsentNum: aftAbsentNum,
        }
    }
    return ClsCheckStu;
}
String.prototype.trim = function () {
    return this.replace(/(^\s*)|(\s*$)/g, "");
}
//公告加载
function getClsNoticeList(ClsNoticeList) {
    var ClsCheckItemListTable = $(".MpTabOh table");
    for (var i = 0; i < ClsCheckItemListTable.length; i++) {
        var Title = $.toString($(".ClsNoticeTitle")[i].value);
        var Date = $.toString($(".ClsNoticDate")[i].value);
        var Context = $.toString($(".ClsNoticContext")[i].value).trim();
        if (Title != "" || Date != "" || Context != "") {
            var ClsNotice = {
                "noticeTitle": Title,
                "noticeTime": Date,
                "noticeContent": Context
            }
            ClsNoticeList.push(ClsNotice);
        }
    }
    return ClsNoticeList;
}
