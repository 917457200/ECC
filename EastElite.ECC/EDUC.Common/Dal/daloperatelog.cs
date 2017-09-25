using System.Data;
using System.Data.SqlClient;
using EDUC.Common.Model;
using PublicLib;

namespace EDUC.Common.Dal
{
    /// <summary>
    /// 后台用户操作日志数据访问类
    /// </summary>
    public partial class daloperatelog 
    {
        MSSqlDataAccess DBHelper = new MSSqlDataAccess();
		int intReturn;
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(operatelogEntity Entity)
        {
            intReturn = 0;
            SqlParameter[] sqlParameters = 
            {
				new SqlParameter("@module", Entity.module),
				new SqlParameter("@pageurl", Entity.pageurl),
				new SqlParameter("@otype", Entity.otype),
				new SqlParameter("@logcontent", Entity.logcontent),
				new SqlParameter("@cuser", Entity.cuser),
                new SqlParameter("@functionName", Entity.functionName)
             };
            intReturn = DBHelper.ExecuteNonQuery("dbo.spOperatelogInfoAdd", CommandType.StoredProcedure, sqlParameters);
            return intReturn;
        }
    }
}