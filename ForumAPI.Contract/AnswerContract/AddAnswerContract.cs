using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumAPI.Contract.AnswerContract
{
    public class AddAnswerContract
    {
        public int UserId { get; set; }
        public int QuestionId { get; set; }
        public string Content { get; set; }
    }
}
