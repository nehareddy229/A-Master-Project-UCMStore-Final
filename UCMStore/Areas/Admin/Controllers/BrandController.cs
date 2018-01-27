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
    public class BrandController : Controller
    {
        public ActionResult Index(BrandModel model)
        {
            var list = model.GetList();
            return View(list);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(BrandModel model)
        {
            if (ModelState.IsValid)
            {
                int result = model.Add(model);

                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(model);
        }

        public ActionResult Update(int? id, BrandModel model)
        {
            if (id != null)
                model = model.GetBrandById(id);

            return View(model);
        }

        [HttpPost]
        public ActionResult Update(BrandModel model)
        {
            if (ModelState.IsValid)
            {
                int result = model.Update(model);

                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(model);
        }
    }
}