using System;
using System.Runtime.Caching;
using System.Collections.Specialized;

namespace IPinfo.Cache
{
    /// <summary>
    /// CacheWrapper for MemoryCache implementing ICache
    /// </summary>
    public sealed class CacheWrapper : ICache
    {
        private const string IPinfoCacheName = "IPinfoCache";
        
        // Version of cache, needs to be updated when launching such new version of library
        // which incorporates change in structure of json response being returned.
        private const string CacheKeyVsn = "1";
        private MemoryCache _memoryCache;
        private CacheConfigurations _config;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="CacheWrapper"/> class.
        /// </summary>
        public CacheWrapper():this(new CacheConfigurations.Builder().Build())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheWrapper"/> class.
        /// </summary>
        /// <param name="config"> configuration for <see cref="CacheWrapper"/>. </param>
        public CacheWrapper(CacheConfigurations config)
        {
            InitMemoryCache(config);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheWrapper"/> class.
        /// </summary>
        /// <param name="action"> Action. </param>
        /// <returns>Builder.</returns>
        public CacheWrapper(Action<CacheConfigurations.Builder> action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            
            CacheConfigurations.Builder configBuilder = new CacheConfigurations.Builder();
            action(configBuilder);

            InitMemoryCache(configBuilder.Build());
        }

        /// <summary>
        /// Initializes memory cache.
        /// </summary>
        /// <param name="config"> configuration for <see cref="CacheWrapper"/>. </param>
        private void InitMemoryCache(CacheConfigurations config)
        {
            this._config = config ?? throw new ArgumentNullException(nameof(config));

            _memoryCache = new MemoryCache(
                IPinfoCacheName,
                new NameValueCollection
                {
                    { "CacheMemoryLimitMegabytes", Convert.ToString(this._config.CacheMaxMbs) }
                }
            );
        }
        
        /// <summary>
        /// Returns cache entry against given key.
        /// </summary>
        /// <param name="key">A unique identifier for the cache entry to return.</param>
        /// <returns> An object that is identified by key, if the entry exists; otherwise, null.</returns>
        public object Get(string key)
        {
            return _memoryCache.Get(VersionedCacheKey(key));
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
        private string VersionedCacheKey(string key)
        {
            return $"{key}:{CacheKeyVsn}";
        }
    }
}
