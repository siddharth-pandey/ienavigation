using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using ClientPoC.Model;

namespace ClientPoC.Classes
{
    public static class CacheManager
    {
        private static readonly ObjectCache SummaryCache = new MemoryCache("Summary");
        private static readonly ObjectCache DetailsCache = new MemoryCache("Details");

        /// <summary>
        /// Retrieve cached item
        /// </summary>
        /// <typeparam name="T">Type of cache item</typeparam>
        /// <param name="cacheKey">Name of cached item</param>
        /// <returns>Cached item as type</returns>
        public static Dictionary<string, T> Get<T>(string cacheKey, CacheType cacheType) where T : class 
        {
            try
            {
                Dictionary<string, T> cachedData = null;
                KeyValuePair<string, object> cache;
                switch (cacheType)
                {
                    case CacheType.Summary:
                        cachedData = new Dictionary<string, T>();
                        cache = SummaryCache.First(k => k.Key.Contains(cacheKey));
                        cachedData.Add(cache.Key, (T)cache.Value);
                        break;
                    case CacheType.Details:
                        cachedData = new Dictionary<string, T>();
                        cache = DetailsCache.First(k => k.Key.Contains(cacheKey));
                        cachedData.Add(cache.Key, (T)cache.Value);
                        break;
                    default:
                        throw new Exception("no type given");
                }
                return cachedData;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Insert value into the cache using
        /// appropriate name/value pairs
        /// </summary>
        /// <param name="objectToCache">Item to be cached</param>
        /// <param name="cacheKey">Name of the item</param>
        //public static void Add(object objectToCache, string cacheKey)
        //{
        //    SummaryCache.Add(cacheKey, objectToCache, DateTime.Now.AddMinutes(5));
        //}

        /// <summary>
        /// Insert value into the cache using
        /// appropriate name/value pairs
        /// </summary>
        /// <typeparam name="T">Type of cached item</typeparam>
        /// <param name="objectToCache">Item to be cached</param>
        /// <param name="cacheKey">Name of item</param>
        public static void Add<T>(T objectToCache, string cacheKey, CacheType cacheType) where T : class
        {
            switch (cacheType)
            {
                case CacheType.Summary:
                    SummaryCache.Add(cacheKey, objectToCache, DateTime.Now.AddMinutes(10));
                    break;
                case CacheType.Details:
                    DetailsCache.Add(cacheKey, objectToCache, DateTime.Now.AddMinutes(10));
                    break;
                default:
                    throw new Exception("no type given");
                    break;

            }
            
        }

        /// <summary>
        /// Remove item from cache
        /// </summary>
        /// <param name="key">Name of cached item</param>
        public static void Clear(string key)
        {
            SummaryCache.Remove(key);
        }

        /// <summary>
        /// Check for item in cache
        /// </summary>
        /// <param name="key">Name of cached item</param>
        /// <returns></returns>
        public static bool Exists(string key)
        {
            return SummaryCache.Get(key) != null;
        }

        public static List<string> GetAllExistingKeys(string key)
        {
            List<string> list = new List<string>();
            foreach (var c in SummaryCache)
            {
                if (c.Key.Contains(key))
                {
                    list.Add(c.Key); 
                }
            }

            foreach (var d in DetailsCache)
            {
                if (d.Key.Contains(key))
                {
                    list.Add(d.Key);
                }
            }

            return list;
        }

        /// <summary>
        /// Gets all cached items as a list by their key.
        /// </summary>
        /// <returns></returns>
        public static List<string> GetAll()
        {
            return SummaryCache.Select(keyValuePair => keyValuePair.Key).ToList();
        }

        public enum CacheType
        {
            Summary,
            Details,
            Edit
        }

        internal static string GetViewReference(string hubSchemaId, CacheType cacheType)
        {
            try
            {
                switch (cacheType)
                {
                    case CacheType.Summary:
                        return SummaryCache.First(k => k.Key.Contains(hubSchemaId)).Key;
                    case CacheType.Details:
                        return DetailsCache.First(k => k.Key.Contains(hubSchemaId)).Key;
                    default:
                        throw new Exception("no type given");
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
