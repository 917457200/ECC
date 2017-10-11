using System;
using System.ComponentModel;
using PublicLib;


namespace EDUC.Common.Model
{
    [Description("")]
    [Serializable]
    public class UserClassInfoEntity
    {
		private long _ID = 0;
		private string _UserCode = string.Empty;
		private int _UserType = 0;
		private string _ClassCode = string.Empty;
		private string _ClassFullCode = string.Empty;
		private string _ClassName = string.Empty;
		private int _RoleType = 0;
		private bool _IsValid = false;
		private string _Note = string.Empty;
		private string _HandledID = string.Empty;
		private string _HandledName = string.Empty;
		private DateTime _HandledDate = DateTime.Parse("1900-01-01");

		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_ID", NotEmpty = false, Length = 8, NotEmptyECode = "UserClassInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "UserClassInfo_002")]
		public long ID
		{
			get { return _ID; }
			set { _ID = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_UserCode", NotEmpty = false, Length = 20, NotEmptyECode = "UserClassInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "UserClassInfo_002")]
		public string UserCode
		{
			get { return _UserCode; }
			set { _UserCode = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_UserType", NotEmpty = false, Length = 1, NotEmptyECode = "UserClassInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "UserClassInfo_002")]
		public int UserType
		{
			get { return _UserType; }
			set { _UserType = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_ClassCode", NotEmpty = false, Length = 50, NotEmptyECode = "UserClassInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "UserClassInfo_002")]
		public string ClassCode
		{
			get { return _ClassCode; }
			set { _ClassCode = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_ClassFullCode", NotEmpty = false, Length = 50, NotEmptyECode = "UserClassInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "UserClassInfo_002")]
		public string ClassFullCode
		{
			get { return _ClassFullCode; }
			set { _ClassFullCode = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_ClassName", NotEmpty = false, Length = 50, NotEmptyECode = "UserClassInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "UserClassInfo_002")]
		public string ClassName
		{
			get { return _ClassName; }
			set { _ClassName = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_RoleType", NotEmpty = false, Length = 1, NotEmptyECode = "UserClassInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "UserClassInfo_002")]
		public int RoleType
		{
			get { return _RoleType; }
			set { _RoleType = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_IsValid", NotEmpty = false, Length = 1, NotEmptyECode = "UserClassInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "UserClassInfo_002")]
		public bool IsValid
		{
			get { return _IsValid; }
			set { _IsValid = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_Note", NotEmpty = false, Length = 500, NotEmptyECode = "UserClassInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "UserClassInfo_002")]
		public string Note
		{
			get { return _Note; }
			set { _Note = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_HandledID", NotEmpty = false, Length = 20, NotEmptyECode = "UserClassInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "UserClassInfo_002")]
		public string HandledID
		{
			get { return _HandledID; }
			set { _HandledID = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_HandledName", NotEmpty = false, Length = 50, NotEmptyECode = "UserClassInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "UserClassInfo_002")]
		public string HandledName
		{
			get { return _HandledName; }
			set { _HandledName = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_HandledDate", NotEmpty = false, Length = 4, NotEmptyECode = "UserClassInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "UserClassInfo_002")]
		public DateTime HandledDate
		{
			get { return _HandledDate; }
			set { _HandledDate = value; }
		}        
    }
}