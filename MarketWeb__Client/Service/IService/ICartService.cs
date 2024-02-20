using MarketWeb__Client.ViewModel;

namespace MarketWeb__Client.Service.IService
{
    public interface ICartService
    {
		public event Action Onchange; // Permet de déclenché une methode
		Task DescrementCart(ShoppingCart i_cartToDecrement);
        Task IncrementCart(ShoppingCart i_cartToAdd);

    }
}
