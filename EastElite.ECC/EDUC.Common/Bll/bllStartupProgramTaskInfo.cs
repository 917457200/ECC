using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EDUC.Common.Bll;
using EDUC.Common.Model;
using PublicLib;

namespace EDUC.Common.Dal
{
	/// <summary>
    /// 业务类
    /// </summary>
    public class bllStartupProgramTaskInfo : bllBase
    {
		dalStartupProgramTaskInfo dal = new dalStartupProgramTaskInfo();
        StartupProgramTaskInfoEntity Entity = new StartupProgramTaskInfoEntity();

        public int Add(ref StartupProgramTaskInfoEntity Entity, operatelogEntity entity, string rootCode, out int errorcode, out string errormsg)
        {
            errorcode = 0;
            errormsg = "";
            int result = dal.Add(ref Entity, rootCode, out errorcode, out errormsg);
            DataRow dr = dtBase.NewRow();
            if (result != 0)
            {
                errorcode = -1;
                errormsg = "操作数据库失败";
            }

            // blllog.Add(entity.module, entity.pageurl, entity.otype, entity.logcontent, entity.cuser.ToString());
            return result;
        }
	
        /// <summary>
        /// 获取单行数据
        /// </summary>
        /// <param name="filter">指定条件</param>
        /// <returns>返回第一行</returns>
        public DataTable GetPagingSigInfo(string filter)
        {
            int recnums = 0;
            int pagenums = 0;
            return GetPagingListInfo(1, 1, filter, string.Empty, out recnums, out pagenums);
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
        public DataTable GetPagingListInfo(int pageSize, int currentpage, string filter, string order, out int recnums, out int pagenums)
        {
            recnums = -1;
            pagenums = -1;
            return new bllPaging().GetPagingInfo("StartupProgramTaskInfo", "ID", @" [ID],
          [Code] ,
          [OperateTypeID] ,
          [MessageSourceID] ,
          [TargetRange] ,
          [Tag] ,
          [Tag_and] ,
          [Alias] ,
          [Registration_ID] ,
          [RargetAlias] ,
          [MessageContent] ,
          [CreatedID] ,
          [CreatedName] ,
          CONVERT(varchar(16), [CreatedDate], 20)  [CreatedDate],
          [Note],(SELECT TOP 1 [ItemValue] FROM dict WHERE [ItemName]='StartupProgramOperateTypeID' AND [ItemKey]=OperateTypeID) OperateTypeIDName,(SELECT TOP 1 [ItemValue] FROM dict WHERE [ItemName]='MessageSourceID' AND [ItemKey]=[MessageSourceID]) MessageSourceIDName", pageSize, currentpage, filter, "", order, out recnums, out pagenums);
        }

        public DataTable InitSP(string deviceSN,string SPJPushID, string version, operatelogEntity entity)
        {
            string sql = string.Format("update [DeviceClassInfo] set spjpushid='{0}' where isvalid=1 and devicesn='{1}'", SPJPushID, deviceSN);

            int result = Helper.StringToInt(new bllPaging().ExecuteScalarBySQL(sql));
            DataRow dr = dtBase.NewRow();
            if (result == 0)
            {
                dr["type"] ="0";
                dr["mes"] = "success";
            }
            else
            {
                dr["type"] = -1;
                dr["mes"] = "操作数据库失败";
            }

            dtBase.Rows.Add(dr);
            dtBase.AcceptChanges();
            //  blllog.Add(entity.module, entity.pageurl, entity.otype, entity.logcontent, entity.cuser.ToString());
            return dtBase;
        }
        public int UpdateSPVersion(string deviceSN, string version, string checkDate)
        {
            string strsql = string.Format("update DeviceClassInfo set InstallerVersion='{0}',InstallerHeartBeatCheckDate='{1}' where DeviceSN='{2}'", version,checkDate, deviceSN);
            return new bllPaging().ExecuteNonQueryBySQL(strsql);
        }

        public int DeleteDeviceTask(string ids)
        {

            int result = dal.DeleteDeviceTask(ids);
            //检测执行结果
            return result;
        }
    }
}