using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memcached.ClientLibrary;
using System.Data;
using System.Collections;
using System.Configuration;


namespace PublicLib
{
    ///<summary>
    ///<para>程序名称：MemCached.aspx</para>
    ///<para>功能描述：封装了MemCachedClient的部分方法，建议用来作为工具类放在Common项目下</para>
    ///</summary>
    ///<remarks>公告列表</remarks>
    public static class MemCached
    {
        //构造函数：初始化 MemcachedClient对象
        static MemCached()
        {
            //Memcached.ClientLibrary.SockIOPool pool = Memcached.ClientLibrary.SockIOPool.GetInstance();
            //string[] serverlist = ConfigurationManager.AppSettings["MemcachedServerList"].Trim().Split(',');
            //try
            //{
            //    //设置连接池管理的链接,这里的连接池指的是Memcached客户端与服务器进行Socket链接的一些属性
            //    pool.SetServers(serverlist);

            //    //设置连接池的初始连接数，最小连接数，最大连接数，Socket读取超时时间，Socket连接超时时间
            //    pool.InitConnections = 3;
            //    pool.MinConnections = 3;
            //    pool.MaxConnections = 500;

            //    pool.SocketConnectTimeout = 1000;
            //    pool.SocketTimeout = 3000;

            //    pool.MaintenanceSleep = 30;
            //    pool.Failover = true;

            //    pool.Nagle = false;
            //    pool.Initialize();
            //}
            //catch (Exception ex)
            //{
            //    //记录Error日志
            //}
            MC = new MemcachedClient();
            MC.EnableCompression = true;
        }
        //定义MemcachedClient对象
        public static Memcached.ClientLibrary.MemcachedClient MC;

        //在Cached的Hashtable内判断Key是否存在
        public static bool IsExitsKey(string key)
        {
            if (MC.KeyExists(key))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //插入或替换缓存内的键值
        public static bool AddOrReplaceCache<T>(string strKey, T value,DateTime ExpiryTime)
        {
            //bool i;
           if( MC.KeyExists(strKey)!=true)
           {

               return MC.Add<T>(strKey, value, ExpiryTime);
           }
           else
           {
               return  MC.Replace<T>(strKey, value, ExpiryTime);
               //DataTable dt = (DataTable)MC.Get("UserInfo_Top10");
               //return i ;
           }
        }

        //删除缓存内的键值
        public static bool DeleteCache(string strKey)
        {
            if (MC.KeyExists(strKey) == true)
            {
                return MC.Delete(strKey);
            }
            else
            {
                return false;
            }
        }

        //获取缓存内的键值
        public static T GetCache<T>(string strKey)
        {
            if (MC.KeyExists(strKey) == true)
            {
                return MC.Get<T>(strKey) ;
            }
            else
            {
                return default(T);
            }
        }

        //查看缓存的相关状态参数
        public static Hashtable GetCacheStats()
        {
            ArrayList al = new ArrayList();
            string[] serverlist = Helper.GetAppSettings("MemcachedServerList").Trim().Split(',');
            if (serverlist.Count<string>() > 1)
            {
                foreach (string i in serverlist)
                {
                    al.Add(i);
                }
                return MC.Stats(al);
            }
            else
            {
                return MC.Stats();
            }
        }
    }
}
