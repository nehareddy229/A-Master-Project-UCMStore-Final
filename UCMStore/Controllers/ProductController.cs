using UCMStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UCMStore.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult Index(ProductModel model)
        {
            var list = model.GetList().Where(m => m.IsAccessories == false);
            return View(list);
        }
        
        public ActionResult Accessories(ProductModel model)
        {
            var list = model.GetList().Where(m => m.IsAccessories == true);
            return View(list);
        }

        public ActionResult Detail(string id, ProductModel model)
        {
            model = model.GetList().FirstOrDefault(m => m.Name.ToLower() == id.ToString().Replace("-", " ").ToLower());
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Detail(CartModel model)
        {
            System.Web.HttpContext.Current.Session["cart"] = model;
            return RedirectToAction("Index", "Cart");
        }
    }
}