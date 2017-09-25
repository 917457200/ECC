using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace PublicLib
{
    /// <summary>
    /// ʵ��������Ϣ��
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ModelInfoAttribute : Attribute
    {
        
        private string _name = string.Empty;
        /// <summary>
        /// ������������
        /// </summary>
        public virtual string Name 
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _controlname = string.Empty;
        /// <summary>
        /// �ؼ�����
        /// </summary>
        public virtual string ControlName
        {
            get { return _controlname; }
            set { _controlname = value; }
        }

        private bool _notempty = false;
        /// <summary>
        /// �Ƿ�Ϊ��
        /// </summary>
        public virtual bool NotEmpty 
        {
            get { return _notempty; }
            set { _notempty = value; }
        }

        private string _notemptyecode = string.Empty;
        /// <summary>
        /// �Ƿ�Ϊ�մ������
        /// </summary>
        public virtual string NotEmptyECode
        {
            get { return _notemptyecode; }
            set { _notemptyecode = value; }
        }

        private RegularExpressions.RegExpType _rtype = RegularExpressions.RegExpType.Normal;
        /// <summary>
        /// ��֤����������
        /// </summary>
        public virtual RegularExpressions.RegExpType RType
        {
            get { return _rtype; }
            set { _rtype = value; }
        }

        private string _rtypeecode = string.Empty;
        /// <summary>
        /// ��֤�������͵Ĵ������
        /// </summary>
        public virtual string RTypeECode
        {
            get { return _rtypeecode; }
            set { _rtypeecode = value; }
        }

        private int _length = 0;
        /// <summary>
        /// ���ݳ���
        /// </summary>
        public virtual int Length 
        {
            get { return _length; }
            set { _length = value; }
        }
    }
}