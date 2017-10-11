using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace PublicLib
{
   public  class SystemError
    {
        /// <summary>
        /// 检测数据库执行结果
        /// </summary>
        /// <param name="Result"></param>
        /// <param name="dr"></param>
       public static string GetJsonSystemErrorMsg(string code)
        {
             return "{" + string.Format("\"status\":\"{0}\",\"mes\":\"{1}\"", code, ErrMessage.GetMessageInfoByCode(code).Body) + "}";
          
        }
       public static string GetSystemErrorMsg(string code)
       {
           return ErrMessage.GetMessageInfoByCode(code).Body;

       }
    }
}
