using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketWeb_Common
{
    public static class StaticDetails
    {
        public static string ShoppingCart { get; set; } = "ShoppingCart";

        public static string Status_Pending { get; set; } = "Pending";
        public static string Status_Shipped { get; set; } = "Shipped";
        public static string Status_Confirmed { get; set; } = "Confirmed";
        public static string Status_Refunded { get; set; } = "Refunder";

        public static string Role_Admin { get; set; } = "Admin";
        public static string Role_Customer { get; set; } = "Customer";

        public static string Local_Token { get; set; } = "Jwt Token";

        public static string Local_UserDtails { get; set; } = "User Details";
    }
}
