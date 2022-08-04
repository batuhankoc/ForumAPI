using ForumAPI.Contract.UserContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumAPI.Service.Abstract
{
    public interface IQuestionService
    {
        Task AddQuestionAsync(AddQuestionContract addQuestionContract);
    }
}
