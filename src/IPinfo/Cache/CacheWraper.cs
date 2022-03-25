using System;
using System.Runtime.Caching;
using System.Collections.Specialized;

namespace IPinfo.Cache
{
    public sealed class CacheWraper : ICache
    {
        // IPinfo cache name
        private const string IPinfoCacheName = "IPinfoCache";
        
        // Version of cache, needs to be updated when launching new version of library which incorporates change in structure of json response being returned
        private const string CacheKeyVsn = "1";
        private MemoryCache _memoryCache;
        private CacheConfigurations _config;
        
        public CacheWraper():this(new CacheConfigurations())
        {

        }

        public CacheWraper(CacheConfigurations config)
        {
            this._config = config ?? throw new ArgumentNullException(nameof(config));
            _memoryCache = new MemoryCache(
                IPinfoCacheName,
                new NameValueCollection
                {
                    { "CacheMemoryLimitMegabytes", Convert.ToString(config.CacheMaxMbs) }
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
            return _memoryCache.Get(VersionedCacheKey(key));
        }

        /// <summary>
        /// Removes cache entry against given key.
        /// </summary>
        /// <param name="key">A unique identifier for the cache entry to remove.</param>
        /// <returns> If the entry is found in the cache, the removed cache entry; otherwise, null.</returns>
        public object Remove(string key)
        {
            return _memoryCache.Remove(VersionedCacheKey(key));
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
                AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(_config.CacheTtl)
            };  
            _memoryCache.Set(VersionedCacheKey(key), value, cacheItemPolicy);
        }

        /// <summary>
        /// Transforms a key into a versioned cache key.
        /// </summary>
        /// <param name="key">The key to be converted into versioned key.</param>
        /// <returns>The versioned key of the provided key.</returns>
        private static string VersionedCacheKey(string key)
        {
            return $"{key}:{CacheKeyVsn}";
        }
    }
}