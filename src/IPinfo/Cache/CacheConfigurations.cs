namespace IPinfo.Cache
{
    public class CacheConfigurations
    {
        // The default max size of the cache in mbs.
        public const int DefaultCacheMaxSizeMbs = 1;
        
        // The default time to live of the cache entry in seconds
        public const long DefaultCacheTtl = 60 * 60 * 24;

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheConfigurations"/>
        /// class.
        /// </summary>
        private CacheConfigurations(
            int cacheMbs,
            long cacheTtl)
        {
            this.CacheMaxMbs = cacheMbs;
            this.CacheTtl = cacheTtl;
        }
        
        /// <summary>
        /// Gets max size of the cache in mbs.
        /// </summary>
        public int CacheMaxMbs { get; }
        
        /// <summary>
        /// Gets time to live of the cache entry in seconds.
        /// </summary>
        public long CacheTtl { get; }

        /// <summary>
        /// Builder class.
        /// </summary>
        public class Builder
        {
            private int _cacheMbs = DefaultCacheMaxSizeMbs;
            private long _cacheTtl = DefaultCacheTtl;

            /// <summary>
            /// Sets CacheMaxMbs.
            /// </summary>
            /// <param name="cacheMbs"> Cache size in MBs. </param>
            /// <returns>Builder.</returns>
            public Builder CacheMaxMbs(int cacheMbs)
            {
                this._cacheMbs = cacheMbs;
                return this;
            }

            /// <summary>
            /// Sets the Cache time to live.
            /// </summary>
            /// <param name="cacheTtl"> Cache entry time to live in seconds. </param>
            /// <returns>Builder.</returns>
            public Builder CacheTtl(long cacheTtl)
            {
                this._cacheTtl = cacheTtl;
                return this;
            }

            /// <summary>
            /// Creates an object of the CacheConfigurations using the values provided for the builder.
            /// </summary>
            /// <returns>CacheConfigurations.</returns>
            public CacheConfigurations Build()
            {
                return new CacheConfigurations(
                    this._cacheMbs,
                    this._cacheTtl);
            }
        }
    }
}