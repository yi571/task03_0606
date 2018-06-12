using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using task03_0606.Models;

namespace task03_0606.Controllers
{
    public class OrderController : Controller
    {
        FoodCourtEntities db = new FoodCourtEntities();
        // GET: Order
        public ActionResult Order_deteail_bussiness()
        {
            int sessionMember = Convert.ToInt32(Session["Member"]);
            var query = from o in db.OrderLists
                        join c in db.OrderDetails on o.OrderId equals c.OrderId into ps
                        from c in ps.DefaultIfEmpty()
                        where string.IsNullOrEmpty(c.FinshTime) && c.MemberID == sessionMember
                        select new orderDetailViewModel
                        {
                            OrderId = c.OrderId,
                            OrderTime = o.OrderTime,
                            SeatID = o.SeatID,
                            CustomerPhone = o.CustomerPhone,
                            FinshTime = c.FinshTime,
                            MemberID = c.MemberID,
                            OrderCount = c.OrderCount,
                            Note = c.Note,
                            ProductID = c.ProductID,
                            ProductName = c.Product.ProductName,
                            UnitPrice = c.Product.UnitPrice,
                        };
            List<orderDetailViewModel> ordersDetailList = query.ToList();
            

            return View(ordersDetailList);
        }


        public ActionResult Order_list_bussiness() {

            var query = from o in db.OrderLists
                        //where string.IsNullOrEmpty(c.FinshTime)
                        select o;
            List<OrderList> orders = query.ToList();

            return View(orders);
        }
        [HttpPost]
        public ActionResult Order_list_bussiness(int OrderId)
        {
            int x = Convert.ToInt32(Session["Member"]);

            var query = from o in db.OrderLists
                        join c in db.OrderDetails on o.OrderId equals c.OrderId into ps
                        from c in ps.DefaultIfEmpty()
                        where string.IsNullOrEmpty(c.FinshTime) && o.OrderId == OrderId && c.MemberID == x
                        select new orderDetailViewModel
                        {
                            OrderId = c.OrderId,
                            OrderTime = o.OrderTime,
                            SeatID = o.SeatID,
                            CustomerPhone = o.CustomerPhone,
                            FinshTime = c.FinshTime,
                            MemberID = c.MemberID,
                            MemberName = c.Member.MemberName,
                            OrderCount = c.OrderCount,
                            Note = c.Note,
                            ProductID = c.ProductID,
                            ProductName = c.Product.ProductName,
                            UnitPrice = c.Product.UnitPrice,
                        };
            List<orderDetailViewModel> orders = query.ToList();
            
            return Json(orders, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Order_list_chickToDatabase_bussiness(int productId_ok, int orderId_ok)
        {
            OrderDetail orderItem = db.OrderDetails.Find(productId_ok & orderId_ok);
            return Content(orderItem.ToString());

            //orderItem.FinshTime = DateTime.Now.ToString();
           
            //db.SaveChanges();
            //return Json(true);

            //return View("OrderBusiness"); ??
            //return RedirectToAction("OrderBusiness"); ??
        }



        public ActionResult Order_list_costomer() {

            string sessionUserID = Session["userID"].ToString();
            var query = from o in db.OrderLists
                        //where string.IsNullOrEmpty(o.FinshTime) && o.CustomerPhone == sessionUserID
                        where o.CustomerPhone == sessionUserID
                        select o;
            List<OrderList> orders = query.ToList();
            return View(orders);
        }
        [HttpPost]
        public ActionResult Order_list_costomer(int OrderId)
        {
            var query = from o in db.OrderLists
                        join c in db.OrderDetails on o.OrderId equals c.OrderId into ps
                        from c in ps.DefaultIfEmpty()
                        //where string.IsNullOrEmpty(o.FinshTime) && o.OrderId == OrderId
                        where o.OrderId == OrderId
                        select new orderDetailViewModel
                        {
                            OrderId = c.OrderId,
                            OrderTime = o.OrderTime,
                            SeatID = o.SeatID,
                            CustomerPhone = o.CustomerPhone,
                            FinshTime = c.FinshTime,
                            MemberID = c.MemberID,
                            MemberName = c.Member.MemberName,
                            OrderCount = c.OrderCount,
                            Note = c.Note,
                            ProductID = c.ProductID,
                            ProductName = c.Product.ProductName,
                            UnitPrice = c.Product.UnitPrice,
                        };
            List<orderDetailViewModel> orders = query.ToList();


            return Json(orders, JsonRequestBehavior.AllowGet);
        }

    }
}