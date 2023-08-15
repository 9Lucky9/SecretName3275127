using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using VitaLabManager.MVVM.Models;
using VitaLabManager.Services.Product;
using VitLabData.DTOs;
using VitLabData.DTOs.Create;

namespace VitaLabManager.MVVM.ViewModels
{
    public partial class ProductControlViewModel : ViewModel
    {
        private readonly IProductManager _productManager;

        private Task _init;

        [ObservableProperty]
        private int _currentPage = 1;

        [ObservableProperty]
        private ObservableCollection<ProductModel> _products;

        public ProductControlViewModel(IProductManager productManager)
        {
            _productManager = productManager;
            _init = LoadDataAsync();
        }

        public async Task LoadDataAsync()
        {
            var loadedDto = (await _productManager.LoadByPage(CurrentPage));
            Products = new ObservableCollection<ProductModel>(loadedDto.Select(ToProductModel));
            Products.CollectionChanged += Products_CollectionChanged; ;
        }

        private async void Products_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    var productModel = (ProductModel)sender;
                    await Remove(productModel.Id);
                    break;
            }
        }

        public async Task Add(ProductCreateRequest productCreateRequest)
        {
            await _productManager.CreateNewProduct(productCreateRequest);
        }

        public async Task Remove(int productId)
        {
            await _productManager.DeleteProduct(productId);
        }

        private ProductModel ToProductModel(ProductDTO productDTO)
        {
            return new ProductModel()
            {
                Id = productDTO.Id,
                Name = productDTO.Name,
                Price = productDTO.Price,
            };
        }

        private ProductDTO ToProductDTO(ProductModel productModel)
        {
            return new ProductDTO(
                productModel.Id,
                productModel.Name,
                productModel.Price);
        }
    }
}
