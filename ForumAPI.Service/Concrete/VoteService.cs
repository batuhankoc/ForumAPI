using AutoMapper;
using ForumAPI.Cache.Redis;
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
        private readonly IRedisCache _redisCache;
        private readonly string GetAllQuestionsContractKey = "GetAllQuestionsContract";
        private readonly string QuestionDetailResponseContractKey = "QuestionDetailResponseContract";

        public VoteService(IVoteRepository voteRepository, IUserRepository userRepository, IQuestionRepository questionRepository, IMapper mapper, IRedisCache redisCache)
        {
            _voteRepository=voteRepository;
            _userRepository=userRepository;
            _questionRepository=questionRepository;
            _mapper=mapper;
            _redisCache=redisCache;
        }

        public async Task AddVote(AddVoteContract addVoteContract)
        {
            await CheckUser(addVoteContract);
            await CheckQuestion(addVoteContract);

            if (addVoteContract.Voted == null)
            {
                throw new ClientSideException("User can not vote null");
            }

            var dbVote = await _voteRepository.GetVote(addVoteContract.QuestionId, addVoteContract.UserId);
            if (dbVote == null)
            {
                var mapVote = _mapper.Map<Vote>(addVoteContract);
                await _voteRepository.AddAsync(mapVote);
                await _redisCache.Remove(GetAllQuestionsContractKey);
                await _redisCache.Remove(QuestionDetailResponseContractKey);
                
            }
            else
            {
                if (dbVote.Voted == addVoteContract.Voted || addVoteContract.Voted == null)
                {
                    throw new ClientSideException("aynı vote veya null gelme durumu");
                }

                dbVote.Voted = dbVote.Voted == null ? addVoteContract.Voted : null;
                await _voteRepository.UpdateAsync(dbVote);
                await _redisCache.Remove(GetAllQuestionsContractKey);
                await _redisCache.Remove(QuestionDetailResponseContractKey);
            }
        }

        private async Task CheckUser(AddVoteContract addVoteContract)
        {
            var user = await _userRepository.GetByIdAsync(addVoteContract.UserId);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }
        }
        private async Task CheckQuestion(AddVoteContract addVoteContract)
        {
            var question = await _questionRepository.GetByIdAsync(addVoteContract.QuestionId);
            if (question == null)
            {
                throw new NotFoundException("Question not found");
            }
        }
    }
}
