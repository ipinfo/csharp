namespace IPinfo.Cache
{
    /// <summary>
    /// The CacheHandler will handle the cache operations.
    /// </summary>
    internal sealed class CacheHandler
    {
        // Implementation of ICache
        private ICache _cacheImplmentation;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="CacheHandler"/> class.
        /// </summary>
        /// <param name="cacheImplmentation"> any custom ICache implemetation. </param>
        internal CacheHandler(ICache cacheImplmentation)
        {
            this._cacheImplmentation = cacheImplmentation;
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="CacheHandler"/> class with default cache.
        /// </summary>
        internal CacheHandler() : this(new CacheWrapper())
        {
        }
        
        /// <summary>
        /// Returns cache implementation being used.
        /// </summary>
        internal ICache Cache { get => _cacheImplmentation; }

        /// <summary>
        /// Returns cache entry against given key.
        /// </summary>
        /// <param name="key">A unique identifier for the cache entry to be returned.</param>
        /// <returns> An object that is identified by key, if the entry exists; otherwise, null.</returns>
        internal object Get(string key)
        {
            return _cacheImplmentation.Get(key);
        }

        /// <summary>
        /// If the specified entry does not exist, it is created. If the specified entry exists, it is updated.
        /// </summary>
        /// <param name="key">A unique identifier for cache entry.</param>
        /// <param name="value">The data for a cache entry.</param>
        internal void Set(string key, object value)
        {
            _cacheImplmentation.Set(key, value);
        }
    }
}
