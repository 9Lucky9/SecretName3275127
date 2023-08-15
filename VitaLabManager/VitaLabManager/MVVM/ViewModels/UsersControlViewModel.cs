using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using VitaLabManager.MVVM.Models;
using VitaLabManager.Services.User;
using VitLabData.DTOs;
using VitLabData.DTOs.Create;

namespace VitaLabManager.MVVM.ViewModels
{
    public partial class UsersControlViewModel : ViewModel
    {
        private readonly IUserManager _userManager;

        private Task _init;

        [ObservableProperty]
        private ObservableCollection<UserModel> _users;

        [ObservableProperty]
        private int _currentPage = 1;

        public UsersControlViewModel(IUserManager userManager)
        {
            _userManager = userManager;
            _init = LoadDataAsync();
        }

        private async void Users_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch(e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    var userDto = ToUserDTO(((UserModel)sender));
                    await Remove(userDto);
                    break;
            }
        }

        private async Task LoadDataAsync()
        {
            var loadedDto = (await _userManager.LoadByPage(CurrentPage));
            Users = new ObservableCollection<UserModel>(loadedDto.Select(ToUserModel));
            Users.CollectionChanged += Users_CollectionChanged;
        }

        public async Task Create(UserCreateRequest userCreateRequest)
        {
            await _userManager.CreateUser(userCreateRequest);
        }

        public async Task Remove(UserDTO userDTO)
        {
            await _userManager.RemoveUser(userDTO);
        }

        private UserModel ToUserModel(UserDTO userDTO)
        {
            return new UserModel()
            {
                Id = userDTO.Id,
                Name = userDTO.Name,
                Login = userDTO.Login,
                Password = userDTO.Password,
            };
        }

        private UserDTO ToUserDTO(UserModel userModel)
        {
            return new UserDTO(
                userModel.Id, 
                userModel.Name, 
                userModel.Login, 
                userModel.Password);
        }
    }
}
