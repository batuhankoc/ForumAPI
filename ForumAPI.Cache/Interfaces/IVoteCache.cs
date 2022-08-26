using ForumAPI.Cache.Redis;
using ForumAPI.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumAPI.Cache.Interfaces
{
    public interface IVoteCache : IRedisCache
    {
        public Task<int> GetNumberOfVotes(int questionId);
        public Task<Vote> GetVote(int questionId, int userId);
    }
}
