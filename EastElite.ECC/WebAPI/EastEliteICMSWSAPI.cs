using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using EDUC.Common.Bll;
using EDUC.Common.Model;
using PublicLib;
using System.ServiceModel;

namespace EastElite.ECC
{
    public class EastEliteICMSWSAPI : ServiceBase
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
                    logentity.module = "数字校园模块";//模块名称
                    logentity.pageurl = GetQueryUrl();
                    switch (context.Request.QueryString[0])
                    {
                        //登陆验证接口
                        case "03-01":
                            logentity.functionName = "登陆验证接口";
                            //operatelog.Add(logentity);
                            CheckUserLoginInfoItem(context);
                            break;
                        //外部登陆验证接口
                        case "03-02":
                            logentity.functionName = "外部登陆验证接口";
                            //operatelog.Add(logentity);
                            CheckUserExternalLoginInfoItem(context);
                            break;
                        //同步设备接口
                        case "03-03":
                            logentity.functionName = "同步设备接口";
                            //operatelog.Add(logentity);
                            SynchronizDevice(context);
                            break;
                        //同步班级接口
                        case "03-04":
                            logentity.functionName = "同步班级接口";
                            //operatelog.Add(logentity);
                            SynchronizClass(context);
                            break;
                        //同步字典接口
                        case "03-05":
                            logentity.functionName = "同步字典接口";
                            //operatelog.Add(logentity);
                            SynchronizDict(context);
                            break;
                        //获取1078教师
                        case "03-06":
                            logentity.functionName = "获取1078教师";
                            //operatelog.Add(logentity);
                            GetECCUserRoleInfoList(context);
                            break;
                        //获取1078教师
                        case "03-07":
                            logentity.functionName = "获取1078教师";
                            //operatelog.Add(logentity);
                            GetClassListByRootCode(context);
                            break;
                        //获取1078教师
                        case "03-08":
                            logentity.functionName = "获取班级信息";
                            //operatelog.Add(logentity);
                            GetClassListInfo( context );
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
        private void GetClassListByRootCode(HttpContext context)
        {
            try
            {
                List<string> param = new List<string>() { "rootCode", "userCode" };

                if (!CheckParameters(param))
                {
                    return;
                }

                string rootCode = context.Request.Form["rootCode"].ToString();
                string userCode = context.Request.Form["userCode"].ToString();

                EastEliteICMSWS.EastEliteICMSWSSoapClient client = new EastEliteICMSWS.EastEliteICMSWSSoapClient();
                string result = client.GetECCDeviceClassList(rootCode, userCode, "1072", "");
                context.Response.Write(result);
                //  UserRoleListEntity list = JsonHelper.JsonToObject<UserRoleListEntity>(result);
                //string s=  JsonHelper.ObjectToJSON<UserRoleListEntity>(list);

            }
            catch (Exception ex)
            {
                logentity.otype = "1";
                logentity.logcontent = ex.Message;
                operatelog.Add(logentity);
                context.Response.Write(JsonHelper.ToJsonResult("1", "failure"));
            }
        }
        private void GetClassListInfo( HttpContext context )
        {
            try
            {
                List<string> param = new List<string>() { "classCode"};

                if( !CheckParameters( param ) )
                {
                    return;
                }

                string classCode = context.Request.Form["classCode"].ToString();

                EastEliteICMSWS.EastEliteICMSWSSoapClient client = new EastEliteICMSWS.EastEliteICMSWSSoapClient();
                string result = client.GetECCClassInfoItem(  classCode );
                context.Response.Write( result );
                //  UserRoleListEntity list = JsonHelper.JsonToObject<UserRoleListEntity>(result);
                //string s=  JsonHelper.ObjectToJSON<UserRoleListEntity>(list);

            }
            catch( Exception ex )
            {
                logentity.otype = "1";
                logentity.logcontent = ex.Message;
                operatelog.Add( logentity );
                context.Response.Write( JsonHelper.ToJsonResult( "1", "failure" ) );
            }
        }
        public class UserRoleListEntity
        {
            public UserClassInfoEntity[] UserList;
        }
        private void GetECCUserRoleInfoList(HttpContext context)
        {
            try
            {
                List<string> param = new List<string>() { "rootCode"};

                if (!CheckParameters(param))
                {
                    return;
                }
            
                string rootCode = context.Request.Form["rootCode"].ToString();
            
                EastEliteICMSWS.EastEliteICMSWSSoapClient client = new EastEliteICMSWS.EastEliteICMSWSSoapClient();
                string result = client.GetECCUserRoleInfoList(rootCode);
                context.Response.Write(result);
              //  UserRoleListEntity list = JsonHelper.JsonToObject<UserRoleListEntity>(result);
              //string s=  JsonHelper.ObjectToJSON<UserRoleListEntity>(list);
               
            }
            catch (Exception ex)
            {
                logentity.otype = "1";
                logentity.logcontent = ex.Message;
                operatelog.Add(logentity);
                context.Response.Write(JsonHelper.ToJsonResult("1", "failure"));
            }
        }

        #region 同步设备
        public class DeviceListEntity
        {
            public DeviceClassInfoEntity[] ClassList;
        }
        private void SynchronizDevice(HttpContext context)
        {
            try
            {
                List<string> param = new List<string>() { "schoolCode", "rootCode", "userCode", "userName", "roleCode", "SynchMode" };

                if (!CheckParameters(param))
                {
                    return;
                }
                string schoolCode = context.Request.Form["schoolCode"].ToString();
                string rootCode = context.Request.Form["rootCode"].ToString();
                string userCode = context.Request.Form["userCode"].ToString();
                string userName = context.Request.Form["userName"].ToString();
                string roleCode = context.Request.Form["roleCode"].ToString();
                string gradeName = context.Request.Form["gradeName"].ToString();
                string SynchMode = context.Request.Form["SynchMode"].ToString();
                EastEliteICMSWS.EastEliteICMSWSSoapClient client = new EastEliteICMSWS.EastEliteICMSWSSoapClient();
                string result = client.GetECCDeviceClassList(rootCode, userCode, roleCode, gradeName);
                DeviceListEntity list = JsonHelper.JsonToObject<DeviceListEntity>(result);
                foreach (DeviceClassInfoEntity entity in list.ClassList)
                {
                    entity.SchoolCode = schoolCode;
                }
                if (list != null && list.ClassList != null && list.ClassList.Length > 0)
                {
                    //操作数据库
                    bllDeviceClassInfo bll = new bllDeviceClassInfo();
                    if (bll.SynchronizDevice(SynchMode, schoolCode, rootCode, gradeName, userCode, userName, rootCode, list.ClassList) == 0)
                    {
                        context.Response.Write(JsonHelper.ToJsonResult("0", "success"));
                    }
                    else
                    {
                        context.Response.Write(JsonHelper.ToJsonResult("1", "failure"));
                    }
                }
                else
                {
                    context.Response.Write(JsonHelper.ToJsonResult("-1", "failure"));
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

        #endregion

        #region 同步班级
        public class ClassListEntity
        {
            public UserClassInfoEntity[] ClassList;
        }
        private void SynchronizClass(HttpContext context)
        {
            try
            {
                List<string> param = new List<string>() { "schoolCode", "rootCode", "userCode", "userName", "roleCode", "SynchMode" };

                if (!CheckParameters(param))
                {
                    return;
                }
                string schoolCode =context.Request.Form["schoolCode"].ToString();
                string rootCode =  context.Request.Form["rootCode"].ToString();
                string userCode =context.Request.Form["userCode"].ToString();
                string userName = context.Request.Form["userName"].ToString();
                string roleCode =context.Request.Form["roleCode"].ToString();
                string gradeName = context.Request.Form["gradeName"].ToString();
                string SynchMode = context.Request.Form["SynchMode"].ToString();
                EastEliteICMSWS.EastEliteICMSWSSoapClient client = new EastEliteICMSWS.EastEliteICMSWSSoapClient();
                ( client.Endpoint.Binding as BasicHttpBinding ).MaxReceivedMessageSize = int.MaxValue;
                ( client.Endpoint.Binding as BasicHttpBinding ).MaxBufferSize = int.MaxValue;
                string result = client.GetECCUserClassList(rootCode, userCode, roleCode, gradeName);

                ClassListEntity list = JsonHelper.JsonToObject<ClassListEntity>(result);
                if (list != null && list.ClassList != null && list.ClassList.Length > 0)
                {
                    
                    //操作数据库
                    bllUserClassInfo bll = new bllUserClassInfo();
                    if (bll.SynchronizClass(SynchMode,schoolCode, rootCode, gradeName, userCode, userName, roleCode, list.ClassList) == 0)
                    {
                        context.Response.Write(JsonHelper.ToJsonResult("0", "success"));
                    }
                    else
                    {
                        context.Response.Write(JsonHelper.ToJsonResult("1", "failure"));
                    }

                }
                else
                {
                    context.Response.Write(JsonHelper.ToJsonResult("-1", "failure"));
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

        #endregion

        #region 同步字典
        public class DictListEntity
        {
            public DictEntity[] DictList;
        }
        private void SynchronizDict(HttpContext context)
        {
            try
            {
                List<string> param = new List<string>() { "rootCode", "itemName" };

                if (!CheckParameters(param))
                {
                    return;
                }
                string rootCode = context.Request.Form["rootCode"].ToString();
                string itemName = context.Request.Form["itemName"].ToString();

                EastEliteICMSWS.EastEliteICMSWSSoapClient client = new EastEliteICMSWS.EastEliteICMSWSSoapClient();
                string result = client.GetDictList(rootCode, itemName);

                DictListEntity list = JsonHelper.JsonToObject<DictListEntity>(result);
                if (list != null && list.DictList != null && list.DictList.Length > 0)
                {
                    //操作数据库
                    bllDict bll = new bllDict();
                    if (bll.SynchronizDict(rootCode, itemName, list.DictList) == 0)
                    {
                        context.Response.Write(JsonHelper.ToJsonResult("0", "success"));
                    }
                    else
                    {
                        context.Response.Write(JsonHelper.ToJsonResult("1", "failure"));
                    }
                }
                else
                {
                    context.Response.Write(JsonHelper.ToJsonResult("-1", "failure"));
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

        #endregion
        /// <summary>
        /// 登陆验证
        /// </summary>
        /// <param name="context"></param>
        private void CheckUserLoginInfoItem(HttpContext context)
        {
            try
            {
                List<string> param = new List<string>() { "userCode", "userType", "password" };

                if (!CheckParameters(param))
                {
                    return;
                }
                string userCode = context.Request.Form["userCode"].ToString();
                byte userType = Convert.ToByte(context.Request.Form["userType"]);
                string password = context.Request.Form["password"].ToString();
                EastEliteICMSWS.EastEliteICMSWSSoapClient client = new EastEliteICMSWS.EastEliteICMSWSSoapClient();
                string result = client.CheckUserLoginDeviceItem(userCode, userType, password);
                if (result.IndexOf("FAIL") > 0)
                {
                    logentity.otype = "0";
                    logentity.logcontent = result;
                  
                }
                context.Response.Write(result);
            }
            catch (Exception ex)
            {
                logentity.otype = "1";
                logentity.logcontent = ex.Message;
                operatelog.Add(logentity);
                context.Response.Write(JsonHelper.ToJsonResult("1", "failure"));
            }
        }

        /// <summary>
        /// 外部登陆
        /// </summary>
        /// <param name="context"></param>
        private void CheckUserExternalLoginInfoItem(HttpContext context)
        {
            try
            {
                List<string> param = new List<string>() { "userCode", "userType", "ts", "userToken" };

                if (!CheckParameters(param))
                {
                    return;
                }

                string userCode = context.Request.Form["userCode"].ToString();
                byte userType = Convert.ToByte(context.Request.Form["userType"]);
                string ts = context.Request.Form["ts"].ToString();
                string userToken = context.Request.Form["userToken"].ToString();



                EastEliteICMSWS.EastEliteICMSWSSoapClient client = new EastEliteICMSWS.EastEliteICMSWSSoapClient();
                string result = client.CheckUserLoginTokenItem(userCode, userType, ts, userToken);
                if (result.IndexOf("FAIL") > 0)
                {
                    logentity.otype = "0";
                    logentity.logcontent = result;
                
                }
                context.Response.Write(result);
            }
            catch (Exception ex)
            {
                logentity.otype = "1";
                logentity.logcontent = ex.Message;
                operatelog.Add(logentity);
                context.Response.Write(JsonHelper.ToJsonResult("1", "failure"));
            }
        }
    }
}