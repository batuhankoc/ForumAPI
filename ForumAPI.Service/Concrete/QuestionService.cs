using AutoMapper;
using ForumAPI.Cache.Redis;
using ForumAPI.Contract.QuestionContract;
using ForumAPI.Data.Abstract;
using ForumAPI.Data.Entity;
using ForumAPI.Service.Abstract;
using ForumAPI.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumAPI.Service.Concrete
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;
        private readonly IFavoriteRepository _favoriteRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRedisCache _redisCache;
        private readonly string GetAllQuestionsContractKey = "GetAllQuestionsContract";
        private readonly string QuestionDetailResponseContractKey = "QuestionDetailResponseContract_{0}";

        public QuestionService(IQuestionRepository questionRepository, IMapper mapper, IFavoriteRepository favoriteRepository, IUserRepository userRepository, IRedisCache redisCache)
        {
            _questionRepository=questionRepository;
            _mapper=mapper;
            _favoriteRepository=favoriteRepository;
            _userRepository=userRepository;
            _redisCache=redisCache;
        }

        public async Task AddQuestionAsync(AddQuestionContract addQuestionContract)
        {
            var addQuestion = _mapper.Map<Question>(addQuestionContract);
            await _questionRepository.AddAsync(addQuestion);
            await _redisCache.Remove(GetAllQuestionsContractKey);

        }

        public async Task AddQuestionToFavAsync(AddQuestionToFavContract addQuestionToFavContract)
        {
            var user = await _userRepository.GetByIdAsync(addQuestionToFavContract.UserId);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }

            var question = await _questionRepository.GetByIdAsync(addQuestionToFavContract.QuestionId);
            if (question == null)
            {
                throw new NotFoundException("Question not found");
            }

            var favorite = await _favoriteRepository.CheckFavorite(addQuestionToFavContract.QuestionId,
                addQuestionToFavContract.UserId);
            if (favorite)
            {
                throw new NotFoundException("Question already in favorites");
            }

            var model = _mapper.Map<Favorite>(addQuestionToFavContract);
            await _favoriteRepository.AddAsync(model);
            await _redisCache.Remove(QuestionDetailResponseContractKey);
        }  

        public async Task<List<GetAllQuestionsContract>> GetAllQuestionsWithDetails()
        {
            
            if (await _redisCache.Any(GetAllQuestionsContractKey))
            {
                return await _redisCache.Get<List<GetAllQuestionsContract>>(GetAllQuestionsContractKey);
            }
            var questions = await _questionRepository.GetAllQuestionsWithDetails();
            await _redisCache.Set(GetAllQuestionsContractKey, questions, TimeSpan.FromSeconds(120));
            return questions.OrderByDescending(x => x.CreatedDateTime).ToList();
        }


        public async Task<QuestionDetailResponseContract> GetQuestionsWithDetail(int id, int userId)
        {
            var cacheKey = string.Format(QuestionDetailResponseContractKey, id);
           if (await _redisCache.Any(cacheKey))
            {
                return await _redisCache.Get<QuestionDetailResponseContract>(cacheKey);
            }
           var questionDb = await _questionRepository.GetQuestionsWithDetail(id);
           questionDb.IsFavorite = await _favoriteRepository.CheckFavorite(questionDb.Id, userId);
           var question = _mapper.Map<QuestionDetailResponseContract>(questionDb);
           await _redisCache.Set(cacheKey, question, TimeSpan.FromMinutes(2)); 
           return question;
        }
    }
}
