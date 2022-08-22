using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumAPI.Cache.Interfaces
{
    public interface IVoteCache
    {
        public Task<int> GetNumberOfVotes(int questionId);
    }
}
