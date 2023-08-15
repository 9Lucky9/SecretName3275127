using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using VitaLabManager.Exceptions;
using VitaLabManager.MVVM.ViewModels.MainWindowViewModel;
using VitaLabManager.Services.Authorization;

namespace VitaLabManager.MVVM.ViewModels
{
    public partial class LoginWindowViewModel : ObservableObject
    {
        private readonly AuthorizationService _authorizationService;
        private readonly MainWindowViewModelFactory _mainWindowViewModelFactory;

        public LoginWindowViewModel(AuthorizationService authorizationService, MainWindowViewModelFactory mainWindowViewModelFactory)
        {
            _authorizationService = authorizationService;
            _mainWindowViewModelFactory = mainWindowViewModelFactory;
        }

        /// <summary>
        /// Constructor for WPF design instance helper.
        /// </summary>
        public LoginWindowViewModel()
        {
        }

        [ObservableProperty]
        private string login = "lucky";
        [ObservableProperty]
        private string password = "1234";
        [ObservableProperty]
        private string error;


        [RelayCommand]
        public async Task LoginMethod()
        {
            if (string.IsNullOrEmpty(Login))
            {
                Error = "Логин не может быть пустым.";
                return;
            }
            if (string.IsNullOrEmpty(Password))
            {
                Error = "Пароль не может быть пустым.";
                return;
            }
            try
            {
                var userRoles = await _authorizationService.Authorize(Login, Password);
                var mainWindow = new MainWindow();
                var mainWindowViewModel = _mainWindowViewModelFactory.CreateViewModel(userRoles);
                mainWindow.DataContext = mainWindowViewModel;
                mainWindow.Show();
                CloseWindow();
            }
            catch (AuthenticationFailed)
            {
                Error = "Не правильный логин или пароль.";
                return;
            }
            catch (HttpRequestException)
            {
                MessageBox.Show("Не возможно присоединиться к серверу, проверьте подключение к интернету или обратитесь в отдел программистов.");
            }
        }

        private void CloseWindow()
        {
            foreach (Window item in Application.Current.Windows)
            {
                if (item.DataContext == this) item.Close();
            }
        }
    }
}
