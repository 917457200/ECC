

//推送平台定义
if (typeof PlatformEnum == "undefined") {
    var PlatformEnum = {};
    PlatformEnum.all = "all";
    PlatformEnum.android = ["android"];
    PlatformEnum.ios = ["ios"];
    PlatformEnum.Winphone = ["winphone"];
    PlatformEnum.android_ios = ["android", "ios"];
    PlatformEnum.android_winphone = ["android", "winphone"];
    PlatformEnum.ios_winphone = ["ios", "winphone"];
}

//定义推送设备对象类
function Audience() {
    var audience = {};
    audience.audience = "all";
    var audienceDictionary = {};
    audienceDictionary.Concat = function () {
        var dictionaryStr = "";
        dictionaryStr = JSON.stringify(this).replace(/],/g, ']\n');
        return dictionaryStr;
    };

   
    audience.AddTagString = function (tagString) {

        if (audienceDictionary.hasOwnProperty("tag")) {
            audienceDictionary.tag.push(tagString);
        }
        else {
            audienceDictionary.tag = [tagString];
        }

        this.audience = audienceDictionary.Concat();

        return this;
    };
    audience.RemoveTagString = function (tagString) {

        if (audienceDictionary.hasOwnProperty("tag")) {
            audienceDictionary.tag.remove(tagString);
        }
       
        this.audience = audienceDictionary.Concat();

        return this;
    };

    audience.AddTagArray = function (tagArray) {

        if (audienceDictionary.hasOwnProperty("tag")) {
            audienceDictionary.tag = audienceDictionary.tag.concat(tagArray);
        }
        else {
            audienceDictionary.tag = tagArray;
        }

        this.audience = audienceDictionary.Concat();

        return this;
    };
    audience.RemoveTagArray = function (tagArray) {

        if (audienceDictionary.hasOwnProperty("tag")) {
            for (var i = 0; i < tagArray.length; i++) {
                audienceDictionary.tag.remove(tagArray[i]);
            }
        }
      
        this.audience = audienceDictionary.Concat();

        return this;
    };
   
    audience.AddTag_andString = function (tag_andString) {

        if (audienceDictionary.hasOwnProperty("tag_and")) {
            audienceDictionary.tag_and.push(tag_andString);
        }
        else {
            audienceDictionary.tag_and = [tag_andString];
        }

        this.audience = audienceDictionary.Concat();

        return this;
    };
    audience.RemoveTag_andString = function (tag_andString) {

        if (audienceDictionary.hasOwnProperty("tag_and")) {
            audienceDictionary.tag_and.remove(tag_andString);
        }
        
        this.audience = audienceDictionary.Concat();

        return this;
    };
    audience.AddTag_andArray = function (tag_andArray) {
        if (audienceDictionary.hasOwnProperty("tag_and")) {
            audienceDictionary.tag_and = audienceDictionary.tag_and.concat(tag_andArray);
        }
        else {
            audienceDictionary.tag_and = tag_andArray;
        }

        this.audience = audienceDictionary.Concat();

        return this;
    };
    audience.RemoveTag_andArray = function (tag_andArray) {
        if (audienceDictionary.hasOwnProperty("tag_and")) {
            for (var i = 0; i < tag_andArray.length; i++) {
                audienceDictionary.tag_and.remove(tag_andArray[i]);
            }
        }
        this.audience = audienceDictionary.Concat();
        return this;
    };
    audience.AddAliasString = function (aliasString) {

        if (audienceDictionary.hasOwnProperty("alias")) {
            audienceDictionary.alias.push(aliasString);
        }
        else {
            audienceDictionary.alias = [aliasString];
        }

        this.audience = audienceDictionary.Concat();

        return this;
    };
    audience.RemoveAliasString = function (aliasString) {

        if (audienceDictionary.hasOwnProperty("alias")) {
            audienceDictionary.alias.remove(aliasString);
        }
        this.audience = audienceDictionary.Concat();

        return this;
    };
    audience.AddAliasArray = function (aliasArray) {
        if (audienceDictionary.hasOwnProperty("alias")) {
            audienceDictionary.alias = audienceDictionary.alias.concat(aliasArray);
        }
        else {
            audienceDictionary.alias = aliasArray;
        }

        this.audience = audienceDictionary.Concat();
        return this;
    };
    audience.RemoveAliasArray = function (aliasArray) {
        if (audienceDictionary.hasOwnProperty("alias")) {
            for (var i = 0; i < aliasArray.length; i++) {
                audienceDictionary.alias.remove(aliasArray[i]);
            }
        }
        this.audience = audienceDictionary.Concat();
        return this;
    };
    audience.AddRegistration_idString = function (registration_idString) {

        if (audienceDictionary.hasOwnProperty("registration_id")) {
            audienceDictionary.registration_id.push(registration_idString);
        }
        else {
            audienceDictionary.registration_id = [registration_idString];
        }

        this.audience = audienceDictionary.Concat();

        return this;
    };
    audience.RemoveRegistration_idString = function (registration_idString) {
        if (audienceDictionary.hasOwnProperty("registration_id")) {
            audienceDictionary.registration_id.remove(registration_idString);
        }
        this.audience = audienceDictionary.Concat();

        return this;
    };
    audience.AddRegistration_idArray = function (registration_idArray) {
        if (audienceDictionary.hasOwnProperty("registration_id")) {
            audienceDictionary.registration_id = audienceDictionary.registration_id.concat(aliasArray);
        }
        else {
            audienceDictionary.registration_id = registration_idArray;
        }
        this.audience = audienceDictionary.Concat();
        return this;
    }
    audience.RemoveRegistration_idArray = function (registration_idArray) {
        if (audienceDictionary.hasOwnProperty("registration_id")) {
            for (var i = 0; i < registration_idArray.length; i++) {
                audienceDictionary.registration_id.remove(registration_idArray[i]);
            }
        }
        this.audience = audienceDictionary.Concat();
        return this;
    }
    return audience;
}

//定义PushPayload
function PushPayload(platformEnum, audienceJson) {
    var pushPayload = {};

    pushPayload.platform = platformEnum;
    pushPayload.audience = audienceJson.audience;

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
    message.title = deviceTaskInfoJson.MessageTitle;
    message.Content_type = "text";
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
function DeviceTaskInfo(DisplayModelID, OperateTypeID, MessageTitle, MessageTypeID, MessageSourceID, MessageContentJsonString, TargetRange, RargetAlias, TaskBeginTime, TaskEndTime, TaskPriorityID, ImageSpanSecond, ImageEffectID, TaskStatusID, Note, CreatedID, CreatedName, CreatedDate,
ModifiedID, ModifiedName, ModifiedDate, TaskResultID, TaskTypeID) {
    var deviceTaskInfo = {};
    deviceTaskInfo.Code = "newCode";
    deviceTaskInfo.DisplayModelID = DisplayModelID;
    deviceTaskInfo.OperateTypeID = OperateTypeID;
    deviceTaskInfo.MessageTitle = MessageTitle;
    deviceTaskInfo.MessageTypeID = MessageTypeID;
    deviceTaskInfo.MessageSourceID = MessageSourceID;
    deviceTaskInfo.MessageContent = MessageContentJsonString;
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
    //customMessageContent.content = "";
    //var customMessageContentjSON = {};

    customMessageContent.TextString = function (textString) {
        if (textString != "") {
            customMessageContent.text = textString;
        }
        // customMessageContent.content = JSON.stringify(customMessageContentjSON);
        // customMessageContent = customMessageContentjSON;
        return this;
    }
    customMessageContent.ImageString = function (imageString) {
        if(imageString!=""){
        if (customMessageContent.hasOwnProperty("image")) {
            customMessageContent.image.push(imageString);
        }
        else {
            customMessageContent.image = [imageString];
        }
}
        // customMessageContent.content = JSON.stringify(customMessageContentjSON);
        // customMessageContent = customMessageContentjSON;
        return this;
    }
    customMessageContent.ImageArray = function (imageArray) {
        if(null!=imageArray&&imageArray.length>0){
        if (customMessageContent.hasOwnProperty("image")) {
            customMessageContent.image = audienceDictionary.image.concat(imageArray);
        }
        else {
            customMessageContent.image = imageArray;
        }}
        //customMessageContent = customMessageContentjSON;
        //customMessageContent.content = JSON.stringify(customMessageContentjSON);
        return this;
    }
    customMessageContent.VideoString = function (videoString) {
        if(videoString!=""){
        if (customMessageContent.hasOwnProperty("video")) {
            customMessageContent.video.push(videoString);
        }
        else {
            customMessageContent.video = [videoString];
        }
        }
        //customMessageContent.content = JSON.stringify(customMessageContentjSON);
        //customMessageContent = customMessageContentjSON;
        return this;
    }
    customMessageContent.VideoArray = function (videoArray) {
        if (null!=videoArray && videoArray.length > 0) {
        if (customMessageContent.hasOwnProperty("video")) {
            customMessageContent.video = audienceDictionary.video.concat(videoArray);
        }
        else {
            customMessageContent.video = videoArray;
        }
        }
        // customMessageContent.content = JSON.stringify(customMessageContentjSON);
        //  customMessageContent = customMessageContentjSON;
        return this;
    }
    return customMessageContent;
}

//待开发
function NotificationForamt() {
}

