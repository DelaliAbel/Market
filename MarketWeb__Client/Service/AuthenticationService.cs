using Blazored.LocalStorage;
using MarketWeb__Client.Helper;
using MarketWeb__Client.Service.IService;
using MarketWeb_Common;
using MarketWeb_Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;

namespace MarketWeb__Client.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _client;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly object? signInRequestDTO;
        [Inject]
        public IJSRuntime _JsRuntime { get; set; }

        public AuthenticationService(HttpClient client, ILocalStorageService localStorage, AuthenticationStateProvider authStateProvider)
        {
            _client = client;
            _localStorage = localStorage;
            _authStateProvider = authStateProvider;
        }

        public async Task<SignInResponseDTO> Login(SignInRequestDTO signInRequest)
        {
            try
            {
                var content = JsonConvert.SerializeObject(signInRequest);
                var bodyContent = new StringContent(content, System.Text.Encoding.UTF8, "application/json");
                var response = await _client.PostAsync("api/account/signin", bodyContent);
                var contentTemp = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<SignInResponseDTO>(contentTemp);

                if (response.IsSuccessStatusCode)
                {
                    await _localStorage.SetItemAsync(StaticDetails.Local_Token, result.Token);
                    await _localStorage.SetItemAsync(StaticDetails.Local_UserDtails, result.UserDTO);

                    ((AuthStateProvider)_authStateProvider).NotifyUserLoggedIn(result.Token);

                    _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Token);
                    
                    return new SignInResponseDTO() { IsAuthSuccessful = true };
                }
                else
                {
                    return result;
                }
            }
            catch(Exception ex) 
            {
                _JsRuntime.ToatrError(ex.Message);
                return null;
            }

        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync(StaticDetails.Local_Token);
            await _localStorage.RemoveItemAsync(StaticDetails.Local_UserDtails);

            ((AuthStateProvider)_authStateProvider).NotifyUserLogout();

            _client.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<SignUpResponseDTO> RegisterUser(SignUpRequestDTO signUpRequest)
        {
            var content = JsonConvert.SerializeObject(signUpRequest);
            var bodyContent = new StringContent(content, System.Text.Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/account/signup", bodyContent);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<SignUpResponseDTO>(contentTemp);

            if (response.IsSuccessStatusCode)
            {
                return new SignUpResponseDTO() { IsRegistrationSuccessful = true };
            }
            else
            {
                return new SignUpResponseDTO() { IsRegistrationSuccessful = false, Errors = result.Errors };
            }
        }
    }
}
