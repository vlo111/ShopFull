using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopFull.Areas.Admin.Controllers
{
    public class Home1Controller : Controller
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ChatUser()
        {

            return View();
        }
    }
}