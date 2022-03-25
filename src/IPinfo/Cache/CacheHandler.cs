namespace IPinfo.Cache
{
    public sealed class CacheHandler
    {
        // Implementation of ICache
        private ICache _cacheImplmentation;
        
        public CacheHandler(ICache cacheImplmentation)
        {
            this._cacheImplmentation = cacheImplmentation;
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
            return _cacheImplmentation.Get(key);
        }

        /// <summary>
        /// Removes cache entry against given key.
        /// </summary>
        /// <param name="key">A unique identifier for the cache entry to remove.</param>
        /// <returns> If the entry is found in the cache, the removed cache entry; otherwise, null.</returns>
        public object Remove(string key)
        {
            return _cacheImplmentation.Remove(key);
        }

        /// <summary>
        /// If the specified entry does not exist, it is created. If the specified entry exists, it is updated.
        /// </summary>
        /// <param name="key">A unique identifier for cache entry.</param>
        /// <param name="value">The data for a cache entry.</param>
        public void Set(string key, object value)
        {
            _cacheImplmentation.Set(key, value);
        }
    }
}