using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MPED.Application.Configurations;
using MPED.Application.DTOs;
using MPED.Application.DTOs.Identity;
using MPED.Application.Interfaces;
using System.Threading.Tasks;

namespace MPED.Infrastructure.Identity.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JWTConfiguration _jwtSettings;

        public IdentityService(UserManager<ApplicationUser> userManager,
            IOptions<JWTConfiguration> jwtSettings,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;
            _signInManager = signInManager;
        }
        public Task<Result<string>> ChangePassword(ChangePasswordDTO model)
        {
            throw new System.NotImplementedException();
        }

        public Task<Result<string>> ConfirmEmailAsync(string userId, string code)
        {
            throw new System.NotImplementedException();
        }

        public Task<Result<string>> ForgotPassword(ForgotPasswordRequest model, string origin)
        {
            throw new System.NotImplementedException();
        }

        public Task<CustomIdentityResult> GetTokenAsync(TokenRequest request, string ipAddress)
        {
            throw new System.NotImplementedException();
        }

        public Task<Result<string>> RegisterAsync(RegisterRequest request, string origin)
        {
            throw new System.NotImplementedException();
        }

        public Task<Result<string>> ResetPassword(ResetPasswordRequest model)
        {
            throw new System.NotImplementedException();
        }
    }
}
