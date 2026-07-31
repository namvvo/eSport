using StackExchange.Redis;


namespace eSport.ServiceDefaults.Infrastructure;

public sealed class RedisCache
{
    private readonly StackExchange.Redis.IDatabase _db;
    public RedisCache(IConnectionMultiplexer redis)
    {
        _db = redis.GetDatabase();
    }
    public Task<RedisValue> GetAsync(string key)
      => _db.StringGetAsync(key);
    public Task<RedisValue[]> GetAsync(RedisKey[] keys)
      => _db.StringGetAsync(keys);
    public Task<bool> SetAsync(
        string key,
        string value,
        TimeSpan ttl)
        => _db.StringSetAsync(key, value, ttl);
}
