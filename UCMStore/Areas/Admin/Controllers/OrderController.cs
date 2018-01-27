using UCMStore.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UCMStore.Areas.Admin.Controllers
{
    [Authorize(Users = "admin")]
    public class OrderController : Controller
    {
      
        public ActionResult Index(OrderModel model)
        {
            if (System.Web.HttpContext.Current.Session["UserId"] == null)
                return RedirectToAction("Login", "Account");

            var list = model.GetList();
            return View(list);
        }

        public ActionResult View(int? id, OrderModel model)
        {
            if (System.Web.HttpContext.Current.Session["UserId"] == null)
                return RedirectToAction("Login", "Account");

            model = model.GetList().FirstOrDefault(m => m.OrderId == id);
            return View(model);
        }


        [HttpPost]
        public ActionResult View(int? id, string btnSubmit)
        {
            if (System.Web.HttpContext.Current.Session["UserId"] == null)
                return RedirectToAction("Login", "Account");

            int status = 3;
            if (btnSubmit == "Cancel")
                status = 2;

            new OrderModel().UpdateOrder(id, status);
            return RedirectToAction("Index");
        }
       


    }
}