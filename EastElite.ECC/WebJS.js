$(function () {
    //loading 提示
    $("body").append('<div id="id_loding" style="display: none;"><div class="background"></div><div id="id_lodingMsg" class="progressBar" style="font-size: 12px; font-family: 微软雅黑;">2222</div></div>');
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

    var loding = $("#id_loding");//loading提示
    $.ajax({
        type: "POST",
        dataType: datatype.toLowerCase(),
        url: url + "?" + Math.random(),
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
        success: function (data) {
            if (lodingState) {
                loding.hide();
            }
            callback(data);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            var XMLHttpRequestStr = XMLHttpRequest.readyState + "  " + XMLHttpRequest.status + "  " +
                XMLHttpRequest.statusText + "  " + XMLHttpRequest.responseText;
            alert("XMLHttpRequest：" + XMLHttpRequestStr + "</br>textStatus：" + textStatus + "</br>errorThrown：" + errorThrown);
        }
    });
}




//推送平台定义
if (typeof PlatformEnum == "undefined") {
    var PlatformEnum = {};
    PlatformEnum.all = { "allPlatform": "all" };
    PlatformEnum.android = { "allPlatform": null, "deviceTypes": ["android"] };
    PlatformEnum.ios = { "allPlatform": null, "deviceTypes": ["ios"] };
    PlatformEnum.Winphone = { "allPlatform": null, "deviceTypes": ["winphone"] };
    PlatformEnum.android_ios = { "allPlatform": null, "deviceTypes": ["android", "ios"] };
    PlatformEnum.android_winphone = { "allPlatform": null, "deviceTypes": ["android", "winphone"] };
    PlatformEnum.ios_winphone = { "allPlatform": null, "deviceTypes": ["ios", "winphone"] };
}

//定义推送设备对象类
function Audience() {
    // {"allAudience":null,"dictionary":{"alias":["shirdrn","Hamtty","Saxery"],"tag":["a","b","c"]}}
    var audience = {};
    audience.allAudience = "all";
   // audience.dictionary = null;
    var audienceDictionary = {};
    audience.Tag = function (tagArray) {
        audienceDictionary.tag = tagArray;
        this.allAudience = null;
        this.dictionary = audienceDictionary;
        return this;
    }
    audience.Tag_and = function (tag_andArray) {
        audienceDictionary.tag_and = tag_andArray;
        this.allAudience = null;
        this.dictionary = audienceDictionary;
        return this;
    }
    audience.Alias = function (aliasArray) {
        audienceDictionary.alias = aliasArray;
        this.allAudience = null;
        this.dictionary = audienceDictionary;
        return this;
    }
    audience.Registration_id = function (registration_idArray) {
        audienceDictionary.registration_id = registration_idArray;
        this.allAudience = null;
        this.dictionary = audienceDictionary;
        return this;
    }
    return audience;
}

//定义PushPayload
function PushPayload(platformJson, audienceJson) {
    var pushPayload = {};

    pushPayload.platform = platformJson.allPlatform != null ? platformJson.allPlatform : platformJson.deviceTypes;
    pushPayload.audience = audienceJson.allAudience != null ? audienceJson.allAudience : audienceJson.dictionary;
   
    pushPayload.Notification = function (notificationJson) {
        this.notification = notificationJson;
        return this;
    }
    pushPayload.Message = function (messageJson) {
        this.message = messageJson;
        return this;
    }
    pushPayload.Options = function (optionsJson) {
        this.options = optionsJson;
        return this;
    }
    return pushPayload;
}

//自定义消息类
function Message(deviceTaskInfoJson) {
    var message = {};
    message.title =deviceTaskInfoJson.MessageTitle;
    message.msg_content = deviceTaskInfoJson;
    
    message.Title = function (titleString) {
        this.title = titleString;
        return this;
    }
    message.Content_type = function (content_typeString) {
        this.content_type = content_typeString;
        return this;
    }
    message.Extras = function (extrasJson) {
        this.extras = extrasJson;
        return this;
    }
    return message;
}

//推送任务类
function DeviceTaskInfo(DisplayModelID, OperateTypeID, MessageTitle, MessageTypeID, MessageSourceID, MessageContent, TargetRange, RargetAlias, TaskBeginTime, TaskEndTime, TaskPriorityID, ImageSpanSecond, ImageEffectID, TaskStatusID, Note, CreatedID, CreatedName, CreatedDate,
ModifiedID, ModifiedName, ModifiedDate, TaskResultID, TaskTypeID) {
    var deviceTaskInfo = {};
    deviceTaskInfo.Code = "";
    deviceTaskInfo.DisplayModelID = DisplayModelID;
    deviceTaskInfo.OperateTypeID = OperateTypeID;
    deviceTaskInfo.MessageTitle = MessageTitle;
    deviceTaskInfo.MessageTypeID = MessageTypeID;
    deviceTaskInfo.MessageSourceID = MessageSourceID;
    deviceTaskInfo.MessageContent = MessageContent;
    deviceTaskInfo.TargetRange = TargetRange;
    deviceTaskInfo.RargetAlias = RargetAlias;
    deviceTaskInfo.TaskBeginTime = TaskBeginTime;
    deviceTaskInfo.TaskEndTime = TaskBeginTime;
    deviceTaskInfo.TaskPriorityID = TaskPriorityID;
    deviceTaskInfo.ImageSpanSecond = ImageSpanSecond;
    deviceTaskInfo.ImageEffectID = ImageEffectID;
    deviceTaskInfo.TaskStatusID = TaskStatusID;
    deviceTaskInfo.Note = Note;
    deviceTaskInfo.CreatedID = CreatedID;
    deviceTaskInfo.CreatedName = CreatedName;
    deviceTaskInfo.CreatedDate = CreatedDate;
    deviceTaskInfo.ModifiedID = ModifiedID;
    deviceTaskInfo.ModifiedName = ModifiedName;
    deviceTaskInfo.ModifiedDate = ModifiedDate;
    deviceTaskInfo.TaskResultID = TaskResultID;
    deviceTaskInfo.TaskTypeID = TaskTypeID;
    return deviceTaskInfo;
}
function CustomMessageContent() {
    var customMessageContent = {};
    customMessageContent.text = "";
    customMessageContent.image = [];
    customMessageContent.video = [];
    customMessageContent.Text = function (textString) {
        this.text = textString;
        return this;
    }
    customMessageContent.Image = function (imageArray) {
        this.image = imageArray;
        return this;
    }
    customMessageContent.Video = function (videoArray) {
        this.video = videoArray;
        return this;
    }
    return customMessageContent;
}

//待开发
function NotificationForamt() {
}


/* 

*/

//推送数据检查
function PushDataCheck() {
    var jsondata = null;
    if (arguments.length > 1) {
        var modeltype = arguments[0];
        var content = arguments[1];
        var message = arguments.length > 2 ? arguments[2] : null;
        var notification = arguments.length > 3 ? arguments[3] : null;
        var platform = arguments.length > 4 ? arguments[4] : "all";
        var audience = arguments.length > 5 ? arguments[5] : "all";


        var payload = { "platform": platform, "audience": audience, "notification": notification, "message": message };

        var aaaa = { "modelType": 1, "msgTitle": "aaaa" };
        var jsondata = { "pushpayload": JSON.stringify(payload), "devicetaskinfo": modeltype, "content": JSON.stringify(aaaa) };

    }
    return jsondata;
}

function PushMessageContentTypeCheck() {
    var content = null;
    if (arguments.length > 0) {
        var text = arguments[0];
        var images = arguments.length > 1 ? arguments[1] : null;
        var videos = arguments.length > 2 ? arguments[2] : null;
        content = { "text": text, "images": images, "videos": videos };
    }
    return content;
}
//推送消息内容检查
function PushMessageContentCheck() {
    var messageContent = null;
    if (arguments.length > 1) {
        var modeltype = arguments[0];
        var content = arguments[1];
        var msgTitle = arguments.length > 2 ? arguments[2] : "";
        var msgType = arguments.length > 3 ? arguments[3] : 0;
        var roomNum = arguments.length > 4 ? arguments[4] : "";
        var alias = arguments.length > 5 ? arguments[5] : "";
        var tags = arguments.length > 6 ? arguments[6] : "";
        var sender = arguments.length > 7 ? arguments[7] : "";
        var sourceType = arguments.length > 8 ? arguments[8] : 0;
        var receiverType = arguments.length > 9 ? arguments[9] : 0;
        var receiverIds = arguments.length > 10 ? arguments[10] : "";
        var receiver = arguments.length > 11 ? arguments[11] : "";
        var deviceSerial = arguments.length > 12 ? arguments[12] : "";

        var createDate = new Date().toLocaleString();
        messageContent = {
            "modeltype": modeltype,
            "deviceSerial": deviceSerial,
            "msgTitle": msgTitle,
            "msgType": msgType,
            "roomNum": roomNum,
            "alias": alias,
            "tags": tags,
            "sender": sender,
            "sourceType": sourceType,
            "receiverType": receiverType,
            "receiverIds": receiverIds,
            "receiver": receiver,
            "msgContent": content,
            //"platform": platform,
            //"audience": audience,
            //"notification": notification,
            //"message": message,
            "createDate": createDate
        };
    }
    return messageContent;
}

//推送消息检查
function PushMessageCheck() {
    var message = null;
    var msg_content = arguments.length > 0 ? arguments[0] : "";
    var content_type = arguments.length > 1 ? arguments[1] : "text";
    var title = arguments.length > 2 ? arguments[2] : "";
    var extras = arguments.length > 3 ? arguments[3] : null;//{"key": "value"}
    message = {
        "msg_content": msg_content,
        "content_type": content_type,
        "title": title,
        "extras": extras
    };
    return message;
}



