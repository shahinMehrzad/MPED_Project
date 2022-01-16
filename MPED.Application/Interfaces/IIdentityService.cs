using AspNetCoreHero.Results;
using MPED.Application.DTOs;
using MPED.Application.DTOs.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPED.Application.Interfaces
{
    public interface IIdentityService
    {
        Task<CustomIdentityResult> GetTokenAsync(TokenRequest request, string ipAddress);

        Task<Result<string>> RegisterAsync(RegisterRequest request, string origin);

        Task<Result<string>> ConfirmEmailAsync(string userId, string code);

        Task<Result<string>> ForgotPassword(ForgotPasswordRequest model, string origin);

        Task<Result<string>> ResetPassword(ResetPasswordRequest model);

        Task<Result<string>> ChangePassword(ChangePasswordDTO model);
    }
}
