using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eSalex.Areas.Emp.Controllers
{
    public class Default1Controller : Controller
    {
        // GET: Emp/Default1
        public ActionResult Index()
        {
            ViewBag.Desc = "Hello Emp";
            return View();
        }
    }
}