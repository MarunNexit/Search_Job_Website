﻿using MediatR;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using Web_search_job.DatabaseClasses.UserFolder.Dtos.Auth;
using Web_search_job.DatabaseClasses.UserFolder.Services;

namespace Web_search_job.DatabaseClasses.UserFolder.Mediator.Auth
{
    public class LoginUserCommand : IRequest<LoginResponseDto>
    {
        public LoginUserCommand(LoginRequest request)
        {
            Request = request;
        }

        public LoginRequest Request { get; set; }

        public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginResponseDto>
        {
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly IAuthService _authService;

            public LoginUserCommandHandler(
                UserManager<ApplicationUser> userManager,
                IAuthService authService)
            {
                _userManager = userManager;
                _authService = authService;
            }

            public async Task<LoginResponseDto> Handle(LoginUserCommand command, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByNameAsync(command.Request.Username!) ?? await _userManager.FindByEmailAsync(command.Request.Username!);

                if (user is null || !await _userManager.CheckPasswordAsync(user, command.Request.Password!))
                {
                    throw new Exception($"Не вдалося автентифікувати користувача {command.Request.Username}");
                }

                if (user.Provider != Consts.LoginProviders.Password)
                {
                    throw new Exception($"Користувач був зареєстрований через {user.Provider} і не можна ввійти через {Consts.LoginProviders.Password}.");
                }

                var token = await _authService.CreateToken(user);
                var refreshToken = _authService.GenerateRefreshToken();

                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = DateTimeOffset.Now.AddDays(14);

                await _userManager.UpdateAsync(user);

                return new LoginResponseDto
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    RefreshToken = refreshToken
                };
            }
        }
    }
}
