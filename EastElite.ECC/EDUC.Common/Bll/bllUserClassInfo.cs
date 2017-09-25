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
    public class bllUserClassInfo : bllBase
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

            return new bllPaging().GetPagingInfo( "UserClassInfo", "ID", "*,SUBSTRING([ClassCode],1,16) gradeCode", pageSize, currentpage, filter, "", order, out recnums, out pagenums );
        }

        /// <summary>
        /// 获取所有年级
        /// </summary>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public DataTable GetUserGradeList( string userCode, string rootCode )
        {
            if( rootCode != "" )
            {
                return new bllPaging().GetDataTableInfoBySQL( string.Format( @"SELECT SUBSTRING([ClassCode],11,6) as gradeCode,SUBSTRING([ClassName],1,6) as gradeName FROM [dbo].[UserClassInfo] WHERE [UserCode]='{0}' and SUBSTRING([ClassCode],1,10)='{1}' and Isvalid=1 GROUP BY SUBSTRING([ClassCode], 11, 6),
       SUBSTRING([ClassName], 1, 6)
       ORDER by SUBSTRING([ClassCode], 11, 6) desc", userCode, rootCode ) );
            }
            else
            {
                return new bllPaging().GetDataTableInfoBySQL( string.Format( @"SELECT SUBSTRING([ClassCode],11,6) as gradeCode,SUBSTRING([ClassName],1,6) as gradeName FROM [dbo].[UserClassInfo] WHERE [UserCode]='{0}' and Isvalid=1 GROUP BY SUBSTRING([ClassCode], 11, 6),
       SUBSTRING([ClassName], 1, 6)
       ORDER by SUBSTRING([ClassCode], 11, 6) desc", userCode, rootCode ) );
            }

        }
        /// <summary>
        /// 获取重新绑定班级失败的班级信息列表
        /// </summary>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public DataTable GetFailBindClassList()
        {
            string strSql = @"SELECT JPushID,ClassCode FROM [dbo].DeviceClassInfo WHERE isValid=1 and  AsyncResultID=2";
            return new bllPaging().GetDataTableInfoBySQL( strSql );

        }
        /// <summary>
        /// 获取班级信息列表
        /// </summary>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public DataTable GetClassListForBindingClasses( string userCode, string ClassCode )
        {
            string strSql = string.Format( @"SELECT *,SUBSTRING([ClassCode],1,16) gradeCode,SUBSTRING([ClassName],1,6) gradeName FROM [dbo].[UserClassInfoList] WHERE ClassCode in ({0}) and UserCode={1} and [UserType]=1 ORDER BY SUBSTRING([ClassCode],1,16) DESC, SUBSTRING([ClassCode],17,2) asc", ClassCode, userCode );
            return new bllPaging().GetDataTableInfoBySQL( strSql );

        }
        /// <summary>
        /// 获取班级信息列表
        /// </summary>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public DataTable GetClassList( string userCode, string ClassCode )
        {
            string strSql = string.Format( @"SELECT *,SUBSTRING([ClassFullCode],1,16) gradeCode,SUBSTRING([ClassName],1,6) gradeName FROM [dbo].[UserClassInfoList] WHERE ClassFullCode in ({0}) and UserCode={1} and [UserType]=1 ORDER BY SUBSTRING([ClassFullCode],1,16) DESC, SUBSTRING([ClassFullCode],17,2) asc", ClassCode, userCode );
            return new bllPaging().GetDataTableInfoBySQL( strSql );

        }
        /// <summary>
        /// 获取非当前版本号班级
        /// </summary>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public DataTable GetUnCurrentVersionECC()
        {
            string strSql = @"SELECT [ClassCode] FROM [dbo].DeviceClassInfo WHERE isValid=1 and  [version] NOT IN (SELECT TOP 1 [version] FROM [dbo].DeviceClassInfo ORDER BY [Version] desc) or version is null";
            return new bllPaging().GetDataTableInfoBySQL( strSql );

        }
        /// <summary>
        /// 获取非当前版本号班级
        /// </summary>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public DataTable GetUnCurrentInstallerVersionECC()
        {
            string strSql = @"SELECT  [ClassCode]
FROM    [dbo].DeviceClassInfo
WHERE   isValid=1 and InstallerVersion NOT IN ( SELECT TOP 1
                                    InstallerVersion
                           FROM     [dbo].DeviceClassInfo
                           ORDER BY InstallerVersion DESC ) OR InstallerVersion IS NULL";
            return new bllPaging().GetDataTableInfoBySQL( strSql );

        }
        /// <summary>
        /// 获取所有班级
        /// </summary>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public DataTable GetUserClassList( string userCode, string rootCode, bool isCheckInit = false )
        {
            if( !isCheckInit )
            {
                return new bllPaging().GetDataTableInfoBySQL( string.Format( @"SELECT *,SUBSTRING([ClassCode],1,16) gradeCode,SUBSTRING([ClassName],1,6) gradeName FROM [dbo].[UserClassInfoList] WHERE [UserCode]='{0}' and SUBSTRING([ClassCode],1,10)='{1}'  and [UserType]=1 and  [ClassName] !=''  ORDER BY SUBSTRING([ClassCode],1,16) DESC, SUBSTRING([ClassCode],17,2) asc", userCode, rootCode ) );
            }
            else
            {
                return new bllPaging().GetDataTableInfoBySQL( string.Format( @"SELECT *,SUBSTRING([ClassCode],1,16) gradeCode,SUBSTRING([ClassName],1,6) gradeName FROM [dbo].[UserClassInfoList] WHERE JPushID<>'' and [UserCode]='{0}' and SUBSTRING([ClassCode],1,10)='{1}'  and [UserType]=1 and  [ClassName] !=''  ORDER BY SUBSTRING([ClassCode],1,16) DESC, SUBSTRING([ClassCode],17,2) asc", userCode, rootCode ) );
            }


        }
        public DataTable GetUserClassListByUserCodeAndRootCodeOrGradeCode( string userCode, string rootCode, string gradeCode )
        {
            string strSql = string.Format( "SELECT ClassCode,SUBSTRING([ClassName],7,10) ClassName FROM [UserClassInfo] WHERE  [UserCode]='{0}'", userCode );
            if( rootCode != "" && gradeCode != "" )
            {
                strSql += string.Format( " AND SUBSTRING([ClassCode],1,10)='{0}'", rootCode );
                strSql += string.Format( " AND SUBSTRING([ClassCode],11,6)='{0}'", gradeCode );
            }
            else
            {
                strSql = "SELECT * FROM [dbo].[UserClassInfo] where 1<>1";
            }

            return new bllPaging().GetDataTableInfoBySQL( strSql );

        }

        /// <summary>
        /// 获取所有的学科类型
        /// </summary>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public DataTable GetUserSubjectTypeList( string userCode, string rootCode )
        {
            if( rootCode != "" )
            {
                return new bllPaging().GetDataTableInfoBySQL( string.Format( @"SELECT SubjectTypeID,SubjectTypeIDText,(dbo.fun_getPY(SubjectTypeIDText)+cast(SubjectTypeID AS nvarchar(10))) SubjectTypeIDValue
  FROM UserClassInfoList WHERE [UserCode]='{0}' AND SUBSTRING([ClassCode],1,10)='{1}' AND SubjectTypeID<>0 GROUP BY SubjectTypeID,SubjectTypeIDText", userCode, rootCode ) );
            }
            else
            {
                return new bllPaging().GetDataTableInfoBySQL( string.Format( @"SELECT SubjectTypeID,SubjectTypeIDText,(dbo.fun_getPY(SubjectTypeIDText)+cast(SubjectTypeID AS nvarchar(10))) SubjectTypeIDValue
  FROM UserClassInfoList WHERE [UserCode]='{0}' AND SubjectTypeID<>0 GROUP BY SubjectTypeID,SubjectTypeIDText", userCode ) );

            }


        }
        /// <summary>
        /// 获取所有班级类型
        /// </summary>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public DataTable GetUserClassTypeList( string userCode, string rootCode )
        {
            if( userCode != "" )
            {
                if( rootCode != "" )
                {
                    return new bllPaging().GetDataTableInfoBySQL( string.Format( @"SELECT ClassTypeID,ClassTypeIDText,(dbo.fun_getPY(ClassTypeIDText)+cast(ClassTypeID AS nvarchar(10))) ClassTypeIDValue
  FROM UserClassInfoList WHERE [UserCode]='{0}'  AND SUBSTRING([ClassCode],1,10)='{1}' AND ClassTypeID<>0 GROUP BY ClassTypeID,ClassTypeIDText", userCode, rootCode ) );
                }
                else
                {
                    return new bllPaging().GetDataTableInfoBySQL( string.Format( @"SELECT ClassTypeID,ClassTypeIDText,(dbo.fun_getPY(ClassTypeIDText)+cast(ClassTypeID AS nvarchar(10))) ClassTypeIDValue
  FROM UserClassInfoList WHERE [UserCode]='{0}' AND ClassTypeID<>0 GROUP BY ClassTypeID,ClassTypeIDText", userCode ) );
                }
            }
            else
            {
                if( rootCode != "" )
                {
                    return new bllPaging().GetDataTableInfoBySQL( string.Format( @"SELECT ClassTypeID,ClassTypeIDText,(dbo.fun_getPY(ClassTypeIDText)+cast(ClassTypeID AS nvarchar(10))) ClassTypeIDValue
  FROM UserClassInfoList WHERE  SUBSTRING([ClassCode],1,10)='{0}' AND ClassTypeID<>0 GROUP BY ClassTypeID,ClassTypeIDText", rootCode ) );
                }
                else
                {
                    return new bllPaging().GetDataTableInfoBySQL( string.Format( @"SELECT ClassTypeID,ClassTypeIDText,(dbo.fun_getPY(ClassTypeIDText)+cast(ClassTypeID AS nvarchar(10))) ClassTypeIDValue
  FROM UserClassInfoList WHERE ClassTypeID<>0 GROUP BY ClassTypeID,ClassTypeIDText" ) );
                }
            }

        }
        /// <summary>
        /// 同步用户班级
        /// </summary>
        /// <param name="schoolCode"></param>
        /// <param name="rootCode"></param>
        /// <param name="userCode"></param>
        /// <param name="userName"></param>
        /// <param name="roleCode"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public int SynchronizClass( string SynchMode, string schoolCode, string rootCode, string gradeName, string userCode, string userName, string roleCode, UserClassInfoEntity[] list )
        {
            string strgradeName = "";
            if( !string.IsNullOrWhiteSpace( gradeName ) )
            {
                strgradeName = " AND classname LIKE '" + gradeName + "%'";
            }
            string strroletype = "";
            if( SynchMode == "1" )//更新，过滤掉roletype=2
            {
                strroletype = " AND Roletype<>2";
            }
            StringBuilder Builder = new StringBuilder();
            Builder.AppendLine( " SET XACT_ABORT ON; " );
            Builder.AppendLine( " BEGIN TRAN; " );
            Builder.AppendLine( string.Format( @"UPDATE [dbo].[UserClassInfo] SET IsValid=0 WHERE SUBSTRING([ClassCode],1,10)='{0}' {1}{2}", rootCode, strgradeName, strroletype ) );

            foreach( UserClassInfoEntity entity in list )
            {
                if( entity.UserCode == ""  )
                {
                    return 1;
                }
                if( entity.UserCode != "" )
                {
                    Builder.AppendLine( string.Format( "  IF EXISTS(SELECT id FROM [dbo].[DeviceClassInfo] WHERE [ClassCode]='{0}') ", entity.ClassCode ) );
                    Builder.AppendLine( " BEGIN" );
                    Builder.AppendLine( string.Format( @" IF EXISTS(SELECT ID FROM [dbo].[UserClassInfo] WHERE [UserCode]='{0}' AND [ClassCode]='{1}')", entity.UserCode, entity.ClassCode ) );
                    Builder.AppendLine( string.Format( @" BEGIN
UPDATE [dbo].[UserClassInfo] SET UserType={0},[ClassFullCode]='{1}',ClassName='{2}',[RoleType]={3},[IsValid]=1 WHERE [UserCode]='{4}' AND [ClassCode]='{5}';
END", entity.UserType, entity.ClassFullCode, entity.ClassName, entity.RoleType, entity.UserCode, entity.ClassCode ) );
                    Builder.AppendLine( "ELSE " );
                    Builder.AppendLine( string.Format( @"BEGIN
        INSERT INTO [dbo].[UserClassInfo]
        ( [UserCode],
          [UserType],
          [ClassCode],
          [ClassFullCode],
          [ClassName],
          [RoleType] ,
          [IsValid] ,
          [Note] ,
          [HandledID] ,
          [HandledName] ,
          [HandledDate]
        )
VALUES  ( N'{0}' , -- UserCode - nvarchar(20)
          {1} , -- UserType - tinyint
          N'{2}' , -- ClassCode - nvarchar(50)
          N'{3}' , -- ClassFullCode - nvarchar(50)
          N'{4}' , -- ClassName - nvarchar(50)
          {5} , -- RoleType - tinyint
          1 , -- IsValid - bit
          N'' , -- Note - nvarchar(500)
          N'{6}' , -- HandledID - nvarchar(20)
          N'{7}' , -- HandledName - nvarchar(50)
           GETDATE()  -- HandledDate - smalldatetime
        )
    END", entity.UserCode, entity.UserType, entity.ClassCode, entity.ClassFullCode, entity.ClassName, entity.RoleType, userCode, userName ) );
                    Builder.AppendLine( " END" );
                }
            }
            Builder.AppendLine( string.Format( "DELETE FROM [UserClassInfo] WHERE IsValid=0 {0}{1};", strgradeName, strroletype ) );
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
            string a = Builder.ToString();
            return new bllPaging().ExecuteNonQueryBySQL( Builder.ToString() );

        }

        public int UpdateUserClass( List<UserClassInfoEntity> list, string userCode )
        {
            StringBuilder Builder = new StringBuilder();
            Builder.AppendLine( " SET XACT_ABORT ON; " );
            Builder.AppendLine( " BEGIN TRAN; " );
            Builder.AppendLine( string.Format( " DELETE FROM [dbo].[UserClassInfo] WHERE  [UserCode]='{0}'", userCode ) );
            foreach( UserClassInfoEntity entity in list )
            {
                Builder.AppendLine( string.Format( @"INSERT INTO [dbo].[UserClassInfo]
        ( [UserCode] ,
          [UserType] ,
          [ClassCode] ,
          [ClassFullCode] ,
          [ClassName] ,
          [RoleType] ,
          [IsValid] ,
          [Note] ,
          [HandledID] ,
          [HandledName] ,
          [HandledDate]
        )
VALUES  ( N'{0}' , -- UserCode - nvarchar(20)
          {1} , -- UserType - tinyint
          N'{2}' , -- ClassCode - nvarchar(50)
          N'{3}' , -- ClassFullCode - nvarchar(50)
          N'{4}' , -- ClassName - nvarchar(50)
          {5} , -- RoleType - tinyint
          {6} , -- IsValid - bit
          N'{7}' , -- Note - nvarchar(500)
          N'{8}' , -- HandledID - nvarchar(20)
          N'{9}' , -- HandledName - nvarchar(50)
          GETDATE()  -- HandledDate - smalldatetime
        )", entity.UserCode, entity.UserType, entity.ClassCode, entity.ClassFullCode, entity.ClassName, entity.RoleType, 1, entity.Note, entity.HandledID, entity.HandledName ) );
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