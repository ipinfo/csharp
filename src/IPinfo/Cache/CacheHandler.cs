namespace IPinfo.Cache
{
    public class CacheHandler
    {
        // Version of cache, needs to be updated when launching new version of library which incorporates change in structure of json response being returned
        private const string CACHE_KEY_VSN = "1";
        
        // Implementation of ICache
        private ICache cacheImplmentation;
        
        public CacheHandler(ICache cacheImplmentation)
        {
            this.cacheImplmentation = cacheImplmentation;
        }
        
        public CacheHandler() : this(new CacheWraper())
        {
        }
        
        /// <summary>
        /// Returns cache entry against given key.
        /// </summary>
        /// <param name="key">A unique identifier for the cache entry to remove.</param>
        /// <returns> An object that is identified by key, if the entry exists; otherwise, null.</returns>
        public object Get(string key)
        {
            return cacheImplmentation.Get(VersionedCacheKey(key));
        }

        /// <summary>
        /// Removes cache entry against given key.
        /// </summary>
        /// <param name="key">A unique identifier for the cache entry to remove.</param>
        /// <returns> If the entry is found in the cache, the removed cache entry; otherwise, null.</returns>
        public object Remove(string key)
        {
            return cacheImplmentation.Remove(VersionedCacheKey(key));
        }

        /// <summary>
        /// If the specified entry does not exist, it is created. If the specified entry exists, it is updated.
        /// </summary>
        /// <param name="key">A unique identifier for cache entry.</param>
        /// <param name="value">The data for a cache entry.</param>
        public void Set(string key, object value)
        {
            cacheImplmentation.Set(VersionedCacheKey(key), value);
        }

        /// <summary>
        /// Transforms a key into a versioned cache key.
        /// </summary>
        /// <param name="key">The key to be converted into versioned key.</param>
        /// <returns>The versioned key of the provided key.</returns>
        private static string VersionedCacheKey(string key)
        {
            return $"{key}:{CACHE_KEY_VSN}";
        }
    }
}
