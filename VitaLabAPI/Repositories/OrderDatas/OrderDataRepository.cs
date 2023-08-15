using Dapper;
using VitaLabAPI.DataBaseAccsess;
using VitLabData;

namespace VitaLabAPI.Repositories.OrderDatas
{
    public class OrderDataRepository : IOrderDataRepository
    {
        private readonly DapperContext _dapperContext;
        private const string TableName = "[OrderData]";

        public OrderDataRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task Create(OrderData item)
        {
            using var connection = _dapperContext.CreateConnection();
            var query = $"INSERT INTO {TableName} (OrderId, ProductId, ProductQuantity, TotalPrice) VALUES(@OrderId, @ProductId, @ProductQuantity, @TotalPrice)";
            var parameters = new DynamicParameters();
            parameters.Add("OrderId", item.Order.Id);
            parameters.Add("ProductId", item.Product.Id);
            parameters.Add("ProductQuantity", item.ProductQuantity);
            parameters.Add("TotalPrice", item.TotalPrice);
            await connection.ExecuteAsync(query, parameters);
        }

        public async Task<OrderData> GetById(int id)
        {
            using var connection = _dapperContext.CreateConnection();
            var query = $"SELECT * FROM {TableName} WHERE [OrderDataId] = @Id";
            var result = await connection.QuerySingleAsync<OrderData>(query, new { Id = id });
            return result;
        }

        public async Task<IEnumerable<OrderData>> GetWithPagination(int pageNumber)
        {
            using var connection = _dapperContext.CreateConnection();
            var query = $"SELECT * FROM {TableName}";
            var result = await connection.QueryAsync<OrderData>(query);
            return result;
        }

        public async Task<IEnumerable<OrderData>> GetOrderDatasByOrderId(int orderId)
        {
            using var connection = _dapperContext.CreateConnection();
            var query = @$"SELECT [OrderData].[OrderDataId], [OrderData].[OrderId], [OrderData].[ProductId], [OrderData].[ProductQuantity], [OrderData].[TotalPrice], 
                            [Order].[UserId], [Order].[OrderDate], [Order].[TotalPrice] as 'OrderTotalPrice', 
                            [Product].[ProductName], [Product].[Price],
                            [User].[UserName], [User].[Login], [User].[Password]
                            FROM {TableName}
                            JOIN [Order] on [Order].[OrderId] = [OrderData].[OrderId]
                            JOIN [Product] on [Product].[ProductId] = [OrderData].[ProductId]
                            JOIN [User] on [User].[UserId] = [Order].[UserId]
                            WHERE [OrderData].[OrderId] = @OrderId";
            var parameters = new DynamicParameters();
            parameters.Add("OrderId", orderId);
            var result = await connection.QueryAsync<OrderData>(query, parameters);
            return result;
        }

        public async Task Update(OrderData item)
        {
            using var connection = _dapperContext.CreateConnection();
            var query = $"UPDATE {TableName} SET OrderId = @OrderId, ProductId = @ProductId, ProductQuantity = @ProductQuantity, TotalPrice = @TotalPrice WHERE [OrderDataId] = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("OrderId", item.Order.Id);
            parameters.Add("ProductId", item.Product.Id);
            parameters.Add("ProductQuantity", item.ProductQuantity);
            parameters.Add("TotalPrice", item.TotalPrice);
            await connection.ExecuteAsync(query, parameters);
        }

        public async Task DeleteById(int id)
        {
            using var connection = _dapperContext.CreateConnection();
            string query = $"DELETE FROM {TableName} WHERE [OrderDataId] = @Id";
            await connection.ExecuteAsync(query, new { Id = id });
        }
    }
}
