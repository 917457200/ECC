using System.Collections.Generic;
using System.Data;
using EDUC.Common.Dal;
using EDUC.Common.Model;
using PublicLib;

namespace EDUC.Common.Bll
{
    /// <summary>
    /// 业务类
    /// </summary>
    public class bllDeviceTaskInfo : bllBase
    {
        dalDeviceTaskInfo dal = new dalDeviceTaskInfo();
        public int Add( ref DeviceTaskInfoEntity Entity, operatelogEntity entity, string rootCode, out int errorcode, out string errormsg )
        {
            errorcode = 0;
            errormsg = "";
            int result = dal.Add( ref Entity, rootCode, out errorcode, out errormsg );
            DataRow dr = dtBase.NewRow();
            if( result != 0 )
            {
                errorcode = -1;
                errormsg = "操作数据库失败";
            }

            // blllog.Add(entity.module, entity.pageurl, entity.otype, entity.logcontent, entity.cuser.ToString());
            return result;
        }

        public int AddNotice( ref DeviceTaskNoticeInfo Entity )
        {
            int errorcode = 0; string errormsg = string.Empty;
            int result = dal.AddNotice( ref Entity, out errorcode, out errormsg );
            DataRow dr = dtBase.NewRow();
            if( result != 0 )
            {
                errorcode = -1;
                errormsg = "操作数据库失败";
            }
            return result;
        }
        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="Code">标识</param>
        /// <param name="Status">状态</param>
        /// <returns></returns>
        public int UpdateTaskResultID( string Code, string TaskResultID )
        {

            int result = dal.UpdateTaskResultID( Code, TaskResultID );
            //检测执行结果
            return result;
        }
        public int DeleteDeviceTask( string ids )
        {

            int result = dal.DeleteDeviceTask( ids );
            //检测执行结果
            return result;
        }
        public int UpdateOperateTypeID( string Code, string OperateTypeID, string ModifiedID, string ModifiedName )
        {

            int result = dal.UpdateOperateTypeID( Code, OperateTypeID, ModifiedID, ModifiedName );
            //检测执行结果
            return result;
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <returns>返回操作结果</returns>


        /// <summary>
        /// 获取单行数据
        /// </summary>
        /// <param name="filter">指定条件</param>
        /// <returns>返回第一行</returns>
        public DataTable GetPagingSigInfo( string filter )
        {
            int recnums = 0;
            int pagenums = 0;
            return GetPagingdetailListInfo( 1, 1, filter, string.Empty, out recnums, out pagenums );
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

            return new bllPaging().GetPagingInfo( "DeviceTaskInfo", "Code", @"ID,[Tag] ,
          [Tag_and] ,
          [Alias] ,
          [Registration_ID] , Code ,[DisplayModelID],
        ( SELECT   itemvalue
          FROM      dict
          WHERE     [ItemName] = 'DisplayModelID'
                    AND [ItemKey] = [DeviceTaskInfo].[DisplayModelID]
        ) [DisplayModelIDName],[RargetAlias], CONVERT(varchar(16), [CreatedDate], 20)  [CreatedDate],
        [OperateTypeID], ( SELECT    itemvalue
          FROM      dict
          WHERE     [ItemName] = 'OperateTypeID'
                    AND [ItemKey] = [DeviceTaskInfo].[OperateTypeID]
        ) [OperateTypeIDName],[MessageContent],[MessageContentAlias],[TaskResultID]", pageSize, currentpage, filter, "", order, out recnums, out pagenums );
        }

        public DataTable GetPagingdetailListInfo( int pageSize, int currentpage, string filter, string order, out int recnums, out int pagenums )
        {

            return new bllPaging().GetPagingInfo( "DeviceTaskInfo", "Code", @"ID,
 Code ,
 [Tag] ,
          [Tag_and] ,
          [Alias] ,
          [Registration_ID] ,
 [DisplayModelID], ( SELECT   itemvalue
          FROM      dict
          WHERE     [ItemName] = 'DisplayModelID'
                    AND [ItemKey] = [DeviceTaskInfo].[DisplayModelID]
        ) [DisplayModelIDName],
        [OperateTypeID], ( SELECT    itemvalue
          FROM      dict
          WHERE     [ItemName] = 'OperateTypeID'
                    AND [ItemKey] = [DeviceTaskInfo].[OperateTypeID]
        ) [OperateTypeIDName],
        [MessageTitle],
        [MessageTypeID],( SELECT    itemvalue
          FROM      dict
          WHERE     [ItemName] = 'MessageTypeID'
                    AND [ItemKey] = [DeviceTaskInfo].[MessageTypeID]
        ) [MessageTypeIDName],
        [MessageSourceID],( SELECT    itemvalue
          FROM      dict
          WHERE     [ItemName] = 'MessageSourceID'
                    AND [ItemKey] = [DeviceTaskInfo].[MessageSourceID]
        ) [MessageSourceIDName],
        [TaskTypeID],( SELECT    itemvalue
          FROM      dict
          WHERE     [ItemName] = 'TaskTypeID'
                    AND [ItemKey] = [DeviceTaskInfo].[TaskTypeID]
        ) [TaskTypeIDName],
        [TaskResultID],
       
        CASE [DeviceTaskInfo].[TaskResultID] WHEN 0 THEN '成功'  ELSE '失败' END TaskResultIDName,
        [RargetAlias],
        [MessageContent],
        [Note],
        CONVERT(varchar(16), [TaskBeginTime], 20) [TaskBeginTime],CONVERT(varchar(16), [TaskEndTime], 20) [TaskEndTime],
        [TaskPriorityID],( SELECT    itemvalue
          FROM      dict
          WHERE     [ItemName] = 'TaskPriorityID'
                    AND [ItemKey] = [DeviceTaskInfo].[TaskPriorityID]
        ) [TaskPriorityIDName],
        [ImageEffectID],( SELECT    itemvalue
          FROM      dict
          WHERE     [ItemName] = 'ImageEffectID'
                    AND [ItemKey] = [DeviceTaskInfo].[ImageEffectID]
        ) [ImageEffectIDName],
        [ImageSpanSecond],
        [CreatedName],
        CONVERT(varchar(16), [CreatedDate], 20) [CreatedDate],
        [ModifiedName],
        CONVERT(varchar(16), [ModifiedDate], 20) [ModifiedDate],[MessageContentAlias]", pageSize, currentpage, filter, "", order, out recnums, out pagenums );
        }
        public DataTable GetPagingSigInfoRepeatTask( string filter, string classname, string rootcode )
        {
            int recnums = 0;
            int pagenums = 0;
            return getRepeatTask( 1, 1, filter, string.Empty, classname, rootcode, out recnums, out pagenums );
        }
        public DataTable getRepeatTask( int pageSize, int currentpage, string filter, string order, string classname, string rootcode, out int recnums, out int pagenums )
        {

            return new bllPaging().GetPagingInfo( "DeviceTaskInfo", "Code", string.Format( @"ID,
 Code ,
 [Tag] ,
          [Tag_and] ,
          [Alias] ,
          [Registration_ID] ,
 [DisplayModelID], ( SELECT   itemvalue
          FROM      dict
          WHERE     [ItemName] = 'DisplayModelID'
                    AND [ItemKey] = [DeviceTaskInfo].[DisplayModelID]
        ) [DisplayModelIDName],
        [OperateTypeID], ( SELECT    itemvalue
          FROM      dict
          WHERE     [ItemName] = 'OperateTypeID'
                    AND [ItemKey] = [DeviceTaskInfo].[OperateTypeID]
        ) [OperateTypeIDName],
        [MessageTitle],
        [MessageTypeID],( SELECT    itemvalue
          FROM      dict
          WHERE     [ItemName] = 'MessageTypeID'
                    AND [ItemKey] = [DeviceTaskInfo].[MessageTypeID]
        ) [MessageTypeIDName],
        [MessageSourceID],( SELECT    itemvalue
          FROM      dict
          WHERE     [ItemName] = 'MessageSourceID'
                    AND [ItemKey] = [DeviceTaskInfo].[MessageSourceID]
        ) [MessageSourceIDName],
        [TaskTypeID],( SELECT    itemvalue
          FROM      dict
          WHERE     [ItemName] = 'TaskTypeID'
                    AND [ItemKey] = [DeviceTaskInfo].[TaskTypeID]
        ) [TaskTypeIDName],
        [TaskResultID],
       
        CASE [DeviceTaskInfo].[TaskResultID] WHEN 0 THEN '成功'  ELSE '失败' END TaskResultIDName,
        [RargetAlias],
        [MessageContent],
        [AreaModule],
        [Note],
        CONVERT(varchar(16), [TaskBeginTime], 20) [TaskBeginTime],CONVERT(varchar(16), [TaskEndTime], 20) [TaskEndTime],
        [TaskPriorityID],( SELECT    itemvalue
          FROM      dict
          WHERE     [ItemName] = 'TaskPriorityID'
                    AND [ItemKey] = [DeviceTaskInfo].[TaskPriorityID]
        ) [TaskPriorityIDName],
        [ImageEffectID],( SELECT    itemvalue
          FROM      dict
          WHERE     [ItemName] = 'ImageEffectID'
                    AND [ItemKey] = [DeviceTaskInfo].[ImageEffectID]
        ) [ImageEffectIDName],
        [ImageSpanSecond],
        [CreatedName],
        CONVERT(varchar(16), [CreatedDate], 20) [CreatedDate],
        [ModifiedName],
        CONVERT(varchar(16), [ModifiedDate], 20) [ModifiedDate],[MessageContentAlias],(select top 1 jpushid from DeviceClassInfo where classname='{0}' and  SUBSTRING(ClassCode,1,10)='{1}') NewJpushID ", classname, rootcode ), pageSize, currentpage, filter, "", order, out recnums, out pagenums );
        }
        public int ExistClass( string ClassCode, string JPushID )
        {
            string strsql = string.Format( "SELECT count(*) FROM [dbo].[DeviceClassInfo] WHERE [JPushID] ='{0}' and [ClassCode] = '{1}'", JPushID, ClassCode );
            return new bllPaging().ExistBySQL( strsql );
        }
        public DataTable getTaskNoticeInfo(string TaskInfoCode )
        {

            return new bllPaging().GetDataTableInfoBySQL( "SELECT * FROM DeviceTaskNoticeInfo WHERE [TaskCode]=" + TaskInfoCode + "" );
        }
        public int ClearVersionByJPushID( string filter )
        {
            string strsql = string.Format( "UPDATE [dbo].[DeviceClassInfo] SET [Version]='' where 1<>1 {0}", filter );
            return new bllPaging().ExecuteNonQueryBySQL( strsql );
        }
        public int ClearHeartBeatCheckDateByJPushID( string filter )
        {
            string strsql = string.Format( "UPDATE [dbo].[DeviceClassInfo] SET [HeartBeatCheckDate]=NULL where 1<>1  {0}", filter );
            return new bllPaging().ExecuteNonQueryBySQL( strsql );
        }
        public int ClearInstallerHeartBeatCheckDateByJPushID( string filter )
        {
            string strsql = string.Format( "UPDATE [dbo].[DeviceClassInfo] SET [InstallerHeartBeatCheckDate]=NULL where 1<>1  {0}", filter );
            return new bllPaging().ExecuteNonQueryBySQL( strsql );
        }
        public int UpdateDeviceClassVersionByClassCode( string classCode, string version )
        {
            string filter = string.Format( "UPDATE [dbo].[DeviceClassInfo] SET [Version]='{0}' WHERE [ClassCode]='{1}'", version, classCode );

            return new bllPaging().ExecuteNonQueryBySQL( filter );
        }
        public int UpdateDeviceClassTastDateByClassCode( string classCode, string tastdate )
        {
            string filter = string.Format( "UPDATE [dbo].[DeviceClassInfo] SET [TastDate]='{0}' WHERE [ClassCode]='{1}'", tastdate, classCode );
            return new bllPaging().ExecuteNonQueryBySQL( filter );
        }
        public int UpdateDeviceClassEmptyDateByClassCode( string classCode, string emptydate )
        {
            string filter = string.Format( "UPDATE [dbo].[DeviceClassInfo] SET [EmptyDate]='{0}' WHERE [ClassCode]='{1}'", emptydate, classCode );
            return new bllPaging().ExecuteNonQueryBySQL( filter );
        }
        public int UpdateDeviceClassSwitchDateByClassCode( string classCode, string switchdate )
        {
            string filter = string.Format( "UPDATE [dbo].[DeviceClassInfo] SET [SwitchDate]='{0}' WHERE [ClassCode]='{1}'", switchdate, classCode );
            return new bllPaging().ExecuteNonQueryBySQL( filter );
        }
        public int UpdateDeviceClassNavigationBarVisibleByClassCode( string classCode, string navigationBarVisible )
        {
            string filter = string.Format( "UPDATE [dbo].[DeviceClassInfo] SET [NavigationBarVisible]={0} WHERE [ClassCode]='{1}'", navigationBarVisible, classCode );
            return new bllPaging().ExecuteNonQueryBySQL( filter );
        }
        public int UpdateDeviceClassUpdateScheduleDateByClassCode( string classCode, string updateScheduleDate )
        {
            string filter = string.Format( "UPDATE [dbo].[DeviceClassInfo] SET [UpdateScheduleDate]='{0}' WHERE [ClassCode]='{1}'", updateScheduleDate, classCode );
            return new bllPaging().ExecuteNonQueryBySQL( filter );
        }
        public int UpdateDeviceClassHeartBeatCheckDateByClassCode( string classCode, string heartBeatCheckDate )
        {
            string filter = string.Format( "UPDATE [dbo].[DeviceClassInfo] SET [HeartBeatCheckDate]='{0}' WHERE [ClassCode]='{1}'", heartBeatCheckDate, classCode );
            return new bllPaging().ExecuteNonQueryBySQL( filter );
        }
        public DataTable GetECCVersionList()
        {
            return new bllPaging().GetDataTableInfoBySQL( "SELECT [Version] FROM [dbo].[DeviceClassInfo] WHERE [IsValid]=1 GROUP BY [Version]" );
        }
        public int UpdateDeviceClassInstallerVersionByClassCode( string classCode, string version, string heartbeatcheckdate )
        {
            string filter = string.Format( "UPDATE [dbo].[DeviceClassInfo] SET [InstallerVersion]='{0}',[InstallerHeartBeatCheckDate]='{1}' WHERE [ClassCode]='{2}'", version, heartbeatcheckdate, classCode );
            return new bllPaging().ExecuteNonQueryBySQL( filter );
        }
        public int UpdateDeviceClassInstallerHeartBeatCheckDateByClassCode( string classCode, string InstallerHeartBeatCheckDate )
        {
            string filter = string.Format( "UPDATE [dbo].[DeviceClassInfo] SET [InstallerHeartBeatCheckDate]='{0}' WHERE [ClassCode]='{1}'", InstallerHeartBeatCheckDate, classCode );
            return new bllPaging().ExecuteNonQueryBySQL( filter );
        }
        public int UpdateDeviceClassStartProgramSwitchStaus( string classCode, string StartProgramSwitchStaus )
        {
            string filter = string.Format( "UPDATE [dbo].[DeviceClassInfo] SET [StartProgramSwitchStatus]='{0}' WHERE [ClassCode]='{1}'", StartProgramSwitchStaus, classCode );
            return new bllPaging().ExecuteNonQueryBySQL( filter );
        }
        public DataTable GetECCInstallerVersionList()
        {
            return new bllPaging().GetDataTableInfoBySQL( "SELECT [InstallerVersion] FROM [dbo].[DeviceClassInfo] WHERE [IsValid]=1 GROUP BY [InstallerVersion]" );
        }
        public int UpdateRepeatBindClassInfoAsyncResultID( string JPushID, string AsyncResultID )
        {
            string filter = string.Format( "UPDATE [dbo].[DeviceClassInfo] SET [AsyncResultID]={0} WHERE IsValid=1 AND [JPushID]='{1}'", AsyncResultID, JPushID );
            return new bllPaging().ExecuteNonQueryBySQL( filter );
        }
        public int UpdateRepeatBindClassInfoAsyncResultID( string JPushIDs )
        {
            string filter = string.Format( "UPDATE [dbo].[DeviceClassInfo] SET [AsyncResultID]=2 WHERE IsValid=1 AND [JPushID] IN ({0})", JPushIDs );
            return new bllPaging().ExecuteNonQueryBySQL( filter );
        }
    }
}