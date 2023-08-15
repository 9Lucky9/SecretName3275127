using VitaLabData.DTOs.Create;
using VitLabData;
using VitLabData.DTOs;
using VitLabData.DTOs.Create;

namespace VitaLabAPI.Services.UserRoles
{
    public interface IUserRoleService : 
        IServiceBase<UserRole>,
        IEntityCreate<UserRoleCreateRequest>,
        IEntityEdit<UserRoleDTO>
    {
        public Task<IEnumerable<UserRoleEnum>> GetRolesByUserId(int userId);
        public Task AssignRolesToUser(UsersRolesAssignRequest request);
    }
}
