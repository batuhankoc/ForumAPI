using FluentValidation.Results;
using ForumAPI.Contract.DeleteContract;
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
        [HttpPost("[action]")]
        public async Task<IActionResult> GetNewestQuestion(PaginationContract paginationContract)
        {
            var questions = await _questionService.GetNewestQuestions(paginationContract);
            return Ok(CustomResponseContract.Success(questions, HttpStatusCode.OK));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GetQuestionsByDescendingVote(PaginationContract paginationContract)
        {
            var questions = await _questionService.GetQuestionsByDescendingVote(paginationContract);
            return Ok(CustomResponseContract.Success(questions, HttpStatusCode.OK));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GetMostAnsweredQuestion(PaginationContract paginationContract)
        {
            var questions = await _questionService.GetQuestionsByDescendingAnswer(paginationContract);
            return Ok(CustomResponseContract.Success(questions, HttpStatusCode.OK));
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> AddQuestionToFav(AddQuestionToFavContract addQuestionToFavContract)
        {
           
            await _questionService.AddQuestionToFavAsync(addQuestionToFavContract);
            return Ok(CustomResponseContract.Success(null, HttpStatusCode.OK));
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetQuestionDetail(int id, int userId)
        {
            var questionDetails = await _questionService.GetQuestionsWithDetail(id, userId);
            return Ok(CustomResponseContract.Success(questionDetails, HttpStatusCode.OK));
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> DeleteQuestion(DeleteContract deleteContract)
        {
            await _questionService.DeleteQuestion(deleteContract);
            return Ok(CustomResponseContract.Success(null,HttpStatusCode.OK));
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> DeleteFavorite(DeleteContract deleteContract)
        {
            await _questionService.DeleteFavorite(deleteContract);
            return Ok(CustomResponseContract.Success(null, HttpStatusCode.OK));
        }



    }
}
