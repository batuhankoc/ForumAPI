using AutoMapper;
using ForumAPI.Cache.Interfaces;
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
        private readonly IFavoriteCache _favoriteCache;
        private readonly IQuestionDetailCache _questionDetailCache;
        private readonly IQuestionsCache _questionsCache;

        public QuestionService(IQuestionRepository questionRepository, IMapper mapper, IFavoriteRepository favoriteRepository,
            IUserRepository userRepository, IFavoriteCache favoriteCache = null, IQuestionDetailCache questionDetailCache = null, IQuestionsCache questionsCache = null)
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
            _favoriteRepository = favoriteRepository;
            _userRepository = userRepository;
            _favoriteCache = favoriteCache;
            _questionDetailCache = questionDetailCache;
            _questionsCache = questionsCache;
        }

        public async Task AddQuestionAsync(AddQuestionContract addQuestionContract)
        {
            var addQuestion = _mapper.Map<Question>(addQuestionContract);
            await _questionRepository.AddAsync(addQuestion);
            await _questionsCache.Remove();
        }

        public async Task AddQuestionToFavAsync(AddQuestionToFavContract addQuestionToFavContract)
        {
            await AddQuestionToFavHelper(addQuestionToFavContract);
            var model = _mapper.Map<Favorite>(addQuestionToFavContract);
            await _favoriteRepository.AddAsync(model);
            await _questionsCache.Remove();
        }

        public async Task<List<GetAllQuestionsContract>> GetAllQuestionsWithDetails()
        {
            var questions = _questionsCache.GetAllQuestionsWithDetails();
            return await questions;
        }

        public async Task<QuestionDetailResponseContract> GetQuestionsWithDetail(int id, int userId)
        {
            var isFavorite = await _favoriteCache.CheckFav(id, userId);
            var questionResponse = await _questionDetailCache.GetQuestionsWithDetail(id);
            questionResponse.IsFavorite = isFavorite;
            return questionResponse;
        }

        private async Task AddQuestionToFavHelper(AddQuestionToFavContract addQuestionToFavContract)
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
        }

    }
}
