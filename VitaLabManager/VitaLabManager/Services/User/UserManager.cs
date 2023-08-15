using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using VitLabData.DTOs;
using VitLabData.DTOs.Create;

namespace VitaLabManager.Services.User
{
    public class UserManager : IUserManager
    {
        private readonly IVitaLabApiWrapper _apiWrapper;
        private const string APIEndpoint = "User";

        public UserManager(IVitaLabApiWrapper apiWrapper)
        {
            _apiWrapper = apiWrapper;
        }

        public async Task<IEnumerable<UserDTO>> LoadByPage(int pageNumber)
        {
            var apiEndPoint = $"{APIEndpoint}/GetByPage";
            var request = $"{apiEndPoint}?page={pageNumber}";
            var response = await _apiWrapper.HttpClient.GetAsync(request);
            var users = await response.Content.ReadFromJsonAsync<IEnumerable<UserDTO>>();
            return users;
        }

        public async Task RemoveUser(UserDTO userDTO)
        {
            var apiEndPoint = $"{APIEndpoint}/Delete";
            var request = $"{apiEndPoint}?userid={userDTO.Id}";
            await _apiWrapper.HttpClient.DeleteAsync(request);
        }

        public async Task CreateUser(UserCreateRequest userCreateRequest)
        {
            var apiEndPoint = $"{APIEndpoint}/Post";
            var json = JsonContent.Create<UserCreateRequest>(userCreateRequest);
            await _apiWrapper.HttpClient.PostAsync(apiEndPoint, json);
        }
    }
}
