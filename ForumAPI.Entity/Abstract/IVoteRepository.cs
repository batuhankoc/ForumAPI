using ForumAPI.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumAPI.Data.Abstract
{
    public interface IVoteRepository : IGenericRepository<Vote>
    {
        Task<bool> CheckVote(int questionId, int userId);
        Task<Vote?> GetVote(int questionId, int userId);
        Task<int> GetNumberOfVotes(int questionId);

    }
}
