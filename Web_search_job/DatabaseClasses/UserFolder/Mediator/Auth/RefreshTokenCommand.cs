using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Web_search_job.DatabaseClasses.UserFolder.ConfigurationModels;
using Web_search_job.DatabaseClasses.UserFolder.Dtos.Auth;
using Web_search_job.DatabaseClasses.UserFolder.Services;

namespace Web_search_job.DatabaseClasses.UserFolder.Mediator.Auth
{
    public class RefreshTokenCommand : IRequest<RefreshTokenDto>
    {
        public RefreshTokenCommand(RefreshTokenDto command)
        {
            Command = command;
        }

        public RefreshTokenDto Command { get; set; }

        public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, RefreshTokenDto>
        {
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly JwtConfiguration _jwtConfiguration;
            private readonly IAuthService _authService;

            public RefreshTokenCommandHandler(
                UserManager<ApplicationUser> userManager,
                IOptions<JwtConfiguration> jwtConfiguration,
                IAuthService authService)
            {
                _userManager = userManager;
                _jwtConfiguration = jwtConfiguration.Value;
                _authService = authService;
            }

            public async Task<RefreshTokenDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
            {
                var accessToken = request.Command.AccessToken;
                var refreshToken = request.Command.RefreshToken;

                var principal = GetPrincipalFromExpiredToken(accessToken);
                if (principal == null)
                {
                    throw new Exception($"Недійсний токен доступу або токен оновлення");
                }

                var username = principal.Identity!.Name!;

                var user = await _userManager.FindByNameAsync(username);

                if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
                {
                    throw new Exception($"Недійсний токен доступу або токен оновлення.");
                }

                var newAccessToken = await _authService.CreateToken(user);
                var newRefreshToken = GenerateRefreshToken();

                user.RefreshToken = newRefreshToken;
                await _userManager.UpdateAsync(user);

                return new RefreshTokenDto
                {
                    AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                    RefreshToken = newRefreshToken
                };
            }

            private static string GenerateRefreshToken()
            {
                var randomNumber = new byte[64];
                using var rng = RandomNumberGenerator.Create();
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }

            private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
            {
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguration.Secret!)),
                    ValidateLifetime = false
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

                if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                    throw new SecurityTokenException("Invalid token");

                return principal;
            }
        }
    }
}
