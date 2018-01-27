using UCMStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UCMStore.Areas.Admin.Controllers
{
    public class UsersController : Controller
    {
        public ActionResult Index(UserModel model)
        {
            var list = model.GetList();
            return View(list);
        }

        public ActionResult Edit(int? id, UserModel model)
        {
            if (id != null)
                model = model.GetUserById(id);

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(UserModel model)
        {
            if (ModelState.IsValid)
            {
                int result = model.Update(model);

                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(model); ;
        }
    }
}