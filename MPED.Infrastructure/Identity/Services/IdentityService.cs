using AspNetCoreHero.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MPED.Application.DTOs.Identity;
using MPED.Application.DTOs.Settings;
using MPED.Application.Enums.Identity;
using MPED.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MPED.Infrastructure.Identity.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JWTSettings _jwtSettings;
        private readonly IConfiguration _config;

        public IdentityService(UserManager<ApplicationUser> userManager,
            IOptions<JWTSettings> jwtSettings,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration config)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;
            _signInManager = signInManager;
            _config = config;
        }
        
        public Task<Result<string>> ConfirmEmailAsync(string userId, string code)
        {
            throw new System.NotImplementedException();
        }
        public Task<Result<string>> ForgotPassword(ForgotPasswordRequest model, string origin)
        {
            throw new System.NotImplementedException();
        }
        public Task<Result<string>> ResetPassword(ResetPasswordRequest model)
        {
            throw new System.NotImplementedException();
        }



        public async Task<CustomIdentityResult> GetTokenAsync(TokenRequest request, string ipAddress)
        {
            //We should use sanitizer here
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
            {
                return new CustomIdentityResult() { message = $"No Accounts Registered with {request.Email}.", succeeded = false, failed = true };
            }
            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
            if (!user.EmailConfirmed)
            {
                return new CustomIdentityResult() { message = $"Account Not Confirmed for '{request.Email}'.", succeeded = false, failed = true };
            }
            if (!result.Succeeded)
            {
                return new CustomIdentityResult() { message = $"Invalid Credentials for '{request.Email}'.", succeeded = false, failed = true };
            }
            JwtSecurityToken jwtSecurityToken = await GenerateJWToken(user, ipAddress);
            var response = new TokenResponse();
            response.Id = user.Id;
            response.JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            response.IssuedOn = jwtSecurityToken.ValidFrom.ToLocalTime();
            response.ExpiresOn = jwtSecurityToken.ValidTo.ToLocalTime();
            response.Email = user.Email;
            response.FirstName = user.FirstName;
            response.LastName = user.LastName;
            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            response.Roles = rolesList.ToList();
            response.IsVerified = user.EmailConfirmed;
            var refreshToken = GenerateRefreshToken(ipAddress);
            response.RefreshToken = refreshToken.Token;
            return new CustomIdentityResult() { message = $"Başarılı Giriş.", succeeded = true, failed = false, data = response };
        }
        public async Task<Result<string>> RegisterAsync(RegisterRequest request, string origin)
        {
            origin = _config.GetSection("OriginAddress").Value;
            //Should using sanitizer

            //Should control Model
            //Like Name, last name,Email and Password are not null
            //Check parametrs length that not more than for example 50 characters
            //Check Email validation
            //Check password rules (Example : Min 8 characters and ...)
            #region Model Validation
            if (request.Password != request.ConfirmPassword)
            {
                return Result<string>.Fail("Password does not match, type again!");
            }
            #endregion

            var userWithSameUserName = await _userManager.FindByEmailAsync(request.Email);
            if (userWithSameUserName != null)
            {
                return Result<string>.Fail($"Email {request.Email } is already registered.");
            }
            var user = new ApplicationUser
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.Email,
                CreateDate = DateTime.Now,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Roles.Customer.ToString());
                //Sending verification email to customer
                var createdUser = await _userManager.FindByEmailAsync(request.Email);
                //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                //code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                return Result<string>.Success(user.Id, message: $"User Registered. Confirmation Mail has been delivered to your Mailbox.");
            }
            else
            {
                return Result<string>.Fail(result.Errors.FirstOrDefault().ToString());
            }
        }
        public async Task<Result<string>> ChangePassword(ChangePasswordDTO model)
        {
            //sanitizer
            
            var userCheck = await _userManager.FindByEmailAsync(model.Email);
            if (userCheck != null)
            {
                var result = await _userManager.ChangePasswordAsync(userCheck, model.OldPassword, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.UpdateAsync(userCheck);
                    return Result<string>.Success(model.Email, message: $"Password has successfully changed");
                }
                else
                {
                    return Result<string>.Fail($"Your email or old password is incorrect.");
                }
            }
            return Result<string>.Fail($"Error occured while reseting the password.");
        }
        private RefreshToken GenerateRefreshToken(string ipAddress)
        {
            return new RefreshToken
            {
                Token = RandomTokenString()
            };
        }
        private string RandomTokenString()
        {
            using var rngCryptoServiceProvider = new RSACryptoServiceProvider();
            var randomBytes = new byte[40];
            //rngCryptoServiceProvider.GetBytes(randomBytes);
            // convert random bytes to hex string
            return BitConverter.ToString(randomBytes).Replace("-", "");
        }
        private async Task<JwtSecurityToken> GenerateJWToken(ApplicationUser user, string ipAddress)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();
            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id),
                new Claim("first_name", user.FirstName),
                new Claim("last_name", user.LastName),
                new Claim("full_name", $"{user.FirstName} {user.LastName}"),
                new Claim("ip", ipAddress)
            }
            .Union(userClaims)
            .Union(roleClaims);
            return JWTGeneration(claims);
        }
        private JwtSecurityToken JWTGeneration(IEnumerable<Claim> claims)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }
        

        
    }
}
