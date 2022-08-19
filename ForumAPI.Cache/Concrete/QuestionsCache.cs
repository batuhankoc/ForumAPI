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
    public class QuestionsCache : RedisCacheManager, IQuestionsCache
    {
        private readonly IQuestionRepository _questionRepository;
        private const string cacheKey = CacheKeys.QuestionsKey;

        public QuestionsCache(IDistributedCache distributedCache, IQuestionRepository questionRepository) : base(distributedCache)
        {
            _questionRepository = questionRepository;
        }

        public async Task<List<GetAllQuestionsContract>> GetAllQuestionsWithDetails()
        {
            if(await Any(cacheKey))
            {
                return await Get<List<GetAllQuestionsContract>>(cacheKey);
            }
            var questions = await _questionRepository.GetAllQuestionsWithDetails();
            await Set(cacheKey, questions, TimeSpan.FromMinutes(15));
            return questions;
        }
        public async Task Remove()
        {
            await Remove(cacheKey);
        }
    }
}
