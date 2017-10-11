using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;
using Aspose.Cells;
using System.Web.Script.Serialization;
using System.Runtime.Serialization;

namespace PublicLib
{
    /// <summary>
    /// Helper帮助类
    /// </summary>
    public sealed class Helper
    {
        /// <summary>
        /// 北京
        /// </summary>
        /// <param name="Mes">发送的信息</param>
        /// <param name="Mobile">手机号码</param>
        /// <returns>返回：发送结果</returns>
        public static bool SendShortMessageByBeijing(string Mobile, string Mes)
        {

            //return true;
            bool Flag = false;
            if (Mobile == null || Mobile.Length == 0)
            {
                return Flag;
            }
            try
            {
                //<add key="ShortMesUrl" value="http://125.208.3.91:8888/sms.aspx"/>
                //<add key="ShortMesUserID" value="8555"/>
                //<add key="ShortMesUser" value="gpit"/>
                //<add key="ShortMesPwd" value="1234567"/>

                //获取URL
                string ShortMesUrl = GetAppSettings("ShortMesUrl");
                //获取企业ID
                string UserID = GetAppSettings("ShortMesUserID");
                //获取用户名
                string UserName = GetAppSettings("ShortMesUser");
                //获取密码
                string UserPwd = GetAppSettings("ShortMesPwd");
                //http://125.208.3.91:8888/sms.aspx?action=send&userid=5160&account=xzl&password=1234567&mobile=13161728051&content=XXXXXXXX[XXXX]&sendTime=&extno=

                string postStr;
                byte[] postBin;
                HttpWebRequest request;
                HttpWebResponse response;
                Stream ioStream;
                postStr = "action=send&userid=" + UserID + "&account=" + UserName + "&password=" + UserPwd + "&mobile=" + Mobile + "&content=" + Mes + "&sendTime=&extno=";//键值对
                postBin = Encoding.UTF8.GetBytes(postStr);//注意提交到的网站的编码，现在是gb2312的
                request = WebRequest.Create(ShortMesUrl) as HttpWebRequest;
                request.Method = "post";
                request.KeepAlive = false;
                request.AllowAutoRedirect = false;//不允许重定向
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = postBin.Length;
                ioStream = request.GetRequestStream();
                ioStream.Write(postBin, 0, postBin.Length);
                ioStream.Flush();
                ioStream.Close();
                response = request.GetResponse() as HttpWebResponse;
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                string r = reader.ReadToEnd();
                reader.Close();
                response.Close();
                if (r != null && r.Length > 0)
                {
                    XmlDocument xml = new XmlDocument();
                    xml.LoadXml(r);
                    string status = string.Empty;
                    string message = string.Empty;
                    XmlNode objNode = xml.SelectSingleNode("returnsms/returnstatus");
                    if (objNode != null)
                    {
                        status = objNode.InnerText;
                    }
                    if (status.ToLower() == "success")
                    {
                        XmlNode objNode2 = xml.SelectSingleNode("returnsms/message");
                        if (objNode2 != null)
                        {
                            message = objNode2.InnerText;
                        }
                        if (message.ToLower() == "ok")
                        {
                            Flag = true;
                        }
                        else
                        {
                            ErrorLog.WriteErrorMessage(message);
                        }
                    }
                    else
                    {
                        ErrorLog.WriteErrorMessage("短信发送失败,手机号:" + Mobile);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorMessage(ex);
            }
            return Flag;
        }

        /// <summary>
        /// 北京空间畅想短信接口
        /// </summary>
        /// <param name="Mes">发送的信息</param>
        /// <param name="Mobile">手机号码</param>
        /// <returns>返回：发送结果</returns>
        public static bool SendShortMessageByBeijingCX(string Mobile, string Mes)
        {
            bool Flag = false;
            try
            {
                //获取URL
                string ShortMesUrl = GetAppSettings("NewUrl");
                //获取用户名
                string UserName = GetAppSettings("NewUName");
                //获取密码
                string UserPwd = GetAppSettings("NewPass");

                string postStr;
                byte[] postBin;
                HttpWebRequest request;
                HttpWebResponse response;
                Stream ioStream;
                postStr = "ua=" + UserName + "&pw=" + UserPwd + "&mb=" + Mobile + "&ms=" + Mes;//键值对
                postBin = Encoding.UTF8.GetBytes(postStr);//注意提交到的网站的编码，现在是gb2312的
                request = WebRequest.Create(ShortMesUrl) as HttpWebRequest;
                request.Method = "post";
                request.KeepAlive = false;
                request.AllowAutoRedirect = false;//不允许重定向
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = postBin.Length;
                ioStream = request.GetRequestStream();
                ioStream.Write(postBin, 0, postBin.Length);
                ioStream.Flush();
                ioStream.Close();
                response = request.GetResponse() as HttpWebResponse;
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                string r = reader.ReadToEnd();
                reader.Close();
                response.Close();
                if (r != null && r.Length > 0)
                {
                    string[] rsplit = r.Split(',');
                    string status = rsplit[0];
                    if (status.ToLower() == "0")
                    {
                        Flag = true;
                    }
                    else
                    {
                        ErrorLog.WriteErrorMessage("短信发送失败,手机号:" + Mobile);
                    }
                    ErrorLog.WriteErrorMessage("短信发送失败,手机号:" + Mobile);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorMessage(ex);
            }
            return Flag;
        }


        /// <summary>
        /// Web请求
        /// </summary>
        /// <param name="serverUrl">地址</param>
        /// <param name="postStr">参数信息</param>
        /// <returns></returns>
        public static string HttpWebRequestByURL(string serverUrl, StringBuilder postStr)
        {
            string result = string.Empty;
            byte[] postBin;
            HttpWebRequest request;
            HttpWebResponse response;
            Stream ioStream;
            postBin = Encoding.UTF8.GetBytes(postStr.ToString());//注意提交到的网站的编码，现在是gb2312的
            request = WebRequest.Create(serverUrl) as HttpWebRequest;
            request.Method = "post";
            request.KeepAlive = false;
            request.AllowAutoRedirect = false;//不允许重定向
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = postBin.Length;
            try
            {
                ioStream = request.GetRequestStream();
                ioStream.Write(postBin, 0, postBin.Length);
                ioStream.Flush();
                ioStream.Close();
                response = request.GetResponse() as HttpWebResponse;
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                result = reader.ReadToEnd();
                reader.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorMessage(ex);
            }
            return result;
        }

        public static System.Drawing.Image HttpWebRequestByURL(string serverUrl)
        {
            System.Drawing.Image img = null;
            HttpWebResponse resp;
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(serverUrl);
            req.Timeout = 150000;
            try
            {
                resp = (HttpWebResponse)req.GetResponse();
                img = new System.Drawing.Bitmap(resp.GetResponseStream());
            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorMessage(ex);
            }
            return img;
        }


        #region JasonHelper
        #region dataTable转换成Json格式
        /// <summary>          
        /// dataTable转换成Json格式          
        /// </summary>          
        /// <param name="dt"></param>          
        /// <returns></returns>          
        public static string DataTable2Json(DataTable dt)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"");
            jsonBuilder.Append(dt.TableName);
            jsonBuilder.Append("\":[");
            jsonBuilder.Append("[");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                jsonBuilder.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(dt.Columns[j].ColumnName);
                    jsonBuilder.Append("\":\"");
                    jsonBuilder.Append(dt.Rows[i][j].ToString());
                    jsonBuilder.Append("\",");
                }
                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
        #endregion dataTable转换成Json格式
        #region DataSet转换成Json格式
        /// <summary>          
        /// DataSet转换成Json格式          
        /// </summary>          
        /// <param name="ds">DataSet</param>         
        /// <returns></returns>          
        public static string Dataset2Json(DataSet ds)
        {
            StringBuilder json = new StringBuilder();
            foreach (DataTable dt in ds.Tables)
            {
                json.Append("{\"");
                json.Append(dt.TableName);
                json.Append("\":");
                json.Append(DataTable2Json(dt));
                json.Append("}");
            }
            return json.ToString();
        }
        #endregion
        #region DataTableToJson

        /// <summary>    
        /// DataSet转换为Json   
        /// </summary>    
        /// <param name="dataSet">DataSet对象</param>   
        /// <returns>Json字符串</returns>    
        public static string DataSetToJson(DataSet dataSet)
        {
            string jsonString = "{";
            foreach (DataTable table in dataSet.Tables)
            {
                jsonString += DataTableToJson(table.TableName, table) + ",";
            }
            jsonString = jsonString.TrimEnd(',');
            return jsonString + "}";
        }

        /// <summary>        
        /// DataTableToJson        
        /// </summary>        
        /// <param name="jsonName"></param>        
        /// <param name="dt"></param>        
        /// <returns></returns>        
        public static string DataTableToJson(string jsonName, DataTable dt)
        {
            StringBuilder Json = new StringBuilder();
            Json.Append("{\"" + jsonName + "\":[");
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Json.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        Json.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":\"" + dt.Rows[i][j].ToString() + "\"");
                        if (j < dt.Columns.Count - 1)
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("}");
                    if (i < dt.Rows.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
            Json.Append("]}");
            return Json.ToString();
        }

        /// <summary>
        /// 自定义格式
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string MyDataTableToJson(DataTable dt)
        {
            StringBuilder Json = new StringBuilder();
            string status = "0";
            string mes = string.Empty;
            string tid = string.Empty;
            if (dt.TableName.ToLower() == "error")
            {
                status = dt.Rows[0]["type"].ToString();
                mes = dt.Rows[0]["mes"].ToString();
                try
                {
                    tid = dt.Rows[0]["tid"].ToString();
                }
                catch (Exception ex)
                {
                    //此处不做任何处理
                }
            }
            Json.Append("{\"status\":\"" + status + "\",\"mes\":\"" + mes + "\"");
            try
            {
                tid = dt.Rows[0]["tid"].ToString();
                Json.Append(",\"tid\":\"" + tid + "\"");
            }
            catch (Exception ex)
            {
                //此处不做任何处理
            }
            Json.Append("}");
            return Json.ToString();
        }

        public static string MyDataTableToJson2(string status, string mes, DataTable dt)
        {
            return MyDataTableToJson2(status, mes, dt, null, null, null, null);
        }

        public static string MyDataTableToJson2(string status, string mes, DataTable dt, int? pageSize, int? recordCount, int? currentPage, int? totalPage)
        {
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            javaScriptSerializer.MaxJsonLength = Int32.MaxValue; //取得最大数值
            ArrayList arrayList = new ArrayList();
            StringBuilder sbJson = new StringBuilder();
            sbJson.Append("{\"status\":\"" + status + "\",\"mes\":\"" + mes + "\",\"data\":");
            foreach (DataRow dataRow in dt.Rows)
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                //实例化一个参数集合
                foreach (DataColumn dataColumn in dt.Columns)
                {
                    if (dataColumn.ColumnName != "RowNumber")
                    {
                        if (dataColumn.DataType.Name.ToLower() == "datetime")
                        {
                            dictionary.Add(dataColumn.ColumnName, dataRow[dataColumn.ColumnName].ToString());
                        }
                        else
                        {
                            dictionary.Add(dataColumn.ColumnName, dataRow[dataColumn.ColumnName]);
                        }
                    }
                }
                arrayList.Add(dictionary); //ArrayList集合中添加键值
            }
            sbJson.Append(javaScriptSerializer.Serialize(arrayList));  //返回一个json字符串
            if (pageSize != null && recordCount != null && currentPage != null && totalPage != null)
            {
                sbJson.Append(",\"pageSize\":" + pageSize.ToString() + ",\"recordCount\":" + recordCount.ToString() + ",\"currentPage\":" + currentPage.ToString() + ",\"totalPage\":" + totalPage.ToString() + "");
            }
            sbJson.Append("}");
            return sbJson.ToString();
        }
        public static string MyDataTableToJson3(string status, string mes, string NP, DataTable dt)
        {
            StringBuilder Json = new StringBuilder();
            Json.Append("{\"status\":\"" + status + "\",\"mes\":\"" + mes + "\",\"data\":[");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Json.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (dt.Columns[j].ColumnName.ToString() == "RowNumber")
                        {
                            dt.Columns.Remove("RowNumber");
                            dt.AcceptChanges();
                            break;
                        }
                    }
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (dt.Columns[j].ColumnName.ToString() != "RowNumber")
                        {
                            Json.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":\"" + dt.Rows[i][j].ToString() + "\"");
                        }

                        if (j < dt.Columns.Count - 1)
                        {
                            Json.Append(",");
                        }

                    }
                    Json.Append("}");
                    if (i < dt.Rows.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
            Json.Append("],\"NP\":\"" + NP + "\"}");
            return Json.ToString();
        }
        public static string MyDataTableToJson4(string status, string mes, string param, string NP, DataTable dt)
        {
            StringBuilder Json = new StringBuilder();
            Json.Append("{\"status\":\"" + status + "\",\"mes\":\"" + mes + "\"," + param + ",\"data\":[");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Json.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (dt.Columns[j].ColumnName.ToString() == "RowNumber")
                        {
                            dt.Columns.Remove("RowNumber");
                            dt.AcceptChanges();
                            break;
                        }
                    }
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        Json.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":\"" + dt.Rows[i][j].ToString() + "\"");

                        if (j < dt.Columns.Count - 1)
                        {
                            Json.Append(",");
                        }

                    }
                    Json.Append("}");
                    if (i < dt.Rows.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
            Json.Append("],\"NP\":\"" + NP + "\"}");
            return Json.ToString();
        }

        /// <summary>
        /// 专属方法。描述（完善资料客户端所需要的省市县，和单位）
        /// </summary>
        /// <param name="status"></param>
        /// <param name="mes"></param>
        /// <param name="NP"></param>
        /// <param name="dt"></param>
        /// <param name="chirdds"></param>
        /// <param name="Chirdname"></param>
        /// <returns></returns>
        public static string InitUsers(string status, string mes, string NP, DataTable dtprovince, DataTable dtcity, DataTable dtarea)
        {
            StringBuilder Json = new StringBuilder();
            Json.Append("{\"status\":\"" + status + "\",\"mes\":\"" + mes + "\",\"data\":{");
            if (dtprovince.Rows.Count > 0)
            {
                for (int i = 0; i < dtprovince.Rows.Count; i++)
                {
                    Json.Append("\"provinceList\":[");
                    Json.Append("\"provinceID\":\"" + dtprovince.Rows[i]["provinceID"].ToString() + "\",");
                    Json.Append("\"province\":\"" + dtprovince.Rows[i]["province"].ToString() + "\",");
                    /*遍历城市*/
                    for (int j = 0; j < dtcity.Rows.Count; j++)
                    {
                        Json.Append("\"cityList\":[");
                        Json.Append("\"cityID\":\"" + dtcity.Rows[j]["cityID"].ToString() + "\",");
                        Json.Append("\"city\":\"" + dtcity.Rows[j]["city"].ToString() + "\",");
                        Json.Append("\"fatherID\":\"" + dtcity.Rows[j]["fatherID"] + "\",");
                        /*遍历区域*/
                        for (int m = 0; m < dtarea.Rows.Count; m++)
                        {
                            Json.Append("\"areaList\":[");
                            Json.Append("\"areaID\":\"" + dtarea.Rows[m]["areaID"].ToString() + "\",");
                            Json.Append("\"area\":\"" + dtarea.Rows[m]["area"].ToString() + "\",");
                            Json.Append("\"fatherID\":\"" + dtarea.Rows[m]["fatherID"].ToString() + "\"");
                            Json.Append("]");
                        }
                        Json.Append("]");
                    }
                    Json.Append("]");
                }
            }
            Json.Append("}");
            return Json.ToString();
        }

        /// <summary>    
        /// 将 Json 解析成 DateTable。   
        /// Json 数据格式如:  
        ///{table:[{column1:1,column2:2,column3:3},{column1:1,column2:2,column3:3}]} /// </summary>    
        /// <param name="strJson">要解析的 Json 字符串</param>    
        /// <returns>返回 DateTable</returns>    
        public static DataTable JsonToDataTable(string strJson)
        {
            // 取出表名    
            var rg = new Regex(@"(?<={)[^:]+(?=:\[)", RegexOptions.IgnoreCase);
            string strName = rg.Match(strJson).Value;
            DataTable tb = null;

            // 去除表名    
            strJson = strJson.Substring(strJson.IndexOf("[") + 1);
            strJson = strJson.Substring(0, strJson.IndexOf("]"));

            // 获取数据    
            rg = new Regex(@"(?<={)[^}]+(?=})");
            MatchCollection mc = rg.Matches(strJson);
            for (int i = 0; i < mc.Count; i++)
            {
                string strRow = mc[i].Value;
                string[] strRows = strRow.Split(',');
                // 创建表    
                if (tb == null)
                {
                    tb = new DataTable();
                    tb.TableName = strName;
                    foreach (string str in strRows)
                    {
                        var dc = new DataColumn();
                        string[] strCell = str.Split(':');
                        dc.ColumnName = strCell[0].Replace("\"", "");
                        tb.Columns.Add(dc);
                    }
                    tb.AcceptChanges();
                }

                // 增加内容    
                DataRow dr = tb.NewRow();
                for (int j = 0; j < strRows.Length; j++)
                {
                    dr[j] = strRows[j].Split(':')[1].Replace("\"", "");
                }
                tb.Rows.Add(dr);
                tb.AcceptChanges();
            }
            return tb;
        }

        /// <summary>
        /// Json 字符串 转换为 DataTable数据集合
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static DataTable ToDataTable(string json)
        {
            DataTable dataTable = new DataTable();  //实例化
            DataTable result;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                javaScriptSerializer.MaxJsonLength = Int32.MaxValue; //取得最大数值
                ArrayList arrayList = javaScriptSerializer.Deserialize<ArrayList>(json);
                if (arrayList.Count > 0)
                {
                    foreach (Dictionary<string, object> dictionary in arrayList)
                    {
                        if (dictionary.Keys.Count<string>() == 0)
                        {
                            result = dataTable;
                            return result;
                        }
                        if (dataTable.Columns.Count == 0)
                        {
                            foreach (string current in dictionary.Keys)
                            {
                                dataTable.Columns.Add(current, dictionary[current].GetType());
                            }
                        }
                        DataRow dataRow = dataTable.NewRow();
                        foreach (string current in dictionary.Keys)
                        {
                            dataRow[current] = dictionary[current];
                        }

                        dataTable.Rows.Add(dataRow); //循环添加行到DataTable中
                    }
                }
            }
            catch
            {
            }
            result = dataTable;
            return result;
        }

        /// <summary>    
        /// 将 Json 解析成 DateTable。   
        /// Json 数据格式如:  
        ///{table:[{column1:1,column2:2,column3:3},{column1:1,column2:2,column3:3}]} /// </summary>    
        /// <param name="strJson">要解析的 Json 字符串</param>    
        /// <param name="strJson">没在Json中，但是需要这个参数</param>    
        /// <returns>返回 DateTable</returns>    
        public static DataTable JsonToDataTable1(string strJson, string strJson1)
        {
            // 取出表名    
            var rg = new Regex(@"(?<={)[^:]+(?=:\[)", RegexOptions.IgnoreCase);
            string strName = rg.Match(strJson).Value;
            DataTable tb = null;

            // 去除表名    
            strJson = strJson.Substring(strJson.IndexOf("[") + 1);
            strJson = strJson.Substring(0, strJson.IndexOf("]"));

            // 获取数据    
            rg = new Regex(@"(?<={)[^}]+(?=})");
            MatchCollection mc = rg.Matches(strJson);
            for (int i = 0; i < mc.Count; i++)
            {
                string strRow = mc[i].Value + strJson1;
                string[] strRows = strRow.Split(',');
                // 创建表    
                if (tb == null)
                {
                    tb = new DataTable();
                    tb.TableName = strName;
                    foreach (string str in strRows)
                    {
                        var dc = new DataColumn();
                        string[] strCell = str.Split(':');
                        dc.ColumnName = strCell[0].Replace("\"", "");
                        tb.Columns.Add(dc);
                    }
                    tb.AcceptChanges();
                }

                // 增加内容    
                DataRow dr = tb.NewRow();
                for (int j = 0; j < strRows.Length; j++)
                {
                    dr[j] = strRows[j].Split(':')[1].Replace("\"", "");
                }
                tb.Rows.Add(dr);
                tb.AcceptChanges();
            }
            return tb;
        }
        public static DataTable MyJsonToDataTable(string strJson, out string status, out string mes, out string NP)
        {
            status = string.Empty;
            mes = string.Empty;
            NP = "0";
            strJson = strJson.TrimStart('{').TrimEnd('}');
            string[] arr = strJson.Split(',');

            status = arr[0].Replace("\"", "").Split(':')[1];
            mes = arr[1].Replace("\"", "").Split(':')[1].Replace("\\n", "\n");
            if (arr.Length == 2)
            {
                return new DataTable();
            }
            else
            {
                StringBuilder sbData = new StringBuilder();
                sbData.Append("{");
                for (int i = 2; i < arr.Length - 1; i++)
                {
                    sbData.Append(arr[i]);
                    if (i != arr.Length - 2)
                    {
                        sbData.Append(",");
                    }
                }
                sbData.Append("}");
                //"NP":"0"
                NP = arr[arr.Length - 1].Replace("\"", "").Split(':')[1];
                return JsonToDataTable(sbData.ToString());
            }
        }
        #endregion
        #endregion

        #region 字符串格式转换
        /// <summary>
        /// SQL字符串过滤
        /// </summary>
        /// <param name="Text">待过滤的内容</param>
        /// <returns>返回：过滤后字符串</returns>
        public static string ReplaceString(string Text)
        {
            if (Text != null)
            {
                return Text.Trim().Replace("'", "‘");
            }
            else
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// SQL字符串过滤
        /// </summary>
        /// <param name="Text">待过滤的内容</param>
        /// <returns>返回：过滤后字符串</returns>
        public static string ReplaceStrin2g(string Text)
        {
            if (Text != null)
            {
                return Text.Trim().Replace("‘", "'");
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 字符串转Int
        /// </summary>
        /// <param name="Text">带转换的内容</param>
        /// <returns>返回：Int结果，非法返回0</returns>
        public static int StringToInt(string Text)
        {
            if (Text != null)
            {
                int temp;
                int.TryParse(Text, out temp);
                return temp;
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// 字符串转bool
        /// </summary>
        /// <param name="Text">带转换的内容</param>
        /// <returns>返回：bool结果，非法返回false</returns>
        public static bool StringToBool(string Text)
        {
            if (Text != null)
            {
                bool temp;
                bool.TryParse(Text, out temp);
                return temp;
            }
            else
            {
                return false;
            }
        }

        public static string ObjectToString(object obj)
        {
            if (obj != null)
            {
                return obj.ToString();
            }
            else
            {
                return "";
            }
        }
   
        public static int ObjectToInt(object obj)
        {
            if (obj != null)
            {
                return Convert.ToInt32(obj);
            }
            else
            {
                return 0;
            }
        }
        public static string[] ObjectToArrry(object obj)
        {
            if (obj != null&&obj!="")
            {
                return obj.ToString().Replace("]", "").Replace("[", "").Replace("\"", "").Split(',');
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 字符串转Long
        /// </summary>
        /// <param name="Text">带转换的内容</param>
        /// <returns>返回：Long结果，非法返回0</returns>
        public static long StringToLong(string Text)
        {
            if (Text != null)
            {
                long temp;
                long.TryParse(Text, out temp);
                return temp;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 字符串转Double
        /// </summary>
        /// <param name="Text">带转换的内容</param>
        /// <returns>返回：Double结果，非法返回0</returns>
        public static double StringToDouble(string Text)
        {
            if (Text != null)
            {
                double temp;
                double.TryParse(Text, out temp);
                return temp;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 字符串转Decimal
        /// </summary>
        /// <param name="Text">带转换的内容</param>
        /// <returns>返回：Decimal结果，非法返回0</returns>
        public static decimal StringToDecimal(string Text)
        {
            if (Text != null)
            {
                decimal temp;
                decimal.TryParse(Text, out temp);
                return temp;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// object转换字符串
        /// </summary>
        /// <param name="Text">object</param>
        /// <returns>返回：字符串结果，非法返回""</returns>
        public static string String(object Text)
        {
            try
            {
                return Convert.ToString(Text);
            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>
        /// 字符串转Float
        /// </summary>
        /// <param name="Text">带转换的内容</param>
        /// <returns>返回：Float结果，非法返回0</returns>
        public static float StringToFloat(string Text)
        {
            if (Text != null)
            {
                float temp;
                float.TryParse(Text, out temp);
                return temp;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 字符串转DateTime
        /// </summary>
        /// <param name="Text">带转换的内容</param>
        /// <returns>返回：DateTime结果，非法返回1900-01-01</returns>
        public static DateTime StringToDateTime(string Text)
        {
            if (Text != null && Text.Length > 0)
            {
                DateTime temp;
                DateTime.TryParse(Text, out temp);
                return temp;
            }
            else
            {
                return DateTime.Parse("1900-01-01");
            }
        }

        /// <summary>
        /// Base64字符串转Byte
        /// </summary>
        /// <param name="Text">带转换的内容</param>
        /// <returns>返回：Byte结果，非法返回null</returns>
        public static byte[] Base64StringToByte(string Text)
        {
            if (Text != null && Text.Length > 0)
            {
                byte[] buf = Convert.FromBase64String(Text);//把字符串读到字节数组中
                return buf;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Byte[]转Image
        /// </summary>
        /// <param name="by">字节数组</param>
        /// <returns>返回：Image对象</returns>
        public static System.Drawing.Image ByteToImage(byte[] by)
        {
            if (by != null)
            {
                MemoryStream ms = new MemoryStream(by);
                return System.Drawing.Image.FromStream(ms);
            }
            return null;
        }

        /// <summary>
        /// Image转Byte[]
        /// </summary>
        /// <param name="by">Image对象</param>
        /// <returns>返回：Byte[]对象</returns>
        public static byte[] ImageToByte(System.Drawing.Image image)
        {
            if (image != null)
            {
                MemoryStream ms = new MemoryStream();
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, (object)image);
                ms.Close();
                return ms.ToArray();
            }
            return null;
        }

        /// <summary>
        /// Image转Base64String
        /// </summary>
        /// <param name="image">Image对象</param>
        /// <returns>返回：Base64String</returns>
        public static string ImageToBase64String(System.Drawing.Image image)
        {
            if (image != null)
            {
                MemoryStream ms = new MemoryStream();
                image.Save(ms, ImageFormat.Jpeg);
                string mes = Convert.ToBase64String(ms.ToArray());
                ms.Close();
                return mes;
            }
            return string.Empty;
        }

        /// <summary>
        /// byte[]转Base64String
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <returns>返回：Base64String</returns>
        public static string ByteToBase64String(byte[] bytes)
        {
            if (bytes != null)
            {
                string mes = Convert.ToBase64String(bytes);
                return mes;
            }
            return string.Empty;
        }

        /// <summary>
        /// byte[]转String
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <returns>返回：String</returns>
        public static string ByteToString(byte[] bytes)
        {
            StringBuilder strBuilder = new StringBuilder();
            foreach (byte bt in bytes)
            {
                strBuilder.AppendFormat("{0:X2}", bt);
            }
            return strBuilder.ToString();
        }

        /// <summary>
        /// Stream转Bytes
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static byte[] StreamToBytes(Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始 
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;

            //if (stream.CanSeek)
            //{
            //    Byte[] buffer = new byte[stream.Length];
            //    stream.Write(buffer, 0, buffer.Length);
            //    return buffer;
            //}
            //用下面的方法
            //return null;
        }

        /// <summary>
        /// DataRow转Table
        /// </summary>
        /// <param name="dr">DataRow对象</param>
        /// <returns>返回：DataTable</returns>
        public static DataTable DataRowToDataTable(DataRow[] dr)
        {
            if (dr == null || dr.Length == 0)
                return null;
            DataTable tmp = dr[0].Table.Clone();  // 复制DataRow的表结构
            tmp.TableName = Guid.NewGuid().ToString();
            foreach (DataRow row in dr)
                tmp.ImportRow(row);  // 将DataRow添加到DataTable中
            return tmp.Copy();
        }
        # endregion


        /// <summary>
        /// 通过身份证获取生日
        /// </summary>
        /// <param name="cardID"></param>
        /// <returns></returns>
        public static string GetBirthDay(string cardID)
        {
            string birthday = string.Empty;
            if (cardID.Length == 18)//处理18位的身份证号码从号码中得到生日和性别代码
            {
                birthday = cardID.Substring(6, 4) + "-" + cardID.Substring(10, 2) + "-" + cardID.Substring(12, 2);
            }
            if (cardID.Length == 15)
            {
                birthday = "19" + cardID.Substring(6, 2) + "-" + cardID.Substring(8, 2) + "-" + cardID.Substring(10, 2);
            }
            return birthday;
        }

        /// <summary>
        /// 把从数据库里取出的文本中的空格和回车换行符转换成ＨＴＭｌ格式
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string FormartStringToHTML(string text)
        {
            return text.Replace(" ", "&nbsp;").Replace("\n", "<BR>");
        }

        #region JS方法
        /// <summary>
        /// Alert字符信息
        /// </summary>
        /// <param name="Mes"></param>
        public static void WriteAlertScript(string Mes)
        {
            System.Web.UI.Page page = (System.Web.UI.Page)System.Web.HttpContext.Current.Handler;
            ScriptManager.RegisterStartupScript(page, typeof(Page), System.DateTime.Now.Ticks.ToString(), "alert('" + Mes + "');", true);
        }

        /// <summary>
        /// 执行JS脚本
        /// </summary>
        /// <param name="Mes"></param>
        public static void WriteScriptMessage(string Mes)
        {
            System.Web.UI.Page page = (System.Web.UI.Page)System.Web.HttpContext.Current.Handler;
            page.ClientScript.RegisterStartupScript(page.GetType(), Guid.NewGuid().ToString(), "<script type='text/javascript'>" + Mes + "</script>");
        }

        /// <summary>
        /// 执行JS脚本
        /// </summary>
        /// <param name="Mes"></param>
        public static void WriteScriptMessage(string Mes, System.Web.UI.Page page)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "PageScript", "<script type='text/javascript'>" + Mes + "</script>");
        }
        #endregion

        /// <summary>
        /// 获取App.config文件Key的值
        /// </summary>
        /// <param name="strKey">Key名称</param>
        /// <returns>Value</returns>
        public static string GetAppSettings(string strKey)
        {
            try
            {
                string strValue = System.Configuration.ConfigurationManager.AppSettings[strKey];
                if (strValue != null)
                    return strValue.ToString();
                else
                    return string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }

        #region DropDownLis方法
        /// <summary>
        /// 绑定下拉框
        /// </summary>
        /// <param name="DDL">下拉框控件</param>
        /// <param name="dtData">数据源</param>
        /// <param name="TextField">Text列名称</param>
        /// <param name="ValueField">Value列名称</param>
        /// <param name="SelectOrALL">0-（不添加）；1-（添加：--全部--）；2-（添加：-请选择-）。</param>
        public static void BindDropDownListForSearch(DropDownList DDL, DataTable dtData, string TextField, string ValueField, int SelectOrALL)
        {
            BindDropDownListInfo(DDL, dtData, TextField, ValueField, SelectOrALL);
        }
        /// <summary>
        /// 根据Text选中下拉框
        /// </summary>
        /// <param name="DDL"></param>
        /// <param name="Text"></param>
        public static void SelectDropDownListText(DropDownList DDL, string Text)
        {
            if (DDL != null)
            {
                for (int i = 0; i < DDL.Items.Count; i++)
                {
                    if (DDL.Items[i].Text == Text)
                    {
                        DDL.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 绑定下拉框
        /// </summary>
        /// <param name="DDL">下拉框控件</param>
        /// <param name="EnumType">枚举名称</param>
        /// <param name="SelectValue">选中枚举值</param>
        /// <param name="IsAdd">true-（添加：--全部--）</param>
        public static void SelectDropDownListText(DropDownList DDL, Type EnumType, string SelectValue, bool IsAdd)
        {
            if (DDL != null)
            {
                ListItemCollection items = new ListItemCollection();
                FieldInfo[] fs = EnumType.GetFields();
                object enumInstance = EnumType.Assembly.CreateInstance(EnumType.FullName);
                EnumAttribute[] cas;
                ListItem item;
                foreach (FieldInfo f in fs)
                {
                    cas = (EnumAttribute[])f.GetCustomAttributes(typeof(EnumAttribute), true);
                    if (cas.Length > 0)
                    {
                        item = new ListItem();
                        item.Value = ((int)f.GetValue(enumInstance)).ToString();
                        item.Text = cas[0].Name;
                        DDL.Items.Add(item);
                    }
                }
                if (IsAdd)
                {
                    item = new ListItem();
                    item.Value = "";
                    item.Text = "-全部-";
                    items.Insert(0, item);
                }
                DDL.SelectedValue = SelectValue;
            }
        }

        /// <summary>
        /// 绑定下拉框
        /// </summary>
        /// <param name="SEL">下拉框控件</param>
        /// <param name="EnumType">枚举名称</param>
        /// <param name="DefaultValue">默认枚举值</param>
        public static void SelectDropDownListText(HtmlSelect SEL, Type EnumType, int DefaultValue)
        {
            if (SEL != null)
            {
                ListItemCollection items = new ListItemCollection();
                FieldInfo[] fs = EnumType.GetFields();
                object enumInstance = EnumType.Assembly.CreateInstance(EnumType.FullName);
                EnumAttribute[] cas;
                ListItem item;
                foreach (FieldInfo f in fs)
                {
                    cas = (EnumAttribute[])f.GetCustomAttributes(typeof(EnumAttribute), true);
                    if (cas.Length > 0)
                    {
                        int val = (int)f.GetValue(enumInstance);
                        if (val == DefaultValue + 1)
                        {
                            item = new ListItem();
                            item.Value = val.ToString();
                            item.Text = cas[0].Name;
                            SEL.Items.Add(item);
                        }
                    }
                }
            }
        }
        #endregion
        #region 私有方法
        private static void BindDropDownListInfo(DropDownList DDL, DataTable dtData, string TextField, string ValueField, int SelectOrALL)
        {
            if (dtData != null)
            {
                try
                {
                    if (DDL != null)
                    {
                        DDL.Items.Clear();
                        DDL.DataSource = dtData;
                        DDL.DataTextField = TextField;
                        DDL.DataValueField = ValueField;
                        DDL.DataBind();
                    }
                }
                catch
                { }
            }
            switch (SelectOrALL)
            {
                case 1:
                    DDL.Items.Insert(0, new ListItem("--全部--", ""));
                    break;
                case 2:
                    DDL.Items.Insert(0, new ListItem("总部", "0"));
                    break;
            }
        }
        #endregion

        /// <summary>
        /// 解析返回结果
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="type"></param>
        /// <param name="mes"></param>
        public static void GetDataTableToResult(DataTable dt, out string type, out string mes)
        {
            type = string.Empty;
            mes = null;
            if (dt != null && dt.TableName.ToLower() == "error")
            {
                if (dt.Rows.Count > 0)
                {
                    type = dt.Rows[0]["type"].ToString();
                    mes = dt.Rows[0]["mes"].ToString();
                }
            }
            else
            {
                type = "0";
                mes = "";
            }
        }

        public static void GetDataTableToResult(DataTable dt, out string type, out string[] mes, out string[] spanids)
        {
            type = string.Empty;
            mes = null;
            spanids = null;
            if (dt != null && dt.TableName.ToLower() == "error")
            {
                if (dt.Rows.Count > 0)
                {
                    type = dt.Rows[0]["type"].ToString();
                    mes = dt.Rows[0]["mes"].ToString().Split('|');
                    spanids = dt.Rows[0]["spanids"].ToString().Split('|');
                }
            }
        }

        /// <summary>
        /// 获取4位流水号，并生成相应编号
        /// <param name="Builder">字符串集合</param>
        /// <param name="tablename">表名</param>
        /// <param name="fieldname">字段名</param>
        /// <param name="number">前缀字符串</param>
        /// </summary>
        public static void SqlExecution(StringBuilder Builder, string tablename, string fieldname, string number)
        {
            Builder.AppendLine(" DECLARE @number INT; ");
            Builder.AppendLine(" DECLARE @max INT; ");
            Builder.AppendLine(" DECLARE @testcode VARCHAR(50); ");
            Builder.AppendLine(" SET @testcode ='" + number + "0001' ;");
            Builder.AppendLine(" IF EXISTS(SELECT id FROM dbo." + tablename + ") ");
            Builder.AppendLine(" BEGIN ");
            //生成试卷编码：课程三大类两位数字+课程类型两位数字+课程6大分类两位数字+4位流水号
            Builder.AppendLine(" SELECT @number=MAX(SUBSTRING(" + fieldname + ",7,10)) FROM dbo." + tablename + " ;");
            Builder.AppendLine(" SET @max= @number+1; ");
            Builder.AppendLine(" SELECT @testcode='" + number + "'+RIGHT(REPLICATE('0',4)+CAST(@max AS varchar(10)),4); ");
            Builder.AppendLine(" END ");
        }

        /// <summary>
        /// 解析object[]数组  object[0]格式是Dictionary 类型        
        /// <typeparam name="T">T</typeparam>
        /// <param name="objArray">object数组对象</param>
        /// <returns>List<T> </returns>
        /// <remarks>tkme 2016-4-20</remarks>
        /// </summary>
        public static List<T> ObjectArrayToList<T>(object objArray)
        {
            //创建对象集合
            List<T> list = new List<T>();
            try
            {
                //创建一个对象
                T myobj = Activator.CreateInstance<T>();
                //解析(object[]) 对象数据
                object[] obj = (object[])objArray;
                for (int i = 0; i < obj.Length; i++)
                {
                    //获取对象属性
                    foreach (KeyValuePair<string, object> nature in (Dictionary<string, object>)(obj[i]))
                    {
                        var keyStr = nature.Key;//等于对象的 属性
                        var valueStr = nature.Value;//等于对象的 值

                        //循环对象的属性
                        PropertyInfo[] propertys = myobj.GetType().GetProperties();
                        foreach (PropertyInfo pinfo in propertys)
                        {
                            //判断对象的属性 与 字典的KEY 是否相等，相等就赋值
                            if (nature.Key == pinfo.Name)
                            {
                                //pinfo.SetValue(myobj, Convert.ChangeType(nature.Value, pinfo.PropertyType), null);
                                pinfo.SetValue(myobj, Convert.ChangeType(CheckType(pinfo.PropertyType, nature.Value), pinfo.PropertyType), null);
                            }
                        }
                    }
                    T myobjCoyp = Copy<T>(myobj); //克隆一个对象
                    list.Add(myobjCoyp);//将克隆的对象 赋值到集合
                }
            }
            catch
            {
            }
            return list;
        }


        /// <summary>
        /// 导入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filename">导入的Excel文件名</param>
        /// <param name="fieldName">数据表字段名，用逗号分隔</param>
        /// <param name="columnsName">Excel文件列明，用逗号分隔</param>
        /// <param name="EntityList">存储数据数的对象集合</param>
        /// <returns></returns>
        public static List<T> ImportExcel<T>(string filename, string fieldName, string columnsName)
        {
            DataTable result = new DataTable();
            //创建对象集合
            List<T> list = new List<T>();

            string[] fieldArry = fieldName.Split(',');
            string[] columArry = columnsName.Split(',');
            if (string.IsNullOrEmpty(filename))
            {
                return null;
            }
            if (fieldArry.Length != columArry.Length || fieldArry.Length <= 0)
            {
                //return "fieldName与columnsName数量不等";
                return null;
            }
            string path = HttpContext.Current.Server.MapPath(filename);
            string fileName = path.Substring(path.LastIndexOf("\\") + 1);
            Aspose.Cells.Workbook workbook = new Workbook();

            try
            {
                workbook.Open(path);
            }
            catch (Exception e)
            {
                switch (e.Message)
                {
                    case "This Excel files contains (Excel3.0 or earlier file format) records.":
                        //ErrorStr = "您上传的Excel文件包含更早的版本的记录，请重新选择要上传的文件。";
                        break;
                    case "This Excel files contains (Excel4.0 or earlier file format) records.":
                        //ErrorStr = "您上传的Excel文件包含更早的版本的记录，请重新选择要上传的文件。";
                        break;
                    case "This file's format is not supported or you don't specify a correct format.":
                        //ErrorStr = "您上传的文件不是Excel格式，请重新选择要上传的文件。";
                        break;
                }
            }

            Worksheets wsts = workbook.Worksheets;
            DataTable dt = new DataTable();
            try
            {
                if (wsts.Count > 0)
                {
                    Worksheet wst = wsts[0];
                    int MaxR = wst.Cells.MaxRow;
                    int MaxC = wst.Cells.MaxColumn;
                    if (MaxR > 0 && MaxC > 0)
                    {
                        dt = wst.Cells.ExportDataTableAsString(0, 0, MaxR + 1, MaxC + 1, true);
                        for (int n = 0; n < dt.Rows.Count; n++)
                        {
                            T myobj = Activator.CreateInstance<T>();
                            for (int i = 0; i < columArry.Length; i++)
                            {
                                //循环对象的属性
                                PropertyInfo[] propertys = myobj.GetType().GetProperties();
                                foreach (PropertyInfo pinfo in propertys)
                                {
                                    //判断对象的属性 与 数据字段 是否相等，相等就赋值
                                    if (fieldArry[i].ToString() == pinfo.Name)
                                    {
                                        pinfo.SetValue(myobj, Convert.ChangeType(Helper.CheckType(pinfo.PropertyType, dt.Rows[n][columArry[i].ToString()].ToString()), pinfo.PropertyType), null);
                                    }
                                }
                            }
                            list.Add(myobj);
                        }
                    }
                }
                return list;
            }
            catch
            {
                return null;
            }
            finally
            {
                //context.Response.Write("[{\"status\":\"" + strResult + "\",\"mes\":\"" + strmsg + "\"}]");
            }
        }

        /// <summary>
        /// Serializable 要复制的实例必须可序列化，包括实例引用的其它实例都必须在类定义时加[Serializable]特性。  
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="RealObject"></param>
        /// <returns></returns>
        public static T Copy<T>(T RealObject)
        {
            using (Stream objectStream = new MemoryStream())
            {
                //利用 System.Runtime.Serialization序列化与反序列化完成引用对象的复制     
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(objectStream, RealObject);
                objectStream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(objectStream);
            }
        }

        /// <summary>
        /// 数据类型有效验证
        /// </summary>
        /// <param name="type">数据类型</param>
        /// <param name="obj">值</param>
        /// <returns>返回正确的数据类型</returns>
        public static Object CheckType(Type type, object obj)
        {
            try
            {
                if (type == typeof(string))
                {
                    //string串类型
                    //obj = Convert.ToString(obj);
                    obj = Helper.ReplaceString(obj.ToString());
                }
                else if (type == typeof(int))
                {
                    //int 类型
                    obj = Convert.ToInt32(obj);
                }
                else if (type == typeof(double))
                {
                    //double串类型
                    obj = Convert.ToDouble(obj);
                }
                else if (type == typeof(float))
                {
                    //float串类型
                    obj = float.Parse(obj.ToString());
                }
                else if (type == typeof(DateTime))
                {
                    //DateTime串类型
                    obj = Convert.ToDateTime(obj);
                }
            }
            catch
            {
                if (type == typeof(string))
                {
                    //string串类型
                    obj = "";
                }
                else if (type == typeof(int))
                {
                    //int 类型
                    obj = 0;
                }
                else if (type == typeof(double))
                {
                    //double串类型
                    obj = 0.0;
                }
                else if (type == typeof(float))
                {
                    //float串类型
                    obj = 0.0;
                }
                else if (type == typeof(DateTime))
                {
                    //DateTime串类型
                    obj = DateTime.Parse("1900-01-01");
                }
            }
            return obj;
        }

        /// <summary>
        /// 将字节大小转换为适的格式
        /// </summary>
        /// <param name="size">容量大小K</param>
        /// <returns>转换后的大小</returns>
        public static string BytesToSize(double size)
        {
            string[] units = new String[] { "B", "KB", "MB", "GB", "TB", "PB" };
            double mod = 1024.0;
            int i = 0;
            while (size >= mod)
            {
                size /= mod;
                i++;
            }
            return Math.Round(size) + units[i];
        }

        //public static List<T> getModel<T>(List<DataTable> dtList, string fliter)
        //{
        //    //获取关系数据数据
        //    string[] whereStr = fliter.ToLower().Split(new string[] { " and " }, StringSplitOptions.RemoveEmptyEntries);
        //    for (int i = 0; i < whereStr.Length; i++)
        //    {
        //        //获取关系数据
        //        string[] relation = whereStr[i].Split(new string[] { " = " }, StringSplitOptions.RemoveEmptyEntries);
        //        string[] fieldA = relation[0].Split('.');
        //        string[] fieldB = relation[1].Split('.');

        //        //查找A表
        //        List<DataTable> dtA = dtList.FindAll(x => x.TableName == fieldA[0]);
        //        //查找B表
        //        List<DataTable> dtB = dtList.FindAll(x => x.TableName == fieldB[0]);



        //        for (int k = 0; k < relation.Length; k++)
        //        {




        //        }
        //    }
        //}

    }
}
