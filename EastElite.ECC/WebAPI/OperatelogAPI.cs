using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using EDUC.Common.Bll;
using PublicLib;

namespace EastElite.ECC
{
    public class OperatelogAPI : ServiceBase
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

                    logentity.module = "日志信息模块";//模块名称
                    logentity.pageurl = GetQueryUrl();
                    switch (context.Request.QueryString[0])
                    {
                        //获取日志信息
                        case "04-01":
                            logentity.functionName = "获取日志信息";
                            //operatelog.Add(logentity);
                            GetOperatelog(context);
                            break;
                        //修改日志状态
                        case "04-02":
                            logentity.functionName = "获取日志信息";
                            //operatelog.Add(logentity);
                            UpdateOperatelogIsValid(context);
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
        private void UpdateOperatelogIsValid(HttpContext context)
        {
            try
            {
                List<string> param = new List<string>() { "id", "isvalid" };

                if (!CheckParameters(param))
                {
                    return;
                }
                blloperatelog bll = new blloperatelog();

                int id = Helper.StringToInt(context.Request.Form["id"].ToString());
                int isvalid = Helper.StringToInt(context.Request.Form["isvalid"].ToString());
                if (bll.UpdateOperatelogIsValid(id, isvalid) == 0)
                {
                    context.Response.Write(JsonHelper.ToJsonResult("0", "success"));
                }
                else
                {
                    context.Response.Write(JsonHelper.ToJsonResult("1", "failure"));
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

        /// <summary>
        /// 获取所有字典
        /// </summary>
        /// <param name="context"></param>
        private void GetOperatelog(HttpContext context)
        {
            try
            {
                List<string> param = new List<string>() { "pageSize", "currentPage","isvalid" };

                if (!CheckParameters(param))
                {
                    return;
                }
                blloperatelog bll = new blloperatelog();

                int pageSize = Helper.StringToInt(context.Request.Form["pageSize"].ToString());
                int currentPage = Helper.StringToInt(context.Request.Form["currentPage"].ToString());
                string filter = "1=1";
                if (context.Request.Form["stime"] != null && context.Request.Form["stime"].ToString() != "")
                {
                    filter += string.Format(" and ctime>='{0}'", context.Request.Form["stime"].ToString());
                }
                if (context.Request.Form["etime"] != null && context.Request.Form["etime"].ToString() != "")
                {
                    filter += string.Format(" and ctime<='{0}'", context.Request.Form["etime"].ToString());
                }
                if (context.Request.Form["isvalid"] != null)
                {
                    filter += string.Format(" and isvalid={0}", Helper.StringToInt(context.Request.Form["isvalid"].ToString()));
                }
                string order = "ctime desc";
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
    }
}