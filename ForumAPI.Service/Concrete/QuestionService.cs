using AutoMapper;
using ForumAPI.Contract.QuestionContract;
using ForumAPI.Data.Abstract;
using ForumAPI.Data.Entity;
using ForumAPI.Service.Abstract;
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

        public QuestionService(IQuestionRepository questionRepository, IMapper mapper)
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
        }

        public async Task AddQuestionAsync(AddQuestionContract addQuestionContract)
        {
            var addQuestion = _mapper.Map<Question>(addQuestionContract);
            await _questionRepository.AddAsync(addQuestion);
        }
    }
}
