using ForumAPI.Contract.QuestionContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumAPI.Cache.Interfaces
{
    public interface IQuestionsCache
    {
        public Task<List<GetAllQuestionsContract>> GetAllQuestionsWithDetails();
        public Task Remove();
    }
}
