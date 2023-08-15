using VitLabData;
using VitLabData.DTOs;
using VitLabData.DTOs.Create;

namespace VitaLabAPI.Services.Users
{
    /// <summary>
    /// Service for user buisness logic interactions.
    /// </summary>
    public interface IUserService : 
        IServiceBase<User>,
        IEntityCreate<UserCreateRequest>,
        IEntityEdit<UserDTO>
    {

        /// <summary>
        /// Get the user by login.
        /// </summary>
        public Task<User> GetByLogin(string login);

        /// <summary>
        /// Change the user password.
        /// </summary>
        public Task ChangePassword(User user, string newPassword);

        /// <summary>
        /// Find the users by name.
        /// </summary>
        public Task<IEnumerable<User>> FindByName(string name);
    }
}
