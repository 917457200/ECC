using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicLib
{
   public abstract class BaseDataAccess
    {

        #region Property & method
        /// <summary>
        /// 数据库连接串
        /// </summary>
        public abstract string ConnectionString
        {
            get;
            set;
        }
        /// <summary>
        /// 打开数据库连接
        /// </summary>
        public abstract void Open();
        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        public abstract void Close();
        /// <summary>
        /// Web事务
        /// </summary>
        public abstract void BeginTransaction();
        /// <summary>
        /// 提交事务
        /// </summary>
        public abstract void Commit();
        /// <summary>
        /// 回滚事务
        /// </summary>
        public abstract void RollBack();

        #endregion

        #region ExecuteNonQuery
        /// <summary>
        /// 执行SQL命令,并返回受影响的行数
        /// </summary>
        /// <param name="commandText">SQL命令</param>
        /// <returns>返回受影响的行数</returns>
        public int ExecuteNonQuery(string commandText)
        {
            return ExecuteNonQuery(commandText, CommandType.Text, null);
        }
        /// <summary>
        /// 执行SQL命令,并返回受影响的行数
        /// </summary>
        /// <param name="commandText">SQL命令</param>
        /// <param name="commandType">SQL命令类型</param>
        /// <returns>返回受影响的行数</returns>
        public int ExecuteNonQuery(string commandText, CommandType commandType)
        {
            return ExecuteNonQuery(commandText, commandType, null);
        }
        /// <summary>
        /// 执行SQL命令,并返回受影响的行数
        /// </summary>
        /// <param name="commandText">SQL命令</param>
        /// <param name="commandType">SQL命令类型</param>
        /// <param name="Parameters">命令参数集合</param>
        /// <returns>返回受影响的行数</returns>
        public abstract int ExecuteNonQuery(string commandText, CommandType commandType, SqlParameter[] Parameters);
        #endregion

        #region ExecuteDataReader
        /// <summary>
        /// 执行SQL命令,并返回数据的只读流
        /// </summary>
        /// <param name="commandText">SQL命令</param>
        /// <returns>返回数据的只读流</returns>
        public DbDataReader ExecuteDataReader(string commandText)
        {
            return ExecuteDataReader(commandText, CommandType.Text, null);
        }
        /// <summary>
        /// 执行SQL命令,并返回数据的只读流
        /// </summary>
        /// <param name="commandText">SQL命令</param>
        /// <param name="commandType">SQL命令类型</param>
        /// <returns>返回数据的只读流</returns>
        public DbDataReader ExecuteDataReader(string commandText, CommandType commandType)
        {
            return ExecuteDataReader(commandText, commandType, null);
        }
        /// <summary>
        /// 执行SQL命令,并返回数据的只读流
        /// </summary>
        /// <param name="commandText">SQL命令</param>
        /// <param name="commandType">SQL命令类型</param>
        /// <param name="Parameters">命令参数集合</param>
        /// <returns>返回数据的只读流</returns>
        public abstract DbDataReader ExecuteDataReader(string commandText, CommandType commandType, SqlParameter[] Parameters);
        #endregion

        #region ExecuteDataTable
        /// <summary>
        /// 执行SQL命令,并返回数据表
        /// </summary>
        /// <param name="commandText">SQL命令</param>
        /// <returns>返回数据表</returns>
        public DataTable ExecuteDataTable(string commandText)
        {
            return ExecuteDataTable(commandText, CommandType.Text, null);
        }
        /// <summary>
        /// 执行SQL命令,并返回数据表
        /// </summary>
        /// <param name="commandText">SQL命令</param>
        /// <param name="commandType">SQL命令类型</param>
        /// <returns>返回数据表</returns>
        public DataTable ExecuteDataTable(string commandText, CommandType commandType)
        {
            return ExecuteDataTable(commandText, commandType, null);
        }
        /// <summary>
        /// 执行SQL命令,并返回数据表
        /// </summary>
        /// <param name="commandText">SQL命令</param>
        /// <param name="commandType">SQL命令类型</param>
        /// <param name="Parameters">命令参数集合</param>
        /// <returns>返回数据表</returns>
        public abstract DataTable ExecuteDataTable(string commandText, CommandType commandType, SqlParameter[] Parameters);
        #endregion

        #region ExecuteDataSet
        /// <summary>
        /// 执行SQL命令,并返回数据集
        /// </summary>
        /// <param name="commandText">SQL命令</param>
        /// <returns>返回数据集</returns>
        public DataSet ExecuteDataSet(string commandText)
        {
            return ExecuteDataSet(commandText, CommandType.Text, null);
        }
        /// <summary>
        /// 执行SQL命令,并返回数据集
        /// </summary>
        /// <param name="commandText">SQL命令</param>
        /// <param name="commandType">SQL命令类型</param>
        /// <returns>返回数据集</returns>
        public DataSet ExecuteDataSet(string commandText, CommandType commandType)
        {
            return ExecuteDataSet(commandText, commandType, null);
        }
        /// <summary>
        /// 执行SQL命令,并返回数据集
        /// </summary>
        /// <param name="commandText">SQL命令</param>
        /// <param name="commandType">SQL命令类型</param>
        /// <param name="Parameters">命令参数集合</param>
        /// <returns>返回数据集</returns>
        public abstract DataSet ExecuteDataSet(string commandText, CommandType commandType, SqlParameter[] Parameters);
        #endregion

        #region ExecuteScalar
        /// <summary>
        /// 执行SQL命令,并返回结果的第一行第一列
        /// </summary>
        /// <param name="commandText">SQL命令</param>
        /// <returns>返回结果的第一行第一列</returns>
        public object ExecuteScalar(string commandText)
        {
            return ExecuteScalar(commandText, CommandType.Text, null);
        }
        /// <summary>
        /// 执行SQL命令,并返回结果的第一行第一列
        /// </summary>
        /// <param name="commandText">SQL命令</param>
        /// <param name="commandType">SQL命令类型</param>
        /// <returns>返回结果的第一行第一列</returns>
        public object ExecuteScalar(string commandText, CommandType commandType)
        {
            return ExecuteScalar(commandText, commandType, null);
        }
        /// <summary>
        /// 执行SQL命令,并返回结果的第一行第一列
        /// </summary>
        /// <param name="commandText">SQL命令</param>
        /// <param name="commandType">SQL命令类型</param>
        /// <param name="parameters">命令参数集合</param>
        /// <returns>返回结果的第一行第一列</returns>
        public abstract object ExecuteScalar(string commandText, CommandType commandType, SqlParameter[] parameters);
        #endregion
    }
}
