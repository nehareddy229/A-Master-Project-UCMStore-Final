using UCMStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;

namespace UCMStore.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        public ActionResult Index(CartModel model)
        {
            if (System.Web.HttpContext.Current.Session["cart"] == null)
                return RedirectToAction("Index", "Home");

            model = (CartModel)System.Web.HttpContext.Current.Session["cart"];
            var product = new ProductModel().GetProductById(model.ProductId);

            model.ProductName = product.Name;
            model.ProductImage = product.ProductImage;
            model.Price = product.Price;
            model.Total = product.Price * model.Quantity;
            model.UserName = User.Identity.Name;
            System.Web.HttpContext.Current.Session["cart"] = model;
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(string CouponCode)
        {
            CouponCode = CouponCode.Trim().Replace(" ", "");
            CouponModel model = new CouponModel();
            var cart = (CartModel)System.Web.HttpContext.Current.Session["cart"];
            
            var coupon = model.GetList().FirstOrDefault(m => m.CouponCode == CouponCode);
            

            if (coupon != null)
                {
                    cart.CouponApplied = true;
                    cart.CouponId = coupon.CouponId;
                    cart.CouponCode = coupon.CouponCode;
                     

                cart.Price = cart.Price - cart.Price * 20 / 100;
                    cart.Total = cart.Price * cart.Quantity;
                    System.Web.HttpContext.Current.Session["cart"] = cart;

                    ViewBag.Message = "Coupon is applied Successfully";
                }
                else
                {
                    ViewBag.Message = "Coupon not found or Expired!";
                }

                return View(cart);
            }
        }
    }
