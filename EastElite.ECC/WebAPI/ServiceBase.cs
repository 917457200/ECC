using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Script.Serialization;

using System.IO;
using EDUC.Common.Model;
using PublicLib;
using EDUC.Common.Bll;

namespace EastElite.ECC
{
    public class ServiceBase : IHttpHandler
    {
        public HttpContext Pagcontext = null;
        public string actionname = string.Empty;
        public operatelogEntity logentity = new operatelogEntity();//日志对象
        public blloperatelog operatelog = new blloperatelog();
        public virtual void ProcessRequest(HttpContext context)
        {

        }
        public bool CheckLongin(HttpContext context)
        {
            Pagcontext = context;
            return true;
        }
        /// <summary>
        /// 检测接口必要参数
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected bool CheckParameters()
        {

            bool Flag = true;
            string mes = string.Empty;
            actionname = Pagcontext.Request["actionname"];
            if (actionname == null)
            {
                mes += "actionname,";
                Flag = false;
            }
            string parameters = Pagcontext.Request["parameters"];
            if (parameters == null)
            {
                mes += "parameters,";
                Flag = false;
            }
            if (!Flag)
            {
                Pagcontext.Response.Write("{\"status\":\"2\",\"mes\":\"缺少" + mes.TrimEnd(',') + "参数\"}");
            }
            return Flag;
        }

        protected string GetQueryUrl()
        {
        

            string strparameters = "";
            for (int i = 0; i < Pagcontext.Request.Form.Keys.Count; i++)
            {
                strparameters += "&" + Pagcontext.Request.Form.Keys[i].ToString();
                strparameters += "=" + Pagcontext.Request.Form[Pagcontext.Request.Form.Keys[i].ToString()].ToString();
              
            }

            return Pagcontext.Request.Url.ToString() + strparameters;
        }
         
        /// <summary>
        /// 必填参数检查
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        protected bool CheckParameters(List<string> parameters)
        {
            foreach (string param in parameters)
            {
                if (Pagcontext.Request.Form[param] == null)
                {
                    logentity.otype = "0";
                    logentity.logcontent = "缺少参数:" + param;
                    operatelog.Add(logentity);
                    Pagcontext.Response.Write("{\"status\":\"-2\",\"mes\":\"缺少参数:" + param + "\"}");
                    return false;
                }
                else if (Pagcontext.Request.Form[param] != null&&Pagcontext.Request.Form[param].ToString().Trim() == "")
                {
                    logentity.otype = "0";
                    logentity.logcontent = "参数值错误:" + param;
                    operatelog.Add(logentity);
                    Pagcontext.Response.Write("{\"status\":\"-2\",\"mes\":\"参数:" + param + "不能为空字符串\"}");

                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// 必填参数检查(可以为"")
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        protected bool CheckEmptyParameters(List<string> parameters)
        {
            foreach (string param in parameters)
            {
                if (Pagcontext.Request.Form[param] == null)
                {
                    logentity.otype = "0";
                    logentity.logcontent = "缺少参数:" + param;
                    operatelog.Add(logentity);
                    Pagcontext.Response.Write("{\"status\":\"-2\",\"mes\":\"缺少参数:" + param + "\"}");

                    return false;
                }

            }
            return true;
        }
        /// <summary>
        /// 可选参数检查("0"不能作为参数值进行检查)
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        protected string CheckOptionalParameters(Dictionary<string, EnumSearchType> parameters, string filter = " WHERE 1=1 ")
        {

            if (parameters != null && parameters.Count > 0)
            {

                foreach (KeyValuePair<string, EnumSearchType> param in parameters)
                {
                    if (Pagcontext.Request.Form[param.Key] != null && Pagcontext.Request.Form[param.Key].ToString() != "" && Pagcontext.Request.Form[param.Key].ToString() != "0")
                    {
                        switch (param.Value)
                        {
                            case EnumSearchType.AndInt:
                                filter += " AND " + param.Key + "=" + Pagcontext.Request.Form[param.Key].ToString();
                                break;
                            case EnumSearchType.AndNotInt:
                                filter += " AND " + param.Key + "<>" + Pagcontext.Request.Form[param.Key].ToString();
                                break;
                            case EnumSearchType.AndString:
                                filter += " AND " + param.Key + "='" + Pagcontext.Request.Form[param.Key].ToString() + "'";
                                break;
                            case EnumSearchType.AndLike:
                                filter += " AND " + param.Key + " LIKE '%" + Pagcontext.Request.Form[param.Key].ToString() + "%'";
                                break;
                            case EnumSearchType.AndLikeS:
                                filter += " AND " + param.Key + " LIKE '" + Pagcontext.Request.Form[param.Key].ToString() + "%'";
                                break;
                            case EnumSearchType.AndLikeE:
                                filter += " AND " + param.Key + " LIKE '%" + Pagcontext.Request.Form[param.Key].ToString() + "'";
                                break;
                            case EnumSearchType.AndSTime:
                                filter += " AND " + "CreatedDate" + " >= '" + Pagcontext.Request.Form[param.Key].ToString() + "'";
                                break;
                            case EnumSearchType.AndETime:
                                filter += " AND " + "CreatedDate" + " <= '" + Pagcontext.Request.Form[param.Key].ToString() + "'";
                                break;
                        }
                    }
                }
            }
            return filter;
        }

        protected int CheckPageSize()
        {
            if (Pagcontext != null)
                return Pagcontext.Request.Form["pagesize"] != null ? Helper.StringToInt(Pagcontext.Request.Form["pagesize"].ToString()) : Helper.StringToInt(Helper.GetAppSettings("PageSize"));
            else
            {
                return Helper.StringToInt(Helper.GetAppSettings("PageSize"));
            }

        }
        protected int CheckCurrentPage()
        {
            if (Pagcontext != null)
                return Pagcontext.Request.Form["currentpage"] != null ? Helper.StringToInt(Pagcontext.Request.Form["currentpage"].ToString()) : Helper.StringToInt(Helper.GetAppSettings("CurrentPage"));
            else
            {
                return Helper.StringToInt(Helper.GetAppSettings("CurrentPage"));
            }

        }
        /// <summary>
        /// 获取jason参数信息
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected Dictionary<string, object> GetParameters()
        {
            Dictionary<string, object> dicPar = new Dictionary<string, object>();
            string parameters = Pagcontext.Request["parameters"];
            if (parameters.Length > 0)
            {
                try
                {
                    string decparameters = parameters;
                    //string decparameters = OEncryp.Decrypt(parameters);
                    JavaScriptSerializer jss = new JavaScriptSerializer();
                    object obj = jss.DeserializeObject(decparameters);
                    dicPar = (Dictionary<string, object>)obj;
                }
                catch (Exception ex)
                {

                    Pagcontext.Response.Write("{\"status\":\"2\",\"mes\":\"参数解析错误\"}");
                    return null;
                }
            }
            return dicPar;
        }

        /// <summary>
        /// 检测调用参数是否合法
        /// </summary>
        /// <param name="dicPar"></param>
        /// <param name="liPra"></param>
        /// <returns></returns>
        protected bool CheckActionParameters(Dictionary<string, object> dicPar, List<string> liPra)
        {
            string mes = string.Empty;
            bool Flag = true;
            if (liPra == null)
            {
                Pagcontext.Response.Write("{\"status\":\"2\",\"mes\":\"List参数不合法\"}");
                return false;
            }
            if (dicPar == null)
            {
                Pagcontext.Response.Write("{\"status\":\"2\",\"mes\":\"parameters参数解析错误\"}");
                return false;
            }
            foreach (string str in liPra)
            {
                if (!dicPar.ContainsKey(str))
                {
                    mes += str + ",";
                    Flag = false;
                }
            }
            if (!Flag)
            {
                Pagcontext.Response.Write("{\"status\":\"2\",\"mes\":\"缺少" + mes.TrimEnd(',') + "参数\"}");
            }

            if (Flag)
            {
                //获取操作人Id
                if (dicPar.ContainsKey("U_ID") && !string.IsNullOrEmpty(dicPar["U_ID"].ToString()))
                {
                    logentity.cuser = Helper.StringToInt(dicPar["U_ID"].ToString());
                }
                //获取操作人Url
                if (dicPar.ContainsKey("pageurl") && !string.IsNullOrEmpty(dicPar["pageurl"].ToString()))
                {
                    logentity.pageurl = dicPar["pageurl"].ToString();
                }
            }
            return Flag;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// 返回数据（不做任何处理）
        /// </summary>
        /// <param name="dt"></param>
        protected void ReturnListJson(string type)
        {
            Pagcontext.Response.Write(type);
        }

        /// <summary>
        /// 返回执行json
        /// </summary>
        /// <param name="dt"></param>
        protected void ReturnJson(DataTable dt)
        {
            string type;
            string mes;
            string json;
            Helper.GetDataTableToResult(dt, out type, out mes);
            json = JsonHelper.ToJson(type, mes);
            Pagcontext.Response.Write(json);
        }

        /// <summary>
        /// 返回单条json
        /// </summary>
        /// <param name="dt"></param>
        protected void ReturnListJson(DataTable dt)
        {
          
                 ReturnListJson(dt, null, null, null, null);
           
        }

        /// <summary>
        /// 返回单条json
        /// </summary>
        /// <param name="dt"></param>
        protected void ReturnListJson(string type, string mes)
        {
            Pagcontext.Response.Write(JsonHelper.ToJson(type, mes));
        }

        /// <summary>
        /// 返回列表json
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="pageSize"></param>
        /// <param name="recordCount"></param>
        /// <param name="currentPage"></param>
        /// <param name="totalPage"></param>
        protected void ReturnListJson(DataTable dt, int? pageSize, long? recordCount, int? currentPage, int? totalPage)
        {
            string type;
            string mes;
            Helper.GetDataTableToResult(dt, out type, out mes);
            if (type != "0")
            {
                Pagcontext.Response.Write(JsonHelper.ToJson(type, mes));
            }
            else
            {
                Pagcontext.Response.Write(JsonHelper.ToJson(type, mes, new ArrayList() { dt }, new string[] { "data" }, pageSize, recordCount, currentPage, totalPage));
            }
        }

        /// <summary>
        /// 返回列表json
        /// </summary>
        /// <param name="arrayListTable">table集合</param>
        /// <param name="tableName">table表名集合</param>
        /// <param name="pageSize"></param>
        /// <param name="recordCount"></param>
        /// <param name="currentPage"></param>
        /// <param name="totalPage"></param>
        protected void ReturnListJson(ArrayList arrayListTable, string[] tableName, int? pageSize, long? recordCount, int? currentPage, int? totalPage)
        {
            string type;
            string mes;
            Helper.GetDataTableToResult(arrayListTable[0] as DataTable, out type, out mes);
            if (type != "0")
            {
                Pagcontext.Response.Write(JsonHelper.ToJson(type, mes));
            }
            else
            {
                Pagcontext.Response.Write(JsonHelper.ToJson(type, mes, arrayListTable, tableName, pageSize, recordCount, currentPage, totalPage));
            }
        }

        /// <summary>
        /// 判断条件拼接
        /// </summary>
        /// <param name="filterNames">需要查询的字段</param>
        /// <param name="dicPar">获取参数的字段</param>
        /// <param name="type">查询类型 like查询：typeof(string)  等号查询：typeof(int)</param>
        /// <param name="filter">查询条件可以不写，默认：WHERE 1=1</param>
        /// <returns></returns>
        public string CombinationFilter(List<string> filterNames, Dictionary<string, object> dicPar, Type type, string filter = " WHERE 1=1 ", serachType stype = serachType.equal)
        {
            string fieldStr = string.Empty;

            #region  字符串拼接
            switch (stype)
            {
                case serachType.OR:
                    //使用like查询
                    if (type == typeof(string))
                    {
                        //string串类型 使用like查询
                        fieldStr = " OR {0} LIKE '%{1}%' ";
                    }
                    else if (type == typeof(int))
                    {
                        //int 类型 = 查询
                        fieldStr = " OR {0} = '{1}' ";
                    }
                    break;
                case serachType.equal:
                    //OR 查询
                    if (type == typeof(string))
                    {
                        //string串类型 使用like查询
                        fieldStr = " AND {0} LIKE '%{1}%' ";
                    }
                    else if (type == typeof(int))
                    {
                        //int 类型 = 查询
                        fieldStr = " AND {0} = '{1}' ";
                    }
                    break;
                default:
                    break;
            }
            #endregion

            //条件拼接
            foreach (string field in filterNames)
            {
                string fieldLower = field.ToLower();//需要查询的字段 转为小写
                string filterName = string.Empty;//数据库字段名称
                string dicName = string.Empty;//别名字段

                if (fieldLower.Contains(" as "))
                {
                    //字符串中出现 id AS b 情况 或者 t.id AS b
                    filterName = fieldLower.Substring(0, fieldLower.IndexOf(" as ")).Trim();
                    dicName = fieldLower.Substring(fieldLower.IndexOf(" as ") + 3).Trim();
                }
                else if (fieldLower.Contains("."))
                {
                    //字符串中出现 t.b 情况
                    dicName = fieldLower.Split('.')[1].Trim();
                }

                if (string.IsNullOrEmpty(dicName))
                {
                    dicName = fieldLower;
                }

                if (string.IsNullOrEmpty(filterName))
                {
                    filterName = fieldLower;
                }

                if (dicPar.ContainsKey(dicName) && !string.IsNullOrWhiteSpace(dicPar[dicName].ToString()))
                {
                    filter += string.Format(fieldStr, filterName, dicPar[dicName].ToString());
                }
            }

            //OR 拼接的时候，去掉第一个OR
            if (serachType.OR == stype && !string.IsNullOrEmpty(filter))
            {
                filter = filter.Substring(3);
            }

            return filter;
        }

        /// <summary>
        /// 导出Excel文件
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <param name="colname">列名:汉子 用英文逗号拼接</param>
        /// <param name="cols">列名:对应table的列名对应的可以是数据库表名 用英文逗号拼接</param>
        /// <param name="dt">表格数据</param>
        public string ExportExcel(string fileName, string columnsName, string columns, DataTable dt)
        {
            string[] colname = columnsName.Split(',');
            string[] cols = columns.Split(',');

            if (colname.Length != cols.Length || colname.Length <= 0)
            {
                return "columnsName与columns数量不等";
            }

            if (dt == null || dt.Rows.Count <= 0)
            {
                return "表格没有数据";
            }

            string floder = "/excel/";
            string url = HttpContext.Current.Request.MapPath(floder);
            //判断文件夹是否存在
            if (!Directory.Exists(url))
            {
                //若不存在则创建
                Directory.CreateDirectory(url);
            }
            ExcelsHelp.OutFileToDisk(dt, colname, cols, url + fileName + ".xls");
            return floder + fileName + ".xls";
        }

    }

    /// <summary>
    /// 查询类型
    /// </summary>
    public enum serachType
    {
        OR,
        equal
    }

}