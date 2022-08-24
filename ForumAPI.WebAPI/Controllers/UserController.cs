using ForumAPI.Contract.DeleteContract;
using ForumAPI.Contract.LoginContract;
using ForumAPI.Contract.ResponseContract;
using ForumAPI.Contract.UserContract;
using ForumAPI.Service.Abstract;
using ForumAPI.Validation.FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;

namespace ForumAPI.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddUser(AddUserContract user)
        {
            await _service.AddUserAsync(user);
            return Ok(CustomResponseContract.Success(null, HttpStatusCode.OK));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(UserLoginContract login)
        {
            await _service.LoginAsync(login);
            return Ok(CustomResponseContract.Success(null, HttpStatusCode.OK));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> DeleteUser(DeleteContract deleteContract)
        {
            await _service.DeleteUser(deleteContract);
            return Ok(CustomResponseContract.Success(null, HttpStatusCode.OK));
        }

    }
}
