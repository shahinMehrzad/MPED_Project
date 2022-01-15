using Microsoft.AspNetCore.Identity;
using System;

namespace MPED.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] ProfilePicture { get; set; }
        public bool IsActive { get; set; } = false;
        public DateTime PasswordChangeDate { get; set; }
        public bool IsPanelUser { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
