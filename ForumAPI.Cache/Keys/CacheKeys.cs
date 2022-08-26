using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumAPI.Cache.Keys
{
    public class CacheKeys
    {
        public const string FavoriteCacheKey = "FavoriteCache_{0}_{1}";
        public const string QuestionDetailCacheKey = "QuestionDetail_{0}";
        public const string NumberOfVoteKey = "NumberOfVote_{0}";
        public const string GetVoteKey = "GetVote_{0}_{1}";
    }
}
