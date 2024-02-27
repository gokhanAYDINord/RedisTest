using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace RedisTest.Services
{
    public class InMemoryCacheService : ICacheService
    {
        public readonly MemoryCache _cache = new MemoryCache(new MemoryCacheOptions());

        public Task<bool> ClearCache()
        {
            _cache.Clear();
            return Task.FromResult(true);
        }

        public Task<string> GetCacheValueAsync(string key)
        {
            return Task.FromResult(_cache.Get<string>(key));
        }

        public T GetData<T>(string key)
        {
            var value = _cache.Get<string>(key);
            if (!string.IsNullOrEmpty(value))
            {
                return JsonSerializer.Deserialize<T>(value);
            }
            else
            {
                return default;
            }
        }

        public Task<bool> RemoveCacheValueAsync(string key)
        {
            _cache.Remove(key);
            return Task.FromResult(true);
        }

        public object RemoveData(string key)
        {
            throw new NotImplementedException();
        }

        public Task SetCacheValueAsync(string key, string value)
        {
            _cache.Set(key, value);
            return Task.CompletedTask;
        }

        public bool SetData<T>(string key, T value, DateTimeOffset dateTimeOffset)
        {
            throw new NotImplementedException();
        }
    }
}
