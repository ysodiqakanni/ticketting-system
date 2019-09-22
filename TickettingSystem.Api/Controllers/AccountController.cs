using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TickettingSystem.Data.DbModel;
using TickettingSystem.Services.Contracts;

namespace TickettingSystem.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        IStaffService staffService;
        public AccountController(IStaffService _staffService)
        {
            staffService = _staffService;
        }
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]LoginParams data)
        {
            string token = "";
            var user = staffService.Authenticate(data.Username, data.Password, out token);
            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });
            var response = new LoginParams
            {
                Username = user.Staffuserid,
                Token = token
            };

       

            return Ok(response);
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = staffService.GetAll();
            return Ok(users);
        }
    }
    public class LoginParams
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public string ACCESS_LEVEL { get; set; }
        public string READ_ONLY { get; set; }
    }
}