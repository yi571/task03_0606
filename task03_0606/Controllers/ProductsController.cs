using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using task03_0606.Models;


namespace task03_0606.Controllers {

    public class ProductsController : Controller {

        FoodProductsEntities db = new FoodProductsEntities();


        // GET: Products
        public ActionResult Index() {

            return View();
        }


        public ActionResult ManagerIndex() {

            return View();
        }



        public ActionResult Create() {

            return View();
        }

        public ActionResult Edit() {

            return View();
        }




        public ActionResult Drinkproducts() {

            return View();

        }
     
        public ActionResult Dessertproducts()
        {
        //    var query = from o in db.myfoodproducts
        //                    //where o.productId < 200 | o.productId > 1
        //                where o.productId <= 200
        //                where o.productId >= 001
        //                select new Class1
        //                {
 
        //productId = o.productId,
        //                    productName = o.title,
        //                    productPrice = o.price,
        //                    productPicture = o.picture,
        //                    productDescription = o.introduce,
        //                    salesVolue = (int)o.addcount

        //                };
        //    List<Class1> prolist = query.ToList();
            return View();
        }
        public ActionResult Noodleproducts() {

            return View();
        }
        public ActionResult Riceproducts() {

            return View();
        }

        public ActionResult Fastfoodproducts() {

            return View();
        }


    }
}
