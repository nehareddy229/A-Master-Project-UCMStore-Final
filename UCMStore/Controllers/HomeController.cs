using UCMStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UCMStore.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(ProductModel model)
        {
            var list = model.GetList().Where(m => m.Active == true);
            model.ProductList = list.Where(m => m.IsAccessories == false);
            model.AccessoriesList = list.Where(m => m.IsAccessories == true);
            return View(model);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "UCM CONTACT";

            return View();
        }




        public ActionResult About()
        {
            return View();
        }

        public JsonResult GetDeals(DealModel model)
        {
            var list = model.GetList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}
