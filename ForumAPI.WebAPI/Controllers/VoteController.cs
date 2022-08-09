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

        public VoteController(IVoteService voteService)
        {
            _voteService = voteService;
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> AddVote(AddVoteContract addVoteContract){
            await _voteService.AddVote(addVoteContract);
            return Ok(CustomResponseContract.Success(null, HttpStatusCode.OK));

        }
    }
}
