using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using UCMStore.Models;
using System.IO;

namespace UCMStore.Areas.Admin.Controllers
{
    [Authorize(Users = "admin")]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            
                
                return View();
            
           
        }
    }
}
