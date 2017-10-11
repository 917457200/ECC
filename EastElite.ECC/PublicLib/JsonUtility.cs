using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PublicLib
{
/// <summary>
        /// Json工具类
        /// </summary>
        public  class JsonUtility
        {
            private static JsonUtility _instance = new JsonUtility();

            /// <summary>
            /// 单例
            /// </summary>
            public static JsonUtility Instance
            {
                get { return _instance; }
                set { _instance = value; }
            }

            /// <summary>
            /// Json转对象
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="json"></param>
            /// <returns></returns>
            public T JsonToObject<T>(string json)
            {
                var ser = new DataContractJsonSerializer(typeof(T));
                var ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
                var jsonObject = (T)ser.ReadObject(ms);
                ms.Close();
                return jsonObject;
            }

            /// <summary>
            /// Json转对象列表
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="json"></param>
            /// <returns></returns>
            public IList<T> JsonToObjectList<T>(string json)
            {
                json = json.Replace("}]}", "");
                var startIndex = json.IndexOf(":[{") + 3;
                var newJson = json.Substring(startIndex);
                var regex = new Regex("},{");
                var jsons = regex.Split(newJson);

                if (newJson.Contains("\":[]}"))
                {
                    throw new Exception("快件单号没有找到");
                }
                var list = new List<T>();

                foreach (var item in jsons)
                {
                    var temp = "{" + item + "}";
                    list.Add(JsonToObject<T>(temp));
                }
                return list;
            }

            /// <summary>
            /// 对象转Json
            /// </summary>
            /// <param name="obj"></param>
            /// <returns></returns>
            public string ObjectToJson(object obj)
            {
                var serializer = new DataContractJsonSerializer(obj.GetType());
                using (var ms = new MemoryStream())
                {
                    serializer.WriteObject(ms, obj);
                    var sb = new StringBuilder();
                    sb.Append(Encoding.UTF8.GetString(ms.ToArray()));
                    return sb.ToString();
                }
            }

            /// <summary>
            /// 对象列表转Json
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="objectList"></param>
            /// <returns></returns>
            public string ObjectListToJson<T>(IList<T> objectList)
            {
                return ObjectListToJson(objectList, "");
            }

            /// <summary>
            /// 对象列表转Json
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="objectList"></param>
            /// <param name="className"></param>
            /// <returns></returns>
            public string ObjectListToJson<T>(IList<T> objectList, string className)
            {
                var sbResult = new StringBuilder();
                sbResult.Append("{");
                className = string.IsNullOrEmpty(className) ? objectList[0].GetType().Name : className;
                sbResult.Append("\"" + className + "\":[");

                for (var i = 0; i < objectList.Count; i++)
                {
                    var item = objectList[i];
                    if (i > 0)
                    {
                        sbResult.Append(",");
                    }
                    sbResult.Append(ObjectToJson(item));
                }

                sbResult.Append("]}");
                return sbResult.ToString();
            }
        }
    }

