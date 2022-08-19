using ForumAPI.Contract.QuestionContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumAPI.Cache.Interfaces
{
    public interface IQuestionDetailCache
    {
        public Task<QuestionDetailResponseContract> GetQuestionsWithDetail(int id);
        public Task Set(object value, TimeSpan timeSpan);
    }
}
