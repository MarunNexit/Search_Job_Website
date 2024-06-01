using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Web_search_job.DatabaseClasses;
using Web_search_job.DatabaseClasses.UserFolder.Dtos.Auth;
using Web_search_job.DatabaseClasses.UserFolder.Entities.GetData;
using Web_search_job.DatabaseClasses.UserFolder.Mediator.Auth;
using Web_search_job.DatabaseClasses.UserFolder.Services;
using Web_search_job.Services.UserService;

namespace Web_search_job.Host.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IAuthService _authService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AuthController> _logger;
        private readonly IUserService _userService;

        public AuthController(IMediator mediator, UserManager<ApplicationUser> userManager, ILogger<AuthController> logger, IAuthService authService, IUserService userService)
        {
            _mediator = mediator;
            _authService = authService;
            _userManager = userManager;
            _logger = logger;
            _authService = authService;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginResponseDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            return Ok(await _mediator.Send(new LoginUserCommand(request)));
        }

        [AllowAnonymous]
        [HttpPost("social-login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginResponseDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SocialLogin([FromBody] SocialLoginRequest request)
        {
            return Ok(await _mediator.Send(new LoginSocialUserCommand(request)));
        }

        [AllowAnonymous]
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RegisterResponseDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            return Ok(await _mediator.Send(new RegisterUserCommand(request)));
        }


        [AllowAnonymous]
        [HttpPost("register-employer")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RegisterResponseDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterEmployer([FromBody] RegisterRequest request)
        {
            return Ok(await _mediator.Send(new RegisterEmployerCommand(request)));
        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RefreshTokenDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDto request)
        {
            return Ok(await _mediator.Send(new RefreshTokenCommand(request)));
        }

        [Authorize]
        [HttpGet("user-data")]
        public async Task<IActionResult> GetUserData()
        {
            _logger.LogInformation("GetUserData called");

            var currentUser = new UserData();

            var userName = _userService.GetMyName();
            if (string.IsNullOrEmpty(userName))
            {
                return NotFound("User name not found");
            }
            _logger.LogInformation(userName);

            var user = await _userManager.Users
                .Include(u => u.UserInfo)
                   .ThenInclude(l => l.Location)
                .FirstOrDefaultAsync(u => u.UserName == userName);

            if (user != null)
            {
                currentUser.Id = user.Id;
                currentUser.Email = user.Email;
                currentUser.Age = user.UserInfo.UserAge;
                currentUser.Img = user.UserInfo.UserImg;
                currentUser.Location = user.UserInfo.Location;
                currentUser.Phone = user.UserInfo.PhoneNumber;
            }
            else {
                _logger.LogInformation("User NULL");
                currentUser.Id = "qwert1";
                currentUser.Email = "searcher_1_@gmail.com";
            }

            return Ok(currentUser);
        }


       /* [HttpGet("user-id")]
        public async Task<IActionResult> GetUserId()
        {
            var userId = await GetUserInfo();

            var userIdDto = new UserId
            {
                Id = userId.Id,
            };

            return Ok(userIdDto);
        }


        protected async Task<User> GetUserInfo()
        {
            ClaimsPrincipal currentUser = this.User;
            var currentUserName = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await _userManager.FindByNameAsync(currentUserName);

            return user;
        }*/

    }
}
