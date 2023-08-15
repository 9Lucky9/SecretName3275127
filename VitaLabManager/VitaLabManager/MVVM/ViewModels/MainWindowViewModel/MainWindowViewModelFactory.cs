using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using VitaLabManager.Services.Navigation;
using VitLabData;

namespace VitaLabManager.MVVM.ViewModels.MainWindowViewModel
{
    public class MainWindowViewModelFactory : ObservableObject
    {
        private INavigationService _navigationService;

        public INavigationService NavigationService
        {
            get { return _navigationService; }
            set
            {
                _navigationService = value;
                OnPropertyChanged();
            }
        }

        private RelayCommand NavigateToCustomerViewModel { get; set; }
        private RelayCommand NavigateToProductOrderViewModel { get; set; }
        private RelayCommand NavigateToBasketViewModel { get; set; }
        private RelayCommand NavigateToProductViewModel { get; set; }
        private RelayCommand NavigateToUsersViewModel { get; set; }

        public MainWindowViewModelFactory(INavigationService navigationService)
        {
            _navigationService = navigationService;

            NavigateToCustomerViewModel =
                new RelayCommand(NavigationService.NavigateTo<CustomerControlViewModel>, () => true);
            NavigateToProductOrderViewModel =
                new RelayCommand(NavigationService.NavigateTo<ProductOrderControlViewModel>, () => true);
            NavigateToBasketViewModel =
                new RelayCommand(NavigationService.NavigateTo<BasketControlViewModel>, () => true);
            NavigateToProductViewModel =
                new RelayCommand(NavigationService.NavigateTo<ProductControlViewModel>, () => true);
            NavigateToUsersViewModel =
                new RelayCommand(NavigationService.NavigateTo<UsersControlViewModel>, () => true);
        }

        public MainWindowViewModel CreateViewModel(IEnumerable<UserRoleEnum> roles)
        {
            return new MainWindowViewModel(GenerateButtons(roles), _navigationService);
        }

        public List<ButtonData> GenerateButtons(IEnumerable<UserRoleEnum> roles)
        {
            var buttonList = new List<ButtonData>();
            foreach (UserRoleEnum role in roles)
            {
                switch (role)
                {
                    case UserRoleEnum.User:
                        buttonList.AddRange(GetCustomerButtons());
                        break;
                    case UserRoleEnum.ProductManager:
                        buttonList.AddRange(GetProductManagerButtons());
                        break;
                    case UserRoleEnum.Administrator:
                        buttonList.AddRange(GetAdministratorButtons());
                        break;
                }
            }
            return buttonList;
        }

        private List<ButtonData> GetCustomerButtons()
        {
            return new List<ButtonData>()
            {
                new ButtonData()
                {
                    ButtonName = "Мои заказы",
                    NavigateCommand = NavigateToCustomerViewModel
                },
                new ButtonData()
                {
                    ButtonName = "Заказ продуктов",
                    NavigateCommand = NavigateToProductOrderViewModel
                },
                new ButtonData()
                {
                    ButtonName = "Корзина",
                    NavigateCommand = NavigateToBasketViewModel
                }
            };
        }

        private List<ButtonData> GetProductManagerButtons()
        {
            return new List<ButtonData>()
            {
                new ButtonData()
                {
                    ButtonName = "Продукты",
                    NavigateCommand = NavigateToProductViewModel
                }
            };
        }

        private List<ButtonData> GetAdministratorButtons()
        {
            return new List<ButtonData>()
            {
                new ButtonData()
                {
                    ButtonName = "Пользователи системы",
                    NavigateCommand = NavigateToUsersViewModel
                }
            };
        }
    }
}
