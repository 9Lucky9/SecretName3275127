using VitLabData;
using VitLabData.DTOs;
using VitLabData.DTOs.Create;

namespace VitaLabAPI.Services.Products
{
    public interface IProductService : 
        IServiceBase<Product>,
        IEntityCreate<ProductCreateRequest>,
        IEntityEdit<ProductDTO>
    {
        public Task<IEnumerable<Product>> FindByName(string name); 
    }
}
