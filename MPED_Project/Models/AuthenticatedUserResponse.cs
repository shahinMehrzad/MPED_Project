using System;

namespace MPED_Project.Models
{
    public class AuthenticatedUserResponse
    {
        public string AccessToken { get; set; }
        public DateTime AccessTokenExpirationTime { get; set; }
        public string RefreshToken { get; set; }
    }
}
