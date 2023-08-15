using Dapper;
using VitaLabAPI.DataBaseAccsess;
using VitLabData;

namespace VitaLabAPI.Repositories.Products
{
    public class ProductRepository : IProductRepository
    {
        private readonly DapperContext _dapperContext;
        private const string TableName = "[Product]";

        public ProductRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        /// <inheritdoc/>
        public async Task Create(Product item)
        {
            using var connection = _dapperContext.CreateConnection();
            var query = $"INSERT INTO {TableName} (ProductId, ProductName, Price)";
            var parameters = new DynamicParameters();
            parameters.Add("ProductId", item.Id);
            parameters.Add("ProductName", item.Name);
            parameters.Add("Price", item.Price);
            await connection.ExecuteAsync(query, parameters);
        }

        /// <inheritdoc/>
        public async Task<Product> GetById(int id)
        {
            using var connection = _dapperContext.CreateConnection();
            var query = $"SELECT * FROM {TableName} WHERE [ProductId] = @Id";
            var result = await connection.QuerySingleAsync<Product>(query, new { Id = id });
            return result;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Product>> GetWithPagination(int pageNumber)
        {
            var count = 20;
            var offset = 0;
            if (pageNumber > 1)
            {
                offset = (count * pageNumber) - count;
            }
            using var connection = _dapperContext.CreateConnection();
            var query = $"SELECT * FROM {TableName} ORDER BY [Product].[ProductId] OFFSET @Offset ROWS FETCH NEXT @Count ROWS ONLY;";
            var parameters = new DynamicParameters();
            parameters.Add("Offset", offset);
            parameters.Add("Count", count);
            var result = await connection.QueryAsync<Product>(query, parameters);
            return result;
        }

        /// <inheritdoc/>
        public async Task DeleteById(int id)
        {
            using var connection = _dapperContext.CreateConnection();
            string query = $"DELETE FROM {TableName} WHERE [ProductId] = @Id";
            await connection.ExecuteAsync(query, new { Id = id });
        }

        /// <inheritdoc/>
        public async Task Update(Product item)
        {
            using var connection = _dapperContext.CreateConnection();
            string sqlQuery = $"UPDATE {TableName} SET [ProductName] = @ProductName, [Price] = @Price WHERE [ProductId] = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("ProductName", item.Name);
            parameters.Add("Price", item.Price);
            await connection.ExecuteAsync(sqlQuery, parameters);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Product>> FindByName(string name)
        {
            using var connection = _dapperContext.CreateConnection();
            var query = $"SELECT * FROM {TableName} WHERE [ProductName] = @Name";
            var result = await connection.QueryAsync<Product>(query, new { Name = name });
            return result;
        }
    }
}
