using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace UCMStore.Models
{
    public class CouponModel
    {
        DBEcomEntities db;

        public CouponModel()
        {
            db = new UCMStore.DBEcomEntities();
        }

        public int CouponId { get; set; }
       
        [Required]
        public string CouponCode { get; set; }
        

        [Required]
        public int? Discount { get; set; }

        public List<CouponModel> GetList()
        {
            List<CouponModel> couponList = new List<CouponModel>();

            couponList = db.Coupons.Select(m => new CouponModel
            {
                CouponId = m.CouponId,
                CouponCode = m.CouponCode,
                Discount = m.Discount
            }).ToList();

           

            return couponList;
        }

        public int Add(CouponModel model)
        {
            var coupon = new Coupon
            {
                CouponCode = model.CouponCode,
                Discount = model.Discount
            };

            db.Coupons.Add(coupon);
            return db.SaveChanges();
        }
        
        public int Update(CouponModel model)
        {
            var coupon = db.Coupons.FirstOrDefault(m => m.CouponId == model.CouponId);

            if (coupon != null)
            {
                coupon.CouponCode = model.CouponCode;
                coupon.Discount = model.Discount;

                return db.SaveChanges();
            }

            return 0;
        }
    }
}