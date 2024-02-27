using StackExchange.Redis;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace RedisTest.Services
{
    public class RedisCacheService : ICacheService
    {
        private readonly IConnectionMultiplexer _redisConnection;
        private readonly IDatabase _database;
        public RedisCacheService(IConnectionMultiplexer connectionMultiplexer)
        {
            _redisConnection = connectionMultiplexer;
            _database = _redisConnection.GetDatabase();  
        }
        public async Task<string> GetCacheValueAsync(string key)
        {
            return await _database.StringGetAsync(key);
        }        

        public Task<bool> RemoveCacheValueAsync(string key)
        {
            return _database.KeyDeleteAsync(key); 
        }
        
        public async Task SetCacheValueAsync(string key, string value)
        {
            await _database.StringSetAsync(key, value);
        }

        public T GetData<T>(string key)
        {
            var value = _database.StringGet(key);
            if(!string.IsNullOrEmpty(value))
            {
               return JsonSerializer.Deserialize<T>(value); 
            }
            else
            {
                return default;
            }
        }

        public object RemoveData(string key)
        {
            var exist = _database.KeyExists(key);
            if(exist)
            {
                _database.KeyDelete(key);
            }
            return false;
        }

        public bool SetData<T>(string key, T value, DateTimeOffset dateTimeOffset)
        {
            var expirtyTime = dateTimeOffset.DateTime.Subtract(DateTime.Now);
            return _database.StringSet(key, JsonSerializer.Serialize(value), expirtyTime);
        }

        public Task<bool> ClearCache()
        {
            var endpoints = _redisConnection.GetEndPoints();
            var server = _redisConnection.GetServer(endpoints[0]);
            server.FlushAllDatabases();
            return Task.FromResult(true);
        }
    }
}
