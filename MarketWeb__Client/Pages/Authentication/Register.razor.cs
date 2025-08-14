using MarketWeb__Client.Service.IService;
using MarketWeb_Models;
using Microsoft.AspNetCore.Components;

namespace MarketWeb__Client.Pages.Authentication
{
    public partial class Register
    {
        [Inject]
        public IAuthenticationService _authService { get; set; }
        [Inject]
        public NavigationManager _navigationManager { get; set; }

        private SignUpRequestDTO SignUpRequest = new();

        public bool IsProcessing { get; set; } = false;
        public bool ShowRegistrationErrors { get; set; }
        public IEnumerable<string> Errors { get; set; }

        private async Task RegisterUser()
        {
            ShowRegistrationErrors = false;
            IsProcessing = true;
            var result = await _authService.RegisterUser(SignUpRequest);
            if (result.IsRegistrationSuccessful)
            {
                //registration is successful
                _navigationManager.NavigateTo("/login");
            }
            else
            {
                //failure
                Errors = result.Errors;
                ShowRegistrationErrors = true;
            }

            IsProcessing = false;
        }
    
}
}
