using CycleShare.Server.Dtos;
using CycleShare.Server.Entities;
using CycleShare.Server.Services;
using CycleShare.Server.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CycleShare.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Auth([FromBody] CredentialsDto credentials)
        {
            Credentials c = _service.AuthUser(credentials.Email, credentials.Password);
            if (c == null)
            {
                return Unauthorized();
            }

            var jwt = _service.GenerateAccessToken(c);
            jwt.RefreshToken = _service.CreateRefreshToken(c.UserId);

            return Ok(jwt);
        }

        [AllowAnonymous]
        [HttpPost("{token}/refresh")]
        public ActionResult RefreshAccessToken(string token)
        {
            return Ok(_service.RefreshAccessToken(token));
        }

        [HttpPost("{token}/revoke")]
        public IActionResult RevokeRefreshToken(string token)
        {
            _service.RevokeRefreshToken(token);
            return NoContent();
        }

    }
}
