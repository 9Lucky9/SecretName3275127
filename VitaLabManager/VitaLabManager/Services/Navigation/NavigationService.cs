using CommunityToolkit.Mvvm.ComponentModel;
using System;
using VitaLabManager.MVVM.ViewModels;

namespace VitaLabManager.Services.Navigation
{
    public class NavigationService : ObservableObject, INavigationService
    {
        private readonly Func<Type, ViewModel> _viewModelFactory;
        private ViewModel _current;
        public ViewModel Current
        {
            get
            {
                return _current;
            }
            private set
            {
                _current = value;
                OnPropertyChanged();
            }
        }

        public NavigationService(Func<Type, ViewModel> viewModelFactory)
        {
            _viewModelFactory = viewModelFactory;
        }

        public void NavigateTo<T>() where T : ViewModel
        {
            var viewModel = _viewModelFactory.Invoke(typeof(T));
            Current = viewModel;
        }

    }
}
