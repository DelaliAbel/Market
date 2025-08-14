using MarketWeb_Models;
using System.ComponentModel.DataAnnotations;

namespace MarketWeb__Client.ViewModel
{
    public class DetailsVM
    {
        [Range(0,int.MaxValue,ErrorMessage ="Please enter a value greater than O")]
        public int Count { get; set; }
        [Required]
        public int SelectedProductPriceId { get; set; } 
        public ProductPriceDTO ProductPrice { get; set; }

        public DetailsVM()
        {
            ProductPrice = new();
            Count = 1;
        }

    }
}
