using AutoMapper;
using ForumAPI.Cache.Interfaces;
using ForumAPI.Cache.Redis;
using ForumAPI.Contract.DeleteContract;
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
        private readonly IVoteCache _voteCache;

        public QuestionService(IQuestionRepository questionRepository, IMapper mapper, IFavoriteRepository favoriteRepository,
            IUserRepository userRepository, IFavoriteCache favoriteCache = null, IQuestionDetailCache questionDetailCache = null, IVoteCache voteCache = null)
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
            _favoriteRepository = favoriteRepository;
            _userRepository = userRepository;
            _favoriteCache = favoriteCache;
            _questionDetailCache = questionDetailCache;
            _voteCache = voteCache;
        }

        public async Task AddQuestionAsync(AddQuestionContract addQuestionContract)
        {
            var addQuestion = _mapper.Map<Question>(addQuestionContract);
            await _questionRepository.AddAsync(addQuestion);
        }

        public async Task AddQuestionToFavAsync(AddQuestionToFavContract addQuestionToFavContract)
        {
            await CheckIfUserFavorited(addQuestionToFavContract);
            await AddQuestionToFavHelper(addQuestionToFavContract);
            var model = _mapper.Map<Favorite>(addQuestionToFavContract);
            await _favoriteRepository.AddAsync(model);
            await _favoriteCache.RemoveFavoriteCache(addQuestionToFavContract.QuestionId, addQuestionToFavContract.UserId);
        }

        public async Task DeleteFavorite(DeleteContract deleteContract)
        {
            var dbFavorite = await _favoriteRepository.GetByIdAsync(deleteContract.Id);
            if (dbFavorite == null)
            {
                throw new ClientSideException("Favorite Bulunumadı");
            }
            await _favoriteRepository.RemoveAsync(dbFavorite);
        }

        public async Task DeleteQuestion(DeleteContract deleteContract)
        {
            var dbQuestion = await _questionRepository.GetByIdAsync(deleteContract.Id);
            if (dbQuestion == null)
            {
                throw new ClientSideException("Soru bulunamadı");
            }
            await _questionRepository.RemoveAsync(dbQuestion);
        }

        public async Task<PaginationResponseContract<GetAllQuestionsContract>> GetNewestQuestions(PaginationContract paginationContract)
        {
            var questions = await _questionRepository.GetNewestQuestions(paginationContract);
            await CheckIfPageExist(paginationContract, questions);
            return questions;

        }

        public async Task<PaginationResponseContract<GetAllQuestionsContract>> GetQuestionsByDescendingAnswer(PaginationContract paginationContract)
        {
            var questions = await _questionRepository.GetQuestionsByDescendingAnswer(paginationContract);
            await CheckIfPageExist(paginationContract, questions);
            return questions;
        }

        public async Task<PaginationResponseContract<GetAllQuestionsContract>> GetQuestionsByDescendingVote(PaginationContract paginationContract)
        {
            var questions = await _questionRepository.GetQuestionsByDescendingVote(paginationContract);
            await CheckIfPageExist(paginationContract, questions);
            return questions;
        }

        public async Task<QuestionDetailResponseContract> GetQuestionsWithDetail(int id, int userId)
        {
            var isFavorite = await _favoriteCache.CheckFav(id, userId);
            var questionResponse = await _questionDetailCache.GetQuestionsWithDetail(id);
            questionResponse.Vote = await _voteCache.GetNumberOfVotes(id);
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
        }
        private async Task CheckIfUserFavorited(AddQuestionToFavContract addQuestionToFavContract)
        {
            var favorite = await _favoriteRepository.CheckFavorite(addQuestionToFavContract.QuestionId, addQuestionToFavContract.UserId);
            if (favorite)
            { throw new ClientSideException("bi kere daha favorilenemez"); }
        }
        private async Task CheckIfPageExist(PaginationContract paginationContract, PaginationResponseContract<GetAllQuestionsContract> questions)
        {
            if (paginationContract.Page> questions.Pagination.TotalPage)
            {
                throw new ClientSideException("sayfada veri yok");
            }
        }


    }
}
