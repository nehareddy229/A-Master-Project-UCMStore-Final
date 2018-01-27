using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace UCMStore.Models
{
    public class OrderModel
    {
        DBEcomEntities db;

        public OrderModel()
        {
            db = new DBEcomEntities();
        }

        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }
        public decimal Total { get; set; }
        public int? OrderStatusId { get; set; }
        public string OrderStatus { get; set; }
        public DateTime? OrderOn { get; set; }
        public DateTime? DeliveredOn { get; set; }
        public string UserName { get; set; }
        public bool? CouponApplied { get; set; }
        public string CouponCode { get; set; }

        public IEnumerable<OrderModel> GetList(string UserName = null)
        {
            List<OrderModel> orderList = new List<OrderModel>();

            orderList = db.Orders.Select(m => new OrderModel
            {
                OrderId = m.OrderId,
                ProductName = m.Product.Name,
                Price = m.Amount,
                Quantity = m.Quantity,
                OrderStatusId = m.OrderStatusId,
                OrderOn = m.OrderOn,
                DeliveredOn = m.DeliveredOn,
                OrderStatus = m.OrderStatu.Status,
                UserName = m.User.UserName,
                CouponApplied = m.CouponApplied,
                CouponCode = m.Coupon.CouponCode
            }).ToList();

            if (!string.IsNullOrEmpty(UserName))
                orderList = orderList.Where(m => m.UserName == UserName).ToList();

            return orderList;
        }

        public int Add(CartModel model)
        {
            var order = new Order
            {
                ProductId = model.ProductId,
                Amount = model.Price,
                Quantity = model.Quantity,
                OrderStatusId = 1,
                OrderOn = DateTime.Now,
                UserId = Convert.ToInt32(System.Web.HttpContext.Current.Session["UserId"])
            };

            if (model.CouponApplied)
            {
                order.CouponApplied = model.CouponApplied;
                order.CouponId = model.CouponId;
            }

            var payment = new Payment
            {
                CardNo = model.Payment.CardNo,
                ExpiryMonth = model.Payment.ExpiryMonth,
                ExpiryYear = model.Payment.ExpiryYear,
                CVV = model.Payment.CVV,
                PaidOn = DateTime.Now,
                PaymentStatus = "Success"
            };

            order.Payments.Add(payment);

            var address = new ShippingAddress
            {
                Name = model.ShippingAddress.Name,
                Mobile = model.ShippingAddress.Mobile,
                Address = model.ShippingAddress.Address,
                City = model.ShippingAddress.City,
                ZipCode = model.ShippingAddress.ZipCode
            };

            order.ShippingAddresses.Add(address);

            db.Orders.Add(order);
            return db.SaveChanges();
        }

        public int UpdateOrder(int? OrderId, int Status)
        {
            var order = db.Orders.FirstOrDefault(m => m.OrderId == OrderId);
            order.OrderStatusId = Status;
            return db.SaveChanges();
        }
    }
}