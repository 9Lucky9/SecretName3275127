using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using VitaLabData.DTOs;
using VitaLabManager.Exceptions;
using VitLabData;
using VitLabData.DTOs;

namespace VitaLabManager.Services
{
    public class VitaLabApiWrapper : IVitaLabApiWrapper
    {
        public HttpClient HttpClient { get; private set; }
        public UserDTO CurrentUser { get; private set; }

        private readonly IConfiguration _configuration;

        public VitaLabApiWrapper(IConfiguration configuration)
        {
            _configuration = configuration;
            HttpClient = new HttpClient();
            HttpClient.BaseAddress =
                new Uri(_configuration.GetConnectionString("VitaLabApi"));
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<UserRoleEnum>> AuthorizeClient(UserLoginRequest userLoginRequest)
        {
            try
            {
                var APIEndPoint = "Identity/Login";
                var json = JsonContent.Create(userLoginRequest);
                var response = await HttpClient.PostAsync(APIEndPoint, json);
                if(response.StatusCode is System.Net.HttpStatusCode.BadRequest)
                {
                    throw new AuthenticationFailed();
                }
                var bearer = await response.Content.ReadAsStringAsync();
                HttpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {bearer}");
                CurrentUser = await GetAuthorizedUserInfo(bearer);
                return await GetUserRoles(bearer);
            }
            catch (HttpRequestException)
            {
                throw;
            }
        }

        private async Task<UserDTO> GetAuthorizedUserInfo(string bearer)
        {
            var APIEndPoint = "Identity/GetUserInformation";
            var request = $"{APIEndPoint}?bearerToken={bearer}";
            var response = await HttpClient.GetAsync(request);
            var userDto = await response.Content.ReadFromJsonAsync<UserDTO>();
            return userDto;
        }

        /// <summary>
        /// Get user roles by sending bearer token to the API.
        /// </summary>
        /// <returns>List of User roles.</returns>
        private async Task<IEnumerable<UserRoleEnum>> GetUserRoles(string bearer)
        {
            var APIEndPoint = "Identity/GetRoles";
            var request = $"{APIEndPoint}?bearerToken={bearer}";
            var response = await HttpClient.GetAsync(request);
            var userRoles = await response.Content.ReadFromJsonAsync<IEnumerable<UserRoleEnum>>();
            return userRoles;
        }
    }
}
