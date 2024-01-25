using Blazored.LocalStorage;
using MarketWeb_Client.Service.IService;
using MarketWeb_Client.ViewModel;
using MarketWeb_Common;

namespace MarketWeb_Client.Service
{
    public class CartService : ICartService
    {

        private ILocalStorageService _localStorage;
        public event Action Onchange; // Permet de déclenché une methode

        public CartService(ILocalStorageService i_localStorage)
        {
            _localStorage = i_localStorage;
        }

        public async Task DescrementCart(ShoppingCart i_cartToDecrement)
        {
            var cart = await _localStorage.GetItemAsync<List<ShoppingCart>>(StaticDetails.ShoppingCart);

            //if count is 0 or 1 the we remove the item
            for(int i =0; i < cart.Count; i++)
            {
                if (cart[i].ProductId == i_cartToDecrement.ProductId && cart[i].ProductPriceId == i_cartToDecrement.ProductPriceId)
                {
                    if (cart[i].Count==0 || i_cartToDecrement.Count==1)
                    {
                        cart.Remove(cart[i]);
                    }
                    else
                    {
                        cart[i].Count -= i_cartToDecrement.Count;
                    }
                }
            }

            //Checking local data if null

            //Setting data in the local storage by passing key and data
            await _localStorage.SetItemAsync(StaticDetails.ShoppingCart, cart);
            Onchange.Invoke();

		}


        public async Task IncrementCart(ShoppingCart i_cartToAdd)
        {
            var cart = await _localStorage.GetItemAsync<List<ShoppingCart>>(StaticDetails.ShoppingCart);
            bool itemInCart = false;

            //Checking local data if null
            if(cart==null)
            {
                cart = new List<ShoppingCart>();
            }

            
            foreach(var item in cart)
            {
                if(item.ProductId == i_cartToAdd.ProductId && item.ProductPriceId == i_cartToAdd.ProductPriceId)
                {
                    itemInCart = true;
                    item.Count += i_cartToAdd.Count;
                }
            }

            if(!itemInCart)
            {
                cart.Add(new ShoppingCart()
                {
                    ProductId = i_cartToAdd.ProductId,
                    ProductPriceId = i_cartToAdd.ProductPriceId,
                    Product = i_cartToAdd.Product,
                    ProductPrice = i_cartToAdd.ProductPrice,
                    Count = i_cartToAdd.Count

                });
            }

            //Setting data in the local storage by passing key and data
            await _localStorage.SetItemAsync(StaticDetails.ShoppingCart, cart);
			Onchange.Invoke();
		}

	}
}
