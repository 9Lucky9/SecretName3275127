using VitaLabManager.MVVM.ViewModels;

namespace VitaLabManager.Services.Navigation
{
    public interface INavigationService
    {
        public ViewModel Current { get; }
        public void NavigateTo<T>() where T: ViewModel;
    }
}
