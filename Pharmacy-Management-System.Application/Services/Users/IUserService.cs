using Pharmacy_Management_System.Application.Dtos.Responses.Users;

namespace Pharmacy_Management_System.Application.Services.Users
{
    public interface IUserService
    {
        #region GET

        Task<UserResponse> LoginWithRefreshTokenAsync(int userId, string refreshToken);

        #endregion

        #region POST


        #endregion

        #region PUT


        #endregion

        #region DELETE


        #endregion
    }
}
