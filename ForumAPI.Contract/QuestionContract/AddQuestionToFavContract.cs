using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumAPI.Contract.QuestionContract
{
    public class AddQuestionToFavContract
    {
       // public int Id { get; set; }
        public int UserId { get; set; }
        public int QuestionId { get; set; }
        
    }
}
