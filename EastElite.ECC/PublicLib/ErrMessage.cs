using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Web;
using System.Xml;

namespace PublicLib
{
    /// <summary>
    /// 实现提示信息的相关逻辑处理
    /// </summary>
    public class ErrMessage
    {

        /// <summary>
        /// 根据错误代码获取错误信息
        /// </summary>
        /// <param name="code">错误代码</param>
        /// <returns>返回：错误信息字符串</returns>
        public static MessageInfo GetMessageInfoByCode(string code)
        {
            MessageInfo model = new MessageInfo();
            string msg = "";
            XmlDocument xml = new XmlDocument();
            string strPath = @"Document\ErrMessage.xml";
            //ty，调用bs cs两种结构
            object obj = WebCache.GetCache("ErrMessageData");
            if (obj != null)
            {
                xml = (XmlDocument)obj;
            }
            else
            {
                if (HttpContext.Current != null)
                {

                    xml.Load(HttpContext.Current.Request.PhysicalApplicationPath + strPath);
                }
                else
                {
                    xml.Load(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.FullName + "\\" + strPath);
                }
                WebCache.Insert("ErrMessageData", xml);
            }
            string path = @"Msgs/Msg[@Code='" + code + "']";
            XmlNode objNode = xml.SelectSingleNode(path);
            if (objNode != null)
            {
                msg += objNode.InnerText + "\\n";
            }
            else
            {
                msg += "编码为" + code.ToString() + "的错误信息没有配置" + "\\n";
            }
            if (msg.EndsWith("\\n"))
            {
                msg = msg.Substring(0, msg.Length - 2);
            }
            model.Body = msg;
            model.Code = code;
            return model;
        }
        /// <summary>
        /// 根据编号获取消息对象
        /// </summary>
        /// <param name="code">错误编号</param>
        /// <param name="detailMessage">详细信息</param>
        /// <returns>返回：消息对象</returns>
        public static string GetMessageInfoByCode(string code, string detailMessage)
        {
            MessageInfo model = GetMessageInfoByCode(code);
            model.Body += "\\n详细信息：" + detailMessage;
            return model.Body;
        }

        /// <summary>
        /// 根据编号和系统类型获取没有详细描述的消息对象(多个消息，内容会进行合并)
        /// 注意：只支持警告性信息
        /// </summary>
        /// <param name="Codes">错误编码集合</param>
        /// <returns></returns>
        public static string GetMessageInfoByCode(List<string> Codes)
        {
            MessageInfo model = new MessageInfo();
            string strMsg = "";
            List<string> detailMessages = new List<string>();
            foreach (string code in Codes)
            {
                model = GetMessageInfoByCode(code);
                strMsg += model.Body + "\\n";
            }
            model.Body = strMsg;
            return model.Body;
        }

        public static string GetMessageInfoByListCode(List<string> Codes)
        {
            MessageInfo model = new MessageInfo();
            string strMsg = "";
            List<string> detailMessages = new List<string>();
            foreach (string code in Codes)
            {
                model = GetMessageInfoByCode(code);
                strMsg += model.Body + "|";
            }
            return strMsg.TrimEnd('|');
        }
    }

    /// <summary>
    /// 消息对象
    /// </summary>
    public class MessageInfo
    {
        /// <summary>
        /// 提示信息类型
        /// </summary>        
        public enum MessageBoxType
        {
            /// <summary>
            /// 警告信息
            /// </summary>

            Alert = 0,
            /// <summary>
            /// 提示信息
            /// </summary>            
            Info = 1,
            /// <summary>
            /// 确认信息
            /// </summary>           
            Question = 2,
            /// <summary>
            /// 错误信息
            /// </summary>           
            Stop = 9
        }
        /// <summary>
        /// 初始设置
        /// </summary>
        public MessageInfo()
        {
            m_Success = true;
        }
        private string m_code;
        /// <summary>
        /// 错误编码
        /// </summary>
        public string Code
        {
            get { return m_code; }
            set { m_code = value; }
        }
        private string m_body;
        /// <summary>
        /// 错误信息
        /// </summary>
        public string Body
        {
            get { return m_body; }
            set { m_body = value; }
        }

        private MessageBoxType m_MBType;
        /// <summary>
        /// 消息类型
        /// </summary>
        public MessageBoxType MBType
        {
            get
            {
                if (m_code.StartsWith("A"))
                {
                    m_MBType = MessageBoxType.Alert;
                }
                else if (m_code.StartsWith("E"))
                {
                    m_MBType = MessageBoxType.Stop;
                }
                else if (m_code.StartsWith("Q"))
                {
                    m_MBType = MessageBoxType.Question;
                }
                else
                {
                    m_MBType = MessageBoxType.Info;
                }
                return m_MBType;
            }
        }
        private bool m_Success;
        /// <summary>
        /// 成功还是失败
        /// </summary>
        public bool Success
        {
            get { return m_Success; }
            set { m_Success = value; }
        }
    }
}
