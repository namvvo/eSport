//namespace eSport.Catalog.API.Infrastructure;

//public sealed class CatalogCache
//{
//    private readonly IDatabase _db;
//    public CatalogCache(IConnectionMultiplexer redis)
//    {
//        _db = redis.GetDatabase();
//    }
//    public Task<RedisValue> GetAsync(string key)
//      => _db.StringGetAsync(key);

//    public Task<bool> SetAsync(
//        string key,
//        string value,
//        TimeSpan ttl)
//        => _db.StringSetAsync(key, value, ttl);
//}
