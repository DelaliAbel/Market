using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MarketWeb_Models
{
    public class OrderHeaderDTO
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        [Display(Name = "Order Total")]
        public double OrderTotal { get; set; }

        [Required]
        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }

        [Required]
        [Display(Name = "Shipping Date")]
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

        [Display(Name = "Email")]
        [Required]
        public string Email { get; set; }


    }
}
