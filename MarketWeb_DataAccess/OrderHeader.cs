using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketWeb_DataAccess
{
    public  class OrderHeader
    {
        [Key]
        public int Id { get; set; }

        [Required] 
        public string UserId { get; set; }
        //Add navigation property : #TODO

        [Required] 
        [Display(Name = "Order Total")] 
        public double OrderTotal { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        [Display(Name = "Order Total")]
        public DateTime ShippingDate { get; set; }

        //Stripe payment
        public string? SessionId { get; set; }
        public string? PayementIntedtId { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Street Adress")]
        public string StreetAdress { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }

        [Display(Name = "Postal Code")]
        [Required]
        public string Email { get; set; }

    }
}
