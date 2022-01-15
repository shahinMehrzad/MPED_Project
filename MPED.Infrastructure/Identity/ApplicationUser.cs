using Microsoft.AspNetCore.Identity;
using System;

namespace MPED.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; } = false;
        public DateTime CreateDate { get; set; }
    }
}
