using VitaLabData.DTOs.Create;
using VitLabData;

namespace VitaLabAPI.Repositories.UserRoles
{
    /// <summary>
    /// <see cref="UserRole"/> repository.
    /// </summary>
    public interface IUserRoleRepository : IRepository<UserRole>
    {
        /// <summary>
        /// Get user role by user id.
        /// </summary>
        public Task<IEnumerable<UserRole>> GetRolesByUserId(int userId);

        /// <summary>
        /// Get user role by name.
        /// </summary>
        public Task<UserRole> GetByName(string name);

        /// <summary>
        /// Assign all specified roles to user.
        /// </summary>
        public Task AssignRolesToUser(int userId, IEnumerable<int> userRoleEnums);

    }
}
