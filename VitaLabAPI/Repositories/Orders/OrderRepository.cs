using Dapper;
using VitaLabAPI.DataBaseAccsess;
using VitaLabData.DTOs;
using VitLabData;

namespace VitaLabAPI.Repositories.Orders
{
    /// <summary>
    /// Implementation of <see cref="IOrderRepository"/>
    /// </summary>
    public class OrderRepository : IOrderRepository
    {
        private readonly DapperContext _dapperContext;
        private const string TableName = "[Order]";

        public OrderRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        /// <inheritdoc/>
        public async Task Create(Order item)
        {
            using var connection = _dapperContext.CreateConnection();
            var query = $"INSERT INTO {TableName} (OrderId, UserId, OrderDate, TotalPrice)";
            var parameters = new DynamicParameters();
            parameters.Add("OrderId", item.Id);
            parameters.Add("UserId", item.User.Id);
            parameters.Add("OrderDate", item.Date);
            parameters.Add("TotalPrice", item.TotalPrice);
            await connection.ExecuteAsync(query, parameters);
        }

        public async Task<int> CreateNewOrder(Order item)
        {
            using var connection = _dapperContext.CreateConnection();
            var query = $"INSERT INTO {TableName} ([UserId], [OrderDate], [TotalPrice]) VALUES(@UserId, @OrderDate, @TotalPrice)";
            var parameters = new DynamicParameters();
            parameters.Add("UserId", item.User.Id);
            parameters.Add("OrderDate", item.Date);
            parameters.Add("TotalPrice", item.TotalPrice);
            await connection.ExecuteAsync(query, parameters);
            var getLastIdQuery = $"SELECT TOP 1 * FROM {TableName} JOIN [User] on [User].[UserId] = [Order].[UserId] ORDER BY [OrderId] DESC";
            var result = await connection.QuerySingleAsync<Order>(getLastIdQuery);
            return result.Id;
        }

        /// <inheritdoc/>
        public async Task<Order> GetById(int id)
        {
            using var connection = _dapperContext.CreateConnection();
            var query = $"SELECT * FROM {TableName} JOIN [User] on [User].[UserId] = [Order].[UserId] WHERE [OrderId] = @Id";
            var result = await connection.QuerySingleAsync<Order>(query, new { Id = id });
            return result;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Order>> GetWithPagination(int pageNumber)
        {
            using var connection = _dapperContext.CreateConnection();
            var query = $"SELECT * FROM {TableName}";
            var result = await connection.QueryAsync<Order>(query);
            return result;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Order>> GetOrdersByUserId(int userId)
        {
            using var connection = _dapperContext.CreateConnection();
            string query = $"SELECT * FROM {TableName} JOIN [User] on [User].[UserId] = [Order].[UserId] WHERE [Order].[UserId] = @UserId";
            var parameters = new DynamicParameters();
            parameters.Add("UserId", userId);
            var result = await connection.QueryAsync<Order>(query, parameters);
            return result;
        }

        /// <inheritdoc/>
        public async Task Update(Order item)
        {
            using var connection = _dapperContext.CreateConnection();
            string sqlQuery = $"UPDATE {TableName} SET [UserId] = @UserId, [OrderDate] = @OrderDate, [TotalPrice] = @TotalPrice WHERE [OrderId] = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("UserId", item.User.Id);
            parameters.Add("OrderDate", item.Date);
            parameters.Add("TotalPrice", item.TotalPrice);
            await connection.ExecuteAsync(sqlQuery, parameters);
        }

        /// <inheritdoc/>
        public async Task DeleteById(int id)
        {
            using var connection = _dapperContext.CreateConnection();
            string query = $"DELETE FROM {TableName} WHERE [OrderId] = @Id";
            await connection.ExecuteAsync(query, new { Id = id });
        }


    }
}
