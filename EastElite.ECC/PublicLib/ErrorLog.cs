using System;
using System.Data;
using System.Data.SqlClient;

namespace PublicLib
{
    /// <summary>
    /// 描述：系统错误日志
    /// 作者：程国栋
    /// </summary>
    public sealed class ErrorLog
    {
        private static string _logFilePath = AppDomain.CurrentDomain.BaseDirectory+"Document\\" + "Error.log";

        /// <summary>
        /// 将错误信息写入日志文件
        /// </summary>
        /// <param name="ErrorMsg">错误信息字符串</param>
        public static void WriteErrorMessage(string ErrorMsg)
        {
            System.IO.StreamWriter sw = new System.IO.StreamWriter(_logFilePath, true, System.Text.Encoding.Default);
            try
            {
                sw.WriteLine();
                sw.WriteLine("/********************" + DateTime.Now.ToString() + "********************/");
                sw.WriteLine("ErrorMessage:" + ErrorMsg);
                sw.WriteLine("/**************************************************************/");
            }
            catch
            {
            }
            finally
            {
                sw.Close();
            }
        }

        /// <summary>
        /// 将错误信息写入日志文件
        /// </summary>
        /// <param name="ex">Exception对象</param>
        public static void WriteErrorMessage(Exception ex)
        {
            System.IO.StreamWriter sw = new System.IO.StreamWriter(_logFilePath, true, System.Text.Encoding.Default);
            try
            {
                sw.WriteLine();
                sw.WriteLine("/********************" + DateTime.Now.ToString() + "********************/");
                sw.WriteLine("Class       :" + ex.TargetSite.DeclaringType.Name);
                sw.WriteLine("Method      :" + ex.TargetSite.Name);
                sw.WriteLine("ErrorMessage:" + ex.Message);
                sw.WriteLine("/**************************************************************/");
                LogToDB(ex);
            }
            catch
            {
            }
            finally
            {
                sw.Close();
            }
        }

        private static void LogToDB(Exception ex)
        {
            MSSqlDataAccess DBHelper = new MSSqlDataAccess();
            int intReturn = 0;
            SqlParameter[] sqlParameters = 
            {
				new SqlParameter("@classname", ex.TargetSite.DeclaringType.Name),
				new SqlParameter("@method", ex.TargetSite.Name),
				new SqlParameter("@errormes", ex.Message)
             };
            intReturn = DBHelper.ExecuteNonQuery("dbo.p_errorlog_Add", CommandType.StoredProcedure, sqlParameters);
        }
    }
}
