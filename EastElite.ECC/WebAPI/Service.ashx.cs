using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EDUC.Common.Bll;
using EDUC.Common.Model;
using PublicLib;

namespace EastElite.ECC
{
    /// <summary>
    /// Service 的摘要说明
    /// </summary>
    public class Service : IHttpHandler
    {

        public void ProcessRequest( HttpContext context )
        {
            context.Response.ContentType = "text/plain";
            operatelogEntity logentity = new operatelogEntity();//日志对象


            logentity.pageurl = GetQueryUrl( context );
            blloperatelog operatelog = new blloperatelog();

            // 判断必须有请求的参数
            if( context.Request.QueryString.Count > 0 && context.Request.QueryString[0].Length >= 2 )
            {
                try
                {
                    // 获取前两个字母来判断
                    switch( context.Request.QueryString[0].ToString().Substring( 0, 2 ) )
                    {

                        // 电子班牌推送模块
                        case "01":
                            //logentity.module = "请求数据:电子班牌推送模块";
                            //operatelog.Add(logentity);
                            PushToDeviceAPI electPlate = new PushToDeviceAPI();
                            electPlate._DoProcess( context );
                            break;
                        // 字典模块
                        case "02":
                            //logentity.module = "请求数据:字典模块";
                            //operatelog.Add(logentity);
                            DictAPI dict = new DictAPI();
                            dict._DoProcess( context );
                            break;
                        // EastEliteICMSWS模块
                        case "03":
                            // logentity.module = "请求数据:数字校园模块";
                            //operatelog.Add(logentity);
                            EastEliteICMSWSAPI eastEliteICMSWS = new EastEliteICMSWSAPI();
                            eastEliteICMSWS._DoProcess( context );
                            break;
                        // EastEliteICMSWS模块
                        case "04":
                            // logentity.module = "请求数据:操作日志模块";
                            //operatelog.Add(logentity);
                            OperatelogAPI operatelogapi = new OperatelogAPI();
                            operatelogapi._DoProcess( context );
                            break;
                        case "05":
                            // logentity.module = "请求数据:考试模块";
                            //operatelog.Add(logentity);
                            ExamtionAPI examtionapi = new ExamtionAPI();
                            examtionapi._DoProcess( context );
                            break;
                        case "06":
                            // logentity.module = "请求数据:启动程序模块";
                            //operatelog.Add(logentity);
                            PushToStartupProgramAPI startupProgramAPI = new PushToStartupProgramAPI();
                            startupProgramAPI._DoProcess( context );
                            break;
                        default:
                            logentity.otype = "1";
                            logentity.logcontent = "没有找到提供的该服务";
                            operatelog.Add( logentity );
                            context.Response.Write( "没有找到提供的该服务" );
                            break;
                    }
                }
                catch( System.Exception ex )
                {
                    logentity.otype = "1";
                    logentity.logcontent = ex.Message;
                    operatelog.Add( logentity );
                    context.Response.Write( ex.Message );
                }
            }
        }
        public string GetQueryUrl( HttpContext context )
        {

            string strparameters = "";
            for( int i = 0; i < context.Request.Params.Count; i++ )
            {
                if( context.Request.Params.Keys[i] != null && ( context.Request.Params.Keys[i].ToString().ToUpper() == "ALL_HTTP" || context.Request.Params.Keys[i].ToString().ToUpper() == "LOGINUSER" ) )
                {
                    break;
                }
                else if( context.Request.Params.Keys[i] != null && context.Request.Params.Keys[i].ToString() != "actionname" )
                {
                    strparameters += "&" + context.Request.Params.Keys[i].ToString();
                    strparameters += "=" + context.Request.Params[i].ToString();
                }
            }

            return context.Request.Url.ToString() + strparameters;
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}