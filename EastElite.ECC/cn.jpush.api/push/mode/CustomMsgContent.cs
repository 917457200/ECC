using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cn.jpush.api.push.mode
{
    [Description("")]
    [Serializable]
    public class MessageContent : BaseMessageContent
    {
        public string text { get; set; }
        public string[] image { get; set; }
        public string[] video { get; set; }

        public AreaModuleClass AreaModule { get; set; }

        [Description("")]
        [Serializable]
        public class AreaModuleClass
        {
            public ClsActive ClsActive { get; set; }
            public List<ClsHonor> ClsHonor { get; set; }
            public List<ClsHomeWk> ClsHomeWk { get; set; }
            public List<ClsCheckItem> ClsCheckItem { get; set; }
            public ClsCheckStu ClsCheckStu { get; set; }
            public List<ClsNotice> ClsNotice { get; set; }

        }
        /// <summary>
        /// 班级活动
        /// </summary>
        [Description("")]
        [Serializable]
        public class ClsActive
        {
            private string _date = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            public string date
            {
                get { return _date; }
                set { _date = value; }
            }
            /// <summary>
            /// 1 图 2视频
            /// </summary>
            public string kind { get; set; }
            public string code { get; set; }

            public string context { get; set; }
            public string url { get; set; }

        }
        /// <summary>
        /// 班级荣誉
        /// </summary>
        [Description("")]
        [Serializable]
        public class ClsHonor
        {
            private string _date = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            public string date
            {
                get { return _date; }
                set { _date = value; }
            }
            //1个人 2班级
            public string kind { get; set; }
            //奖（比赛）
            public string prise { get; set; }
            //等级（其他）
            public string rank { get; set; }
            //获奖感言
            public string summary { get; set; }
            //地址
            public string url { get; set; }
            public string code { get; set; }

        }
        /// <summary>
        /// 作业布置
        /// </summary>
        [Description("")]
        [Serializable]
        public class ClsHomeWk
        {
            private string _date = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            public string date
            {
                get { return _date; }
                set { _date = value; }
            }
            //(预留)
            public string kind { get; set; }
            //科目
            public string subject { get; set; }
            //内容
            public string context { get; set; }
            public string code { get; set; }


        }
        /// <summary>
        /// 指标检查考勤
        /// </summary>
        [Description("")]
        [Serializable]
        public class ClsCheckItem
        {
            private string _date = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            public string date
            {
                get { return _date; }
                set { _date = value; }
            }
            //检查项目
            public string checkItem { get; set; }
            //分数（正数）
            public double itemScore { get; set; }
            public string code { get; set; }


        }
        /// <summary>
        /// 学生出勤考勤
        /// </summary>
        [Description("")]
        [Serializable]
        public class ClsCheckStu
        {
            private string _date = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            public string date
            {
                get { return _date; }
                set { _date = value; }
            }
            public double sumNum { get; set; }//
            public double absentNum { get; set; }//旷课
            public double lateNum { get; set; }//迟到
            public double actualNum { get; set; }// 实到
            public double monAbsentNum { get; set; }// 上午缺勤
            public double aftAbsentNum { get; set; }//下午缺勤
            public string code { get; set; }

        }
        /// <summary>
        /// 通知公告
        /// </summary>
        [Description("")]
        [Serializable]
        public class ClsNotice
        {
            private string _date = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            public string date
            {
                get { return _date; }
                set { _date = value; }
            }
            public string context { get; set; }
            public string code  { get; set; }
            public string title { get; set; }
            public string url { get; set; }


        }
      
    }
    [Description("")]
    [Serializable]
    public class CustomMsgContent : BaseCustomMsgContent
    {

        private string _Code = string.Empty;
        private int _DisplayModelID = 0;
        private int _OperateTypeID = 0;
        private string _MessageTitle = string.Empty;
        private int _MessageTypeID = 0;
        private int _MessageSourceID = 0;
        private MessageContent _MessageContent = null;
        private string _TargetRange = string.Empty;
        private string _RargetAlias = string.Empty;
        private string _TaskBeginTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
        private string _TaskEndTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
        private int _TaskPriorityID = 0;
        private int _ImageSpanSecond = 0;
        private int _ImageEffectID = 0;
        private int _TaskTypeID = 0;
        private int _TaskStatusID = 0;
        private int _TaskResultID = 0;
        private string _Note = string.Empty;
        private string _CreatedID = string.Empty;
        private string _CreatedName = string.Empty;
        private string _CreatedDate = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
        private string _ModifiedID = string.Empty;
        private string _ModifiedName = string.Empty;
        private string _ModifiedDate = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");


        public string Code
        {
            get { return _Code; }
            set { _Code = value; }
        }

        public int DisplayModelID
        {
            get { return _DisplayModelID; }
            set { _DisplayModelID = value; }
        }

        public int OperateTypeID
        {
            get { return _OperateTypeID; }
            set { _OperateTypeID = value; }
        }

        public string MessageTitle
        {
            get { return _MessageTitle; }
            set { _MessageTitle = value; }
        }

        public int MessageTypeID
        {
            get { return _MessageTypeID; }
            set { _MessageTypeID = value; }
        }

        public int MessageSourceID
        {
            get { return _MessageSourceID; }
            set { _MessageSourceID = value; }
        }

        public MessageContent MessageContent
        {
            get { return _MessageContent; }
            set { _MessageContent = value; }
        }

        public string TargetRange
        {
            get { return _TargetRange; }
            set { _TargetRange = value; }
        }

        public string RargetAlias
        {
            get { return _RargetAlias; }
            set { _RargetAlias = value; }
        }

        public string TaskBeginTime
        {
            get { return _TaskBeginTime; }
            set { _TaskBeginTime = value; }
        }

        public string TaskEndTime
        {
            get { return _TaskEndTime; }
            set { _TaskEndTime = value; }
        }

        public int TaskPriorityID
        {
            get { return _TaskPriorityID; }
            set { _TaskPriorityID = value; }
        }

        public int ImageSpanSecond
        {
            get { return _ImageSpanSecond; }
            set { _ImageSpanSecond = value; }
        }

        public int ImageEffectID
        {
            get { return _ImageEffectID; }
            set { _ImageEffectID = value; }
        }

        public int TaskTypeID
        {
            get { return _TaskTypeID; }
            set { _TaskTypeID = value; }
        }

        public int TaskStatusID
        {
            get { return _TaskStatusID; }
            set { _TaskStatusID = value; }
        }

        public int TaskResultID
        {
            get { return _TaskResultID; }
            set { _TaskResultID = value; }
        }

        public string Note
        {
            get { return _Note; }
            set { _Note = value; }
        }

        public string CreatedID
        {
            get { return _CreatedID; }
            set { _CreatedID = value; }
        }

        public string CreatedName
        {
            get { return _CreatedName; }
            set { _CreatedName = value; }
        }

        public string CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }

        public string ModifiedID
        {
            get { return _ModifiedID; }
            set { _ModifiedID = value; }
        }

        public string ModifiedName
        {
            get { return _ModifiedName; }
            set { _ModifiedName = value; }
        }

        public string ModifiedDate
        {
            get { return _ModifiedDate; }
            set { _ModifiedDate = value; }
        }
    }
    /// <summary>
    /// 通知公告
    /// </summary>
    [Description("")]
    [Serializable]
    public class NoticeInfo
    {
        private string _noticecreatetime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
        private string _noticetime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");

        public string NoticeCreateTime
        {
            get { return _noticecreatetime; }
            set { _noticecreatetime = value; }
        }
        public string NoticeTime
        {
            get { return _noticetime; }
            set { _noticetime = value; }
        }
        public string NoticeTitle { get; set; }
        public string NoticeContent { get; set; }
        public long TaskID { get; set; }

    }

    public class BaseCustomMsgContent
    {
        private string _Code = string.Empty;
        private int _OperateTypeID = 0;
        //private string _MessageTitle = string.Empty;
        private int _MessageSourceID = 0;
        private BaseMessageContent _MessageContent = null;

        public string Code
        {
            get { return _Code; }
            set { _Code = value; }
        }


        public int OperateTypeID
        {
            get { return _OperateTypeID; }
            set { _OperateTypeID = value; }
        }

        //public string MessageTitle
        //{
        //    get { return _MessageTitle; }
        //    set { _MessageTitle = value; }
        //}



        public int MessageSourceID
        {
            get { return _MessageSourceID; }
            set { _MessageSourceID = value; }
        }

        public BaseMessageContent MessageContent
        {
            get { return _MessageContent; }
            set { _MessageContent = value; }
        }


    }

    public class BaseMessageContent
    {
        public string text { get; set; }
    }
}
