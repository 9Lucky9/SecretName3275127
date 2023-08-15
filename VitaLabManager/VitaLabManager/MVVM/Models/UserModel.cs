using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using VitLabData.DTOs;

namespace VitaLabManager.MVVM.Models
{
    public partial class UserModel : ObservableObject
    {
        [ObservableProperty]
        private int _id;
        [ObservableProperty]
        private string _name;
        [ObservableProperty]
        private string _login;
        [ObservableProperty]
        private string _password;
        [ObservableProperty]
        private List<UserRoleDTO> _userRoles;

        public UserModel()
        {
            
        }
    }
}
