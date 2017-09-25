using System.Collections.Generic;
using System.Data;
using EDUC.Common.Dal;
using EDUC.Common.Model;
using PublicLib;

namespace EDUC.Common.Bll
{
    /// <summary>
    /// 后台用户操作日志业务类
    /// </summary>
    public class blloperatelog
    {
       daloperatelog dal = new daloperatelog();
        operatelogEntity Entity = new operatelogEntity();

        public blloperatelog()
        {
        }

        /// <summary>
        /// 添加一条操作记录
        /// </summary>
        /// <param name="module">操作模块</param>
        /// <param name="pageurl">页面地址</param>
        /// <param name="otype">操作类型</param>
        /// <param name="logcontent">日志信息</param>
        /// <param name="cuser">操作用户</param>
        /// <returns></returns>
        public int Add(string module, string pageurl, string otype, string logcontent, string cuser,string functionname)
        {
            Entity.module = module;
            Entity.pageurl = pageurl;
            Entity.otype = otype;
            Entity.logcontent = logcontent;
            Entity.cuser = Helper.StringToLong(cuser);
            Entity.functionName = functionname;
            return dal.Add(Entity);
        }
        public int Add(operatelogEntity entity)
        {
            return dal.Add(entity);
        }
        /// <summary>
        /// 获取单行数据
        /// </summary>
        /// <param name="filter">指定条件</param>
        /// <returns>返回第一行</returns>
        public DataTable GetPagingSigInfo( string UserType, string filter)
        {

            int recnums = 0;
            int pages = 0;
            DataTable dt = GetPagingListInfo(1, 1, filter, string.Empty, out recnums, out pages);
            return dt;
        }

        /// <summary>
        /// 分页方法
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="currentpage"></param>
        /// <param name="filter"></param>
        /// <param name="order"></param>
        /// <param name="recnums"></param>
        /// <returns></returns>
        public DataTable GetPagingListInfo(int pageSize, int currentpage, string filter, string order, out int recnums, out int pages)
        {
            return new bllPaging().GetPagingInfo("OperateLogInfo", "id", @"[id],[otype] ,
          [module] ,
          [functionName] ,
          [pageurl] ,
          [logcontent] ,
          [cuser] ,
          CONVERT(varchar(16), [ctime], 20)  [CreatedDate],[isValid]", pageSize, currentpage, filter, "", order, out recnums, out pages);
        }

        /// <summary>
        /// 分页方法(日志查询，基于视图)
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="currentpage"></param>
        /// <param name="filter"></param>
        /// <param name="order"></param>
        /// <param name="recnums"></param>
        /// <returns></returns>
        public DataTable GetPagingListInfoView(string GUID, string UID, int pageSize, int currentpage, string filter, string order, out int recnums, out int pages)
        {
            return new bllPaging().GetPagingInfo("operatelog_view", "id", "*", pageSize, currentpage, filter, "", order, out recnums, out pages);
        }
        public int UpdateOperatelogIsValid(int id, int isvalid)
        {
            string sql = string.Format("update [OperateLogInfo] set isvalid={0} where id={1}", isvalid, id);
            return Helper.StringToInt(new bllPaging().ExecuteScalarBySQL(sql));
        }
    }
}