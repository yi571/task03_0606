using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using task03_0606.Models;

namespace task03_0606.Controllers
{
    public class ProductsController : Controller
    {

        FoodProductsEntities db = new FoodProductsEntities();
        // GET: Products
        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult ManagerIndex()
        {
            var query = from o in db.myfoodproducts
                        select new newproduct{
                            productId=o.productId,
                           title=o.title,
                           price=o.price,
                           picture=o.picture,
                           introduce=o.introduce      
                        };
            List<newproduct> prolist = query.ToList();

            
            return View(prolist);
        }

 
        public ActionResult Create(int productId)
        {
            var query = from o in db.myfoodproducts
                        where o.productId == productId
                        select o;
            myfoodproduct mfp = query.Single();

            return View(mfp);
        }
        [HttpPost]
        public ActionResult Create(myfoodproduct newproduct)
        {
            myfoodproduct mfp = db.myfoodproducts.Find(newproduct.productId);
            mfp.productId = newproduct.productId;
            mfp.title = newproduct.title;
            mfp.price = newproduct.price;
            mfp.picture = newproduct.picture;
            mfp.introduce = newproduct.introduce;
            db.SaveChanges();
            

            return RedirectToAction("ManagerIndex");
        }






        public ActionResult Edit()
        {

            return View();
        }





        public ActionResult Drinkproducts()
        {
            var query = from o in db.myfoodproducts
                //where o.productId < 200 | o.productId > 1
                        where o.productId <= 200
                        where o.productId >= 1
                        select new Class1
                        {
                            productId = o.productId,
                            title = o.title,
                            price = o.price,
                            picture = o.picture,
                            introduce = o.introduce,
                            addcount = (int)o.addcount

                        };
            List<Class1> prolist = query.ToList();
            return View(prolist);

        }

        public ActionResult Dessertproducts()
        {
            var query = from o in db.myfoodproducts
                        where o.productId <= 400
                        where o.productId >= 201
                        select new Class1
                        {
                            productId = o.productId,
                            title = o.title,
                            price = o.price,
                            picture = o.picture,
                            introduce = o.introduce,
                            addcount = (int)o.addcount

                        };
            List<Class1> prolist = query.ToList();
            return View(prolist);
        }
        public ActionResult Noodleproducts()
        {
            var query = from o in db.myfoodproducts
                        where o.productId <= 600
                        where o.productId >= 401
                        select new Class1
                        {
                            productId = o.productId,
                            title = o.title,
                            price = o.price,
                            picture = o.picture,
                            introduce = o.introduce,
                            addcount = (int)o.addcount

                        };
            List<Class1> prolist = query.ToList();
            return View(prolist);
        }
        public ActionResult Riceproducts()
        {
            var query = from o in db.myfoodproducts
                        where o.productId <= 800
                        where o.productId >= 601
                        select new Class1
                        {
                            productId = o.productId,
                            title = o.title,
                            price = o.price,
                            picture = o.picture,
                            introduce = o.introduce,
                            addcount = (int)o.addcount

                        };
            List<Class1> prolist = query.ToList();
            return View(prolist);
        }

        public ActionResult Fastfoodproducts()

           
        {
            var query = from o in db.myfoodproducts
                        where o.productId <= 1000
                        where o.productId >= 801
                        select new Class1
                        {
                            productId = o.productId,
                            title = o.title,
                            price = o.price,
                            picture = o.picture,
                            introduce=o.introduce,
                            addcount=(int)o.addcount
                         
                        };
            List<Class1> prolist = query.ToList();
            return View(prolist);
    }
        
         
        }
    }
