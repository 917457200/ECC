using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using cn.jpush.api;
using cn.jpush.api.push;
using cn.jpush.api.push.mode;
using EDUC.Common.Bll;
using EDUC.Common.Dal;
using EDUC.Common.Model;
using PublicLib;

namespace EastElite.ECC
{
    public class PushToStartupProgramAPI : ServiceBase
    {
        DataTable dt = new DataTable();
        #region (public) 基本请求入口 DoProcess
        public void _DoProcess(HttpContext context)
        {
            try
            {
                if (CheckLongin(context))
                {
                    //记录日志信息

                    logentity.module = "启动程序模块";//模块名称
                    logentity.pageurl = GetQueryUrl();
                    switch (context.Request.QueryString[0])
                    {
                        //发送任务
                        case "06-01":
                            logentity.functionName = "发送任务";
                            //operatelog.Add(logentity);
                            PushTaskToSP(context);
                            break;
                        //获取任务
                        case "06-02":
                            logentity.functionName = "获取任务";
                            //operatelog.Add(logentity);
                            GetSPTask(context);
                            break;
                        //初始化启动程序
                        case "06-03":
                            logentity.functionName = "初始化启动程序";
                            //operatelog.Add(logentity);
                            InitSP(context);
                            break;
                        case "06-04":
                            logentity.functionName = "上传数据库";
                            //operatelog.Add(logentity);
                            UploadDatabase(context);
                            break;
                        case "06-05":
                            logentity.functionName = "下载数据库";
                            //operatelog.Add(logentity);
                            DownloadDatabase(context);
                            break;
                        case "06-06":
                            logentity.functionName = "检查启动程序版本回调";
                            //operatelog.Add(logentity);
                            UpdateSPVersion(context);
                            break;
                        case "06-07":
                            logentity.functionName = "删除启动程序任务";
                            //operatelog.Add(logentity);
                            DeleteTask(context);
                            break;
                        default:
                            logentity.otype = "1";
                            logentity.logcontent = "没有找到提供的该方法"; ;
                            operatelog.Add(logentity);
                            context.Response.Write("没有找到提供的该方法");
                            break;
                    }
                }
                else
                {
                    logentity.otype = "1";
                    logentity.logcontent = "未登录";
                    operatelog.Add(logentity);
                }
            }
            catch (Exception ex)
            {
                logentity.otype = "1";
                logentity.logcontent = ex.Message;
                operatelog.Add(logentity);
                context.Response.Write(ex.Message);
            }
        }
        #endregion

        protected enum EnumMessageSourceID { 电子班牌平台 = 1, 数字校园平台 = 2, 手机智慧校园 = 3, 微信服务号 = 4 }
        private void PushTaskToSP(HttpContext context)
        {
            try
            {
                //消息来源
                int messagesourseid = 0;
                if (context.Request.Form["messagesourceid"] != null)
                {
                    messagesourseid = Helper.StringToInt(context.Request.Form["messagesourceid"].ToString());
                    logentity.functionName = logentity.functionName + "--" + (EnumMessageSourceID)messagesourseid;

                }
                List<string> param = new List<string>() { "messagesourceid", "operatetypeid", "createdid", "createdname", "rootcode", "rargetalias", "rootcode", "TaskStatusID" };
                if (!CheckParameters(param))
                {
                    return;
                }
                logentity.logcontent = context.Request.Form["rargetalias"].ToString();
                JPushClient client = new JPushClient(Helper.GetAppSettings("SPAppKey"), Helper.GetAppSettings("SPMasterSecret"));
                //操作类型
                int operatetypeid = Helper.StringToInt(context.Request.Form["operatetypeid"].ToString());
                int TaskStatusID = Helper.StringToInt(context.Request.Form["TaskStatusID"].ToString());
                //接收终端别名
                string rargetalias = context.Request.Form["rargetalias"].ToString();
                //操作人
                string createdid = context.Request.Form["createdid"].ToString();
                //操作人姓名
                string createdname = context.Request.Form["createdname"].ToString();
                //发送人校区编号
                string rootcode = context.Request.Form["rootcode"].ToString();
                var tag = Helper.ObjectToArrry(context.Request.Form["tag"]);
                var tag_and = Helper.ObjectToArrry(context.Request.Form["tag_and"]);
                var alias = Helper.ObjectToArrry(context.Request.Form["alias"]);
                var registration_id = Helper.ObjectToArrry(context.Request.Form["registration_id"]);
                PushPayload payload = new PushPayload();
                payload.platform = Platform.all();
                if (tag != null || tag_and != null || alias != null || registration_id != null)
                {
                    payload.audience = Audience.all();
                    if (tag != null)
                    {
                        payload.audience = payload.audience.tag(tag);
                    }
                    if (tag_and != null)
                    {
                        payload.audience = payload.audience.tag_and(tag_and);
                    }
                    if (alias != null)
                    {
                        payload.audience = payload.audience.alias(alias);
                    }
                    if (registration_id != null)
                    {
                        payload.audience = payload.audience.registrationId(registration_id);
                    }
                    payload.options = new Options();
                    payload.options.apns_production = true;
                    payload.options.time_to_live = 86400;//(1天)
                    StartupProgramTaskInfoEntity task = new StartupProgramTaskInfoEntity();
                    task.OperateTypeID = operatetypeid;
                    task.MessageSourceID = messagesourseid;
                    task.TargetRange = payload.audience != null ? JsonHelper.ObjectToJSON(payload.audience.dictionary) : "";
                    task.Tag = context.Request.Form["tag"];
                    task.Tag_and = context.Request.Form["tag_and"];
                    task.Alias = context.Request.Form["alias"];
                    task.Registration_ID = context.Request.Form["registration_id"];
                    task.RargetAlias = rargetalias;
                    task.CreatedID = createdid;
                    task.CreatedName = createdname;
                    task.TaskStatusID = TaskStatusID;
                    bllStartupProgramTaskInfo bll = new bllStartupProgramTaskInfo();
                    int errorcode = 0; string errormsg = string.Empty;
                    if (bll.Add(ref task, logentity, rootcode, out errorcode, out errormsg) == 0)
                    {

                        BaseCustomMsgContent customMsgContent = new BaseCustomMsgContent();
                        customMsgContent.Code = task.Code;
                        customMsgContent.OperateTypeID = task.OperateTypeID;
                        customMsgContent.MessageSourceID = task.MessageSourceID;
                        //customMsgContent.MessageTitle = "";
                        BaseMessageContent mess = new BaseMessageContent();
                        if (operatetypeid == 1)//电子班牌升级
                        {
                            mess.text = Helper.GetAppSettings("softwareupgradeurl");
                            if (Helper.ObjectToString(context.Request.Form["messagecontent_text"]) != "")
                            {
                                mess.text = Helper.GetAppSettings("softwareupgradeurl") + "?Version=" + Helper.ObjectToString(context.Request.Form["messagecontent_text"]);
                            }
                            else
                            {
                                mess.text = Helper.GetAppSettings("softwareupgradeurl") + "?Version=9.9.9";
                            }
                        }
                        else if (operatetypeid == 3) //启动设备软件升级
                        {
                            mess.text = Helper.GetAppSettings("softwareinstallerurl");
                            if (Helper.ObjectToString(context.Request.Form["messagecontent_text"]) != "")
                            {
                                mess.text = Helper.GetAppSettings("softwareinstallerurl") + "?Version=" + Helper.ObjectToString(context.Request.Form["messagecontent_text"]);
                            }
                            else
                            {
                                mess.text = Helper.GetAppSettings("softwareinstallerurl") + "?Version=9.9.9";
                            }
                        }
                        //mess.text = context.Request.Form["messagecontent_text"].ToString();
                        customMsgContent.MessageContent = mess;
                        payload.message = Message.content(customMsgContent);

                        payload.message.setTitle("");
                        try
                        {
                            MessageResult result = client.SendPush(payload);
                            //操作
                            if (result.isResultOK())
                            {
                                context.Response.Write(JsonHelper.ToJsonResult("0", "success"));
                                return;
                            }
                            else
                            {
                                logentity.otype = "0";
                                logentity.logcontent += ",发送消息失败";
                                operatelog.Add(logentity);
                                context.Response.Write(JsonHelper.ToJsonResult("1", "failure"));
                                return;
                            }
                        }
                        catch
                        {
                           
                            logentity.logcontent += ",请检查终端目标是否存在";
                            operatelog.Add(logentity);
                            context.Response.Write(JsonHelper.ToJsonResult("1", "发送失败，请检查终端目标是否存在"));
                            return;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                logentity.otype = "1";
                logentity.logcontent += "," + ex.Message;
                operatelog.Add(logentity);
                context.Response.Write(JsonHelper.ToJsonResult("1", "failure"));
            }
        }

        private void GetSPTask(HttpContext context)
        {
            try
            {
                //必填参数
                List<string> param = new List<string>() { "pageSize", "currentPage" };
                //必填参数检查
                if (!CheckParameters(param))
                {
                    return;
                }
                bllStartupProgramTaskInfo bll = new bllStartupProgramTaskInfo();
                int pageSize = Helper.StringToInt(context.Request.Form["pageSize"].ToString());
                int currentPage = Helper.StringToInt(context.Request.Form["currentPage"].ToString());

                string filter = "where TaskStatusID=1";//正常并且发送成功
                //可选参数
                Dictionary<string, EnumSearchType> optionalParameters = new Dictionary<string, EnumSearchType> { 
            { "messageSourceID", EnumSearchType.AndInt } 
            ,{ "operatetypeid", EnumSearchType.AndInt }
            ,{ "stime", EnumSearchType.AndSTime }
            ,{ "etime", EnumSearchType.AndETime }
            ,{ "createdid", EnumSearchType.AndString }
            };
                //可选参数检查
                filter = CheckOptionalParameters(optionalParameters, filter);
                if (context.Request.Form["rootcode"] != null && context.Request.Form["rootcode"].ToString().Trim() != "")
                {
                    filter += string.Format(" and SUBSTRING(code,1,10)='{0}'", context.Request.Form["rootcode"].ToString());
                }
                if (context.Request.Form["className"] != null && context.Request.Form["className"].ToString().Trim() != "")
                {
                    filter += string.Format(" and LEN(Registration_ID)=23 and RargetAlias LIKE '%{0}%'", context.Request.Form["className"].ToString());

                }
                string order = "ID desc";

                int recordCount = 0;
                int totalPage = 0;
                dt = bll.GetPagingListInfo(pageSize, currentPage, filter, order, out recordCount, out totalPage);

                ReturnListJson(dt, pageSize, recordCount, currentPage, totalPage);
            }
            catch (Exception ex)
            {
                logentity.otype = "1";
                logentity.logcontent = ex.Message;
                operatelog.Add(logentity);
                context.Response.Write(JsonHelper.ToJsonResult("1", "failure"));
            }
        }


        private void InitSP(HttpContext context)
        {
            try
            {
                List<string> param = new List<string>() { "DeviceSN", "SPJPushID", "Version" };

                if (!CheckParameters(param))
                {
                    return;
                }
                string deviceSN = context.Request.Form["deviceSN"].ToString();

                string SPJPushID = context.Request.Form["SPJPushID"].ToString();

                string version = context.Request.Form["version"].ToString();


                bllStartupProgramTaskInfo bll = new bllStartupProgramTaskInfo();

                dt = bll.InitSP(deviceSN, SPJPushID, version, logentity);

                ReturnListJson(dt);
            }
            catch (Exception ex)
            {
                logentity.otype = "1";
                logentity.logcontent += "," + ex.Message;
                operatelog.Add(logentity);
                context.Response.Write(JsonHelper.ToJsonResult("1", "failure"));
            }
        }


        private void UploadDatabase(HttpContext context)
        {
            try
            {
                List<string> param = new List<string>() { "DeviceSN" };
                if (!CheckParameters(param))
                {
                    return;
                }
                string DeviceSN = context.Request.Form["DeviceSN"];
                //获取配置文件路径
                string strurl = "/uploads/DataBase/";//拼接上传路径   上传文件的相对路径
                string strpath = context.Server.MapPath(strurl);//上传文件绝对路径  
                if (!Directory.Exists(strpath))
                {
                    //若不存在则创建
                    Directory.CreateDirectory(strpath);
                }

                dynamic dyn = new { url = strurl, path = strpath };

                if (HttpContext.Current.Request.Files.Count > 0)
                {

                    HttpPostedFile file = HttpContext.Current.Request.Files[0];
                    Stream stream = file.InputStream;
                    string filname = Path.GetFileName(file.FileName);//上传文件名称

                    string newfilename = DeviceSN + Path.GetExtension(file.FileName);

                    string path = dyn.path + newfilename;//文件绝对路径
                    string url = dyn.url + newfilename;//文件相对路径

                    //保存文件
                    file.SaveAs(path);
                    context.Response.Write(JsonHelper.ToJsonResult("0", "success"));
                }
                else
                {
                    logentity.otype = "1";
                    logentity.logcontent = DeviceSN + "没有获取到文件";
                    operatelog.Add(logentity);
                    context.Response.Write(JsonHelper.ToJsonResult("1", "failure:没有获取到关于" + DeviceSN + "的文件"));
                }
            }
            catch (Exception ex)
            {
                logentity.otype = "1";
                logentity.logcontent = ex.Message;
                operatelog.Add(logentity);
                context.Response.Write(JsonHelper.ToJsonResult("1", "failure"));
            }
        }

        private void DownloadDatabase(HttpContext context)
        {
            try
            {
                List<string> param = new List<string>() { "DeviceSN" };
                if (!CheckParameters(param))
                {
                    return;
                }
                string DeviceSN = context.Request.Form["DeviceSN"];
                //获取配置文件路径
                string strurl = "/uploads/DataBase/" + DeviceSN + ".db";//拼接上传路径   上传文件的相对路径
                string strpath = context.Server.MapPath(strurl);//上传文件绝对路径  

                if (File.Exists(strpath))
                {
                    strurl = Regex.Replace(context.Request.Url.ToString(), "/Service.ashx.*", strurl);
                    context.Response.Write(JsonHelper.ToJsonResult("0", strurl));
                }
                else
                {
                    logentity.otype = "1";
                    logentity.logcontent = DeviceSN + "没有找到文件";
                    operatelog.Add(logentity);
                    context.Response.Write(JsonHelper.ToJsonResult("1", "failure:没有找到关于" + DeviceSN + "的文件"));
                }

            }
            catch (Exception ex)
            {
                logentity.otype = "1";
                logentity.logcontent = ex.Message;
                operatelog.Add(logentity);
                context.Response.Write(JsonHelper.ToJsonResult("1", "failure"));
            }
        }

        private void UpdateSPVersion(HttpContext context)
        {
            try
            {
                List<string> param = new List<string>() { "DeviceSN", "Vesion", "CheckDate" };
                if (!CheckParameters(param))
                {
                    return;
                }
                string DeviceSN = context.Request.Form["DeviceSN"];
                string Vesion = context.Request.Form["Vesion"];
                string CheckDate = context.Request.Form["CheckDate"];
                bllStartupProgramTaskInfo bll = new bllStartupProgramTaskInfo();
                int result = bll.UpdateSPVersion(DeviceSN, Vesion, CheckDate);
                if (result == 0)
                {
                    context.Response.Write(JsonHelper.ToJsonResult("0", "成功"));
                }
                else
                {
                    logentity.otype = "0";
                    logentity.logcontent += ",修改数据库返回内容错误" + result;
                    operatelog.Add(logentity);
                    context.Response.Write(JsonHelper.ToJsonResult("1", "失败"));
                }



            }
            catch (Exception ex)
            {
                logentity.otype = "1";
                logentity.logcontent = ex.Message;
                operatelog.Add(logentity);
                context.Response.Write(JsonHelper.ToJsonResult("1", "failure"));
            }
        }

        private void DeleteTask(HttpContext context)
        {
            try
            {
                List<string> param = new List<string>() { "ids" };
                if (!CheckParameters(param))
                {
                    return;
                }
                string ids = context.Request.Form["ids"].ToString();
                bllStartupProgramTaskInfo bll = new bllStartupProgramTaskInfo();
                int result = bll.DeleteDeviceTask(ids);
                if (result == 0)
                {
                    context.Response.Write(JsonHelper.ToJsonResult("0", "success"));
                }
                else
                {
                    logentity.otype = "0";
                    logentity.logcontent = "删除任务失败";
                    operatelog.Add(logentity);
                    context.Response.Write(JsonHelper.ToJsonResult("1", "failure"));
                }
            }
            catch (Exception ex)
            {
                logentity.otype = "1";
                logentity.logcontent = ex.Message;
                operatelog.Add(logentity);
            }
        }
    }
}