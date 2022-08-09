using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumAPI.Contract.VoteContract
{
    public class AddVoteContract
    {
        public int UserId { get; set; }
        public int QuestionId { get; set; }
        public bool? Voted { get; set; }
    }
}
