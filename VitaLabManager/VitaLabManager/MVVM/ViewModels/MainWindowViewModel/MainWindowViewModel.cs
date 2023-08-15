using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using VitaLabManager.Services.Navigation;

namespace VitaLabManager.MVVM.ViewModels.MainWindowViewModel
{
    public partial class MainWindowViewModel : ViewModel
    {
        [ObservableProperty]
        private INavigationService _navigationService;
        public List<ButtonData> Buttons { get; set; }

        /// <summary>
        /// Constructor for WPF.
        /// </summary>
        public MainWindowViewModel()
        {
        }

        public MainWindowViewModel(List<ButtonData> buttonDatas, INavigationService navigationService)
        {
            Buttons = buttonDatas;
            _navigationService = navigationService;
        }


    }

    public class ButtonData
    {
        public string ButtonName { get; set; }
        public RelayCommand NavigateCommand { get; set; }
    }
}
