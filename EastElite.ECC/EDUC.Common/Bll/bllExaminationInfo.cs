using System.Collections.Generic;
using System.Data;
using System.Text;
using EDUC.Common.Dal;
using EDUC.Common.Model;
using PublicLib;

namespace EDUC.Common.Bll
{
	/// <summary>
    /// 业务类
    /// </summary>
    public class bllExaminationInfo : bllBase
    {
		
        /// <summary>
        /// 获取单行数据
        /// </summary>
        /// <param name="filter">指定条件</param>
        /// <returns>返回第一行</returns>
        public DataTable GetPagingSigInfo( string filter)
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
            return new bllPaging().GetPagingInfo("ExaminationInfo", "id", @"[id] ,
          [ClassCode] ,
          [ClassName] ,
          CONVERT(varchar(100), [VisibleTime], 20) [VisibleTime] ,
          CONVERT(varchar(100), [HideTime], 20) [HideTime]  ,
          [ExamName] ,
          [ExamRoom] ,
          [ExamSubject] ,
          [ExamTime] ,
          [StudentNumberRange] ,
          [StudentNumber] ,
          [Teachers] ,
          [Notice] ,
          [Note]
         ", pageSize, currentpage, filter, "", order, out recnums, out pagenums);
        }
        public DataTable GetExamInfos(string classCode)
        {
            string sql=string.Format(@"select [id] ExamId,
          [ClassCode] ,
          [ClassName] ,
          CONVERT(varchar(100), [VisibleTime], 20) [VisibleTime] ,
          CONVERT(varchar(100), [HideTime], 20) [HideTime]  ,
          [ExamName] ,
          [ExamRoom] ,
          [ExamSubject] ,
          [ExamTime] ,
          [StudentNumberRange] ,
          [StudentNumber] ,
          [Teachers] ,
          [Notice] ,
          [Note] from ExaminationInfo where isValid=1 and ClassCode='{0}' order by ExamTime asc",classCode);
            return new bllPaging().GetDataTableInfoBySQL(sql);
        }
      
      
        public DataTable ImportExaminationData(string isDel,List<ExaminationInfoEntity> list,int pageSize, int currentpage, string filter, string order, out int recnums, out int pagenums,operatelogEntity log)
        {
            recnums = 0;
            pagenums = 0;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SET NOCOUNT ON;\n");
            sb.AppendLine("BEGIN TRAN tan1;\n");
            sb.Append("DECLARE @rowCount int;\n");
            sb.Append("SET @rowCount=0;\n");
            sb.Append("DECLARE @ClassCode NVARCHAR(50);\n");
            if (isDel == "1")
            {
                sb.Append("DELETE FROM [dbo].[ExaminationInfo];\n");
            }
            foreach (ExaminationInfoEntity entity in list)
            {
                sb.Append("SET @ClassCode='';\n");
                sb.Append(string.Format("IF EXISTS(SELECT id FROM [dbo].[DeviceClassInfo] WHERE [IsValid]=1 AND [ClassName]='{0}' AND SUBSTRING([ClassCode],1,10)='{1}' )\n", entity.ClassName, entity.Campus));
                sb.Append("BEGIN\n");
                sb.Append(string.Format("SELECT @ClassCode=classcode FROM [dbo].[DeviceClassInfo] WHERE [IsValid]=1 AND [ClassName]='{0}' AND SUBSTRING([ClassCode],1,10)='{1}'\n", entity.ClassName, entity.Campus));
                sb.Append(string.Format(@"INSERT INTO [dbo].[ExaminationInfo]
        (
          [ClassCode] ,
          [ClassName] ,
          [VisibleTime] ,
          [HideTime] ,
          [ExamName] ,
          [ExamRoom] ,
          [ExamSubject] ,
          [ExamTime] ,
          [StudentNumberRange] ,
          [StudentNumber] ,
          [Teachers] ,
          [Notice] ,
          [Note] ,
          [IsValid] ,
          [HandledDate] ,
          [HandledID]
        )
VALUES  ( 
          @ClassCode , -- ClassCode - nvarchar(50)
          N'{0}' , -- ClassName - nvarchar(50)
          '{1}' , -- VisibleTime - smalldatetime
          '{2}' , -- HideTime - smalldatetime
          N'{3}' , -- ExamName - nvarchar(50)
          N'{4}' , -- ExamRoom - nvarchar(50)
          N'{5}' , -- ExamSubject - nvarchar(50)
          N'{6}' , -- ExamTime - nvarchar(50)
          N'{7}' , -- StudentNumberRange - nvarchar(50)
          {8} , -- StudentNumber - int
          N'{9}' , -- Teachers - nvarchar(50)
          N'{10}' , -- Notice - nvarchar(max)
          N'{11}' , -- Note - nvarchar(max)
          1 , -- IsValid - bit
          GETDATE() , -- HandledDate - smalldatetime
          N'{12}'  -- HandledID - nvarchar(20)
        );", entity.ClassName, entity.VisibleTime, entity.HideTime, entity.ExamName, entity.ExamRoom, entity.ExamSubject, entity.ExamTime, entity.StudentNumberRange, entity.StudentNumber, entity.Teachers, entity.Notice, entity.Note, entity.HandledID));

                sb.Append("SET @rowCount=@rowCount+1;\n");
                sb.Append("END\n");

            }

            sb.AppendLine(" if(@@error=0)\n BEGIN\n COMMIT TRAN tan1;\n  END");
            sb.AppendLine(" else \n begin \n ROLLBACK TRAN tan1; set @rowCount=0; END \n");
            sb.AppendLine(" select @rowCount;");
            sb.AppendLine(" SET NOCOUNT OFF;");
            string sql=sb.ToString();
            if (Helper.StringToInt(new bllPaging().ExecuteScalarBySQL(sql)) > 0)
            {
                return GetPagingListInfo(pageSize, currentpage, filter, order, out  recnums, out  pagenums);
            }
            else
            {
                return null;
            }
           
          
        }

        public void EmptyExamInfo()
        {
            new bllPaging().ExecuteScalarBySQL("DELETE FROM [ExaminationInfo]");
        }

        public void DeleteExamInfo(string ids,string isValid)
        {
            new bllPaging().ExecuteScalarBySQL(string.Format("DELETE FROM [ExaminationInfo] where id in ({0})",ids));
        }
        public DataTable GetGradeCode(string campus)
        {
            string sql = string.Format(@"SELECT SUBSTRING(CLASSCODE,11,6) GradeCode,SUBSTRING([ClassName],1,6) GradeName FROM [ExaminationInfo] WHERE IsValid=1 AND SUBSTRING(CLASSCODE,1,10)='{0}' GROUP BY SUBSTRING(CLASSCODE,11,6),SUBSTRING([ClassName],1,6);", campus);
            return new bllPaging().GetDataTableInfoBySQL(sql);
        }
        public DataTable GetClassCode(string campus,string gradeCode)
        {
            string sql = string.Format(@"SELECT ClassCode,SUBSTRING([ClassName],7,5) ClassName FROM [ExaminationInfo] WHERE IsValid=1 AND SUBSTRING(CLASSCODE,1,10)='{0}' AND SUBSTRING(CLASSCODE,11,6)='{1}' GROUP BY [ClassCode],SUBSTRING([ClassName], 7, 5);", campus, gradeCode);
            return new bllPaging().GetDataTableInfoBySQL(sql);
        }
    }
}