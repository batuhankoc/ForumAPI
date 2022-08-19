using ForumAPI.Cache.Interfaces;
using ForumAPI.Cache.Keys;
using ForumAPI.Cache.Redis;
using ForumAPI.Contract.QuestionContract;
using ForumAPI.Data.Abstract;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumAPI.Cache.Concrete
{
    public class FavoriteCache : RedisCacheManager, IFavoriteCache
    {
        private readonly IFavoriteRepository _favoriteRepository;
        public FavoriteCache(IDistributedCache distributedCache, IFavoriteRepository favoriteRepository) : base(distributedCache)
        {
            _favoriteRepository = favoriteRepository;
        }
        public async Task<bool> CheckFav(int id, int userId)
        {
            string cacheKey = string.Format(CacheKeys.FavoriteCacheKey, id, userId);
            if (await Any(cacheKey))
            {
                return await Get<bool>(cacheKey);
            }
            bool isFavorite = await _favoriteRepository.CheckFavorite(id, userId);
            if (isFavorite)
                await Set(cacheKey, isFavorite, TimeSpan.FromMinutes(10));
            else
                await Set(cacheKey, false, TimeSpan.FromMinutes(10));
            return isFavorite;
        }
        public async Task RemoveFavoriteCache(int id, int userId)
        {
            string cacheKey = string.Format(CacheKeys.FavoriteCacheKey, id, userId);
            await Remove(cacheKey);
        }
    }
}
