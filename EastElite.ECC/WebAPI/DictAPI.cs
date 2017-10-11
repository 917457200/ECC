using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using EDUC.Common.Bll;
using PublicLib;
using System.Text;
using System.Web.Security;

namespace EastElite.ECC
{
    public class DictAPI : ServiceBase
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

                    logentity.module = "字典模块";//模块名称
                    logentity.pageurl = GetQueryUrl();
                    switch( context.Request.QueryString[0] )
                    {
                        //获取字典信息
                        case "02-01":
                            logentity.functionName = "获取字典信息";
                            //operatelog.Add(logentity);
                            GetDict( context );
                            break;
                        /// 获取所有模版字典
                        case "02-02":
                            logentity.functionName = "获取模版字典信息";
                            //operatelog.Add(logentity);
                            GetDictTemplate( context );
                            break;
                        /// 获取模版下模块字典
                        case "02-03":
                            logentity.functionName = "获取模版下模块字典";
                            //operatelog.Add(logentity);
                            GetDictTemplateK( context );
                            break;
                        default:
                            logentity.otype = "1";
                            logentity.logcontent = "没有找到提供的该方法"; ;
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


        /// <summary>
        /// 获取所有字典
        /// </summary>
        /// <param name="context"></param>
        private void GetDict( HttpContext context )
        {
            try
            {
                List<string> param = new List<string>() { "itemName" };

                if( !CheckParameters( param ) )
                {
                    return;
                }
                bllDict bll = new bllDict();

                int pageSize = Helper.StringToInt( Helper.GetAppSettings( "PageSize" ) );
                int currentPage = 1;
                string itemName = context.Request.Form["itemName"].ToString();
                string filter = "";
                if( !string.IsNullOrWhiteSpace( itemName ) )
                {
                    filter = "where [IsValid]=1 and ItemName='" + itemName + "'";
                }
                if( context.Request.Form["filter"] != null )
                {
                    filter += " and (" + context.Request.Form["filter"].ToString() + ")";
                }

                string order = "";
                int recordCount = 0;
                int totalPage = 0;
                dt = bll.GetPagingListInfo( pageSize, currentPage, filter, order, out recordCount, out totalPage );
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
        /// 获取所有模版字典
        /// </summary>
        /// <param name="context"></param>
        private void GetDictTemplate( HttpContext context )
        {
            try
            {
                bllDict bll = new bllDict();
                string userCode = context.Request.Form["userCode"] != null ? context.Request.Form["userCode"].ToString() : "";
                EastEliteICMSWS.EastEliteICMSWSSoapClient client = new EastEliteICMSWS.EastEliteICMSWSSoapClient();

                //计算ts 是从1970年1月1日（UTC/GMT的午夜）开始所经过的秒数
                DateTime TimeNow = DateTime.Now;
                DateTime TimeOld = Convert.ToDateTime( "1970-1-1 00:00:00" );
                TimeSpan TimeX = TimeNow - TimeOld;//计算时间差
                int ts = (int) TimeX.TotalSeconds;
                //计算Md 对于字符串UsCd+ts+secretKey计算MD5后，得到的字符，secretKey是我们协商加密字符串，约定为 furture#$sdf&
                string Md = FormsAuthentication.HashPasswordForStoringInConfigFile( userCode + ts + "furture#$eccs&", "MD5" );
                ///登录失败

                string userJson = client.CheckUserLoginTokenItem( userCode, 1, ts.ToString(), Md );

                TeUser U = new TeUser();
                if( userJson.IndexOf( "SUCC" ) > -1 )
                {
                    U = GetUserNameForSerVice( userJson );
                }
                string TemplateJson = bll.GetDictTemplate( U.roleCode );
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
        /// <summary>
        /// 获取模版下模块字典
        /// </summary>
        /// <param name="context"></param>
        private void GetDictTemplateK( HttpContext context )
        {
            try
            {
                bllDict bll = new bllDict();
                string Note = context.Request.Form["Note"].ToString();
                string TemplateJson = bll.GetDictTemplateK( Note );
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

        /// <summary>
        // 获取用户名
        /// </summary>
        /// <returns></returns>
        public TeUser GetUserNameForSerVice( string Josn )
        {
            Newtonsoft.Json.Linq.JObject LoginUser = (Newtonsoft.Json.Linq.JObject) Newtonsoft.Json.JsonConvert.DeserializeObject<object>( Josn );
            TeUser user = new TeUser();
            if( LoginUser != null )
            {
                string U = LoginUser["data"]["result"].ToString().Replace( "[", "" ).Replace( "]", "" );
                user = Newtonsoft.Json.JsonConvert.DeserializeObject<TeUser>( U );
            }
            return user;
        }

        [Serializable]
        public partial class TeUser
        {
            public string userCode { get; set; }
            public string userType { get; set; }
            public string userName { get; set; }
            public string unitCode { get; set; }
            public string unitName { get; set; }
            public string rootCode { get; set; }
            public string rootName { get; set; }
            public string rootType { get; set; }
            public string roleCode { get; set; }

        }
    }
}