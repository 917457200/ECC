using System;
using System.ComponentModel;
using cn.jpush.api.push.mode;
using PublicLib;

namespace EDUC.Common.Model
{
    [Description("")]
    [Serializable]
    public class ExaminationInfoEntity
    {
        private long _id = 0;
        private string _ClassCode = string.Empty;
        private string _VisibleTime = "1900-01-01";
        private string _HideTime = "1900-01-01";
        private string _ClassName = string.Empty;
        private string _ExamName = string.Empty;
        private string _ExamRoom = string.Empty;
        private string _ExamSubject = string.Empty;
        private string _ExamTime = string.Empty;
        private string _StudentNumberRange = string.Empty;
        private int _StudentNumber = 0;
        private string _Teachers = string.Empty;
        private string _Notice = string.Empty;
        private string _Note = string.Empty;
        private bool _IsValid = false;
        private DateTime _HandledDate = DateTime.Parse("1900-01-01");
        private string _HandledID = string.Empty;
        private string _Campus = string.Empty;

        /// <summary>
        ///
        /// <summary>
        [ModelInfo(Name = "", ControlName = "txt_id", NotEmpty = false, Length = 8, NotEmptyECode = "ExaminationInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "ExaminationInfo_002")]
        public long id
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        ///
        /// <summary>
        [ModelInfo(Name = "", ControlName = "txt_ClassCode", NotEmpty = false, Length = 50, NotEmptyECode = "ExaminationInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "ExaminationInfo_002")]
        public string ClassCode
        {
            get { return _ClassCode; }
            set { _ClassCode = value; }
        }
        /// <summary>
        ///
        /// <summary>
        [ModelInfo(Name = "", ControlName = "txt_VisibleTime", NotEmpty = false, Length = 4, NotEmptyECode = "ExaminationInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "ExaminationInfo_002")]
        public string VisibleTime
        {
            get { return _VisibleTime; }
            set { _VisibleTime = value; }
        }
        /// <summary>
        ///
        /// <summary>
        [ModelInfo(Name = "", ControlName = "txt_HideTime", NotEmpty = false, Length = 4, NotEmptyECode = "ExaminationInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "ExaminationInfo_002")]
        public string HideTime
        {
            get { return _HideTime; }
            set { _HideTime = value; }
        }
        /// <summary>
        ///
        /// <summary>
        [ModelInfo(Name = "", ControlName = "txt_ClassName", NotEmpty = false, Length = 50, NotEmptyECode = "ExaminationInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "ExaminationInfo_002")]
        public string ClassName
        {
            get { return _ClassName; }
            set { _ClassName = value; }
        }
        /// <summary>
        ///
        /// <summary>
        [ModelInfo(Name = "", ControlName = "txt_ExamName", NotEmpty = false, Length = 50, NotEmptyECode = "ExaminationInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "ExaminationInfo_002")]
        public string ExamName
        {
            get { return _ExamName; }
            set { _ExamName = value; }
        }
        /// <summary>
        ///
        /// <summary>
        [ModelInfo(Name = "", ControlName = "txt_ExamRoom", NotEmpty = false, Length = 50, NotEmptyECode = "ExaminationInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "ExaminationInfo_002")]
        public string ExamRoom
        {
            get { return _ExamRoom; }
            set { _ExamRoom = value; }
        }
        /// <summary>
        ///
        /// <summary>
        [ModelInfo(Name = "", ControlName = "txt_ExamSubject", NotEmpty = false, Length = 50, NotEmptyECode = "ExaminationInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "ExaminationInfo_002")]
        public string ExamSubject
        {
            get { return _ExamSubject; }
            set { _ExamSubject = value; }
        }
        /// <summary>
        ///
        /// <summary>
        [ModelInfo(Name = "", ControlName = "txt_ExamTime", NotEmpty = false, Length = 50, NotEmptyECode = "ExaminationInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "ExaminationInfo_002")]
        public string ExamTime
        {
            get { return _ExamTime; }
            set { _ExamTime = value; }
        }
        /// <summary>
        ///
        /// <summary>
        [ModelInfo(Name = "", ControlName = "txt_StudentNumberRange", NotEmpty = false, Length = 50, NotEmptyECode = "ExaminationInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "ExaminationInfo_002")]
        public string StudentNumberRange
        {
            get { return _StudentNumberRange; }
            set { _StudentNumberRange = value; }
        }
        /// <summary>
        ///
        /// <summary>
        [ModelInfo(Name = "", ControlName = "txt_StudentNumber", NotEmpty = false, Length = 4, NotEmptyECode = "ExaminationInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "ExaminationInfo_002")]
        public int StudentNumber
        {
            get { return _StudentNumber; }
            set { _StudentNumber = value; }
        }
        /// <summary>
        ///
        /// <summary>
        [ModelInfo(Name = "", ControlName = "txt_Teachers", NotEmpty = false, Length = 50, NotEmptyECode = "ExaminationInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "ExaminationInfo_002")]
        public string Teachers
        {
            get { return _Teachers; }
            set { _Teachers = value; }
        }
        /// <summary>
        ///
        /// <summary>
        [ModelInfo(Name = "", ControlName = "txt_Notice", NotEmpty = false, Length = 0, NotEmptyECode = "ExaminationInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "ExaminationInfo_002")]
        public string Notice
        {
            get { return _Notice; }
            set { _Notice = value; }
        }
        /// <summary>
        ///
        /// <summary>
        [ModelInfo(Name = "", ControlName = "txt_Note", NotEmpty = false, Length = 0, NotEmptyECode = "ExaminationInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "ExaminationInfo_002")]
        public string Note
        {
            get { return _Note; }
            set { _Note = value; }
        }
        /// <summary>
        ///
        /// <summary>
        [ModelInfo(Name = "", ControlName = "txt_IsValid", NotEmpty = false, Length = 1, NotEmptyECode = "ExaminationInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "ExaminationInfo_002")]
        public bool IsValid
        {
            get { return _IsValid; }
            set { _IsValid = value; }
        }
        /// <summary>
        ///
        /// <summary>
        [ModelInfo(Name = "", ControlName = "txt_HandledDate", NotEmpty = false, Length = 4, NotEmptyECode = "ExaminationInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "ExaminationInfo_002")]
        public DateTime HandledDate
        {
            get { return _HandledDate; }
            set { _HandledDate = value; }
        }
        /// <summary>
        ///
        /// <summary>
        [ModelInfo(Name = "", ControlName = "txt_HandledID", NotEmpty = false, Length = 20, NotEmptyECode = "ExaminationInfo_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "ExaminationInfo_002")]
        public string HandledID
        {
            get { return _HandledID; }
            set { _HandledID = value; }
        }
        public string Campus
        {
            get { return _Campus; }
            set { _Campus = value; }
        }
    }
}