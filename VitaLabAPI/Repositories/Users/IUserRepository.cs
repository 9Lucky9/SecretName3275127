using VitLabData;

namespace VitaLabAPI.Repositories.Users
{
    /// <summary>
    /// User repository.
    /// </summary>
    public interface IUserRepository : IRepository<User>
    {
        /// <summary>
        /// Get the user by login.
        /// </summary>
        public Task<User> GetByLogin(string login);

        /// <summary>
        /// Find users with that name.
        /// </summary>
        public Task<IEnumerable<User>> FindByName(string name);
    }
}
