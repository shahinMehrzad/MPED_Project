using System;

namespace MPED_Project.Models
{
    public class RefreshToken
    {
        public Guid Id { get; set; }
        public string Token { get; set; }
        public string UserId { get; set; }
    }
}
