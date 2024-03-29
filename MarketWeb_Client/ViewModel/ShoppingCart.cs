﻿using MarketWeb_Models;

namespace MarketWeb_Client.ViewModel
{
    public class ShoppingCart
    {
        public int ProductId { get; set; }
        public ProductDTO Product { get; set; }
        public int ProductPriceId { get; set; }
		public ProductPriceDTO ProductPrice { get; set; }
		public int Count { get; set; }
    }
}
