using VitLabData;

namespace VitaLabAPI.Repositories.Products
{
    /// <summary>
    /// <see cref="Product"/> repository.
    /// </summary>
    public interface IProductRepository : IRepository<Product>
    {
        /// <summary>
        /// Find products with provided name.
        /// </summary>
        public Task<IEnumerable<Product>> FindByName(string name);
    }
}
