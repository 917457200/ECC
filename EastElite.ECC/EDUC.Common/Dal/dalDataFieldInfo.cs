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
    public partial class dalDataFieldInfo 
    {
        MSSqlDataAccess DBHelper = new MSSqlDataAccess();
		int intReturn;
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ref DataFieldInfoEntity Entity,out int errorcode, out string errormsg)
        {
            errorcode = 0; 
            errormsg = "";
            SqlParameter[] sqlParameters = 
            {	new SqlParameter("@ID", SqlDbType.BigInt,8),
                new SqlParameter("@Errorcode", SqlDbType.Int),
				new SqlParameter("@Errormsg", SqlDbType.NVarChar,256),
				new SqlParameter("@FieldCode", Entity.FieldCode),
				new SqlParameter("@FieldTypeID", Entity.FieldTypeID),
		
				new SqlParameter("@FieldName1", Entity.FieldName1),
			
				new SqlParameter("@FieldContent", Entity.FieldContent),
			
				new SqlParameter("@CreatedID", Entity.CreatedID)
				
             };
            sqlParameters[0].Direction = ParameterDirection.Output;
            sqlParameters[1].Direction = ParameterDirection.Output;
            sqlParameters[2].Direction = ParameterDirection.Output;
            intReturn = DBHelper.ExecuteNonQuery("dbo.spDataFieldInfoAdd", CommandType.StoredProcedure, sqlParameters);
            if (intReturn == 0)
            {
                if (intReturn == 0)
                {

                    errorcode = Convert.ToInt32(sqlParameters[0].Value);
                    errormsg = sqlParameters[1].Value.ToString();
                }
            }
            return intReturn;
        }

       public int UploadTerminalScreenshot(string classCode,ref DataFieldInfoEntity Entity,out int errorcode, out string errormsg)
        {
            errorcode = 0; 
            errormsg = "";
            SqlParameter[] sqlParameters = 
            {	new SqlParameter("@ID", SqlDbType.BigInt,8),
                new SqlParameter("@Errorcode", SqlDbType.Int),
				new SqlParameter("@Errormsg", SqlDbType.NVarChar,256),
                new SqlParameter("@ClassCode",classCode),
				new SqlParameter("@FieldCode", Entity.FieldCode),
				new SqlParameter("@FieldTypeID", Entity.FieldTypeID),
		
				new SqlParameter("@FieldName1", Entity.FieldName1),
			
				new SqlParameter("@FieldContent", Entity.FieldContent),
			
				new SqlParameter("@CreatedID", Entity.CreatedID)
				
             };
            sqlParameters[0].Direction = ParameterDirection.Output;
            sqlParameters[1].Direction = ParameterDirection.Output;
            sqlParameters[2].Direction = ParameterDirection.Output;
            intReturn = DBHelper.ExecuteNonQuery("dbo.spUploadTerminalScreenshot", CommandType.StoredProcedure, sqlParameters);
            if (intReturn == 0)
            {
                if (intReturn == 0)
                {

                    errorcode = Convert.ToInt32(sqlParameters[0].Value);
                    errormsg = sqlParameters[1].Value.ToString();
                }
            }
            return intReturn;
        }

    }
}