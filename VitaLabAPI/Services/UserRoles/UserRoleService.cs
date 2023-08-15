using VitaLabAPI.Exceptions.UserRole;
using VitaLabAPI.Exceptions.Users;
using VitaLabAPI.Repositories.UserRoles;
using VitaLabAPI.Repositories.Users;
using VitaLabData;
using VitaLabData.DTOs.Create;
using VitLabData;
using VitLabData.DTOs;
using VitLabData.DTOs.Create;

namespace VitaLabAPI.Services.UserRoles
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IUserRepository _userRepository;

        public UserRoleService(IUserRoleRepository userRoleRepository, IUserRepository userRepository)
        {
            _userRoleRepository = userRoleRepository;
            _userRepository = userRepository;
        }

        /// <inheritdoc/>
        /// <exception cref="UserRoleAlreadyExists"></exception>
        public async Task Create(UserRoleCreateRequest entity)
        {
            var foundUserRole = await _userRoleRepository.GetByName(entity.Name);
            if (foundUserRole is not null)
                throw new UserRoleAlreadyExists();
            var userRole = new UserRole(0, entity.Name);
            await _userRoleRepository.Create(userRole);
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentException"></exception>
        public async Task<IEnumerable<UserRole>> GetByPage(int page)
        {
            if (page <= 0)
                throw new ArgumentException("Page canno't be zero or less");
            return await _userRoleRepository.GetWithPagination(page);
        }

        /// <inheritdoc/>
        /// <exception cref="UserRoleNotFoundException"></exception>
        public async Task<UserRole> GetById(int id)
        {
            var foundUserRole = await _userRoleRepository.GetById(id);
            if (foundUserRole is null)
                throw new UserRoleNotFoundException();
            return await _userRoleRepository.GetById(id);
        }

        /// <inheritdoc/>
        /// <exception cref="UserRoleNotFoundException"></exception>
        public async Task<IEnumerable<UserRoleEnum>> GetRolesByUserId(int userId)
        {
            var foundUser = await _userRepository.GetById(userId);
            if(foundUser is null)
                throw new UserNotFoundException();
            var userRoles = await _userRoleRepository.GetRolesByUserId(userId);
            var userRolesEnums = UserRoleToRoleEnumConvertor.ConvertToRoleEnum(userRoles);
            return userRolesEnums;
        }

        /// <inheritdoc/>
        /// <exception cref="UserRoleNotFoundException"></exception>
        public async Task Edit(UserRoleDTO entity)
        {
            try
            {
                var userRole = await GetById(entity.Id);
                userRole.Name = entity.Name;
                await _userRoleRepository.Update(userRole);
            }
            catch (UserRoleNotFoundException)
            {
                throw;
            }
        }

        /// <inheritdoc/>
        /// <exception cref="UserRoleNotFoundException"></exception>
        public async Task DeleteById(int id)
        {
            try
            {
                var userRole = await GetById(id);
                await _userRoleRepository.DeleteById(id);
            }
            catch (UserRoleNotFoundException)
            {
                throw;
            }
        }

        public async Task AssignRolesToUser(UsersRolesAssignRequest request)
        {
            try
            {
                foreach(var userRoleId in request.RoleIds)
                {
                    var userRole = await GetById(userRoleId);
                }
                var user = await _userRepository.GetById(request.UserId);
                if(user is null)
                    throw new UserNotFoundException();
                await _userRoleRepository.AssignRolesToUser(request.UserId, request.RoleIds);
            }
            catch (UserRoleNotFoundException)
            {
                throw;
            }
        }
    }
}
