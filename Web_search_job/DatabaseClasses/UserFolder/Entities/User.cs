using Microsoft.AspNetCore.Identity;

namespace Web_search_job.DatabaseClasses
{
    public class ApplicationUser : IdentityUser
    {
        public string Provider { get; set; } = null!;
        public string? RefreshToken { get; set; }
        public DateTimeOffset RefreshTokenExpiryTime { get; set; }

        public virtual UserInfo UserInfo { get; set; }
    }
}
