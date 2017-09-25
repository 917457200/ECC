using System;
using System.ComponentModel;
using PublicLib;

namespace EDUC.Common.Model
{
    [Description("")]
    [Serializable]
    public class DictEntity
    {
		private long _ID = 0;
		private string _ItemName = string.Empty;
		private int _ItemKey = 0;
		private string _ItemValue = string.Empty;
		private int _ItemOrder = 0;
		private bool _IsValid = false;
		private string _Note = string.Empty;

		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_ID", NotEmpty = false, Length = 8, NotEmptyECode = "Dict_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "Dict_002")]
		public long ID
		{
			get { return _ID; }
			set { _ID = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_ItemName", NotEmpty = false, Length = 50, NotEmptyECode = "Dict_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "Dict_002")]
		public string ItemName
		{
			get { return _ItemName; }
			set { _ItemName = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_ItemKey", NotEmpty = false, Length = 4, NotEmptyECode = "Dict_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "Dict_002")]
		public int ItemKey
		{
			get { return _ItemKey; }
			set { _ItemKey = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_ItemValue", NotEmpty = false, Length = 50, NotEmptyECode = "Dict_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "Dict_002")]
		public string ItemValue
		{
			get { return _ItemValue; }
			set { _ItemValue = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_ItemOrder", NotEmpty = false, Length = 4, NotEmptyECode = "Dict_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "Dict_002")]
		public int ItemOrder
		{
			get { return _ItemOrder; }
			set { _ItemOrder = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_IsValid", NotEmpty = false, Length = 1, NotEmptyECode = "Dict_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "Dict_002")]
		public bool IsValid
		{
			get { return _IsValid; }
			set { _IsValid = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_Note", NotEmpty = false, Length = 500, NotEmptyECode = "Dict_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "Dict_002")]
		public string Note
		{
			get { return _Note; }
			set { _Note = value; }
		}        
    }
}