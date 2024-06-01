using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Web_search_job.DatabaseClasses.UserFolder.Services
{
    public interface IAuthService
    {
        Task<JwtSecurityToken> CreateToken(ApplicationUser user);
        string GenerateRefreshToken();
        Task<List<Claim>> GetClaims(ApplicationUser user);
    }
}
