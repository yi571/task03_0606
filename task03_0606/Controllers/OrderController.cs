﻿using System;
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

        // GET: Order
        public ActionResult Index()
        {
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
       
        public ActionResult OrderPayment()
        {
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
                     
            } return Content("訂購成功");

            }
            return View();


        }

        public ActionResult Order_deteail_bussiness()
        {
            return View();
        }


        public ActionResult Order_list_bussiness() {
            

            return View();
        }
        [HttpPost]
        public ActionResult Order_list_bussiness(int OrderId)
        {



            return View();
        }

        public ActionResult Order_list_chickToDatabase_bussiness(int productId_ok, int orderId_ok)
        {

            return View();
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