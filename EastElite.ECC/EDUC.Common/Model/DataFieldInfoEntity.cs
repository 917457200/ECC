using System;
using System.ComponentModel;
using PublicLib;

namespace EDUC.Common.Model
{
    [Description("")]
    [Serializable]
    public class DataFieldInfoEntity
    {
		private long _ID = 0;
		private string _FieldCode = string.Empty;
		private int _FieldTypeID = 0;
		private int _FieldSerialID = 0;
		private string _FieldName1 = string.Empty;
		private string _FieldName2 = string.Empty;
		private string _FieldName3 = string.Empty;
		private string _FieldName4 = string.Empty;
		private string _FieldName5 = string.Empty;
		private string _FieldName6 = string.Empty;
		private int _FieldNum1 = 0;
		private int _FieldNum2 = 0;
		private string _FieldContent = string.Empty;
		private string _Attachment = string.Empty;
		private bool _IsValid = false;
		private string _TokenKey = string.Empty;
		private string _CreatedID = string.Empty;
		private DateTime _CreatedDate = DateTime.Parse("1900-01-01");
		private string _ModifiedID = string.Empty;
		private DateTime _ModifiedDate = DateTime.Parse("1900-01-01");

		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_ID", NotEmpty = false, Length = 8, NotEmptyECode = "DataFieldInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DataFieldInfo_002")]
		public long ID
		{
			get { return _ID; }
			set { _ID = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_FieldCode", NotEmpty = false, Length = 50, NotEmptyECode = "DataFieldInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DataFieldInfo_002")]
		public string FieldCode
		{
			get { return _FieldCode; }
			set { _FieldCode = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_FieldTypeID", NotEmpty = false, Length = 4, NotEmptyECode = "DataFieldInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DataFieldInfo_002")]
		public int FieldTypeID
		{
			get { return _FieldTypeID; }
			set { _FieldTypeID = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_FieldSerialID", NotEmpty = false, Length = 4, NotEmptyECode = "DataFieldInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DataFieldInfo_002")]
		public int FieldSerialID
		{
			get { return _FieldSerialID; }
			set { _FieldSerialID = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_FieldName1", NotEmpty = false, Length = 1000, NotEmptyECode = "DataFieldInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DataFieldInfo_002")]
		public string FieldName1
		{
			get { return _FieldName1; }
			set { _FieldName1 = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_FieldName2", NotEmpty = false, Length = 1000, NotEmptyECode = "DataFieldInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DataFieldInfo_002")]
		public string FieldName2
		{
			get { return _FieldName2; }
			set { _FieldName2 = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_FieldName3", NotEmpty = false, Length = 1000, NotEmptyECode = "DataFieldInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DataFieldInfo_002")]
		public string FieldName3
		{
			get { return _FieldName3; }
			set { _FieldName3 = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_FieldName4", NotEmpty = false, Length = 1000, NotEmptyECode = "DataFieldInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DataFieldInfo_002")]
		public string FieldName4
		{
			get { return _FieldName4; }
			set { _FieldName4 = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_FieldName5", NotEmpty = false, Length = 1000, NotEmptyECode = "DataFieldInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DataFieldInfo_002")]
		public string FieldName5
		{
			get { return _FieldName5; }
			set { _FieldName5 = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_FieldName6", NotEmpty = false, Length = 1000, NotEmptyECode = "DataFieldInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DataFieldInfo_002")]
		public string FieldName6
		{
			get { return _FieldName6; }
			set { _FieldName6 = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_FieldNum1", NotEmpty = false, Length = 4, NotEmptyECode = "DataFieldInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DataFieldInfo_002")]
		public int FieldNum1
		{
			get { return _FieldNum1; }
			set { _FieldNum1 = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_FieldNum2", NotEmpty = false, Length = 4, NotEmptyECode = "DataFieldInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DataFieldInfo_002")]
		public int FieldNum2
		{
			get { return _FieldNum2; }
			set { _FieldNum2 = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_FieldContent", NotEmpty = false, Length = 0, NotEmptyECode = "DataFieldInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DataFieldInfo_002")]
		public string FieldContent
		{
			get { return _FieldContent; }
			set { _FieldContent = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_Attachment", NotEmpty = false, Length = 0, NotEmptyECode = "DataFieldInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DataFieldInfo_002")]
		public string Attachment
		{
			get { return _Attachment; }
			set { _Attachment = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_IsValid", NotEmpty = false, Length = 1, NotEmptyECode = "DataFieldInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DataFieldInfo_002")]
		public bool IsValid
		{
			get { return _IsValid; }
			set { _IsValid = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_TokenKey", NotEmpty = false, Length = 20, NotEmptyECode = "DataFieldInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DataFieldInfo_002")]
		public string TokenKey
		{
			get { return _TokenKey; }
			set { _TokenKey = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_CreatedID", NotEmpty = false, Length = 20, NotEmptyECode = "DataFieldInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DataFieldInfo_002")]
		public string CreatedID
		{
			get { return _CreatedID; }
			set { _CreatedID = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_CreatedDate", NotEmpty = false, Length = 4, NotEmptyECode = "DataFieldInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DataFieldInfo_002")]
		public DateTime CreatedDate
		{
			get { return _CreatedDate; }
			set { _CreatedDate = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_ModifiedID", NotEmpty = false, Length = 20, NotEmptyECode = "DataFieldInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DataFieldInfo_002")]
		public string ModifiedID
		{
			get { return _ModifiedID; }
			set { _ModifiedID = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_ModifiedDate", NotEmpty = false, Length = 4, NotEmptyECode = "DataFieldInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DataFieldInfo_002")]
		public DateTime ModifiedDate
		{
			get { return _ModifiedDate; }
			set { _ModifiedDate = value; }
		}        
    }
}