using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pharmacy_Management_System.Application.Dtos.Requests.Users;
using Pharmacy_Management_System.Application.Services.Users;
using Pharmacy_Management_System.Service.Validators;

namespace Pharmacy_Management_System.Controllers.Users
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController (
            IUserService _userService
        ) : ControllerBase 
    {
        #region GET

        [HttpGet("refresh-token")]
        public async Task<IActionResult> RefreshToken(
            [FromQuery] int userId,
            [FromQuery] string refreshToken)
        {
            var response = await _userService.LoginWithRefreshTokenAsync(userId, refreshToken);
            return Ok(response);
        }

        #endregion

        #region LOGIN

        [HttpPost("login")]
        public async Task<IActionResult> Login(
            [FromBody] LoginRequest request,
            [FromServices] IValidator<LoginRequest> loginValidator)
        {
            var errorResult = await request.ValidateModel(loginValidator);
            if (!errorResult.Success)
                return BadRequest(errorResult.Errors);

            var response = await _userService.Login(request);
            return Ok(response);
        }

        #endregion

        #region REGISTRATION

        [HttpPost("registration")]
        public async Task<IActionResult> Registration(
            [FromBody] UserRequest request,
            [FromServices] IValidator<UserRequest> registrationValidator)
        {
            var result = await request.ValidateModel(registrationValidator);
            if (!result.Success)
                return BadRequest(result.Errors);

            var response = await _userService.Add(request);
            return Ok(response);
        }

        #endregion
    }
}
