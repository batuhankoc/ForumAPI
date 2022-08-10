using ForumAPI.Contract.UserContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumAPI.Contract.AnswerContract
{
    public class AnswerResponseContract
    {
        public UserResponseContract User { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string Content { get; set; }

    }
}
