using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using System.Xml;

namespace PublicLib
{
    public static class JsonHelper
    {
        /// <summary>  
        /// 将JSON解析成DataSet（只限标准的JSON数据）  
        /// 例如：Json＝{t1:[{name:'数据name',type:'数据type'}]} 或 Json＝{t1:[{name:'数据name',type:'数据type'}],t2:[{id:'数据id',gx:'数据gx',val:'数据val'}]}  
        /// </summary>  
        /// <param name="json">Json字符串</param>  
        /// <returns>DataSet</returns>  
        public static DataSet JsonToDataSet(string json, out string status, out string mes, out string NP)
        {
            status = string.Empty;
            mes = string.Empty;
            NP = string.Empty;
            try
            {
                var ds = new DataSet();
                var jss = new JavaScriptSerializer();
                object obj = jss.DeserializeObject(json);
                var datajson = (Dictionary<string, object>)obj;
                foreach (var item in datajson)
                {
                    bool Flag = false;
                    switch (item.Key.ToLower())
                    {
                        case "status":
                            status = item.Value.ToString();
                            Flag = true;
                            break;
                        case "mes":
                            mes = item.Value.ToString();
                            Flag = true;
                            break;
                        case "np":
                            NP = item.Value.ToString();
                            Flag = true;
                            break;
                    }
                    if (Flag)
                    {
                        continue;
                    }
                    var dt = new DataTable(item.Key);
                    var rows = (object[])item.Value;
                    foreach (object row in rows)
                    {
                        var val = (Dictionary<string, object>)row;
                        DataRow dr = dt.NewRow();
                        foreach (var sss in val)
                        {
                            if (!dt.Columns.Contains(sss.Key))
                            {
                                dt.Columns.Add(sss.Key);
                                dr[sss.Key] = sss.Value;
                            }
                            else
                                dr[sss.Key] = sss.Value;
                        }
                        dt.Rows.Add(dr);
                    }
                    ds.Tables.Add(dt);
                }
                return ds;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        /// <param name="status"></param>
        public static void JsonToStatus(string json, out string status)
        {
            status = string.Empty;
            try
            {
                var ds = new DataSet();
                var jss = new JavaScriptSerializer();
                object obj = jss.DeserializeObject(json);
                var datajson = (Dictionary<string, object>)obj;
                foreach (var item in datajson)
                {
                    bool Flag = false;
                    switch (item.Key.ToLower())
                    {
                        case "status":
                            status = item.Value.ToString();
                            Flag = true;
                            break;
                    }
                    if (Flag)
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Json 字符串 转换为 DataTable数据集合
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static DataTable ToDataTable(this string json)
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
                        if (dictionary.Keys.Count == 0)
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
        /// 转Json字符串
        /// </summary>
        /// <param name="status">状态</param>
        /// <param name="mes">提示信息</param>
        /// <returns>返回：Json字符串</returns>
        public static string ToJson(string status, string mes)
        {
            return ToJson(status, mes, null, null, null, null, null, null);
        }

        /// <summary>
        /// 转Json字符串
        /// </summary>
        /// <param name="status">状态</param>
        /// <param name="mes">提示信息</param>
        /// <param name="arrData">DataTable（DataRow）List对象</param>
        /// <param name="arrTBName">对应的Table名称</param>
        /// <returns>返回：Json字符串</returns>
        public static string ToJson(string status, string mes, ArrayList arrData, string[] arrTBName)
        {
            return ToJson(status, mes, arrData, arrTBName, null, null, null, null);
        }

        /// <summary>
        /// 转Json字符串
        /// </summary>
        /// <param name="status">状态</param>
        /// <param name="mes">提示信息</param>
        /// <param name="arrData">DataTable（DataRow）List对象</param>
        /// <param name="arrTBName">对应的Table名称</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="currentPage">当前页码</param>
        /// <param name="totalPage">总页数</param>
        /// <returns>返回：Json字符串</returns>
        public static string ToJson(string status, string mes, ArrayList arrData, string[] arrTBName, int? pageSize, long? recordCount, int? currentPage, int? totalPage)
        {
            StringBuilder sbJson = new StringBuilder();
            sbJson.Append("{\"status\":\"" + status + "\",\"mes\":\"" + mes + "\"");
            if (arrData != null && arrTBName != null && arrData.Count == arrTBName.Length)
            {
                string T;
                for (int i = 0; i < arrData.Count; i++)
                {
                    T = arrData[i].GetType().Name.ToLower();
                    switch (T)
                    {
                        case "datarow":
                            sbJson.Append(DataRowToString((DataRow)arrData[i], arrTBName[i]));
                            break;
                        case "datatable":
                            sbJson.Append(DataTableToString((DataTable)arrData[i], arrTBName[i]));
                            break;
                    }
                }
            }
            if (pageSize != null && recordCount != null && currentPage != null && totalPage != null)
            {
                sbJson.Append(",\"pageSize\":" + pageSize.ToString() + ",\"recordCount\":" + recordCount.ToString() + ",\"currentPage\":" + currentPage.ToString() + ",\"totalPage\":" + totalPage.ToString());
            }
            sbJson.Append("}");
            return sbJson.ToString();
        }

        /// <summary>
        /// 待转换的DT
        /// </summary>
        /// <param name="dt">dt</param>
        /// <param name="TBName">表名称</param>
        /// <returns>Json字符串</returns>
        private static string DataTableToString(DataTable dt, string TBName)
        {
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            javaScriptSerializer.MaxJsonLength = Int32.MaxValue; //取得最大数值
            ArrayList arrayList = new ArrayList();
            StringBuilder sbJson = new StringBuilder();
            if (dt != null)
            {
                if (TBName.Length > 0)
                {
                    sbJson.Append(",\"" + TBName + "\":");
                }
                else
                {
                    sbJson.Append(",\"data\":");
                }
                foreach (DataRow dataRow in dt.Rows)
                {
                    Dictionary<string, object> dictionary = new Dictionary<string, object>();
                    //实例化一个参数集合
                    foreach (DataColumn dataColumn in dt.Columns)
                    {
                        if (dataColumn.DataType.Name.ToLower() == "datetime")
                        {
                            dictionary.Add(dataColumn.ColumnName, Helper.ReplaceStrin2g(dataRow[dataColumn.ColumnName].ToString()));
                        }
                        else
                        {
                            dictionary.Add(dataColumn.ColumnName, Helper.ReplaceStrin2g(dataRow[dataColumn.ColumnName].ToString()));
                        }
                    }
                    arrayList.Add(dictionary); //ArrayList集合中添加键值
                }
                sbJson.Append(javaScriptSerializer.Serialize(arrayList));  //返回一个json字符串
            }
            return sbJson.ToString();
        }

        /// <summary>
        /// 待转换的DR
        /// </summary>
        /// <param name="dr">dr</param>
        /// <param name="TBName">表名称</param>
        /// <returns>Json字符串</returns>
        private static string DataRowToString(DataRow dr, string TBName)
        {
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            javaScriptSerializer.MaxJsonLength = Int32.MaxValue; //取得最大数值
            ArrayList arrayList = new ArrayList();
            StringBuilder sbJson = new StringBuilder();
            if (dr != null)
            {
                if (TBName.Length > 0)
                {
                    sbJson.Append(",\"" + TBName + "\":");
                }
                else
                {
                    sbJson.Append(",\"data\":");
                }
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                //实例化一个参数集合
                foreach (DataColumn dataColumn in dr.Table.Columns)
                {
                    if (dataColumn.DataType.Name.ToLower() == "datetime")
                    {
                        dictionary.Add(dataColumn.ColumnName, dr[dataColumn.ColumnName].ToString());
                    }
                    else
                    {
                        dictionary.Add(dataColumn.ColumnName, dr[dataColumn.ColumnName]);
                    }
                }
                arrayList.Add(dictionary); //ArrayList集合中添加键值
                string Result = javaScriptSerializer.Serialize(arrayList);
                if (Result.Length >= 2)
                {
                    Result = Result.TrimStart('[').TrimEnd(']');
                }
                sbJson.Append(Result);  //返回一个json字符串
            }
            return sbJson.ToString();
        }
        public static void JsonToRecMes(string json, out string status, out string ltime, out string lasthead)
        {
            status = string.Empty;
            ltime = string.Empty;
            lasthead = string.Empty;
            try
            {
                var ds = new DataSet();
                var jss = new JavaScriptSerializer();
                object obj = jss.DeserializeObject(json);
                var datajson = (Dictionary<string, object>)obj;
                foreach (var item in datajson)
                {
                    switch (item.Key.ToLower())
                    {
                        case "status":
                            status = item.Value.ToString();
                            break;
                        case "ltime":
                            ltime = item.Value.ToString();
                            break;
                        case "lasthead":
                            lasthead = item.Value.ToString();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorMessage(ex);
            }
        }

        public static string GetJsonValByKey(string json, string key)
        {
            string Val = string.Empty;
            try
            {
                var ds = new DataSet();
                var jss = new JavaScriptSerializer();
                object obj = jss.DeserializeObject(json);
                var datajson = (Dictionary<string, object>)obj;
                foreach (var item in datajson)
                {
                    if (item.Key.ToLower() == key.ToLower())
                    {
                        Val = item.Value.ToString();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorMessage(ex);
            }
            return Val;
        }

        /// <summary>  
        ///     将DataSet转化成JSON数据  
        /// </summary>  
        /// <param name="ds"></param>  
        /// <returns></returns>  
        public static string DataSetToJson(DataSet ds)
        {
            string json = string.Empty;
            try
            {
                if (ds.Tables.Count == 0)
                    return string.Empty;
                json = "{";
                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    json += "T" + (i + 1) + ":[";
                    for (int j = 0; j < ds.Tables[i].Rows.Count; j++)
                    {
                        json += "{";
                        for (int k = 0; k < ds.Tables[i].Columns.Count; k++)
                        {
                            json += ds.Tables[i].Columns[k].ColumnName + ":'" + ds.Tables[i].Rows[j][k] + "'";
                            if (k != ds.Tables[i].Columns.Count - 1)
                                json += ",";
                        }
                        json += "}";
                        if (j != ds.Tables[i].Rows.Count - 1)
                            json += ",";
                    }
                    json += "]";
                    if (i != ds.Tables.Count - 1)
                        json += ",";
                }
                json += "}";
            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorMessage(ex);
            }
            return json;
        }

        /// <summary>  
        ///     json字符串转换为Xml对象  
        /// </summary>  
        /// <param name="sJson"></param>  
        /// <returns></returns>  
        public static XmlDocument JsonToXml(string sJson)
        {
            var serializer = new JavaScriptSerializer();
            var dic = (Dictionary<string, object>)serializer.DeserializeObject(sJson);
            var doc = new XmlDocument();
            XmlDeclaration xmlDec = doc.CreateXmlDeclaration("1.0", "gb2312", "yes");
            doc.InsertBefore(xmlDec, doc.DocumentElement);
            XmlElement root = doc.CreateElement("root");
            doc.AppendChild(root);
            foreach (var item in dic)
            {
                XmlElement element = doc.CreateElement(item.Key);
                KeyValue2Xml(element, item);
                root.AppendChild(element);
            }
            return doc;
        }

        private static void KeyValue2Xml(XmlElement node, KeyValuePair<string, object> source)
        {
            object kValue = source.Value;
            if (kValue.GetType() == typeof(Dictionary<string, object>))
            {
                var dictionary = kValue as Dictionary<string, object>;
                if (dictionary != null)
                    foreach (var item in dictionary)
                    {
                        if (node.OwnerDocument != null)
                        {
                            XmlElement element = node.OwnerDocument.CreateElement(item.Key);
                            KeyValue2Xml(element, item);
                            node.AppendChild(element);
                        }
                    }
            }
            else if (kValue.GetType() == typeof(object[]))
            {
                var o = kValue as object[];
                if (o != null)
                    foreach (object t in o)
                    {
                        if (node.OwnerDocument != null)
                        {
                            XmlElement xitem = node.OwnerDocument.CreateElement("Item");
                            var item = new KeyValuePair<string, object>("Item", t);
                            KeyValue2Xml(xitem, item);
                            node.AppendChild(xitem);
                        }
                    }
            }
            else
            {
                if (node.OwnerDocument != null)
                {
                    XmlText text = node.OwnerDocument.CreateTextNode(kValue.ToString());
                    node.AppendChild(text);
                }
            }
        }

        /// <summary>
        /// 数据表转JSON
        /// </summary>
        /// <param name="dataTable">数据表</param>
        /// <returns>JSON字符串</returns>
        public static string DataTableToJSON(DataTable dt)
        {
            string jsonStr = ObjectToJSON(DataTableToList(dt));
            return jsonStr;
        }

        public static List<Dictionary<string, object>> DataTableToList(DataTable dt)
        {
            List<Dictionary<string, object>> list
            = new List<Dictionary<string, object>>();
            foreach (DataRow dr in dt.Rows)
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                foreach (DataColumn dc in dt.Columns)
                {
                    dic.Add(dc.ColumnName, dr[dc.ColumnName]);
                }
                list.Add(dic);
            }
            return list;
        }
        public static string ObjectToJSON(object obj)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            try
            {
                string str = jss.Serialize(obj);
                str = Regex.Replace(str, @"\\/Date\((\d+)\)\\/", match =>
                {
                    DateTime dt = new DateTime(1970, 1, 1);
                    dt = dt.AddMilliseconds(long.Parse(match.Groups[1].Value));
                    dt = dt.ToLocalTime();
                    return dt.ToString("yyyy-MM-dd HH:mm:ss");
                });

                return str;// jss.Serialize(obj);
            }
            catch (Exception ex)
            {
                throw new Exception("JSONHelper.ObjectToJSON(): " + ex.Message);
            }
        }

        /// <summary>
        /// 泛型使用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ObjectToJSON<T>(object obj)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            try
            {
                string str = jss.Serialize(obj);
                str = Regex.Replace(str, @"\\/Date\((\d+)\)\\/", match =>
                {
                    DateTime dt = new DateTime(1970, 1, 1);
                    dt = dt.AddMilliseconds(long.Parse(match.Groups[1].Value));
                    dt = dt.ToLocalTime();
                    return dt.ToString("yyyy-MM-dd HH:mm:ss");
                });

                return ("{\"status\":\"" + 1 + "\",\"data\":" + str + ",\"status\":\"查询成功\"}");
            }
            catch (Exception ex)
            {
                throw new Exception("JSONHelper.ObjectToJSON<T>(): " + ex.Message);
            }
        }
        public static M ObjectToObject<T, M>(T fromObj) where M : new()
        {
            Type fromType= fromObj.GetType();

            Type toType = typeof(M);
            M result = new M();
            //Type type2 = fromObj.GetType();


            foreach (PropertyInfo pi in toType.GetProperties())
            {
             
                string name = pi.Name;//获得属性的名字,后面就可以根据名字判断来进行些自己想要的操作
                //获得属性的类型,进行判断然后进行以后的操作,例如判断获得的属性是整数
                object value = fromType.GetProperty(name).GetValue(fromObj, null);//获取属性值

                if (fromType.GetProperty(name).PropertyType.FullName == "System.DateTime")
                {
                    pi.SetValue(result, Convert.ToDateTime(value).ToString("yyyy-MM-dd hh:mm:ss"), null);
                }
                else if (pi.PropertyType.FullName == "System.Int32" && fromType.GetProperty(name).PropertyType.FullName == "System.Int32")
                {
                    pi.SetValue(result, Convert.ToInt32(value) > 0 ? Convert.ToInt32(value) : 1, null);
                }
                else
                {
                    pi.SetValue(result, value, null);
                }
            }
            return result;
        }

        /// <summary>
        /// 解析JSON字符串生成对象实体
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">json字符串(eg.{"ID":"112","Name":"石子儿"})</param>
        /// <returns>对象实体</returns>
        public static T JsonToObject<T>(string json) where T : class
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            T t = jss.Deserialize<T>(json);
            return t;
        }


        /// <summary>
        /// 解析JSON数组生成对象实体集合
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">json数组字符串(eg.[{"ID":"112","Name":"石子儿"}])</param>
        /// <returns>对象实体集合</returns>
        public static List<T> DeserializeJsonToList<T>(string json) where T : class
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            List<T> list = jss.Deserialize<List<T>>(json);
            return list;

        }
        public static string ToJsonResult(string status, string msg)
        {
            return "{" + string.Format("\"status\":\"{0}\",\"mes\":\"{1}\"", status, msg) + "}";
        }

    }
}
