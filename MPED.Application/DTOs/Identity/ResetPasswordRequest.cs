using System.ComponentModel.DataAnnotations;

namespace MPED.Application.DTOs.Identity
{
    public class ResetPasswordRequest
    {
        public string Email { get; set; }

        [Required]
        public string Token { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
