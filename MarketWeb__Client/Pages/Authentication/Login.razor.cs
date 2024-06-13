using MarketWeb__Client.Service.IService;
using MarketWeb_Models;
using Microsoft.AspNetCore.Components;

namespace MarketWeb__Client.Pages.Authentication
{
    public partial class Login
    {
        [Inject]
        public IAuthenticationService _authService { get; set; }
        [Inject]
        public NavigationManager _navigationManager { get; set; }

        private SignInRequestDTO SignInRequest = new();

        public bool IsProcessing { get; set; } = false;
        public bool ShowSignInErrors { get; set; }
        public string Errors { get; set; }

        private async Task LoginUser()
        {
            ShowSignInErrors = false;
            IsProcessing = true;
            var result = await _authService.Login(SignInRequest);
            if (result.IsAuthSuccessful)
            {
                //registration is successful
                _navigationManager.NavigateTo("/", forceLoad: true);

                //Eviter de forcer le relaod : voir AuthStateProvider.NotifyUserLoggedIn
                _navigationManager.NavigateTo("/");
               }
            else
            { 
                //failure
                Errors = result.ErrorMessage;
                ShowSignInErrors = true;
            }

            IsProcessing = false;
        }

    }
}
