namespace IPinfo.Cache
{
    public class CacheConfigurations
    {
        // The default max size of the cache in mbs.
        public const int DEFAULT_CACHE_MAX_SIZE_MBS = 1;
        
        // The default time to live of the cache entry in seconds
        public const long DEFAULT_CACHE_TTL = 60 * 60 * 24;
        
        // The max size of the cache in mbs.
        public int CacheMaxMBs { get; set; } = DEFAULT_CACHE_MAX_SIZE_MBS;
        
        // The time to live of the cache entry in seconds
        public long CacheTTL { get; set; } = DEFAULT_CACHE_TTL;
    }
}
