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

        public async Task<PaginationResponseContract<GetAllQuestionsContract>> GetNewestQuestions(PaginationContract paginationContract)
        {
            var totalData = await _dbSet.CountAsync();
            var pageSize = paginationContract.PageSize;
            var page = paginationContract.Page;
            var totalPage = Math.Ceiling(Convert.ToDecimal(totalData)/ Convert.ToDecimal(pageSize));
            var paginationResponseContract = new PaginationResponseContract<GetAllQuestionsContract>
            {
               Pagination = new PaginationContract 
               { 
                   Page=page,
                   PageSize=pageSize,
                   TotalData=totalData,
                   TotalPage=Convert.ToInt32( totalPage)

                },
                Data = await _dbSet.AsNoTracking().OrderByDescending(c => c.Id)
                 .Skip((page - 1) * pageSize).Take(pageSize).Select(p => new GetAllQuestionsContract
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
                 }).ToListAsync()
            };
            return paginationResponseContract;
        }
        public async Task<PaginationResponseContract<GetAllQuestionsContract>> GetQuestionsByDescendingVote (PaginationContract paginationContract)
        {
            var totalData = await _dbSet.CountAsync();
            var pageSize = paginationContract.PageSize;
            var page = paginationContract.Page;
            var totalPage = Math.Ceiling(Convert.ToDecimal(totalData)/ Convert.ToDecimal(pageSize));
            var paginationResponseContract = new PaginationResponseContract<GetAllQuestionsContract>
            {
                Pagination = new PaginationContract
                {
                    Page = page,
                    PageSize = pageSize,
                    TotalData = totalData,
                    TotalPage = Convert.ToInt32(totalPage)

                },
                Data = await _dbSet.AsNoTracking().OrderByDescending(c => c.Votes.Where(y => y.Voted == true).Count() - c.Votes.Where(z => z.Voted == false).Count())
                 .Skip((page - 1) * pageSize).Take(pageSize).Select(p => new GetAllQuestionsContract
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
                 }).ToListAsync()
            };
            return paginationResponseContract;
        }

        public async Task<PaginationResponseContract<GetAllQuestionsContract>> GetQuestionsByDescendingAnswer(PaginationContract paginationContract)
        {
            var totalData = await _dbSet.CountAsync();
            var pageSize = paginationContract.PageSize;
            var page = paginationContract.Page;
            var totalPage = Math.Ceiling(Convert.ToDecimal(totalData)/ Convert.ToDecimal(pageSize));
            var paginationResponseContract = new PaginationResponseContract<GetAllQuestionsContract>
            {
                Pagination = new PaginationContract
                {
                    Page = page,
                    PageSize = pageSize,
                    TotalData = totalData,
                    TotalPage = Convert.ToInt32(totalPage)

                },
                Data = await _dbSet.AsNoTracking().OrderByDescending(c => c.Answers.Count)
                 .Skip((page - 1) * pageSize).Take(pageSize).Select(p => new GetAllQuestionsContract
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
                 }).ToListAsync()
            };
            return paginationResponseContract;
        }


        public async Task<QuestionDetailResponseContract> GetQuestionsWithDetail(int id)
        {
            var questionDetail = await _dbSet.Where(x => x.Id == id).Select(p => new QuestionDetailResponseContract
            {
                Title = p.Title,
                View = p.QuestionViews.Count(),
                Content = p.Content,
                Category = p.Category,
                Answer = p.Answers.Count(),
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





