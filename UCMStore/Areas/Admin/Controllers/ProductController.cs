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
    public class ProductController : Controller
    {
        public ActionResult Index(ProductModel model)
        {
            var list = model.GetList();
            return View(list);
        }

        public ActionResult Add(int? id, ProductModel model)
        {
            model.BrandList = new BrandModel().GetList();
            return View(model);
        }

        [HttpPost]
        public ActionResult Add(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                string fileName = "";
                if (model.Image != null)
                {
                    fileName = DateTime.Now.ToFileTime() + Path.GetExtension(model.Image.FileName);
                    model.ProductImage = "~/UploadFiles/Products/" + fileName;
                }

                int result = model.Add(model);

                if (result > 0)
                {
                    if (model.Image != null)
                        model.Image.SaveAs(Server.MapPath(model.ProductImage));

                    return RedirectToAction("Index");
                }
            }

            model.BrandList = new BrandModel().GetList();
            return View(model);
        }

        public ActionResult Update(int? id, ProductModel model)
        {
            if (id != null)
                model = model.GetList().FirstOrDefault(m => m.ProductId == id);

            model.BrandList = new BrandModel().GetList();
            return View(model);
        }

        [HttpPost]
        public ActionResult Update(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                string fileName = "";
                if (model.Image != null)
                {
                    fileName = DateTime.Now.ToFileTime() + Path.GetExtension(model.Image.FileName);
                    model.ProductImage = "~/UploadFiles/Products/" + fileName;
                }

                int result = model.Update(model);

                if (result > 0)
                {
                    if (model.Image != null)
                        model.Image.SaveAs(Server.MapPath(model.ProductImage));

                    return RedirectToAction("Index");
                }
            }

            model.BrandList = new BrandModel().GetList();
            return View(model);
        }
    }
}