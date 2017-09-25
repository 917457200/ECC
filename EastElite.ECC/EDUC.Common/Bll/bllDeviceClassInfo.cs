using System.Collections.Generic;
using System.Data;
using System.Text;
using EDUC.Common.Dal;
using EDUC.Common.Model;
using PublicLib;
using System;

namespace EDUC.Common.Bll
{
    /// <summary>
    /// 业务类
    /// </summary>
    public class bllDeviceClassInfo : bllBase
    {
        dalDeviceClassInfo dal = new dalDeviceClassInfo();

        public DataTable UpdateDeviceSN( out int errorcode, out string errormsg, string classCode, string deviceSN, string roomNum, string IPAddress, int IPPort, string modifiedID, string modifiedName, int isCheckInt, int isCheckRoomNum, string JPushID, int deviceTypeID, string version, operatelogEntity entity )
        {
            errorcode = 0; errormsg = "";
            int result = dal.UpdateDeviceSN( out errorcode, out errormsg, classCode, deviceSN, roomNum, IPAddress, IPPort, modifiedID, modifiedName, isCheckInt, isCheckRoomNum, JPushID, deviceTypeID, version );
            DataRow dr = dtBase.NewRow();
            if( result == 0 )
            {
                dr["type"] = errorcode.ToString();
                dr["mes"] = errormsg.ToString();
            }
            else
            {
                dr["type"] = -1;
                dr["mes"] = "操作数据库失败";
            }

            dtBase.Rows.Add( dr );
            dtBase.AcceptChanges();
            //  blllog.Add(entity.module, entity.pageurl, entity.otype, entity.logcontent, entity.cuser.ToString());
            return dtBase;
        }

        public DataTable UpdateDeviceClass( out int errorcode, out string errormsg, string ID, string BanZhuRenPhotoPath, string BanZhuRenQRPath, string RoomNum, string ClassNickName, string ClassSlogan, string ZuoYouMing, string Introduction, string Recommended, string ClassLogoPath, string ClassQRPath, string ModefiedID, string ModifiedName )
        {
            errorcode = 0; errormsg = "";
            int result = dal.UpdateDeviceClass( out errorcode, out errormsg, ID, BanZhuRenPhotoPath, BanZhuRenQRPath, RoomNum, ClassNickName, ClassSlogan, ZuoYouMing, Introduction, Recommended, ClassLogoPath, ClassQRPath, ModefiedID, ModifiedName );
            DataRow dr = dtBase.NewRow();
            if( result == 0 )
            {
                dr["type"] = errorcode.ToString();
                dr["mes"] = errormsg.ToString();
            }
            else
            {
                dr["type"] = -1;
                dr["mes"] = "操作数据库失败";
            }

            dtBase.Rows.Add( dr );
            dtBase.AcceptChanges();
            //  blllog.Add(entity.module, entity.pageurl, entity.otype, entity.logcontent, entity.cuser.ToString());
            return dtBase;
        }
        public DataTable RepeatDeviceClass( out int errorcode, out string errormsg, string JPushID, string ClassCode, string OldClassName, string ClassName, string ClassFullCode, string BanZhuRenPhotoPath, string BanZhuRenQRPath, string ClassNickName, string ClassSlogan, string ZuoYouMing, string Introduction, string Recommended, string ClassLogoPath, string ClassQRPath, int SubjectTypeID, string SubjectTypeIDText, int ClassTypeID, string ClassTypeIDText, string SemesterName, string BanZhuRenCode, string BanZhuRenName, string ModefiedID, string ModifiedName, string RoomNum )
        {
            errorcode = 0; errormsg = "";
            int result = dal.RepeatDeviceClass( out errorcode, out errormsg, JPushID, ClassCode, OldClassName, ClassName, ClassFullCode, BanZhuRenPhotoPath, BanZhuRenQRPath, ClassNickName, ClassSlogan, ZuoYouMing, Introduction, Recommended, ClassLogoPath, ClassQRPath, SubjectTypeID, SubjectTypeIDText, ClassTypeID, ClassTypeIDText, SemesterName, BanZhuRenCode, BanZhuRenName, ModefiedID, ModifiedName, RoomNum );
            DataRow dr = dtBase.NewRow();
            if( result == 0 )
            {
                dr["type"] = errorcode.ToString();
                dr["mes"] = errormsg.ToString();
            }
            else
            {
                dr["type"] = -1;
                dr["mes"] = "操作数据库失败";
            }

            dtBase.Rows.Add( dr );
            dtBase.AcceptChanges();
            //  blllog.Add(entity.module, entity.pageurl, entity.otype, entity.logcontent, entity.cuser.ToString());
            return dtBase;
        }

        public void RepeatUserClass( string UserCode, int UserType, string ClassCode, string ClassFullCode, string ClassName, int RoleType, int IsValid, string Note, string HandledID, string HandledName, DateTime HandledDate )
        {
            dal.RepeatUserClass( UserCode, UserType, ClassCode,ClassFullCode,ClassName, RoleType, IsValid, Note, HandledID, HandledName, HandledDate);

        }
        /// <summary>
        /// 获取单行数据
        /// </summary>
        /// <param name="filter">指定条件</param>
        /// <returns>返回第一行</returns>
        public DataTable GetPagingSigInfo( string filter )
        {
            int recnums = 0;
            int pagenums = 0;
            return GetPagingListInfo( 1, 1, filter, "id", out recnums, out pagenums );
        }
        public DataTable GetDeviceTerminalScreenshotList( int pageSize, int currentpage, string filter, string order, out int recnums, out int pagenums )
        {
            return new bllPaging().GetPagingInfo( "DeviceClassInfo", "", @"ClassCode,[Version],
          [ClassName] ,(SELECT [FieldContent] FROM [dbo].[DataFieldInfo] WHERE id=[TerminalScreenshot]) TerminalScreenshot, CONVERT(varchar(16), (SELECT [CreatedDate] FROM [dbo].[DataFieldInfo] WHERE id=[TerminalScreenshot]), 20) CreatedDate
      ", pageSize, currentpage, filter, "", order, out recnums, out pagenums );
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
            return new bllPaging().GetPagingInfo( "DeviceClassInfo", "", @"[ID],[DeviceSN] ,
          [DeviceCode] ,
          [IPAddress] ,
          [IPPort] ,
          [RoomNum] ,
          [JPushID] ,
          [DeviceTypeID] ,
          [SchoolCode] ,
          [ClassCode] ,
          [ClassFullCode] ,
          [ClassName] ,
          [ClassNickName] ,
          [ClassLogoPath] ,
          [ClassQRPath] ,
          [ClassSlogan] ,
          [SemesterName] ,
          [BanZhuRenCode] ,
          [BanZhuRenName] ,
          [BanZhuRenPhotoPath] ,
          [BanZhuRenQRPath] ,
          [ZuoYouMing] ,
          [Introduction] ,
          [Recommended] ,
          [SubjectTypeID] ,
          [SubjectTypeIDText] ,
          [ClassTypeID] ,
          [ClassTypeIDText] ,
          [OldDeviceSN] ,
          [DeviceStatusID] ,
          [Attachment] ,
           CONVERT(varchar(16), [TastDate], 20) TastDate ,
         CONVERT(varchar(16), [EmptyDate], 20) EmptyDate ,
           CONVERT(varchar(16), [SwitchDate], 20) SwitchDate ,
          [NavigationBarVisible] ,
          CONVERT(varchar(16), [UpdateScheduleDate], 20) UpdateScheduleDate ,
           CONVERT(varchar(16), [HeartBeatCheckDate], 20) HeartBeatCheckDate ,
          [IsValid] ,
          [Note] ,
          [Version] ,
          [CreatedID] ,
          [CreatedName] ,
           CONVERT(varchar(16), [CreatedDate], 20) CreatedDate ,
          [ModifiedID] ,
          [ModifiedName] ,(CASE WHEN SUBSTRING(ClassFullCode,1,10)='6101051007' THEN '东校区' ELSE '校本部' END) AS CampusName,
          [ModifiedDate],'U'+CAST([DeviceTypeID] AS NVARCHAR(2)) UserType,SUBSTRING([ClassCode],1,16) as gradeCode,('S'+cast(SubjectTypeID AS nvarchar(10))) SubjectTypeIDValue,('L'+cast(ClassTypeID AS nvarchar(10))) ClassTypeIDValue, [InstallerVersion] ,
           CONVERT(varchar(16), [InstallerHeartBeatCheckDate], 20) InstallerHeartBeatCheckDate,StartProgramSwitchStatus", pageSize, currentpage, filter, "", order, out recnums, out pagenums );
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
        public DataTable GetIniClassInfoList( int pageSize, int currentpage, string filter, string order, out int recnums, out int pagenums )
        {
            return new bllPaging().GetPagingInfo( "DeviceClassInfo", "", @"[DeviceSN] ,
         [DeviceCode] ,
          [IPAddress] ,
          [IPPort] ,
          [RoomNum] ,
          [JPushID] ,
          [ClassCode] ,
          [ClassFullCode] ,
          [ClassName] ,
          [ClassNickName] ,
          [ClassLogoPath] ,
          [ClassQRPath] ,
          [ClassSlogan] ,
          [SemesterName] ,
          [BanZhuRenCode] ,
          [BanZhuRenName] ,
          [BanZhuRenPhotoPath] ,
          [BanZhuRenQRPath] ,
          [ZuoYouMing] ,
          [Introduction] ,
          [Recommended] ,
          [SubjectTypeID] ,
          [SubjectTypeIDText] ,
          [ClassTypeID] ,
          [ClassTypeIDText] ,
          [OldDeviceSN] ,
          [DeviceStatusID] ,
          [Attachment] ,
          [Note] ,
          [CreatedID] ,
          [CreatedName] ,
           CONVERT(varchar(16), [CreatedDate], 20) CreatedDate ,
          [ModifiedID] ,
          [ModifiedName] ,
          [ModifiedDate],
'U'+CAST([DeviceTypeID] AS NVARCHAR(2)) UserType,
		  SUBSTRING([ClassCode],1,16) as gradeCode,
		  ('S'+cast(SubjectTypeID AS nvarchar(10))) SubjectTypeIDValue,
		  ('L'+cast(ClassTypeID AS nvarchar(10))) ClassTypeIDValue,(CASE WHEN SUBSTRING(ClassFullCode,1,10)='6101051007' THEN '东校区' ELSE '校本部' END) AS CampusName,
		   [SchoolCode]", pageSize, currentpage, filter, "", order, out recnums, out pagenums );
        }
        /// <summary>
        /// 同步班级设备
        /// </summary>
        /// <param name="schoolCode"></param>
        /// <param name="rootCode"></param>
        /// <param name="userCode"></param>
        /// <param name="userName"></param>
        /// <param name="roleCode"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public int SynchronizDevice( string SynchDeviceMode, string schoolCode, string rootCode, string gradeName, string userCode, string userName, string roleCode, DeviceClassInfoEntity[] list )
        {
            string strgradeName = "";
            if( !string.IsNullOrWhiteSpace( gradeName ) )
            {
                strgradeName = " AND classname LIKE '" + gradeName + "%'";
            }
            StringBuilder Builder = new StringBuilder();
            Builder.AppendLine( " SET XACT_ABORT ON; " );
            Builder.AppendLine( " BEGIN TRAN; " );
            Builder.AppendLine( " DECLARE @deviceCode NVARCHAR(50); " );
            Builder.AppendLine( string.Format( @" UPDATE [dbo].[DeviceClassInfo] SET [IsValid]=0 WHERE schoolCode='{0}' and SUBSTRING([ClassCode],1,10)='{1}' {2}", schoolCode, rootCode, strgradeName ) );
            foreach( DeviceClassInfoEntity entity in list )
            {
                Builder.AppendLine( string.Format( @" IF EXISTS ( SELECT  id
            FROM    DeviceClassInfo
            WHERE   ClassCode = '{0}') ", entity.ClassCode ) );
                Builder.AppendLine( string.Format( @" BEGIN
        UPDATE  [dbo].[DeviceClassInfo]
        SET     [ClassFullCode] = '{0}' ,
                ClassName = '{1}' ,
                SemesterName = '{2}' ,
                BanZhuRenCode = '{3}' ,
                BanZhuRenName = '{4}' ,
                BanZhuRenPhotoPath = '{5}' ,
                ZuoYouMing = '{6}' ,
                Introduction = '{7}' ,
                Recommended = '{8}' ,
                SubjectTypeID = '{9}' ,
                SubjectTypeIDText = '{10}' ,
                ClassTypeID = '{11}' ,
                ClassTypeIDText = '{12}',
                IsValid = 1
        WHERE  [ClassCode] = '{13}';
    END", entity.ClassFullCode, entity.ClassName, entity.SemesterName, entity.BanZhuRenCode, entity.BanZhuRenName, entity.BanZhuRenPhotoPath, entity.ZuoYouMing, entity.Introduction, entity.Recommended, entity.SubjectTypeID, entity.SubjectTypeIDText, entity.ClassTypeID, entity.ClassTypeIDText, entity.ClassCode ) );
                if( SynchDeviceMode == "2" )
                {


                    Builder.AppendLine( "ELSE " );
                    Builder.AppendLine( string.Format( @"BEGIN
SET @deviceCode='';
SELECT TOP 1  @deviceCode= [DeviceCode] FROM [dbo].[DeviceClassInfo] ORDER BY id desc;
IF @deviceCode='' OR @deviceCode IS NULL
SET @deviceCode='00';
SET @deviceCode=SUBSTRING(@deviceCode,1,2)+RIGHT('0000'+CAST(CAST(SUBSTRING(@deviceCode,3,4) AS BIGINT)+1 AS NVARCHAR(50)),4);
        INSERT  INTO [dbo].[DeviceClassInfo]
                ( [DeviceSN] ,
                  [DeviceCode] ,
                  [IPAddress] ,
                  [IPPort] ,
                  [RoomNum] ,
                  [JPushID] ,
                  [DeviceTypeID] ,
                  [SchoolCode] ,
                  [ClassCode] ,
                  [ClassFullCode] ,
                  [ClassName] ,
                  [ClassNickName] ,
                  [ClassLogoPath] ,
                  [ClassQRPath] ,
                  [ClassSlogan] ,
                  [SemesterName] ,
                  [BanZhuRenCode] ,
                  [BanZhuRenName] ,
                  [BanZhuRenPhotoPath] ,
                  [BanZhuRenQRPath] ,
                  [ZuoYouMing] ,
                  [Introduction] ,
                  [Recommended] ,
                  [SubjectTypeID] ,
                  [SubjectTypeIDText] ,
                  [ClassTypeID] ,
                  [ClassTypeIDText] ,
                  [OldDeviceSN] ,
                  [DeviceStatusID] ,
                  [Attachment] ,
                  [IsValid] ,
                  [Note] ,
                  [Version] ,
                  [CreatedID] ,
                  [CreatedName] ,
                  [CreatedDate] ,
                  [ModifiedID] ,
                  [ModifiedName] ,
                  [ModifiedDate]
                )
        VALUES  ( N'' , -- DeviceSN - nvarchar(50)
                  @deviceCode , -- DeviceCode - nvarchar(50)
                  N'' , -- IPAddress - nvarchar(50)
                  0 , -- IPPort - int
                  N'' , -- RoomNum - nvarchar(50)
                  N'' , -- JPushID - nvarchar(50)
                  4 , -- DeviceTypeID - tinyint
                  N'{0}' , -- SchoolCode - nvarchar(50)
                  N'{1}' , -- ClassCode - nvarchar(50)
                  N'{2}' , -- ClassFullCode - nvarchar(50)
                  N'{3}' , -- ClassName - nvarchar(50)
                  N'' , -- ClassNickName - nvarchar(50)
                  N'' , -- ClassLogoPath - nvarchar(500)
                  N'' , -- ClassQRPath - nvarchar(500)
                  N'' , -- ClassSlogan - nvarchar(500)
                  N'{4}' , -- SemesterName - nvarchar(50)
                  N'{5}' , -- BanZhuRenCode - nvarchar(20)
                  N'{6}' , -- BanZhuRenName - nvarchar(50)
                  N'{7}' , -- BanZhuRenPhotoPath - nvarchar(500)
                  N'' , -- BanZhuRenQRPath - nvarchar(500)
                  N'{8}' , -- ZuoYouMing - nvarchar(500)
                  N'{9}' , -- Introduction - nvarchar(4000)
                  N'{10}' , -- Recommended - nvarchar(4000)
                  {11}, -- SubjectTypeID - tinyint
                  N'{12}' , -- SubjectTypeIDText - nvarchar(50)
                  {13}, -- ClassTypeID - tinyint
                  N'{14}' , -- ClassTypeIDText - nvarchar(50)
                  N'' , -- OldDeviceSN - nvarchar(50)
                  0 , -- DeviceStatusID - tinyint
                  N'' , -- Attachment - nvarchar(4000)
                  1 , -- IsValid
                  N'' , -- Note - nvarchar(4000)
                  N'' , -- Version - nvarchar(50)
                  N'{15}' , -- CreatedID - nvarchar(20)
                  N'{16}' , -- CreatedName - nvarchar(50)
                  GETDATE() , -- CreatedDate - smalldatetime
                  N'' , -- ModifiedID - nvarchar(20)
                  N'' , -- ModifiedName - nvarchar(50)
                  NULL  -- ModifiedDate - smalldatetime
                )
    END", entity.SchoolCode, entity.ClassCode, entity.ClassFullCode, entity.ClassName, entity.SemesterName, entity.BanZhuRenCode, entity.BanZhuRenName, entity.BanZhuRenPhotoPath, entity.ZuoYouMing, entity.Introduction, entity.Recommended, entity.SubjectTypeID, entity.SubjectTypeIDText, entity.ClassTypeID, entity.ClassTypeIDText, userCode, userName ) );
                }
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
            string b = Builder.ToString();
            return new bllPaging().ExecuteNonQueryBySQL( Builder.ToString() );
        }

    }
}