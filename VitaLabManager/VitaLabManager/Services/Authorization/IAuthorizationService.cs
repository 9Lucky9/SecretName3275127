using System.Collections.Generic;
using System.Threading.Tasks;
using VitLabData;

namespace VitaLabManager.Services.Authorization
{
    public interface IAuthorizationService
    {
        public Task<IEnumerable<UserRoleEnum>> Authorize(string login, string password);
    }
}
