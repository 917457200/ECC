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
    public partial class dalExaminationInfo
    {
        MSSqlDataAccess DBHelper = new MSSqlDataAccess();
        int intReturn;
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ref ExaminationInfoEntity Entity)
        {
            intReturn = 0;
            SqlParameter[] sqlParameters = 
            {
				new SqlParameter("@id", Entity.id),
				new SqlParameter("@ClassCode", Entity.ClassCode),
				new SqlParameter("@VisibleTime", Entity.VisibleTime),
				new SqlParameter("@HideTime", Entity.HideTime),
				new SqlParameter("@ClassName", Entity.ClassName),
				new SqlParameter("@ExamName", Entity.ExamName),
				new SqlParameter("@ExamRoom", Entity.ExamRoom),
				new SqlParameter("@ExamSubject", Entity.ExamSubject),
				new SqlParameter("@ExamTime", Entity.ExamTime),
				new SqlParameter("@StudentNumberRange", Entity.StudentNumberRange),
				new SqlParameter("@StudentNumber", Entity.StudentNumber),
				new SqlParameter("@Teachers", Entity.Teachers),
				new SqlParameter("@Notice", Entity.Notice),
				new SqlParameter("@Note", Entity.Note),
				new SqlParameter("@IsValid", Entity.IsValid),
				new SqlParameter("@HandledDate", Entity.HandledDate),
				new SqlParameter("@HandledID", Entity.HandledID),
             };
            sqlParameters[0].Direction = ParameterDirection.Output;
            intReturn = DBHelper.ExecuteNonQuery("dbo.p_ExaminationInfo_Add", CommandType.StoredProcedure, sqlParameters);
            if (intReturn == 0)
            {
                Entity.id = int.Parse(sqlParameters[0].Value.ToString());
            }
            return intReturn;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ExaminationInfoEntity Entity)
        {
            SqlParameter[] sqlParameters = 
            {
				new SqlParameter("@id", Entity.id),
				new SqlParameter("@ClassCode", Entity.ClassCode),
				new SqlParameter("@VisibleTime", Entity.VisibleTime),
				new SqlParameter("@HideTime", Entity.HideTime),
				new SqlParameter("@ClassName", Entity.ClassName),
				new SqlParameter("@ExamName", Entity.ExamName),
				new SqlParameter("@ExamRoom", Entity.ExamRoom),
				new SqlParameter("@ExamSubject", Entity.ExamSubject),
				new SqlParameter("@ExamTime", Entity.ExamTime),
				new SqlParameter("@StudentNumberRange", Entity.StudentNumberRange),
				new SqlParameter("@StudentNumber", Entity.StudentNumber),
				new SqlParameter("@Teachers", Entity.Teachers),
				new SqlParameter("@Notice", Entity.Notice),
				new SqlParameter("@Note", Entity.Note),
				new SqlParameter("@IsValid", Entity.IsValid),
				new SqlParameter("@HandledDate", Entity.HandledDate),
				new SqlParameter("@HandledID", Entity.HandledID),
             };
            return DBHelper.ExecuteNonQuery("dbo.p_ExaminationInfo_Update", CommandType.StoredProcedure, sqlParameters);
        }

        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="id">标识</param>
        /// <param name="Status">状态</param>
        /// <returns></returns>
        public int UpdateStatus(string id, string Status)
        {
            SqlParameter[] sqlParameters = 
            {
				new SqlParameter("@id", id),
				new SqlParameter("@status", Status)
             };
            return DBHelper.ExecuteNonQuery("dbo.p_ExaminationInfo_UpdateStatus", CommandType.StoredProcedure, sqlParameters);
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="ID">主键ID，多个用,分隔</param>
        /// <returns>返回操作结果</returns>
        public int Delete(string id)
        {
            SqlParameter[] sqlParameters = 
            {
                 new SqlParameter("@ids", id)
             };
            return DBHelper.ExecuteNonQuery("dbo.p_ExaminationInfo_Delete", CommandType.StoredProcedure, sqlParameters);
        }
    }

}