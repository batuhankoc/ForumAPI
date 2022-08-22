using ForumAPI.Cache.Interfaces;
using ForumAPI.Cache.Keys;
using ForumAPI.Cache.Redis;
using ForumAPI.Data.Abstract;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumAPI.Cache.Concrete
{
    public class VoteCache : RedisCacheManager, IVoteCache
    {
        private readonly IVoteRepository _voteRepository;
        public VoteCache(IDistributedCache distributedCache, IVoteRepository voteRepository) : base(distributedCache)
        {
            _voteRepository = voteRepository;
        }

        public async Task<int> GetNumberOfVotes(int questionId)
        {
            string cacheKey = string.Format(CacheKeys.NumberOfVoteKey, questionId);
            if(await Any(cacheKey))
            {
                return await Get<int>(cacheKey);
            }
            var numberOfVoted = await _voteRepository.GetNumberOfVotes(questionId);
            await Set(cacheKey, numberOfVoted, TimeSpan.FromMinutes(10));
            return numberOfVoted;
        }
    }
}
