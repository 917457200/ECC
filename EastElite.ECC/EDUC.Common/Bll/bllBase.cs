using System;
using System.Collections.Generic;
using System.Data;
using PublicLib;

namespace EDUC.Common.Bll
{
    /// <summary>
    /// 数据验证
    /// </summary>
    public class bllBase
    {
        public blloperatelog blllog = new blloperatelog();
        public DataTable dtBase = new DataTable();
        DataTable dtSensitiveWords = new DataTable();//敏感词数据

        public bllBase()
        {
            dtBase = new DataTable("error");
            dtBase.Columns.Add("type", typeof(string));
            dtBase.Columns.Add("mes", typeof(string));
            dtBase.Columns.Add("spanids", typeof(string));
            dtBase.AcceptChanges();
        }

        /// <summary>
        /// 关键字过滤
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string checkFilter(string str)
        {
            string rsStr = Helper.ReplaceString(str);
            try
            {
                if (dtSensitiveWords == null || dtSensitiveWords.Rows.Count <= 0)
                {
                    int recnums, pagenums;
                    dtSensitiveWords = new bllPaging().GetPagingInfo("SensitiveWords", "id", "*", 1, 1, "", "", "", out recnums, out pagenums);
                }
                string[] describe = dtSensitiveWords.Rows[0]["describe"].ToString().Split(',');
                for (int i = 0; i < describe.Length; i++)
                {
                    rsStr = rsStr.Replace(describe[i], "*");
                }
            }
            catch
            {
                return rsStr;
            }
            return rsStr;
        }

        /// <summary>
        /// List转string
        /// </summary>
        /// <param name="spans"></param>
        /// <returns></returns>
        public string ListTostring(List<string> spans)
        {
            string strReturn = string.Empty;
            foreach (string str in spans)
            {
                strReturn += str + "|";
            }
            return strReturn.TrimEnd('|');
        }

        public bool CheckControl(string strReturn, string spanids)
        {
            bool Flag = true;
            //数据验证
            if (strReturn.Length > 0)
            {
                Flag = false;
                DataRow DataVerify = dtBase.NewRow();
                DataVerify["type"] = "1";
                DataVerify["mes"] = strReturn;
                DataVerify["spanids"] = spanids;
                dtBase.Rows.Add(DataVerify);
            }
            return Flag;
        }

        /// <summary>
        /// 检测用户登录状态
        /// </summary>
        /// <param name="GUID"></param>
        /// <param name="UID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool CheckLogin(string GUID, string UID, string UserType)
        {
            bool Flag = true;
            dtBase = LoginUniqueness.LoginedCheckFromPage(GUID, UID, UserType);
            if (dtBase.Rows.Count > 0)//非法登录
            {
                Flag = false;
            }
            return Flag;
        }
        /// <summary>
        /// 检测用户登录状态
        /// </summary>
        /// <param name="GUID"></param>
        /// <param name="UID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool CheckLogin(string GUID, string UID)
        {
            bool Flag = true;
            dtBase = LoginUniqueness.LoginedCheckFromPage(GUID, UID);
            if (dtBase.Rows.Count > 0)//非法登录
            {
                Flag = false;
            }
            return Flag;
        }
    
        /// <summary>
        /// 检验表单数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="PropertyName">实体属性名称</param>
        /// <param name="value">待验证的属性值</param>
        /// <param name="errorCode">返回的错误List</param>
        /// <param name="t">实体对象</param>
        public void CheckValue<T>(List<string> EName, List<string> EValue, ref List<string> errorCode, ref List<string> ControlName, T t)
        {
            if (EName == null && EValue == null && t == null)
            {
                return;
            }
            if (EName.Count != EValue.Count)
            {
                return;
            }
            ModelInfoAttribute ModelInfo = new ModelInfoAttribute();
            System.Reflection.MemberInfo propInfo = null;
            ModelInfoAttribute myAttr;
            string PropertyName = string.Empty;
            for (int i = 0; i < EName.Count; i++)
            {
                PropertyName = EName[i];
                if (PropertyName.Trim().Length > 0)
                {
                    propInfo = t.GetType().GetProperty(PropertyName);
                    if (propInfo != null)
                    {
                        myAttr = (ModelInfoAttribute)Attribute.GetCustomAttribute(propInfo, typeof(ModelInfoAttribute));
                        if (myAttr != null)
                        {
                            ModelInfo.Length = myAttr.Length;
                            ModelInfo.Name = myAttr.Name;
                            ModelInfo.ControlName = myAttr.ControlName;
                            ModelInfo.NotEmpty = myAttr.NotEmpty;
                            ModelInfo.NotEmptyECode = myAttr.NotEmptyECode;
                            ModelInfo.RType = myAttr.RType;
                            ModelInfo.RTypeECode = myAttr.RTypeECode;
                            CheckValue(EValue[i], ModelInfo, ref errorCode, ref ControlName);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 验证数据
        /// </summary>
        /// <param name="value"></param>
        /// <param name="ModelInfo"></param>
        /// <param name="errorCode"></param>
        private void CheckValue(string value, ModelInfoAttribute ModelInfo, ref List<string> ErrorCode, ref List<string> ControlName)
        {
            if (value.Length == 0)
            {
                if (ModelInfo.NotEmpty)//不能为空
                {
                    ErrorCode.Add(ModelInfo.NotEmptyECode);
                    ControlName.Add(ModelInfo.ControlName + "_span");
                }
            }
            else
            {
                if (value.Length > ModelInfo.Length)//长度溢出
                {
                    ErrorCode.Add(ModelInfo.RTypeECode);
                    ControlName.Add(ModelInfo.ControlName + "_span");
                }
                else
                {
                    if (ModelInfo.RType != RegularExpressions.RegExpType.Normal)
                    {
                        if (!RegularExpressions.IsRegExpType(value, ModelInfo.RType))//格式不正确
                        {
                            ErrorCode.Add(ModelInfo.RTypeECode);
                            ControlName.Add(ModelInfo.ControlName + "_span");
                        }
                    }
                }
            }
        }
    }
}
