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
       
        // GET: Order
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