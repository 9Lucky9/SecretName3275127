using Dapper;
using System.Text;
using VitaLabAPI.DataBaseAccsess;
using VitLabData;

namespace VitaLabAPI.Repositories.UserRoles
{
    /// <inheritdoc cref="IUserRoleRepository"/>
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly DapperContext _dapperContext;
        private const string TableName = "[UserRole]";
        private const string ManyToManyTableName = "[UsersRoles]";

        public UserRoleRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        /// <inheritdoc/>
        public async Task Create(UserRole role)
        {
            using var connection = _dapperContext.CreateConnection();
            var query = $"INSERT INTO {TableName} (UserRoleId, RoleName)";
            var parameters = new DynamicParameters();
            parameters.Add("UserRoleId", 0);
            parameters.Add("RoleName", role.Name);
            await connection.ExecuteAsync(query, parameters);
        }

        /// <inheritdoc/>
        public async Task<UserRole> GetById(int id)
        {
            using var connection = _dapperContext.CreateConnection();
            var query = @$"SELECT * FROM {TableName} 
                           WHERE [UserRoleId] = @Id";
            var result = await connection.QuerySingleAsync<UserRole>(query, new { Id = id });
            return result;
        }

        /// <inheritdoc/>
        public async Task<UserRole> GetByName(string name)
        {
            using var connection = _dapperContext.CreateConnection();
            var query = @$"SELECT * FROM {TableName} 
                           WHERE [RoleName] = @Name";
            var result = await connection.QuerySingleAsync<UserRole>(query, new { Name = name });
            return result;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<UserRole>> GetRolesByUserId(int userId)
        {
            using var connection = _dapperContext.CreateConnection();
            var query = @$"SELECT * FROM {ManyToManyTableName} 
                           WHERE UserId = @UserId";
            var result = await connection.QueryAsync<UserRole>(query, new { UserId = userId });
            return result;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<UserRole>> GetWithPagination(int pageNumber)
        {
            using var connection = _dapperContext.CreateConnection();
            var query = $"SELECT * FROM {TableName}";
            var result = await connection.QueryAsync<UserRole>(query);
            return result;
        }

        /// <inheritdoc/>
        public async Task DeleteById(int id)
        {
            using var connection = _dapperContext.CreateConnection();
            string query = @$"DELETE FROM {TableName} 
                              WHERE [UserRoleId] = @Id";
            await connection.ExecuteAsync(query, new { Id = id });
        }

        /// <inheritdoc/>
        public async Task Update(UserRole item)
        {
            using var connection = _dapperContext.CreateConnection();
            string query = @$"UPDATE {TableName} 
                                 SET [RoleName] = @RoleName 
                                 WHERE [UserRoleId] = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("RoleName", item.Name);
            await connection.ExecuteAsync(query, parameters);
        }

        public async Task AssignRolesToUser(int userId, IEnumerable<int> userRoleEnums)
        {
            using var connection = _dapperContext.CreateConnection();
            var queryBuilder = new StringBuilder($"INSERT INTO {TableName} ([UserId], [RoleId]) VALUES ");
            string query = $"INSERT INTO {TableName} ([UserId], [RoleId]) VALUES (@UserId, @RoleId)";
            var parameters = new DynamicParameters();
            var userRoleEnumsList = userRoleEnums.ToList();
            for (int i = 0; i < userRoleEnumsList.Count(); i++)
            {
                queryBuilder.AppendLine($"(@UserId{i}, @RoleId{i})");
                parameters.Add($"UserId{i}", userId);
                parameters.Add($"RoleId{i}", userRoleEnumsList[i]);
            }
            await connection.ExecuteAsync(query, parameters);
        }
    }
}
