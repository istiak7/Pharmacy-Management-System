using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pharmacy_Management_System.Application.Services.Users;

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

        [HttpGet]
        public async Task<IActionResult> Get(
            [FromQuery] int userId,
            [FromQuery] string refreshToken)
        {
            var response = await _userService.LoginWithRefreshTokenAsync(userId, refreshToken);
            return Ok(response);
        }

        #endregion

        #region POST


        #endregion

        #region PUT


        #endregion

        #region DELETE


        #endregion
    }
}
