using AutoMapper;
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

        public QuestionService(IQuestionRepository questionRepository, IMapper mapper , 
            IFavoriteRepository favoriteRepository, IUserRepository userRepository)
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
            _favoriteRepository = favoriteRepository;
            _userRepository = userRepository;
        }

        public async Task AddQuestionAsync(AddQuestionContract addQuestionContract)
        {
            var addQuestion = _mapper.Map<Question>(addQuestionContract);
            await _questionRepository.AddAsync(addQuestion);
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
        }  

        public async Task<List<GetAllQuestionsContract>> GetAllQuestionsWithDetails()
        {
            var questions = await _questionRepository.GetAllQuestionsWithDetails();
            return questions.OrderByDescending(x => x.CreatedDateTime).ToList();
        }


        public async Task<QuestionDetailContract> GetQuestionsWithDetail(int id)
        {
           
            return await _questionRepository.GetQuestionsWithDetail(id);
        }
    }
}
