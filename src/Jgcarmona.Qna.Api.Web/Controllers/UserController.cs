using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Jgcarmona.Qna.Domain.Entities;
using Jgcarmona.Qna.Application.Features.Users;
using Jgcarmona.Qna.Application.Features.Users.Models;

namespace Jgcarmona.Qna.Api.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] SignupModel model)
        {
            var newUser = new User
            {
                Username = model.Username,
                Role = model.Role
            };

            var createdUser = await _userService.CreateUserAsync(newUser, model.Password);
            if (createdUser == null)
                return BadRequest("Failed to create user");

            return Ok(createdUser);
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> Me()
        {
            var username = User?.Identity?.Name;
            if (username == null)
            {
                return BadRequest("User is not authenticated");
            }
            var user = await _userService.GetUserByUsernameAsync(username);
            return Ok(user);
        }
    }
}
