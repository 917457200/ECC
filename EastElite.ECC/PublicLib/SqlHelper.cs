using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicLib
{

       public static class SqlClientManager
       {
           #region Private Member Variables
           private static string connectionStringToUse = null;
           private static int commandTimeout = 90;
           #endregion

           #region Public Properties

           /// <summary>
           /// Gets or sets the connection string to use.
           /// </summary>
           /// <value>The connection string to use.</value>
           [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
           public static string ConnectionStringToUse
           {
               get { return connectionStringToUse; }
               set { connectionStringToUse = value; }
           }

           /// <summary>
           /// Gets or sets the command timeout.
           /// </summary>
           /// <value>The command timeout.</value>
           public static int CommandTimeout
           {
               get { return commandTimeout; }
               set
               {
                   if (value < 1)
                   {
                       throw new ArgumentOutOfRangeException("CommandTimeout");
                   }

                   commandTimeout = value;
               }
           }

           #endregion

           #region Public Method Helper


           /// <summary>
           /// 批量导入
           /// </summary>
           /// <param name="dataTable"></param>
           public static void ExecuteStatementWithBatch(DataTable dataTable)
           {
               try
               {
                   SqlBulkCopy sqlbulkCopy = new SqlBulkCopy(connectionStringToUse);
                   sqlbulkCopy.NotifyAfter = dataTable.Rows.Count;

                   sqlbulkCopy.DestinationTableName = dataTable.TableName;
                   foreach (DataColumn dataColumn in dataTable.Columns)
                   {
                       sqlbulkCopy.ColumnMappings.Add(dataColumn.ColumnName, dataColumn.ColumnName);
                   }
                   sqlbulkCopy.WriteToServer(dataTable);
                   sqlbulkCopy.Close();

               }
               catch (Exception ex)
               {
                   throw new Exception(ex.Message);
               }
               finally
               {
               }
           }


           /// <summary>
           /// Executes the statement.
           /// </summary>
           /// <param name="statement">The statement.</param>
           public static void ExecuteStatement(string statement)
           {
               using (SqlConnection connection = new SqlConnection(connectionStringToUse))
               {
                   connection.Open();

                   ExecuteStatement(statement, connection);
               }
           }

           /// <summary>
           /// Executes the statement.
           /// </summary>
           /// <param name="statements">The statements.</param>
           public static void ExecuteStatement(string[] statements)
           {
               using (SqlConnection connection = new SqlConnection(connectionStringToUse))
               {
                   connection.Open();

                   foreach (string statement in statements)
                   {
                       ExecuteStatement(statement, connection);
                   }
               }
           }

           /// <summary>
           /// Executes the statement.
           /// </summary>
           /// <param name="statement">The statement.</param>
           /// <param name="connection">The connection.</param>
           private static void ExecuteStatement(string statement, SqlConnection connection)
           {
               using (SqlCommand command = new SqlCommand(statement, connection))
               {
                   try
                   {
                       command.CommandTimeout = commandTimeout;
                       int blah = command.ExecuteNonQuery();
                   }
                   catch (Exception e)
                   {
                       throw new InvalidOperationException(string.Format("An error occurred while executing this statement: {0}", statement), e);
                   }
               }
           }

           /// <summary>
           /// Executes the statement.
           /// </summary>
           /// <param name="statement">The statement.</param>
           /// <param name="parameters">The parameters.</param>
           public static void ExecuteStatement(string statement, SqlParameter[] parameters)
           {
               using (SqlConnection connection = new SqlConnection(connectionStringToUse))
               {
                   connection.Open();

                   using (SqlCommand command = new SqlCommand(statement, connection))
                   {
                       try
                       {
                           command.CommandTimeout = commandTimeout;
                           command.Parameters.AddRange(parameters);
                           int blah = command.ExecuteNonQuery();
                       }
                       catch (Exception e)
                       {
                           throw new InvalidOperationException(string.Format("An error occurred while executing this statement: {0}", statement), e);
                       }
                   }
               }
           }

           /// <summary>
           /// Executes the statement and identity.
           /// </summary>
           /// <param name="statement">The statement.</param>
           /// <param name="databaseName">Name of the database.</param>
           /// <param name="parameters">The parameters.</param>
           /// <returns></returns>
           public static int ExecuteStatementAndIdentity(string statement, SqlParameter[] parameters)
           {
               using (SqlConnection connection = new SqlConnection(connectionStringToUse))
               {
                   connection.Open();

                   using (SqlCommand command = new SqlCommand(statement, connection))
                   {
                       try
                       {
                           command.CommandTimeout = commandTimeout;
                           command.Parameters.AddRange(parameters);
                           int blah = command.ExecuteNonQuery();
                           SqlCommand tempCommand = new SqlCommand("SELECT @@IDENTITY", connection);
                           return int.Parse(tempCommand.ExecuteScalar().ToString());
                       }
                       catch (Exception e)
                       {
                           throw new InvalidOperationException(string.Format("An error occurred while executing this statement: {0}", statement), e);
                       }
                   }
               }
           }

           /// <summary>
           /// Executes the statement from adapter.
           /// </summary>
           /// <param name="statement">The statement.</param>
           /// <param name="tableName">Name of the table.</param>
           /// <param name="dataSet">The data set.</param>
           /// <param name="adapter">The adapter.</param>
           /// <returns></returns>
           public static long ExecuteStatementFromAdapter(string statement, string tableName, DataSet dataSet, out SqlDataAdapter adapter)
           {
               using (SqlConnection connection = new SqlConnection(connectionStringToUse))
               {
                   connection.Open();

                   using (adapter = new SqlDataAdapter(statement, connection))
                   {

                       try
                       {
                           adapter.MissingMappingAction = MissingMappingAction.Passthrough;
                           adapter.MissingSchemaAction = MissingSchemaAction.Add;

                           adapter.SelectCommand = new SqlCommand(statement, connection);
                           SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

                           adapter.Update(dataSet.Tables[tableName]);
                           SqlCommand tempCommand = new SqlCommand("SELECT @@IDENTITY", connection);
                           return long.Parse(tempCommand.ExecuteScalar().ToString());
                       }

                       catch (Exception e)
                       {
                           throw new InvalidOperationException(string.Format("An error occurred while executing this statement: {0}", statement), e);
                       }
                   }
               }
           }

           /// <summary>
           /// Executes the statemen from light adapter.
           /// </summary>
           /// <param name="statement">The statement.</param>
           /// <param name="tableName">Name of the table.</param>
           /// <param name="dataSet">The data set.</param>
           /// <param name="adapter">The adapter.</param>
           public static void ExecuteStatemenFromLightAdapter(string statement, string tableName, DataSet dataSet, out SqlDataAdapter adapter)
           {
               using (SqlConnection connection = new SqlConnection(connectionStringToUse))
               {
                   connection.Open();

                   using (adapter = new SqlDataAdapter(statement, connection))
                   {

                       try
                       {
                           adapter.MissingMappingAction = MissingMappingAction.Passthrough;
                           adapter.MissingSchemaAction = MissingSchemaAction.Add;

                           adapter.SelectCommand = new SqlCommand(statement, connection);
                           SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

                           adapter.Update(dataSet.Tables[tableName]);
                       }

                       catch (Exception e)
                       {
                           throw new InvalidOperationException(string.Format("An error occurred while executing this statement: {0}", statement), e);
                       }
                   }
               }
           }

           /// <summary>
           /// Executes the statement from file.
           /// </summary>
           /// <param name="scriptFileName">Name of the script file.</param>
           public static void ExecuteStatementFromFile(string scriptFileName)
           {
               string line;
               StringBuilder statement = new StringBuilder();

               using (StreamReader stream = new StreamReader(scriptFileName, true))
               {

                   using (SqlConnection connection = new SqlConnection(connectionStringToUse))
                   {
                       connection.Open();

                       while ((line = stream.ReadLine()) != null)
                       {

                           if (line == "GO")
                           {
                               ExecuteStatement(statement.ToString(), connection);
                               statement = new StringBuilder();
                           }
                           else if (line.Length != 0)
                           {
                               statement.AppendLine(line);
                           }
                       }
                       if (statement.Length != 0)
                       {
                           ExecuteStatement(statement.ToString(), connection);
                       }

                   }
               }
           }

           /// <summary>
           /// Executes the statement.
           /// </summary>
           /// <param name="statement">The statement.</param>
           /// <param name="parameters">The parameters.</param>
           public static void ExecuteStatementBySP(string storedProcedureName, SqlParameter[] parameters)
           {
               using (SqlConnection connection = new SqlConnection(connectionStringToUse))
               {
                   connection.Open();

                   using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                   {
                       try
                       {
                           command.CommandType = CommandType.StoredProcedure;
                           command.CommandTimeout = commandTimeout;
                           command.Parameters.AddRange(parameters);
                           int blah = command.ExecuteNonQuery();
                       }
                       catch (Exception e)
                       {
                           throw new InvalidOperationException(string.Format("An error occurred while executing this statement: {0}", storedProcedureName), e);
                       }
                   }
               }
           }

           /// <summary>
           /// Executes the read operation.
           /// </summary>
           /// <param name="statement">The statement.</param>
           /// <returns></returns>
           public static DataSet ExecuteReadOperation(string statement)
           {
               DataSet dataSet = new DataSet();

               using (SqlConnection connection = new SqlConnection(connectionStringToUse))
               {
                   connection.Open();

                   using (SqlDataAdapter dataAdapter = new SqlDataAdapter(statement, connection))
                   {

                       try
                       {
                           dataAdapter.MissingMappingAction = MissingMappingAction.Passthrough;
                           dataAdapter.MissingSchemaAction = MissingSchemaAction.Add;

                           dataAdapter.Fill(dataSet);
                       }

                       catch (Exception e)
                       {
                           throw new InvalidOperationException(string.Format("An error occurred while executing this statement: {0}", statement), e);
                       }
                   }
               }
               return dataSet;
           }

           /// <summary>
           /// Executes the read page operation.
           /// </summary>
           /// <param name="statement"></param>
           /// <param name="startIndex"></param>
           /// <param name="pageCount"></param>
           /// <returns></returns>
           public static DataSet ExecuteReadPageOperateion(string statement, int startIndex, int pageCount)
           {
               DataSet dataSet = new DataSet();

               using (SqlConnection connection = new SqlConnection(connectionStringToUse))
               {
                   connection.Open();

                   using (SqlDataAdapter dataAdapter = new SqlDataAdapter(statement, connection))
                   {

                       try
                       {
                           dataAdapter.MissingMappingAction = MissingMappingAction.Passthrough;
                           dataAdapter.MissingSchemaAction = MissingSchemaAction.Add;

                           dataAdapter.Fill(dataSet, startIndex, pageCount, "temptbl");
                       }

                       catch (Exception e)
                       {
                           throw new InvalidOperationException(string.Format("An error occurred while executing this statement: {0}", statement), e);
                       }
                   }
               }
               return dataSet;
           }

           /// <summary>
           /// Executes the read operation.
           /// </summary>
           /// <param name="statement">The statement.</param>
           /// <param name="tableName">Name of the table.</param>
           /// <param name="dataSet">The data set.</param>
           /// <param name="adapter">The adapter.</param>
           public static void ExecuteReadOperation(string statement, string tableName, DataSet dataSet, out SqlDataAdapter adapter)
           {
               using (SqlConnection connection = new SqlConnection(connectionStringToUse))
               {
                   connection.Open();

                   using (adapter = new SqlDataAdapter(statement, connection))
                   {

                       try
                       {
                           adapter.MissingMappingAction = MissingMappingAction.Passthrough;
                           adapter.MissingSchemaAction = MissingSchemaAction.Add;

                           adapter.Fill(dataSet, tableName);
                       }
                       catch (Exception e)
                       {
                           throw new InvalidOperationException(string.Format("An error occurred while executing this statement: {0}", statement), e);
                       }
                   }
               }
           }

           /// <summary>
           /// Executes the read operation.
           /// </summary>
           /// <param name="statement">The statement.</param>
           /// <param name="tableName">Name of the table.</param>
           /// <param name="dataSet">The data set.</param>
           /// <param name="adapter">The adapter.</param>
           public static void ExecuteReadOperation(string statement, SqlParameter[] parameters, string tableName, DataSet dataSet, out SqlDataAdapter adapter)
           {
               using (SqlConnection connection = new SqlConnection(connectionStringToUse))
               {
                   connection.Open();

                   using (adapter = new SqlDataAdapter(statement, connection))
                   {

                       try
                       {
                           adapter.MissingMappingAction = MissingMappingAction.Passthrough;
                           adapter.MissingSchemaAction = MissingSchemaAction.Add;

                           adapter.Fill(dataSet, tableName);
                       }
                       catch (Exception e)
                       {
                           throw new InvalidOperationException(string.Format("An error occurred while executing this statement: {0}", statement), e);
                       }
                   }
               }
           }

           /// <summary>
           /// Executes the read operation.
           /// </summary>
           /// <param name="storedProcedureName">Name of the stored procedure.</param>
           /// <param name="parameters">The parameters.</param>
           /// <param name="tableName">Name of the table.</param>
           /// <param name="dataSet">The data set.</param>
           public static void ExecuteReadOperation(string storedProcedureName, SqlParameter[] parameters, string tableName, DataSet dataSet)
           {
               using (SqlConnection connection = new SqlConnection(connectionStringToUse))
               {
                   connection.Open();

                   using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                   {
                       command.CommandType = CommandType.StoredProcedure;
                       command.CommandTimeout = commandTimeout;
                       command.Parameters.AddRange(parameters);

                       using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                       {

                           try
                           {
                               dataAdapter.MissingMappingAction = MissingMappingAction.Passthrough;
                               dataAdapter.MissingSchemaAction = MissingSchemaAction.Add;

                               dataAdapter.Fill(dataSet, tableName);
                           }

                           catch (Exception e)
                           {
                               throw new InvalidOperationException(string.Format("An error occurred while executing this statement: {0}", storedProcedureName), e);
                           }
                       }
                   }
               }
           }

           public static ArrayList ExecuteLightReadOperation(string statement)
           {
               ArrayList list = new ArrayList();
               list.Add("");
               using (SqlConnection connection = new SqlConnection(connectionStringToUse))
               {
                   connection.Open();

                   using (SqlCommand command = new SqlCommand(statement, connection))
                   {
                       SqlDataReader dataReader = command.ExecuteReader(CommandBehavior.CloseConnection);
                       while (dataReader.Read())
                       {
                           list.Add(dataReader[0]);
                       }
                       dataReader.Close();
                       return list;
                   }
               }
           }

           public static object ExecuteScalar(string statement)
           {
               using (SqlConnection connection = new SqlConnection(connectionStringToUse))
               {
                   connection.Open();
                   using (SqlCommand command = new SqlCommand(statement, connection))
                   {
                       try
                       {
                           command.CommandTimeout = commandTimeout;
                           return command.ExecuteScalar();
                       }
                       catch (Exception e)
                       {
                           throw new InvalidOperationException(string.Format("An error occurred while executing this statement: {0}", statement), e);
                       }
                   }
               }
           }


           /// <summary>
           /// Executes the statement.
           /// </summary>
           /// <param name="statement">The statement.</param>
           /// <param name="parameters">The parameters.</param>
           public static void ExecuteStatementBySP(string storedProcedureName, SqlParameter[] parameters, out DataSet dataSet)
           {
               using (SqlConnection connection = new SqlConnection(connectionStringToUse))
               {
                   connection.Open();

                   using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                   {
                       try
                       {
                           command.CommandType = CommandType.StoredProcedure;
                           command.CommandTimeout = commandTimeout;
                           command.Parameters.AddRange(parameters);
                           SqlDataAdapter da = new SqlDataAdapter(command);
                           dataSet = new DataSet();
                           da.Fill(dataSet);
                       }
                       catch (Exception e)
                       {
                           throw new InvalidOperationException(string.Format("An error occurred while executing this statement: {0}", storedProcedureName), e);
                       }
                   }
               }
           }



           #endregion

           #region Public Method Other

           public static bool CheckDataSet(DataSet ds)
           {
               if (ds != null && ds.Tables.Count > 0)
                   if (ds.Tables[0].Rows.Count > 0)
                       return true;

               return false;
           }

           #endregion
       }

}
