using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using task03_0606.Models;
using System.Data.SqlClient;

namespace task03_0606.Controllers
{
    public class OrderController : Controller
    {

        // 購物車清單
        public ActionResult Index()
        {
            if (String.IsNullOrEmpty((string)Session["logState"])) {  //如未登入，則重導到登入頁面
                Session["lastPage"] = "/Order/Index";      //儲存最後頁面
                return RedirectToAction("Login", "Member");  //重導到登入頁面
            }
            //將存在session中的 電話取出
            string phonString = Session["userPhone"].ToString();
            //將存在session中的 桌號取出
            int tableNum = Convert.ToInt32(Session["seat"]) ;
            
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
                ViewBag.seatLoction = seat.tableLocation;
            }
                return View();
        }

        //購物車清單 ==> 訂單
        public ActionResult OrderPayment()
        {
            if (String.IsNullOrEmpty((string)Session["logState"])) {  //如未登入，則重導到登入頁面
                Session["lastPage"] = "/Order/OrderPayment";      //儲存最後頁面
                return RedirectToAction("Login", "Member");  //重導到登入頁面
            }
            //將存在session中的 電話取出
            string phonString = Session["userPhone"].ToString();
            //將存在session中的 桌號取出
            int tableNum = Convert.ToInt32(Session["seat"]);
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
                 //清空購物車session   
                     
            } //return Content("訂購成功");
                return RedirectToAction("Index", "Products");

            }
            return View();


        }

        //廠商未出餐清單
        public ActionResult Order_deteail_bussiness()
        {
            string stroeId = Session["storeId"].ToString();

            //依結單時間=null &　廠商編號，篩選廠商訂單明細
            using (Models.FoodCourtDBEntities db = new FoodCourtDBEntities())
            {
                var query = from o in db.Orders
                                 join c in db.OrderDetials on o.orderId equals c.orderId into ps
                                 from c in ps.DefaultIfEmpty()
                                 where c.productionStatus == 1  && c.Product.Store.storeId == stroeId
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


        public ActionResult Order_list_bussiness() {
            

            return View();
        }
        [HttpPost]
        public ActionResult Order_list_bussiness(int OrderId)
        {



            return View();
        }

        //
        public ActionResult Order_list_chickToDatabase_bussiness(int productId_ok, int orderId_ok)
        {
            int test = productId_ok;

            using (Models.FoodCourtDBEntities db = new FoodCourtDBEntities()) {
                var result = (from o in db.OrderDetials
                          where o.productID == productId_ok && o.orderId == orderId_ok
                          select o).FirstOrDefault();

                result.productionStatus= 2 ; 
                db.SaveChanges();
                ViewBag.Message = string.Format("確定訂單{0}的{1}出餐", result.orderId, result.Product.productName);
                return Json(true);
            }
        }

        public ActionResult Order_list_history_bussiness()
        {


            return View();
        }



        public ActionResult Order_list_costomer() {

           
            return View();
        }
        [HttpPost]
        public ActionResult Order_list_costomer(int OrderId)
        {



            return View();
        }

    }
}