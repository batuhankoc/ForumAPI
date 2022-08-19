using ForumAPI.Cache.Keys;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumAPI.Cache.Redis
{
    public class RedisCacheManager : IRedisCache
        {
        protected readonly IDistributedCache _distributedCache;

        public RedisCacheManager(IDistributedCache distributedCache)
        {
            _distributedCache=distributedCache;
        }

        public async Task<bool> Any(string key)
        {
            return await _distributedCache.GetAsync(key) != null;
        }

        public async Task<byte[]> Get(string key)
        {
            return await _distributedCache.GetAsync(key);
        }

        public async Task<T> Get<T>(string key)
        {
            var utf8String = Encoding.UTF8.GetString(await _distributedCache.GetAsync(key));
            var result = JsonConvert.DeserializeObject<T>(utf8String);
            return result;

        }

        public async Task Refresh(string key)
        {
            await _distributedCache.RefreshAsync(key);
        }

        public async Task Remove(string key)
        {
            await _distributedCache.RemoveAsync(key);
        }

        public async Task Set(string key, object value, TimeSpan timeSpan)
        {
            var options = new DistributedCacheEntryOptions()
            .SetAbsoluteExpiration(timeSpan);

            var serializedObject = JsonConvert.SerializeObject(value);
            var utf8String = Encoding.UTF8.GetBytes(serializedObject);
            await _distributedCache.SetAsync(key, utf8String, options);

        }
    }
}
