using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicLib
{
    public sealed class MSSqlDataAccess : BaseDataAccess
    {
        private SqlConnection DbConnection;
        private SqlTransaction trans;
        private static int commandtimeout = 15000;
        private static bool istrans = false;

        public int Commandtimeout
        {
            get { return commandtimeout; }
            set { commandtimeout = value; }
        }

        public bool IsTrans
        {
            get { return istrans; }
            set { istrans = value; }
        }

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public MSSqlDataAccess()
        {
            this.DbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnectionString"].ToString());
        }

        /// <summary>
        /// 构造函数,自定义数据库连接串
        /// </summary>
        /// <param name="connString">数据库连接字符串</param>
        public MSSqlDataAccess(string connString)
        {
            this.DbConnection = new SqlConnection(connString);

        }
        #endregion

        #region int ExecuteNonQuery
        /// <summary>
        /// 执行SQL命令,并返回受影响的行数
        /// </summary>
        /// <param name="commandText">SQL命令</param>
        /// <param name="commandType">SQL命令类型</param>
        /// <param name="Parameters">命令参数集合</param>
        /// <returns>返回受影响的行数</returns>
        public override int ExecuteNonQuery(string commandText, System.Data.CommandType commandType, SqlParameter[] Parameters)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = commandText;
                cmd.CommandType = commandType;
                cmd.Connection = this.DbConnection;
                cmd.CommandTimeout = commandtimeout;
                Open();
                if (IsTrans)
                {
                    BeginTransaction();
                    cmd.Transaction = trans;
                }
                ParametersAdd(ref cmd, Parameters);
                cmd.Parameters.Add(new SqlParameter("ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, string.Empty, DataRowVersion.Default, null));
                cmd.ExecuteNonQuery();
                if (IsTrans)
                {
                    Commit();
                }
                return (int)cmd.Parameters["ReturnValue"].Value;
            }
            catch (Exception ex)
            {
                if (IsTrans)
                {
                    RollBack();
                }
              
                return -1;
            }
            finally
            {
                Close();
            }
        }
        #endregion

        #region ExecuteDataReader
        /// <summary>
        /// 执行SQL命令,并返回数据的只读流
        /// </summary>
        /// <param name="commandText">SQL命令</param>
        /// <param name="commandType">SQL命令类型</param>
        /// <param name="Parameters">命令参数集合</param>
        /// <returns>返回数据的只读流</returns>
        public override DbDataReader ExecuteDataReader(string commandText, CommandType commandType, SqlParameter[] Parameters)
        {
            try
            {
                SqlCommand cmd = this.DbConnection.CreateCommand();
                cmd.CommandText = commandText;
                cmd.CommandType = commandType;
                cmd.CommandTimeout = commandtimeout;
                if (trans != null)
                {
                    cmd.Transaction = trans;
                }
                ParametersAdd(ref cmd, Parameters);
                SqlDataReader reader = cmd.ExecuteReader();
                return reader;
            }
            catch (Exception ex)
            {
              
                return null;
            }
        }

        #endregion

        #region ExecuteDataTable
        /// <summary>
        /// 执行SQL命令,并返回数据表
        /// </summary>
        /// <param name="commandText">SQL命令</param>
        /// <param name="commandType">SQL命令类型</param>
        /// <param name="Parameters">命令参数集合</param>
        /// <param name="tableName">指定的数据表名</param>
        /// <returns>返回数据表</returns>
        public override DataTable ExecuteDataTable(string commandText, CommandType commandType, SqlParameter[] Parameters)
        {
            try
            {
                DataTable dt = new DataTable("SearchInfomationDataTable");
                SqlDataAdapter sda = new SqlDataAdapter();

                SqlCommand cmd = this.DbConnection.CreateCommand();
                cmd.CommandText = commandText;
                cmd.CommandType = commandType;
                cmd.CommandTimeout = commandtimeout;
                if (trans != null)
                {
                    cmd.Transaction = trans;
                }
                ParametersAdd(ref cmd, Parameters);
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
             
                return null;
            }
        }
        #endregion

        #region ExecuteDataSet
        /// <summary>
        /// 执行SQL命令,并返回数据集
        /// </summary>
        /// <param name="commandText">SQL命令</param>
        /// <param name="commandType">SQL命令类型</param>
        /// <param name="Parameters">命令参数集合</param>
        /// <returns>返回数据集</returns>
        public override DataSet ExecuteDataSet(string commandText, CommandType commandType, SqlParameter[] Parameters)
        {
            try
            {
                DataSet ds = new DataSet("SearchInfomationDataSet");
                SqlDataAdapter sda = new SqlDataAdapter();

                SqlCommand cmd = this.DbConnection.CreateCommand();
                cmd.CommandText = commandText;
                cmd.CommandType = commandType;
                cmd.CommandTimeout = commandtimeout;
                if (trans != null)
                {
                    cmd.Transaction = trans;
                }
                ParametersAdd(ref cmd, Parameters);
                sda.SelectCommand = cmd;
                sda.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
              
                return null;
            }
        }
        #endregion

        #region object ExecuteScalar
        /// <summary>
        /// 执行SQL命令,并返回结果的第一行第一列
        /// </summary>
        /// <param name="commandText">SQL命令</param>
        /// <param name="commandType">SQL命令类型</param>
        /// <param name="Parameters">命令参数集合</param>
        /// <returns>返回结果的第一行第一列</returns>
        public override object ExecuteScalar(string commandText, CommandType commandType, SqlParameter[] Parameters)
        {
            try
            {
                SqlCommand cmd = this.DbConnection.CreateCommand();
                cmd.CommandText = commandText;
                cmd.CommandType = commandType;
                cmd.CommandTimeout = commandtimeout;
                if (trans != null)
                {
                    cmd.Transaction = trans;
                }
                ParametersAdd(ref cmd, Parameters);
                Open();
                return cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
              
                return null;
            }
            finally
            {
                Close();
            }
        }
        #endregion

        /// <summary>
        /// 批量添加数据
        /// </summary>
        /// <param name="dt">需要插入数据库的表</param>
        /// <param name="DBTableName">表名称</param>
        /// <param name="source">数据库字段名</param>
        /// <param name="destination">插入数据库表字段名称</param>
        /// <returns></returns>
        public bool BulkInsert(DataTable dt, string DBTableName, string[] source, string[] destination)
        {
            if (source.Length != destination.Length || DBTableName.Length == 0)
            {
                return false;
            }

            //批量插入数据
            try
            {
                SqlConnection conn = DbConnection;
                conn.Open();
                using (SqlBulkCopy bcp = new SqlBulkCopy(conn))
                {
                    bcp.DestinationTableName = DBTableName;
                    for (int i = 0; i < source.Length; i++)
                    {
                        bcp.ColumnMappings.Add(source[i], destination[i]);
                    }
                    bcp.WriteToServer(dt);
                }
                return true;
            }
            catch (SqlException ex)
            {
              
                return false;
            }
            catch (Exception ex)
            {
              
                return false;
            }
        }

        private void ParametersAdd(ref SqlCommand cmd, SqlParameter[] Parameters)
        {
            if (!object.Equals(Parameters, null))
            {
                foreach (SqlParameter p in Parameters)
                {
                    cmd.Parameters.Add(p);
                }

            }
        }
        #region DataAccess 成员
        /// <summary>
        /// 数据库连接
        /// </summary>
        public override string ConnectionString
        {
            get
            {
                return this.DbConnection.ConnectionString;
            }
            set
            {
                this.DbConnection.ConnectionString = value;
            }
        }

        /// <summary>
        /// 打开数据库
        /// </summary>
        public override void Open()
        {
            if (this.DbConnection.State == ConnectionState.Closed)
            {
                this.DbConnection.Open();
            }
        }

        /// <summary>
        /// 关闭数据库
        /// </summary>
        public override void Close()
        {
            if (this.DbConnection.State == ConnectionState.Open)
            {
                this.DbConnection.Close();
            }
        }
        /// <summary>
        /// 开户数据库事务
        /// </summary>
        public override void BeginTransaction()
        {
            trans = this.DbConnection.BeginTransaction();
        }
        /// <summary>
        /// 提交数据库事务
        /// </summary>
        public override void Commit()
        {
            if (trans != null)
            {
                trans.Commit();
            }
            IsTrans = false;
            trans = null;
        }
        /// <summary>
        /// 回滚数据库事务
        /// </summary>
        public override void RollBack()
        {
            if (trans != null)
            {
                trans.Rollback();
            }
            IsTrans = false;
            trans = null;
        }
        #endregion
    }
}
