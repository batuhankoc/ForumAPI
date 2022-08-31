using AutoMapper;
using ForumAPI.Cache.Interfaces;
using ForumAPI.Cache.Keys;
using ForumAPI.Cache.Redis;
using ForumAPI.Contract.QuestionContract;
using ForumAPI.Data.Abstract;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumAPI.Cache.Concrete
{
    public class QuestionDetailCache : RedisCacheManager, IQuestionDetailCache
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IFavoriteCache _favoriteCache;
        private readonly IMapper _mapper;
        public QuestionDetailCache(IDistributedCache distributedCache, IQuestionRepository questionRepository, IFavoriteCache favoriteCache, IMapper mapper) : base(distributedCache)
        {
            _questionRepository = questionRepository;
            _favoriteCache = favoriteCache;
            _mapper = mapper;
        }

        public async Task<QuestionDetailResponseContract> GetQuestionsWithDetail(int id)
        {
            string cacheKey = string.Format(CacheKeys.QuestionDetailCacheKey, id);
            if (await Any(cacheKey))
            {
                var questionFromCache = await Get<QuestionDetailResponseContract>(cacheKey);
                return questionFromCache;
            }
            var questionDb = await _questionRepository.GetQuestionsWithDetail(id);
            await Set(cacheKey, questionDb, TimeSpan.FromMinutes(10));
            return questionDb;
        }
    }
}
