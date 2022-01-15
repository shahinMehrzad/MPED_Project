namespace MPED.Application.DTOs.Identity
{
    public class CustomIdentityResult
    {
        public string redirect { get; set; }
        public bool failed { get; set; }
        public string message { get; set; }
        public bool succeeded { get; set; }
        public TokenResponse data { get; set; }
    }
}
