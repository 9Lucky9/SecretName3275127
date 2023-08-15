using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using VitLabData.DTOs;
using VitLabData.DTOs.Create;

namespace VitaLabManager.Services.Product
{
    public class ProductManager : IProductManager
    {
        private readonly IVitaLabApiWrapper _apiWrapper;
        private const string APIEndpoint = "Product";
        public ProductManager(IVitaLabApiWrapper apiWrapper)
        {
            _apiWrapper = apiWrapper;
        }

        public async Task CreateNewProduct(ProductCreateRequest productCreateRequest)
        {
            var apiEndPoint = $"{APIEndpoint}/Post";
            var json = JsonContent.Create<ProductCreateRequest>(productCreateRequest);
            await _apiWrapper.HttpClient.PostAsync(apiEndPoint, json);
        }

        public async Task<IEnumerable<ProductDTO>> LoadByPage(int pageNumber)
        {
            var apiEndPoint = $"{APIEndpoint}/GetByPage";
            var request = $"{apiEndPoint}?page={pageNumber}";
            var response = await _apiWrapper.HttpClient.GetAsync(request);
            var products = await response.Content.ReadFromJsonAsync<IEnumerable<ProductDTO>>();
            return products;
        }

        public Task ChangeProduct(ProductDTO product)
        {
            throw new System.NotImplementedException();
        }

        public Task<ProductDTO> GetProductById(int productId)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<ProductDTO>> GetProductsByName(string name)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteProduct(int productId)
        {
            throw new System.NotImplementedException();
        }


    }
}
