using VitaLabAPI.Exceptions.Products;
using VitaLabAPI.Repositories.Products;
using VitLabData;
using VitLabData.DTOs;
using VitLabData.DTOs.Create;

namespace VitaLabAPI.Services.Products
{
    /// <inheritdoc cref="IProductService"/>
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        /// <inheritdoc/>
        public async Task Create(ProductCreateRequest entity)
        {
            if (string.IsNullOrEmpty(entity.Name))
            {
                throw new ArgumentException("Name canno't be empty or null");
            }
            if (entity.Price < 0)
            {
                throw new ArgumentException("Price canno't be below zero.");
            }
            var product = new Product(0, entity.Name, entity.Price);
            await _productRepository.Create(product);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Product>> GetByPage(int page)
        {
            if (page <= 0)
                throw new ArgumentException("Page canno't be zero or less");
            return await _productRepository.GetWithPagination(page);
        }

        /// <inheritdoc/>
        public async Task<Product> GetById(int id)
        {
            var product = await _productRepository.GetById(id);
            if (product is null)
                throw new ProductNotFoundException();
            return product;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Product>> FindByName(string name)
        {
            return await _productRepository.FindByName(name);
        }

        /// <inheritdoc/>
        public async Task Edit(ProductDTO entity)
        {
            try
            {
                if (string.IsNullOrEmpty(entity.Name))
                {
                    throw new ArgumentException("Name canno't be empty or null");
                }
                if (entity.Price < 0)
                {
                    throw new ArgumentException("Price canno't be below zero.");
                }
                var product = await GetById(entity.Id);
                product.Id = entity.Id;
                product.Name = entity.Name;
                product.Price = entity.Price;
                await _productRepository.Update(product);
            }
            catch (ProductNotFoundException)
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
                await _productRepository.DeleteById(id);
            }
            catch (ProductNotFoundException)
            {
                throw;
            }
        }
    }
}
