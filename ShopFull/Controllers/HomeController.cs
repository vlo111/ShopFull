using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopFull.Models;
using System.Text.RegularExpressions;
using System.Text;
using Stripe;
using System.Threading.Tasks;

namespace ShopFull.Controllers
{
    public class HomeController : Controller
    {
        /*stripe methods*/
        #region Visa Card

        #endregion
        /*stripe methods*/
        ShopDBEntities db = new ShopDBEntities();
        public ActionResult Index()
        {
            var item = db.Products.ToList();
            return View(item);
        }



        #region Login
        public ActionResult Login()
        {
            Models.Account a = new Models.Account();
            return View(a);
        }
        [HttpPost]
        public ActionResult Login(Models.Account acount)
        {
            ViewBag.Message = Request.Form["Domain"];

            bool flag1 = true;
            if (ViewBag.Message != null)
            {
                int count = 0;
                foreach (var item in db.Accounts)
                {
                    if (ViewBag.Message.Contains(item.Name))
                    {
                        count++;
                        if (ViewBag.Message.Contains(item.Password))
                        {
                            Session.Add("UserID", item.Id);
                            return View("Index", db.Products);
                        }
                    }
                }
                if (count == 0)
                {
                    ViewBag.Valid2 = "User Name does not exist";
                }
                else if (count == 1)
                    ViewBag.Valid2 = "Password does not exist";
                flag1 = false;
            }
            if (flag1)
            {
                if (!string.IsNullOrEmpty(acount.Name) && acount.Name != "Name")
                {
                    if (!string.IsNullOrEmpty(acount.Password) && acount.Password != "Password")
                    {
                        if (!string.IsNullOrEmpty(acount.Email) && acount.Email != "E-Mail")
                        {
                            db.Accounts.Add(acount);
                            db.SaveChanges();
                        }
                        else ViewBag.Valid1 = "Email is not correct";
                    }
                    else ViewBag.Valid1 = "Password is not correct";
                }
                else ViewBag.Valid1 = "User Name is not correct";
            }
            return View();
        }
        #endregion
        public ActionResult Products()
        {
            var Items = db.Products;
            return View(Items);
        }
        public ActionResult Preview(int id)
        {
            var queryProduct = db.Products.Include(o => o.Category).FirstOrDefault(x => x.Id == id);
            TempData["Categories"] = db.Categories;
            return View(queryProduct);
        }
        public ActionResult TopBrands()
        {
            var Items = db.Products;
            return View(Items);
        }
        public ActionResult Cart()
        {
            return View(db.Carts);
        }
        [HttpPost]
        public ActionResult CartDeleteOnClickCheckOut()
        {
            try
            {
                var request = Request.Form["Image"];
                int id = (int)Session["UserID"];


                var aaa = (from a in db.Carts
                           where a.accauntId == id
                           select a).ToList();
                if (aaa.Count() > 0)
                {
                    db.Carts.RemoveRange(aaa);
                    db.SaveChanges();
                }

                return Json(new { success = true });

            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.ToString() });
            }

        }
        public void CreateStripeCustomer(string token)
        {
            var myCustomer = new StripeCustomerCreateOptions();

            // set these properties if it makes you happy
            myCustomer.Email = "YourSoToBeCustomers@email.com";
            myCustomer.Description = "Customer Name here";
            myCustomer.SourceToken = token;

            var customerService = new StripeCustomerService();
            StripeCustomer stripeCustomer = customerService.Create(myCustomer);
            var StripeCustomerId = stripeCustomer.Id;

        }

        [HttpPost]
        public ActionResult Cart(int id, int number, string userLogin)
        {

            if (Session["UserID"] != null)
            {
                var product = new Product();
                foreach (var item in db.Products)
                {
                    if (item.Id == id)
                    {
                        product = item;
                    }
                }
                var cart = new Cart();
                cart.Image = product.Image;
                cart.Price = product.Price;
                cart.Quantity = number;
                cart.ProductName = product.TopBrands;
                cart.TotalPrice = number * product.Price;
                cart.ProductId = product.Id;
                cart.accauntId = (int)Session["UserID"];
                db.Carts.Add(cart);
                db.SaveChanges();
            }
            else
            {
                Response.Redirect("~/Home/Login");
            }

            return View(db.Carts);
        }

        [HttpPost]
        public ActionResult CartUpdate(int id, int quantity)
        {
            var update = db.Carts.FirstOrDefault(x => x.Id == id);
            update.Quantity = quantity;
            update.TotalPrice = quantity * update.Price;

            db.SaveChanges();
            return View("Cart", db.Carts);
        }

        public ActionResult CartDelete(int id)
        {
            var update = db.Carts.FirstOrDefault(x => x.Id == id);
            db.Carts.Remove(update);

            db.SaveChanges();
            return View("Cart", db.Carts);
        }
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult ContactUser()
        {
            var info = Request.Form["label"];
            string[] str = info.Split(',');
            var contact = new Contact()
            {
                Name = str[0],
                E_Mail = str[1],
                Tel = str[2],
                Subject = str[3]
            };
            db.Contacts.Add(contact);
            db.SaveChanges();
            return View("Contact");
        }
        public ActionResult ProductByCat(int id)
        {
            var CatName = from p in db.Categories
                          where p.Id == id
                          select p;
            foreach (var item in CatName)
            {
                ViewBag.CategoryName = item.Name;
            }
            var queryProduct = from p in db.Products
                               where p.CategoryId == id
                               select p;
            return View(queryProduct);
        }

        public ActionResult PaymentWithPaypal()
        {
            var cart = db.Carts.FirstOrDefault(x => x.accauntId == (int)Session["UserID"]);
            return View(cart);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}