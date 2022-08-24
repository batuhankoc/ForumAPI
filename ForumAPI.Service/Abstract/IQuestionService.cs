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
        Task<List<GetAllQuestionsContract>> GetAllQuestionsWithDetails();
        Task AddQuestionToFavAsync(AddQuestionToFavContract addQuestionToFavContract);
        Task<QuestionDetailResponseContract> GetQuestionsWithDetail(int id, int userId);
        Task DeleteQuestion(DeleteContract deleteContract);
        Task DeleteFavorite(DeleteContract deleteContract);
        
    }
}
