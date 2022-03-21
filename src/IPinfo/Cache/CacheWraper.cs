using System;
using System.Runtime.Caching;
using System.Collections.Specialized;

namespace IPinfo.Cache
{
    public class CacheWraper : ICache
    {
        // IPinfo cache name
        private const string IPINFO_CACHE_NAME = "IPinfoCache";
        
        private MemoryCache memoryCache;
        private CacheConfigurations config;
        
        public CacheWraper():this(new CacheConfigurations())
        {

        }

        public CacheWraper(CacheConfigurations config)
        {
            this.config = config ?? throw new ArgumentNullException(nameof(config));
            memoryCache = new MemoryCache(
                IPINFO_CACHE_NAME,
                new NameValueCollection
                {
                    { "CacheMemoryLimitMegabytes", Convert.ToString(config.CacheMaxMBs) }
                }
            );
        }
        
        /// <summary>
        /// Returns cache entry against given key.
        /// </summary>
        /// <param name="key">A unique identifier for the cache entry to remove.</param>
        /// <returns> An object that is identified by key, if the entry exists; otherwise, null.</returns>
        public object Get(string key)
        {
            return memoryCache.Get(key);
        }

        /// <summary>
        /// Removes cache entry against given key.
        /// </summary>
        /// <param name="key">A unique identifier for the cache entry to remove.</param>
        /// <returns> If the entry is found in the cache, the removed cache entry; otherwise, null.</returns>
        public object Remove(string key)
        {
            return memoryCache.Remove(key);
        }

        /// <summary>
        /// If the specified entry does not exist, it is created. If the specified entry exists, it is updated.
        /// </summary>
        /// <param name="key">A unique identifier for cache entry.</param>
        /// <param name="value">The data for a cache entry.</param>
        public void Set(string key, object value)
        {
            var cacheItemPolicy = new CacheItemPolicy  
            {  
                AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(config.CacheTTL)
            };  
            memoryCache.Set(key, value, cacheItemPolicy);
        }
    }
}
