using ForumAPI.Contract.QuestionContract;
using ForumAPI.Contract.ResponseContract;
using ForumAPI.Service.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ForumAPI.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;

        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddQuestion(AddQuestionContract addQuestionContract)
        {
            await _questionService.AddQuestionAsync(addQuestionContract);
            return Ok(CustomResponseContract.Success(null, HttpStatusCode.OK)); // null yerine addQuestionContract yazılabilir diye düşündük
        }
    }


   }
