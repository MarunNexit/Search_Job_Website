using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Web_search_job.DatabaseClasses.UserFolder.ConfigurationModels;
using Web_search_job.DatabaseClasses.UserFolder.Entities.GetData;

namespace Web_search_job.DatabaseClasses.UserFolder.Services
{
    public class AuthService : IAuthService
    {
        private readonly JwtConfiguration _jwtConfiguration;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthService(IOptions<JwtConfiguration> jwtConfiguration, UserManager<ApplicationUser> userManager)
        {
            _jwtConfiguration = jwtConfiguration.Value;
            _userManager = userManager;
        }

        public async Task<JwtSecurityToken> CreateToken(ApplicationUser user)
        {
            var authClaims = await GetClaims(user);
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguration.Secret!));

            var token = new JwtSecurityToken(
                issuer: _jwtConfiguration.ValidIssuer,
                audience: _jwtConfiguration.ValidAudience,
                expires: DateTime.Now.AddHours(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

            return token;
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }


        //private
        public async Task<List<Claim>> GetClaims(ApplicationUser user)
        {
            var authClaims = new List<Claim>
        {
            new(ClaimTypes.Sid, Guid.NewGuid().ToString()),
            new(ClaimTypes.Name, user.UserName!),
            new(ClaimTypes.Email, user.Email!),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

            var userRoles = await _userManager.GetRolesAsync(user);

            if (userRoles.Any())
            {
                authClaims.AddRange(userRoles.Select(userRole => new Claim(ClaimTypes.Role, userRole)));
            }

            return authClaims;
        }

    }
}


