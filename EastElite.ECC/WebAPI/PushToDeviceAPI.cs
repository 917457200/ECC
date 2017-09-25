using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Web;
using cn.jpush.api;
using cn.jpush.api.push;
using cn.jpush.api.push.mode;
using cn.jpush.api.push.notification;
using EDUC.Common;
using EDUC.Common.Bll;
using EDUC.Common.Dal;
using EDUC.Common.Model;
using PublicLib;
using System.IO;
using System.Collections;
using System.Text;
using System.Web.Script.Serialization;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace EastElite.ECC
{
    /// <summary>
    /// 电子班牌推送接口
    /// </summary>
    public class PushToDeviceAPI : ServiceBase
    {
        DataTable dt = new DataTable();

        #region (public) 基本请求入口 DoProcess
        public void _DoProcess( HttpContext context )
        {

            try
            {
                if( CheckLongin( context ) )
                {
                    //记录日志信息

                    logentity.module = "电子班牌推送模块";//模块名称
                    logentity.pageurl = GetQueryUrl();
                    logentity.logcontent = "";
                    switch( context.Request.QueryString[0] )
                    {
                        //推送消息到电子班牌(参数为JSON对象:暂时用到)
                        case "01-01":

                            logentity.functionName = "推送消息到电子班牌";
                            // operatelog.Add(logentity);
                            PushJSONMessageToECC( context );

                            break;
                        //初始化设备前获取所有安装设备的班级
                        case "01-02":

                            logentity.functionName = "获取班级设备信息列表";
                            //  operatelog.Add(logentity);
                            GetDeviceClassList( context );
                            break;
                        //初始化电子班牌
                        case "01-03":

                            logentity.functionName = "初始化设备";
                            // operatelog.Add(logentity);
                            DeviceInt( context );
                            break;
                        //获取权限下年级的所有班级
                        case "01-04":
                            logentity.functionName = "获取权限下年级的所有年级或班级";
                            // operatelog.Add(logentity);
                            GetUserClassList( context );
                            break;
                        //获取用户所有年级
                        case "01-05":

                            logentity.functionName = "获取用户所有年级";
                            //  operatelog.Add(logentity);
                            GetUserGradeList( context );
                            break;
                        //获取用户所有学科类型
                        case "01-06":
                            logentity.functionName = "获取用户所有学科类型";
                            // operatelog.Add(logentity);
                            GetUserSubjectTypeList( context );
                            break;
                        //获取用户所有班级类型
                        case "01-07":

                            logentity.functionName = "获取用户所有班级类型";
                            // operatelog.Add(logentity);
                            GetUserClassTypeList( context );
                            break;
                        //推送通知到app
                        case "01-08":

                            logentity.functionName = "推送通知到app";
                            //  operatelog.Add(logentity);
                            PushNotificationToAPP( context );
                            break;
                        //推送消息到电子班牌(20170717有更新 0144)
                        case "01-09":

                            logentity.functionName = "推送消息到电子班牌";
                            // operatelog.Add(logentity);
                            PushMessageToECC( context );
                            break;
                        //推送通知到App(参数为JSON对象：暂时用不到)
                        case "01-10":

                            logentity.functionName = "送通知到App(参数为JSON对象：暂时用不到)";
                            // operatelog.Add(logentity);
                            PushJSONNotificationToAPP( context );
                            break;
                        ////
                        //case "01-11":
                        //    PushJSONNotificationToAPP(context);
                        //    break;
                        ////获取任务列表
                        case "01-12":
                            logentity.functionName = "获取任务列表";
                            //  operatelog.Add(logentity);
                            GetTaskList( context );
                            break;
                        //撤销任务
                        case "01-13":

                            logentity.functionName = "撤销任务";
                            // operatelog.Add(logentity);
                            CancelTask( context );
                            break;
                        //删除任务
                        case "01-14":

                            logentity.functionName = "删除任务";
                            //  operatelog.Add(logentity);
                            DeleteTask( context );
                            break;
                        //查询任务详情
                        case "01-15":

                            logentity.functionName = "查询任务详情";
                            //  operatelog.Add(logentity);
                            GetTaskDetial( context );
                            break;
                        //App发送消息到电子班牌（供app微信调用 20170717 有更新01-45）
                        case "01-16":

                            logentity.functionName = "App发送消息到电子班牌（供app调用）";
                            // operatelog.Add(logentity);
                            WeChatPushMessageToECC( context );
                            break;
                        //修改班级电子设备信息
                        case "01-17":

                            logentity.functionName = "更新班级电子设备信息";
                            // operatelog.Add(logentity);
                            UpdateDeviceClass( context );
                            break;
                        //回调修改班级电子设备信息班牌系统的版本号
                        case "01-18":

                            logentity.functionName = "回调修改电子班牌系统的版本号";
                            // operatelog.Add(logentity);
                            UpdateDeviceClassVersion( context );
                            break;
                        //获取电子班牌版本号列表
                        case "01-19":

                            logentity.functionName = "获取电子班牌版本号列表";
                            // operatelog.Add(logentity);
                            GetECCVersionList( context );
                            break;
                        //回调修改班级电子设备信息班牌系统的任务时间
                        case "01-20":

                            logentity.functionName = "回调修改班级电子设备信息班牌系统的任务时间";
                            // operatelog.Add(logentity);
                            UpdateDeviceClassTastDate( context );
                            break;
                        //回调修改班级电子设备信息班牌系统的数据清空时间
                        case "01-21":

                            logentity.functionName = "回调修改班级电子设备信息班牌系统的数据清空时间";
                            // operatelog.Add(logentity);
                            UpdateDeviceClassEmptyDate( context );
                            break;
                        //回调修改班级电子设备信息班牌系统的开关机时间
                        case "01-22":

                            logentity.functionName = "回调修改班级电子设备信息班牌系统的开关机时间";
                            // operatelog.Add(logentity);
                            UpdateDeviceClassSwitchDate( context );
                            break;
                        //回调修改班级电子设备信息班牌系统的导航栏是否可见
                        case "01-23":

                            logentity.functionName = "回调修改班级电子设备信息班牌系统的导航栏是否可见";
                            // operatelog.Add(logentity);
                            UpdateDeviceClassNavigationBarVisible( context );
                            break;
                        //回调修改班级电子设备信息班牌系统的更新课程表时间
                        case "01-24":

                            logentity.functionName = "回调修改班级电子设备信息班牌系统的更新课程表时间";
                            // operatelog.Add(logentity);
                            UpdateDeviceClassUpdateScheduleDate( context );
                            break;
                        //回调修改班级电子设备信息班牌系统的心跳检查时间
                        case "01-25":

                            logentity.functionName = "回调修改班级电子设备信息班牌系统的心跳检查时间";
                            // operatelog.Add(logentity);
                            UpdateDeviceClassHeartBeatCheckDate( context );
                            break;
                        //根据校区，年级，用户代码获取班级
                        case "01-26":

                            logentity.functionName = "根据校区，年级，用户代码获取班级";
                            // operatelog.Add(logentity);
                            GetUserClassListByUserCodeAndRootCodeOrGradeCode( context );
                            break;
                        //获取非当前版本号班级
                        case "01-27":

                            logentity.functionName = "获取非当前版本号班级";
                            // operatelog.Add(logentity);
                            GetUnCurrentVersionECC( context );
                            break;
                        //获取用户的班级
                        case "01-28":

                            logentity.functionName = "获取用户的班级";
                            // operatelog.Add(logentity);
                            GetUserClassByUserCode( context );
                            break;
                        //修改用户班级
                        case "01-29":
                            logentity.functionName = "修改用户班级";
                            // operatelog.Add(logentity);
                            UpdateUserClass( context );
                            break;
                        //回调修改班级电子设备信息班牌系统的启动程序软件升级版本号
                        case "01-30":
                            logentity.functionName = "回调修改班级电子设备信息班牌系统的启动程序软件升级版本号";
                            // operatelog.Add(logentity);
                            UpdateDeviceClassInstallerVersion( context );
                            break;
                        ////回调修改班级电子设备信息班牌系统的启动程序心跳检查时间*(不用)
                        //case "01-31":
                        //    logentity.functionName = "回调修改班级电子设备信息班牌系统的启动程序心跳检查时间";
                        //    // operatelog.Add(logentity);
                        //    UpdateDeviceClassInstallerHeartBeatCheckDate(context);
                        //    break;
                        //回调修改班级电子设备信息班牌系统的启动程序开关状态
                        case "01-32":
                            logentity.functionName = "回调修改班级电子设备信息班牌系统的启动程序开关状态";
                            // operatelog.Add(logentity);
                            UpdateDeviceClassStartProgramSwitchStaus( context );
                            break;
                        //获取电子班牌服务版本号列表
                        case "01-33":
                            logentity.functionName = "获取电子班牌服务版本号列表";
                            // operatelog.Add(logentity);
                            GetECCInstallerVersionList( context );
                            break;
                        //获取非当前启动版本号班级
                        case "01-34":

                            logentity.functionName = "获取非当前启动版本号班级";
                            // operatelog.Add(logentity);
                            GetUnCurrentInstallerVersionECC( context );
                            break;
                        case "01-35":
                            logentity.functionName = "推送消息到电子班牌(四十四中)";
                            // operatelog.Add(logentity);
                            PushMessageToECC1( context );
                            break;
                        case "01-36":
                            logentity.functionName = "班级绑定";
                            // operatelog.Add(logentity);
                            RepeatDeviceClass( context );
                            break;
                        case "01-37":
                            logentity.functionName = "回调重新绑定班级信息";
                            // operatelog.Add(logentity);       
                            RepeatBindClassInfo( context );
                            break;
                        case "01-38":
                            logentity.functionName = "获取重新绑定失败的信息";
                            // operatelog.Add(logentity);
                            GetFailBindClassList( context );
                            break;
                        case "01-39":
                            logentity.functionName = "上传终端截屏图片";
                            // operatelog.Add(logentity);
                            UploadTerminalScreenshot( context );
                            break;
                        case "01-40":
                            logentity.functionName = "上传移动终端异常日志";
                            // operatelog.Add(logentity);
                            SaveAppErrorLog( context );
                            break;
                        case "01-41":
                            logentity.functionName = "获取设备终端截屏信息";
                            // operatelog.Add(logentity);
                            GetDeviceTerminalScreenshotList( context );
                            break;
                        case "01-42":
                            logentity.functionName = "app清空设备数据库";
                            // operatelog.Add(logentity);
                            EmptyDeviceDataBase( context );
                            break;
                        case "01-43":
                            logentity.functionName = "重新发送任务";
                            // operatelog.Add(logentity);
                            RepeatSendTask( context );
                            break;
                        //推送消息到电子班牌(zhc 20170713)
                        case "01-44":

                            logentity.functionName = "推送消息到电子班牌";
                            // operatelog.Add(logentity);
                            PushMessageToECCNew( context );
                            break;
                        ///App发送消息到电子班牌（供app调用 zhc 20170713)
                        case "01-45":
                            logentity.functionName = "App发送消息到电子班牌（供app调用）";
                            // operatelog.Add(logentity);
                            WeChatPushMessageToECCNew( context );
                            break;
                        ////获取用户下的班级信息( zhc 20170727)
                        case "01-46":
                            logentity.functionName = "获取用户下的班级信息";
                            // operatelog.Add(logentity);
                            GetUserClass( context );
                            break;
                        ////发送进入设置界面 命令( zhc 20170727)
                        case "01-47":
                            logentity.functionName = "发送进入设置界面 命令";
                            // operatelog.Add(logentity);
                            WeChatPushMessageToInterface( context );
                            break;
                        //是否存在绑定班级
                        case "01-48":
                            logentity.functionName = "是否存在绑定班级";
                            // operatelog.Add(logentity);
                            ExistClass( context );
                            break;
                        default:
                            logentity.otype = "1";
                            logentity.logcontent = "没有找到提供的该方法";
                            operatelog.Add( logentity );
                            context.Response.Write( "没有找到提供的该方法" );
                            break;
                    }
                }
                else
                {
                    logentity.otype = "1";
                    logentity.logcontent = "未登录";
                    operatelog.Add( logentity );
                }
            }
            catch( Exception ex )
            {
                logentity.otype = "1";
                logentity.logcontent = ex.Message;
                operatelog.Add( logentity );
                context.Response.Write( ex.Message );
            }
        }
        #endregion

        private void RepeatSendTask( HttpContext context )
        {


            try
            {
                List<string> param = new List<string>() { "id", "classname", "rootcode" };

                if( !CheckParameters( param ) )
                {
                    return;
                }
                //获取历史推送任务
                bllDeviceTaskInfo bll = new bllDeviceTaskInfo();

                dt = bll.GetPagingSigInfoRepeatTask( string.Format( "id={0}", context.Request.Form["id"].ToString() ), context.Request.Form["classname"].ToString(), context.Request.Form["rootcode"].ToString() );
                if( dt != null && dt.Rows.Count > 0 )
                {
                    DeviceTaskInfoEntity deviceTaskInfo = new DeviceTaskInfoEntity();
                    deviceTaskInfo.Code = dt.Rows[0]["Code"].ToString();
                    deviceTaskInfo.DisplayModelID = Helper.StringToInt( dt.Rows[0]["DisplayModelID"].ToString() );
                    deviceTaskInfo.MessageSourceID = Helper.StringToInt( dt.Rows[0]["MessageSourceID"].ToString() );
                    deviceTaskInfo.MessageTypeID = Helper.StringToInt( dt.Rows[0]["MessageTypeID"].ToString() );
                    deviceTaskInfo.OperateTypeID = Helper.StringToInt( dt.Rows[0]["OperateTypeID"].ToString() );

                    deviceTaskInfo.MessageTitle = dt.Rows[0]["MessageTitle"].ToString();

                    deviceTaskInfo.TaskPriorityID = Helper.StringToInt( dt.Rows[0]["TaskPriorityID"].ToString() );

                    deviceTaskInfo.TaskTypeID = Helper.StringToInt( dt.Rows[0]["TaskTypeID"].ToString() );
                    deviceTaskInfo.MessageContent = new MessageContent();
                    string customcontent = dt.Rows[0]["MessageContent"].ToString();
                    if( !string.IsNullOrWhiteSpace( customcontent ) && customcontent.Contains( "text" ) && customcontent.Contains( "image" ) && customcontent.Contains( "video" ) )
                    {
                        deviceTaskInfo.MessageContent.text = customcontent.Substring( 8, customcontent.IndexOf( "image" ) - 11 );
                        string strimage = customcontent.Substring( customcontent.IndexOf( "image" ) + 8, customcontent.IndexOf( "video" ) - customcontent.IndexOf( "image" ) - 11 ).Replace( "\"", "" );

                        deviceTaskInfo.MessageContent.image = !string.IsNullOrWhiteSpace( strimage ) ? strimage.Split( ',' ) : new string[0];
                        deviceTaskInfo.MessageContent.video = new string[0];
                    }
                    string areamodule = dt.Rows[0]["AreaModule"].ToString();
                    if( areamodule != "null" )
                    {
                        deviceTaskInfo.MessageContent.AreaModule = new MessageContent.AreaModuleClass();
                        deviceTaskInfo.MessageContent.AreaModule = JsonHelper.JsonToObject<MessageContent.AreaModuleClass>( areamodule.ToString() );
                        DataTable dtNotice = bll.getTaskNoticeInfo( deviceTaskInfo.Code );
                        //通知公告
                        if( dtNotice != null )
                        {
                            deviceTaskInfo.MessageContent.AreaModule.ClsNotice = new List<MessageContent.ClsNotice>();
                            for( int i = 0; i < dtNotice.Rows.Count; i++ )
                            {
                                MessageContent.ClsNotice ClsNotice = new MessageContent.ClsNotice();
                                ClsNotice.code = dtNotice.Rows[i]["TaskCode"].ToString();
                                ClsNotice.context = dtNotice.Rows[i]["NoticeContent"].ToString();
                                ClsNotice.title = dtNotice.Rows[i]["NoticeTitle"].ToString();
                                ClsNotice.date = DateTime.Parse( dtNotice.Rows[i]["NoticeTime"].ToString() ).ToString( "yyyy-MM-dd hh:mm:ss" );
                                deviceTaskInfo.MessageContent.AreaModule.ClsNotice.Add( ClsNotice );
                            }
                        }
                        if( deviceTaskInfo.MessageContent.AreaModule.ClsActive != null )
                        {
                            deviceTaskInfo.MessageContent.AreaModule.ClsActive.code = deviceTaskInfo.Code;//撤销Code
                        }
                        if( deviceTaskInfo.MessageContent.AreaModule.ClsCheckStu != null )
                        {
                            deviceTaskInfo.MessageContent.AreaModule.ClsCheckStu.code = deviceTaskInfo.Code;//撤销Code
                        }

                        if( deviceTaskInfo.MessageContent.AreaModule.ClsCheckItem != null )
                        {
                            for( int i = 0; i < deviceTaskInfo.MessageContent.AreaModule.ClsCheckItem.Count; i++ )
                            {
                                deviceTaskInfo.MessageContent.AreaModule.ClsCheckItem[i].code = deviceTaskInfo.Code;//撤销Code
                            }

                        }
                        if( deviceTaskInfo.MessageContent.AreaModule.ClsHonor != null )
                        {
                            for( int i = 0; i < deviceTaskInfo.MessageContent.AreaModule.ClsHonor.Count; i++ )
                            {
                                deviceTaskInfo.MessageContent.AreaModule.ClsHonor[i].code = deviceTaskInfo.Code;//撤销Code
                            }
                        }
                        if( deviceTaskInfo.MessageContent.AreaModule.ClsHomeWk != null )
                        {
                            for( int i = 0; i < deviceTaskInfo.MessageContent.AreaModule.ClsHomeWk.Count; i++ )
                            {
                                deviceTaskInfo.MessageContent.AreaModule.ClsHomeWk[i].code = deviceTaskInfo.Code;//撤销Code
                            }
                        }
                    }

                    //deviceTaskInfo.MessageContent.text = "系统消息13";
                    //deviceTaskInfo.MessageContent.image = null;
                    //deviceTaskInfo.MessageContent.video = null;

                    deviceTaskInfo.TaskBeginTime = Helper.StringToDateTime( dt.Rows[0]["TaskBeginTime"].ToString() );
                    deviceTaskInfo.TaskEndTime = Helper.StringToDateTime( dt.Rows[0]["TaskEndTime"].ToString() );
                    deviceTaskInfo.ImageEffectID = Helper.StringToInt( dt.Rows[0]["ImageEffectID"].ToString() );
                    deviceTaskInfo.ImageSpanSecond = Helper.StringToInt( dt.Rows[0]["ImageSpanSecond"].ToString() );
                    deviceTaskInfo.TargetRange = "";
                    deviceTaskInfo.Tag = dt.Rows[0]["Tag"].ToString();
                    deviceTaskInfo.Tag_and = dt.Rows[0]["Tag_and"].ToString();
                    deviceTaskInfo.Alias = dt.Rows[0]["Alias"].ToString();
                    deviceTaskInfo.Registration_ID = dt.Rows[0]["NewJpushID"].ToString();
                    deviceTaskInfo.RargetAlias = dt.Rows[0]["RargetAlias"].ToString();


                    JPushClient client = new JPushClient( Helper.GetAppSettings( "AppKey" ), Helper.GetAppSettings( "MasterSecret" ) );

                    PushPayload payload = new PushPayload();
                    payload.platform = Platform.all();
                    payload.audience = Audience.all();
                    payload.audience.registrationId( dt.Rows[0]["NewJpushID"].ToString().Trim( '[' ).Trim( ']' ).Trim( '"' ) );
                    payload.options = new Options();
                    payload.options.apns_production = true;
                    payload.options.time_to_live = 86400;//(1天)
                    CustomMsgContent customMsgContent = JsonHelper.ObjectToObject<DeviceTaskInfoEntity, CustomMsgContent>( deviceTaskInfo );
                    payload.message = Message.content( customMsgContent );
                    payload.message.setTitle( "" );
                    try
                    {
                        MessageResult result = client.SendPush( payload );


                        //操作
                        if( result.isResultOK() )
                        {
                            context.Response.Write( JsonHelper.ToJsonResult( "0", "success" ) );
                            return;
                        }
                        else
                        {
                            bll.UpdateTaskResultID( deviceTaskInfo.Code, "1" );
                            logentity.otype = "0";
                            logentity.logcontent += ",发送消息失败";
                            operatelog.Add( logentity );
                            context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
                            return;
                        }
                    }
                    catch
                    {
                        context.Response.Write( JsonHelper.ToJsonResult( "1", "解析数据异常" ) );

                        return;
                    }
                }
                //bllDeviceClassInfo bll = new bllDeviceClassInfo();

                //int pageSize = Helper.StringToInt(context.Request.Form["pageSize"].ToString());
                //int currentPage = Helper.StringToInt(context.Request.Form["currentPage"].ToString());
                ////  gradeCode: $.toString($('#GradeCode').val()),
                ////ClassCode: $.toString($('#ClassCode').val()),
                //string filter = "1=1";
                //if (context.Request.Form["rootCode"] != null && context.Request.Form["rootCode"].ToString().Trim() != "")
                //{
                //    filter += " AND SUBSTRING([ClassCode],1,10)='" + context.Request.Form["rootCode"] + "'";

                //}
                //if (context.Request.Form["gradeCode"] != null && context.Request.Form["gradeCode"].ToString().Trim() != "")
                //{
                //    filter += " AND SUBSTRING([ClassCode],11,6)='" + context.Request.Form["gradeCode"] + "'";

                //}
                //if (context.Request.Form["ClassCode"] != null && context.Request.Form["ClassCode"].ToString().Trim() != "")
                //{
                //    filter += " AND ClassCode='" + context.Request.Form["ClassCode"] + "'";

                //}
                //string order = "SUBSTRING(classcode,1,10) asc,SUBSTRING([ClassCode],11,2) DESC,SUBSTRING([ClassCode],13,4) DESC,SUBSTRING([ClassCode],17,2) asc";
                //int recordCount = 0;
                //int totalPage = 0;
                //dt = bll.GetDeviceTerminalScreenshotList(pageSize, currentPage, filter, order, out recordCount, out totalPage);
                //ReturnListJson(dt, pageSize, recordCount, currentPage, totalPage);
            }
            catch( Exception ex )
            {
                logentity.otype = "1";
                logentity.logcontent = ex.Message;
                operatelog.Add( logentity );
                context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
            }
        }
        private void EmptyDeviceDataBase( HttpContext context )
        {
            try
            {
                List<string> param = new List<string>() { "usercode", "username" };
                if( !CheckParameters( param ) )
                {
                    return;
                }

                JPushClient client = new JPushClient( Helper.GetAppSettings( "AppKey" ), Helper.GetAppSettings( "MasterSecret" ) );
                //消息来源
                int messagesourseid = 4;//微信服务号
                //布局类型
                int displaymodelid = 1;//班级模式

                //消息类型
                int messagetypeid = 1;//新闻通知

                //操作类型--默认设备数据库清空
                int operatetypeid = Helper.StringToInt( context.Request.Form["operatetypeid"] ) > 0 ? Helper.StringToInt( context.Request.Form["operatetypeid"] ) : 7;//operatetypeid

                //优先级
                int taskpriorityid = 1;//1级
                //任务状态
                int taskstatusid = 1;//正常

                //任务类型
                int tasktypeid = 1;//普通任务
                //发送人校区编号
                string userCode = context.Request.Form["userCode"].ToString();

                string rootCode = Helper.ObjectToString( context.Request.Form["rootCode"] );
                //操作人姓名
                string createdname = context.Request.Form["username"].ToString();
                string messagecontent_text = Helper.ObjectToString( context.Request.Form["messagecontent_text"] );
                //图片显示秒数
                int imagespansecond = Helper.ObjectToInt( context.Request.Form["imagespansecond"] ) > 0 ? Helper.ObjectToInt( context.Request.Form["imagespansecond"] ) : 5;

                //图片显示效果类型
                int imageeffectid = Helper.ObjectToInt( context.Request.Form["imageeffectid"] ) > 0 ? Helper.ObjectToInt( context.Request.Form["imageeffectid"] ) : 1;


                bllUserClassInfo classInfo = new bllUserClassInfo();

                dt = classInfo.GetUserClassList( userCode, rootCode, true );

                if( dt != null && dt.Rows.Count > 0 )
                {
                    PushPayload payload = new PushPayload();
                    DeviceTaskInfoEntity deviceTaskInfo = new DeviceTaskInfoEntity();
                    payload.platform = Platform.all();
                    payload.audience = Audience.all();
                    payload.audience = payload.audience.tag_and( Helper.GetAppSettings( "schoolCode" ) ).alias( "U4" );

                    deviceTaskInfo.Tag_and = "[\"" + Helper.GetAppSettings( "schoolCode" ) + "\"]";
                    deviceTaskInfo.Alias = "[\"U4\"]";

                    deviceTaskInfo.RargetAlias = "";
                    //有可能一个教室管理有两个校区不同的班级,则一块发送，所以不能写校区
                    ArrayList jpushidarray = new ArrayList();

                    for( int i = 0; i < dt.Rows.Count; i++ )
                    {

                        if( dt.Rows[i]["JPushID"].ToString() != "" )
                        {
                            //添加校区，和JPushID
                            payload.audience = payload.audience.registrationId( dt.Rows[i]["JPushID"].ToString() );
                            jpushidarray.Add( dt.Rows[i]["JPushID"].ToString() );
                            deviceTaskInfo.RargetAlias += dt.Rows[i]["ClassName"].ToString() + ",";
                        }
                    }
                    if( deviceTaskInfo.RargetAlias.Length > 0 )
                    {
                        deviceTaskInfo.RargetAlias = deviceTaskInfo.RargetAlias.Remove( deviceTaskInfo.RargetAlias.Length - 1, 1 );
                    }
                    if( jpushidarray.Count > 0 )
                    {
                        string strjpushid = "[";
                        for( int j = 0; j < jpushidarray.Count; j++ )
                        {
                            strjpushid += "\"" + jpushidarray[j].ToString() + "\",";
                        }
                        if( strjpushid.Length > 0 )
                        {
                            strjpushid = strjpushid.Remove( strjpushid.Length - 1, 1 );
                        }
                        strjpushid += "]";
                        deviceTaskInfo.Registration_ID = strjpushid;
                    }
                    payload.options = new Options();
                    payload.options.apns_production = true;
                    payload.options.time_to_live = 86400;//(1天)

                    deviceTaskInfo.DisplayModelID = displaymodelid;
                    deviceTaskInfo.MessageSourceID = messagesourseid;
                    deviceTaskInfo.MessageTypeID = messagetypeid;
                    deviceTaskInfo.OperateTypeID = operatetypeid;
                    deviceTaskInfo.MessageTitle = "";
                    deviceTaskInfo.MessageContent = new MessageContent();
                    deviceTaskInfo.TaskPriorityID = taskpriorityid;
                    deviceTaskInfo.TaskStatusID = taskstatusid;
                    deviceTaskInfo.TaskTypeID = tasktypeid;
                    deviceTaskInfo.MessageContent.text = messagecontent_text;
                    string[] imageArray = new string[0];
                    string[] imagenameArray = new string[0];

                    deviceTaskInfo.MessageContent.image = imageArray;
                    deviceTaskInfo.MessageContentAlias = new MessageContent();
                    deviceTaskInfo.MessageContentAlias.image = imagenameArray;
                    deviceTaskInfo.MessageContent.video = null;//不能发送视频

                    deviceTaskInfo.MessageContentAlias.text = "";

                    deviceTaskInfo.MessageContentAlias.video = null;
                    deviceTaskInfo.ImageEffectID = imageeffectid;
                    deviceTaskInfo.ImageSpanSecond = imagespansecond;
                    deviceTaskInfo.TargetRange = payload.audience != null ? JsonHelper.ObjectToJSON( payload.audience.dictionary ) : "";


                    deviceTaskInfo.CreatedID = userCode;
                    deviceTaskInfo.CreatedName = createdname;

                    //操作=
                    bllDeviceTaskInfo bll = new bllDeviceTaskInfo();
                    int errorcode = 0; string errormsg = string.Empty;
                    try
                    {
                        if( bll.Add( ref deviceTaskInfo, logentity, rootCode, out errorcode, out errormsg ) == 0 )
                        {
                            try
                            {
                                CustomMsgContent customMsgContent = JsonHelper.ObjectToObject<DeviceTaskInfoEntity, CustomMsgContent>( deviceTaskInfo );
                                payload.message = Message.content( customMsgContent );

                                MessageResult result = client.SendPush( payload );


                                //操作
                                if( result.isResultOK() )
                                {
                                    context.Response.Write( JsonHelper.ToJsonResult( "0", "success" ) );
                                }
                                else
                                {
                                    bll.UpdateTaskResultID( deviceTaskInfo.Code, "1" );
                                    logentity.otype = "0";
                                    logentity.logcontent = "发送消息失败";
                                    operatelog.Add( logentity );
                                    context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
                                    return;
                                }
                            }
                            catch( Exception ex )
                            {
                                bll.UpdateTaskResultID( deviceTaskInfo.Code, "1" );
                                logentity.otype = "1";
                                logentity.logcontent = ex.Message;
                                operatelog.Add( logentity );
                                context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
                                return;
                            }
                        }
                        else
                        {
                            logentity.otype = "0";
                            logentity.logcontent = "插入消息任务到数据库失败";
                            operatelog.Add( logentity );
                            context.Response.Write( JsonHelper.ToJsonResult( errorcode.ToString(), errormsg ) );
                            return;
                        }
                    }
                    catch( Exception ex )
                    {
                        logentity.otype = "1";
                        logentity.logcontent = ex.Message;
                        operatelog.Add( logentity );
                        return;
                    }
                }
                else
                {
                    logentity.otype = "0";
                    logentity.logcontent = "请检查该教师编号和校区编号或者确认该教师隶属班级电子班牌是否已经完成初始化";
                    operatelog.Add( logentity );
                    context.Response.Write( JsonHelper.ToJsonResult( "1", "请检查该教师编号和校区编号或者确认该教师隶属班级电子班牌是否已经完成初始化" ) );
                    return;
                }
            }
            catch( Exception ex )
            {
                logentity.otype = "1";
                logentity.logcontent = ex.Message;
                operatelog.Add( logentity );
                context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
            }


        }

        private void GetDeviceTerminalScreenshotList( HttpContext context )
        {
            try
            {
                List<string> param = new List<string>() { "pageSize", "currentPage" };

                if( !CheckParameters( param ) )
                {
                    return;
                }
                bllDeviceClassInfo bll = new bllDeviceClassInfo();

                int pageSize = Helper.StringToInt( context.Request.Form["pageSize"].ToString() );
                int currentPage = Helper.StringToInt( context.Request.Form["currentPage"].ToString() );
                //  gradeCode: $.toString($('#GradeCode').val()),
                //ClassCode: $.toString($('#ClassCode').val()),
                string filter = "1=1";
                if( context.Request.Form["rootCode"] != null && context.Request.Form["rootCode"].ToString().Trim() != "" )
                {
                    filter += " AND SUBSTRING([ClassCode],1,10)='" + context.Request.Form["rootCode"] + "'";

                }
                if( context.Request.Form["gradeCode"] != null && context.Request.Form["gradeCode"].ToString().Trim() != "" )
                {
                    filter += " AND SUBSTRING([ClassCode],11,6)='" + context.Request.Form["gradeCode"] + "'";

                }
                if( context.Request.Form["ClassCode"] != null && context.Request.Form["ClassCode"].ToString().Trim() != "" )
                {
                    filter += " AND ClassCode='" + context.Request.Form["ClassCode"] + "'";

                }
                if( context.Request.Form["version"] != null && context.Request.Form["version"].ToString() != "" )
                {
                    if( context.Request.Form["version"].ToString() == "-1" )
                    {
                        filter += " AND (version='' or version is null)";
                    }
                    else
                    {
                        filter += " AND version='" + context.Request.Form["version"] + "'";
                    }


                }
                string order = "TerminalScreenshot desc";
                //20170808 正定改
                //string order = "SUBSTRING(classcode,1,10) asc,SUBSTRING([ClassCode],11,2) DESC,SUBSTRING([ClassCode],13,4) DESC,SUBSTRING([ClassCode],17,2) asc";
                int recordCount = 0;
                int totalPage = 0;
                dt = bll.GetDeviceTerminalScreenshotList( pageSize, currentPage, filter, order, out recordCount, out totalPage );
                ReturnListJson( dt, pageSize, recordCount, currentPage, totalPage );
            }
            catch( Exception ex )
            {
                logentity.otype = "1";
                logentity.logcontent = ex.Message;
                operatelog.Add( logentity );
                context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
            }
        }
        private void SaveAppErrorLog( HttpContext context )
        {
            try
            {
                List<string> param = new List<string>() { "logcontent", "module" };
                if( !CheckParameters( param ) )
                {
                    return;
                }
                logentity.ctime = DateTime.Now;
                logentity.module = context.Request.Form["module"];
                logentity.otype = "3";
                logentity.logcontent = context.Request.Form["logcontent"];
                logentity.cuser = 0;
                if( operatelog.Add( logentity ) == 0 )
                {
                    context.Response.Write( JsonHelper.ToJsonResult( "0", "success" ) );
                }
                else
                {
                    context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
                }
            }
            catch( Exception ex )
            {
                logentity.otype = "1";
                logentity.logcontent = ex.Message;
                operatelog.Add( logentity );
                context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
            }
        }

        private void UploadTerminalScreenshot( HttpContext context )
        {
            try
            {
                List<string> param = new List<string>() { "classcode" };
                if( !CheckParameters( param ) )
                {
                    return;
                }
                string classcode = context.Request.Form["classcode"];
                //获取配置文件路径
                string strurl = "/uploads/" + classcode + "/";//拼接上传路径   上传文件的相对路径
                string strpath = context.Server.MapPath( strurl );//上传文件绝对路径  
                if( !Directory.Exists( strpath ) )
                {
                    //若不存在则创建
                    Directory.CreateDirectory( strpath );
                }

                dynamic dyn = new { url = strurl, path = strpath };

                if( HttpContext.Current.Request.Files.Count > 0 )
                {

                    HttpPostedFile file = HttpContext.Current.Request.Files[0];
                    Stream stream = file.InputStream;
                    string filname = Path.GetFileName( file.FileName );//上传文件名称

                    TimeSpan ts = DateTime.UtcNow - new DateTime( 1970, 1, 1, 0, 0, 0, 0 );
                    string filecode = Convert.ToInt64( ts.TotalMilliseconds ).ToString() + ( 0 + 1 ).ToString( "00" );//生成唯一码
                    string newfilename = DateTime.Now.ToString( "yyyyMMddHHmmss" ) + filecode + Path.GetExtension( file.FileName );

                    string path = dyn.path + newfilename;//文件绝对路径
                    string url = dyn.url + newfilename;//文件相对路径

                    //保存文件
                    file.SaveAs( path );

                    bllDataFieldInfo bll = new bllDataFieldInfo();
                    DataFieldInfoEntity entity = new DataFieldInfoEntity();
                    string errormsg = "";
                    int errorcode = 0;
                    entity.FieldCode = filecode;
                    entity.FieldName1 = filname;//上传文件名称
                    entity.FieldContent = url;//保存文件相对路径
                    entity.FieldTypeID = 1;
                    if( bll.UploadTerminalScreenshot( classcode, ref entity, new operatelogEntity(), out errorcode, out errormsg ) == 0 )
                    {
                        context.Response.Write( JsonHelper.ToJsonResult( "0", "success" ) );
                    }
                    else
                    {
                        logentity.otype = "1";
                        logentity.logcontent = classcode + "终端截屏图片上传失败";
                        operatelog.Add( logentity );
                        context.Response.Write( JsonHelper.ToJsonResult( "1", "failure:终端截屏图片上传失败" ) );
                    }
                }
                else
                {
                    logentity.otype = "1";
                    logentity.logcontent = classcode + "没有获取到终端截屏图片";
                    operatelog.Add( logentity );
                    context.Response.Write( JsonHelper.ToJsonResult( "1", "failure:没有获取到终端截屏图片" ) );
                }
            }
            catch( Exception ex )
            {
                logentity.otype = "1";
                logentity.logcontent = ex.Message;
                operatelog.Add( logentity );
                context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
            }
        }



        /// <summary>
        /// 获取重新绑定班级失败的班级信息列表
        /// </summary>
        /// <param name="context"></param>
        private void GetFailBindClassList( HttpContext context )
        {
            try
            {
                bllUserClassInfo bll = new bllUserClassInfo();
                dt = bll.GetFailBindClassList();

                ReturnListJson( dt );
            }
            catch( Exception ex )
            {
                logentity.otype = "1";
                logentity.logcontent = ex.Message;
                operatelog.Add( logentity );
                context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
            }
        }
        private void RepeatBindClassInfo( HttpContext context )
        {
            try
            {
                List<string> param = new List<string>() { "JPushID", "AsyncResultID" };
                if( !CheckParameters( param ) )
                {
                    return;
                }
                bllDeviceTaskInfo bll = new bllDeviceTaskInfo();
                string jpushid = context.Request.Form["jpushid"];
                string AsyncResultID = context.Request.Form["AsyncResultID"];

                if( bll.UpdateRepeatBindClassInfoAsyncResultID( jpushid, AsyncResultID ) == 0 )
                {
                    context.Response.Write( JsonHelper.ToJsonResult( "0", "成功" ) );
                }
                else
                {
                    logentity.otype = "0";
                    logentity.logcontent += ",修改数据库返回内容错误";
                    operatelog.Add( logentity );
                    context.Response.Write( JsonHelper.ToJsonResult( "1", "失败" ) );
                }
            }
            catch( Exception ex )
            {
                logentity.otype = "1";
                logentity.logcontent += "," + ex.Message;
                operatelog.Add( logentity );
                context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
            }
        }

        public class ClassListEntity
        {
            public DeviceClassInfoEntity[] ClassList;
        }
        /// <summary>
        /// 班级绑定
        /// </summary>
        /// <param name="context"></param>
        private void RepeatDeviceClass( HttpContext context )
        {
            try
            {
                List<string> param = new List<string>() { "JPushID", "ClassCode" };

                //if( !CheckParameters( param ) )
                //{
                //    return;
                //}
                bllDeviceClassInfo bll = new bllDeviceClassInfo();
                string JPushID = context.Request.Form["JPushID"].ToString();
                string ClassCode = context.Request.Form["ClassCode"].ToString();
                string OldClassName = context.Request.Form["OldClassName"].ToString();
                string ModefiedID = context.Request.Form["ModefiedID"].ToString();
                string ModifiedName = context.Request.Form["ModifiedName"].ToString();
                string RoomNum = context.Request.Form["roomnum"].ToString();
                string orderSelect = context.Request.Form["orderSelect"].ToString();
                string rootCode = context.Request.Form["rootCode"].ToString();

                string ClassName = "";
                string ClassFullCode = "";
                string BanZhuRenPhotoPath = "";
                string BanZhuRenQRPath = "";
                string ClassNickName = "";
                string ClassSlogan = "";
                string ZuoYouMing = "";
                string Introduction = "";
                string Recommended = "";
                string ClassLogoPath = "";
                string ClassQRPath = "";
                int SubjectTypeID = 0;
                string SubjectTypeIDText = "";
                int ClassTypeID = 0;
                string ClassTypeIDText = "";
                string SemesterName = "";
                string BanZhuRenCode = "";
                string BanZhuRenName = "";
                int errorcode = 0; string errormsg = string.Empty;
                EastEliteICMSWS.EastEliteICMSWSSoapClient client = new EastEliteICMSWS.EastEliteICMSWSSoapClient();
                string result = client.GetECCDeviceClassItem( ClassCode );
                ClassListEntity list = JsonHelper.JsonToObject<ClassListEntity>( result );
                if( list.ClassList.Length > 0 )
                {
                    ClassName = list.ClassList[0].ClassName;
                    ClassFullCode = list.ClassList[0].ClassFullCode;
                    BanZhuRenPhotoPath = list.ClassList[0].BanZhuRenPhotoPath;
                    BanZhuRenQRPath = list.ClassList[0].BanZhuRenQRPath;
                    ClassNickName = list.ClassList[0].ClassNickName;
                    ClassSlogan = list.ClassList[0].ClassSlogan;
                    ZuoYouMing = list.ClassList[0].ZuoYouMing;
                    Introduction = list.ClassList[0].Introduction;
                    Recommended = list.ClassList[0].Recommended;
                    ClassLogoPath = list.ClassList[0].ClassLogoPath;
                    ClassQRPath = list.ClassList[0].ClassQRPath;
                    SubjectTypeID = Helper.ObjectToInt( list.ClassList[0].SubjectTypeID );
                    SubjectTypeIDText = list.ClassList[0].SubjectTypeIDText;
                    ClassTypeID = Helper.ObjectToInt( list.ClassList[0].ClassTypeID );
                    ClassTypeIDText = list.ClassList[0].ClassTypeIDText;
                    SemesterName = list.ClassList[0].SemesterName;
                    BanZhuRenCode = list.ClassList[0].BanZhuRenCode;
                    BanZhuRenName = list.ClassList[0].BanZhuRenName;
                }

                dt = bll.RepeatDeviceClass( out errorcode, out errormsg, JPushID, ClassCode, OldClassName, ClassName, ClassFullCode, BanZhuRenPhotoPath, BanZhuRenQRPath, ClassNickName, ClassSlogan, ZuoYouMing, Introduction, Recommended, ClassLogoPath, ClassQRPath, SubjectTypeID, SubjectTypeIDText, ClassTypeID, ClassTypeIDText, SemesterName, BanZhuRenCode, BanZhuRenName, ModefiedID, ModifiedName, RoomNum );
                ///自动发送重新绑定班级信息命令
                if( orderSelect == "1" )
                {
                    BindingClasses( ModefiedID, ModifiedName, ClassCode, JPushID );
                }
                JavaScriptSerializer jss = new JavaScriptSerializer();
                string resultuser = client.GetECCUserClassList( rootCode, "", "1072", ClassName );

                JObject userlist = (JObject) JsonConvert.DeserializeObject( resultuser );

                JArray ClassList = JArray.Parse( userlist["ClassList"].ToString() );

                List<UserClassInfoEntity> UserClass = JsonHelper.JsonToObject<List<UserClassInfoEntity>>( ClassList.ToString() );

                for( int i = 0; i < UserClass.Count; i++ )
                {
                    if( UserClass[i].UserCode != "" )
                    {
                        bll.RepeatUserClass( UserClass[i].UserCode, UserClass[i].UserType, UserClass[i].ClassCode, UserClass[i].ClassFullCode, UserClass[i].ClassName, UserClass[i].RoleType, 1, "", ModefiedID, ModifiedName, DateTime.Now );
                    }
                }
                ///UserClassInfo插入当前用户数据
                bll.RepeatUserClass( ModefiedID, 1, ClassCode, ClassFullCode, ClassName, 51, 1, "", ModefiedID, ModifiedName, DateTime.Now );
                if( errorcode != 0 )
                {
                    logentity.otype = "0";
                    logentity.logcontent = "班级绑定失败：" + errorcode;
                    operatelog.Add( logentity );
                }
                ReturnListJson( dt );
            }
            catch( Exception ex )
            {
                logentity.otype = "1";
                logentity.logcontent = ex.Message;
                operatelog.Add( logentity );
                context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
            }
        }
        /// <summary>
        /// 重新绑定班级信息接口（20170717 zhc 改）
        /// </summary>
        /// <param name="context"></param>
        private void BindingClasses( string userCode, string username, string classcode, string JPushID )
        {
            try
            {
                //消息来源
                int messagesourseid = 4;
                logentity.functionName = logentity.functionName + "--" + (EnumMessageSourceID) messagesourseid;

                JPushClient client = new JPushClient( Helper.GetAppSettings( "AppKey" ), Helper.GetAppSettings( "MasterSecret" ) );

                //消息类型
                int messagetypeid = 1;//新闻通知

                //操作类型--默认为进入设置界面
                int operatetypeid = 17;//operatetypeid
                //布局类型
                int displaymodelid = 1;//displaymodelid
                //优先级
                int taskpriorityid = 1;//1级
                //任务状态
                int taskstatusid = 1;//正常

                //任务类型
                int tasktypeid = 1;//普通任务

                string campusCode = classcode.Substring( 0, 10 );

                string messagecontent_text = "";



                if( !string.IsNullOrWhiteSpace( JPushID ) )
                {
                    //获取班级信息
                    bllDeviceClassInfo bll = new bllDeviceClassInfo();

                    DataTable dts = bll.GetPagingSigInfo( "IsValid=1" + " and JPushID in ('" + JPushID + "')" );
                    if( dts != null )
                    {
                        messagecontent_text = JsonHelper.DataTableToJSON( dts );
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    return;
                }
                //操作人姓名
                string createdname = username;

                bllUserClassInfo classInfo = new bllUserClassInfo();

                DataTable dt2 = classInfo.GetClassListForBindingClasses( userCode, classcode );

                if( dt2 != null && dt2.Rows.Count > 0 )
                {
                    PushPayload payload = new PushPayload();
                    DeviceTaskInfoEntity deviceTaskInfo = new DeviceTaskInfoEntity();
                    payload.platform = Platform.all();
                    payload.audience = Audience.all();
                    string schoolCode = "[\"" + Helper.GetAppSettings( "schoolCode" ) + "\",\"RC" + campusCode.Substring( 6, 4 ) + "\"]";
                    string[] schoolCodetag_and = new string[] { Helper.GetAppSettings( "schoolCode" ), "RC" + campusCode.Substring( 6, 4 ) };
                    deviceTaskInfo.Tag_and = schoolCode;
                    deviceTaskInfo.Alias = "[\"U4\"]";
                    payload.audience = payload.audience.tag_and( schoolCodetag_and ).alias( "U4" );

                    deviceTaskInfo.RargetAlias = "";
                    //有可能一个教室管理有两个校区不同的班级,则一块发送，所以不能写校区
                    ArrayList jpushidarray = new ArrayList();

                    for( int i = 0; i < dt2.Rows.Count; i++ )
                    {

                        if( dt2.Rows[i]["JPushID"].ToString() != "" )
                        {
                            //添加校区，和JPushID
                            payload.audience = payload.audience.registrationId( dt2.Rows[i]["JPushID"].ToString() );
                            jpushidarray.Add( dt2.Rows[i]["JPushID"].ToString() );
                            deviceTaskInfo.RargetAlias += dt2.Rows[i]["ClassName"].ToString() + ",";
                        }
                    }

                    if( deviceTaskInfo.RargetAlias.Length > 0 )
                    {
                        deviceTaskInfo.RargetAlias = deviceTaskInfo.RargetAlias.Remove( deviceTaskInfo.RargetAlias.Length - 1, 1 );
                    }
                    if( jpushidarray.Count > 0 )
                    {
                        string strjpushid = "[";
                        for( int j = 0; j < jpushidarray.Count; j++ )
                        {
                            strjpushid += "\"" + jpushidarray[j].ToString() + "\",";
                        }
                        if( strjpushid.Length > 0 )
                        {
                            strjpushid = strjpushid.Remove( strjpushid.Length - 1, 1 );
                        }
                        strjpushid += "]";
                        deviceTaskInfo.Registration_ID = strjpushid;
                    }


                    payload.options = new Options();
                    payload.options.apns_production = true;
                    payload.options.time_to_live = 86400;//(1天)

                    deviceTaskInfo.DisplayModelID = 1;
                    deviceTaskInfo.MessageSourceID = messagesourseid;
                    deviceTaskInfo.MessageTypeID = messagetypeid;
                    deviceTaskInfo.OperateTypeID = operatetypeid;
                    deviceTaskInfo.MessageTitle = "";
                    deviceTaskInfo.MessageContent = new MessageContent();
                    deviceTaskInfo.TaskPriorityID = taskpriorityid;
                    deviceTaskInfo.TaskStatusID = taskstatusid;
                    deviceTaskInfo.TaskTypeID = tasktypeid;
                    deviceTaskInfo.MessageContent.text = messagecontent_text;

                    deviceTaskInfo.MessageContentAlias = new MessageContent();
                    deviceTaskInfo.MessageContentAlias.text = "";
                    deviceTaskInfo.MessageContentAlias.image = null;
                    deviceTaskInfo.MessageContentAlias.video = null;
                    deviceTaskInfo.MessageContentAlias.AreaModule = null;

                    deviceTaskInfo.TargetRange = payload.audience != null ? JsonHelper.ObjectToJSON( payload.audience.dictionary ) : "";

                    deviceTaskInfo.DisplayModelID = displaymodelid;
                    deviceTaskInfo.CreatedID = userCode;
                    deviceTaskInfo.CreatedName = createdname;

                    //操作=
                    bllDeviceTaskInfo bll = new bllDeviceTaskInfo();
                    int errorcode = 0; string errormsg = string.Empty;
                    try
                    {
                        if( bll.Add( ref deviceTaskInfo, logentity, campusCode, out errorcode, out errormsg ) == 0 )
                        {
                            #region MyRegion
                            try
                            {
                                CustomMsgContent customMsgContent = JsonHelper.ObjectToObject<DeviceTaskInfoEntity, CustomMsgContent>( deviceTaskInfo );
                                payload.message = Message.content( customMsgContent );

                                MessageResult result = client.SendPush( payload );
                                //操作
                                if( result.isResultOK() )
                                {
                                    return;
                                }
                                else
                                {
                                    bll.UpdateTaskResultID( deviceTaskInfo.Code, "1" );
                                    logentity.otype = "0";
                                    logentity.logcontent = "发送消息失败";
                                    operatelog.Add( logentity );
                                    return;
                                }
                            }
                            catch( Exception ex )
                            {
                                bll.UpdateTaskResultID( deviceTaskInfo.Code, "1" );
                                logentity.otype = "1";
                                logentity.logcontent = "极光推送报错" + ex.Message;
                                operatelog.Add( logentity );
                                return;
                            }
                            #endregion
                        }
                        else
                        {
                            logentity.otype = "0";
                            logentity.logcontent = "插入消息任务到数据库失败";
                            operatelog.Add( logentity );
                            return;
                        }
                    }
                    catch( Exception ex )
                    {
                        logentity.otype = "1";
                        logentity.logcontent = ex.Message;
                        operatelog.Add( logentity );
                        return;
                    }
                }
                else
                {
                    logentity.otype = "0";
                    logentity.logcontent = "请检查该教师编号和校区编号或者确认该教师隶属班级电子班牌是否已经完成初始化";
                    operatelog.Add( logentity );
                    return;
                }
            }
            catch( Exception ex )
            {
                logentity.otype = "1";
                logentity.logcontent = ex.Message;
                operatelog.Add( logentity );
            }
        }
        //
        private void ExistClass( HttpContext context )
        {
            bllDeviceTaskInfo bll = new bllDeviceTaskInfo();
            string ClassCode = context.Request.Form["classcode"].ToString();
            string JPushID = context.Request.Form["JPushID"].ToString();
            int dt = bll.ExistClass( ClassCode, JPushID );
            if( dt > 0 )
            {
                context.Response.Write( JsonHelper.ToJsonResult( "0", "Suc" ) );
            }
            else {
                context.Response.Write( JsonHelper.ToJsonResult( "1", "Err" ) );
            }

        }
        private void GetECCInstallerVersionList( HttpContext context )
        {
            try
            {
                bllDeviceTaskInfo bll = new bllDeviceTaskInfo();
                dt = bll.GetECCInstallerVersionList();
                ReturnListJson( dt );
            }
            catch( Exception ex )
            {
                logentity.otype = "1";
                logentity.logcontent = ex.Message;
                operatelog.Add( logentity );
                context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
            }
        }
        private void UpdateDeviceClassStartProgramSwitchStaus( HttpContext context )
        {
            try
            {
                List<string> param = new List<string>() { "classCode", "startprogramswitchstaus" };
                if( !CheckParameters( param ) )
                {
                    return;
                }

                bllDeviceTaskInfo bll = new bllDeviceTaskInfo();
                string classCode = context.Request.Form["classCode"];
                string StartProgramSwitchStaus = context.Request.Form["startprogramswitchstaus"];

                if( bll.UpdateDeviceClassStartProgramSwitchStaus( classCode, StartProgramSwitchStaus ) == 0 )
                {
                    context.Response.Write( JsonHelper.ToJsonResult( "0", "成功" ) );
                }
                else
                {
                    logentity.otype = "0";
                    logentity.logcontent += ",修改数据库返回内容错误";
                    operatelog.Add( logentity );
                    context.Response.Write( JsonHelper.ToJsonResult( "1", "失败" ) );
                }
            }
            catch( Exception ex )
            {
                logentity.otype = "1";
                logentity.logcontent += "," + ex.Message;
                operatelog.Add( logentity );
                context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
            }
        }

        private void UpdateDeviceClassInstallerHeartBeatCheckDate( HttpContext context )
        {
            try
            {
                List<string> param = new List<string>() { "classCode", "heartbeatcheckdate" };
                if( !CheckParameters( param ) )
                {
                    return;
                }
                logentity.logcontent = Helper.ObjectToString( context.Request.Form["classCode"] );
                bllDeviceTaskInfo bll = new bllDeviceTaskInfo();
                string classCode = context.Request.Form["classCode"];
                string heartbeatcheckdate = context.Request.Form["heartbeatcheckdate"];
                DateTime dtDate;
                if( DateTime.TryParse( heartbeatcheckdate, out dtDate ) )
                {
                    if( bll.UpdateDeviceClassInstallerHeartBeatCheckDateByClassCode( classCode, heartbeatcheckdate ) == 0 )
                    {
                        context.Response.Write( JsonHelper.ToJsonResult( "0", "成功" ) );
                    }
                    else
                    {
                        logentity.otype = "0";
                        logentity.logcontent += ",修改数据库返回内容错误";
                        operatelog.Add( logentity );
                        context.Response.Write( JsonHelper.ToJsonResult( "1", "失败" ) );
                    }
                }
                else
                {
                    logentity.otype = "0";
                    logentity.logcontent += ",参数值错误:" + heartbeatcheckdate;
                    operatelog.Add( logentity );
                    context.Response.Write( JsonHelper.ToJsonResult( "1", logentity.logcontent ) );
                }
            }
            catch( Exception ex )
            {
                logentity.otype = "1";
                logentity.logcontent += "," + ex.Message;
                operatelog.Add( logentity );
                context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
            }
        }

        private void UpdateDeviceClassInstallerVersion( HttpContext context )
        {
            try
            {
                List<string> param = new List<string>() { "classCode", "version", "heartbeatcheckdate" };

                if( !CheckParameters( param ) )
                {
                    return;
                }

                bllDeviceTaskInfo bll = new bllDeviceTaskInfo();
                string classCode = context.Request.Form["classCode"];
                string Version = context.Request.Form["version"];
                string heartbeatcheckdate = context.Request.Form["heartbeatcheckdate"];
                logentity.otype = "2";
                if( bll.UpdateDeviceClassInstallerVersionByClassCode( classCode, Version, heartbeatcheckdate ) == 0 )
                {
                    context.Response.Write( JsonHelper.ToJsonResult( "0", "成功" ) );
                }
                else
                {
                    logentity.otype = "0";
                    logentity.logcontent += ",修改数据库返回内容错误";
                    operatelog.Add( logentity );
                    context.Response.Write( JsonHelper.ToJsonResult( "1", "失败" ) );
                }
            }
            catch( Exception ex )
            {
                logentity.otype = "1";
                logentity.logcontent += "," + ex.Message;
                operatelog.Add( logentity );
                context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
            }
        }

        private void UpdateUserClass( HttpContext context )
        {
            try
            {
                List<string> param = new List<string>() { "userCode", "data" };

                if( !CheckParameters( param ) )
                {
                    return;
                }
                bllUserClassInfo bll = new bllUserClassInfo();
                string userCode = context.Request.Form["userCode"].ToString();
                //DataTable userclassList = Helper.JsonToDataTable(context.Request.Form["data"].ToString());

                List<UserClassInfoEntity> list = JsonHelper.DeserializeJsonToList<UserClassInfoEntity>( context.Request.Form["data"].ToString() );
                if( list != null && list.Count > 0 )
                {
                    if( bll.UpdateUserClass( list, userCode ) == 0 )
                    {
                        context.Response.Write( JsonHelper.ToJsonResult( "0", "success" ) );
                    }
                    else
                    {
                        context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
                    }
                }
                else
                {
                    logentity.otype = "0";
                    logentity.logcontent = "请检查data对象内容";
                    operatelog.Add( logentity );
                    context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
                }
            }
            catch( Exception ex )
            {
                logentity.otype = "1";
                logentity.logcontent = ex.Message;
                operatelog.Add( logentity );
                context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
            }
        }
        private void GetUserClassByUserCode( HttpContext context )
        {
            try
            {
                List<string> param = new List<string>() { "userCode" };

                if( !CheckParameters( param ) )
                {
                    return;
                }
                bllUserClassInfo bll = new bllUserClassInfo();

                string userCode = context.Request.Form["userCode"].ToString();
                string filter = string.Format( " isValid=1 and userCode='{0}'", userCode );
                int rowTotal = 0;
                int pageTotal = 0;
                dt = bll.GetPagingListInfo( 100000, 1, filter, "", out rowTotal, out pageTotal );

                ReturnListJson( dt );
            }
            catch( Exception ex )
            {
                logentity.otype = "1";
                logentity.logcontent = ex.Message;
                operatelog.Add( logentity );
                context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
            }
        }

        /// <summary>
        /// 获取非当前版本号班级
        /// </summary>
        /// <param name="context"></param>
        private void GetUnCurrentVersionECC( HttpContext context )
        {
            try
            {
                bllUserClassInfo bll = new bllUserClassInfo();
                dt = bll.GetUnCurrentVersionECC();

                ReturnListJson( dt );
            }
            catch( Exception ex )
            {
                logentity.otype = "1";
                logentity.logcontent = ex.Message;
                operatelog.Add( logentity );
                context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
            }
        }
        /// <summary>
        /// 获取非当前启动版本号班级
        /// </summary>
        /// <param name="context"></param>
        private void GetUnCurrentInstallerVersionECC( HttpContext context )
        {
            try
            {
                bllUserClassInfo bll = new bllUserClassInfo();
                dt = bll.GetUnCurrentInstallerVersionECC();

                ReturnListJson( dt );
            }
            catch( Exception ex )
            {
                logentity.otype = "1";
                logentity.logcontent = ex.Message;
                operatelog.Add( logentity );
                context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
            }
        }

        /// <summary>
        /// 根据校区，年级，用户代码获取班级
        /// </summary>
        /// <param name="context"></param>
        private void GetUserClassListByUserCodeAndRootCodeOrGradeCode( HttpContext context )
        {
            try
            {
                List<string> param = new List<string>() { "userCode" };

                if( !CheckParameters( param ) )
                {
                    return;
                }
                bllUserClassInfo bll = new bllUserClassInfo();

                string userCode = context.Request.Form["userCode"].ToString();
                string rootCode = Helper.ObjectToString( context.Request.Form["rootCode"] );
                string gradeCode = Helper.ObjectToString( context.Request.Form["gradeCode"] );
                dt = bll.GetUserClassListByUserCodeAndRootCodeOrGradeCode( userCode, rootCode, gradeCode );

                ReturnListJson( dt );
            }
            catch( Exception ex )
            {
                logentity.otype = "1";
                logentity.logcontent = ex.Message;
                operatelog.Add( logentity );
                context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
            }
        }
        private void UpdateDeviceClassHeartBeatCheckDate( HttpContext context )
        {
            try
            {
                List<string> param = new List<string>() { "classCode", "heartbeatcheckdate" };
                if( !CheckParameters( param ) )
                {
                    return;
                }
                logentity.logcontent = Helper.ObjectToString( context.Request.Form["classCode"] );
                bllDeviceTaskInfo bll = new bllDeviceTaskInfo();
                string classCode = context.Request.Form["classCode"];
                string heartbeatcheckdate = context.Request.Form["heartbeatcheckdate"];
                DateTime dtDate;
                if( DateTime.TryParse( heartbeatcheckdate, out dtDate ) )
                {
                    if( bll.UpdateDeviceClassHeartBeatCheckDateByClassCode( classCode, heartbeatcheckdate ) == 0 )
                    {
                        context.Response.Write( JsonHelper.ToJsonResult( "0", "成功" ) );
                    }
                    else
                    {
                        logentity.otype = "0";
                        logentity.logcontent += ",修改数据库返回内容错误";
                        operatelog.Add( logentity );
                        context.Response.Write( JsonHelper.ToJsonResult( "1", "失败" ) );
                    }
                }
                else
                {
                    logentity.otype = "0";
                    logentity.logcontent += ",参数值错误:" + heartbeatcheckdate;
                    operatelog.Add( logentity );
                    context.Response.Write( JsonHelper.ToJsonResult( "1", logentity.logcontent ) );
                }
            }
            catch( Exception ex )
            {
                logentity.otype = "1";
                logentity.logcontent += "," + ex.Message;
                operatelog.Add( logentity );
                context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
            }
        }
        private void UpdateDeviceClassUpdateScheduleDate( HttpContext context )
        {
            try
            {
                List<string> param = new List<string>() { "classCode", "updatescheduledate" };
                if( !CheckParameters( param ) )
                {
                    return;
                }
                logentity.logcontent = Helper.ObjectToString( context.Request.Form["classCode"] );
                bllDeviceTaskInfo bll = new bllDeviceTaskInfo();
                string classCode = context.Request.Form["classCode"];
                string updatescheduledate = context.Request.Form["updatescheduledate"];
                DateTime dtDate;
                if( DateTime.TryParse( updatescheduledate, out dtDate ) )
                {
                    if( bll.UpdateDeviceClassUpdateScheduleDateByClassCode( classCode, updatescheduledate ) == 0 )
                    {
                        context.Response.Write( JsonHelper.ToJsonResult( "0", "成功" ) );
                    }
                    else
                    {
                        logentity.otype = "0";
                        logentity.logcontent += ",修改数据库返回内容错误";
                        operatelog.Add( logentity );
                        context.Response.Write( JsonHelper.ToJsonResult( "1", "失败" ) );
                    }
                }
                else
                {
                    logentity.otype = "0";
                    logentity.logcontent += ",参数值错误:" + updatescheduledate;
                    operatelog.Add( logentity );
                    context.Response.Write( JsonHelper.ToJsonResult( "1", logentity.logcontent ) );
                }
            }
            catch( Exception ex )
            {
                logentity.otype = "1";
                logentity.logcontent += "," + ex.Message;
                operatelog.Add( logentity );
                context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
            }
        }
        private void UpdateDeviceClassNavigationBarVisible( HttpContext context )
        {
            try
            {
                List<string> param = new List<string>() { "classCode", "navigationbarvisible" };
                if( !CheckParameters( param ) )
                {
                    return;
                }
                logentity.logcontent = Helper.ObjectToString( context.Request.Form["classCode"] );
                bllDeviceTaskInfo bll = new bllDeviceTaskInfo();
                string classCode = context.Request.Form["classCode"];
                string navigationbarvisible = context.Request.Form["navigationbarvisible"];
                if( bll.UpdateDeviceClassNavigationBarVisibleByClassCode( classCode, navigationbarvisible ) == 0 )
                {
                    context.Response.Write( JsonHelper.ToJsonResult( "0", "成功" ) );
                }
                else
                {
                    logentity.otype = "0";
                    logentity.logcontent += ",修改数据库返回内容错误";
                    operatelog.Add( logentity );
                    context.Response.Write( JsonHelper.ToJsonResult( "1", "失败" ) );
                }
            }
            catch( Exception ex )
            {
                logentity.otype = "1";
                logentity.logcontent += "," + ex.Message;
                operatelog.Add( logentity );
                context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
            }
        }
        private void UpdateDeviceClassSwitchDate( HttpContext context )
        {
            try
            {
                List<string> param = new List<string>() { "classCode", "switchdate" };
                if( !CheckParameters( param ) )
                {
                    return;
                }
                logentity.logcontent = Helper.ObjectToString( context.Request.Form["classCode"] );
                bllDeviceTaskInfo bll = new bllDeviceTaskInfo();
                string classCode = context.Request.Form["classCode"];
                string switchdate = context.Request.Form["switchdate"];
                if( bll.UpdateDeviceClassSwitchDateByClassCode( classCode, switchdate ) == 0 )
                {
                    context.Response.Write( JsonHelper.ToJsonResult( "0", "成功" ) );
                }
                else
                {
                    logentity.otype = "0";
                    logentity.logcontent += ",修改数据库返回内容错误";
                    operatelog.Add( logentity );
                    context.Response.Write( JsonHelper.ToJsonResult( "1", "失败" ) );
                }
            }
            catch( Exception ex )
            {
                logentity.otype = "1";
                logentity.logcontent += "," + ex.Message;
                operatelog.Add( logentity );
                context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
            }
        }
        private void UpdateDeviceClassEmptyDate( HttpContext context )
        {
            try
            {
                List<string> param = new List<string>() { "classCode", "emptydate" };
                if( !CheckParameters( param ) )
                {
                    return;
                }
                logentity.logcontent = Helper.ObjectToString( context.Request.Form["classCode"] );

                bllDeviceTaskInfo bll = new bllDeviceTaskInfo();
                string classCode = context.Request.Form["classCode"];
                string emptydate = context.Request.Form["emptydate"];
                DateTime dtDate;
                if( DateTime.TryParse( emptydate, out dtDate ) )
                {
                    if( bll.UpdateDeviceClassEmptyDateByClassCode( classCode, emptydate ) == 0 )
                    {
                        context.Response.Write( JsonHelper.ToJsonResult( "0", "成功" ) );
                    }
                    else
                    {
                        logentity.otype = "0";
                        logentity.logcontent += ",修改数据库返回内容错误";
                        operatelog.Add( logentity );
                        context.Response.Write( JsonHelper.ToJsonResult( "1", "失败" ) );
                    }
                }
                else
                {
                    logentity.otype = "0";
                    logentity.logcontent += ",参数值错误:" + emptydate;
                    operatelog.Add( logentity );
                    context.Response.Write( JsonHelper.ToJsonResult( "1", logentity.logcontent ) );
                }
            }
            catch( Exception ex )
            {
                logentity.otype = "1";
                logentity.logcontent += "," + ex.Message;
                operatelog.Add( logentity );
                context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
            }
        }
        private void UpdateDeviceClassTastDate( HttpContext context )
        {
            try
            {
                List<string> param = new List<string>() { "classCode", "tastdate" };

                if( !CheckParameters( param ) )
                {
                    return;
                }
                logentity.logcontent = Helper.ObjectToString( context.Request.Form["classCode"] );
                bllDeviceTaskInfo bll = new bllDeviceTaskInfo();
                string classCode = context.Request.Form["classCode"];
                string tastdate = context.Request.Form["tastdate"];
                DateTime dtDate;
                if( DateTime.TryParse( tastdate, out dtDate ) )
                {
                    if( bll.UpdateDeviceClassTastDateByClassCode( classCode, tastdate ) == 0 )
                    {
                        context.Response.Write( JsonHelper.ToJsonResult( "0", "成功" ) );
                    }
                    else
                    {
                        logentity.otype = "0";
                        logentity.logcontent += ",修改数据库返回内容错误";
                        operatelog.Add( logentity );
                        context.Response.Write( JsonHelper.ToJsonResult( "1", "失败" ) );
                    }
                }
                else
                {
                    logentity.otype = "0";
                    logentity.logcontent += ",参数值错误:" + tastdate;
                    operatelog.Add( logentity );
                    context.Response.Write( JsonHelper.ToJsonResult( "1", logentity.logcontent ) );
                }
            }
            catch( Exception ex )
            {
                logentity.otype = "1";
                logentity.logcontent += "," + ex.Message;
                operatelog.Add( logentity );
                context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
            }
        }
        private void GetECCVersionList( HttpContext context )
        {
            try
            {
                bllDeviceTaskInfo bll = new bllDeviceTaskInfo();
                dt = bll.GetECCVersionList();
                ReturnListJson( dt );
            }
            catch( Exception ex )
            {
                logentity.otype = "1";
                logentity.logcontent = ex.Message;
                operatelog.Add( logentity );
                context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
            }
        }
        private void UpdateDeviceClassVersion( HttpContext context )
        {
            try
            {
                List<string> param = new List<string>() { "classCode", "version" };

                if( !CheckParameters( param ) )
                {
                    return;
                }

                bllDeviceTaskInfo bll = new bllDeviceTaskInfo();
                string classCode = context.Request.Form["classCode"];
                string Version = context.Request.Form["version"];

                int result = bll.UpdateDeviceClassVersionByClassCode( classCode, Version );
                if( result == 0 )
                {
                    context.Response.Write( JsonHelper.ToJsonResult( "0", "成功" ) );
                }
                else
                {
                    logentity.otype = "0";
                    logentity.logcontent += ",修改数据库返回内容错误" + result;
                    operatelog.Add( logentity );
                    context.Response.Write( JsonHelper.ToJsonResult( "1", "失败" ) );
                }
            }
            catch( Exception ex )
            {
                logentity.otype = "1";
                logentity.logcontent += "," + ex.Message;
                operatelog.Add( logentity );
                context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
            }
        }
        private void WeChatPushMessageToECC( HttpContext context )
        {
            try
            {
                //消息来源
                int messagesourseid = 4;
                if( context.Request.Form["messagesourceid"] != null )
                {
                    messagesourseid = Helper.StringToInt( context.Request.Form["messagesourceid"].ToString() );

                }
                logentity.functionName = logentity.functionName + "--" + (EnumMessageSourceID) messagesourseid;
                List<string> param = new List<string>() { "usercode", "username" };
                if( !CheckParameters( param ) )
                {
                    return;
                }

                //获取配置文件路径
                string strurl = "/uploads/" + DateTime.Now.ToString( "yyyyMMdd" ) + "/";//拼接上传路径   上传文件的相对路径
                string strpath = context.Server.MapPath( strurl );//上传文件绝对路径  
                if( !Directory.Exists( strpath ) )
                {
                    //若不存在则创建
                    Directory.CreateDirectory( strpath );
                }

                dynamic dyn = new { url = strurl, path = strpath };
                Dictionary<string, string> imagelist = new Dictionary<string, string>();

                for( int i = 0; i < HttpContext.Current.Request.Files.Count; i++ )
                {


                    HttpPostedFile file = HttpContext.Current.Request.Files[i];
                    Stream stream = file.InputStream;
                    string filname = Path.GetFileName( file.FileName );//上传文件名称

                    TimeSpan ts = DateTime.UtcNow - new DateTime( 1970, 1, 1, 0, 0, 0, 0 );
                    string filecode = Convert.ToInt64( ts.TotalMilliseconds ).ToString() + ( i + 1 ).ToString( "00" );//生成唯一码
                    string newfilename = DateTime.Now.ToString( "yyyyMMddHHmmss" ) + filecode + Path.GetExtension( file.FileName );

                    string path = dyn.path + newfilename;//文件绝对路径
                    string url = dyn.url + newfilename;//文件相对路径
                    string thumbnailPath = dyn.path + "thumbnail" + newfilename;//缩略图的绝对路径
                    string thumbnailUrl = dyn.url + "thumbnail" + newfilename;//缩略图的相对路径
                    int index = HttpContext.Current.Request.Url.ToString().IndexOf( "Service" );
                    string ip = HttpContext.Current.Request.Url.ToString().Substring( 0, index - 1 );

                    imagelist.Add( ip + url, filname );//图片的访问路径和源文件名称

                    //保存文件
                    file.SaveAs( path );
                    System.Drawing.Image image1 = null;
                    try
                    {
                        image1 = System.Drawing.Image.FromStream( stream );

                    }
                    catch
                    {
                        context.Response.Write( JsonHelper.ToJsonResult( "1", "图片无法解析" ) );
                        return;
                    }
                    int thumbnailImageWidth = Helper.StringToInt( Helper.GetAppSettings( "thumbnailImageWidth" ) );
                    int thumbnailImageHeight = Helper.StringToInt( Helper.GetAppSettings( "thumbnailImageHeight" ) );
                    FileHelper.MakeThumbnail( image1, thumbnailPath, thumbnailImageWidth, thumbnailImageHeight );
                    bllDataFieldInfo bll = new bllDataFieldInfo();
                    DataFieldInfoEntity entity = new DataFieldInfoEntity();
                    string errormsg = "";
                    int errorcode = 0;
                    entity.FieldCode = filecode;
                    entity.FieldName1 = filname;//上传文件名称
                    entity.FieldContent = url;//保存文件相对路径
                    entity.FieldTypeID = 1;
                    bll.Add( ref entity, new operatelogEntity(), out errorcode, out errormsg );
                }

                JPushClient client = new JPushClient( Helper.GetAppSettings( "AppKey" ), Helper.GetAppSettings( "MasterSecret" ) );


                //布局类型
                int displaymodelid = 1;//班级模式

                //消息类型
                int messagetypeid = 1;//新闻通知

                //操作类型--默认为追加
                int operatetypeid = Helper.StringToInt( context.Request.Form["operatetypeid"] ) > 0 ? Helper.StringToInt( context.Request.Form["operatetypeid"] ) : 1;//operatetypeid

                //优先级
                int taskpriorityid = 1;//1级
                //任务状态
                int taskstatusid = 1;//正常

                //任务类型
                int tasktypeid = 1;//普通任务
                //发送人校区编号
                string userCode = context.Request.Form["userCode"].ToString();

                string rootCode = Helper.ObjectToString( context.Request.Form["rootCode"] );
                //操作人姓名
                string createdname = context.Request.Form["username"].ToString();
                string messagecontent_text = Helper.ObjectToString( context.Request.Form["messagecontent_text"] );
                //图片显示秒数
                int imagespansecond = Helper.ObjectToInt( context.Request.Form["imagespansecond"] ) > 0 ? Helper.ObjectToInt( context.Request.Form["imagespansecond"] ) : 5;

                //图片显示效果类型
                int imageeffectid = Helper.ObjectToInt( context.Request.Form["imageeffectid"] ) > 0 ? Helper.ObjectToInt( context.Request.Form["imageeffectid"] ) : 1;


                bllUserClassInfo classInfo = new bllUserClassInfo();

                dt = classInfo.GetUserClassList( userCode, rootCode, true );

                if( dt != null && dt.Rows.Count > 0 )
                {
                    PushPayload payload = new PushPayload();
                    DeviceTaskInfoEntity deviceTaskInfo = new DeviceTaskInfoEntity();
                    payload.platform = Platform.all();
                    payload.audience = Audience.all();
                    payload.audience = payload.audience.tag_and( Helper.GetAppSettings( "schoolCode" ) ).alias( "U4" );

                    deviceTaskInfo.Tag_and = "[\"" + Helper.GetAppSettings( "schoolCode" ) + "\"]";
                    deviceTaskInfo.Alias = "[\"U4\"]";

                    deviceTaskInfo.RargetAlias = "";
                    //有可能一个教室管理有两个校区不同的班级,则一块发送，所以不能写校区
                    ArrayList jpushidarray = new ArrayList();

                    for( int i = 0; i < dt.Rows.Count; i++ )
                    {

                        if( dt.Rows[i]["JPushID"].ToString() != "" )
                        {
                            //添加校区，和JPushID
                            payload.audience = payload.audience.registrationId( dt.Rows[i]["JPushID"].ToString() );
                            jpushidarray.Add( dt.Rows[i]["JPushID"].ToString() );
                            deviceTaskInfo.RargetAlias += dt.Rows[i]["ClassName"].ToString() + ",";
                        }
                    }
                    if( deviceTaskInfo.RargetAlias.Length > 0 )
                    {
                        deviceTaskInfo.RargetAlias = deviceTaskInfo.RargetAlias.Remove( deviceTaskInfo.RargetAlias.Length - 1, 1 );
                    }
                    if( jpushidarray.Count > 0 )
                    {
                        string strjpushid = "[";
                        for( int j = 0; j < jpushidarray.Count; j++ )
                        {
                            strjpushid += "\"" + jpushidarray[j].ToString() + "\",";
                        }
                        if( strjpushid.Length > 0 )
                        {
                            strjpushid = strjpushid.Remove( strjpushid.Length - 1, 1 );
                        }
                        strjpushid += "]";
                        deviceTaskInfo.Registration_ID = strjpushid;
                    }
                    payload.options = new Options();
                    payload.options.apns_production = true;
                    payload.options.time_to_live = 86400;//(1天)

                    deviceTaskInfo.DisplayModelID = displaymodelid;
                    deviceTaskInfo.MessageSourceID = messagesourseid;
                    deviceTaskInfo.MessageTypeID = messagetypeid;
                    deviceTaskInfo.OperateTypeID = operatetypeid;
                    deviceTaskInfo.MessageTitle = "";
                    deviceTaskInfo.MessageContent = new MessageContent();
                    deviceTaskInfo.TaskPriorityID = taskpriorityid;
                    deviceTaskInfo.TaskStatusID = taskstatusid;
                    deviceTaskInfo.TaskTypeID = tasktypeid;
                    deviceTaskInfo.MessageContent.text = messagecontent_text;
                    string[] imageArray = new string[imagelist.Count];
                    string[] imagenameArray = new string[imagelist.Count];
                    imagelist.Keys.CopyTo( imageArray, 0 );

                    imagelist.Values.CopyTo( imagenameArray, 0 );
                    deviceTaskInfo.MessageContent.image = imageArray;
                    deviceTaskInfo.MessageContentAlias = new MessageContent();
                    deviceTaskInfo.MessageContentAlias.image = imagenameArray;
                    deviceTaskInfo.MessageContent.video = null;//不能发送视频

                    deviceTaskInfo.MessageContentAlias.text = "";

                    deviceTaskInfo.MessageContentAlias.video = null;
                    deviceTaskInfo.ImageEffectID = imageeffectid;
                    deviceTaskInfo.ImageSpanSecond = imagespansecond;
                    deviceTaskInfo.TargetRange = payload.audience != null ? JsonHelper.ObjectToJSON( payload.audience.dictionary ) : "";


                    deviceTaskInfo.CreatedID = userCode;
                    deviceTaskInfo.CreatedName = createdname;

                    //操作=
                    bllDeviceTaskInfo bll = new bllDeviceTaskInfo();
                    int errorcode = 0; string errormsg = string.Empty;
                    try
                    {
                        if( bll.Add( ref deviceTaskInfo, logentity, rootCode, out errorcode, out errormsg ) == 0 )
                        {
                            #region MyRegion
                            try
                            {
                                CustomMsgContent customMsgContent = JsonHelper.ObjectToObject<DeviceTaskInfoEntity, CustomMsgContent>( deviceTaskInfo );
                                payload.message = Message.content( customMsgContent );

                                MessageResult result = client.SendPush( payload );


                                //操作
                                if( result.isResultOK() )
                                {
                                    context.Response.Write( JsonHelper.ToJsonResult( "0", "success" ) );
                                    return;
                                }
                                else
                                {
                                    bll.UpdateTaskResultID( deviceTaskInfo.Code, "1" );
                                    logentity.otype = "0";
                                    logentity.logcontent = "发送消息失败";
                                    operatelog.Add( logentity );
                                    context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
                                    return;
                                }
                            }
                            catch( Exception ex )
                            {
                                bll.UpdateTaskResultID( deviceTaskInfo.Code, "1" );
                                logentity.otype = "1";
                                logentity.logcontent = "极光推送报错" + ex.Message;
                                operatelog.Add( logentity );
                                context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
                                return;
                            }
                            #endregion
                        }
                        else
                        {
                            logentity.otype = "0";
                            logentity.logcontent = "插入消息任务到数据库失败";
                            operatelog.Add( logentity );
                            context.Response.Write( JsonHelper.ToJsonResult( errorcode.ToString(), errormsg ) );
                            return;
                        }
                    }
                    catch( Exception ex )
                    {
                        logentity.otype = "1";
                        logentity.logcontent = ex.Message;
                        operatelog.Add( logentity );
                        context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
                        return;
                    }
                }
                else
                {
                    logentity.otype = "0";
                    logentity.logcontent = "请检查该教师编号和校区编号或者确认该教师隶属班级电子班牌是否已经完成初始化";
                    operatelog.Add( logentity );
                    context.Response.Write( JsonHelper.ToJsonResult( "1", "请检查该教师编号和校区编号或者确认该教师隶属班级电子班牌是否已经完成初始化" ) );
                    return;
                }
            }
            catch( Exception ex )
            {
                logentity.otype = "1";
                logentity.logcontent = ex.Message;
                operatelog.Add( logentity );
                context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
            }


        }
        /// <summary>
        /// 微信推送接口（20170717 zhc 改）
        /// </summary>
        /// <param name="context"></param>
        private void WeChatPushMessageToECCNew( HttpContext context )
        {
            try
            {
                //消息来源
                int messagesourseid = 4;
                if( context.Request.Form["messagesourceid"] != null )
                {
                    messagesourseid = Helper.StringToInt( context.Request.Form["messagesourceid"].ToString() );

                }
                logentity.functionName = logentity.functionName + "--" + (EnumMessageSourceID) messagesourseid;
                List<string> param = new List<string>() { "usercode", "username" };
                if( !CheckParameters( param ) )
                {
                    return;
                }

                JPushClient client = new JPushClient( Helper.GetAppSettings( "AppKey" ), Helper.GetAppSettings( "MasterSecret" ) );


                //布局类型
                int displaymodelid = Helper.StringToInt( context.Request.Form["displaymodelid"] ) > 0 ? Helper.StringToInt( context.Request.Form["displaymodelid"] ) : 9;//displaymodelid

                //消息类型
                int messagetypeid = 1;//新闻通知

                //操作类型--默认为追加
                int operatetypeid = Helper.StringToInt( context.Request.Form["operatetypeid"] ) > 0 ? Helper.StringToInt( context.Request.Form["operatetypeid"] ) : 1;//operatetypeid

                //优先级
                int taskpriorityid = 1;//1级
                //任务状态
                int taskstatusid = 1;//正常

                //任务类型
                int tasktypeid = 1;//普通任务
                //发送人校区编号
                string userCode = context.Request.Form["userCode"].ToString();

                string rootCode = Helper.ObjectToString( context.Request.Form["rootCode"] );
                //操作人姓名
                string createdname = context.Request.Form["username"].ToString();

                bllUserClassInfo classInfo = new bllUserClassInfo();

                dt = classInfo.GetUserClassList( userCode, rootCode, true );

                if( dt != null && dt.Rows.Count > 0 )
                {
                    PushPayload payload = new PushPayload();
                    DeviceTaskInfoEntity deviceTaskInfo = new DeviceTaskInfoEntity();
                    payload.platform = Platform.all();
                    payload.audience = Audience.all();
                    payload.audience = payload.audience.tag_and( Helper.GetAppSettings( "schoolCode" ) ).alias( "U4" );

                    deviceTaskInfo.Tag_and = "[\"" + Helper.GetAppSettings( "schoolCode" ) + "\"]";
                    deviceTaskInfo.Alias = "[\"U4\"]";

                    deviceTaskInfo.RargetAlias = "";
                    //有可能一个教室管理有两个校区不同的班级,则一块发送，所以不能写校区
                    ArrayList jpushidarray = new ArrayList();

                    for( int i = 0; i < dt.Rows.Count; i++ )
                    {

                        if( dt.Rows[i]["JPushID"].ToString() != "" )
                        {
                            //添加校区，和JPushID
                            payload.audience = payload.audience.registrationId( dt.Rows[i]["JPushID"].ToString() );
                            jpushidarray.Add( dt.Rows[i]["JPushID"].ToString() );
                            deviceTaskInfo.RargetAlias += dt.Rows[i]["ClassName"].ToString() + ",";
                        }
                    }
                    if( deviceTaskInfo.RargetAlias.Length > 0 )
                    {
                        deviceTaskInfo.RargetAlias = deviceTaskInfo.RargetAlias.Remove( deviceTaskInfo.RargetAlias.Length - 1, 1 );
                    }
                    if( jpushidarray.Count > 0 )
                    {
                        string strjpushid = "[";
                        for( int j = 0; j < jpushidarray.Count; j++ )
                        {
                            strjpushid += "\"" + jpushidarray[j].ToString() + "\",";
                        }
                        if( strjpushid.Length > 0 )
                        {
                            strjpushid = strjpushid.Remove( strjpushid.Length - 1, 1 );
                        }
                        strjpushid += "]";
                        deviceTaskInfo.Registration_ID = strjpushid;
                    }
                    payload.options = new Options();
                    payload.options.apns_production = true;
                    payload.options.time_to_live = 86400;//(1天)

                    deviceTaskInfo.DisplayModelID = displaymodelid;
                    deviceTaskInfo.MessageSourceID = messagesourseid;
                    deviceTaskInfo.MessageTypeID = messagetypeid;
                    deviceTaskInfo.OperateTypeID = operatetypeid;
                    deviceTaskInfo.MessageTitle = "";
                    deviceTaskInfo.MessageContent = new MessageContent();
                    deviceTaskInfo.TaskPriorityID = taskpriorityid;
                    deviceTaskInfo.TaskStatusID = taskstatusid;
                    deviceTaskInfo.TaskTypeID = tasktypeid;
                    deviceTaskInfo.MessageContentAlias = new MessageContent();
                    deviceTaskInfo.MessageContentAlias.text = "";
                    deviceTaskInfo.MessageContentAlias.image = null;
                    deviceTaskInfo.MessageContentAlias.video = null;

                    //zhc 模版信息加载
                    object ClsActiveStr = context.Request.Form["ClsActive"];
                    object ClsHonorStr = context.Request.Form["ClsHonor"];
                    object ClsHomeWkStr = context.Request.Form["ClsHomeWk"];
                    object ClsCheckItemStr = context.Request.Form["ClsCheckItem"];
                    object ClsCheckStuStr = context.Request.Form["ClsCheckStu"];
                    object ClsNoticeStr = context.Request.Form["ClsNotice"];

                    if( ClsActiveStr != null || ClsHonorStr != null || ClsHomeWkStr != null || ClsCheckItemStr != null || ClsCheckStuStr != null || ClsNoticeStr != null )
                    {
                        deviceTaskInfo.MessageContent.AreaModule = new MessageContent.AreaModuleClass();
                        //班级活动
                        if( ClsActiveStr != null )
                        {
                            deviceTaskInfo.MessageContent.AreaModule.ClsActive = new MessageContent.ClsActive();
                            deviceTaskInfo.MessageContent.AreaModule.ClsActive = JsonHelper.JsonToObject<MessageContent.ClsActive>( ClsActiveStr.ToString() );
                        }
                        //班级荣誉 list添加
                        if( ClsHonorStr != null )
                        {
                            deviceTaskInfo.MessageContent.AreaModule.ClsHonor = new List<MessageContent.ClsHonor>();
                            MessageContent.ClsHonor Honor = JsonHelper.JsonToObject<MessageContent.ClsHonor>( ClsHonorStr.ToString() );
                            deviceTaskInfo.MessageContent.AreaModule.ClsHonor.Add( Honor );
                        }
                        //作业布置 list添加
                        if( ClsHomeWkStr != null )
                        {
                            deviceTaskInfo.MessageContent.AreaModule.ClsHomeWk = new List<MessageContent.ClsHomeWk>();
                            MessageContent.ClsHomeWk HomeWk = JsonHelper.JsonToObject<MessageContent.ClsHomeWk>( ClsHomeWkStr.ToString() );
                            deviceTaskInfo.MessageContent.AreaModule.ClsHomeWk.Add( HomeWk );
                        }
                        //指标检查考勤 list添加
                        if( ClsCheckItemStr != null )
                        {
                            deviceTaskInfo.MessageContent.AreaModule.ClsCheckItem = new List<MessageContent.ClsCheckItem>();
                            MessageContent.ClsCheckItem CheckItem = JsonHelper.JsonToObject<MessageContent.ClsCheckItem>( ClsCheckItemStr.ToString() );
                            deviceTaskInfo.MessageContent.AreaModule.ClsCheckItem.Add( CheckItem );
                        }
                        //学生出勤考勤
                        if( ClsCheckStuStr != null )
                        {
                            deviceTaskInfo.MessageContent.AreaModule.ClsCheckStu = new MessageContent.ClsCheckStu();
                            deviceTaskInfo.MessageContent.AreaModule.ClsCheckStu = JsonHelper.JsonToObject<MessageContent.ClsCheckStu>( ClsCheckStuStr.ToString() );
                        }
                    }

                    deviceTaskInfo.TargetRange = payload.audience != null ? JsonHelper.ObjectToJSON( payload.audience.dictionary ) : "";


                    deviceTaskInfo.CreatedID = userCode;
                    deviceTaskInfo.CreatedName = createdname;

                    //操作=
                    bllDeviceTaskInfo bll = new bllDeviceTaskInfo();
                    int errorcode = 0; string errormsg = string.Empty;
                    try
                    {
                        if( bll.Add( ref deviceTaskInfo, logentity, rootCode, out errorcode, out errormsg ) == 0 )
                        {
                            #region MyRegion
                            try
                            {
                                //通知公告
                                if( ClsNoticeStr != null )
                                {


                                    string NoticeStr = HttpContext.Current.Server.UrlDecode( ClsNoticeStr.ToString() );

                                    DeviceTaskNoticeInfo Notice = JsonHelper.JsonToObject<DeviceTaskNoticeInfo>( NoticeStr );
                                    Notice.taskCode = deviceTaskInfo.Code;
                                    deviceTaskInfo.MessageContent.AreaModule.ClsNotice = new List<MessageContent.ClsNotice>();

                                    if( bll.AddNotice( ref Notice ) == 0 )
                                    {
                                        MessageContent.ClsNotice ClsNotice1 = new MessageContent.ClsNotice();
                                        ClsNotice1.title = Notice.noticeTitle;
                                        ClsNotice1.context = Notice.noticeContent;
                                        ClsNotice1.date = Notice.noticeTime;
                                        ClsNotice1.code = deviceTaskInfo.Code;
                                        ClsNotice1.url = "NoticeInfo.html?id=" + Notice.noticeId;
                                        deviceTaskInfo.MessageContent.AreaModule.ClsNotice.Add( ClsNotice1 );
                                    }
                                }
                                if( ClsActiveStr != null )
                                {
                                    deviceTaskInfo.MessageContent.AreaModule.ClsActive.code = deviceTaskInfo.Code;//撤销Code
                                }
                                if( ClsCheckStuStr != null )
                                {
                                    deviceTaskInfo.MessageContent.AreaModule.ClsCheckStu.code = deviceTaskInfo.Code;//撤销Code
                                }

                                if( ClsCheckItemStr != null )
                                {
                                    for( int i = 0; i < deviceTaskInfo.MessageContent.AreaModule.ClsCheckItem.Count; i++ )
                                    {
                                        deviceTaskInfo.MessageContent.AreaModule.ClsCheckItem[i].code = deviceTaskInfo.Code;//撤销Code
                                    }

                                }
                                if( ClsHonorStr != null )
                                {
                                    for( int i = 0; i < deviceTaskInfo.MessageContent.AreaModule.ClsHonor.Count; i++ )
                                    {
                                        deviceTaskInfo.MessageContent.AreaModule.ClsHonor[i].code = deviceTaskInfo.Code;//撤销Code
                                    }
                                }
                                if( ClsHomeWkStr != null )
                                {
                                    for( int i = 0; i < deviceTaskInfo.MessageContent.AreaModule.ClsHomeWk.Count; i++ )
                                    {
                                        deviceTaskInfo.MessageContent.AreaModule.ClsHomeWk[i].code = deviceTaskInfo.Code;//撤销Code
                                    }
                                }

                                CustomMsgContent customMsgContent = JsonHelper.ObjectToObject<DeviceTaskInfoEntity, CustomMsgContent>( deviceTaskInfo );
                                payload.message = Message.content( customMsgContent );

                                MessageResult result = client.SendPush( payload );


                                //操作
                                if( result.isResultOK() )
                                {
                                    context.Response.Write( JsonHelper.ToJsonResult( "0", "success" ) );
                                    return;
                                }
                                else
                                {
                                    bll.UpdateTaskResultID( deviceTaskInfo.Code, "1" );
                                    logentity.otype = "0";
                                    logentity.logcontent = "发送消息失败";
                                    operatelog.Add( logentity );
                                    context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
                                    return;
                                }
                            }
                            catch( Exception ex )
                            {
                                bll.UpdateTaskResultID( deviceTaskInfo.Code, "1" );
                                logentity.otype = "1";
                                logentity.logcontent = "极光推送报错" + ex.Message;
                                operatelog.Add( logentity );
                                context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
                                return;
                            }
                            #endregion
                        }
                        else
                        {
                            logentity.otype = "0";
                            logentity.logcontent = "插入消息任务到数据库失败";
                            operatelog.Add( logentity );
                            context.Response.Write( JsonHelper.ToJsonResult( errorcode.ToString(), errormsg ) );
                            return;
                        }
                    }
                    catch( Exception ex )
                    {
                        logentity.otype = "1";
                        logentity.logcontent = ex.Message;
                        operatelog.Add( logentity );
                        context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
                        return;
                    }
                }
                else
                {
                    logentity.otype = "0";
                    logentity.logcontent = "请检查该教师编号和校区编号或者确认该教师隶属班级电子班牌是否已经完成初始化";
                    operatelog.Add( logentity );
                    context.Response.Write( JsonHelper.ToJsonResult( "1", "请检查该教师编号和校区编号或者确认该教师隶属班级电子班牌是否已经完成初始化" ) );
                    return;
                }
            }
            catch( Exception ex )
            {
                logentity.otype = "1";
                logentity.logcontent = ex.Message;
                operatelog.Add( logentity );
                context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
            }


        }

        /// <summary>
        /// 微信推送进入设置界面接口（20170717 zhc 改）
        /// </summary>
        /// <param name="context"></param>
        private void WeChatPushMessageToInterface( HttpContext context )
        {
            try
            {
                //消息来源
                int messagesourseid = 4;
                if( context.Request.Form["messagesourceid"] != null )
                {
                    messagesourseid = Helper.StringToInt( context.Request.Form["messagesourceid"].ToString() );

                }
                logentity.functionName = logentity.functionName + "--" + (EnumMessageSourceID) messagesourseid;
                List<string> param = new List<string>() { "usercode", "username", "classcode" };
                if( !CheckParameters( param ) )
                {
                    return;
                }

                JPushClient client = new JPushClient( Helper.GetAppSettings( "AppKey" ), Helper.GetAppSettings( "MasterSecret" ) );

                //消息类型
                int messagetypeid = 1;//新闻通知

                //操作类型--默认为进入设置界面
                int operatetypeid = 21;//operatetypeid
                //布局类型
                int displaymodelid = 1;//displaymodelid
                //优先级
                int taskpriorityid = 1;//1级
                //任务状态
                int taskstatusid = 1;//正常

                //任务类型
                int tasktypeid = 1;//普通任务
                //发送人校区编号
                string userCode = context.Request.Form["userCode"].ToString();

                string rootCode = Helper.ObjectToString( context.Request.Form["rootCode"] );

                string ClassCode = context.Request.Form["classcode"].ToString();

                string campusCode = context.Request.Form["campuscode"].ToString();

                //操作人姓名
                string createdname = context.Request.Form["username"].ToString();

                bllUserClassInfo classInfo = new bllUserClassInfo();

                dt = classInfo.GetClassList( userCode, ClassCode );

                if( dt != null && dt.Rows.Count > 0 )
                {
                    PushPayload payload = new PushPayload();
                    DeviceTaskInfoEntity deviceTaskInfo = new DeviceTaskInfoEntity();
                    payload.platform = Platform.all();
                    payload.audience = Audience.all();
                    string schoolCode = "[\"" + Helper.GetAppSettings( "schoolCode" ) + "\",\"RC" + campusCode.Substring( 6, 4 ) + "\"]";
                    string[] schoolCodetag_and = new string[] { Helper.GetAppSettings( "schoolCode" ), "RC" + campusCode.Substring( 6, 4 ) };
                    deviceTaskInfo.Tag_and = schoolCode;
                    deviceTaskInfo.Alias = "[\"U4\"]";
                    payload.audience = payload.audience.tag_and( schoolCodetag_and ).alias( "U4" );

                    deviceTaskInfo.RargetAlias = "";
                    //有可能一个教室管理有两个校区不同的班级,则一块发送，所以不能写校区
                    ArrayList jpushidarray = new ArrayList();

                    for( int i = 0; i < dt.Rows.Count; i++ )
                    {

                        if( dt.Rows[i]["JPushID"].ToString() != "" )
                        {
                            //添加校区，和JPushID
                            payload.audience = payload.audience.registrationId( dt.Rows[i]["JPushID"].ToString() );
                            jpushidarray.Add( dt.Rows[i]["JPushID"].ToString() );
                            deviceTaskInfo.RargetAlias += dt.Rows[i]["ClassName"].ToString() + ",";
                        }
                    }

                    if( deviceTaskInfo.RargetAlias.Length > 0 )
                    {
                        deviceTaskInfo.RargetAlias = deviceTaskInfo.RargetAlias.Remove( deviceTaskInfo.RargetAlias.Length - 1, 1 );
                    }
                    if( jpushidarray.Count > 0 )
                    {
                        string strjpushid = "[";
                        for( int j = 0; j < jpushidarray.Count; j++ )
                        {
                            strjpushid += "\"" + jpushidarray[j].ToString() + "\",";
                        }
                        if( strjpushid.Length > 0 )
                        {
                            strjpushid = strjpushid.Remove( strjpushid.Length - 1, 1 );
                        }
                        strjpushid += "]";
                        deviceTaskInfo.Registration_ID = strjpushid;
                    }


                    payload.options = new Options();
                    payload.options.apns_production = true;
                    payload.options.time_to_live = 86400;//(1天)

                    deviceTaskInfo.DisplayModelID = 1;
                    deviceTaskInfo.MessageSourceID = messagesourseid;
                    deviceTaskInfo.MessageTypeID = messagetypeid;
                    deviceTaskInfo.OperateTypeID = operatetypeid;
                    deviceTaskInfo.MessageTitle = "";
                    deviceTaskInfo.MessageContent = new MessageContent();
                    deviceTaskInfo.TaskPriorityID = taskpriorityid;
                    deviceTaskInfo.TaskStatusID = taskstatusid;
                    deviceTaskInfo.TaskTypeID = tasktypeid;
                    deviceTaskInfo.MessageContentAlias = new MessageContent();
                    deviceTaskInfo.MessageContentAlias.text = "";
                    deviceTaskInfo.MessageContentAlias.image = null;
                    deviceTaskInfo.MessageContentAlias.video = null;
                    deviceTaskInfo.MessageContentAlias.AreaModule = null;

                    deviceTaskInfo.TargetRange = payload.audience != null ? JsonHelper.ObjectToJSON( payload.audience.dictionary ) : "";

                    deviceTaskInfo.DisplayModelID = displaymodelid;
                    deviceTaskInfo.CreatedID = userCode;
                    deviceTaskInfo.CreatedName = createdname;

                    //操作=
                    bllDeviceTaskInfo bll = new bllDeviceTaskInfo();
                    int errorcode = 0; string errormsg = string.Empty;
                    try
                    {
                        if( bll.Add( ref deviceTaskInfo, logentity, rootCode, out errorcode, out errormsg ) == 0 )
                        {
                            #region MyRegion
                            try
                            {
                                CustomMsgContent customMsgContent = JsonHelper.ObjectToObject<DeviceTaskInfoEntity, CustomMsgContent>( deviceTaskInfo );
                                payload.message = Message.content( customMsgContent );

                                MessageResult result = client.SendPush( payload );


                                //操作
                                if( result.isResultOK() )
                                {
                                    context.Response.Write( JsonHelper.ToJsonResult( "0", "success" ) );
                                    return;
                                }
                                else
                                {
                                    bll.UpdateTaskResultID( deviceTaskInfo.Code, "1" );
                                    logentity.otype = "0";
                                    logentity.logcontent = "发送消息失败";
                                    operatelog.Add( logentity );
                                    context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
                                    return;
                                }
                            }
                            catch( Exception ex )
                            {
                                bll.UpdateTaskResultID( deviceTaskInfo.Code, "1" );
                                logentity.otype = "1";
                                logentity.logcontent = "极光推送报错" + ex.Message;
                                operatelog.Add( logentity );
                                context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
                                return;
                            }
                            #endregion
                        }
                        else
                        {
                            logentity.otype = "0";
                            logentity.logcontent = "插入消息任务到数据库失败";
                            operatelog.Add( logentity );
                            context.Response.Write( JsonHelper.ToJsonResult( errorcode.ToString(), errormsg ) );
                            return;
                        }
                    }
                    catch( Exception ex )
                    {
                        logentity.otype = "1";
                        logentity.logcontent = ex.Message;
                        operatelog.Add( logentity );
                        context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
                        return;
                    }
                }
                else
                {
                    logentity.otype = "0";
                    logentity.logcontent = "请检查该教师编号和校区编号或者确认该教师隶属班级电子班牌是否已经完成初始化";
                    operatelog.Add( logentity );
                    context.Response.Write( JsonHelper.ToJsonResult( "1", "请检查该教师编号和校区编号或者确认该教师隶属班级电子班牌是否已经完成初始化" ) );
                    return;
                }
            }
            catch( Exception ex )
            {
                logentity.otype = "1";
                logentity.logcontent = ex.Message;
                operatelog.Add( logentity );
                context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
            }


        }
        private void DeleteTask( HttpContext context )
        {
            try
            {
                List<string> param = new List<string>() { "ids" };
                if( !CheckParameters( param ) )
                {
                    return;
                }
                string ids = context.Request.Form["ids"].ToString();
                bllDeviceTaskInfo bll = new bllDeviceTaskInfo();
                int result = bll.DeleteDeviceTask( ids );
                if( result == 0 )
                {
                    context.Response.Write( JsonHelper.ToJsonResult( "0", "success" ) );
                }
                else
                {
                    logentity.otype = "0";
                    logentity.logcontent = "删除任务失败";
                    operatelog.Add( logentity );
                    context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
                }
            }
            catch( Exception ex )
            {
                logentity.otype = "1";
                logentity.logcontent = ex.Message;
                operatelog.Add( logentity );
            }
        }
        private void CancelTask( HttpContext context )
        {
            List<string> param = new List<string>() { "code", "modifiedid", "modifiedname" };
            logentity.logcontent = Helper.ObjectToString( context.Request.Form["code"].ToString() );
            if( !CheckParameters( param ) )
            {
                return;
            }
            bllDeviceTaskInfo bll = new bllDeviceTaskInfo();
            string code = context.Request.Form["code"].ToString();
            string filter = string.Format( "where code='{0}'", code );
            string modifiedid = context.Request.Form["modifiedid"].ToString();
            string modifiedname = context.Request.Form["modifiedname"].ToString();
            dt = bll.GetPagingSigInfo( filter );
            PushPayload payload = new PushPayload();
            payload.platform = Platform.all();

            if( dt != null && dt.Rows.Count > 0 )
            {
                JPushClient client = new JPushClient( Helper.GetAppSettings( "AppKey" ), Helper.GetAppSettings( "MasterSecret" ) );
                var tag = Helper.ObjectToArrry( dt.Rows[0]["tag"].ToString() );
                var tag_and = Helper.ObjectToArrry( dt.Rows[0]["tag_and"].ToString() );
                var alias = Helper.ObjectToArrry( dt.Rows[0]["alias"].ToString() );
                var registration_id = Helper.ObjectToArrry( dt.Rows[0]["registration_id"].ToString() );
                if( tag != null || tag_and != null || alias != null || registration_id != null )
                {
                    payload.audience = Audience.all();
                    if( tag != null )
                    {
                        payload.audience = payload.audience.tag( tag );
                    }
                    if( tag_and != null )
                    {
                        payload.audience = payload.audience.tag_and( tag_and );
                    }
                    if( alias != null )
                    {
                        payload.audience = payload.audience.alias( alias );
                    }
                    if( registration_id != null )
                    {
                        payload.audience = payload.audience.registrationId( registration_id );
                    }
                    payload.options = new Options();
                    payload.options.apns_production = true;
                    payload.options.time_to_live = 86400;//(1天)
                    DeviceTaskInfoEntity deviceTaskInfo = new DeviceTaskInfoEntity();
                    deviceTaskInfo.OperateTypeID = 3;
                    deviceTaskInfo.DisplayModelID = Convert.ToInt32( dt.Rows[0]["DisplayModelID"].ToString() );
                    deviceTaskInfo.Code = code;
                    CustomMsgContent customMsgContent = JsonHelper.ObjectToObject<DeviceTaskInfoEntity, CustomMsgContent>( deviceTaskInfo );
                    payload.message = Message.content( customMsgContent );

                    payload.message.setTitle( "" );
                    try
                    {

                        MessageResult result = client.SendPush( payload );

                        bll.UpdateOperateTypeID( code, "3", modifiedid, modifiedname );
                        //操作
                        if( result.isResultOK() )
                        {
                            context.Response.Write( JsonHelper.ToJsonResult( "0", "success" ) );
                        }
                        else
                        {
                            logentity.otype = "0";
                            logentity.logcontent += ",发送消息失败";
                            operatelog.Add( logentity );
                            context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
                        }
                    }
                    catch( Exception ex )
                    {
                        logentity.otype = "1";
                        logentity.logcontent += "," + ex.Message;
                        operatelog.Add( logentity );
                        context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
                    }
                }
                else
                {
                    logentity.otype = "0";
                    logentity.logcontent += ",撤销任务时，没有找到接收对象";
                    operatelog.Add( logentity );
                    context.Response.Write( JsonHelper.ToJsonResult( "1", "请选择接收对象" ) );
                }


            }

        }
        private void GetTaskDetial( HttpContext context )
        {
            try
            {
                //必填参数
                List<string> param = new List<string>() { "id" };
                //必填参数检查
                if( !CheckParameters( param ) )
                {
                    return;
                }
                bllDeviceTaskInfo bll = new bllDeviceTaskInfo();


                string filter = "where TaskStatusID=1 and id=" + context.Request.Form["id"].ToString();

                dt = bll.GetPagingSigInfo( filter );

                ReturnListJson( dt );
            }
            catch( Exception ex )
            {
                logentity.otype = "1";
                logentity.logcontent = ex.Message;
                operatelog.Add( logentity );
                context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
            }
        }
        private void GetTaskList( HttpContext context )
        {
            try
            {
                //必填参数
                List<string> param = new List<string>() { "pageSize", "currentPage" };
                //必填参数检查
                if( !CheckParameters( param ) )
                {
                    return;
                }
                bllDeviceTaskInfo bll = new bllDeviceTaskInfo();
                int pageSize = Helper.StringToInt( context.Request.Form["pageSize"].ToString() );
                int currentPage = Helper.StringToInt( context.Request.Form["currentPage"].ToString() );

                string filter = "where TaskStatusID=1";//正常并且发送成功
                //可选参数
                Dictionary<string, EnumSearchType> optionalParameters = new Dictionary<string, EnumSearchType> { 
            { "messageSourceID", EnumSearchType.AndInt } 
            ,{ "displaymodelid", EnumSearchType.AndInt }
            ,{ "operatetypeid", EnumSearchType.AndInt }
            ,{ "tasktypeid", EnumSearchType.AndInt }
            ,{ "stime", EnumSearchType.AndSTime }
            ,{ "etime", EnumSearchType.AndETime }
            ,{ "createdid", EnumSearchType.AndString }
            };
                //可选参数检查
                filter = CheckOptionalParameters( optionalParameters, filter );
                if( context.Request.Form["rootcode"] != null && context.Request.Form["rootcode"].ToString().Trim() != "" )
                {
                    filter += string.Format( " and SUBSTRING(code,1,10)='{0}'", context.Request.Form["rootcode"].ToString() );
                }
                if( context.Request.Form["className"] != null && context.Request.Form["className"].ToString().Trim() != "" )
                {
                    filter += string.Format( " and LEN(Registration_ID)=23 and RargetAlias LIKE '%{0}%'", context.Request.Form["className"].ToString() );

                }
                string order = "ID desc";

                int recordCount = 0;
                int totalPage = 0;
                dt = bll.GetPagingListInfo( pageSize, currentPage, filter, order, out recordCount, out totalPage );

                ReturnListJson( dt, pageSize, recordCount, currentPage, totalPage );
            }
            catch( Exception ex )
            {
                logentity.otype = "1";
                logentity.logcontent = ex.Message;
                operatelog.Add( logentity );
                context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
            }
        }

        /// <summary>
        /// 推送消息到电子平板
        /// </summary>
        /// <param name="context"></param>
        private void PushJSONMessageToECC( HttpContext context )
        {
            try
            {
                List<string> param = new List<string>() { "devicetaskinfo", "pushpayload", "rootcode" };

                if( !CheckParameters( param ) )
                {
                    return;
                }
                JPushClient client = new JPushClient( Helper.GetAppSettings( "AppKey" ), Helper.GetAppSettings( "MasterSecret" ) );

                string deviceTaskInfoString = context.Request.Form["devicetaskinfo"].ToString();//模式
                string payloadString = context.Request.Form["pushpayload"].ToString();
                string rootCode = context.Request.Form["rootCode"].ToString();
                DeviceTaskInfoEntity deviceTaskInfo = JsonHelper.JsonToObject<DeviceTaskInfoEntity>( deviceTaskInfoString );
                bllDeviceTaskInfo bll = new bllDeviceTaskInfo();
                int errorcode = 0; string errormsg = string.Empty;

                if( bll.Add( ref deviceTaskInfo, logentity, rootCode, out errorcode, out errormsg ) == 0 )
                {

                    payloadString = payloadString.Replace( "newCode", deviceTaskInfo.Code );
                    MessageResult result = client.SendPush( payloadString );

                    bll.UpdateTaskResultID( deviceTaskInfo.Code, result.sendno.ToString() );
                    //操作
                    if( result.isResultOK() )
                    {
                        context.Response.Write( JsonHelper.ToJsonResult( "0", "success" ) );
                    }
                    else
                    {
                        logentity.otype = "0";
                        logentity.logcontent = "发送失败";
                        operatelog.Add( logentity );
                        context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
                    }

                }
                else
                {
                    logentity.otype = "0";
                    logentity.logcontent = "插入数据库失败";
                    operatelog.Add( logentity );
                    context.Response.Write( JsonHelper.ToJsonResult( errorcode.ToString(), errormsg ) );
                }
            }
            catch( Exception ex )
            {
                logentity.otype = "1";
                logentity.logcontent = ex.Message;
                operatelog.Add( logentity );
                context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
            }
        }

        /// <summary>
        /// 设备初始化
        /// </summary>
        /// <param name="context"></param>
        private void DeviceInt( HttpContext context )
        {
            try
            {
                List<string> param = new List<string>() { "deviceSN", "roomNum", "IPAddress", "IPPort", "classcode", "isCheckInit", "isCheckRoomNum", "JPushID", "deviceTypeID", "version" };

                logentity.logcontent = Helper.ObjectToString( context.Request.Form["classcode"] );
                if( !CheckParameters( param ) )
                {
                    return;
                }
                string deviceSN = context.Request.Form["deviceSN"].ToString();
                string roomNum = context.Request.Form["roomNum"].ToString();
                string IPAddress = context.Request.Form["IPAddress"].ToString();
                int IPPort = Helper.StringToInt( context.Request.Form["IPPort"].ToString() );
                string classcode = context.Request.Form["classcode"].ToString();
                int isCheckInit = Helper.StringToInt( context.Request.Form["isCheckInit"].ToString() );
                int isCheckRoomNum = Helper.StringToInt( context.Request.Form["isCheckRoomNum"].ToString() );
                string JPushID = context.Request.Form["JPushID"].ToString();
                int deviceTypeID = Helper.StringToInt( context.Request.Form["deviceTypeID"].ToString() );
                string version = context.Request.Form["version"].ToString();
                string modifiedID = "1";
                string modifiedName = "1";
                int errorCode = 0;
                string errorMsg = "";
                bllDeviceClassInfo bll = new bllDeviceClassInfo();

                dt = bll.UpdateDeviceSN( out errorCode, out errorMsg, classcode, deviceSN, roomNum, IPAddress, IPPort, modifiedID, modifiedName, isCheckInit, isCheckRoomNum, JPushID, deviceTypeID, version, logentity );

                if( errorCode != 0 && errorCode != 2 && errorCode != 3 )
                {
                    logentity.otype = "0";
                    logentity.logcontent += ",设备初始化失败：" + errorMsg;
                    operatelog.Add( logentity );
                }
                ReturnListJson( dt );
            }
            catch( Exception ex )
            {
                logentity.otype = "1";
                logentity.logcontent += "," + ex.Message;
                operatelog.Add( logentity );
                context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
            }
        }

        /// <summary>
        /// 获取所有班级
        /// </summary>
        /// <param name="context"></param>
        private void GetDeviceClassList( HttpContext context )
        {
            try
            {
                bllDeviceClassInfo bll = new bllDeviceClassInfo();

                string filter = "where 1=1 AND IsValid=1 ";
                //可选参数
                Dictionary<string, EnumSearchType> optionalParameters = new Dictionary<string, EnumSearchType> { 
            { "classcode", EnumSearchType.AndString } 
            ,{ "classTypeID", EnumSearchType.AndInt }
            ,{ "subjectTypeID", EnumSearchType.AndInt }
            ,{ "className", EnumSearchType.AndLike }
        ,{ "jpushid", EnumSearchType.AndString }
            };

                //可选参数检查
                filter = CheckOptionalParameters( optionalParameters, filter );
                if( context.Request.Form["JPushIDStatus"] != null && context.Request.Form["JPushIDStatus"].ToString() != "" )
                {
                    if( context.Request.Form["JPushIDStatus"].ToString() == "1" )
                    {
                        filter += " AND LEN(ISNULL([JPushID],''))>0";
                    }
                    else
                    {
                        filter += " AND LEN(ISNULL([JPushID],''))<=0";
                    }


                }
                if( context.Request.Form["HeartBeatStatus"] != null && context.Request.Form["HeartBeatStatus"].ToString() != "" )
                {
                    if( context.Request.Form["HeartBeatStatus"].ToString() == "1" )
                    {
                        filter += " AND HeartBeatCheckDate IS NOT NULL";
                    }
                    else
                    {
                        filter += " AND (HeartBeatCheckDate IS NULL)";
                    }


                }
                if( context.Request.Form["InstallerHeartBeatStatus"] != null && context.Request.Form["InstallerHeartBeatStatus"].ToString() != "" )
                {
                    if( context.Request.Form["InstallerHeartBeatStatus"].ToString() == "1" )
                    {
                        filter += " AND InstallerHeartBeatCheckDate IS NOT NULL";
                    }
                    else
                    {
                        filter += " AND (InstallerHeartBeatCheckDate IS NULL)";
                    }


                }
                if( context.Request.Form["installerversion"] != null && context.Request.Form["installerversion"].ToString() != "" )
                {
                    if( context.Request.Form["installerversion"].ToString() == "-1" )
                    {
                        filter += " AND (installerversion='' or installerversion is null)";
                    }
                    else
                    {
                        filter += " AND installerversion='" + context.Request.Form["installerversion"] + "'";
                    }


                }
                if( context.Request.Form["version"] != null && context.Request.Form["version"].ToString() != "" )
                {
                    if( context.Request.Form["version"].ToString() == "-1" )
                    {
                        filter += " AND (version='' or version is null)";
                    }
                    else
                    {
                        filter += " AND version='" + context.Request.Form["version"] + "'";
                    }


                }
                if( context.Request.Form["rootCode"] != null && context.Request.Form["rootCode"].ToString().Trim() != "" )
                {
                    filter += " AND SUBSTRING([ClassCode],1,10)='" + context.Request.Form["rootCode"] + "'";

                }
                if( context.Request.Form["gradeCode"] != null && context.Request.Form["gradeCode"].ToString().Trim() != "" )
                {
                    filter += " AND SUBSTRING([ClassCode],11,6)='" + context.Request.Form["gradeCode"] + "'";

                }
                if( context.Request.Form["startprogramswitchstatus"] != null && context.Request.Form["startprogramswitchstatus"].ToString().Trim() != "" )
                {
                    filter += " AND StartProgramSwitchStatus=" + context.Request.Form["startprogramswitchstatus"] + "";

                }

                int pageSize = CheckPageSize();
                int currentPage = CheckCurrentPage();

                string order = "SUBSTRING(classcode,1,10) asc,SUBSTRING([ClassCode],11,2) DESC,SUBSTRING([ClassCode],13,4) DESC,SUBSTRING([ClassCode],17,2) asc";
                if( context.Request.Form["order"] != null && context.Request.Form["order"].ToString() != "" )
                {

                    order = context.Request.Form["order"].ToString();

                }

                int recordCount = 0;
                int totalPage = 0;
                if( context.Request.Form["sourceType"] != null && context.Request.Form["sourceType"].ToString() != "" && context.Request.Form["sourceType"].ToString() == "1" )
                {
                    dt = bll.GetPagingListInfo( pageSize, currentPage, filter, order, out recordCount, out totalPage );
                }
                else
                {
                    dt = bll.GetIniClassInfoList( pageSize, currentPage, filter, order, out recordCount, out totalPage );
                }

                ReturnListJson( dt, pageSize, recordCount, currentPage, totalPage );
                //   ReturnListJson(dt);
            }
            catch( Exception ex )
            {
                logentity.otype = "1";
                logentity.logcontent = ex.Message;
                operatelog.Add( logentity );
                context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
            }
        }

        /// <summary>
        /// 获取用户所有班级
        /// </summary>
        /// <param name="context"></param>
        private void GetUserClassList( HttpContext context )
        {
            try
            {
                List<string> param = new List<string>() { "userCode", "rootCode" };

                if( !CheckParameters( param ) )
                {
                    return;
                }
                bllUserClassInfo bll = new bllUserClassInfo();
                //int pageSize = Helper.StringToInt(context.Request.Form["pageSize"].ToString());
                //int currentPage = Helper.StringToInt(context.Request.Form["currentPage"].ToString());
                string userCode = context.Request.Form["userCode"].ToString();
                //  string gradeCode = context.Request.Form["gradeCode"].ToString();
                //string filter = "where usercode='" + userCode+"'";
                //string order = "ClassCode asc";
                string rootCode = context.Request.Form["rootCode"].ToString();
                dt = bll.GetUserClassList( userCode, rootCode );

                ReturnListJson( dt );
            }
            catch( Exception ex )
            {
                logentity.otype = "1";
                logentity.logcontent = ex.Message;
                operatelog.Add( logentity );
                context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
            }
        }
        /// <summary>
        /// 获取用户所有年级
        /// </summary>
        /// <param name="context"></param>
        private void GetUserGradeList( HttpContext context )
        {
            try
            {
                List<string> param = new List<string>() { "userCode" };

                if( !CheckParameters( param ) )
                {
                    return;
                }
                bllUserClassInfo bll = new bllUserClassInfo();

                string userCode = context.Request.Form["userCode"].ToString();
                string rootCode = Helper.ObjectToString( context.Request.Form["rootCode"] );
                dt = bll.GetUserGradeList( userCode, rootCode );

                ReturnListJson( dt );
            }
            catch( Exception ex )
            {
                logentity.otype = "1";
                logentity.logcontent = ex.Message;
                operatelog.Add( logentity );
                context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
            }
        }
        /// <summary>
        /// 获取用户所有学科类型
        /// </summary>
        /// <param name="context"></param>
        private void GetUserSubjectTypeList( HttpContext context )
        {
            try
            {

                bllUserClassInfo bll = new bllUserClassInfo();

                string userCode = context.Request.Form["userCode"].ToString();
                string rootCode = Helper.ObjectToString( context.Request.Form["rootCode"] );

                dt = bll.GetUserSubjectTypeList( userCode, rootCode );

                ReturnListJson( dt );
            }
            catch( Exception ex )
            {
                logentity.otype = "1";
                logentity.logcontent = ex.Message;
                operatelog.Add( logentity );
                context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
            }
        }
        /// <summary>
        /// 获取用户所有班级类型
        /// </summary>
        /// <param name="context"></param>
        private void GetUserClassTypeList( HttpContext context )
        {
            try
            {

                bllUserClassInfo bll = new bllUserClassInfo();

                string userCode = context.Request.Form["userCode"].ToString();
                string rootCode = Helper.ObjectToString( context.Request.Form["rootCode"] );

                dt = bll.GetUserClassTypeList( userCode, rootCode );

                ReturnListJson( dt );
            }
            catch( Exception ex )
            {
                logentity.otype = "1";
                logentity.logcontent = ex.Message;
                operatelog.Add( logentity );
                context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
            }
        }
        protected enum EnumMessageSourceID { 电子班牌平台 = 1, 数字校园平台 = 2, 手机智慧校园 = 3, 微信服务号 = 4 }

        /// <summary>
        /// 推送消息到电子平板
        /// </summary>
        /// <param name="context"></param>
        private void PushNotificationToAPP( HttpContext context )
        {
            try
            {
                //消息来源
                int messagesourseid = 0;
                if( context.Request.Form["messagesourceid"] != null )
                {
                    messagesourseid = Helper.StringToInt( context.Request.Form["messagesourceid"].ToString() );
                    logentity.functionName = logentity.functionName + "--" + (EnumMessageSourceID) messagesourseid;
                }
                List<string> param = new List<string>() { "messagesourceid", "messagetypeid", "operatetypeid", "displaymodelid", "createdid", "createdname", "rootcode", "rargetalias", "taskpriorityid", "taskstatusid", "alert", "title" };
                logentity.logcontent = Helper.ObjectToString( context.Request.Form["rargetalias"].ToString() );
                if( !CheckParameters( param ) )
                {
                    return;
                }

                JPushClient client = new JPushClient( Helper.GetAppSettings( "AppKey" ), Helper.GetAppSettings( "MasterSecret" ) );
                ////消息来源
                //int messagesourseid = Helper.StringToInt(context.Request.Form["messagesourceid"].ToString());
                //logentity.functionName = logentity.functionName + "--" + (EnumMessageSourceID)messagesourseid;
                //布局类型
                int displaymodelid = Helper.StringToInt( context.Request.Form["displaymodelid"].ToString() );
                //消息类型
                int messagetypeid = Helper.StringToInt( context.Request.Form["messagetypeid"].ToString() );
                //操作类型
                int operatetypeid = Helper.StringToInt( context.Request.Form["operatetypeid"].ToString() );
                int taskpriorityid = Helper.StringToInt( context.Request.Form["taskpriorityid"].ToString() );
                int taskstatusid = Helper.StringToInt( context.Request.Form["taskstatusid"].ToString() );

                string title = Helper.ObjectToString( context.Request.Form["title"].ToString() );
                //接收终端别名
                string rargetalias = context.Request.Form["rargetalias"].ToString();
                //操作人
                string createdid = context.Request.Form["createdid"].ToString();
                //操作人姓名
                string createdname = context.Request.Form["createdname"].ToString();

                //发送人校区编号
                string rootcode = context.Request.Form["rootcode"].ToString();

                //通知内容
                string alert = context.Request.Form["alert"].ToString();

                var tag = Helper.ObjectToArrry( context.Request.Form["tag"] );
                var tag_and = Helper.ObjectToArrry( context.Request.Form["tag_and"] );
                var alias = Helper.ObjectToArrry( context.Request.Form["alias"] );
                var registration_id = Helper.ObjectToArrry( context.Request.Form["registration_id"] );
                PushPayload payload = new PushPayload();
                payload.platform = Platform.all();

                if( tag != null || tag_and != null || alias != null || registration_id != null )
                {
                    payload.audience = Audience.all();
                    if( tag != null )
                    {
                        payload.audience = payload.audience.tag( tag );
                    }
                    if( tag_and != null )
                    {
                        payload.audience = payload.audience.tag_and( tag_and );
                    }
                    if( alias != null )
                    {
                        payload.audience = payload.audience.alias( alias );
                    }
                    if( registration_id != null )
                    {
                        payload.audience = payload.audience.registrationId( registration_id );
                    }
                    payload.notification = new Notification();

                    payload.notification.setIos( new IosNotification().setAlert( alert ).setSound( "tweet_sent.mp3" ) );

                    payload.notification.setAndroid( new AndroidNotification().setAlert( alert ).setTitle( title ) );
                    payload.notification.setWinphone( new WinphoneNotification().setAlert( alert ).setTitle( title ) );
                    payload.options = new Options();
                    payload.options.apns_production = false;
                    payload.options.time_to_live = 86400;//(1天)

                    DeviceTaskInfoEntity deviceTaskInfo = new DeviceTaskInfoEntity();
                    deviceTaskInfo.MessageSourceID = messagesourseid;
                    deviceTaskInfo.MessageTypeID = messagetypeid;
                    deviceTaskInfo.OperateTypeID = operatetypeid;
                    deviceTaskInfo.MessageContent = new MessageContent();
                    deviceTaskInfo.MessageContentAlias = new MessageContent();
                    deviceTaskInfo.MessageContent.text = alert;
                    deviceTaskInfo.TargetRange = payload.audience != null ? JsonHelper.ObjectToJSON( payload.audience.dictionary ) : "";
                    deviceTaskInfo.Tag = context.Request.Form["tag"];
                    deviceTaskInfo.Tag_and = context.Request.Form["tag_and"];
                    deviceTaskInfo.Alias = context.Request.Form["alias"];
                    deviceTaskInfo.Registration_ID = context.Request.Form["registration_id"];
                    deviceTaskInfo.RargetAlias = rargetalias;
                    deviceTaskInfo.CreatedID = createdid;
                    deviceTaskInfo.CreatedName = createdname;
                    deviceTaskInfo.MessageTitle = title;



                    //操作

                    bllDeviceTaskInfo bll = new bllDeviceTaskInfo();
                    int errorcode = 0; string errormsg = string.Empty;
                    try
                    {
                        if( bll.Add( ref deviceTaskInfo, logentity, rootcode, out errorcode, out errormsg ) == 0 )
                        {

                            try
                            {
                                MessageResult result = client.SendPush( payload );

                                //操作
                                if( result.isResultOK() )
                                {
                                    context.Response.Write( JsonHelper.ToJsonResult( "0", "success" ) );
                                    return;
                                }
                                else
                                {
                                    logentity.otype = "0";
                                    logentity.logcontent += ",发送通知失败";
                                    operatelog.Add( logentity );
                                    bll.UpdateTaskResultID( deviceTaskInfo.Code, "1" );
                                    context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
                                    return;
                                }
                            }
                            catch( Exception ex )
                            {
                                bll.UpdateTaskResultID( deviceTaskInfo.Code, "1" );
                                logentity.otype = "1";
                                logentity.logcontent += "," + ex.Message;
                                operatelog.Add( logentity );
                                context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
                                return;
                            }
                        }
                        else
                        {
                            logentity.otype = "0";
                            logentity.logcontent += ",插入通知任务到数据库失败";
                            operatelog.Add( logentity );
                            context.Response.Write( JsonHelper.ToJsonResult( errorcode.ToString(), errormsg ) );
                            return;
                        }
                    }
                    catch( Exception ex )
                    {
                        bll.UpdateTaskResultID( deviceTaskInfo.Code, "1" );
                        logentity.otype = "1";
                        logentity.logcontent += "," + ex.Message;
                        operatelog.Add( logentity );
                        context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
                        return;
                    }
                }
                else
                {
                    logentity.otype = "0";
                    logentity.logcontent += ",发送通知时请选择接收对象";
                    operatelog.Add( logentity );
                    context.Response.Write( JsonHelper.ToJsonResult( "1", "请选择接收对象" ) );
                    return;
                }
            }
            catch( Exception ex )
            {
                logentity.otype = "1";
                logentity.logcontent += "," + ex.Message;
                operatelog.Add( logentity );
                context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
            }


        }

        /// <summary>
        /// 推送消息到电子平板
        /// </summary>
        /// <param name="context"></param>
        private void PushMessageToECC( HttpContext context )
        {

            try
            {
                //消息来源
                int messagesourseid = 0;
                if( context.Request.Form["messagesourceid"] != null )
                {
                    messagesourseid = Helper.StringToInt( context.Request.Form["messagesourceid"].ToString() );
                    logentity.functionName = logentity.functionName + "--" + (EnumMessageSourceID) messagesourseid;
                }

                List<string> param = new List<string>() { "messagesourceid", "messagetypeid", "operatetypeid", "displaymodelid", "createdid", "createdname", "rootcode", "rargetalias", "taskpriorityid", "taskstatusid", "tasktypeid" };

                if( !CheckParameters( param ) )
                {
                    return;
                }
                logentity.logcontent = context.Request.Form["rargetalias"].ToString();
                JPushClient client = new JPushClient( Helper.GetAppSettings( "AppKey" ), Helper.GetAppSettings( "MasterSecret" ) );


                //布局类型
                int displaymodelid = Helper.StringToInt( context.Request.Form["displaymodelid"].ToString() );

                //消息类型
                int messagetypeid = Helper.StringToInt( context.Request.Form["messagetypeid"].ToString() );

                //操作类型
                int operatetypeid = Helper.StringToInt( context.Request.Form["operatetypeid"].ToString() );
                //优先级
                int taskpriorityid = Helper.StringToInt( context.Request.Form["taskpriorityid"].ToString() );
                //任务状态
                int taskstatusid = Helper.StringToInt( context.Request.Form["taskstatusid"].ToString() );
                //任务类型
                int tasktypeid = Helper.StringToInt( context.Request.Form["tasktypeid"].ToString() );
                //接收终端别名
                string rargetalias = context.Request.Form["rargetalias"].ToString();
                //操作人
                string createdid = context.Request.Form["createdid"].ToString();
                //操作人姓名
                string createdname = context.Request.Form["createdname"].ToString();

                //发送人校区编号
                string rootcode = context.Request.Form["rootcode"].ToString();

                DateTime taskbegintime = context.Request.Form["TaskBeginTime"] != null ? Helper.StringToDateTime( context.Request.Form["TaskBeginTime"].ToString() ) : new DateTime();
                DateTime taskendtime = context.Request.Form["TaskEndTime"] != null ? Helper.StringToDateTime( context.Request.Form["TaskEndTime"].ToString() ) : new DateTime();

                //图片显示秒数--默认5秒
                int imagespansecond = Helper.ObjectToInt( context.Request.Form["imagespansecond"] ) > 0 ? Helper.ObjectToInt( context.Request.Form["imagespansecond"] ) : 5;
                //图片显示效果类型--默认为1
                int imageeffectid = Helper.ObjectToInt( context.Request.Form["imageeffectid"] ) > 0 ? Helper.ObjectToInt( context.Request.Form["imageeffectid"] ) : 1;
                //消息标题
                string title = Helper.ObjectToString( context.Request.Form["title"] );
                //消息内容
                string messagecontent_text = "";

                if( operatetypeid == 4 )//软件升级
                {
                    messagecontent_text = Helper.GetAppSettings( "softwareupgradeurl" );
                    if( Helper.ObjectToString( context.Request.Form["messagecontent_text"] ) != "" )
                    {
                        messagecontent_text = Helper.GetAppSettings( "softwareupgradeurl" ) + "?Version=" + Helper.ObjectToString( context.Request.Form["messagecontent_text"] );
                    }
                    else
                    {
                        messagecontent_text = Helper.GetAppSettings( "softwareupgradeurl" ) + "?Version=9.9.9";
                    }

                }
                else if( operatetypeid == 14 ) //启动设备软件升级
                {
                    messagecontent_text = Helper.GetAppSettings( "softwareinstallerurl" );
                    if( Helper.ObjectToString( context.Request.Form["messagecontent_text"] ) != "" )
                    {
                        messagecontent_text = Helper.GetAppSettings( "softwareinstallerurl" ) + "?Version=" + Helper.ObjectToString( context.Request.Form["messagecontent_text"] );
                    }
                    else
                    {
                        messagecontent_text = Helper.GetAppSettings( "softwareinstallerurl" ) + "?Version=9.9.9";
                    }
                }
                else if( operatetypeid == 17 ) //重新初始化参数
                {
                    string JPushID = context.Request.Form["messagecontent_text"].ToString();
                    if( !string.IsNullOrWhiteSpace( JPushID ) )
                    {
                        //获取班级信息
                        bllDeviceTaskInfo bll = new bllDeviceTaskInfo();

                        if( bll.UpdateRepeatBindClassInfoAsyncResultID( JPushID ) != 0 )
                        {
                            context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
                            return;
                        }

                    }
                    else
                    {
                        context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
                        return;
                    }



                }
                else
                {
                    messagecontent_text = Helper.ObjectToString( context.Request.Form["messagecontent_text"] );
                }

                var messagecontent_image = Helper.ObjectToArrry( context.Request.Form["messagecontent_image"] );
                var messagecontent_video = Helper.ObjectToArrry( context.Request.Form["messagecontent_video"] );
                string messagecontent_textalias = "";
                var messagecontent_imagealias = Helper.ObjectToArrry( context.Request.Form["messagecontent_imagealias"] );
                var messagecontent_videoalias = Helper.ObjectToArrry( context.Request.Form["messagecontent_videoalias"] );
                var tag = Helper.ObjectToArrry( context.Request.Form["tag"] );
                var tag_and = Helper.ObjectToArrry( context.Request.Form["tag_and"] );
                var alias = Helper.ObjectToArrry( context.Request.Form["alias"] );
                var registration_id = Helper.ObjectToArrry( context.Request.Form["registration_id"] );


                PushPayload payload = new PushPayload();
                payload.platform = Platform.all();
                if( tag != null || tag_and != null || alias != null || registration_id != null )
                {
                    payload.audience = Audience.all();
                    if( tag != null )
                    {
                        payload.audience = payload.audience.tag( tag );
                    }
                    if( tag_and != null )
                    {
                        payload.audience = payload.audience.tag_and( tag_and );
                    }
                    if( alias != null )
                    {
                        payload.audience = payload.audience.alias( alias );
                    }
                    if( registration_id != null )
                    {
                        payload.audience = payload.audience.registrationId( registration_id );
                    }
                    payload.options = new Options();
                    payload.options.apns_production = true;
                    payload.options.time_to_live = 86400;//(1天)



                    DeviceTaskInfoEntity deviceTaskInfo = new DeviceTaskInfoEntity();
                    deviceTaskInfo.DisplayModelID = displaymodelid;
                    deviceTaskInfo.MessageSourceID = messagesourseid;
                    deviceTaskInfo.MessageTypeID = messagetypeid;
                    deviceTaskInfo.OperateTypeID = operatetypeid;

                    deviceTaskInfo.MessageTitle = title;
                    deviceTaskInfo.MessageContent = new MessageContent();
                    deviceTaskInfo.TaskPriorityID = taskpriorityid;
                    deviceTaskInfo.TaskStatusID = taskstatusid;
                    deviceTaskInfo.TaskTypeID = tasktypeid;
                    deviceTaskInfo.MessageContent.text = messagecontent_text;
                    deviceTaskInfo.MessageContent.image = messagecontent_image;
                    deviceTaskInfo.MessageContent.video = messagecontent_video;
                    deviceTaskInfo.MessageContentAlias = new MessageContent();
                    deviceTaskInfo.MessageContentAlias.text = messagecontent_textalias;
                    deviceTaskInfo.MessageContentAlias.image = messagecontent_imagealias;
                    deviceTaskInfo.MessageContentAlias.video = messagecontent_videoalias;
                    deviceTaskInfo.TaskBeginTime = taskbegintime;
                    deviceTaskInfo.TaskEndTime = taskendtime;
                    deviceTaskInfo.ImageEffectID = imageeffectid;
                    deviceTaskInfo.ImageSpanSecond = imagespansecond;
                    deviceTaskInfo.TargetRange = payload.audience != null ? JsonHelper.ObjectToJSON( payload.audience.dictionary ) : "";
                    deviceTaskInfo.Tag = context.Request.Form["tag"];
                    deviceTaskInfo.Tag_and = context.Request.Form["tag_and"];
                    deviceTaskInfo.Alias = context.Request.Form["alias"];
                    deviceTaskInfo.Registration_ID = context.Request.Form["registration_id"];
                    deviceTaskInfo.RargetAlias = rargetalias;
                    deviceTaskInfo.CreatedID = createdid;
                    deviceTaskInfo.CreatedName = createdname;

                    //操作=
                    bllDeviceTaskInfo bll = new bllDeviceTaskInfo();
                    int errorcode = 0; string errormsg = string.Empty;
                    try
                    {
                        if( bll.Add( ref deviceTaskInfo, logentity, rootcode, out errorcode, out errormsg ) == 0 )
                        {
                            try
                            {
                                if( deviceTaskInfo.OperateTypeID == 13 || deviceTaskInfo.OperateTypeID == 15 )
                                {
                                    string clearHeartBeatCheckDateFilter = "";
                                    if( deviceTaskInfo.Registration_ID != "" )//班级
                                    {
                                        clearHeartBeatCheckDateFilter = deviceTaskInfo.Registration_ID;

                                        clearHeartBeatCheckDateFilter = "[JPushID] in (" + clearHeartBeatCheckDateFilter.Replace( "\"", "'" ).Trim( '[' ).Trim( ']' ) + ")";
                                        //过滤不等空的JPushID
                                        //clearVersionFilter = clearVersionFilter + " and [JPushID]!='' and [JPushID] is not null ";
                                    }
                                    else if( deviceTaskInfo.Tag != "" && deviceTaskInfo.Tag_and != "" )//年级
                                    {
                                        // clearVersionFilter=["G012014","G012015"] ["SC1100009000","RC1006"]
                                        string grades = deviceTaskInfo.Tag.Replace( "\"", "'" ).Trim( ']' ).Trim( '[' );
                                        string[] rootsarry = deviceTaskInfo.Tag_and.Replace( "\"", "" ).Trim( ']' ).Trim( '[' ).Split( ',' );
                                        string school = "";
                                        string root = "";
                                        if( rootsarry.Length == 2 )
                                        {
                                            if( rootsarry[0].Contains( "SC" ) )
                                            {
                                                school = "\'" + rootsarry[0].Replace( "SC", "" ).Trim() + "\'";
                                            }
                                            if( rootsarry[1].Contains( "RC" ) )
                                            {
                                                root = rootsarry[1].Replace( "RC", "" ).Trim();
                                                grades = grades.Replace( "G", root );
                                            }
                                        }
                                        clearHeartBeatCheckDateFilter = string.Format( "SchoolCode={0} and SUBSTRING(ClassCode,7,16) in ({1})", school, grades );
                                    }
                                    else if( deviceTaskInfo.Tag_and != "" )//校区或者学科
                                    {
                                        string[] rootsarry = deviceTaskInfo.Tag_and.Trim( ']' ).Trim( '[' ).Split( ',' );
                                        string school = "";
                                        string root = "";
                                        string classtype = "";
                                        string subjecttype = "";
                                        if( rootsarry.Length == 2 )
                                        {
                                            if( rootsarry[0].Contains( "SC" ) )
                                            {
                                                school = rootsarry[0].Replace( "SC", "" ).Replace( '"', '\'' );
                                            }
                                            if( rootsarry[1].Contains( "RC" ) )
                                            {
                                                root = rootsarry[1].Replace( "RC", "" ).Replace( '"', ' ' ).Trim();

                                            }
                                            clearHeartBeatCheckDateFilter = string.Format( "SchoolCode={0} and SUBSTRING(ClassCode,7,4) = '{1}'", school, root );
                                        }
                                        else if( rootsarry.Length == 3 )
                                        {
                                            if( rootsarry[0].Contains( "SC" ) )
                                            {
                                                school = rootsarry[0].Replace( "SC", "" ).Replace( '"', '\'' );
                                            }
                                            if( rootsarry[1].Contains( "RC" ) )
                                            {
                                                root = rootsarry[1].Replace( "RC", "" ).Replace( '"', ' ' ).Trim();

                                            }
                                            if( rootsarry[2].Contains( "L" ) )
                                            {
                                                classtype = rootsarry[2].Replace( "L", "" ).Replace( '"', ' ' ).Trim();

                                            }
                                            else if( rootsarry[2].Contains( "S" ) )
                                            {
                                                subjecttype = rootsarry[2].Replace( "S", "" ).Replace( '"', ' ' ).Trim();
                                            }
                                            clearHeartBeatCheckDateFilter = string.Format( "SchoolCode={0} and SUBSTRING(ClassCode,7,4) = '{1}'", school, root );
                                            if( classtype != "" )
                                            {
                                                clearHeartBeatCheckDateFilter += string.Format( " and classtypeID={0}", classtype );
                                            }
                                            if( subjecttype != "" )
                                            {
                                                clearHeartBeatCheckDateFilter += string.Format( " and SubjectTypeID={0}", subjecttype );
                                            }
                                        }
                                        else if( rootsarry.Length == 4 )
                                        {
                                            if( rootsarry[0].Contains( "SC" ) )
                                            {
                                                school = rootsarry[0].Replace( "SC", "" ).Replace( '"', '\'' );
                                            }
                                            if( rootsarry[1].Contains( "RC" ) )
                                            {
                                                root = rootsarry[1].Replace( "RC", "" ).Replace( '"', ' ' ).Trim();

                                            }
                                            if( rootsarry[2].Contains( "L" ) )
                                            {
                                                classtype = rootsarry[2].Replace( "L", "" ).Replace( '"', ' ' ).Trim();

                                            }
                                            if( rootsarry[3].Contains( "S" ) )
                                            {
                                                subjecttype = rootsarry[3].Replace( "S", "" ).Replace( '"', ' ' ).Trim();
                                            }
                                            clearHeartBeatCheckDateFilter = string.Format( "SchoolCode={0} and SUBSTRING(ClassCode,7,4) = '{1}'", school, root );
                                            if( classtype != "" )
                                            {
                                                clearHeartBeatCheckDateFilter += string.Format( " and classtypeID={0}", classtype );
                                            }
                                            if( subjecttype != "" )
                                            {
                                                clearHeartBeatCheckDateFilter += string.Format( " and SubjectTypeID={0}", subjecttype );
                                            }
                                        }

                                    }
                                    if( clearHeartBeatCheckDateFilter != "" )
                                    {
                                        clearHeartBeatCheckDateFilter = " OR (" + clearHeartBeatCheckDateFilter + ")";
                                    }
                                    if( deviceTaskInfo.OperateTypeID == 13 )
                                    {
                                        if( bll.ClearHeartBeatCheckDateByJPushID( clearHeartBeatCheckDateFilter ) > 0 )
                                        {

                                        }
                                    }

                                    if( deviceTaskInfo.OperateTypeID == 15 )
                                    {
                                        if( bll.ClearInstallerHeartBeatCheckDateByJPushID( clearHeartBeatCheckDateFilter ) > 0 )
                                        {

                                        }
                                    }
                                }
                                CustomMsgContent customMsgContent = JsonHelper.ObjectToObject<DeviceTaskInfoEntity, CustomMsgContent>( deviceTaskInfo );
                                payload.message = Message.content( customMsgContent );

                                payload.message.setTitle( title );

                                try
                                {
                                    MessageResult result = client.SendPush( payload );


                                    //操作
                                    if( result.isResultOK() )
                                    {
                                        context.Response.Write( JsonHelper.ToJsonResult( "0", "success" ) );
                                        return;
                                    }
                                    else
                                    {
                                        bll.UpdateTaskResultID( deviceTaskInfo.Code, "1" );
                                        logentity.otype = "0";
                                        logentity.logcontent += ",发送消息失败";
                                        operatelog.Add( logentity );
                                        context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
                                        return;
                                    }
                                }
                                catch
                                {
                                    context.Response.Write( JsonHelper.ToJsonResult( "1", "请检查终端目标是否存在" ) );

                                    return;
                                }

                            }
                            catch( Exception ex )
                            {
                                bll.UpdateTaskResultID( deviceTaskInfo.Code, "1" );
                                logentity.otype = "1";
                                logentity.logcontent += "," + ex.Message;
                                operatelog.Add( logentity );
                                context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
                                return;
                            }
                        }
                        else
                        {
                            logentity.otype = "0";
                            logentity.logcontent += ",插入消息任务到数据库失败";
                            operatelog.Add( logentity );
                            context.Response.Write( JsonHelper.ToJsonResult( errorcode.ToString(), errormsg ) );
                            return;
                        }
                    }
                    catch( Exception ex )
                    {
                        logentity.otype = "1";
                        logentity.logcontent += "," + ex.Message + "插入任务";
                        operatelog.Add( logentity );
                        context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
                        return;
                    }
                }
                else
                {
                    logentity.otype = "0";
                    logentity.logcontent += ",发送消息时请选择接收对象";
                    operatelog.Add( logentity );
                    context.Response.Write( JsonHelper.ToJsonResult( "1", "请选择接收对象" ) );
                    return;
                }
            }
            catch( Exception ex )
            {
                logentity.otype = "1";
                logentity.logcontent += "," + ex.Message + "总的捕捉";
                operatelog.Add( logentity );
                context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
            }
        }

        /// <summary>
        /// 推送消息到电子平板
        /// </summary>
        /// <param name="context"></param>
        private void PushMessageToECC1( HttpContext context )
        {

            try
            {
                //消息来源
                int messagesourseid = 0;
                if( context.Request.Form["messagesourceid"] != null )
                {
                    messagesourseid = Helper.StringToInt( context.Request.Form["messagesourceid"].ToString() );
                    logentity.functionName = logentity.functionName + "--" + (EnumMessageSourceID) messagesourseid;
                }
                List<string> param1 = new List<string>() { "rootcode" };

                if( !CheckEmptyParameters( param1 ) )
                {
                    return;
                }
                List<string> param = new List<string>() { "messagesourceid", "messagetypeid", "operatetypeid", "displaymodelid", "createdid", "createdname", "rargetalias", "taskpriorityid", "taskstatusid", "tasktypeid" };

                if( !CheckEmptyParameters( param ) )
                {
                    return;
                }
                logentity.logcontent = context.Request.Form["rargetalias"].ToString();
                JPushClient client = new JPushClient( Helper.GetAppSettings( "AppKey" ), Helper.GetAppSettings( "MasterSecret" ) );
                ////消息来源
                //int messagesourseid = Helper.StringToInt(context.Request.Form["messagesourceid"].ToString());
                //logentity.functionName = logentity.functionName + "--" + (EnumMessageSourceID)messagesourseid;
                //布局类型
                int displaymodelid = Helper.StringToInt( context.Request.Form["displaymodelid"].ToString() );

                //消息类型
                int messagetypeid = Helper.StringToInt( context.Request.Form["messagetypeid"].ToString() );

                //操作类型
                int operatetypeid = Helper.StringToInt( context.Request.Form["operatetypeid"].ToString() );
                //优先级
                int taskpriorityid = Helper.StringToInt( context.Request.Form["taskpriorityid"].ToString() );
                //任务状态
                int taskstatusid = Helper.StringToInt( context.Request.Form["taskstatusid"].ToString() );
                //任务类型
                int tasktypeid = Helper.StringToInt( context.Request.Form["tasktypeid"].ToString() );
                //接收终端别名
                string rargetalias = context.Request.Form["rargetalias"].ToString();
                //操作人
                string createdid = context.Request.Form["createdid"].ToString();
                //操作人姓名
                string createdname = context.Request.Form["createdname"].ToString();

                //发送人校区编号
                string rootcode = context.Request.Form["rootcode"].ToString();

                DateTime taskbegintime = context.Request.Form["TaskBeginTime"] != null ? Helper.StringToDateTime( context.Request.Form["TaskBeginTime"].ToString() ) : new DateTime();
                DateTime taskendtime = context.Request.Form["TaskEndTime"] != null ? Helper.StringToDateTime( context.Request.Form["TaskEndTime"].ToString() ) : new DateTime();

                //图片显示秒数--默认5秒
                int imagespansecond = Helper.ObjectToInt( context.Request.Form["imagespansecond"] ) > 0 ? Helper.ObjectToInt( context.Request.Form["imagespansecond"] ) : 5;
                //图片显示效果类型--默认为1
                int imageeffectid = Helper.ObjectToInt( context.Request.Form["imageeffectid"] ) > 0 ? Helper.ObjectToInt( context.Request.Form["imageeffectid"] ) : 1;
                //消息标题
                string title = Helper.ObjectToString( context.Request.Form["title"] );
                //消息内容
                string messagecontent_text = "";

                if( operatetypeid == 4 )//软件升级
                {
                    messagecontent_text = Helper.GetAppSettings( "softwareupgradeurl" );
                    if( Helper.ObjectToString( context.Request.Form["messagecontent_text"] ) != "" )
                    {
                        messagecontent_text = Helper.GetAppSettings( "softwareupgradeurl" ) + "?Version=" + Helper.ObjectToString( context.Request.Form["messagecontent_text"] );
                    }
                    else
                    {
                        messagecontent_text = Helper.GetAppSettings( "softwareupgradeurl" ) + "?Version=9.9.9";
                    }

                }
                else if( operatetypeid == 14 ) //启动设备软件升级
                {
                    messagecontent_text = Helper.GetAppSettings( "softwareinstallerurl" );
                    if( Helper.ObjectToString( context.Request.Form["messagecontent_text"] ) != "" )
                    {
                        messagecontent_text = Helper.GetAppSettings( "softwareinstallerurl" ) + "?Version=" + Helper.ObjectToString( context.Request.Form["messagecontent_text"] );
                    }
                    else
                    {
                        messagecontent_text = Helper.GetAppSettings( "softwareinstallerurl" ) + "?Version=9.9.9";
                    }
                }
                else if( operatetypeid == 17 ) //重新初始化参数
                {
                    string classcode = context.Request.Form["messagecontent_text"].ToString();
                    if( !string.IsNullOrWhiteSpace( classcode ) )
                    {
                        //获取班级信息
                        bllDeviceClassInfo bll = new bllDeviceClassInfo();

                        dt = bll.GetPagingSigInfo( "IsValid=1" + " and classcode='" + classcode + "'" );
                        if( dt != null )
                        {
                            messagecontent_text = JsonHelper.DataTableToJSON( dt );
                        }
                        else
                        {
                            context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
                            return;
                        }
                    }
                    else
                    {
                        context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
                        return;
                    }



                }
                else
                {
                    messagecontent_text = Helper.ObjectToString( context.Request.Form["messagecontent_text"] );
                }

                var messagecontent_image = Helper.ObjectToArrry( context.Request.Form["messagecontent_image"] );
                var messagecontent_video = Helper.ObjectToArrry( context.Request.Form["messagecontent_video"] );
                string messagecontent_textalias = "";
                var messagecontent_imagealias = Helper.ObjectToArrry( context.Request.Form["messagecontent_imagealias"] );
                var messagecontent_videoalias = Helper.ObjectToArrry( context.Request.Form["messagecontent_videoalias"] );
                var tag = Helper.ObjectToArrry( context.Request.Form["tag"] );
                var tag_and = Helper.ObjectToArrry( context.Request.Form["tag_and"] );
                var alias = Helper.ObjectToArrry( context.Request.Form["alias"] );
                var registration_id = Helper.ObjectToArrry( context.Request.Form["registration_id"] );


                PushPayload payload = new PushPayload();
                payload.platform = Platform.all();
                if( tag != null || tag_and != null || alias != null || registration_id != null )
                {
                    payload.audience = Audience.all();
                    if( tag != null )
                    {
                        payload.audience = payload.audience.tag( tag );
                    }
                    if( tag_and != null )
                    {
                        payload.audience = payload.audience.tag_and( tag_and );
                    }
                    if( alias != null )
                    {
                        payload.audience = payload.audience.alias( alias );
                    }
                    if( registration_id != null )
                    {
                        payload.audience = payload.audience.registrationId( registration_id );
                    }
                    payload.options = new Options();
                    payload.options.apns_production = true;
                    payload.options.time_to_live = 86400;//(1天)



                    DeviceTaskInfoEntity deviceTaskInfo = new DeviceTaskInfoEntity();
                    deviceTaskInfo.DisplayModelID = displaymodelid;
                    deviceTaskInfo.MessageSourceID = messagesourseid;
                    deviceTaskInfo.MessageTypeID = messagetypeid;
                    deviceTaskInfo.OperateTypeID = operatetypeid;

                    deviceTaskInfo.MessageTitle = title;
                    deviceTaskInfo.MessageContent = new MessageContent();
                    deviceTaskInfo.TaskPriorityID = taskpriorityid;
                    deviceTaskInfo.TaskStatusID = taskstatusid;
                    deviceTaskInfo.TaskTypeID = tasktypeid;
                    deviceTaskInfo.MessageContent.text = messagecontent_text;
                    deviceTaskInfo.MessageContent.image = messagecontent_image;
                    deviceTaskInfo.MessageContent.video = messagecontent_video;
                    deviceTaskInfo.MessageContentAlias = new MessageContent();
                    deviceTaskInfo.MessageContentAlias.text = messagecontent_textalias;
                    deviceTaskInfo.MessageContentAlias.image = messagecontent_imagealias;
                    deviceTaskInfo.MessageContentAlias.video = messagecontent_videoalias;
                    deviceTaskInfo.TaskBeginTime = taskbegintime;
                    deviceTaskInfo.TaskEndTime = taskendtime;
                    deviceTaskInfo.ImageEffectID = imageeffectid;
                    deviceTaskInfo.ImageSpanSecond = imagespansecond;
                    deviceTaskInfo.TargetRange = payload.audience != null ? JsonHelper.ObjectToJSON( payload.audience.dictionary ) : "";
                    deviceTaskInfo.Tag = context.Request.Form["tag"];
                    deviceTaskInfo.Tag_and = context.Request.Form["tag_and"];
                    deviceTaskInfo.Alias = context.Request.Form["alias"];
                    deviceTaskInfo.Registration_ID = context.Request.Form["registration_id"];
                    deviceTaskInfo.RargetAlias = rargetalias;
                    deviceTaskInfo.CreatedID = createdid;
                    deviceTaskInfo.CreatedName = createdname;

                    //操作=
                    bllDeviceTaskInfo bll = new bllDeviceTaskInfo();
                    int errorcode = 0; string errormsg = string.Empty;
                    try
                    {
                        if( bll.Add( ref deviceTaskInfo, logentity, rootcode, out errorcode, out errormsg ) == 0 )
                        {
                            try
                            {
                                if( deviceTaskInfo.OperateTypeID == 13 || deviceTaskInfo.OperateTypeID == 15 )
                                {
                                    string clearHeartBeatCheckDateFilter = "";
                                    if( deviceTaskInfo.Registration_ID != "" )//班级
                                    {
                                        clearHeartBeatCheckDateFilter = deviceTaskInfo.Registration_ID;

                                        clearHeartBeatCheckDateFilter = "[JPushID] in (" + clearHeartBeatCheckDateFilter.Replace( "\"", "'" ).Trim( '[' ).Trim( ']' ) + ")";
                                        //过滤不等空的JPushID
                                        //clearVersionFilter = clearVersionFilter + " and [JPushID]!='' and [JPushID] is not null ";
                                    }
                                    else if( deviceTaskInfo.Tag != "" && deviceTaskInfo.Tag_and != "" )//年级
                                    {
                                        // clearVersionFilter=["G012014","G012015"] ["SC1100009000","RC1006"]
                                        string grades = deviceTaskInfo.Tag.Replace( "\"", "'" ).Trim( ']' ).Trim( '[' );
                                        string[] rootsarry = deviceTaskInfo.Tag_and.Replace( "\"", "" ).Trim( ']' ).Trim( '[' ).Split( ',' );
                                        string school = "";
                                        string root = "";
                                        if( rootsarry.Length == 2 )
                                        {
                                            if( rootsarry[0].Contains( "SC" ) )
                                            {
                                                school = "\'" + rootsarry[0].Replace( "SC", "" ).Trim() + "\'";
                                            }
                                            if( rootsarry[1].Contains( "RC" ) )
                                            {
                                                root = rootsarry[1].Replace( "RC", "" ).Trim();
                                                grades = grades.Replace( "G", root );
                                            }
                                        }
                                        clearHeartBeatCheckDateFilter = string.Format( "SchoolCode={0} and SUBSTRING(ClassCode,7,16) in ({1})", school, grades );
                                    }
                                    else if( deviceTaskInfo.Tag_and != "" )//校区或者学科
                                    {
                                        string[] rootsarry = deviceTaskInfo.Tag_and.Trim( ']' ).Trim( '[' ).Split( ',' );
                                        string school = "";
                                        string root = "";
                                        string classtype = "";
                                        string subjecttype = "";
                                        if( rootsarry.Length == 2 )
                                        {
                                            if( rootsarry[0].Contains( "SC" ) )
                                            {
                                                school = rootsarry[0].Replace( "SC", "" ).Replace( '"', '\'' );
                                            }
                                            if( rootsarry[1].Contains( "RC" ) )
                                            {
                                                root = rootsarry[1].Replace( "RC", "" ).Replace( '"', ' ' ).Trim();

                                            }
                                            clearHeartBeatCheckDateFilter = string.Format( "SchoolCode={0} and SUBSTRING(ClassCode,7,4) = '{1}'", school, root );
                                        }
                                        else if( rootsarry.Length == 3 )
                                        {
                                            if( rootsarry[0].Contains( "SC" ) )
                                            {
                                                school = rootsarry[0].Replace( "SC", "" ).Replace( '"', '\'' );
                                            }
                                            if( rootsarry[1].Contains( "RC" ) )
                                            {
                                                root = rootsarry[1].Replace( "RC", "" ).Replace( '"', ' ' ).Trim();

                                            }
                                            if( rootsarry[2].Contains( "L" ) )
                                            {
                                                classtype = rootsarry[2].Replace( "L", "" ).Replace( '"', ' ' ).Trim();

                                            }
                                            else if( rootsarry[2].Contains( "S" ) )
                                            {
                                                subjecttype = rootsarry[2].Replace( "S", "" ).Replace( '"', ' ' ).Trim();
                                            }
                                            clearHeartBeatCheckDateFilter = string.Format( "SchoolCode={0} and SUBSTRING(ClassCode,7,4) = '{1}'", school, root );
                                            if( classtype != "" )
                                            {
                                                clearHeartBeatCheckDateFilter += string.Format( " and classtypeID={0}", classtype );
                                            }
                                            if( subjecttype != "" )
                                            {
                                                clearHeartBeatCheckDateFilter += string.Format( " and SubjectTypeID={0}", subjecttype );
                                            }
                                        }
                                        else if( rootsarry.Length == 4 )
                                        {
                                            if( rootsarry[0].Contains( "SC" ) )
                                            {
                                                school = rootsarry[0].Replace( "SC", "" ).Replace( '"', '\'' );
                                            }
                                            if( rootsarry[1].Contains( "RC" ) )
                                            {
                                                root = rootsarry[1].Replace( "RC", "" ).Replace( '"', ' ' ).Trim();

                                            }
                                            if( rootsarry[2].Contains( "L" ) )
                                            {
                                                classtype = rootsarry[2].Replace( "L", "" ).Replace( '"', ' ' ).Trim();

                                            }
                                            if( rootsarry[3].Contains( "S" ) )
                                            {
                                                subjecttype = rootsarry[3].Replace( "S", "" ).Replace( '"', ' ' ).Trim();
                                            }
                                            clearHeartBeatCheckDateFilter = string.Format( "SchoolCode={0} and SUBSTRING(ClassCode,7,4) = '{1}'", school, root );
                                            if( classtype != "" )
                                            {
                                                clearHeartBeatCheckDateFilter += string.Format( " and classtypeID={0}", classtype );
                                            }
                                            if( subjecttype != "" )
                                            {
                                                clearHeartBeatCheckDateFilter += string.Format( " and SubjectTypeID={0}", subjecttype );
                                            }
                                        }

                                    }
                                    if( clearHeartBeatCheckDateFilter != "" )
                                    {
                                        clearHeartBeatCheckDateFilter = " OR (" + clearHeartBeatCheckDateFilter + ")";
                                    }
                                    if( deviceTaskInfo.OperateTypeID == 13 )
                                    {
                                        if( bll.ClearHeartBeatCheckDateByJPushID( clearHeartBeatCheckDateFilter ) > 0 )
                                        {

                                        }
                                    }

                                    if( deviceTaskInfo.OperateTypeID == 15 )
                                    {
                                        if( bll.ClearInstallerHeartBeatCheckDateByJPushID( clearHeartBeatCheckDateFilter ) > 0 )
                                        {

                                        }
                                    }
                                }
                                CustomMsgContent customMsgContent = JsonHelper.ObjectToObject<DeviceTaskInfoEntity, CustomMsgContent>( deviceTaskInfo );
                                payload.message = Message.content( customMsgContent );

                                payload.message.setTitle( title );
                                MessageResult result = client.SendPush( payload );


                                //操作
                                if( result.isResultOK() )
                                {
                                    context.Response.Write( JsonHelper.ToJsonResult( "0", "success" ) );
                                    return;
                                }
                                else
                                {
                                    bll.UpdateTaskResultID( deviceTaskInfo.Code, "1" );
                                    logentity.otype = "0";
                                    logentity.logcontent += ",发送消息失败";
                                    operatelog.Add( logentity );
                                    context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
                                    return;
                                }

                            }
                            catch( Exception ex )
                            {
                                bll.UpdateTaskResultID( deviceTaskInfo.Code, "1" );
                                logentity.otype = "1";
                                logentity.logcontent += "," + ex.Message + "OperateTypeID=13,15时，分析jpushid，清空版本号";
                                operatelog.Add( logentity );
                                context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
                                return;
                            }
                        }
                        else
                        {
                            logentity.otype = "0";
                            logentity.logcontent += ",插入消息任务到数据库失败";
                            operatelog.Add( logentity );
                            context.Response.Write( JsonHelper.ToJsonResult( errorcode.ToString(), errormsg ) );
                            return;
                        }
                    }
                    catch( Exception ex )
                    {
                        logentity.otype = "1";
                        logentity.logcontent += "," + ex.Message + "插入任务";
                        operatelog.Add( logentity );
                        context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
                        return;
                    }
                }
                else
                {
                    logentity.otype = "0";
                    logentity.logcontent += ",发送消息时请选择接收对象";
                    operatelog.Add( logentity );
                    context.Response.Write( JsonHelper.ToJsonResult( "1", "请选择接收对象" ) );
                    return;
                }
            }
            catch( Exception ex )
            {
                logentity.otype = "1";
                logentity.logcontent += "," + ex.Message + "总的捕捉";
                operatelog.Add( logentity );
                context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
            }
        }
        /// <summary>
        /// 推送消息到电子平板(zhc 20170713)
        /// </summary>
        /// <param name="context"></param>
        private void PushMessageToECCNew( HttpContext context )
        {

            try
            {
                //消息来源
                int messagesourseid = 0;
                if( context.Request.Form["messagesourceid"] != null )
                {
                    messagesourseid = Helper.StringToInt( context.Request.Form["messagesourceid"].ToString() );
                    logentity.functionName = logentity.functionName + "--" + (EnumMessageSourceID) messagesourseid;
                }
                List<string> param1 = new List<string>() { "rootcode" };

                if( !CheckEmptyParameters( param1 ) )
                {
                    return;
                }
                List<string> param = new List<string>() { "messagesourceid", "messagetypeid", "operatetypeid", "displaymodelid", "createdid", "createdname", "rargetalias", "taskpriorityid", "taskstatusid", "tasktypeid" };

                if( !CheckEmptyParameters( param ) )
                {
                    return;
                }
                logentity.logcontent = context.Request.Form["rargetalias"].ToString();
                JPushClient client = new JPushClient( Helper.GetAppSettings( "AppKey" ), Helper.GetAppSettings( "MasterSecret" ) );
                ////消息来源
                //int messagesourseid = Helper.StringToInt(context.Request.Form["messagesourceid"].ToString());
                //logentity.functionName = logentity.functionName + "--" + (EnumMessageSourceID)messagesourseid;
                //布局类型
                int displaymodelid = Helper.StringToInt( context.Request.Form["displaymodelid"].ToString() );

                //消息类型
                int messagetypeid = Helper.StringToInt( context.Request.Form["messagetypeid"].ToString() );

                //操作类型
                int operatetypeid = Helper.StringToInt( context.Request.Form["operatetypeid"].ToString() );
                //优先级
                int taskpriorityid = Helper.StringToInt( context.Request.Form["taskpriorityid"].ToString() );
                //任务状态
                int taskstatusid = Helper.StringToInt( context.Request.Form["taskstatusid"].ToString() );
                //任务类型
                int tasktypeid = Helper.StringToInt( context.Request.Form["tasktypeid"].ToString() );
                //接收终端别名
                string rargetalias = context.Request.Form["rargetalias"].ToString();
                //操作人
                string createdid = context.Request.Form["createdid"].ToString();
                //操作人姓名
                string createdname = context.Request.Form["createdname"].ToString();

                //发送人校区编号
                string rootcode = context.Request.Form["rootcode"].ToString();

                DateTime taskbegintime = context.Request.Form["TaskBeginTime"] != null ? Helper.StringToDateTime( context.Request.Form["TaskBeginTime"].ToString() ) : new DateTime();
                DateTime taskendtime = context.Request.Form["TaskEndTime"] != null ? Helper.StringToDateTime( context.Request.Form["TaskEndTime"].ToString() ) : new DateTime();

                //图片显示秒数--默认5秒
                int imagespansecond = Helper.ObjectToInt( context.Request.Form["imagespansecond"] ) > 0 ? Helper.ObjectToInt( context.Request.Form["imagespansecond"] ) : 5;
                //图片显示效果类型--默认为1
                int imageeffectid = Helper.ObjectToInt( context.Request.Form["imageeffectid"] ) > 0 ? Helper.ObjectToInt( context.Request.Form["imageeffectid"] ) : 1;
                //消息标题
                string title = Helper.ObjectToString( context.Request.Form["title"] );
                //消息内容
                string messagecontent_text = "";

                if( operatetypeid == 4 )//软件升级
                {
                    messagecontent_text = Helper.GetAppSettings( "softwareupgradeurl" );
                    if( Helper.ObjectToString( context.Request.Form["messagecontent_text"] ) != "" )
                    {
                        messagecontent_text = Helper.GetAppSettings( "softwareupgradeurl" ) + "?Version=" + Helper.ObjectToString( context.Request.Form["messagecontent_text"] );
                    }
                    else
                    {
                        messagecontent_text = Helper.GetAppSettings( "softwareupgradeurl" ) + "?Version=9.9.9";
                    }

                }
                else if( operatetypeid == 14 ) //启动设备软件升级
                {
                    messagecontent_text = Helper.GetAppSettings( "softwareinstallerurl" );
                    if( Helper.ObjectToString( context.Request.Form["messagecontent_text"] ) != "" )
                    {
                        messagecontent_text = Helper.GetAppSettings( "softwareinstallerurl" ) + "?Version=" + Helper.ObjectToString( context.Request.Form["messagecontent_text"] );
                    }
                    else
                    {
                        messagecontent_text = Helper.GetAppSettings( "softwareinstallerurl" ) + "?Version=9.9.9";
                    }
                }
                else if( operatetypeid == 17 ) //重新初始化参数
                {
                    string classcode = context.Request.Form["messagecontent_text"].ToString();
                    if( !string.IsNullOrWhiteSpace( classcode ) )
                    {
                        //获取班级信息
                        bllDeviceClassInfo bll = new bllDeviceClassInfo();

                        dt = bll.GetPagingSigInfo( "IsValid=1" + " and JPushID in (" + classcode + ")" );
                        if( dt != null )
                        {
                            messagecontent_text = JsonHelper.DataTableToJSON( dt );
                        }
                        else
                        {
                            context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
                            return;
                        }
                    }
                    else
                    {
                        context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
                        return;
                    }



                }
                else
                {
                    messagecontent_text = Helper.ObjectToString( context.Request.Form["messagecontent_text"] );
                }

                var messagecontent_image = Helper.ObjectToArrry( context.Request.Form["messagecontent_image"] );
                var messagecontent_video = Helper.ObjectToArrry( context.Request.Form["messagecontent_video"] );
                string messagecontent_textalias = "";
                var messagecontent_imagealias = Helper.ObjectToArrry( context.Request.Form["messagecontent_imagealias"] );
                var messagecontent_videoalias = Helper.ObjectToArrry( context.Request.Form["messagecontent_videoalias"] );
                var tag = Helper.ObjectToArrry( context.Request.Form["tag"] );
                var tag_and = Helper.ObjectToArrry( context.Request.Form["tag_and"] );
                var alias = Helper.ObjectToArrry( context.Request.Form["alias"] );
                var registration_id = Helper.ObjectToArrry( context.Request.Form["registration_id"] );


                PushPayload payload = new PushPayload();
                payload.platform = Platform.all();
                if( tag != null || tag_and != null || alias != null || registration_id != null )
                {
                    payload.audience = Audience.all();
                    if( tag != null )
                    {
                        payload.audience = payload.audience.tag( tag );
                    }
                    if( tag_and != null )
                    {
                        payload.audience = payload.audience.tag_and( tag_and );
                    }
                    if( alias != null )
                    {
                        payload.audience = payload.audience.alias( alias );
                    }
                    if( registration_id != null )
                    {
                        payload.audience = payload.audience.registrationId( registration_id );
                    }
                    payload.options = new Options();
                    payload.options.apns_production = true;
                    payload.options.time_to_live = 86400;//(1天)



                    DeviceTaskInfoEntity deviceTaskInfo = new DeviceTaskInfoEntity();
                    deviceTaskInfo.DisplayModelID = displaymodelid;
                    deviceTaskInfo.MessageSourceID = messagesourseid;
                    deviceTaskInfo.MessageTypeID = messagetypeid;
                    deviceTaskInfo.OperateTypeID = operatetypeid;

                    deviceTaskInfo.MessageTitle = title;
                    deviceTaskInfo.MessageContent = new MessageContent();
                    deviceTaskInfo.TaskPriorityID = taskpriorityid;
                    deviceTaskInfo.TaskStatusID = taskstatusid;
                    deviceTaskInfo.TaskTypeID = tasktypeid;
                    deviceTaskInfo.MessageContent.text = messagecontent_text;
                    deviceTaskInfo.MessageContent.image = messagecontent_image;
                    deviceTaskInfo.MessageContent.video = messagecontent_video;
                    //zhc 模版信息加载
                    object ClsActiveStr = context.Request.Form["ClsActive"];
                    object ClsHonorStr = context.Request.Form["ClsHonor"];
                    object ClsHomeWkStr = context.Request.Form["ClsHomeWk"];
                    object ClsCheckItemStr = context.Request.Form["ClsCheckItem"];
                    object ClsCheckStuStr = context.Request.Form["ClsCheckStu"];
                    object ClsNoticeStr = context.Request.Form["ClsNotice"];

                    if( ClsActiveStr != null || ClsHonorStr.ToString() != "[]" || ClsHomeWkStr.ToString() != "[]" || ClsCheckItemStr.ToString() != "[]" || ClsCheckStuStr != null || ClsNoticeStr.ToString() != "[]" )
                    {
                        deviceTaskInfo.MessageContent.AreaModule = new MessageContent.AreaModuleClass();
                        //班级活动
                        if( ClsActiveStr != null )
                        {
                            deviceTaskInfo.MessageContent.AreaModule.ClsActive = new MessageContent.ClsActive();
                            deviceTaskInfo.MessageContent.AreaModule.ClsActive = JsonHelper.JsonToObject<MessageContent.ClsActive>( ClsActiveStr.ToString() );
                        }
                        //班级荣誉
                        if( ClsHonorStr.ToString() != "[]" )
                        {
                            deviceTaskInfo.MessageContent.AreaModule.ClsHonor = new List<MessageContent.ClsHonor>();
                            deviceTaskInfo.MessageContent.AreaModule.ClsHonor = JsonHelper.DeserializeJsonToList<MessageContent.ClsHonor>( ClsHonorStr.ToString() );
                        }
                        //作业布置
                        if( ClsHomeWkStr.ToString() != "[]" )
                        {
                            deviceTaskInfo.MessageContent.AreaModule.ClsHomeWk = new List<MessageContent.ClsHomeWk>();
                            deviceTaskInfo.MessageContent.AreaModule.ClsHomeWk = JsonHelper.DeserializeJsonToList<MessageContent.ClsHomeWk>( ClsHomeWkStr.ToString() );
                        }
                        //指标检查考勤
                        if( ClsCheckItemStr.ToString() != "[]" )
                        {
                            deviceTaskInfo.MessageContent.AreaModule.ClsCheckItem = new List<MessageContent.ClsCheckItem>();
                            deviceTaskInfo.MessageContent.AreaModule.ClsCheckItem = JsonHelper.DeserializeJsonToList<MessageContent.ClsCheckItem>( ClsCheckItemStr.ToString() );
                        }
                        //学生出勤考勤
                        if( ClsCheckStuStr != null )
                        {
                            deviceTaskInfo.MessageContent.AreaModule.ClsCheckStu = new MessageContent.ClsCheckStu();
                            deviceTaskInfo.MessageContent.AreaModule.ClsCheckStu = JsonHelper.JsonToObject<MessageContent.ClsCheckStu>( ClsCheckStuStr.ToString() );
                        }
                    }

                    deviceTaskInfo.MessageContentAlias = new MessageContent();
                    deviceTaskInfo.MessageContentAlias.text = messagecontent_textalias;
                    deviceTaskInfo.MessageContentAlias.image = messagecontent_imagealias;
                    deviceTaskInfo.MessageContentAlias.video = messagecontent_videoalias;
                    deviceTaskInfo.TaskBeginTime = taskbegintime;
                    deviceTaskInfo.TaskEndTime = taskendtime;
                    deviceTaskInfo.ImageEffectID = imageeffectid;
                    deviceTaskInfo.ImageSpanSecond = imagespansecond;
                    deviceTaskInfo.TargetRange = payload.audience != null ? JsonHelper.ObjectToJSON( payload.audience.dictionary ) : "";
                    deviceTaskInfo.Tag = context.Request.Form["tag"];
                    deviceTaskInfo.Tag_and = context.Request.Form["tag_and"];
                    deviceTaskInfo.Alias = context.Request.Form["alias"];
                    deviceTaskInfo.Registration_ID = context.Request.Form["registration_id"];
                    deviceTaskInfo.RargetAlias = rargetalias;
                    deviceTaskInfo.CreatedID = createdid;
                    deviceTaskInfo.CreatedName = createdname;

                    //操作=
                    bllDeviceTaskInfo bll = new bllDeviceTaskInfo();
                    int errorcode = 0; string errormsg = string.Empty;
                    try
                    {
                        if( bll.Add( ref deviceTaskInfo, logentity, rootcode, out errorcode, out errormsg ) == 0 )
                        {
                            try
                            {
                                //通知公告
                                if( ClsNoticeStr.ToString() != "[]" && ClsNoticeStr != null )
                                {
                                    string NoticeStr = HttpContext.Current.Server.UrlDecode( ClsNoticeStr.ToString() );
                                    List<DeviceTaskNoticeInfo> Noticelist = new List<DeviceTaskNoticeInfo>();
                                    Noticelist = JsonHelper.DeserializeJsonToList<DeviceTaskNoticeInfo>( NoticeStr );
                                    deviceTaskInfo.MessageContent.AreaModule.ClsNotice = new List<MessageContent.ClsNotice>();
                                    for( int i = 0; i < Noticelist.Count; i++ )
                                    {
                                        Noticelist[i].taskCode = deviceTaskInfo.Code;
                                        DeviceTaskNoticeInfo Notice = Noticelist[i];

                                        if( bll.AddNotice( ref Notice ) == 0 )
                                        {
                                            MessageContent.ClsNotice ClsNotice = new MessageContent.ClsNotice();
                                            ClsNotice.title = Notice.noticeTitle;
                                            ClsNotice.context = Notice.noticeContent;
                                            ClsNotice.date = Notice.noticeTime;
                                            ClsNotice.code = deviceTaskInfo.Code;//撤销Code

                                            ClsNotice.url = "NoticeInfo.html?id=" + Notice.noticeId;
                                            deviceTaskInfo.MessageContent.AreaModule.ClsNotice.Add( ClsNotice );
                                        };
                                    }
                                }
                                if( ClsActiveStr != null )
                                {
                                    deviceTaskInfo.MessageContent.AreaModule.ClsActive.code = deviceTaskInfo.Code;//撤销Code
                                }
                                if( ClsCheckStuStr != null )
                                {
                                    deviceTaskInfo.MessageContent.AreaModule.ClsCheckStu.code = deviceTaskInfo.Code;//撤销Code
                                }

                                if( ClsCheckItemStr.ToString() != "[]" )
                                {
                                    for( int i = 0; i < deviceTaskInfo.MessageContent.AreaModule.ClsCheckItem.Count; i++ )
                                    {
                                        deviceTaskInfo.MessageContent.AreaModule.ClsCheckItem[i].code = deviceTaskInfo.Code;//撤销Code
                                    }

                                }
                                if( ClsHonorStr.ToString() != "[]" )
                                {
                                    for( int i = 0; i < deviceTaskInfo.MessageContent.AreaModule.ClsHonor.Count; i++ )
                                    {
                                        deviceTaskInfo.MessageContent.AreaModule.ClsHonor[i].code = deviceTaskInfo.Code;//撤销Code
                                    }
                                }
                                if( ClsHomeWkStr.ToString() != "[]" )
                                {
                                    for( int i = 0; i < deviceTaskInfo.MessageContent.AreaModule.ClsHomeWk.Count; i++ )
                                    {
                                        deviceTaskInfo.MessageContent.AreaModule.ClsHomeWk[i].code = deviceTaskInfo.Code;//撤销Code
                                    }
                                }

                                if( deviceTaskInfo.OperateTypeID == 13 || deviceTaskInfo.OperateTypeID == 15 )
                                {
                                    string clearHeartBeatCheckDateFilter = "";
                                    if( deviceTaskInfo.Registration_ID != "" )//班级
                                    {
                                        clearHeartBeatCheckDateFilter = deviceTaskInfo.Registration_ID;

                                        clearHeartBeatCheckDateFilter = "[JPushID] in (" + clearHeartBeatCheckDateFilter.Replace( "\"", "'" ).Trim( '[' ).Trim( ']' ) + ")";
                                        //过滤不等空的JPushID
                                        //clearVersionFilter = clearVersionFilter + " and [JPushID]!='' and [JPushID] is not null ";
                                    }
                                    else if( deviceTaskInfo.Tag != "" && deviceTaskInfo.Tag_and != "" )//年级
                                    {
                                        // clearVersionFilter=["G012014","G012015"] ["SC1100009000","RC1006"]
                                        string grades = deviceTaskInfo.Tag.Replace( "\"", "'" ).Trim( ']' ).Trim( '[' );
                                        string[] rootsarry = deviceTaskInfo.Tag_and.Replace( "\"", "" ).Trim( ']' ).Trim( '[' ).Split( ',' );
                                        string school = "";
                                        string root = "";
                                        if( rootsarry.Length == 2 )
                                        {
                                            if( rootsarry[0].Contains( "SC" ) )
                                            {
                                                school = "\'" + rootsarry[0].Replace( "SC", "" ).Trim() + "\'";
                                            }
                                            if( rootsarry[1].Contains( "RC" ) )
                                            {
                                                root = rootsarry[1].Replace( "RC", "" ).Trim();
                                                grades = grades.Replace( "G", root );
                                            }
                                        }
                                        clearHeartBeatCheckDateFilter = string.Format( "SchoolCode={0} and SUBSTRING(ClassCode,7,16) in ({1})", school, grades );
                                    }
                                    else if( deviceTaskInfo.Tag_and != "" )//校区或者学科
                                    {
                                        string[] rootsarry = deviceTaskInfo.Tag_and.Trim( ']' ).Trim( '[' ).Split( ',' );
                                        string school = "";
                                        string root = "";
                                        string classtype = "";
                                        string subjecttype = "";
                                        if( rootsarry.Length == 2 )
                                        {
                                            if( rootsarry[0].Contains( "SC" ) )
                                            {
                                                school = rootsarry[0].Replace( "SC", "" ).Replace( '"', '\'' );
                                            }
                                            if( rootsarry[1].Contains( "RC" ) )
                                            {
                                                root = rootsarry[1].Replace( "RC", "" ).Replace( '"', ' ' ).Trim();

                                            }
                                            clearHeartBeatCheckDateFilter = string.Format( "SchoolCode={0} and SUBSTRING(ClassCode,7,4) = '{1}'", school, root );
                                        }
                                        else if( rootsarry.Length == 3 )
                                        {
                                            if( rootsarry[0].Contains( "SC" ) )
                                            {
                                                school = rootsarry[0].Replace( "SC", "" ).Replace( '"', '\'' );
                                            }
                                            if( rootsarry[1].Contains( "RC" ) )
                                            {
                                                root = rootsarry[1].Replace( "RC", "" ).Replace( '"', ' ' ).Trim();

                                            }
                                            if( rootsarry[2].Contains( "L" ) )
                                            {
                                                classtype = rootsarry[2].Replace( "L", "" ).Replace( '"', ' ' ).Trim();

                                            }
                                            else if( rootsarry[2].Contains( "S" ) )
                                            {
                                                subjecttype = rootsarry[2].Replace( "S", "" ).Replace( '"', ' ' ).Trim();
                                            }
                                            clearHeartBeatCheckDateFilter = string.Format( "SchoolCode={0} and SUBSTRING(ClassCode,7,4) = '{1}'", school, root );
                                            if( classtype != "" )
                                            {
                                                clearHeartBeatCheckDateFilter += string.Format( " and classtypeID={0}", classtype );
                                            }
                                            if( subjecttype != "" )
                                            {
                                                clearHeartBeatCheckDateFilter += string.Format( " and SubjectTypeID={0}", subjecttype );
                                            }
                                        }
                                        else if( rootsarry.Length == 4 )
                                        {
                                            if( rootsarry[0].Contains( "SC" ) )
                                            {
                                                school = rootsarry[0].Replace( "SC", "" ).Replace( '"', '\'' );
                                            }
                                            if( rootsarry[1].Contains( "RC" ) )
                                            {
                                                root = rootsarry[1].Replace( "RC", "" ).Replace( '"', ' ' ).Trim();

                                            }
                                            if( rootsarry[2].Contains( "L" ) )
                                            {
                                                classtype = rootsarry[2].Replace( "L", "" ).Replace( '"', ' ' ).Trim();

                                            }
                                            if( rootsarry[3].Contains( "S" ) )
                                            {
                                                subjecttype = rootsarry[3].Replace( "S", "" ).Replace( '"', ' ' ).Trim();
                                            }
                                            clearHeartBeatCheckDateFilter = string.Format( "SchoolCode={0} and SUBSTRING(ClassCode,7,4) = '{1}'", school, root );
                                            if( classtype != "" )
                                            {
                                                clearHeartBeatCheckDateFilter += string.Format( " and classtypeID={0}", classtype );
                                            }
                                            if( subjecttype != "" )
                                            {
                                                clearHeartBeatCheckDateFilter += string.Format( " and SubjectTypeID={0}", subjecttype );
                                            }
                                        }

                                    }
                                    if( clearHeartBeatCheckDateFilter != "" )
                                    {
                                        clearHeartBeatCheckDateFilter = " OR (" + clearHeartBeatCheckDateFilter + ")";
                                    }
                                    if( deviceTaskInfo.OperateTypeID == 13 )
                                    {
                                        if( bll.ClearHeartBeatCheckDateByJPushID( clearHeartBeatCheckDateFilter ) > 0 )
                                        {

                                        }
                                    }

                                    if( deviceTaskInfo.OperateTypeID == 15 )
                                    {
                                        if( bll.ClearInstallerHeartBeatCheckDateByJPushID( clearHeartBeatCheckDateFilter ) > 0 )
                                        {

                                        }
                                    }
                                }
                                CustomMsgContent customMsgContent = JsonHelper.ObjectToObject<DeviceTaskInfoEntity, CustomMsgContent>( deviceTaskInfo );
                                payload.message = Message.content( customMsgContent );

                                payload.message.setTitle( title );
                                MessageResult result = client.SendPush( payload );


                                //操作
                                if( result.isResultOK() )
                                {
                                    context.Response.Write( JsonHelper.ToJsonResult( "0", "success" ) );
                                    return;
                                }
                                else
                                {
                                    bll.UpdateTaskResultID( deviceTaskInfo.Code, "1" );
                                    logentity.otype = "0";
                                    logentity.logcontent += ",发送消息失败";
                                    operatelog.Add( logentity );
                                    context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
                                    return;
                                }

                            }
                            catch( Exception ex )
                            {
                                bll.UpdateTaskResultID( deviceTaskInfo.Code, "1" );
                                logentity.otype = "1";
                                logentity.logcontent += "," + ex.Message + "OperateTypeID=13,15时，分析jpushid，清空版本号";
                                operatelog.Add( logentity );
                                context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
                                return;
                            }
                        }
                        else
                        {
                            logentity.otype = "0";
                            logentity.logcontent += ",插入消息任务到数据库失败";
                            operatelog.Add( logentity );
                            context.Response.Write( JsonHelper.ToJsonResult( errorcode.ToString(), errormsg ) );
                            return;
                        }
                    }
                    catch( Exception ex )
                    {
                        logentity.otype = "1";
                        logentity.logcontent += "," + ex.Message + "插入任务";
                        operatelog.Add( logentity );
                        context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
                        return;
                    }
                }
                else
                {
                    logentity.otype = "0";
                    logentity.logcontent += ",发送消息时请选择接收对象";
                    operatelog.Add( logentity );
                    context.Response.Write( JsonHelper.ToJsonResult( "1", "请选择接收对象" ) );
                    return;
                }
            }
            catch( Exception ex )
            {
                logentity.otype = "1";
                logentity.logcontent += "," + ex.Message + "总的捕捉";
                operatelog.Add( logentity );
                context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
            }
        }

        private void PushJSONNotificationToAPP( HttpContext context )
        {

        }

        /// <summary>
        /// 修改班级设备信息
        /// </summary>
        /// <param name="context"></param>
        private void UpdateDeviceClass( HttpContext context )
        {
            try
            {
                List<string> param = new List<string>() { "ID" };

                if( !CheckParameters( param ) )
                {
                    return;
                }
                bllDeviceClassInfo bll = new bllDeviceClassInfo();
                string ID = context.Request.Form["ID"].ToString();
                string BanZhuRenPhotoPath = context.Request.Form["BanZhuRenPhotoPath"].ToString();
                string BanZhuRenQRPath = context.Request.Form["BanZhuRenQRPath"].ToString();
                string RoomNum = context.Request.Form["RoomNum"].ToString();
                string ClassNickName = context.Request.Form["ClassNickName"].ToString();
                string ClassSlogan = context.Request.Form["ClassSlogan"].ToString();
                string ZuoYouMing = context.Request.Form["ZuoYouMing"].ToString();
                string Introduction = context.Request.Form["Introduction"].ToString();
                string Recommended = context.Request.Form["Recommended"].ToString();
                string ClassLogoPath = context.Request.Form["ClassLogoPath"].ToString();
                string ClassQRPath = context.Request.Form["ClassQRPath"].ToString();
                string ModefiedID = context.Request.Form["ModefiedID"].ToString();
                string ModifiedName = context.Request.Form["ModifiedName"].ToString();
                int errorcode = 0; string errormsg = string.Empty;

                dt = bll.UpdateDeviceClass( out errorcode, out errormsg, ID, BanZhuRenPhotoPath, BanZhuRenQRPath, RoomNum, ClassNickName, ClassSlogan, ZuoYouMing, Introduction, Recommended, ClassLogoPath, ClassQRPath, ModefiedID, ModifiedName );
                if( errorcode != 0 )
                {
                    logentity.otype = "0";
                    logentity.logcontent = "更新班级电子设备信息失败：" + errorcode;
                    operatelog.Add( logentity );
                }
                ReturnListJson( dt );
            }
            catch( Exception ex )
            {
                logentity.otype = "1";
                logentity.logcontent = ex.Message;
                operatelog.Add( logentity );
                context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
            }
        }

        /// <summary>
        ////获取用户下的班级信息( zhc 20170727)
        /// </summary>
        /// <param name="context"></param>
        private void GetUserClass( HttpContext context )
        {
            try
            {
                bllDict bll = new bllDict();
                string userCode = context.Request.Form["userCode"] != null ? context.Request.Form["userCode"].ToString() : "";
                string TemplateJson = bll.GetUserClassToJson( userCode );
                context.Response.Write( TemplateJson );
            }
            catch( Exception ex )
            {
                logentity.otype = "1";
                logentity.logcontent = ex.Message;
                operatelog.Add( logentity );
                context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
            }
        }
    }
}