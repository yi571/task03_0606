using System;
using System.Collections.Generic;
using System.IO;
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
                        select new Class1{
                            productId=o.productId,
                           title=o.title,
                           price=o.price,
                           picture=o.picture,
                           introduce=o.introduce      
                        };
            List<Class1> prolist = query.ToList();
            return View(prolist);
        }


        public ActionResult Create()
        {

            return View();
        }

       
    [HttpPost]
        public ActionResult Create(string productsId, string title, string price,string picture,string introduce)
        {

            myfoodproduct mfd = new myfoodproduct()
            {
                productId = Convert.ToInt32(productsId),
                title = title,
                price = Convert.ToInt32(price),
                picture =picture,
                introduce = introduce,
                addcount = 12,
                imageURL = ""
            };

            //var query = from o in db.myfoodproducts
            //            select o;
            //myfoodproduct mfp = query.Single();
            db.myfoodproducts.Add(mfd);
            db.SaveChanges();
            //return Content(productsId);
            return RedirectToAction("ManagerIndex", "Products");
        }
        public ActionResult Upload() {

            return View();

        }
        //[HttpPost]
        //public ActionResult Upload(HttpPostedFileBase file) {

        //    if (file.ContentLength > 0) {

        //        var fileName = Path.GetFileName(file.FileName);

        //        var path = Path.Combine(Server.MapPath("~/photo"), fileName);

        //        file.SaveAs(path);

        //    }

        //    return RedirectToAction("ManagerIndex", "Products");
        //}

        public ActionResult Edit()
        {
            return View();
        }

        public ActionResult Edit(int productId)
        {
            var query = from o in db.myfoodproducts
                        where o.productId == productId
                        select o;
            myfoodproduct mfp = query.Single();

            return View(mfp);
        }

        [HttpPost]
        public ActionResult Edit(myfoodproduct mfp)
        {
            myfoodproduct mfpdbserver = db.myfoodproducts.Find(mfp.productId);
            mfpdbserver.title = mfp.title;
            mfpdbserver.price = mfp.price;
            mfpdbserver.introduce = mfp.introduce;
            mfpdbserver.picture = mfp.picture;
            db.SaveChanges();
            return RedirectToAction("ManagerIndex");
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
