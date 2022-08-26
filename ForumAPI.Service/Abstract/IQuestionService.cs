using ForumAPI.Contract.DeleteContract;
using ForumAPI.Contract.QuestionContract;
using ForumAPI.Data.Entity;
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
        Task<PaginationResponseContract<GetAllQuestionsContract>> GetNewestQuestions(PaginationContract paginationContract);
        Task<PaginationResponseContract<GetAllQuestionsContract>> GetQuestionsByDescendingVote(PaginationContract paginationContract);
        Task<PaginationResponseContract<GetAllQuestionsContract>> GetQuestionsByDescendingAnswer(PaginationContract paginationContract);
        Task AddQuestionToFavAsync(AddQuestionToFavContract addQuestionToFavContract);
        Task<QuestionDetailResponseContract> GetQuestionsWithDetail(int id, int userId);
        Task DeleteQuestion(DeleteContract deleteContract);
        Task DeleteFavorite(DeleteContract deleteContract);
        
    }
}
