using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopFull.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        Models.ShopDBEntities db = new Models.ShopDBEntities();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string Login, string Password)
        {
            foreach (var item in db.AdminPanels)
            {
                if (Login == item.AdminName)
                {
                    if (Password == item.Password)
                    {
                        Session.Add("AdminLogin", item.AdminName);
                        Response.Redirect(@"/Admin/Products/Index");
                    }
                    else
                    {
                        ViewBag.ValidPassword = "Password does not exist";
                    }
                }
                else
                {
                    ViewBag.ValidLogin = "Admin Name does not exist";
                }
            }
            
            return View();
        }
    }
}