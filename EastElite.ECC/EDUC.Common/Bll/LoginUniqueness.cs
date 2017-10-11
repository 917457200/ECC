using System;
using System.Collections;
using System.Data;
using PublicLib;


namespace EDUC.Common.Bll
{
    /// <summary>
    /// 单点登录操作
    /// </summary>
    public class LoginUniqueness : bllBase
    {
        /// <summary>
        /// 判断用户登录是否合法性
        /// </summary>
        /// <param name="GUID">唯一标识</param>
        /// <param name="UserID">用户ID</param>
        /// <returns>返回：""空字符合法，否则非法</returns>
        public static DataTable LoginedCheckFromPage(string GUID, string UserID, string userType)
        {
            DataTable dt = new DataTable("error");
            dt.Columns.Add("type", typeof(string));
            dt.Columns.Add("mes", typeof(string));
            dt.Columns.Add("spanids", typeof(string));
            dt.AcceptChanges();
            if (UserID != "0")
            {
                //验证用户合法性
                if (!LoginedCheck(GUID, UserID, userType))
                {
                    DataRow LoginVerify = dt.NewRow();
                    LoginVerify["type"] = "-1";
                    LoginVerify["mes"] = "用户已在其他位置登录，请重新登录！";
                    dt.Rows.Add(LoginVerify);
                }
            }
            return dt;
        }
        public static DataTable LoginedCheckFromPage(string GUID, string UserID)
        {
            DataTable dt = new DataTable("error");
            dt.Columns.Add("type", typeof(string));
            dt.Columns.Add("mes", typeof(string));
            dt.Columns.Add("spanids", typeof(string));
            dt.AcceptChanges();
            if (UserID != "0")
            {
                //验证用户合法性
                if (!LoginedCheck(GUID, UserID))
                {
                    DataRow LoginVerify = dt.NewRow();
                    LoginVerify["type"] = "-1";
                    LoginVerify["mes"] = "用户已在其他位置登录，请重新登录！";
                    dt.Rows.Add(LoginVerify);
                }
            }
            return dt;
        }

        /// <summary>
        /// 登录身份验证
        /// </summary>
        /// <param name="GUID">登录GUID</param>
        /// <param name="UserID">用户ID</param>
        /// <returns>返回是否合法</returns>
        public static bool LoginedCheck(string GUID, string UserID, string userType)
        {
            bool Flag = false;
            //Hashtable hOnline = MemCached.GetCache<Hashtable>("LoginOnline");
            Hashtable hOnline = (Hashtable)WebCache.GetCache("LoginOnline");
            if (hOnline != null)
            {
                object Val = hOnline[UserID + "-" + userType];
                if (Val != null && Val.ToString() == GUID)
                {
                    Flag = true;
                }
            }
            return Flag;
        }

        /// <summary>
        /// 登录身份验证
        /// </summary>
        /// <param name="GUID">登录GUID</param>
        /// <param name="UserID">用户ID</param>
        /// <returns>返回是否合法</returns>
        public static bool LoginedCheck(string GUID, string UserID)
        {
            bool Flag = false;
            //Hashtable hOnline = MemCached.GetCache<Hashtable>("LoginOnline");
            Hashtable hOnline = (Hashtable)WebCache.GetCache("LoginOnline");
            if (hOnline != null)
            {
                object Val = hOnline[UserID];
                if (Val != null && Val.ToString() == GUID)
                {
                    Flag = true;
                }
            }
            return Flag;
        }
        /// <summary>
        /// 获取在线人数
        /// </summary>
        /// <returns></returns>
        public static int GetOnlinePerson()
        {
            int Flag = 0;
            //Hashtable hOnline = MemCached.GetCache<Hashtable>("LoginOnline");
            Hashtable hOnline = (Hashtable)WebCache.GetCache("LoginOnline");
            if (hOnline != null)
            {
                Flag = hOnline.Count;
            }
            return Flag;
        }

        /// <summary>
        /// 登录系统
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <param name="Type"></param>
        /// <returns>返回：唯一GUID</returns>
        public static string LoginedSetKey(string UserID, string userType)
        {
            //以guid作为用户的唯一标识
            string guid = Guid.NewGuid().ToString();
            //
            //Hashtable hOnline = MemCached.GetCache<Hashtable>("LoginOnline");
            Hashtable hOnline = (Hashtable)WebCache.GetCache("LoginOnline");
            if (hOnline == null)
            {
                hOnline = new Hashtable();
            }

            hOnline[UserID + "-" + userType] = guid;
            WebCache.Insert("LoginOnline", hOnline, 0);
            //MemCached.AddOrReplaceCache<Hashtable>("LoginOnline", hOnline, DateTime.Now.AddYears(1));
            return guid;
        }


        /// <summary>
        /// 登出系统，清除系统缓存
        /// </summary>
        /// <param name="UserID">用户ID</param>
        public static bool LogoutSystem(string UserID, string userType)
        {
            //Hashtable hOnline = MemCached.GetCache<Hashtable>("LoginOnline");
            Hashtable hOnline = (Hashtable)WebCache.GetCache("LoginOnline");
            if (hOnline != null)
            {
                hOnline.Remove(UserID + "-" + userType);
                MemCached.AddOrReplaceCache<Hashtable>("LoginOnline", hOnline, DateTime.Now.AddYears(1));
                return true;
            }
            return false;
        }
    }
}