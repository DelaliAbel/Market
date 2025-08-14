using MarketWeb__Client.Service.IService;
using Microsoft.AspNetCore.Components;

namespace MarketWeb__Client.Pages.Authentication
{
    public partial class RedirectToLogin
    {
        [Inject]
        public IAuthenticationService _authService { get; set; }
        [Inject]
        public NavigationManager _navigationManager { get; set; }



    }
}
