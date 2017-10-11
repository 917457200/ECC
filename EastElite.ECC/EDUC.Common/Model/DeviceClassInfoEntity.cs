using System;
using System.ComponentModel;
using PublicLib;

namespace EDUC.Common.Model
{
    [Description("")]
    [Serializable]
    public class DeviceClassInfoEntity
    {
		private long _ID = 0;
		private string _DeviceSN = string.Empty;
		private string _DeviceCode = string.Empty;
		private string _IPAddress = string.Empty;
		private int _IPPort = 0;
		private string _RoomNum = string.Empty;
		private string _ClassCode = string.Empty;
        private string _ClassFullCode = string.Empty;
		private string _ClassName = string.Empty;
		private string _ClassNickName = string.Empty;
		private string _ClassLogoPath = string.Empty;
		private string _ClassQRPath = string.Empty;
		private string _ClassSlogan = string.Empty;
		private string _SemesterName = string.Empty;
		private string _BanZhuRenCode = string.Empty;
		private string _BanZhuRenName = string.Empty;
		private string _BanZhuRenPhotoPath = string.Empty;
		private string _BanZhuRenQRPath = string.Empty;
		private string _ZuoYouMing = string.Empty;
		private string _Introduction = string.Empty;
		private string _Recommended = string.Empty;
		private int _SubjectTypeID = 0;
		private string _SubjectTypeIDText = string.Empty;
		private int _ClassTypeID = 0;
		private string _ClassTypeIDText = string.Empty;
		private string _OldDeviceSN = string.Empty;
		private int _DeviceStatusID = 0;
		private string _Attachment = string.Empty;
		private string _Note = string.Empty;
		private string _CreatedID = string.Empty;
		private string _CreatedName = string.Empty;
		private DateTime _CreatedDate = DateTime.Parse("1900-01-01");
		private string _ModifiedID = string.Empty;
		private string _ModifiedName = string.Empty;
		private DateTime _ModifiedDate = DateTime.Parse("1900-01-01");
        private string _JPushID = string.Empty;
        private int _DeviceTypeID = 0;
        private string _SchoolCode = string.Empty;
        private bool _IsValid = false;
        private string _Version = string.Empty;
        private string _OldClassName = string.Empty;
        private int _AsyncResultID = 0;
        private string _SPJPushID = string.Empty;
     

        /// <summary>
        ///
        /// <summary>

        public int AsyncResultID
        {
            get { return _AsyncResultID; }
            set { _AsyncResultID = value; }
        }
        /// <summary>
        ///
        /// <summary>

        public string OldClassName
        {
            get { return _OldClassName; }
            set { _OldClassName = value; }
        }
        /// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_Version", NotEmpty = false, Length = 50, NotEmptyECode = "DeviceClassInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DeviceClassInfo_002")]
		public string Version
		{
			get { return _Version; }
			set { _Version = value; }
		}
        /// <summary>
        ///
        /// <summary>
        [ModelInfo(Name = "", ControlName = "txt_IsValid", NotEmpty = false, Length = 1, NotEmptyECode = "DeviceClassInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DeviceClassInfo_002")]
        public bool IsValid
        {
            get { return _IsValid; }
            set { _IsValid = value; }
        }
        /// <summary>
        ///
        /// <summary>
        [ModelInfo(Name = "", ControlName = "txt_DeviceTypeID", NotEmpty = false, Length = 1, NotEmptyECode = "DeviceClassInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DeviceClassInfo_002")]
        public int DeviceTypeID
        {
            get { return _DeviceTypeID; }
            set { _DeviceTypeID = value; }
        }
        /// <summary>
        ///
        /// <summary>
        [ModelInfo(Name = "", ControlName = "txt_SchoolCode", NotEmpty = false, Length = 50, NotEmptyECode = "DeviceClassInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DeviceClassInfo_002")]
        public string SchoolCode
        {
            get { return _SchoolCode; }
            set { _SchoolCode = value; }
        }
        /// <summary>
        ///
        /// <summary>
        [ModelInfo(Name = "", ControlName = "txt_JPushID", NotEmpty = false, Length = 50, NotEmptyECode = "DeviceClassInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DeviceClassInfo_002")]
        public string JPushID
        {
            get { return _JPushID; }
            set { _JPushID = value; }
        }
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_ID", NotEmpty = false, Length = 8, NotEmptyECode = "DeviceClassInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DeviceClassInfo_002")]
		public long ID
		{
			get { return _ID; }
			set { _ID = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_DeviceSN", NotEmpty = false, Length = 50, NotEmptyECode = "DeviceClassInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DeviceClassInfo_002")]
		public string DeviceSN
		{
			get { return _DeviceSN; }
			set { _DeviceSN = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_DeviceCode", NotEmpty = false, Length = 50, NotEmptyECode = "DeviceClassInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DeviceClassInfo_002")]
		public string DeviceCode
		{
			get { return _DeviceCode; }
			set { _DeviceCode = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_IPAddress", NotEmpty = false, Length = 50, NotEmptyECode = "DeviceClassInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DeviceClassInfo_002")]
		public string IPAddress
		{
			get { return _IPAddress; }
			set { _IPAddress = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_IPPort", NotEmpty = false, Length = 4, NotEmptyECode = "DeviceClassInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DeviceClassInfo_002")]
		public int IPPort
		{
			get { return _IPPort; }
			set { _IPPort = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_RoomNum", NotEmpty = false, Length = 50, NotEmptyECode = "DeviceClassInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DeviceClassInfo_002")]
		public string RoomNum
		{
			get { return _RoomNum; }
			set { _RoomNum = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_ClassCode", NotEmpty = false, Length = 50, NotEmptyECode = "DeviceClassInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DeviceClassInfo_002")]
		public string ClassCode
		{
			get { return _ClassCode; }
			set { _ClassCode = value; }
		}
        public string ClassFullCode
        {
            get { return _ClassFullCode; }
            set { _ClassFullCode = value; }
        }
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_ClassName", NotEmpty = false, Length = 50, NotEmptyECode = "DeviceClassInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DeviceClassInfo_002")]
		public string ClassName
		{
			get { return _ClassName; }
			set { _ClassName = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_ClassNickName", NotEmpty = false, Length = 50, NotEmptyECode = "DeviceClassInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DeviceClassInfo_002")]
		public string ClassNickName
		{
			get { return _ClassNickName; }
			set { _ClassNickName = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_ClassLogoPath", NotEmpty = false, Length = 500, NotEmptyECode = "DeviceClassInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DeviceClassInfo_002")]
		public string ClassLogoPath
		{
			get { return _ClassLogoPath; }
			set { _ClassLogoPath = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_ClassQRPath", NotEmpty = false, Length = 500, NotEmptyECode = "DeviceClassInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DeviceClassInfo_002")]
		public string ClassQRPath
		{
			get { return _ClassQRPath; }
			set { _ClassQRPath = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_ClassSlogan", NotEmpty = false, Length = 500, NotEmptyECode = "DeviceClassInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DeviceClassInfo_002")]
		public string ClassSlogan
		{
			get { return _ClassSlogan; }
			set { _ClassSlogan = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_SemesterName", NotEmpty = false, Length = 50, NotEmptyECode = "DeviceClassInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DeviceClassInfo_002")]
		public string SemesterName
		{
			get { return _SemesterName; }
			set { _SemesterName = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_BanZhuRenCode", NotEmpty = false, Length = 20, NotEmptyECode = "DeviceClassInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DeviceClassInfo_002")]
		public string BanZhuRenCode
		{
			get { return _BanZhuRenCode; }
			set { _BanZhuRenCode = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_BanZhuRenName", NotEmpty = false, Length = 50, NotEmptyECode = "DeviceClassInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DeviceClassInfo_002")]
		public string BanZhuRenName
		{
			get { return _BanZhuRenName; }
			set { _BanZhuRenName = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_BanZhuRenPhotoPath", NotEmpty = false, Length = 500, NotEmptyECode = "DeviceClassInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DeviceClassInfo_002")]
		public string BanZhuRenPhotoPath
		{
			get { return _BanZhuRenPhotoPath; }
			set { _BanZhuRenPhotoPath = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_BanZhuRenQRPath", NotEmpty = false, Length = 500, NotEmptyECode = "DeviceClassInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DeviceClassInfo_002")]
		public string BanZhuRenQRPath
		{
			get { return _BanZhuRenQRPath; }
			set { _BanZhuRenQRPath = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_ZuoYouMing", NotEmpty = false, Length = 500, NotEmptyECode = "DeviceClassInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DeviceClassInfo_002")]
		public string ZuoYouMing
		{
			get { return _ZuoYouMing; }
			set { _ZuoYouMing = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_Introduction", NotEmpty = false, Length = 4000, NotEmptyECode = "DeviceClassInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DeviceClassInfo_002")]
		public string Introduction
		{
			get { return _Introduction; }
			set { _Introduction = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_Recommended", NotEmpty = false, Length = 4000, NotEmptyECode = "DeviceClassInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DeviceClassInfo_002")]
		public string Recommended
		{
			get { return _Recommended; }
			set { _Recommended = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_SubjectTypeID", NotEmpty = false, Length = 1, NotEmptyECode = "DeviceClassInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DeviceClassInfo_002")]
		public int SubjectTypeID
		{
			get { return _SubjectTypeID; }
			set { _SubjectTypeID = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_SubjectTypeIDText", NotEmpty = false, Length = 50, NotEmptyECode = "DeviceClassInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DeviceClassInfo_002")]
		public string SubjectTypeIDText
		{
			get { return _SubjectTypeIDText; }
			set { _SubjectTypeIDText = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_ClassTypeID", NotEmpty = false, Length = 1, NotEmptyECode = "DeviceClassInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DeviceClassInfo_002")]
		public int ClassTypeID
		{
			get { return _ClassTypeID; }
			set { _ClassTypeID = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_ClassTypeIDText", NotEmpty = false, Length = 50, NotEmptyECode = "DeviceClassInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DeviceClassInfo_002")]
		public string ClassTypeIDText
		{
			get { return _ClassTypeIDText; }
			set { _ClassTypeIDText = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_OldDeviceSN", NotEmpty = false, Length = 50, NotEmptyECode = "DeviceClassInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DeviceClassInfo_002")]
		public string OldDeviceSN
		{
			get { return _OldDeviceSN; }
			set { _OldDeviceSN = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_DeviceStatusID", NotEmpty = false, Length = 1, NotEmptyECode = "DeviceClassInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DeviceClassInfo_002")]
		public int DeviceStatusID
		{
			get { return _DeviceStatusID; }
			set { _DeviceStatusID = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_Attachment", NotEmpty = false, Length = 4000, NotEmptyECode = "DeviceClassInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DeviceClassInfo_002")]
		public string Attachment
		{
			get { return _Attachment; }
			set { _Attachment = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_Note", NotEmpty = false, Length = 4000, NotEmptyECode = "DeviceClassInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DeviceClassInfo_002")]
		public string Note
		{
			get { return _Note; }
			set { _Note = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_CreatedID", NotEmpty = false, Length = 20, NotEmptyECode = "DeviceClassInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DeviceClassInfo_002")]
		public string CreatedID
		{
			get { return _CreatedID; }
			set { _CreatedID = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_CreatedName", NotEmpty = false, Length = 50, NotEmptyECode = "DeviceClassInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DeviceClassInfo_002")]
		public string CreatedName
		{
			get { return _CreatedName; }
			set { _CreatedName = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_CreatedDate", NotEmpty = false, Length = 4, NotEmptyECode = "DeviceClassInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DeviceClassInfo_002")]
		public DateTime CreatedDate
		{
			get { return _CreatedDate; }
			set { _CreatedDate = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_ModifiedID", NotEmpty = false, Length = 20, NotEmptyECode = "DeviceClassInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DeviceClassInfo_002")]
		public string ModifiedID
		{
			get { return _ModifiedID; }
			set { _ModifiedID = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_ModifiedName", NotEmpty = false, Length = 50, NotEmptyECode = "DeviceClassInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DeviceClassInfo_002")]
		public string ModifiedName
		{
			get { return _ModifiedName; }
			set { _ModifiedName = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_ModifiedDate", NotEmpty = false, Length = 4, NotEmptyECode = "DeviceClassInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DeviceClassInfo_002")]
		public DateTime ModifiedDate
		{
			get { return _ModifiedDate; }
			set { _ModifiedDate = value; }
		}
        public string SPJPushID
        {
            get { return _SPJPushID; }
            set { _SPJPushID = value; }
        }
     
      
    }
}