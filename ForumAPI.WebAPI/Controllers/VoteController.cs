using ForumAPI.Cache.Redis;
using ForumAPI.Contract.ResponseContract;
using ForumAPI.Contract.VoteContract;
using ForumAPI.Service.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ForumAPI.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoteController : ControllerBase
    {
        private readonly IVoteService _voteService;
        private readonly IRedisCache _redisCache;
        private const string GetAllQuestionsContractKey = "GetAllQuestionsContract";

        public VoteController(IVoteService voteService, IRedisCache redisService)
        {
            _voteService=voteService;
            _redisCache=redisService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddVote(AddVoteContract addVoteContract){
            await _voteService.AddVote(addVoteContract);
            await _redisCache.Remove(GetAllQuestionsContractKey);
            return Ok(CustomResponseContract.Success(null, HttpStatusCode.OK));

        }
    }
}
