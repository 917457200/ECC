<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm6.aspx.cs" Inherits="EastElite.ECC.WebForm6" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>推送任务</title>
    <style>
        *{
            padding:0;
            margin:0;
        }
        #classHeadList span{
            margin-right:20px;
        }
       .typeList:after{
            content:"";clear:both;display:block;
        }
        .typeList{
            zoom:1;
        }
        .typeList1:after{
            content:"";clear:both;display:block;
        }
        .typeList1{
            zoom:1;
        }
        .xiaoxiType:after{
            content:"";clear:both;display:block;
        }
        .xiaoxiType{
            zoom:1;
        }
        .xiaoximeirong:after{
            content:"";clear:both;display:block;
        }
        .xiaoximeirong{
            zoom:1;
        }
        .layeType,.handleType,.myType,.youxianji,.xiaoxi,.laiyuan{float:left;}
        .layeType,.myType,.xiaoxi{
            margin-right:50px;
        }
    </style>
    <script src="js/jquery-1.7.2.min.js"></script>
  
    <script src="js/MY97DATE/WdatePicker.js"></script>
    <script src="js/WebJS.js"></script>
    <script src="js/UploadJS.js"></script>
    <script src="js/PushJS.js"></script>
    <link href="js/Uploadify/uploadify.css" rel="stylesheet" />
      <script src="js/Uploadify/jquery.uploadify.min.js"></script>
    <script type="text/javascript">
        var loginUser;

          if (getCookie("loginUser") == null) {
              gotourl("/WebForm7.aspx");
          }
           else {
               loginUser = eval("(" + getCookie("loginUser") + ")");
           }
        var audience;
        $(function () {
            //加载所有学科类型
            getUserSubjectTypeList(loginUser.userCode);
            //加载所有班级类型
            getUserClassTypeList(loginUser.userCode);

            //加载所有班级
            getUserClassList(loginUser.userCode);
            var url = "/Service.ashx?actionname=01-01";
            //任务类型
            GetDictsByType("TaskTypeID", "TaskTypeID");
            //布局类型
            GetDictsByType("DisplayModelID", "DisplayModelID");
            //任务操作类型
            GetDictsByType("OperateTypeID", "OperateTypeID");
            //任务优先级
            GetDictsByType("TaskPriorityID", "TaskPriorityID");
            //消息类型
            GetDictsByType("MessageTypeID", "MessageTypeID");
            //消息来源
            GetDictsByType("MessageSourceID", "MessageSourceID");
            //图片显示效果类型
            GetDictsByType("ImageEffectID", "ImageEffectID");
            $('#push').click(function () {

                var DisplayModelID = $.toInt($('#DisplayModelID').val());
                var OperateTypeID = $.toInt($('#OperateTypeID').val());
                var MessageTypeID = $.toInt($('#MessageTypeID').val());
                var MessageSourceID = $.toInt($('#MessageSourceID').val());
                var TaskPriorityID = $.toInt($('#TaskPriorityID').val());
                var ImageSpanSecond = $.toInt($('#ImageSpanSecond').val());
                var ImageEffectID = $.toInt($('#ImageEffectID').val());
                var TaskStatusID = $.toInt($('#TaskStatusID').val());
                var TaskTypeID = $.toInt($('#TaskTypeID').val());
                var TaskResultID = 0;
                var TaskBeginTime = $.toString($('#stime').val()) == "" ? $.currentDate().DateTime : $.toString($('#stime').val());
                var TaskEndTime = $.toString($('#etime').val()) == "" ? $.currentDate().DateTime : $.toString($('#etime').val());
                var Note = "";
                var MessageTitle = $.toString($('#MessageTitle').val());
                var MessageContent = "";
                var RargetAlias = "";
                var CreatedID = 1;
                var CreatedName = 'hxx';
                var CreatedDate = "";
                var ModifiedID = 1;
                var ModifiedName = 'hxx';
                var ModifiedDate = "";
                var url = "/Service.ashx?actionname=01-01";

                var msgContentText = $('#msgContentText').val();

                var msgContentImage = getAllImage();
                var msgContentVideo = getAllVideo();

                var customMessageContent = CustomMessageContent().TextString(msgContentText).ImageArray(msgContentImage).VideoArray(msgContentVideo);

                MessageContent = customMessageContent;

                audience = Audience();
                if ($('#ckbAll').is(':checked')) {
                    //发送所有年级
                    audience.AddTagString();
                }
                else {
                    var isclass = true;
                    $('#classHeadList :checkbox[name=classtype]').each(function (i, o) {
                        if ($(o).is(':checked')) {
                            audience.AddTagString($(o).val());
                            isclass = false;
                        }
                    });
                    $('#classHeadList :checkbox[name=subjecttype]').each(function (i, o) {
                        if ($(o).is(':checked')) {
                            audience.AddTagString($(o).val());
                            isclass = false;
                        }
                    });

                    $('#classContentList :checkbox[name=grade]').each(function (i, o) {
                        if ($(o).is(':checked')) {
                            audience.AddAliasString($(o).val());
                            isclass = false;
                        }
                    });
                    if (isclass) {
                        $('#classContentList :checkbox[name=class]').each(function (i, o) {
                            if ($(o).is(':checked')) {
                                audience.AddRegistration_idString($(o).val());

                            }
                        });
                    }

                }



                //接收范围
                var TargetRange = audience.audience;
                var deviceTaskInfo = DeviceTaskInfo(DisplayModelID, OperateTypeID, MessageTitle, MessageTypeID, MessageSourceID,
                   MessageContent, TargetRange, RargetAlias, TaskBeginTime, TaskEndTime, TaskPriorityID, ImageSpanSecond,
                    ImageEffectID, TaskStatusID, Note, CreatedID, CreatedName, CreatedDate, ModifiedID, ModifiedName, ModifiedDate, TaskResultID, TaskTypeID);
                var message = Message(deviceTaskInfo);
                var pushPayload = PushPayload(PlatformEnum.all, audience).Message(message);
                var pushData = { "pushpayload": JSON.stringify(pushPayload), "devicetaskinfo": JSON.stringify(deviceTaskInfo), "rootCode": loginUser.rootCode };

                //EDUCAjax(JSON.stringify(aaa), function () { }, function (data) { }, url);
                EDUCAjax(pushData, function () { }, function (res) {
                    alert(res.mes);
                }, url);

            });

            //
            $.Upload("file_uploadImage", "uploadImageQueue", UploadTypeEnum.image, function (data) {

                var htmlstr = '<tr>';
                htmlstr += '<td data-url="' + data.url + '">' + data.name + '</td>';
                htmlstr += '<td><img src="' + data.thumnailUrl + '"/></td>';
                htmlstr += '<td>' + $.toString(data.width) + '*' + $.toString(data.height) + '</td>';
                htmlstr += '<td>删除</td>';
                htmlstr += '</tr>';
                $('#id_table_list_image').append(htmlstr);
            });
            $.Upload("file_uploadVideo", "uploadVideoQueue", UploadTypeEnum.video, function (data) {

                var htmlstr = '<tr>';
                htmlstr += '<td data-url="' + data.url + '">' + data.name + '</td>';
                htmlstr += '<td>' + data.size + '</td>';
                htmlstr += '<td>删除</td>';
                htmlstr += '</tr>';
                $('#id_table_list_video').append(htmlstr);
            });



            //班级选择框事件
            $('#classContentList').delegate(':checkbox[name="class"]', 'change', function () {
                var grade = $(this).attr("data-grade");
                if ($(this).is(':checked')) {

                    $('#classHeadList :checked').attr("disabled", "disabled");
                    $('#' + grade + ' :checkbox[name="grade"]').attr("disabled", "disabled");
                }
                else {
                    $('#classHeadList :checked').removeAttr("checked");
                    $('#classHeadList :checked').removeAttr("disabled");
                    $('#' + grade + ' :checkbox[name="grade"]').removeAttr("checked");
                    $('#' + grade + ' :checkbox[name="grade"]').removeAttr("disabled");
                }

                //var grade = $(this).attr("data-grade");
                ////班级控制全部
                //var isallGrade = true;
                //$('#classContentList :checkbox[name="class"]').each(function (i, o) {
                //    if (!$(o).is(':checked')) {
                //        isallGrade = false;
                //        return;
                //    }

                //});
                //if (isallGrade) {
                //    $('#ckbAll').attr("checked", 'true');
                //}
                //else {
                //    $('#ckbAll').removeAttr("checked");
                //}
                ////班级控制年级
                //var isGrade = true;
                //$('#' + grade + ' :checkbox[name="class"][data-grade="' + grade + '"]').each(function (i, o) {
                //    if (!$(o).is(':checked')) {
                //        isGrade = false;
                //        return;
                //    }
                //})

                //if (isGrade) {
                //    $('#' + grade + ' :checkbox[name="grade"]').attr("checked", 'true');
                //}
                //else {
                //    $('#' + grade + ' :checkbox[name="grade"]').removeAttr("checked");
                //}

            });

            //年级选择框事件
            $('#classContentList').delegate(':checkbox[name="grade"]', 'change', function () {
                ////年级控制全选
                //var isallGrade = true;
                //$('#classContentList :checkbox[name="grade"]').each(function (i, o) {
                //    if (!$(o).is(':checked')) {
                //        isallGrade = false;
                //        return;
                //    }
                //})
                //if (isallGrade) {
                //    $('#ckbAll').attr("checked", 'true');
                //}
                //else {
                //    $('#ckbAll').removeAttr("checked");
                //}

                ////年级控制班级
                //var grade = $(this).val();
                //if ($(this).is(':checked')) {
                //    $('#classContentList :checkbox[name="grade"]').attr("checked", 'true');
                //    $('#' + grade + ' :checkbox[name="class"][data-grade="' + grade + '"]').removeAttr("checked");
                //    $('#' + grade + ' :checkbox[name="class"][data-grade="' + grade + '"]').attr("disabled", "disabled");

                //}
                //else {
                //    $('#' + grade + ' :checkbox[name="class"][data-grade="' + grade + '"]').removeAttr("disabled");

                //  //  $('#' + grade + ' :checkbox[name="class"][data-grade="' + grade + '"]').removeAttr("checked");
                //}

            });   /**/




            //班级类型事件
            $('#classHeadList').delegate(':checkbox[name="classtype"]', 'change', function () {
                $('#classContentList :checkbox[name="class"]').removeAttr("checked");
                var classtypeid = $(this).val();
                if ($(this).is(':checked')) {
                    if ($('#ckbAll').is(':checked')) {
                        $('#ckbAll').removeAttr("checked");
                        $('#classContentList :checkbox').removeAttr("checked");
                    }

                    $('#classContentList :checkbox[name="class"]').attr("disabled", "disabled");
                    // $('#classContentList :checkbox[name="class"][data-classtypeid="' + classtypeid + '"]').attr("checked", 'true');
                }
                else {

                    $('#classContentList :checkbox[name="class"]').removeAttr("disabled");
                    //$('#classContentList :checkbox[name="class"][data-classtypeid="' + classtypeid + '"]').removeAttr("checked");
                }
            });


            //学科类型事件
            $('#classHeadList').delegate(':checkbox[name="subjecttype"]', 'change', function () {
                $('#ckbAll').removeAttr("checked");
                $('#classContentList :checkbox[name="class"]').removeAttr("checked");
                var subjecttypeid = $(this).val();
                if ($(this).is(':checked')) {

                    $('#classContentList :checkbox[name="class"]').attr("disabled", "disabled");
                    // $('#classContentList :checkbox[name="class"][data-subjecttypeid="' + subjecttypeid + '"]').attr("checked", 'true');
                }
                else {

                    $('#classContentList :checkbox[name="class"]').removeAttr("disabled");
                    // $('#classContentList :checkbox[name="class"][data-subjecttypeid="' + subjecttypeid + '"]').removeAttr("checked");
                }
            });
            //全选事件
            $('#ckbAll').change(function () {
                $(this).siblings(":checkbox").removeAttr("checked");
                $(':checkbox[name="class"]').removeAttr("checked");
                if ($(this).is(':checked')) {
                    $(':checkbox[name="grade"]').attr("checked", 'true');
                    $(':checkbox[name="class"]').attr("disabled", 'disabled');
                   
                }
                else {
                    $(':checkbox[name="grade"]').removeAttr("checked");
                    $(':checkbox[name="class"]').removeAttr("disabled");
                }
            });

            $('#TaskTypeID').change(function () {
                var TaskTypeID = $.toString($('#TaskTypeID').val());
                if (TaskTypeID == "2") {
                    $('#TaskTypeIDDiv').show();
                }
                else {
                    $('#TaskTypeIDDiv').hide();
                }

            });

        });


        //获取用户学科类型
        function getUserSubjectTypeList(userCode) {
            var url = "/Service.ashx?actionname=01-06";
            var par = { "userCode": userCode };
            EDUCAjax(par, function () { }, function (res) {
                if (res.status == 0) {
                    var htmlstr = '';
                    $(res.data).each(function (i, o) {
                        htmlstr += '<input id="{0}"  name="subjecttype" type="checkbox" value="{1}"/> <span style="font-size: 16px">' + $.toString(o.SubjectTypeIDText) + '</span>';
                        htmlstr = htmlstr.format(o.SubjectTypeID, o.SubjectTypeIDValue);
                    });
                    $('#classHeadList').append(htmlstr);
                }
            }, url);

        }
        //获取用户班级类型
        function getUserClassTypeList(userCode) {
            var url = "/Service.ashx?actionname=01-07";
            var par = { "userCode": userCode };
            EDUCAjax(par, function () { }, function (res) {
                if (res.status == 0) {
                    var htmlstr = '';
                    $(res.data).each(function (i, o) {
                        htmlstr += '<input id="{0}" name="classtype" type="checkbox" value="{1}"/> <span style="font-size: 16px">' + $.toString(o.ClassTypeIDText) + '</span>';
                        htmlstr = htmlstr.format(o.ClassTypeID, o.ClassTypeIDValue);
                    });
                    $('#classHeadList').append(htmlstr);
                }
            }, url);

        }
        //获取用户年级
        function getUserClassList(userCode) {
            var url = "/Service.ashx?actionname=01-04";
            var par = { "userCode": userCode };
            EDUCAjax(par, function () { }, function (res) {
                if (res.status == 0) {

                    //循环添加年级
                    $(res.data).each(function (i, o) {
                        var htmlstr = '';
                        if ($('#classContentList #' + o.gradeCode + '').length > 0) {

                        }
                        else {

                            htmlstr += '<div id="{0}"><input id="{0}" type="checkbox" value="{0}" name="grade"/> <span style="font-size: 16px">' + $.toString(o.gradeName) + '</span><div>';

                            htmlstr = htmlstr.format(o.gradeCode);
                        }
                        $('#classContentList').append(htmlstr);
                    });

                    //循环添加班级
                    $(res.data).each(function (i, o) {
                        var htmlCalss = '<input id="{0}" name="class" type="checkbox" data-SubjectTypeID="{1}"  data-ClassTypeID="{2}" data-Grade="{3}" value="{4}"/> <span style="font-size: 16px">' + $.toString(o.ClassName) + '</span>';
                        htmlCalss = htmlCalss.format($.toString(o.ClassCode), $.toString(o.SubjectTypeID), $.toString(o.ClassTypeID), $.toString(o.gradeCode), $.toString(o.JPushID));
                        $('#classContentList #' + o.gradeCode + '').append(htmlCalss);
                    });
                }
            }, url);

        }
        var userCalssList = new Array();

        //获取发送图片
        function getAllImage() {
            var imageArray = new Array();
            $('#id_table_list_image tr').each(function (i, o) {
                imageArray.push($(o).find('td').eq(0).attr("data-url"));
            });
            return imageArray;
        }
        //获取发送视频
        function getAllVideo() {
            var videoArray = new Array();
            $('#id_table_list_video tr').each(function (i, o) {
                videoArray.push($(o).find('td').eq(0).attr("data-url"));
            });
            return videoArray;
        }
        function doUplaodImage() {
            $('#file_uploadImage').uploadify('upload', '*');
        }

        function closeLoadImage() {
            $('#file_uploadImage').uploadify('cancel', '*');
        }

        function doUplaodVideo() {
            $('#file_uploadVideo').uploadify('upload', '*');
        }

        function closeLoadVideo() {
            $('#file_uploadVideo').uploadify('cancel', '*');
        }


    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="classList">
                <div id="classHeadList">
                    <input id="ckbAll" type="checkbox" value="0" /><span style="font-size: 16px">全部</span>
                   
                    <%--    <div id="SubjectTypeList"></div>
                <div id="ClassTypeList"></div>--%>
                </div>
                <div id="classContentList"></div>
            </div>
            <div class="typeList">
                <div class="layeType">
                    <label for="">布局类型：</label>
                    <select id="DisplayModelID">
                        <%-- <option value="1">班级模式</option>
                        <option value="2">考试模式</option>
                        <option value="3">紧急模式</option>
                        <option value="4">全屏图片模式</option>
                        <option value="5">全屏视频模式q</option>
                        <option value="6">人物评选模式</option>
                        <option value="7">课堂考勤模式</option>--%>
                    </select>
                </div>
                <div class="handleType">
                    <label for="">任务操作类型：</label>
                    <select id="OperateTypeID">
                        <%--   <option value="1">更新上次任务模式</option>
                        <option value="2">创建新建任务模式</option>
                        <option value="3">删除任务</option>--%>
                    </select>
                </div>
            </div>
            <div class="typeList1">
                <div class="myType">
                    <label for="">任务类型：</label>
                    <select id="TaskTypeID">
                        <%-- <option value="1">普通任务</option>
                    <option value="2">定时任务</option>--%>
                    </select>
                    <div id="TaskTypeIDDiv" style="display:none;">
                    <p>
                        <label for="">任务开始时间：</label>
                        <input id="stime" type="text" placeholder="请选择开始时间" onclick="WdatePicker({ dateFmt: 'yyyy-MM-dd HH:mm:ss', minDate: '%y-%M-#{%d}', maxDate: '#F{$dp.$D(\'etime\')||\'3000-10-01\'}' })" data-model="stime" />

                    </p>
                    <p class="">
                        <label for="">任务结束时间：</label>
                        <input id="etime" type="text" placeholder="请选择结束时间" onclick="WdatePicker({ dateFmt: 'yyyy-MM-dd HH:mm:ss', minDate: '#F{$dp.$D(\'stime\')}', maxDate: '3000-10-01' })" data-model="etime" />

                    </p>
                </div>
            </div>
                <div class="youxianji">
                    <label for="">任务优先级：</label>
                        <select id="TaskPriorityID">
                            <%-- <option value="1">1级</option>
                            <option value="2">2级</option>
                            <option value="3">3级</option>--%>
                    </select>
                </div>
            </div>
            
            <div class="xiaoxiType">
                <div class="xiaoxi">
                     <label for="">消息类型：</label>
                         <select id="MessageTypeID">
                        <%--    <option value="1">新闻通知</option>
                        <option value="2">课程表</option>
                        <option value="3">班级常规检查</option>--%>
                      </select>
                </div>
                <div class="laiyuan">
                    <label for="">消息来源：</label>
                    <select id="MessageSourceID">
                        <%--    <option value="1">电子班牌平台</option>
                        <option value="2">数字校园平台</option>
                        <option value="3">手机智慧校园</option>--%>
                    </select>
                </div>
            </div>

           
            
            <div >
                <div class="xiaoximeirong">
                    <p style="float:left; margin-right:30px;">
                        <label for="">消息标题：</label>
                        <input type="text" id="MessageTitle" placeholder="请输入消息标题" />
                    </p>
                    <p style="float:left;">
                        <label for=""style="float:left;">消息文本内容：</label>
                        <textarea id="msgContentText" placeholder="请输入消息文本内容" style="float:left"></textarea>
                    </p>
                </div>
                

                <div>
                    <label for="">上传资源：</label>
                     <div id="file_uploadImage"></div>
                    <div id="uploadImageQueue" style="padding: 3px;"></div>
                   

                 <%--   <img alt="" src="js/Uploadify/BeginUpload.gif" width="77" height="23" onclick="doUplaodImage()" style="cursor: pointer" />
                    <img alt="" src="js/Uploadify/CancelUpload.gif" width="77" height="23" onclick="closeLoadImage()" style="cursor: pointer" />--%>


                    <table>
                        <tr>
                            <th class="th3">名称</th>
                            <th class="th3">缩略图</th>
                            <th class="th3">原图尺寸</th>
                            <th class="th5">删除</th>
                        </tr>
                        <tbody id="id_table_list_image">
                        </tbody>
                    </table>
                </div>
                <div>
                    <div id="uploadVideoQueue" style="padding: 3px;"></div>
                    <div id="file_uploadVideo"></div>
                    <img alt="" src="js/Uploadify/BeginUpload.gif" width="77" height="23" onclick="doUplaodVideo()" style="cursor: pointer" />
                    <img alt="" src="js/Uploadify/CancelUpload.gif" width="77" height="23" onclick="closeLoadVideo()" style="cursor: pointer" />


                    <table>
                        <tr>
                            <th class="th3">名称</th>
                            <th class="th3">视频大小</th>
                            <th class="th5">删除</th>
                        </tr>
                        <tbody id="id_table_list_video">
                        </tbody>
                    </table>
                </div>
                <label for="">图片：</label>

                <label for="">图片显示秒数：</label>
                <input type="text" id="ImageSpanSecond" placeholder="请输入图片显示秒数" />
                <label for="">图片显示效果类型：</label>
                <select id="ImageEffectID">
                    <%-- <option value="1">图片显示效果类型</option>
                    <option value="2">图片显示效果类型</option>
                    <option value="3">图片显示效果类型</option>--%>
                </select>
            </div>
            <input type="button" id="push" value="推送" />








        </div>
    </form>
</body>
</html>
