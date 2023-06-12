using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;

namespace HiStaffAPI.AppHelpers
{
    public class MemoryCacheHelper
    {
        public static bool EnableCache = true;
        public static void Add<T>(string key, T o)
        {
            try
            {
                if (o != null)
                {
                    if (CacheEnable())
                    {
                        if (!Exists(key))
                        {

                            ObjectCache defCache = System.Runtime.Caching.MemoryCache.Default;
                            defCache.Add(key, o, new CacheItemPolicy { Priority = System.Runtime.Caching.CacheItemPriority.Default });
                            LogHelper.WriteGeneralLog(string.Format("Key : {0} inserted: ", key));

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteExceptionToLog($"key:{key},exception:{ex.Message}", ex);
            }
        }

        private static bool CacheEnable()
        {
            return EnableCache;
        }

        /// <summary>
        /// Remove item from cache
        /// </summary>
        /// <param name="key">Name of cached item</param>

        public static bool Clear(string key)
        {
            var keyDeleted = false;
            if (CacheEnable())
            {
                try
                {
                    if (Exists(key))
                    {
                        ObjectCache defCache = System.Runtime.Caching.MemoryCache.Default;
                        defCache.Remove(key);

                        LogHelper.WriteGeneralLog(string.Format("Key : {0} cleared", key));
                        keyDeleted = true;
                    }
                    else
                        LogHelper.WriteGeneralLog(string.Format("key : {0} not found", key));
                }
                catch (Exception ex)
                {
                    LogHelper.WriteExceptionToLog($"key:{key},exception:{ex.Message}");
                }
            }
            return keyDeleted;
        }

        public static bool Exists(string key)
        {
            ObjectCache defCache = System.Runtime.Caching.MemoryCache.Default;
            return defCache[key] != null;
        }



        public static T Get<T>(string key)
        {
            try
            {
                if (CacheEnable())
                {
                    if (Exists(key))
                    {
                        ObjectCache defCache = MemoryCache.Default;
                        return (T)defCache[key];
                    }
                    //LogHelper.WriteExceptionToLog("Key : {0} does not exists", key);
                }
                return default;
            }
            catch (Exception ex)
            {
                LogHelper.WriteExceptionToLog($"key:{key},exception:{ex.Message}");
                return default;
            }
        }



        public static bool ClearFromTablePrefix(string key)
        {
            var keyDeleted = false;
            if (CacheEnable())
            {
                var cacheKeys = MemoryCache.Default.Select(kvp => kvp.Key).ToList();
                var cacheItem = new List<string>();
                foreach (var cacheKey in cacheKeys)
                {
                    if (cacheKey.StartsWith(key))
                        cacheItem.Add(cacheKey);
                }
                //Logger.DebugFormat("Number of Key(s) found {0}, with Prefix {1}", cacheItem.Count, key);
                try
                {
                    foreach (var newkey in cacheItem)
                    {
                        ObjectCache defCache = MemoryCache.Default;
                        defCache.Remove(key);
                        //if (LogCacheRelatedActivities)
                        {
                            //Logger.DebugFormat("Key : {0} cleared", newkey);
                        }
                    }
                    keyDeleted = true;
                }
                catch (Exception ex)
                {
                    LogHelper.WriteExceptionToLog($"key:{key},exception:{ex.Message}");
                }

            }
            return keyDeleted;
        }



        public static bool RemoveAllCache()
        {
            try
            {
                if (CacheEnable())
                {
                    var cacheKeys = MemoryCache.Default.Select(kvp => kvp.Key).ToList();
                    foreach (var cacheKey in cacheKeys)
                    {
                        MemoryCache.Default.Remove(cacheKey);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteExceptionToLog(ex.Message);
                return false;
            }
        }
    }
}