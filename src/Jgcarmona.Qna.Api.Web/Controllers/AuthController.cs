using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Jgcarmona.Qna.Application.Features.Auth;
using Jgcarmona.Qna.Application.Features.Auth.Models;
using Jgcarmona.Qna.Core;

namespace Jgcarmona.Qna.Api.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("token")]
        public IActionResult Authenticate([FromBody] LoginModel model)
        {
            var user = _authService.AuthenticateUser(model.Username, model.Password);

            if (user == null)
                return Unauthorized(new { message = "Incorrect username or password" });

            var token = _authService.GenerateJwtToken(user);
            return Ok(new { token });
        }
    }
}
