using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Web_search_job.Data;
using Web_search_job.DatabaseClasses;
using Web_search_job.DatabaseClasses.UserFolder;
using Web_search_job.DatabaseClasses.UserFolder.Dtos.Auth;
using Web_search_job.DatabaseClasses.UserFolder.Entities.GetData;
using Web_search_job.DatabaseClasses.UserFolder.Mediator.Auth;
using Web_search_job.DatabaseClasses.UserFolder.Services;
using Web_search_job.DTO.User;
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
        private readonly DataContext _context;

        public AuthController(IMediator mediator, UserManager<ApplicationUser> userManager, ILogger<AuthController> logger, IAuthService authService, IUserService userService, DataContext context)
        {
            _mediator = mediator;
            _authService = authService;
            _userManager = userManager;
            _logger = logger;
            _authService = authService;
            _userService = userService;
            _context = context;
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
            var currentUser = new UserDTO();

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
                currentUser.Id = user.UserInfo != null && user.UserInfo.Id != null ? user.UserInfo.Id : null ;
                currentUser.UserId = user.Id;
                currentUser.Email = user.Email;
                currentUser.FirstName = user.UserInfo != null && user.UserInfo.FirstName != null ? user.UserInfo.FirstName : null;
                currentUser.LastName = user.UserInfo != null && user.UserInfo.LastName != null ? user.UserInfo.LastName : null;
                currentUser.UserAge = user.UserInfo != null && user.UserInfo.DateOfBirth != null ? CalculateAge(user.UserInfo.DateOfBirth) : null;
                currentUser.UserImg = user.UserInfo != null && user.UserInfo.UserImg != null ? user.UserInfo.UserImg : null;
                currentUser.Location = user.UserInfo != null && user.UserInfo.Location != null ? user.UserInfo.Location : null;
                currentUser.Gender = user.UserInfo != null && user.UserInfo.Gender != null ? user.UserInfo.Gender : null;
                currentUser.PhoneNumber = user.UserInfo != null && user.UserInfo.PhoneNumber != null ? user.UserInfo.PhoneNumber : null;
                currentUser.UserCreatedAt = user.UserInfo != null && user.UserInfo.ActionCreatedAt != null ? user.UserInfo.ActionCreatedAt : DateTime.Now ;
            }
            else {
                _logger.LogInformation("User NULL");
                currentUser.Id = 0;
                currentUser.Email = "searcher_1_@gmail.com";
            }

            return Ok(currentUser);
        }



        [HttpPost("set-user-data")]
        public async Task<IActionResult> SetUserData( [FromBody] UserSetDTO userData )
        {
            Console.Write(userData);

            var userName = _userService.GetMyName();
            if (string.IsNullOrEmpty(userName))
            {
                return NotFound("User name not found");
            }
            _logger.LogInformation(userName);

            if (string.IsNullOrEmpty(userData.userId))
            {
                return NotFound("Error");
            }
            var userInfo = await _context.UsersInfo
                .FirstOrDefaultAsync(ui => ui.user_Id == userData.userId);

            if (userInfo != null)
            {
                // Оновлення існуючого запису
                userInfo.DateOfBirth = userData.dateOfBirth != null ? userData.dateOfBirth : userInfo.DateOfBirth;
                userInfo.Gender = userData.gender != null ? userData.gender : userInfo.Gender;
                userInfo.LocationId = userData.locationId != null ? userData.locationId : userInfo.LocationId;
                userInfo.FirstName = userData.firstName != null ? userData.firstName : userInfo.FirstName;
                userInfo.LastName = userData.lastName != null ? userData.lastName : userInfo.LastName;
                userInfo.PhoneNumber = userData.phoneNumber != null ? userData.phoneNumber : userInfo.PhoneNumber;
                userInfo.UserImg = userData.userImg != null ? userData.userImg : userInfo.UserImg;
            }
            else
            {
                // Створення нового запису
                userInfo = new UserInfo()
                {
                    user_Id = userData.userId,
                    DateOfBirth = userData.dateOfBirth != null ? userData.dateOfBirth : null,
                    Gender = userData.gender != null ? userData.gender : null,
                    LocationId = userData.locationId != null ? userData.locationId : null,
                    FirstName = userData.firstName != null ? userData.firstName : null,
                    LastName = userData.lastName != null ? userData.lastName : null,
                    PhoneNumber = userData.phoneNumber != null ? userData.phoneNumber : null,
                    UserImg = userData.userImg != null ? userData.userImg : null,
                    ActionCreatedAt = DateTime.UtcNow
                };

                _context.UsersInfo.Add(userInfo);
            }

            await _context.SaveChangesAsync();

            var currentUser = new UserDTO()
            {
                Id = userInfo.Id,
                UserId = userInfo.user_Id,
                Email = userName, 
                FirstName = userInfo.FirstName,
                LastName = userInfo.LastName,
                UserAge = CalculateAge(userInfo.DateOfBirth),
                UserImg = userInfo.UserImg,
                Location = userInfo.Location,
                Gender = userInfo.Gender,
                PhoneNumber = userInfo.PhoneNumber,
                UserCreatedAt = userInfo.ActionCreatedAt
            };

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


        private int? CalculateAge(DateTime? dateOfBirth)
        {
            if (dateOfBirth.HasValue)
            {
                var today = DateTime.Today;
                var birthDate = dateOfBirth.Value;
                var age = today.Year - birthDate.Year;
                if (birthDate > today.AddYears(-age)) age--;
                return age;
            }
            else
            {
                return null;
            }
        }
    }
}
