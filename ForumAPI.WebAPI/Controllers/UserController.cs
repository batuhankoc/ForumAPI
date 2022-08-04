﻿using ForumAPI.Contract.UserContract;
using ForumAPI.Service.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        [HttpPost]
        public async Task<IActionResult> AddUser(AddUserContract user)
        {
            await _service.AddUserAsync(user);
            return Ok();
        }
    }
}