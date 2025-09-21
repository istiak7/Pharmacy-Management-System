using Pharmacy_Management_System.Application.AppSettings;
using Pharmacy_Management_System.Application.Dtos.Responses.Users;
using Pharmacy_Management_System.Application.Repositories.Users;
using Pharmacy_Management_System.Application.Services.Users;

namespace Pharmacy_Management_System.Service.Services.Users
{
    public class UserService(
            JWTSettings _jWTSettings,
            IUserRepository _userRepository 
        ) : IUserService
    {
        #region PRIVATE


        #endregion

        #region LOGIN

        public async Task<UserResponse> LoginWithRefreshTokenAsync(int userId, string refreshToken)
        {
            var activeUser = await _userRepository.GetActiveUserByRefreshTokenAsync(
                userId,
                refreshToken);
        }

        #endregion

        #region Registration


        #endregion

    }
}
