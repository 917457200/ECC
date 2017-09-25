using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EDUC.Common.Model;
using PublicLib;

namespace EDUC.Common.Dal
{
    /// <summary>
    /// 数据访问类
    /// </summary>
    public partial class dalStartupProgramTaskInfo
    {
        MSSqlDataAccess DBHelper = new MSSqlDataAccess();
        int intReturn;
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ref StartupProgramTaskInfoEntity Entity, string rootCode, out int errorcode, out string errormsg)
        {
            errorcode = 0;
            errormsg = "";
            intReturn = 0;

            SqlParameter[] sqlParameters = 
            {
				new SqlParameter("@ID", SqlDbType.BigInt,8),
                new SqlParameter("@Errorcode", SqlDbType.Int),
                new SqlParameter("@Errormsg", SqlDbType.NVarChar,256),
				new SqlParameter("@Code",SqlDbType.NVarChar,50),
				new SqlParameter("@OperateTypeID", Entity.OperateTypeID),
				new SqlParameter("@MessageSourceID", Entity.MessageSourceID),
                new SqlParameter("@TaskStatusID", Entity.TaskStatusID),
                new SqlParameter("@MessageContent", Entity.MessageContent),
				new SqlParameter("@TargetRange", Entity.TargetRange),
				new SqlParameter("@RargetAlias", Entity.RargetAlias),
				new SqlParameter("@rootCode", rootCode),
				new SqlParameter("@CreatedID", Entity.CreatedID),
				new SqlParameter("@CreatedName", Entity.CreatedName),
                new SqlParameter("@Tag",Entity.Tag==null?"":Entity.Tag),
				new SqlParameter("@Tag_and",Entity.Tag_and==null?"":Entity.Tag_and),
                new SqlParameter("@Alias", Entity.Alias==null?"":Entity.Alias),
                new SqlParameter("@Registration_ID",Entity.Registration_ID==null?"":Entity.Registration_ID),
             	new SqlParameter("@Note", Entity.Note),
             };

            sqlParameters[0].Direction = ParameterDirection.Output;
            sqlParameters[1].Direction = ParameterDirection.Output;
            sqlParameters[2].Direction = ParameterDirection.Output;
            sqlParameters[3].Direction = ParameterDirection.Output;
            intReturn = DBHelper.ExecuteNonQuery("dbo.spStartupProgramTaskInfoAdd", CommandType.StoredProcedure, sqlParameters);

            if (intReturn == 0)
            {
                Entity.ID = 0;
                errorcode = Convert.ToInt32(sqlParameters[1].Value);
                errormsg = sqlParameters[2].Value.ToString();
                Entity.Code = sqlParameters[3].Value.ToString();
            }

            return errorcode;
        }

        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="ID">标识</param>
        /// <param name="Status">状态</param>
        /// <returns></returns>
        public int UpdateStatus(string ID, string Status)
        {
            SqlParameter[] sqlParameters = 
            {
				new SqlParameter("@ID", ID),
				new SqlParameter("@status", Status)
             };
            return DBHelper.ExecuteNonQuery("dbo.p_StartupProgramTaskInfo_UpdateStatus", CommandType.StoredProcedure, sqlParameters);
        }

        public int DeleteDeviceTask(string ids)
        {
            SqlParameter[] sqlParameters = 
            { new SqlParameter("@IDs", ids)
             };


            return DBHelper.ExecuteNonQuery("dbo.spStartupProgramTaskInfoDelete", CommandType.StoredProcedure, sqlParameters);
        }
    }
}