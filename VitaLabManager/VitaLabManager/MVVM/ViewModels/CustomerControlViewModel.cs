using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using VitaLabManager.MVVM.Models;
using VitaLabManager.Services.Order;

namespace VitaLabManager.MVVM.ViewModels
{
    public partial class CustomerControlViewModel : ViewModel
    {
        private readonly IOrderManager _orderManager;

        private Task _init;

        [ObservableProperty]
        private ObservableCollection<MyOrder> _myOrders;

        public CustomerControlViewModel(IOrderManager orderManager)
        {
            _orderManager = orderManager;
            _init = LoadOrders();
        }

        private async Task LoadOrders()
        {
            MyOrders = new ObservableCollection<MyOrder>(
                await _orderManager.GetOrdersOfAuthorizedUser());
        }


        [RelayCommand]
        public async Task DeleteOrder(int id)
        {
            await _orderManager.DeleteOrder(id);
        }
    }
}
