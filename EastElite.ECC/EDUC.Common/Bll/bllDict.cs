using System.Collections.Generic;
using System.Data;
using System.Text;
using EDUC.Common.Dal;
using EDUC.Common.Model;
using PublicLib;
using System.Collections;

namespace EDUC.Common.Bll
{
    /// <summary>
    /// 业务类
    /// </summary>
    public class bllDict : bllBase
    {


        /// <summary>
        /// 获取单行数据
        /// </summary>
        /// <param name="filter">指定条件</param>
        /// <returns>返回第一行</returns>
        public DataTable GetPagingSigInfo( string filter )
        {
            int recnums = 0;
            int pagenums = 0;
            return GetPagingListInfo( 1, 1, filter, string.Empty, out recnums, out pagenums );
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
        public DataTable GetPagingListInfo( int pageSize, int currentpage, string filter, string order, out int recnums, out int pagenums )
        {

            return new bllPaging().GetPagingInfo( "Dict", "ID", "*", pageSize, currentpage, filter, "", order, out recnums, out pagenums );
        }
        /// <summary>
        /// 获取模版Json
        /// </summary>
        /// <returns></returns>
        public string GetDictTemplate( string roleCode )
        {
            return DictTemplateToJson( roleCode );
        }
        /// <summary>
        /// 获取模版Json
        /// </summary>
        /// <returns></returns>
        public string GetDictTemplateK( string Note )
        {
            Note = "SELECT ItemKey,ItemValue, ItemOrder, IsValid, Note FROM Dict where  ItemName ='DisplayTemplateID' and ItemKey in (" + Note + ")  order By ItemOrder asc";
            return DictTemplateToJsonK( Note );
        }
        public string GetUserClassToJson( string userCode )
        {
            string Sql = "SELECT  ClassFullCode,ClassName,SUBSTRING(ClassName,1,6) AS GradeName,(CASE WHEN SUBSTRING(ClassFullCode,1,10)='6101051007' THEN '东校区' ELSE '校本部' END) AS CampusName,SUBSTRING(ClassCode, 1, 10) AS CampusCode FROM dbo.UserClassInfo WHERE RoleType=1 AND [UserCode]='" + userCode + "' ORDER BY ClassFullCode";
            return GetUserClassToJsonStr( Sql );
        }
        /// <summary>
        /// 拼装字符串
        /// </summary>
        /// <returns></returns>
        public string DictTemplateToJson( string roleCode )
        {

            DataTable dt = new bllPaging().GetDictTemplateDataTable( " ItemName ='DisplayModelID' and IsValid=1 and ItemKey!=8" );
            StringBuilder JsonStr = new StringBuilder();
            JsonStr.Append( "{\"status\":\"0\",\"mes\":\"success\",\"roleCode\":\"" + roleCode + "\",\"Data\":[" );
            for( int i = 0; i < dt.Rows.Count; i++ )
            {
                JsonStr.Append( "{" );
                JsonStr.Append( "\"DisplayTemplate\":[" );

                DataTable Templatedt = new bllPaging().GetDictTemplateDataTable( " ItemName ='DisplayTemplateID'  and IsValid=1 and ItemKey in (" + dt.Rows[i]["Note"].ToString() + ")" );
                for( int c = 0; c < Templatedt.Rows.Count; c++ )
                {
                    JsonStr.Append( "{" );
                    for( int j = 0; j < Templatedt.Columns.Count; j++ )
                    {
                        JsonStr.Append( "\"" );
                        JsonStr.Append( Templatedt.Columns[j].ColumnName );
                        JsonStr.Append( "\":\"" );
                        JsonStr.Append( Templatedt.Rows[c][j].ToString() );
                        JsonStr.Append( "\"," );
                    }
                    JsonStr.Remove( JsonStr.Length - 1, 1 );
                    JsonStr.Append( "}," );
                }
                JsonStr.Remove( JsonStr.Length - 1, 1 );

                JsonStr.Append( "]," );
                for( int j = 0; j < dt.Columns.Count; j++ )
                {
                    JsonStr.Append( "\"" );
                    JsonStr.Append( dt.Columns[j].ColumnName );
                    JsonStr.Append( "\":\"" );
                    JsonStr.Append( dt.Rows[i][j].ToString() );
                    JsonStr.Append( "\"," );
                }

                JsonStr.Remove( JsonStr.Length - 1, 1 );
                JsonStr.Append( "}," );

            }
            JsonStr.Remove( JsonStr.Length - 1, 1 );
            JsonStr.Append( "]}" );
            return JsonStr.ToString();
        }
        /// <summary>
        /// 拼装字符串
        /// </summary>
        /// <returns></returns>
        public string DictTemplateToJsonK( string Note )
        {
            StringBuilder JsonStr = new StringBuilder();
            JsonStr.Append( "{\"status\":\"0\",\"mes\":\"success\",\"Data\":[" );
            DataTable Templatedt = new bllPaging().GetDataTableInfoBySQL( Note );
            for( int c = 0; c < Templatedt.Rows.Count; c++ )
            {
                JsonStr.Append( "{" );
                for( int j = 0; j < Templatedt.Columns.Count; j++ )
                {
                    JsonStr.Append( "\"" );
                    JsonStr.Append( Templatedt.Columns[j].ColumnName );
                    JsonStr.Append( "\":\"" );
                    JsonStr.Append( Templatedt.Rows[c][j].ToString() );
                    JsonStr.Append( "\"," );
                }
                JsonStr.Remove( JsonStr.Length - 1, 1 );
                JsonStr.Append( "}," );
            }
            JsonStr.Remove( JsonStr.Length - 1, 1 );
            JsonStr.Append( "]}" );
            return JsonStr.ToString();
        }
        /// <summary>
        /// 拼装字符串
        /// </summary>
        /// <returns></returns>
        public string GetUserClassToJsonStr( string Note )
        {
            StringBuilder JsonStr = new StringBuilder();
            JsonStr.Append( "{\"status\":\"0\",\"mes\":\"success\",\"Data\":[" );
            DataTable Templatedt = new bllPaging().GetDataTableInfoBySQL( Note );
            ArrayList CampusName = new ArrayList();
            string Campus = "";
            for( int i = 0; i < Templatedt.Rows.Count; i++ )
            {
                if( Campus ==""|| Campus!= Templatedt.Rows[i][3].ToString())
                {
                    Campus = Templatedt.Rows[i][3].ToString();
                    string[] CampusStr = new string[] { Campus, Templatedt.Rows[i][4].ToString() };
                    CampusName.Add( CampusStr );
                }
            }

            for( int i = 0; i < CampusName.Count; i++ )
            {
                DataRow[] CampusNameTable = Templatedt.Select( "CampusName='" + ( (string[]) ( CampusName[i] ) )[0] + "'" );
                if( CampusNameTable.Length > 0 )
                {
                    JsonStr.Append( "{" );
                    JsonStr.Append( "\"CampusName\":\"" + ( (string[]) ( CampusName[i] ) )[0] + "\",\"CampusCode\":\"" + ( (string[]) ( CampusName[i] ) )[1] );
                    JsonStr.Append( "\",\"CampusNameData\":[" );
                    string GradeName = "";
                    for( int c = 0; c < CampusNameTable.Length; c++ )
                    {
                        if( GradeName == "" || GradeName != CampusNameTable[c][2].ToString() )
                        {
                            GradeName = CampusNameTable[c][2].ToString();
                            JsonStr.Append( "{" );
                            JsonStr.Append( "\"GradeName\":\"" + GradeName );
                            JsonStr.Append( "\",\"GradeData\":[" );
                            for( int j = 0; j < CampusNameTable.Length; j++ )
                            {
                                if( GradeName == CampusNameTable[j][2].ToString() )
                                {
                                    JsonStr.Append( "{" );
                                    JsonStr.Append( "\"ClassCode" );
                                    JsonStr.Append( "\":\"" );
                                    JsonStr.Append( Templatedt.Rows[j][0].ToString() );
                                    JsonStr.Append( "\"," );
                                    JsonStr.Append( "\"ClassName" );
                                    JsonStr.Append( "\":\"" );
                                    JsonStr.Append( Templatedt.Rows[j][1].ToString() );
                                    JsonStr.Append( "\"" );
                                    JsonStr.Append( "}," );
                                }
                            }
                            JsonStr.Remove( JsonStr.Length - 1, 1 );
                            JsonStr.Append( "]}," );
                        }
                    }
                    JsonStr.Remove( JsonStr.Length - 1, 1 );
                    JsonStr.Append( "]}," );
                }
            }
            if( Templatedt.Rows.Count > 0 )
            {
                JsonStr.Remove( JsonStr.Length - 1, 1 );
            }
            JsonStr.Append( "]}" );
            return JsonStr.ToString();
        }
        public int SynchronizDict( string rootCode, string itemName, DictEntity[] list )
        {
            StringBuilder Builder = new StringBuilder();
            Builder.AppendLine( " SET XACT_ABORT ON; " );
            Builder.AppendLine( " BEGIN TRAN; " );
            Builder.AppendLine( string.Format( " DELETE FROM dbo.DICT WHERE ItemName='{0}'", itemName ) );
            foreach( DictEntity entity in list )
            {
                Builder.AppendLine( string.Format( @"BEGIN
INSERT INTO [dbo].[Dict]
        ( [ItemName] ,
          [ItemKey] ,
          [ItemValue] ,
          [ItemOrder] ,
          [IsValid] ,
          [Note]
        )
VALUES  ( N'{0}' , -- ItemName - nvarchar(50)
          {1} , -- ItemKey - int
          N'{2}' , -- ItemValue - nvarchar(50)
          {3} , -- ItemOrder - int
          1 , -- IsValid - bit
          N''  -- Note - nvarchar(500)
        )
    END", itemName, entity.ItemKey, entity.ItemValue, entity.ItemOrder ) );
            }
            Builder.AppendLine( @"IF @@ERROR = 0 
    BEGIN
        COMMIT TRAN;
        select 0;
    END
ELSE 
    BEGIN
        ROLLBACK TRAN;
        select 1;
    END" );
            Builder.AppendLine( "  set nocount OFF; " );
            return new bllPaging().ExecuteNonQueryBySQL( Builder.ToString() );
        }
    }
}