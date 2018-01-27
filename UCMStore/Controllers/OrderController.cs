using UCMStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UCMStore.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        public ActionResult Index(OrderModel model)
        {
            var list = model.GetList(User.Identity.Name);
            return View(list);
        }

        public ActionResult ViewOrder(int? id, OrderModel model)
        {
            model = model.GetList(User.Identity.Name).FirstOrDefault(m => m.OrderId == id);
            return View(model);
        }

        [HttpPost]
        public ActionResult ViewOrder(int? id)
        {
            new OrderModel().UpdateOrder(id, 2);
            return RedirectToAction("Index");
        }
    }
}