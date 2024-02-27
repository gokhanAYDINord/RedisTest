using System;
using System.Threading.Tasks;

namespace RedisTest.Services
{
    public interface ICacheService
    {
        Task<string> GetCacheValueAsync(string key);
        Task<bool> RemoveCacheValueAsync(string key);
        Task SetCacheValueAsync(string key, string value);

        T GetData<T>(string key);
        bool SetData<T>(string key, T value, DateTimeOffset dateTimeOffset);
        object RemoveData(string key);
        Task<bool> ClearCache();
    }
}
