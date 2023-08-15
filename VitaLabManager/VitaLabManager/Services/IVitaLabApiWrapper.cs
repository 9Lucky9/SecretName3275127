using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using VitaLabData.DTOs;
using VitLabData;
using VitLabData.DTOs;

namespace VitaLabManager.Services
{
    public interface IVitaLabApiWrapper
    {
        /// <summary>
        /// Authorize client by the credentials: login, password
        /// and get user role in return.
        /// </summary>
        /// <returns>Bearer token.</returns>
        public Task<IEnumerable<UserRoleEnum>> AuthorizeClient(UserLoginRequest userLoginRequest);

        /// <summary>
        /// Get authorized http client.
        /// no need to pass/retrieve the bearer token.
        /// </summary>
        public HttpClient HttpClient { get; }

        public UserDTO CurrentUser { get; }
    }
}
