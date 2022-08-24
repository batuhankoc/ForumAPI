using ForumAPI.Contract.AnswerContract;
using ForumAPI.Contract.DeleteContract;
using ForumAPI.Contract.ResponseContract;
using ForumAPI.Service.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ForumAPI.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        private readonly IAnswerService _answerService;

        public AnswerController(IAnswerService answerService)
        {
            _answerService = answerService;
        }
        
        [HttpPost("[action]")]
        public async Task<IActionResult> AddAnswer(AddAnswerContract addAnswerContract)
        {
            await _answerService.AddAnswerAsync(addAnswerContract);
            return Ok(CustomResponseContract.Success(null, HttpStatusCode.OK)); 
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> DeleteAnswer(DeleteContract deleteContract)
        {
            await _answerService.DeleteAnswerAsync(deleteContract);
            return Ok(CustomResponseContract.Success("Yanıt Silindi", HttpStatusCode.OK));

        }

    
    }
}
