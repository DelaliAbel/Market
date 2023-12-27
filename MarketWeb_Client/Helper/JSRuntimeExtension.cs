using Microsoft.JSInterop;

namespace MarketWeb_Client.Helper
{
    public static class JSRuntimeExtension
    {
        public static async ValueTask ToatrSuccess(this IJSRuntime jsRuntime, string message)
        {
            await jsRuntime.InvokeVoidAsync("ShowToastr", "success",message);
        }
        public static async ValueTask ToatrError(this IJSRuntime jsRuntime, string message)
        {
            await jsRuntime.InvokeVoidAsync("ShowToastr", "error", message);
        }
    }
}
