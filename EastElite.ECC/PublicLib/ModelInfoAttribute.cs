using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace PublicLib
{
    /// <summary>
    /// 实体属性信息类
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ModelInfoAttribute : Attribute
    {
        
        private string _name = string.Empty;
        /// <summary>
        /// 属性中文描述
        /// </summary>
        public virtual string Name 
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _controlname = string.Empty;
        /// <summary>
        /// 控件名称
        /// </summary>
        public virtual string ControlName
        {
            get { return _controlname; }
            set { _controlname = value; }
        }

        private bool _notempty = false;
        /// <summary>
        /// 是否为空
        /// </summary>
        public virtual bool NotEmpty 
        {
            get { return _notempty; }
            set { _notempty = value; }
        }

        private string _notemptyecode = string.Empty;
        /// <summary>
        /// 是否为空错误编码
        /// </summary>
        public virtual string NotEmptyECode
        {
            get { return _notemptyecode; }
            set { _notemptyecode = value; }
        }

        private RegularExpressions.RegExpType _rtype = RegularExpressions.RegExpType.Normal;
        /// <summary>
        /// 验证的数据类型
        /// </summary>
        public virtual RegularExpressions.RegExpType RType
        {
            get { return _rtype; }
            set { _rtype = value; }
        }

        private string _rtypeecode = string.Empty;
        /// <summary>
        /// 验证数据类型的错误编码
        /// </summary>
        public virtual string RTypeECode
        {
            get { return _rtypeecode; }
            set { _rtypeecode = value; }
        }

        private int _length = 0;
        /// <summary>
        /// 数据长度
        /// </summary>
        public virtual int Length 
        {
            get { return _length; }
            set { _length = value; }
        }
    }
}