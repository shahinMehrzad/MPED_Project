using AspNetCoreHero.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using MPED.Application.DTOs;
using MPED.Application.DTOs.Identity;
using MPED.Application.Interfaces;
using MPED.Infrastructure.Identity;
using MPED_Project.Middlewares;
using MPED_Project.Services.RefreshTokenRepositories;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MPED_Project.Controllers
{
    [Route("api/identity")]
    [ApiController]
    [ValidateModel]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly UserManager<ApplicationUser> _userManager;


        public IdentityController(IIdentityService identityService, IRefreshTokenRepository refreshTokenRepository, UserManager<ApplicationUser> userManager)
        {
            this._identityService = identityService;
            _refreshTokenRepository = refreshTokenRepository;
            _userManager = userManager;
        }

        /// <summary>
        /// Generates a JSON Web Token for a valid combination of emailId and password.
        /// </summary>
        /// <param name="tokenRequest"></param>
        /// <returns></returns>
        [HttpPost("token")]
        [AllowAnonymous]
        public async Task<IActionResult> GetTokenAsync(TokenRequest tokenRequest)
        {
            var ipAddress = GenerateIPAddress();
            var token = await _identityService.GetTokenAsync(tokenRequest, ipAddress);
            if (token.data != null)
            {
                var model = new Models.RefreshToken()
                {
                    Id = Guid.Parse(token.data.Id),
                    Token = token.data.JWToken,
                    UserId = token.data.Id
                };
                await _refreshTokenRepository.Create(model);
            }
            return Ok(token);
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            string rawUserId = HttpContext.User.FindFirstValue("uid");
            if (!Guid.TryParse(rawUserId, out Guid userId))
            {
                return Unauthorized();
            }
            await _refreshTokenRepository.DeleteAll(userId);
            return Ok(new Result<string>() { Succeeded = true, Message = "You have successfully logged out." });
            //return NoContent();
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterAsync(RegisterRequest request)
        {
            var origin = Request.Headers["origin"];
            return Ok(await _identityService.RegisterAsync(request, origin));
        }

        [HttpGet("confirm-email")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmailAsync([FromQuery] string userId, [FromQuery] string code)
        {
            return Ok(await _identityService.ConfirmEmailAsync(userId, code));
        }

        [HttpPost("forgot-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest model)
        {
            return Ok(await _identityService.ForgotPassword(model, Request.Headers["origin"]));
        }

        [HttpPost("reset-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest model)
        {
            return Ok(await _identityService.ResetPassword(model));
        }

        [HttpPost("Change-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ChangePassword(ChangePasswordDTO model)
        {
            var result = await _identityService.ChangePassword(model);
            if (result.Succeeded)
            {
                var usr = await _userManager.FindByEmailAsync(model.Email);
                await _refreshTokenRepository.DeleteAll(Guid.Parse(usr.Id));
            }
            return Ok(result);
        }

        private string GenerateIPAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }

        [HttpPost("IsTokenValid")]
        public async Task<IActionResult> IsTokenValid()
        {
            string Token = Request.Headers[HeaderNames.Authorization].ToString().Substring(7);
            if (!string.IsNullOrEmpty(Token))
            {
                var result = await _refreshTokenRepository.GetByToken(Token);
                if (result != null)
                {
                    return Ok(new AspNetCoreHero.Results.Result() { Succeeded = true, Message = "The session is valid." });
                }
            }
            return Ok(new AspNetCoreHero.Results.Result { Succeeded = false, Message = "Your session has expired, please login again." });
        }
    }
}
