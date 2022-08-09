using ForumAPI.Contract.VoteContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumAPI.Service.Abstract
{
    public interface IVoteService
    {
        Task AddVote(AddVoteContract addVoteContract);
    }
}
