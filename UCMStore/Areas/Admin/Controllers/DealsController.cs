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
    public class DealsController : Controller
    {
        public ActionResult Index(DealModel model)
        {
            var list = model.GetList();
            return View(list);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(DealModel model)
        {
            if (ModelState.IsValid)
            {
                string fileName = "";
                if (model.Image != null)
                {
                    fileName = DateTime.Now.ToFileTime() + Path.GetExtension(model.Image.FileName);
                    model.DealImage = "~/UploadFiles/Deals/" + fileName;
                }

                int result = model.Add(model);

                if (result > 0)
                {
                    if (model.Image != null)
                        model.Image.SaveAs(Server.MapPath(model.DealImage));

                    return RedirectToAction("Index");
                }
            }

            return View(model);
        }

        public ActionResult Update(int? id, DealModel model)
        {
            if (id != null)
                model = model.GetDealById(id);

            return View(model);
        }

        [HttpPost]
        public ActionResult Update(DealModel model)
        {
            if (ModelState.IsValid)
            {
                string fileName = "";
                if (model.Image != null)
                {
                    fileName = DateTime.Now.ToFileTime() + Path.GetExtension(model.Image.FileName);
                    model.DealImage = "~/UploadFiles/Deals/" + fileName;
                }
                int result = model.Update(model);

                if (result > 0)
                {
                    if (model.Image != null)
                        model.Image.SaveAs(Server.MapPath(model.DealImage));

                    return RedirectToAction("Index");
                }
            }

            return View(model);
        }
    }
}