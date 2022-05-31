using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;
using Zikunov.ServiceStation.WebApp.Shared.Models;
using Zikunov.ServiceStation.Data.Models;
using Zikunov.ServiceStation.Logic.Interfaces;
using Zikunov.ServiceStation.WebApi.Contracts.Requests;
using Zikunov.ServiceStation.WebApi.Contracts.Responses;
using Zikunov.ServiceStation.WebApi.Settings;

namespace Zikunov.ServiceStation.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private SignInManager<User> _signInManager;
        private UserManager<User> _userManager;
        private IJwtService _jwtService;
        private readonly AppSettings _appSettings;

        public UserController(
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            IJwtService jwtService,
            IOptions<AppSettings> appSettings)
        {
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _jwtService = jwtService ?? throw new ArgumentNullException(nameof(jwtService));

            if (appSettings is null)
            {
                throw new ArgumentNullException(nameof(appSettings));
            }
            _appSettings = appSettings.Value;
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignInAsync(UserSignInRequest model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
            
            if (!result.Succeeded)
            {
                return BadRequest(new { message = "Email or password is incorrect." });
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            var token = _jwtService.GenerateJwtToken(user.Id, _appSettings.Secret);

            var response = new AuthenticateResponse(user, token);

            return Ok(response);
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUpAsync(UserSignUpRequest request)
        {
            User user = new User
            {
                Email = request.Email,
                UserName = request.Email,
                FullName = request.FullName,
                Phone = request.Phone,
                IsActive = true
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                return BadRequest(new ErrorResponse<string>
                {
                    Message = "Can't registrate new user.",
                    Errors = result.Errors.Select(error => error.Description)
                });
            }

            var token = _jwtService.GenerateJwtToken(user.Id, _appSettings.Secret);

            var response = new AuthenticateResponse(user, token);

            return Ok(response);
        }

        [HttpPost("test")]
        public async Task<IActionResult> TestAsync()
        {
            var email = "test@test.test";
            var user = await _userManager.FindByEmailAsync(email);
            var token = _jwtService.GenerateJwtToken(user.Id, _appSettings.Secret);

            var response = new AuthenticateResponse(user, token);

            return Ok(response);
        }
    }
}
