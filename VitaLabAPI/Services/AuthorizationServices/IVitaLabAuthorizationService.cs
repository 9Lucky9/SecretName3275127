using VitaLabData.DTOs;
using VitLabData;
using VitLabData.DTOs;

namespace VitaLabAPI.Services.AuthorizationServices
{
    public interface IVitaLabAuthorizationService
    {
        /// <summary>
        /// Sign in with login and password.
        /// </summary>
        public Task<string> SignIn(UserLoginRequest userLoginRequest);

        /// <summary>
        /// Get user roles by bearer token.
        /// </summary>
        public IEnumerable<UserRoleEnum> GetRoles(string bearerToken);

        /// <summary>
        /// Get user dto from bearer token.
        /// </summary>
        public Task<UserDTO> GetUserInfoByToken(string bearerToken);
    }
}
