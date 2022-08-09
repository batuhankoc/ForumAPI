using AutoMapper;
using ForumAPI.Contract.VoteContract;
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
    public class VoteService : IVoteService
    {
        private readonly IVoteRepository _voteRepository;
        private readonly IUserRepository _userRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;

        public VoteService(IVoteRepository voteRepository, IUserRepository userRepository,
            IQuestionRepository questionRepository, IMapper mapper)
        {
            _voteRepository = voteRepository;
            _userRepository = userRepository;
            _questionRepository = questionRepository;
            _mapper = mapper;
        }

        public async Task AddVote(AddVoteContract addVoteContract)
        {
            var user = await _userRepository.GetByIdAsync(addVoteContract.UserId);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }

            var question = await _questionRepository.GetByIdAsync(addVoteContract.QuestionId);
            if (question == null)
            {
                throw new NotFoundException("Question not found");
            }

            var mapVote = _mapper.Map<Vote>(addVoteContract);
            var isVoted = await _voteRepository.CheckVote(addVoteContract.QuestionId,
              addVoteContract.UserId);
            if (!isVoted)
            {
                await _voteRepository.AddAsync(mapVote);
            }
            var dbVote = await _voteRepository.GetByIdAsync(mapVote.Id);
            if(dbVote.Voted == null)
            {
                await _voteRepository.UpdateAsync(mapVote);
            }
            else if(dbVote.Voted == true)
            {
                if(mapVote.Voted == true)
                {
                    throw new ClientSideException("Bir daha oy veremezsiniz");
                }
                else if(mapVote.Voted == false)
                {
                    mapVote.Voted = null;
                    await _voteRepository.UpdateAsync(mapVote);
                }
            }


           
        }

       
    }
}
