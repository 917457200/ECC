using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicLib
{
    public enum EnumSearchType
    {
        /// <summary>
        /// 起始字符模糊查找
        /// </summary>
        [Description("起始字符模糊查找")]
        AndLikeS,

        /// <summary>
        /// 结尾字符模糊查找
        /// </summary>
        [Description("结尾字符模糊查找")]
        AndLikeE,

        /// <summary>
        /// 全文模糊查找
        /// </summary>
        [Description("全文模糊查找")]
        AndLike,
        /// <summary>
        /// 等于一个字符串
        /// </summary>
        [Description("相等:用于字符串")]
        AndString,
        /// <summary>
        /// 等于一个数字
        /// </summary>
        [Description("等于一个数字")]
        AndInt,
        /// <summary>
        /// 不等于一个数字
        /// </summary>
        [Description("等于一个数字")]
        AndNotInt,
        /// <summary>
        /// 大于等于开始时间
        /// </summary>
        [Description("大于等于开始时间")]
        AndSTime,
        /// <summary>
        /// 小于等于结束时间
        /// </summary>
        [Description("小于等于结束时间")]
        AndETime,
      

    }
}
