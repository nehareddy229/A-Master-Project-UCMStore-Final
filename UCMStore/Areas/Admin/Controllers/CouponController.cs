using UCMStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UCMStore.Areas.Admin.Controllers
{
    [Authorize(Users = "admin")]
    public class CouponController : Controller
    {
        public ActionResult Index(CouponModel model)
        {
            var list = model.GetList();
            return View(list);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(CouponModel model)
        {
            if (ModelState.IsValid)
            {
                int result = model.Add(model);
                if (result > 0)
                    return RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult Edit(int? id, CouponModel model)
        {
            if (id != null)
                model = model.GetList().FirstOrDefault(m => m.CouponId == id);

            return View(model);
        }

        [HttpPost]
        //edit Function
        public ActionResult Edit(CouponModel model)
        {
            if (ModelState.IsValid)
            {
                int result = model.Update(model);

                if (result > 0)
                    return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}