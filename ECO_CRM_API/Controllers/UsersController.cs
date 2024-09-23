using BusinessLayer.Services.UserServices;
using DtoLayer.User.Requests;
using ECO_CRM_API.Models;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECO_CRM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExists = await _userService.TGetUserByUsernameAsync(model.Username);
            if (userExists != null)
                return BadRequest("User already exists");

            var newUser = new AddUserRequest
            {
                Username = model.Username,
                FullName = model.FullName,
                Password = model.Password
            };

            await _userService.AddUserAsync(newUser);
            return Ok("User registered successfully");
        }
    }
}
