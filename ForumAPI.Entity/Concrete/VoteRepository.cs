using ForumAPI.Data.Abstract;
using ForumAPI.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumAPI.Data.Concrete
{
    public class VoteRepository : GenericRepository<Vote>, IVoteRepository
    {
        private readonly DbSet<Vote> _vote;

        public VoteRepository(DataContext context) : base(context)
        {
            _vote = context.Set<Vote>();
        }
        public async Task<bool> CheckVote(int questionId, int userId)
        {
            return await _vote.AnyAsync(x => x.QuestionId == questionId && x.UserId == userId);
        }
        public async Task<Vote?> GetVote(int questionId, int userId)
        {
            return await _vote.FirstOrDefaultAsync(x => x.QuestionId.Equals(questionId) && x.UserId.Equals(userId) );
        }

        public async Task<int> GetNumberOfVotes(int questionId)
        {
            var trueVotes = await _vote.Where(x => x.QuestionId.Equals(questionId)).Where(y => y.Voted==true).CountAsync();
            var falseVotes = await _vote.Where(x => x.QuestionId.Equals(questionId)).Where(y => y.Voted==false).CountAsync();
            return trueVotes - falseVotes;
        }
    }
}
