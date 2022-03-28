namespace IPinfo.Cache
{
    /// <summary>
    /// ICache.
    /// </summary>
    public interface ICache
    {
        /// <summary>
        /// Returns cache entry against given key.
        /// </summary>
        /// <param name="key">A unique identifier for the cache entry to return.</param>
        /// <returns> An object that is identified by key, if the entry exists; otherwise, null.</returns>
        object Get(string key);

        /// <summary>
        /// Removes cache entry against given key.
        /// </summary>
        /// <param name="key">A unique identifier for the cache entry to remove.</param>
        /// <returns> If the entry is found in the cache, the removed cache entry; otherwise, null.</returns>
        object Remove(string key);

        /// <summary>
        /// If the specified entry does not exist, it is created. If the specified entry exists, it is updated.
        /// </summary>
        /// <param name="key">A unique identifier for cache entry.</param>
        /// <param name="value">The data for a cache entry.</param>
        void Set(string key, object value);
    }
}