using System;
using System.Web;
using System.Web.Caching;
using System.Configuration;
namespace PublicLib
{
    /// <summary>
    /// Web缓存
    /// </summary>
    public class WebCache
    {
        private static Cache _cache = HttpContext.Current.Cache;
        private static int _iDefaultMinutes = Helper.StringToInt(Helper.GetAppSettings("CacheExpiresMin"));

        /// <summary>
        /// 根据缓存关键值添加缓存项
        /// </summary>
        /// <param name="Key">与缓存项相关的关键值</param>
        /// <param name="Value">缓存的对象</param>
        public static void Insert(string Key, object Value)
        {
            Insert(Key, Value, null, _iDefaultMinutes, 0);
        }

        /// <summary>
        /// 根据缓存关键值添加缓存项
        /// </summary>
        /// <param name="Key">与缓存项相关的关键值</param>
        /// <param name="Value">缓存的对象</param>
        /// <param name="CacheMinutes">所插入对象将过期并被从缓存中移除的时间间隔(分钟)</param>
        public static void Insert(string Key, object Value, int CacheMinutes)
        {
            Insert(Key, Value, null, CacheMinutes, 0);
        }

        /// <summary>
        /// 根据指定的关键值、依赖项、缓存期间添加缓存项
        /// </summary>
        /// <param name="Key">与缓存项相关的关键值</param>
        /// <param name="Value">缓存的对象</param>
        /// <param name="Dependency">文件依赖项或缓存键依赖项</param>
        /// <param name="CacheMinutes">所插入对象将过期并被从缓存中移除的时间间隔(分钟)</param>
        /// <param name="SlideMinutes">可调过期的时间间隔(分钟)</param>
        private static void Insert(string Key, object Value, CacheDependency Dependency, int CacheMinutes, int SlideMinutes)
        {
            if (CacheMinutes > 0)
            {
                _cache.Insert(Key, Value, Dependency, DateTime.Now.AddMinutes(CacheMinutes), TimeSpan.Zero);
            }
            else
            {
                _cache.Insert(Key, Value, Dependency, DateTime.MaxValue, TimeSpan.FromMinutes(SlideMinutes));
            }
        }        

        /// <summary>
        /// 判断指定的缓存是否存在
        /// </summary>
        /// <param name="Key">与缓存项相关的关键值</param>
        /// <returns>是否存在指定的缓存</returns>
        public static bool IsExist(string Key)
        {
            return (_cache[Key] == null) ? false : true;
        }        

        /// <summary>
        /// 返回指定的缓存项
        /// </summary>
        /// <param name="Key">与缓存项相关的关键值</param>
        /// <returns></returns>
        public static object GetCache(string Key)
        {
            return _cache[Key];
        }        

        /// <summary>
        /// 删除指定的缓存项
        /// </summary>
        /// <param name="Key">与缓存项相关的关键值</param>
        /// <returns>是否存在指定的缓存</returns>
        public static void Remove(string Key)
        {
            if (_cache[Key] != null)
            {
                _cache.Remove(Key);
            }
        }        
    }
}
