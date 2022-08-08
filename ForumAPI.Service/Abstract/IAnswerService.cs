using ForumAPI.Contract.AnswerContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumAPI.Service.Abstract
{
    public interface IAnswerService
    {
        Task AddAnswerAsync(AddAnswerContract addAnswerContract);
        
    }
}
