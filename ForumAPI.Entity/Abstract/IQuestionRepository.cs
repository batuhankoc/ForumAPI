using ForumAPI.Contract.QuestionContract;
using ForumAPI.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumAPI.Data.Abstract
{
    public interface IQuestionRepository : IGenericRepository<Question> 
    {
        Task<PaginationResponseContract<GetAllQuestionsContract>> GetNewestQuestions(PaginationContract paginationContract);
        Task<PaginationResponseContract<GetAllQuestionsContract>> GetQuestionsByDescendingVote(PaginationContract paginationContract);
        Task<PaginationResponseContract<GetAllQuestionsContract>> GetQuestionsByDescendingAnswer(PaginationContract paginationContract);

        Task<QuestionDetailResponseContract> GetQuestionsWithDetail(int id);
    }
}
