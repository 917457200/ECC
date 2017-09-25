using System;
using System.Data;
using System.Data.SqlClient;
using PublicLib;
using System.Text;


namespace EDUC.Common.Bll
{
    public class bllPaging
    {
        MSSqlDataAccess Obj = new MSSqlDataAccess();
        /// <summary>
        /// 分页功能
        /// </summary>
        /// <param name="tableName">表名，可以是多个表，最好用别名</param>
        /// <param name="primarykey">主键，可以为空，但@order为空时该值不能为空</param>
        /// <param name="fields">要取出的字段，可以是多个表的字段，可以为空，为空表示select *  </param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="currentpage">当前页，表示第页</param>
        /// <param name="filter">条件，可以为空，不用填where</param>
        /// <param name="group">分组依据，可以为空，不用填group by</param>
        /// <param name="order">排序，可以为空，为空默认按主键升序排列，不用填order by</param>
        /// <param name="recnums">记录个数</param>
        /// <param name="pagenums">页数</param>
        /// <returns></returns>
        public DataTable GetPagingInfo(string tableName, string primarykey, string fields, int pageSize, int currentpage, string filter, string group, string order, out int recnums, out int pagenums)
        {

            #region 调试时使用

            string sql = "exec spPagingLarge ";
            sql += " @tablenames='" + tableName + "', ";
            sql += " @primarykey='" + primarykey + "', ";
            sql += " @fields='" + fields.Replace("'", "''") + "', ";
            sql += " @pagesize='" + pageSize + "', ";
            sql += " @currentpage='" + currentpage + "', ";
            sql += " @filter='" + filter + "', ";
            sql += " @group='" + group + "', ";
            sql += " @order='" + order + "', ";
            sql += " @recnums='" + 0 + "', ";
            sql += " @pagenums='" + 0 + "' ";
            string selectSQl = sql;

            #endregion


            DataTable Dt = new DataTable("data");
            try
            {
                SqlParameter[] sqlParameters = 
                {
                     new SqlParameter("@tablenames", tableName),
                     new SqlParameter("@primarykey", primarykey),
                     new SqlParameter("@fields", fields),
                     new SqlParameter("@pagesize", pageSize),
                     new SqlParameter("@currentpage", currentpage),
                     new SqlParameter("@filter", filter),
                     new SqlParameter("@group", group),
                     new SqlParameter("@order", order),
                     new SqlParameter("@recnums", 0),
                     new SqlParameter("@pagenums", 0)
                 };
                sqlParameters[8].Direction = ParameterDirection.Output;
                sqlParameters[9].Direction = ParameterDirection.Output;
                Dt = Obj.ExecuteDataTable("dbo.spPagingLarge", CommandType.StoredProcedure, sqlParameters);

                recnums = Helper.StringToInt(sqlParameters[8].Value.ToString());
                pagenums = Helper.StringToInt(sqlParameters[9].Value.ToString());
                return Dt;
            }
            catch (Exception ex)
            {
              
                recnums = 0;
                pagenums = 0;
                return Dt;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SQL"></param>
        /// <returns></returns>
        public DataTable GetDataTableInfoBySQL(string SQL)
        {
            return Obj.ExecuteDataTable(SQL);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SQL"></param>
        /// <returns></returns>
        public string ExecuteScalarBySQL(string SQL)
        {
           
            object obje = Obj.ExecuteScalar(SQL);
            if (obje != null)
            {
                return obje.ToString();
            }
            return string.Empty;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SQL"></param>
        /// <returns></returns>
        public int ExistBySQL( string SQL )
        {

            object obje = Obj.ExecuteScalar( SQL );
            if( obje != null )
            {
                return int.Parse(obje.ToString());
            }
            return 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SQL"></param>
        /// <returns></returns>
        public int ExecuteNonQueryBySQL(string SQL)
        {
            return Obj.ExecuteNonQuery(SQL);
        }
        //获取模版DataTable
        public DataTable GetDictTemplateDataTable(string StrWhere)
        {
            StringBuilder Sql = new StringBuilder();
            Sql.Append( string.Format( "SELECT ItemKey,ItemValue, ItemOrder, IsValid, Note FROM Dict where {0} order By ItemOrder asc ", StrWhere ) );
            return  GetDataTableInfoBySQL(Sql.ToString());
        }
    }
}
