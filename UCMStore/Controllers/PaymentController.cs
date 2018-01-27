using UCMStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UCMStore.Controllers
{
    [Authorize]
    public class PaymentController : Controller
    {
        public ActionResult Index()
        {
            if (System.Web.HttpContext.Current.Session["cart"] == null)
                return RedirectToAction("index", "home");
            var cart = (CartModel)System.Web.HttpContext.Current.Session["cart"];
            PaymentModel model = new PaymentModel();
            model.Amount = cart.Total;
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(PaymentModel model)
        {
            if (System.Web.HttpContext.Current.Session["UserId"] == null)
                return RedirectToAction("Login", "Account");

            if (ModelState.IsValid)
            {
                var cart = (CartModel)System.Web.HttpContext.Current.Session["cart"];
                cart.Payment = model;

                int result = new OrderModel().Add(cart);

                if (result > 0)
                {
                    return RedirectToAction("Confirmation", new { id = cart.ProductName });

                }

            }

            return View(model);
        }

        public ActionResult Address()
        {
            if (System.Web.HttpContext.Current.Session["cart"] == null)
                return RedirectToAction("index", "home");
            return View();
        }

        [HttpPost]
        public ActionResult Address(ShippingAddressModel model)
        {
            if (System.Web.HttpContext.Current.Session["cart"] == null)
                return RedirectToAction("index", "home");

            if (ModelState.IsValid)
            {
                var cart = (CartModel)System.Web.HttpContext.Current.Session["cart"];
                cart.ShippingAddress = model;
                System.Web.HttpContext.Current.Session["cart"] = cart;

                return RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult Confirmation(string id)
        {
            if (System.Web.HttpContext.Current.Session["cart"] == null)
                return RedirectToAction("Index", "Home");
       
            var cart = (CartModel)System.Web.HttpContext.Current.Session["cart"]; 
            return View(cart);
            
            //if (id != null)
            //    ViewBag.Name = id.Replace("-", " ");
            //System.Web.HttpContext.Current.Session["cart"] = null;
        }

        public ActionResult Failure()
        {
            return View();
        }
        //
    }
}
