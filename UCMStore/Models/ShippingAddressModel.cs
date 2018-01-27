using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace UCMStore.Models
{
    public class ShippingAddressModel
    {
        public int ShippingAddressId { get; set; }

        [Required(ErrorMessage = "You must Mention your Name for Shipping")]
        [StringLength(50)]
        [Display(Name = "Name")]
        
        public string Name { get; set; }

        [Required]
        [Display(Name = "Mobile No")]
        [MaxLength(10)]
        [MinLength(10)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Mobile No must be numeric")]
        public string Mobile { get; set; }
        [Required]
        [Display(Name = "Address")]
        [StringLength(100)]
        public string Address { get; set; }
        [Required]
        [Display(Name="City")]
        [StringLength(25)]
        public string City { get; set; }

        [Required(ErrorMessage = "Zip is Required")]
        [RegularExpression("^[64]{2}\\d{3}", ErrorMessage = "Sorry!Delivery is only available for Warrensburg and Lee Summit locations ")]
        public int ZipCode { get; set; }
       
    }
}