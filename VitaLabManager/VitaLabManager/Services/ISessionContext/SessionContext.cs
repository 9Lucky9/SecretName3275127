using CommunityToolkit.Mvvm.ComponentModel;
using VitaLabManager.MVVM.Models;

namespace VitaLabManager.Services.ISessionContext
{
    public partial class SessionContext : ObservableObject, ISessionContext
    {
        [ObservableProperty]
        private BasketOrder _basketOrder = new BasketOrder();
    }
}
