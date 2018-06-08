using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using task03_0606.Models;

namespace task03_0606.Controllers
{
    public class HomeController : Controller
    {
        FoodCourtEntities db = new FoodCourtEntities();

        public ActionResult Index()
        {
            if (Session["memberType"].ToString() == "Business") {
                return View("Order");
            }
            return View();
        }

        // GET: Home
        public ActionResult Restaurent()
        {
            return View();
        }

        public ActionResult Order()
        {
            var query = from o in db.OrderLists
                        select o;
            List<OrderList> orders = query.ToList();
            
            return View(orders);
        }
    }
}