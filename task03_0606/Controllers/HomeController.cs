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

        public ActionResult OrderBusiness()
        {
            var query = from o in db.OrderLists
                        where o.FinshTime == null
                        select o;
            List<OrderList> orders = query.ToList();
            
            return View(orders);
        }
        
        [HttpPost]
        public ActionResult OrderBusiness(string orderDetail_time_input, string orderDetail_customerPhone_input, int orderDetail_orderID_input, int orderDetail_seatID_input)
        {


            OrderList orderItem = db.OrderLists.Find(orderDetail_orderID_input);

            orderItem.OrderTime = orderDetail_time_input;
            orderItem.CustomerPhone = orderDetail_customerPhone_input;
            orderItem.SeatID = orderDetail_seatID_input;
            orderItem.FinshTime = DateTime.Now.ToString();
            //return Content(DateTime.Now.ToString());
            db.SaveChanges();
            return RedirectToAction("OrderBusiness");
        }


        public ActionResult OrderCustomer()
        {
           // Session["userID"] = "0912345677"
            var query = from o in db.OrderLists
                        //where o.CustomerPhone == Session["userID"].ToString()
                        select o;
            List<OrderList> orders = query.ToList();

            return View(orders);
        }
    }
}