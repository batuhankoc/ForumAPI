using AutoMapper;
using ForumAPI.Contract.AnswerContract;
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

        public AnswerService(IAnswerRepository answerRepository, IMapper mapper, IUserRepository userRepository, IQuestionRepository questionRepository)
        {
            _answerRepository = answerRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _questionRepository = questionRepository;
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



        }


    }
}
