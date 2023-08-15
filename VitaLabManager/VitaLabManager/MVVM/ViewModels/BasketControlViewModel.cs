using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using VitaLabManager.MVVM.Models;
using VitaLabManager.Services.ISessionContext;
using VitaLabManager.Services.Order;
using VitLabData.DTOs.Create;

namespace VitaLabManager.MVVM.ViewModels
{
    public partial class BasketControlViewModel : ViewModel
    {
        private readonly ISessionContext _sessionContext;
        public ISessionContext SessionContext 
        { 
            get { return _sessionContext; }
        }

        private readonly IOrderManager _orderManager;

        public BasketControlViewModel(ISessionContext sessionContext, IOrderManager orderManager)
        {
            _sessionContext = sessionContext;
            _orderManager = orderManager;
        }

        [RelayCommand]
        public async Task DeleteFromBasket(ProductModel productToDelete)
        {
            var productOrder = _sessionContext.BasketOrder.ProductOrders.FirstOrDefault(x => x.Product.Id == productToDelete.Id);
            _sessionContext.BasketOrder.ProductOrders.Remove(productOrder);
        }

        [RelayCommand]
        public async Task MakeOrder()
        {
            var orderDataCreateRequests = new List<OrderDataCreateRequest>();

            if (!SessionContext.BasketOrder.ProductOrders.Any())
            {
                MessageBox.Show("В корзине пусто!");
                return;
            }
            foreach (var da in SessionContext.BasketOrder.ProductOrders)
            {
                orderDataCreateRequests.Add(
                    new OrderDataCreateRequest(0, da.Product.Id, da.Quantity));
            }
            var createdOrderId = await _orderManager.CreateNewOrder(orderDataCreateRequests);
        }
    }
}
