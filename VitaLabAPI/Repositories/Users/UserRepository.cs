using Dapper;
using VitaLabAPI.DataBaseAccsess;
using VitaLabData;
using VitLabData;

namespace VitaLabAPI.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly DapperContext _dapperContext;

        private const string TableName = "[User]";

        public UserRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        /// <inheritdoc/>
        public async Task Create(User user)
        {
            using var connection = _dapperContext.CreateConnection();
            var query = $"INSERT INTO {TableName} ([UserName], [Login], [Password]) VALUES(@UserName, @Login, @Password)";
            var parameters = new DynamicParameters();
            parameters.Add("UserName", user.Name);
            parameters.Add("Login", user.Login);
            parameters.Add("Password", user.Password);
            await connection.ExecuteAsync(query, parameters);
        }

        /// <inheritdoc/>
        public async Task<User> GetById(int id)
        {
            using var connection = _dapperContext.CreateConnection();
            var query = @$"SELECT * FROM {TableName} WHERE [UserId] = @Id";
            var result = await connection.QuerySingleOrDefaultAsync<User>(query, new { Id = id });
            if (result is null)
                return null;
            var rolesEnum = await GetRoles(id);
            result.Roles = rolesEnum;
            return result;
        }

        /// <inheritdoc/>
        public async Task<User> GetByLogin(string login)
        {
            using var connection = _dapperContext.CreateConnection();
            var query = $"SELECT * FROM {TableName} WHERE [Login] = @Login";
            var result = await connection.QuerySingleOrDefaultAsync<User>(query, new { Login = login });
            if (result is null)
                return null;
            var rolesEnum = await GetRoles(result.Id);
            result.Roles = rolesEnum;
            return result;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<User>> FindByName(string name)
        {
            using var connection = _dapperContext.CreateConnection();
            var query = $"SELECT * FROM {TableName} WHERE [UserName] = @Name";
            var result = await connection.QueryAsync<User>(query, new { Name = name });
            return result;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<User>> GetWithPagination(int pageNumber)
        {
            var count = 20;
            var offset = 0;
            if(pageNumber > 1)
            {
                offset = (count * pageNumber) - count;
            }
            using var connection = _dapperContext.CreateConnection();
            var query = $"SELECT * FROM {TableName} ORDER BY [User].[UserId] OFFSET @Offset ROWS FETCH NEXT @Count ROWS ONLY;";
            var parameters = new DynamicParameters();
            parameters.Add("Offset", offset);
            parameters.Add("Count", count);
            var result = await connection.QueryAsync<User>(query, parameters);
            return result;
        }

        /// <inheritdoc/>
        public async Task DeleteById(int id)
        {
            using var connection = _dapperContext.CreateConnection();
            string query = $"DELETE FROM {TableName} WHERE [UserId] = @Id";
            await connection.ExecuteAsync(query, new { Id = id });
        }

        /// <inheritdoc/>
        public async Task Update(User user)
        {
            using var connection = _dapperContext.CreateConnection();
            string sqlQuery = $"UPDATE {TableName} SET [UserName] = @Name, [Login] = @Login, [Password] = @Password WHERE [UserId] = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("Name", user.Name);
            parameters.Add("Login", user.Login);
            parameters.Add("Password", user.Password);
            await connection.ExecuteAsync(sqlQuery, parameters);
        }


        private async Task<IEnumerable<UserRoleEnum>> GetRoles(int userId)
        {
            using var connection = _dapperContext.CreateConnection();
            string query = @$"SELECT [UserRoleId], [RoleName] FROM [UserRole]
                                 JOIN [UsersRoles] on [UsersRoles].[RoleId] = [UserRole] .[UserRoleId]
                                 JOIN [User] on [User].[UserId] = [UsersRoles].[UserId]
                                 WHERE [User].[UserId] = @UserId";
            var parameters = new DynamicParameters();
            parameters.Add("UserId", userId);
            var result = await connection.QueryAsync<UserRole>(query, parameters);
            return UserRoleToRoleEnumConvertor.ConvertToRoleEnum(result);
        }

    }
}
