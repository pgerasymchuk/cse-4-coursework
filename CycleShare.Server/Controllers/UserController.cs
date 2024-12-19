using AutoMapper;
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
    public class UserController : ControllerBase { 

        private readonly IUserService _userService;
        private readonly IAuthService _authService;

        public UserController(IUserService userService, IAuthService authService)
        {
            this._userService = userService; 
            this._authService = authService;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult SignUp([FromBody] SignUpDto signup)
        {
            User user = _userService.CreateUser(signup);
            return Ok(CreateNewJWT(user.Id));
        }

        [AllowAnonymous]
        [HttpPost("password/forgot")]
        public IActionResult ForgotPassword(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return BadRequest();
            }
            _userService.ForgotPassword(email);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("password/reset")]
        public IActionResult ResetPassword([FromBody] ResetPasswordDto dto)
        {
            if (dto.Password != dto.RepeatPassword)
            {
                return BadRequest();
            }
            var user = _userService.ResetPassword(dto.Token, dto.Password);
            return Ok(CreateNewJWT(user.Id));
        }

        private JsonWebToken CreateNewJWT(int userId)
        {
            var jwt = _authService.GenerateAccessToken(userId);
            _authService.RevokeRefreshToken(userId);
            jwt.RefreshToken = _authService.CreateRefreshToken(userId);
            return jwt;
        }
    }
}
