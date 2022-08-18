using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumAPI.Cache.Redis
{
    public interface IRedisCache
    {
        Task<byte[]> Get(string key);
        Task<T> Get<T>(string key);
        Task Set(string key, object value, TimeSpan timeSpan);
        Task Refresh(string key);
        Task<bool> Any(string key);
        Task Remove(string key);

    }
}
