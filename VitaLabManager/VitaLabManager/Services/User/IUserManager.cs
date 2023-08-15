using System.Threading.Tasks;
using VitLabData.DTOs;
using VitLabData.DTOs.Create;

namespace VitaLabManager.Services.User
{
    public interface IUserManager : ILoadByPage<UserDTO>
    {
        public Task CreateUser(UserCreateRequest userCreateRequest);
        public Task RemoveUser(UserDTO userDTO);
    }
}
