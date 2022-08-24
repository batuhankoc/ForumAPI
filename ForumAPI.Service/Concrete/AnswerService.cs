using AutoMapper;
using ForumAPI.Cache.Redis;
using ForumAPI.Contract.AnswerContract;
using ForumAPI.Contract.DeleteContract;
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
    public class AnswerService : IAnswerService
    {
        private readonly IAnswerRepository _answerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;
        private readonly IRedisCache _redisCache;
        private readonly string GetAllQuestionsContractKey = "GetAllQuestionsContract";
        private readonly string QuestionDetailResponseContractKey = "QuestionDetailResponseContract";

        public AnswerService(IAnswerRepository answerRepository, IUserRepository userRepository, IQuestionRepository questionRepository, IMapper mapper, IRedisCache redisCache)
        {
            _answerRepository=answerRepository;
            _userRepository=userRepository;
            _questionRepository=questionRepository;
            _mapper=mapper;
            _redisCache=redisCache;
        }

        public async Task AddAnswerAsync(AddAnswerContract addAnswerContract)
        {

            var user = await _userRepository.GetByIdAsync(addAnswerContract.UserId);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }

            var question = await _questionRepository.GetByIdAsync(addAnswerContract.QuestionId);
            if (question == null)
            {
                throw new NotFoundException("User not found");
            }

            var addAnswer = _mapper.Map<Answer>(addAnswerContract);
            await _answerRepository.AddAsync(addAnswer);
            await _redisCache.Remove(GetAllQuestionsContractKey);
            await _redisCache.Remove(QuestionDetailResponseContractKey);



        }

        public async Task DeleteAnswerAsync(DeleteContract deleteContract)
        {
            var dbAnswer = await _answerRepository.GetByIdAsync(deleteContract.Id);
            if(dbAnswer?.IsDeleted == true) { throw new ClientSideException("Böyle bir Yanıt yok"); };
            await _answerRepository.RemoveAsync(dbAnswer);
        }
    }
}
