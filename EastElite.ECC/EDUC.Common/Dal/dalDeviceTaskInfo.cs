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
    public partial class dalDeviceTaskInfo
    {
        MSSqlDataAccess DBHelper = new MSSqlDataAccess();
        int intReturn;
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ref DeviceTaskInfoEntity Entity, string rootCode, out int errorcode, out string errormsg)
        {
            errorcode = 0;
            errormsg = "";
            intReturn = 0;
            string MessageContent = "";
            string strtext = '"' + Entity.MessageContent.text + '"';

            string strimage = "";
            strimage += "[";
            if (Entity.MessageContent.image != null)
            {

                foreach (string image in Entity.MessageContent.image)
                {
                    strimage += "\"" + image + "\",";
                }
                if (strimage.Length > 1)
                {
                    strimage = strimage.Remove(strimage.Length - 1);
                }

            }
            strimage += "]";
            string strvideo = "";
            strvideo += "[";
            if (Entity.MessageContent.video != null)
            {

                foreach (string video in Entity.MessageContent.video)
                {
                    strvideo += "\"" + video + "\",";
                }
                if (strvideo.Length > 1)
                {
                    strvideo = strvideo.Remove(strvideo.Length - 1);
                }

            }
            strvideo += "]";
            //if(strtext!="")
            //{
            //    MessageContent += string.Format("\"text\":{0}", strtext);
            //}
            //if (strimage != "")
            //{
            //    MessageContent += string.Format("\"image\":{0}", strimage);
            //}
            //if (strvideo != "")
            //{
            //    MessageContent += string.Format("\"strvideo\":{0}", strvideo);
            //}
            MessageContent = string.Format("\"text\":{0},\"image\":{1},\"video\":{2}", strtext, strimage, strvideo);
            string MessageContentAlias = "";

            string strtextalias = '"' + Entity.MessageContentAlias.text + '"';

            string strimagealias = "";
            strimagealias += "[";
            if (Entity.MessageContentAlias.image != null)
            {

                foreach (string image in Entity.MessageContentAlias.image)
                {
                    strimagealias += "\"" + image + "\",";
                }
                if (strimagealias.Length > 1)
                {
                    strimagealias = strimagealias.Remove(strimagealias.Length - 1);
                }

            }
            strimagealias += "]";
            string strvideoalias = "";
            strvideoalias += "[";
            if (Entity.MessageContentAlias.video != null)
            {

                foreach (string video in Entity.MessageContentAlias.video)
                {
                    strvideoalias += "\"" + video + "\",";
                }
                if (strvideoalias.Length > 1)
                {
                    strvideoalias = strvideoalias.Remove(strvideoalias.Length - 1);
                }

            }
            strvideoalias += "]";
            MessageContentAlias = string.Format("\"text\":{0},\"image\":{1},\"video\":{2}", strtextalias, strimagealias, strvideoalias);
            string AreaModule = JsonHelper.ObjectToJSON(Entity.MessageContent.AreaModule);

            SqlParameter[] sqlParameters = 
            {
				new SqlParameter("@ID", SqlDbType.BigInt,8),
                new SqlParameter("@Errorcode", SqlDbType.Int),
                new SqlParameter("@Errormsg", SqlDbType.NVarChar,256),
				new SqlParameter("@Code",SqlDbType.NVarChar,50),
				new SqlParameter("@DisplayModelID", Entity.DisplayModelID),
				new SqlParameter("@OperateTypeID", Entity.OperateTypeID),
				new SqlParameter("@MessageTitle", Entity.MessageTitle),
				new SqlParameter("@MessageTypeID", Entity.MessageTypeID),
				new SqlParameter("@MessageSourceID", Entity.MessageSourceID),
				new SqlParameter("@MessageContent", MessageContent),
				new SqlParameter("@TargetRange", Entity.TargetRange),
				new SqlParameter("@RargetAlias", Entity.RargetAlias),
				new SqlParameter("@TaskBeginTime",Entity.TaskBeginTime),
				new SqlParameter("@TaskEndTime", Entity.TaskEndTime),
				new SqlParameter("@TaskPriorityID", Entity.TaskPriorityID),
				new SqlParameter("@ImageSpanSecond", Entity.ImageSpanSecond),
				new SqlParameter("@ImageEffectID", Entity.ImageEffectID),
				new SqlParameter("@TaskTypeID", Entity.TaskTypeID),
				new SqlParameter("@TaskStatusID", Entity.TaskStatusID),
				new SqlParameter("@Note", Entity.Note),
				new SqlParameter("@CreatedID", Entity.CreatedID),
				new SqlParameter("@CreatedName", Entity.CreatedName),
                new SqlParameter("@rootCode", rootCode),
                new SqlParameter("@Tag",Entity.Tag==null?"":Entity.Tag),
				new SqlParameter("@Tag_and",Entity.Tag_and==null?"":Entity.Tag_and),
                new SqlParameter("@Alias", Entity.Alias==null?"":Entity.Alias),
                new SqlParameter("@Registration_ID",Entity.Registration_ID==null?"":Entity.Registration_ID),
                  new SqlParameter("@MessageContentAlias",MessageContentAlias),
                  new SqlParameter("@AreaModule",AreaModule)
			
             };

            sqlParameters[0].Direction = ParameterDirection.Output;
            sqlParameters[1].Direction = ParameterDirection.Output;
            sqlParameters[2].Direction = ParameterDirection.Output;
            sqlParameters[3].Direction = ParameterDirection.Output;
            intReturn = DBHelper.ExecuteNonQuery("dbo.spDeviceTaskInfoAdd", CommandType.StoredProcedure, sqlParameters);

            if (intReturn == 0)
            {
                Entity.ID = 0;
                errorcode = Convert.ToInt32(sqlParameters[1].Value);
                errormsg = sqlParameters[2].Value.ToString();
                Entity.Code = sqlParameters[3].Value.ToString();
            }

            return errorcode;
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int AddNotice(ref DeviceTaskNoticeInfo Notice, out int errorcode, out string errormsg)
        {
            errorcode = 0;
            errormsg = "";
            intReturn = 0;
            SqlParameter[] sqlParameters = 
            {
                  new SqlParameter("@NoticeID", SqlDbType.BigInt,8),
                  new SqlParameter("@Errorcode", SqlDbType.Int),
                  new SqlParameter("@Errormsg", SqlDbType.NVarChar,256),
			      new SqlParameter("@TaskCode", Notice.taskCode),
			      new SqlParameter("@NoticeTitle", Notice.noticeTitle),
				  new SqlParameter("@NoticeContent", Notice.noticeContent),
				  new SqlParameter("@NoticeTime", Notice.noticeTime)
             };
            sqlParameters[0].Direction = ParameterDirection.Output;
            sqlParameters[1].Direction = ParameterDirection.Output;
            sqlParameters[2].Direction = ParameterDirection.Output;

            intReturn = DBHelper.ExecuteNonQuery("dbo.spDeviceTaskNoticeInfoAdd", CommandType.StoredProcedure, sqlParameters);

            if (intReturn == 0)
            {
                Notice.noticeId = Convert.ToInt32(sqlParameters[0].Value);
                errorcode = Convert.ToInt32(sqlParameters[1].Value);
                errormsg = sqlParameters[2].Value.ToString();
            }

            return errorcode;
        }


        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="Code">标识</param>
        /// <param name="Status">状态</param>
        /// <returns></returns>
        public int UpdateTaskResultID(string Code, string TaskResultID)
        {
            SqlParameter[] sqlParameters = 
            { new SqlParameter("@Errorcode", SqlDbType.Int),
                new SqlParameter("@Errormsg", SqlDbType.NVarChar,256),
				new SqlParameter("@Code", Code),
				new SqlParameter("@TaskResultID", TaskResultID)
             };
            sqlParameters[0].Direction = ParameterDirection.Output;
            sqlParameters[1].Direction = ParameterDirection.Output;

            return DBHelper.ExecuteNonQuery("dbo.spDeviceTaskInfoUpdateTaskResultID", CommandType.StoredProcedure, sqlParameters);
        }
        public int DeleteDeviceTask(string ids)
        {
            SqlParameter[] sqlParameters = 
            { new SqlParameter("@IDs", ids)
             };


            return DBHelper.ExecuteNonQuery("dbo.spDeviceTaskInfoDelete", CommandType.StoredProcedure, sqlParameters);
        }
        public int UpdateOperateTypeID(string Code, string OperateTypeID, string ModifiedID, string ModifiedName)
        {
            SqlParameter[] sqlParameters = 
            { new SqlParameter("@Errorcode", SqlDbType.Int),
                new SqlParameter("@Errormsg", SqlDbType.NVarChar,256),
				new SqlParameter("@Code", Code),
				new SqlParameter("@OperateTypeID", OperateTypeID),
                new SqlParameter("@ModifiedID", ModifiedID),
                new SqlParameter("@ModifiedName", ModifiedName)
             };
            sqlParameters[0].Direction = ParameterDirection.Output;
            sqlParameters[1].Direction = ParameterDirection.Output;

            return DBHelper.ExecuteNonQuery("dbo.spDeviceTaskInfoUpdateOperateTypeID", CommandType.StoredProcedure, sqlParameters);
        }
    }
}