using ForumAPI.Contract.AnswerContract;
using ForumAPI.Contract.QuestionContract;
using ForumAPI.Contract.UserContract;
using ForumAPI.Data.Abstract;
using ForumAPI.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumAPI.Data.Concrete
{
    public class QuestionRepository : GenericRepository<Question>, IQuestionRepository
    {
        private readonly DbSet<Question> _dbSet;
        
        public QuestionRepository(DataContext context) : base(context)
        {
            _dbSet = context.Set<Question>();
        }

        public async Task<List<GetAllQuestionsContract>> GetAllQuestionsWithDetails()
        {

            var allQuestions = await _dbSet.AsNoTracking().Select(p => new GetAllQuestionsContract
            {
                Title = p.Title,
                View = p.QuestionViews.Count(),
                Content = p.Content,
                Category = p.Category,
                Answer = p.Answers.Count(),
                Vote = p.Votes.Where(y => y.Voted == true).Count() - p.Votes.Where(z => z.Voted == false).Count(),
                User = new UserResponseContract
                {
                    Name = p.User.Name,
                    Id = p.User.Id,
                    Surname = p.User.Surname,
                    Image = p.User.Image
                }
            }).ToListAsync();

            return allQuestions;
        }

        public async Task<QuestionDetailContract> GetQuestionsWithDetail(int id)
        {
            var questionDetail = await _dbSet.Where(x => x.Id == id).Select(p => new QuestionDetailContract
            {
                Id = p.Id,
                Title = p.Title,
                View = p.QuestionViews.Count(),
                Content = p.Content,
                Category = p.Category,
                Answer = p.Answers.Count(),
                //Vote = p.Votes.Where(y => y.Voted == true).Count() - p.Votes.Where(z => z.Voted == false).Count(),
                User = new UserResponseContract
                {
                    Name = p.User.Name,
                    Id = p.User.Id,
                    Surname = p.User.Surname,
                    Image = p.User.Image
                },
                AnswerResponse = p.Answers.Select(q => new AnswerResponseContract
                {
                    User = new UserResponseContract
                    {
                        Name = q.User.Name,
                        Id = q.User.Id,
                        Surname = q.User.Surname,
                        Image = q.User.Image
                    },
                    Content = q.Content,
                    CreatedDateTime = q.CreatedTime
                }).ToList(),
                Favorite = p.Favorites.Where(x => x.IsDeleted == false).Count(),


            }).FirstOrDefaultAsync();
            return questionDetail;

        }
    }
}




