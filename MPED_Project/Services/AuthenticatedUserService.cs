using Microsoft.AspNetCore.Http;
using MPED.Application.Interfaces.Shared;
using System.Security.Claims;

namespace MPED_Project.Services
{
    public class AuthenticatedUserService : IAuthenticatedUserService
    {
        public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue("uid");
        }

        public string UserId { get; }
        public string Username { get; }
    }
}
