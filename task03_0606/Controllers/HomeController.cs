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
            //if (Session["memberType"].ToString() == "Business") {
            //    return View("OrderBusiness");
            //}
            return View();
        }

        // GET: Home
        public ActionResult Restaurent()
        {
            return View();
        }




        //public ActionResult OrderBusiness()
        //{
        //    var query = from o in db.OrderLists
        //                where string.IsNullOrEmpty(o.FinshTime) 
        //                select o;
        //    List<OrderList> orders = query.ToList();

        //    //        var query = from o in db.OrderLists
        //    //                    join c in db.OrderDetails on o.OrderId equals c.OrderId into ps
        //    //                    from c in ps.DefaultIfEmpty()
        //    //                    where o.FinshTime == null
        //    //                    select new orderViewModel {
        //    //                       OrderId = c.OrderId,
        //    //                       OrderTime = o.OrderTime,
        //    //                       SeatID = o.SeatID,
        //    //                       CustomerPhone = o.CustomerPhone,
        //    //                       FinshTime = o.FinshTime,
        //    //                       MemberID = c.MemberID,
        //    //                       OrderCount = c.OrderCount, 
        //    //                       Note =c.Note,
        //    //                       ProductID = c.ProductID,
        //    //                       //ProductName =c.p
        //    //                       //UnitPrice = c.
        //    //};
        //    //        List<orderViewModel> orders = query.ToList();


        //    return View(orders);
        //}

        //[HttpPost]
        //public ActionResult OrderBusiness(string orderDetail_time_input, string orderDetail_customerPhone_input, int orderDetail_orderID_input, int orderDetail_seatID_input)
        //{


        //    OrderList orderItem = db.OrderLists.Find(orderDetail_orderID_input);

        //    orderItem.OrderTime = orderDetail_time_input;
        //    orderItem.CustomerPhone = orderDetail_customerPhone_input;
        //    orderItem.SeatID = orderDetail_seatID_input;
        //    orderItem.FinshTime = DateTime.Now.ToString();
        //    //return Content(DateTime.Now.ToString());
        //    db.SaveChanges();
        //    //return Json(true);
        //    return View("OrderBusiness");
        //    //return RedirectToAction("OrderBusiness");
        //}

        //[HttpPost]
        //public ActionResult OrderBusiness_detail(int OrderId)
        //{
        //    Session["Member"] = 2;
        //    int x = Convert.ToInt32(Session["Member"]);
            
        //    var query = from o in db.OrderLists
        //                join c in db.OrderDetails on o.OrderId equals c.OrderId into ps
        //                from c in ps.DefaultIfEmpty()
        //                where string.IsNullOrEmpty(o.FinshTime) && o.OrderId== OrderId && c.MemberID == x
        //                select new orderDetailViewModel
        //                {
        //                    OrderId = c.OrderId,
        //                    OrderTime = o.OrderTime,
        //                    SeatID = o.SeatID,
        //                    CustomerPhone = o.CustomerPhone,
        //                    FinshTime = o.FinshTime,
        //                    MemberID = c.MemberID,
        //                    MemberName = c.Member.MemberName,
        //                    OrderCount = c.OrderCount,
        //                    Note = c.Note,
        //                    ProductID = c.ProductID,
        //                    ProductName =c.Product.ProductName,
        //                    UnitPrice = c.Product.UnitPrice,
        //                };
        //    List<orderDetailViewModel> orders = query.ToList();


        //    return Json(orders, JsonRequestBehavior.AllowGet);

            
        //}


        //public ActionResult OrderCustomer()
        //{
        //    // Session["userID"] = "0912345677"
        //    string userID = Session["userID"].ToString();
        //     var query = from o in db.OrderLists
        //                where o.CustomerPhone == userID
        //                 select o;
        //    List<OrderList> orders = query.ToList();

        //    return View(orders);
        //}
    }
}