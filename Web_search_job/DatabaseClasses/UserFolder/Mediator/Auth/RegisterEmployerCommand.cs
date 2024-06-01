using MediatR;
using Microsoft.AspNetCore.Identity;
using Web_search_job.DatabaseClasses.UserFolder.Dtos.Auth;

namespace Web_search_job.DatabaseClasses.UserFolder.Mediator.Auth
{
    public class RegisterEmployerCommand : IRequest<RegisterResponseDto>
    {
        public RegisterEmployerCommand(RegisterRequest request)
        {
            Request = request;
        }

        public RegisterRequest Request { get; set; }

        public class RegisterEmployerCommandHandler : IRequestHandler<RegisterEmployerCommand, RegisterResponseDto>
        {
            private readonly UserManager<ApplicationUser> _userManager;

            public RegisterEmployerCommandHandler(UserManager<ApplicationUser> userManager)
            {
                _userManager = userManager;
            }

            public async Task<RegisterResponseDto> Handle(RegisterEmployerCommand command, CancellationToken cancellationToken)
            {
                var userByEmail = await _userManager.FindByEmailAsync(command.Request.Email!);
/*                var userByUsername = await _userManager.FindByNameAsync(command.Request.Email!);
*/
                if (userByEmail is not null /*|| userByUsername is not null*/)
                {
/*                    throw new Exception($"User with email {command.Request.Email} or username {command.Request.Username} already exists.");
*/                    throw new Exception($"User with email {command.Request.Email} already exists.");
                }

                ApplicationUser user = new()
                {
                    Email = command.Request.Email,
                    UserName = command.Request.Email,
                    Provider = Consts.LoginProviders.Password,
                };

                var result = await _userManager.CreateAsync(user, command.Request.Password!);

                await _userManager.AddToRoleAsync(user, Role.Employer);

                Console.WriteLine(Role.Employer);
                if (!result.Succeeded)
                {
                    throw new Exception(
                                    $"Unable to register user {command.Request.Email}, errors: {GetErrorsText(result.Errors)}");
                }

                return new RegisterResponseDto();
            }

            private static string GetErrorsText(IEnumerable<IdentityError> errors)
            {
                return string.Join(", ", errors.Select(error => error.Description).ToArray());
            }
        }
    }
}
