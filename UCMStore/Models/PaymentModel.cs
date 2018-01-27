using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace UCMStore.Models
{
    public class PaymentModel
    {
        public int PaymentId { get; set; }

        [Required]
        [RegularExpression("^4[0-9]{12}(?:[0-9]{3})?$", ErrorMessage = "We only Accept Visa Cards.")]
        public string CardNo { get; set; }

        [Required]
        public int? ExpiryMonth { get; set; }

        [Required]
        public int? ExpiryYear { get; set; }

        [Required]
        [RegularExpression("^[0-9]{3}$", ErrorMessage = "Please enter your 3-Digit CVV Number on the back of your card")]
        public int? CVV { get; set; }
        public string PaidOn { get; set; }
        public string PaymentStatus { get; set; }
        public decimal? Amount { get; set; }
    }
}