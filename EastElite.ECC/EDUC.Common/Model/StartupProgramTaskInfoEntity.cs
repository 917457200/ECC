using System;
using System.ComponentModel;
using PublicLib;

namespace EDUC.Common.Model
{
    [Description("")]
    [Serializable]
    public class StartupProgramTaskInfoEntity
    {
		private long _ID = 0;
		private string _Code = string.Empty;
		private int _OperateTypeID = 0;
		private int _MessageSourceID = 0;
		private string _TargetRange = string.Empty;
		private string _Tag = string.Empty;
		private string _Tag_and = string.Empty;
		private string _Alias = string.Empty;
		private string _Registration_ID = string.Empty;
		private string _RargetAlias = string.Empty;
		private string _CreatedID = string.Empty;
		private string _CreatedName = string.Empty;
        private DateTime _CreatedDate = DateTime.Now;
        private string _MessageContent = string.Empty;
        private string _Note = string.Empty;
        private int _TaskStatusID = 0;

		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_ID", NotEmpty = false, Length = 8, NotEmptyECode = "StartupProgramTaskInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "StartupProgramTaskInfo_002")]
		public long ID
		{
			get { return _ID; }
			set { _ID = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_Code", NotEmpty = false, Length = 50, NotEmptyECode = "StartupProgramTaskInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "StartupProgramTaskInfo_002")]
		public string Code
		{
			get { return _Code; }
			set { _Code = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_OperateTypeID", NotEmpty = false, Length = 1, NotEmptyECode = "StartupProgramTaskInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "StartupProgramTaskInfo_002")]
		public int OperateTypeID
		{
			get { return _OperateTypeID; }
			set { _OperateTypeID = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_MessageSourceID", NotEmpty = false, Length = 1, NotEmptyECode = "StartupProgramTaskInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "StartupProgramTaskInfo_002")]
		public int MessageSourceID
		{
			get { return _MessageSourceID; }
			set { _MessageSourceID = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_TargetRange", NotEmpty = false, Length = 0, NotEmptyECode = "StartupProgramTaskInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "StartupProgramTaskInfo_002")]
		public string TargetRange
		{
			get { return _TargetRange; }
			set { _TargetRange = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_Tag", NotEmpty = false, Length = 0, NotEmptyECode = "StartupProgramTaskInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "StartupProgramTaskInfo_002")]
		public string Tag
		{
			get { return _Tag; }
			set { _Tag = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_Tag_and", NotEmpty = false, Length = 0, NotEmptyECode = "StartupProgramTaskInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "StartupProgramTaskInfo_002")]
		public string Tag_and
		{
			get { return _Tag_and; }
			set { _Tag_and = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_Alias", NotEmpty = false, Length = 0, NotEmptyECode = "StartupProgramTaskInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "StartupProgramTaskInfo_002")]
		public string Alias
		{
			get { return _Alias; }
			set { _Alias = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_Registration_ID", NotEmpty = false, Length = 0, NotEmptyECode = "StartupProgramTaskInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "StartupProgramTaskInfo_002")]
		public string Registration_ID
		{
			get { return _Registration_ID; }
			set { _Registration_ID = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_RargetAlias", NotEmpty = false, Length = 0, NotEmptyECode = "StartupProgramTaskInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "StartupProgramTaskInfo_002")]
		public string RargetAlias
		{
			get { return _RargetAlias; }
			set { _RargetAlias = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_CreatedID", NotEmpty = false, Length = 20, NotEmptyECode = "StartupProgramTaskInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "StartupProgramTaskInfo_002")]
		public string CreatedID
		{
			get { return _CreatedID; }
			set { _CreatedID = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_CreatedName", NotEmpty = false, Length = 50, NotEmptyECode = "StartupProgramTaskInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "StartupProgramTaskInfo_002")]
		public string CreatedName
		{
			get { return _CreatedName; }
			set { _CreatedName = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_CreatedDate", NotEmpty = false, Length = 4, NotEmptyECode = "StartupProgramTaskInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "StartupProgramTaskInfo_002")]
		public DateTime CreatedDate
		{
			get { return _CreatedDate; }
			set { _CreatedDate = value; }
		}
        public string MessageContent
        {
            get { return _MessageContent; }
            set { _MessageContent = value; }
        }
        public string Note
        {
            get { return _Note; }
            set { _Note = value; }
        }
        public int TaskStatusID
        {
            get { return _TaskStatusID; }
            set { _TaskStatusID = value; }
        }
    }
}