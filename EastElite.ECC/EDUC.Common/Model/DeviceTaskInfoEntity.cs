using System;
using System.ComponentModel;
using cn.jpush.api.push.mode;
using PublicLib;

namespace EDUC.Common.Model
{

    [Description("")]
    [Serializable]
    public class DeviceTaskInfoEntity
    {
		private long _ID = 0;
		private string _Code = string.Empty;
		private int _DisplayModelID = 0;
		private int _OperateTypeID = 0;
		private string _MessageTitle = string.Empty;
		private int _MessageTypeID = 0;
		private int _MessageSourceID = 0;
        private MessageContent _MessageContent = null;
        private MessageContent _MessageContentAlias = null;
		private string _TargetRange = string.Empty;
		private string _RargetAlias = string.Empty;
		private DateTime _TaskBeginTime = DateTime.Now;
        private DateTime _TaskEndTime = DateTime.Now;
		private int _TaskPriorityID = 0;
		private int _ImageSpanSecond = 0;
		private int _ImageEffectID = 0;
		private int _TaskTypeID = 0;
		private int _TaskStatusID = 0;
		private int _TaskResultID = 0;
		private string _Note = string.Empty;
		private string _CreatedID = string.Empty;
		private string _CreatedName = string.Empty;
        private DateTime _CreatedDate = DateTime.Now;
		private string _ModifiedID = string.Empty;
		private string _ModifiedName = string.Empty;
        private DateTime _ModifiedDate = DateTime.Now;
        private string _Tag = string.Empty;
        private string _Tag_and = string.Empty;
        private string _Alias = string.Empty;
        private string _Registration_ID = string.Empty;

        public string Tag
        {
            get { return _Tag; }
            set { _Tag = value; }
        }
        public string Tag_and
        {
            get { return _Tag_and; }
            set { _Tag_and = value; }
        }
        public string Alias
        {
            get { return _Alias; }
            set { _Alias = value; }
        }
        public string Registration_ID
        {
            get { return _Registration_ID; }
            set { _Registration_ID = value; }
        }
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_ID", NotEmpty = false, Length = 8, NotEmptyECode = "DeviceTaskInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DeviceTaskInfo_002")]
		public long ID
		{
			get { return _ID; }
			set { _ID = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_Code", NotEmpty = false, Length = 50, NotEmptyECode = "DeviceTaskInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DeviceTaskInfo_002")]
		public string Code
		{
			get { return _Code; }
			set { _Code = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_DisplayModelID", NotEmpty = false, Length = 1, NotEmptyECode = "DeviceTaskInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DeviceTaskInfo_002")]
		public int DisplayModelID
		{
			get { return _DisplayModelID; }
			set { _DisplayModelID = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_OperateTypeID", NotEmpty = false, Length = 1, NotEmptyECode = "DeviceTaskInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DeviceTaskInfo_002")]
		public int OperateTypeID
		{
			get { return _OperateTypeID; }
			set { _OperateTypeID = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_MessageTitle", NotEmpty = false, Length = 500, NotEmptyECode = "DeviceTaskInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DeviceTaskInfo_002")]
		public string MessageTitle
		{
			get { return _MessageTitle; }
			set { _MessageTitle = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_MessageTypeID", NotEmpty = false, Length = 1, NotEmptyECode = "DeviceTaskInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DeviceTaskInfo_002")]
		public int MessageTypeID
		{
			get { return _MessageTypeID; }
			set { _MessageTypeID = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_MessageSourceID", NotEmpty = false, Length = 1, NotEmptyECode = "DeviceTaskInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DeviceTaskInfo_002")]
		public int MessageSourceID
		{
			get { return _MessageSourceID; }
			set { _MessageSourceID = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_MessageContent", NotEmpty = false, Length = 0, NotEmptyECode = "DeviceTaskInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DeviceTaskInfo_002")]
        public MessageContent MessageContent
		{
			get { return _MessageContent; }
            set { _MessageContent = value; }
		}
        public MessageContent MessageContentAlias
        {
            get { return _MessageContentAlias; }
            set { _MessageContentAlias = value; }
        }
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_TargetRange", NotEmpty = false, Length = 0, NotEmptyECode = "DeviceTaskInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DeviceTaskInfo_002")]
		public string TargetRange
		{
			get { return _TargetRange; }
			set { _TargetRange = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_RargetAlias", NotEmpty = false, Length = 0, NotEmptyECode = "DeviceTaskInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DeviceTaskInfo_002")]
		public string RargetAlias
		{
			get { return _RargetAlias; }
			set { _RargetAlias = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_TaskBeginTime", NotEmpty = false, Length = 4, NotEmptyECode = "DeviceTaskInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DeviceTaskInfo_002")]
		public DateTime TaskBeginTime
		{
			get { return _TaskBeginTime; }
			set { _TaskBeginTime = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_TaskEndTime", NotEmpty = false, Length = 4, NotEmptyECode = "DeviceTaskInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DeviceTaskInfo_002")]
		public DateTime TaskEndTime
		{
			get { return _TaskEndTime; }
			set { _TaskEndTime = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_TaskPriorityID", NotEmpty = false, Length = 1, NotEmptyECode = "DeviceTaskInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DeviceTaskInfo_002")]
		public int TaskPriorityID
		{
			get { return _TaskPriorityID; }
			set { _TaskPriorityID = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_ImageSpanSecond", NotEmpty = false, Length = 1, NotEmptyECode = "DeviceTaskInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DeviceTaskInfo_002")]
		public int ImageSpanSecond
		{
			get { return _ImageSpanSecond; }
			set { _ImageSpanSecond = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_ImageEffectID", NotEmpty = false, Length = 1, NotEmptyECode = "DeviceTaskInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DeviceTaskInfo_002")]
		public int ImageEffectID
		{
			get { return _ImageEffectID; }
			set { _ImageEffectID = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_TaskTypeID", NotEmpty = false, Length = 1, NotEmptyECode = "DeviceTaskInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DeviceTaskInfo_002")]
		public int TaskTypeID
		{
			get { return _TaskTypeID; }
			set { _TaskTypeID = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_TaskStatusID", NotEmpty = false, Length = 1, NotEmptyECode = "DeviceTaskInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DeviceTaskInfo_002")]
		public int TaskStatusID
		{
			get { return _TaskStatusID; }
			set { _TaskStatusID = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_TaskResultID", NotEmpty = false, Length = 4, NotEmptyECode = "DeviceTaskInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DeviceTaskInfo_002")]
		public int TaskResultID
		{
			get { return _TaskResultID; }
			set { _TaskResultID = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_Note", NotEmpty = false, Length = 4000, NotEmptyECode = "DeviceTaskInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DeviceTaskInfo_002")]
		public string Note
		{
			get { return _Note; }
			set { _Note = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_CreatedID", NotEmpty = false, Length = 20, NotEmptyECode = "DeviceTaskInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DeviceTaskInfo_002")]
		public string CreatedID
		{
			get { return _CreatedID; }
			set { _CreatedID = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_CreatedName", NotEmpty = false, Length = 50, NotEmptyECode = "DeviceTaskInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DeviceTaskInfo_002")]
		public string CreatedName
		{
			get { return _CreatedName; }
			set { _CreatedName = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_CreatedDate", NotEmpty = false, Length = 4, NotEmptyECode = "DeviceTaskInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DeviceTaskInfo_002")]
		public DateTime CreatedDate
		{
			get { return _CreatedDate; }
			set { _CreatedDate = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_ModifiedID", NotEmpty = false, Length = 20, NotEmptyECode = "DeviceTaskInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DeviceTaskInfo_002")]
		public string ModifiedID
		{
			get { return _ModifiedID; }
			set { _ModifiedID = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_ModifiedName", NotEmpty = false, Length = 50, NotEmptyECode = "DeviceTaskInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DeviceTaskInfo_002")]
		public string ModifiedName
		{
			get { return _ModifiedName; }
			set { _ModifiedName = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_ModifiedDate", NotEmpty = false, Length = 4, NotEmptyECode = "DeviceTaskInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DeviceTaskInfo_002")]
		public DateTime ModifiedDate
		{
			get { return _ModifiedDate; }
			set { _ModifiedDate = value; }
		}        
    }
}