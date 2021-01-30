using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using wordmeister_api.Dtos.Account;
using wordmeister_api.Dtos.General;
using wordmeister_api.Entities;
using wordmeister_api.Interfaces;
using wordmeister_api.Services;

namespace wordmeister_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public IActionResult Login(AuthenticateRequest model)
        {
            var response = _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            var result = new General.ResponseResult() { Data = response };

            return Ok(result);
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }

        [AllowAnonymous]
        [HttpPost("signup")]
        public IActionResult SignUp([FromBody] SignUp model)
        {
            return Ok(_userService.CreateUser(model));
        }

        [Authorize]
        [HttpGet("Authenticated")]
        public IActionResult Authenticated()
        {
            return Ok();
        }
    }
}
