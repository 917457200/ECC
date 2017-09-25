using System.Collections.Generic;
using System.Data;
using EDUC.Common.Dal;
using EDUC.Common.Model;
using PublicLib;

namespace EDUC.Common.Bll
{
	/// <summary>
    /// 业务类
    /// </summary>
    public class bllDataFieldInfo : bllBase
    {
        dalDataFieldInfo dal = new dalDataFieldInfo();
        public int Add(ref DataFieldInfoEntity Entity, operatelogEntity entity,out int errorcode, out string errormsg)
        {
            errorcode = 0;
            errormsg = ""; 
            int result = dal.Add(ref Entity,out errorcode,out errormsg);
            DataRow dr = dtBase.NewRow();
            if (result != 0)
            {
                errorcode = -1;
                errormsg = "操作数据库失败";
            }

            //  blllog.Add(entity.module, entity.pageurl, entity.otype, entity.logcontent, entity.cuser.ToString());
            return result;
        }
        public int UploadTerminalScreenshot(string classCode,ref DataFieldInfoEntity Entity, operatelogEntity entity, out int errorcode, out string errormsg)
        {
            errorcode = 0;
            errormsg = "";
            int result = dal.UploadTerminalScreenshot(classCode,ref Entity, out errorcode, out errormsg);
            DataRow dr = dtBase.NewRow();
            if (result != 0)
            {
                errorcode = -1;
                errormsg = "操作数据库失败";
            }

            //  blllog.Add(entity.module, entity.pageurl, entity.otype, entity.logcontent, entity.cuser.ToString());
            return result;
        }
        /// <summary>
        /// 获取单行数据
        /// </summary>
        /// <param name="filter">指定条件</param>
        /// <returns>返回第一行</returns>
        public DataTable GetPagingSigInfo(string GUID, string UID, string filter)
        {
            int recnums = 0;
            int pagenums = 0;
            return GetPagingListInfo(GUID, UID, 1, 1, filter, string.Empty, out recnums, out pagenums);
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
        public DataTable GetPagingListInfo(string GUID, string UID, int pageSize, int currentpage, string filter, string order, out int recnums, out int pagenums)
        {
			if (!CheckLogin(GUID, UID))//非法登录
            {
                recnums = -1;
                pagenums = -1;
                return dtBase;
            }
            return new bllPaging().GetPagingInfo("DataFieldInfo", "ID", "*", pageSize, currentpage, filter, "", order, out recnums, out pagenums);
        }
    }
}