using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VitaLabManager.MVVM.Models;
using VitaLabManager.Services.ISessionContext;
using VitaLabManager.Services.Product;
using VitLabData.DTOs;

namespace VitaLabManager.MVVM.ViewModels
{
    public partial class ProductOrderControlViewModel : ViewModel
    {
        private readonly IProductManager _productManager;
        private readonly ISessionContext _sessionContext;

        private Task _init;

        [ObservableProperty]
        private List<ProductDTO> _products;

        [ObservableProperty]
        private int _currentPage = 1;

        public ProductOrderControlViewModel(IProductManager productManager, ISessionContext sessionContext)
        {
            _productManager = productManager;
            _sessionContext = sessionContext;
            _init = LoadDataAsync();
        }

        [RelayCommand]
        public void AddToBasket(AddToBasketModel addtoBasketModel)
        {
            //var commandParameters = commandParameterString.Split(':');
            //var productId = Convert.ToInt32(commandParameters[0]);
            //var quantity = Convert.ToInt32(commandParameters[1]);
            var selectedProduct = Products
                .FirstOrDefault(x => x.Id == addtoBasketModel.ProductId);
            var product = new ProductModel() 
            { 
                Id = addtoBasketModel.ProductId, 
                Name = selectedProduct.Name, 
                Price = selectedProduct.Price 
            };
            var productOrder = new ProductOrder() 
            { 
                Product = product, 
                Quantity = addtoBasketModel.Quantity 
            };
            _sessionContext.BasketOrder.ProductOrders.Add(productOrder);
        }

        private async Task LoadDataAsync()
        {
            var loadedDto = (await _productManager.LoadByPage(CurrentPage));
            Products = loadedDto.ToList();
        }
    }
}
