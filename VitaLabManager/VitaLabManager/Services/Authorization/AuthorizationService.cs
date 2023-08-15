using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using VitaLabData.DTOs;
using VitaLabManager.Exceptions;
using VitLabData;

namespace VitaLabManager.Services.Authorization
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IVitaLabApiWrapper _vitaLabApiWrapper;

        public AuthorizationService(IVitaLabApiWrapper vitaLabApiWrapper)
        {
            _vitaLabApiWrapper = vitaLabApiWrapper;
        }

        public async Task<IEnumerable<UserRoleEnum>> Authorize(string login, string password)
        {
            try
            {
                var userLoginRequest = new UserLoginRequest(login, password);
                var roles = await _vitaLabApiWrapper.AuthorizeClient(userLoginRequest);
                return roles;
            }
            catch (AuthenticationFailed)
            {
                throw;
            }
            catch (HttpRequestException)
            {
                throw;
            }

        }
    }
}
