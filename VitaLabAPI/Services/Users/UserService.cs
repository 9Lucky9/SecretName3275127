using Microsoft.AspNetCore.Identity;
using VitaLabAPI.Exceptions.Users;
using VitaLabAPI.Repositories.Users;
using VitLabData;
using VitLabData.DTOs;
using VitLabData.DTOs.Create;

namespace VitaLabAPI.Services.Users
{
    /// <inheritdoc cref="IUserService"/>
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <inheritdoc/>
        public async Task Create(UserCreateRequest userCreate)
        {
            try
            {
                var findedUser = await _userRepository.GetByLogin(userCreate.Login);
                if (findedUser is not null)
                    throw new ArgumentException("User with that login already exists");
                ValidateName(userCreate.Name);
                ValidateLogin(userCreate.Login);
                ValidatePassword(userCreate.Password);
                var hashedPassword = new PasswordHasher<UserCreateRequest>().HashPassword(userCreate, userCreate.Password);
                var user = new User(
                    0, 
                    userCreate.Name, 
                    userCreate.Login,
                    hashedPassword, 
                    new List<UserRoleEnum>() { UserRoleEnum.User });
                await _userRepository.Create(user);
            }
            catch(ArgumentException) 
            {
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task ChangePassword(User user, string newPassword)
        {
            try
            {
                var foundUser = await GetById(user.Id);
                user.Password = newPassword;
                await _userRepository.Update(user);
            }
            catch (UserNotFoundException)
            {
                throw;
            }

        }

        public async Task<IEnumerable<User>> GetByPage(int page)
        {
            if (page <= 0)
                throw new ArgumentException("Page canno't be zero or less");
            return await _userRepository.GetWithPagination(page);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<User>> FindByName(string name)
        {
            var users = await _userRepository.FindByName(name);
            return users;
        }

        /// <inheritdoc/>
        public async Task<User> GetById(int id)
        {
            var user = await _userRepository.GetById(id);
            if (user is null)
                throw new UserNotFoundException();
            return user;
        }

        /// <inheritdoc/>
        public async Task<User> GetByLogin(string login)
        {
            var user = await _userRepository.GetByLogin(login);
            if (user is null)
                throw new UserNotFoundException();
            return user;
        }

        /// <inheritdoc/>
        public async Task Edit(UserDTO entity)
        {
            try
            {
                var foundUser = await _userRepository.GetById(entity.Id);
                ValidateName(entity.Name);
                ValidateLogin(entity.Login);
                ValidatePassword(entity.Password);
                foundUser.Name = entity.Name;
                foundUser.Login = entity.Login;
                foundUser.Password = entity.Password;
                await _userRepository.Update(foundUser);
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (UserNotFoundException)
            {
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task DeleteById(int id)
        {
            try
            {
                var product = await GetById(id);
                await _userRepository.DeleteById(id);
            }
            catch (UserNotFoundException)
            {
                throw;
            }
        }

        /// <summary>
        /// Validate that field name is correct.
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        private void ValidateName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name canno't be empty or null");
        }

        /// <summary>
        /// Validate that field login is correct.
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        private void ValidateLogin(string login)
        {
            if(string.IsNullOrEmpty(login))
                throw new ArgumentException("Login canno't be empty or null");
        }

        /// <summary>
        /// Validate that field password is correct.
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        private void ValidatePassword(string password)
        {
            if(string.IsNullOrEmpty(password))
                throw new ArgumentException("Password canno't be empty or null");
        }
    }
}
