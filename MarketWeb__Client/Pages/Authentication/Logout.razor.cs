using MarketWeb__Client.Service.IService;
using Microsoft.AspNetCore.Components;

namespace MarketWeb__Client.Pages.Authentication
{
    public partial class Logout
    {
        [Inject]
        public IAuthenticationService _authService { get; set; }
        [Inject]
        public NavigationManager _navigationManager { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await _authService.Logout();
            //Forcer le relaod
            //_navigationManager.NavigateTo("/", forceLoad: true);

            //Eviter de forcer le relaod : voir AuthStateProvider.NotifyUserLoggout
            _navigationManager.NavigateTo("/");
        }
    }
}
