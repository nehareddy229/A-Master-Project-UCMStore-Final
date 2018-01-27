using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UCMStore.Models
{
    public class CartModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public int Quantity { get; set; }
        public decimal? Price { get; set; }
        public decimal? Total { get; set; }
        public string UserName { get; set; }
        public bool CouponApplied { get; set; }
        public int CouponId { get; set; }
        public string CouponCode { get; set; }
        public PaymentModel Payment { get; set; }
        public ShippingAddressModel ShippingAddress { get; set; }
    }
}