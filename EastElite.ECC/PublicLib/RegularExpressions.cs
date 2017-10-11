using System;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace PublicLib
{
    /// <summary>
    /// 创建人：
    /// 时间：
    /// 功能描述：常用数据验证
    /// </summary>
    [Description("常用数据验证")]
    [Serializable]
    public sealed class RegularExpressions
    {
        /// <summary>
        /// 验证类型
        /// </summary>
        public enum RegExpType
        {
            //Normal
            [EnumAttribute(Name = "整型")]
            Normal = 0,
            //整型
            [EnumAttribute(Name = "整型")]
            Int = 1,

            //金额
            [EnumAttribute(Name = "金额")]
            Money = 2,

            //浮点型
            [EnumAttribute(Name = "浮点型")]
            Float = 3,

            //中国邮政编码
            [EnumAttribute(Name = "中国邮政编码")]
            Postcode = 4,

            //中国手机号码
            [EnumAttribute(Name = "中国手机号码")]
            Mobile = 5,

            //用户名(长度为3-20位,必须以字母开头,可以包含_.)
            [EnumAttribute(Name = "用户名格式")]
            Username = 6,

            //密码(不包含*,&,<,>,,",',空格)
            [EnumAttribute(Name = "密码格式")]
            Password = 7,

            //电子邮件
            [EnumAttribute(Name = "邮箱格式")]
            Email = 8,

            //中文汉字
            [EnumAttribute(Name = "中文汉字")]
            Chinese = 9,

            //英文字母
            [EnumAttribute(Name = "英文字母")]
            English = 10,

            //身份证号码
            [EnumAttribute(Name = "身份证号码")]
            ChineseID = 11,

            //网站地址
            [EnumAttribute(Name = "网站地址")]
            URL = 12,

            //IP地址
            [EnumAttribute(Name = "IP地址")]
            IPAddress = 13,

            //中国电话号码
            [EnumAttribute(Name = "中国电话号码")]
            Tel = 14,
            //金额Decimal
            [EnumAttribute(Name = "金额Decimal")]
            Decimal = 15,

            [EnumAttribute(Name = "昵称(汉字或字母)")]
            NickName = 16
        }

        public static bool IsRegExpType(string strVerifyString, RegExpType RType)
        {
            string strRegExp = String.Empty;

            switch (RType)
            {
                case RegExpType.Int:
                    strRegExp = "^(-?[0-9])*$";
                    break;
                case RegExpType.Money:
                    strRegExp = "^(-?[0-9]{1,})(.[0-9]{1,2})?$";
                    break;
                case RegExpType.Float:
                    strRegExp = "^(-?[0-9]{1,})(.[0-9]{1,4})?$";
                    break;
                case RegExpType.Postcode:
                    strRegExp = "^([0-9]){6}$";
                    break;
                case RegExpType.Mobile:
                    strRegExp = "^1([0-9]){10}$";
                    break;
                case RegExpType.Username:
                    strRegExp = "^([a-zA-Z]{1}[a-zA-Z0-9_.]{2,19})$";
                    break;
                case RegExpType.Password:
                    strRegExp = "^([^*&<>\"\'\\s]){6,10}$";
                    break;
                case RegExpType.Email:
                    strRegExp = @"^\w+([-+./]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
                    break;
                case RegExpType.Chinese:
                    strRegExp = "^[\u4e00-\u9fa5]{1,}*";
                    break;
                case RegExpType.English:
                    strRegExp = "^[A-Za-z]*$";
                    break;
                case RegExpType.ChineseID:
                    strRegExp = "^([0-9]{15}|[0-9]{17}[xX0-9]{1})$";
                    break;
                case RegExpType.URL:
                    strRegExp = @"http://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?";
                    break;
                case RegExpType.IPAddress:
                    strRegExp = @"^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$";
                    break;
                case RegExpType.Tel:
                    //strRegExp = @"(\d{3,4})-?\d{7,8}";
                    //strRegExp = @"(^(\d{3,4}-)?\d{7,8})|(1[0-9]{10})$";
                    strRegExp = @"^\d{3,4}[-]?\d{7,8}$";
                    break;
                case RegExpType.Decimal:
                    strRegExp = @"^(([1-9]{1}\d*)|([0]{1}))(\.(\d){1,2})?$";
                    break;
                case RegExpType.NickName:
                    strRegExp = @"^([a-zA-Z\u4e00-\u9fa5]{1}[a-zA-Z\u4e00-\u9fa50-9]{2,7})$";
                    break;
                case RegExpType.Normal:
                    return true;
                    break;
            }
            Regex Reg = new Regex(strRegExp);
            Match Mat = Reg.Match(strVerifyString);
            if (Mat.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string[] PROVINCE_CODE = {
            null, null, null, null, null, null, null, null, null, null, null,
            "北京", "天津", "河北", "山西", "内蒙古", null, null, null, null, null,
            "辽宁", "吉林", "黑龙江", null, null, null, null, null, null, null,
            "上海", "江苏", "浙江", "安微", "福建", "江西", "山东", null, null, null,
            "河南", "湖北", "湖南", "广东", "广西", "海南", null, null, null,
            "重庆", "四川", "贵州", "云南", "西藏", null, null, null, null, null, null,
            "陕西", "甘肃", "青海", "宁夏", "新疆", null, null, null, null, null,
            "台湾", null, null, null, null, null, null, null, null, null,
            "香港", "澳门", null, null, null, null, null, null, null, null,
            "国外"};

        public static bool CheckChinaIDInfo(string ACidCode)
        {
            if (string.IsNullOrEmpty(ACidCode)) return false; // 空
            if (ACidCode.Trim().Length != 18) return false; // 身份证位数不对
            for (int i = 0; i < 17; i++)
                if ("0123456789".IndexOf(ACidCode[i]) < 0) return false; // 前17位必须数字
            if ("0123456789Xx".IndexOf(ACidCode[17]) < 0) return false; // 最后一位必须数字或X
            int vProvince = int.Parse(ACidCode.Substring(0, 2));
            if (vProvince > PROVINCE_CODE.Length ||
                string.IsNullOrEmpty(PROVINCE_CODE[vProvince])) return false; // 地址码错误
            DateTime vBirthday;
            if (!DateTime.TryParseExact(ACidCode.Substring(6, 8), "yyyyMMdd", null,
                System.Globalization.DateTimeStyles.None, out vBirthday)) return false; // 日期格式错误
            if (vBirthday > DateTime.Now) return false; // 未来的人？？？
            if (vBirthday < new DateTime(1900, 10, 01)) return false; // 还没建国
            int T = 0;
            for (int i = 0; i < 18; i++)
            {
                int j = "xX".IndexOf(ACidCode[i]) < 0 ? ACidCode[i] - '0' : 10;
                T += (int)Math.Pow(2, 17 - i) % 11 * j;
            }
            if (T % 11 != 1) return false; // 验证码错误
            return true;
        }


        #region 中国身份证号码验证
        private static bool CheckCidInfo(string cid)
        {
            if (cid.Length == 18)
            {
                int iS = 0;
                //加权因子常数
                int[] iW = new int[] { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2 };
                //校验码常数
                string LastCode = "10X98765432";

                //进行加权求和
                for (int i = 0; i < 17; i++)
                {
                    iS += int.Parse(cid.Substring(i, 1)) * iW[i];
                }
                //取模运算，得到模值
                int iY = iS % 11;
                //从LastCode中取得以模为索引号的值，加到身份证的最后一位，即为新身份证号。 
                string strlast = LastCode.Substring(iY, 1);
                if (strlast != cid.Substring(17).ToUpper())
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            {
                return false;
            }
        }
        #endregion

        #region 身份证号码15升级为18位
        private string ChineseIDUpgrade(string ShortCid)
        {
            char[] strJiaoYan = { '1', '0', 'X', '9', '8', '7', '6', '5', '4', '3', '2' };
            int[] intQuan = { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2, 1 };
            string strTemp;
            int intTemp = 0;

            strTemp = ShortCid.Substring(0, 6) + "19" + ShortCid.Substring(6);
            for (int i = 0; i <= strTemp.Length - 1; i++)
            {
                intTemp += int.Parse(strTemp.Substring(i, 1)) * intQuan[i];
            }
            intTemp = intTemp % 11;
            return strTemp + strJiaoYan[intTemp];
        }
        #endregion
    }
}
