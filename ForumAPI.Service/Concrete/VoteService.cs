using AutoMapper;
using ForumAPI.Cache.Interfaces;
using ForumAPI.Cache.Keys;
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
        private readonly IVoteCache _voteCache;

        public VoteService(IVoteRepository voteRepository, IUserRepository userRepository, IQuestionRepository questionRepository, IMapper mapper, IRedisCache redisCache, IVoteCache voteCache)
        {
            _voteRepository = voteRepository;
            _userRepository = userRepository;
            _questionRepository = questionRepository;
            _mapper = mapper;
            _redisCache = redisCache;
            _voteCache = voteCache;
        }

        public async Task AddVote(AddVoteContract addVoteContract)
        {
            await CheckUser(addVoteContract);
            await CheckQuestion(addVoteContract);

            if (addVoteContract.Voted == null)
            {
                throw new ClientSideException("User can not vote null");
            }

            var vote = await _voteCache.GetVote(addVoteContract.QuestionId, addVoteContract.UserId);
            string cacheKey = string.Format(CacheKeys.GetVoteKey, addVoteContract.QuestionId, addVoteContract.UserId);
            if (vote == null)
            {
                var mapVote = _mapper.Map<Vote>(addVoteContract);
                await _voteRepository.AddAsync(mapVote);
            }
            else
            {
                if (vote.Voted == addVoteContract.Voted || addVoteContract.Voted == null)
                {
                    throw new ClientSideException("aynı vote veya null gelme durumu");
                }

                vote.Voted = vote.Voted == null ? addVoteContract.Voted : null;
                await _voteRepository.UpdateAsync(vote);             

            }
            await _voteCache.Remove(cacheKey);
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
