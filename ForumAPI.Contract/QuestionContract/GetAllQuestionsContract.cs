using ForumAPI.Contract.UserContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumAPI.Contract.QuestionContract
{
    public class GetAllQuestionsContract
    {
        public UserResponseContract User { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string Category { get; set; }
        public int Vote { get; set; }
        public int View { get; set; }
        public int Answer { get; set; }
    }

}
