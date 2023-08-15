using System.Collections.Generic;
using System.Threading.Tasks;
using VitLabData.DTOs;
using VitLabData.DTOs.Create;

namespace VitaLabManager.Services.Product
{
    public interface IProductManager : ILoadByPage<ProductDTO>
    {
        public Task CreateNewProduct(ProductCreateRequest productCreateRequest);
        public Task<ProductDTO> GetProductById(int productId);
        public Task<IEnumerable<ProductDTO>> GetProductsByName(string name);
        public Task ChangeProduct(ProductDTO product);
        public Task DeleteProduct(int productId);
    }
}
