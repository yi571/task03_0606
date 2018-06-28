using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using task03_0606.Models;
using System.Data.SqlClient;
using task03_0606.Hubs;
using Microsoft.AspNet.SignalR;

namespace task03_0606.Controllers
{
    public class OrderController : Controller
    {

        //客戶購物車清單
        public ActionResult Index()
        {
            if (String.IsNullOrEmpty((string)Session["logState"])) {  //如未登入，則重導到登入頁面
                Session["lastPage"] = "/Order/Index";      //儲存最後頁面
                return RedirectToAction("Login", "Member");  //重導到登入頁面
            }
            //將存在session中的 電話取出
            string phonString = Session["userPhone"].ToString();
            //將存在session中的 桌號取出
            int tableNum = 0;
            if (Session["TableId"] != null) {
                tableNum = Convert.ToInt32(Session["TableId"]);
            }
            
            
            using (FoodCourtDBEntities db = new FoodCourtDBEntities()) {
                //查詢用戶姓名
                var user = (from o in db.UserInfoes
                                where o.phoneNum == phonString
                                select o).FirstOrDefault();

                ViewBag.userFirstName = user.firstName;
                ViewBag.userLastName = user.lastName;
                //查詢座位
                var seat = (from o in db.TableListes
                            where o.tableId == tableNum
                            select o).FirstOrDefault();
                //ViewBag.seatLoction = seat.tableLocation;
                ViewBag.seatLoction = Session["TableId"].ToString();
            }
                return View();
        }

        //客戶購物車清單 ==> 訂單
        public ActionResult OrderPayment()
        {
            if (String.IsNullOrEmpty((string)Session["logState"])) {  //如未登入，則重導到登入頁面
                Session["lastPage"] = "/Order/OrderPayment";      //儲存最後頁面
                return RedirectToAction("Login", "Member");  //重導到登入頁面
            }
            //將存在session中的 電話取出
            string phonString = Session["userPhone"].ToString();
            //將存在session中的 桌號取出
            int tableNum = Convert.ToInt32(Session["TableId"]);
            //int tableNum = 1;

            //現在時間字串
            string dateString = DateTime.Now.ToString();

            if (this.ModelState.IsValid) {
                //取得購物車
            var currentCart = Models.Cart.Operation.GetCurrentCart();

            using (FoodCourtDBEntities db = new FoodCourtDBEntities()) {
               
                //新增訂單，存入database
                Models.Order newOrder = new Order()
                {
                    orderDate = dateString,
                    phoneNum = phonString,
                    tableId = tableNum,
                    orderState = 1, //訂單狀態：1-未結單 2-結單
                };
                db.Orders.Add(newOrder);
                db.SaveChanges();

               //傳入訂單編號，取得訂單明細，存入database 
               var newOrderDetail = currentCart.ToOrderDetail(newOrder.orderId);
               db.OrderDetials.AddRange(newOrderDetail);
               db.SaveChanges();      
                

                     
            } //return Content("訂購成功");
                var context = GlobalHost.ConnectionManager.GetHubContext<StoreHub>();

                context.Clients.All.addNewMessageToPage("新訂單");
                return RedirectToAction("ClearFromCart", "Cart");

            }
            return View();


        }

        //客戶訂單
        public ActionResult Order_list_costomer()
        {
            if (String.IsNullOrEmpty((string)Session["logState"])) {  //如未登入，則重導到登入頁面
                Session["lastPage"] = "/Order/Order_list_costomer";      //儲存最後頁面
                return RedirectToAction("Login", "Member");  //重導到登入頁面
            }
            string phoneString = Session["userPhone"].ToString();

            using (Models.FoodCourtDBEntities db = new FoodCourtDBEntities())
            {
                var orders = (from o in db.Orders
                              where o.phoneNum == phoneString
                              orderby o.orderId descending
                              select o).ToList();
                return View(orders);
            }
        }
        
        //客戶訂單 ==>訂單明細
        public ActionResult Order_detail_costomer()
        {
            if (String.IsNullOrEmpty((string)Session["logState"])) {  //如未登入，則重導到登入頁面
                Session["lastPage"] = "/Order/Order_detail_costomer";      //儲存最後頁面
                return RedirectToAction("Login", "Member");  //重導到登入頁面
            }
            int itemOrderId = Convert.ToInt32(Request["itemOrderId"]);
            
            ViewBag.itemOrderId = itemOrderId;
            ViewBag.itemOrderDate =  Request["orderDate"].ToString();
            //將存在session中的 電話取出
            string phonString = Session["userPhone"].ToString();
            //將存在session中的 桌號取出
            int tableNum = Convert.ToInt32(Session["seat"]);
            ViewBag.tableNum = tableNum;
            using (Models.FoodCourtDBEntities db = new FoodCourtDBEntities())
            {
                //查詢用戶姓名
                var user = (from o in db.UserInfoes
                            where o.phoneNum == phonString
                            select o).FirstOrDefault();

                ViewBag.userFirstName = user.firstName;
                ViewBag.userLastName = user.lastName;
                

                var query = from o in db.Orders
                            join c in db.OrderDetials on o.orderId equals c.orderId into ps
                            from c in ps.DefaultIfEmpty()
                            where o.orderId == itemOrderId
                            select new OrderDetailViewModel
                            {
                                orderId = o.orderId,
                                orderTime = o.orderDate,
                                seatID = o.tableId,
                                customerPhone = o.phoneNum,
                                storeID = c.Product.Store.storeId,
                                storeName = c.Product.Store.storeName,
                                productID = c.productID,
                                productName = c.Product.productName,
                                productCount = c.productCount,
                                unitPrice = c.Product.productPrice,
                                customerNote = c.customerNote,
                                productionStatus = c.productionStatus,
                                productPicture = c.Product.productPicture,
                            };
               
                List<OrderDetailViewModel> orders = query.ToList();

                
                return View(orders);

            }
        }


        //廠商未出餐點明細清單
        public ActionResult Order_deteail_bussiness()
        {
            if (String.IsNullOrEmpty((string)Session["logState"])) {  //如未登入，則重導到登入頁面
                Session["lastPage"] = "/Order/Order_deteail_bussiness";      //儲存最後頁面
                return RedirectToAction("Login", "Member");  //重導到登入頁面
            }
            string stroeId = Session["storeId"].ToString();

            //依訂單狀況 & 廠商編號，篩選廠商訂單明細
            using (Models.FoodCourtDBEntities db = new FoodCourtDBEntities())
            {
                var query = from o in db.Orders
                                 join c in db.OrderDetials on o.orderId equals c.orderId into ps
                                 from c in ps.DefaultIfEmpty()
                                 where c.productionStatus == 1  && c.Product.Store.storeId == stroeId && o.orderState == 1
                                 select new OrderDetailViewModel
                                 {
                                     orderId = o.orderId,
                                     orderTime = o.orderDate,
                                     seatID = o.tableId,
                                     customerPhone = o.phoneNum,
                                     storeID = c.Product.Store.storeId,
                                     storeName = c.Product.Store.storeName,
                                     productID = c.productID,
                                     productName = c.Product.productName,
                                     productCount = c.productCount,
                                     unitPrice = c.Product.productPrice,
                                     customerNote = c.customerNote,
                                     productionStatus = c.productionStatus,
                                 };

                List<OrderDetailViewModel>  orderList  = query.ToList();

                return View(orderList);
            }
    }
        //廠商未出餐點明細清單 ==> 準備完成
        public ActionResult Order_list_chickToDatabase_bussiness(int productId_ok, int orderId_ok)
        {
            int test = productId_ok;

            using (Models.FoodCourtDBEntities db = new FoodCourtDBEntities())
            {
                var result = (from o in db.OrderDetials
                              where o.productID == productId_ok && o.orderId == orderId_ok
                              orderby o.orderId
                              select o).FirstOrDefault();

                if (result.productionStatus == 1)
                {
                    result.productionStatus = 2;
                }
                else { result.productionStatus = 1; }
                    
             
                db.SaveChanges();
                return Json(true);
            }
        }

        //廠商未出餐點訂單列表
        public ActionResult Order_list_bussiness()
        {
            if (String.IsNullOrEmpty((string)Session["logState"])) {  //如未登入，則重導到登入頁面
                Session["lastPage"] = "/Order/Order_list_bussiness";      //儲存最後頁面
                return RedirectToAction("Login", "Member");  //重導到登入頁面
            }
            string stroeId = Session["storeId"].ToString();

            //依結單時間=null &　廠商編號，篩選廠商訂單明細
            using (Models.FoodCourtDBEntities db = new FoodCourtDBEntities())
            {
                var query = from o in db.OrderDetials
                            join c in db.Orders
                            on new { OrderId = o.orderId } equals
                               new { OrderId = c.orderId } into temp
                            from ds in temp.DefaultIfEmpty()
                            where o.Product.storeId == stroeId && o.Order.orderState == 1
                            orderby o.orderId
                            select ds;

                List<Order> orderDetailList = query.ToList();


                var queryByID = from o in orderDetailList
                                group o by new { o.orderId, o.phoneNum, o.tableId, o.orderDate } into g
                                select new Order
                                {
                                    orderId = g.Key.orderId,
                                    phoneNum = g.Key.phoneNum,
                                    tableId = g.Key.tableId,
                                    //OrderTime =string.Format("{0:T}", Convert.ToDateTime( g.Key.OrderTime))
                                    orderDate = g.Key.orderDate,
                                };


                List<Order> orders = queryByID.ToList();

                return View(orders);
            }
        }
        //廠商未出餐點訂單列表 ==>準備完成,出餐
        [HttpPost]
        public ActionResult Order_list_bussiness(int orderId_ok)
        {
            using (Models.FoodCourtDBEntities db = new FoodCourtDBEntities())
            {
                var order = (from o in db.Orders
                             where o.orderId == orderId_ok
                             select o).FirstOrDefault();
                order.orderState = 2;
                db.SaveChanges();
                return Json(true);
            }
        }


        public ActionResult Order_list_history_bussiness()
        {


            return View();
        }


        
    }
}