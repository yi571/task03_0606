using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using task03_0606.Models;


namespace task03_0606.Controllers
{

    public class ProductsController : Controller
    {

        FoodProductsEntities db = new FoodProductsEntities();
        private readonly string base64;

        // GET: Products
        public ActionResult Index()
        {
            
            return View();
        }


        public ActionResult ManagerIndex()
        {
            List<Models.myfoodproduct> result = new List<Models.myfoodproduct>();
            ViewBag.ResultMessage = TempData["ResultMessage"];
            using (Models.FoodProductsEntities db = new Models.FoodProductsEntities())
            {
                result = (from pro in db.myfoodproducts select pro).ToList();
                return View(result);
            }
                var query = from o in db.myfoodproducts
                            select new Class1
                            {
                                productId = o.productId,
                                title = o.title,
                                price = o.price,
                                picture = o.picture,
                                introduce = o.introduce
                            };
            List<Class1> prolist = query.ToList();
            return View(prolist);
        }
       


        public ActionResult Create()
        {

            return View();
        }

       
    [HttpPost]
        public ActionResult Create(string productsId, string title,string price,string picture,string introduce)
        {
             myfoodproduct mfd = new myfoodproduct()
            {
                productId = Convert.ToInt32(productsId),
                title = title,
                price = Convert.ToInt32(price),
                picture = (picture),
               
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
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {

            if (file.ContentLength > 0)
            {

                var fileName = Path.GetFileName(file.FileName);

                var path = Path.Combine(Server.MapPath("~/photo/"), fileName);

                file.SaveAs(path);

            }

            return RedirectToAction("ManagerIndex", "Products");
        }





        public ActionResult Edit(int productId)
        {
            using (Models.FoodProductsEntities db = new Models.FoodProductsEntities())
            {
                var result = (from pro in db.myfoodproducts where pro.productId == productId select pro).FirstOrDefault();
                if (result != default(Models.myfoodproduct))
                {
                    return View(result);
                }
                else
                {
                    TempData["resultMessage"] = "ERROR";
                    return RedirectToAction("ManagerIndex");
                }
            }
        }

        //[HttpPost]
        //public ActionResult Edit(Models.myfoodproduct postback)
        //{
        //    if (this.ModelState.IsValid)
        //    {
        //        using (Models.FoodProductsEntities db = new Models.FoodProductsEntities())
        //        {
        //            var result = (from pro in db.myfoodproducts where pro.productId == postback.productId select pro).FirstOrDefault();
        //            result.title = postback.title;
        //            result.price = postback.price;
        //            result.introduce = postback.introduce;
        //            result.picture = postback.picture;

        //            db.SaveChanges();
        //            TempData["ResultMessage"] = string.Format("商品[{0}]成功編輯", postback.title);
        //            return RedirectToAction("ManagerIndex");
        //        }
        //    }
        //    else
        //    {
        //        return View(postback);
        //    }
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "productId,title,price,picture,introduce")]Class1 class1)
        {
            if (ModelState.IsValid)
            {
                db.Entry(class1).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ManagerIndex");
            }
            return View(class1);
        }

        [HttpPost]
        public ActionResult Delete(int productId)
        {
            using (Models.FoodProductsEntities db = new Models.FoodProductsEntities())
            {
                //抓取Product.Id等於輸入id的資料
                var result = (from s in db.myfoodproducts where s.productId == productId select s).FirstOrDefault();
                if (result != default(Models.myfoodproduct)) //判斷此id是否有資料
                {
                    db.myfoodproducts.Remove(result);

                    //儲存所有變更
                    db.SaveChanges();

                    //設定成功訊息並導回index頁面
                    TempData["ResultMessage"] = string.Format("商品[{0}]成功刪除", result.title);
                    return RedirectToAction("ManagerIndex");
                }
                else
                {   //如果沒有資料則顯示錯誤訊息並導回Index頁面
                    TempData["resultMessage"] = "指定資料不存在，無法刪除，請重新操作";
                    return RedirectToAction("ManagerIndex");
                }
            }
        }

        //[HttpPost]
        //public ActionResult Edit(int productId)
        //{
        //    var query = from o in db.myfoodproducts
        //                where o.productId == productId
        //                select o;
        //    myfoodproduct mfp = query.Single();

        //    return View(mfp);
        //}

        //[HttpPost]
        //public ActionResult Edit(myfoodproduct mfp)
        //{
        //    myfoodproduct mfpdbserver = db.myfoodproducts.Find(mfp.productId);
        //    mfpdbserver.title = mfp.title;
        //    mfpdbserver.price = mfp.price;
        //    mfpdbserver.introduce = mfp.introduce;
        //    mfpdbserver.picture = mfp.picture;
        //    db.SaveChanges();
        //    return RedirectToAction("ManagerIndex");
        //}

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
