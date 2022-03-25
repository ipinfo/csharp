namespace IPinfo.Cache
{
    public sealed class CacheConfigurations
    {
        // The default max size of the cache in mbs.
        public const int DefaultCacheMaxSizeMbs = 1;
        
        // The default time to live of the cache entry in seconds
        public const long DefaultCacheTtl = 60 * 60 * 24;
        
        // The max size of the cache in mbs.
        public int CacheMaxMbs { get; set; } = DefaultCacheMaxSizeMbs;
        
        // The time to live of the cache entry in seconds
        public long CacheTtl { get; set; } = DefaultCacheTtl;
    }
}