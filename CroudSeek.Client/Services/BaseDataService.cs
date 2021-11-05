using Blazored.LocalStorage;
using CroudSeek.Client.Contracts;
using Microsoft.AspNetCore.Components;
using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CroudSeek.Client.Services
{
    public class BaseDataService
    {
        protected readonly ILocalStorageService _localStorage;
        NavigationManager _navigation;
        protected IClient _client;
        private IAuthenticationService _authenticationService;

        public BaseDataService(IClient client, ILocalStorageService localStorage, NavigationManager navigation,IAuthenticationService authenticationService)
        {
            _client = client;
            _localStorage = localStorage;
            _navigation = navigation;
            _authenticationService = authenticationService;
        }

        protected ApiResponse<Guid> ConvertApiExceptions<Guid>(ApiException ex)
        {
            if (ex.StatusCode == 400)
            {
                return new ApiResponse<Guid>() { Message = "Validation errors have occured.", ValidationErrors = ex.Response, Success = false };
            }
            else if (ex.StatusCode == 404)
            {
                return new ApiResponse<Guid>() { Message = "The requested item could not be found.", Success = false };
            }
            else
            {
                return new ApiResponse<Guid>() { Message = "Something went wrong, please try again.", Success = false };
            }
        }

        protected async Task<bool> AddBearerToken(bool ignoreExpired=false)
        {
            if (await _localStorage.ContainKeyAsync("expiry"))
            {
                var expiration = await _localStorage.GetItemAsync<string>("expiry");
                var expiry = DateTime.Parse(expiration);
                if (DateTime.Now > expiry)
                {
                    await _authenticationService?.Logout();
                    if (ignoreExpired)
                    {
                        return true;
                    }
                    _navigation.NavigateTo("login");
                    return false;
                }
            }

            if (await _localStorage.ContainKeyAsync("token"))
                _client.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _localStorage.GetItemAsync<string>("token"));
            return true;
        }
    }
}
