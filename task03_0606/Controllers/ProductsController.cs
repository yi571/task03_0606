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


        FoodCourtDBEntities dbpro = new FoodCourtDBEntities();

        // GET: Products
        public ActionResult Index()
        {
            var query = from o in dbpro.Categories
                        select new FoodCategories
                        {
                            categoryID = o.categoryID,
                            categoryName = o.categoryName,
                            Description = o.Description,
                            categoryPicture = o.categoryPicture,
                            categoryURL=o.categoryURL

    };
            List<FoodCategories> categorieslist = query.ToList();
            return View(categorieslist);
        }

        //管理人員-所有商品列表
        public ActionResult ManagerIndex()
        {


            int salesVolume = 0;
            var query = from o in dbpro.Products
                        select new FoodProduct
                        {
                            productID = o.productID,
                            productName = o.productName,
                            productPicture = o.productPicture,
                            salesVolume = o.salesVolume == null ? default(int) : salesVolume,
                            storeProductId = o.storeProductId,
                            productDescription = o.productDescription,
                            productPrice = o.productPrice,
                            storeId = o.storeId,
                            productState =o.productState,
                            categoryID = o.categoryID
                        };
            List<FoodProduct> foodproductslist = query.ToList();
            return View(foodproductslist);
        }

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(Models.Product postback)
        {
            if (this.ModelState.IsValid) //如果資料驗證成功
            {
                using (Models.FoodCourtDBEntities db = new Models.FoodCourtDBEntities())
                {
                    //將回傳資料postback加入至Products
                    db.Products.Add(postback);

                    //儲存異動資料
                    db.SaveChanges();

                    //設定成功訊息
                    TempData["ResultMessage"] = String.Format("商品[{0}]成功建立", postback.productName);

                    //轉導Product/Index頁面
                    return RedirectToAction("ManagerIndex");
                }
            }
            //失敗訊息
            ViewBag.ResultMessage = "輸入資料有誤，請檢查";

            //停留在Create頁面
            return View(postback);
        }






        public ActionResult Edit(int? productID)
        {
            using (Models.FoodCourtDBEntities db = new Models.FoodCourtDBEntities())
            {
                //抓取Product.Id等於輸入id的資料
                var result = (from s in db.Products where s.productID == productID select s).FirstOrDefault();
                if (result != default(Models.Product)) //判斷此id是否有資料
                {
                    return View(result); //如果有回傳編輯商品頁面
                }
                else
                {   //如果沒有資料則顯示錯誤訊息並導回Index頁面
                    TempData["resultMessage"] = "資料有誤，請重新操作";
                    return RedirectToAction("ManagerIndex");
                }
            }
        }
        //編輯商品頁面 - 資料傳回處理
        [HttpPost]
        public ActionResult Edit(Models.Product postback)
        {
            if (this.ModelState.IsValid) //判斷使用者輸入資料是否正確
            {
                using (Models.FoodCourtDBEntities db = new Models.FoodCourtDBEntities())
                {
                    //抓取Product.Id等於回傳postback.Id的資料
                    var result = (from s in db.Products where s.productID == postback.productID select s).FirstOrDefault();

                    //儲存使用者變更資料
                    result.productName = postback.productName; result.productPicture = postback.productPicture;
                    result.salesVolume = postback.salesVolume; result.storeProductId = postback.storeProductId;
                    result.productDescription = postback.productDescription; result.productPrice = postback.productPrice;
                    result.storeId = postback.storeId; result.productState = postback.productState;
                    result.categoryID = postback.categoryID;

                    //儲存所有變更
                    db.SaveChanges();

                    //設定成功訊息並導回index頁面
                    TempData["ResultMessage"] = String.Format("商品[{0}]成功編輯", postback.productName);
                    return RedirectToAction("ManagerIndex");
                }
            }
            else //如果資料不正確則導向自己(Edit頁面)
            {
                return View(postback);
            }
        }
        // GET: Products/Delete/5
        public ActionResult Delete(int? productID)
        {
            if (productID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product pd = dbpro.Products.Find(productID);
            if (pd == null)
            {
                return HttpNotFound();
            }
            return View(pd);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int productID)
        {
            Product product = dbpro.Products.Find(productID);
            dbpro.Products.Remove(product);
            dbpro.SaveChanges();
            return RedirectToAction("ManagerIndex");
        }





        //[HttpPost]
        //public ActionResult Delete(int? productID)
        //{
        //    using (Models.FoodCourtDBEntities db = new Models.FoodCourtDBEntities())
        //    {
        //        //抓取Product.Id等於輸入id的資料
        //        var result = (from s in db.Products where s.productID == productID select s).FirstOrDefault();
        //        if (result != default(Models.Product)) //判斷此id是否有資料
        //        {
        //            db.Products.Remove(result);

        //            //儲存所有變更
        //            db.SaveChanges();

        //            //設定成功訊息並導回ManagerIndex頁面
        //            TempData["ResultMessage"] = string.Format("商品[{0}]成功刪除", result.productName);
        //            return RedirectToAction("ManagerIndex");
        //        }
        //        else
        //        {   //如果沒有資料則顯示錯誤訊息並導回ManagerIndex頁面
        //            TempData["resultMessage"] = "指定資料不存在，無法刪除，請重新操作";
        //            return RedirectToAction("ManagerIndex");
        //        }
        //    }
        //}

            //麥當勞管理介面
        public ActionResult store00543689Index(string storeId)
        {
            
            int salesVolume = 0;
            var query = from o in dbpro.Products
                        where o.storeId = 
                        select new FoodProduct
                        {
                            productID = o.productID,
                            productName = o.productName,
                            productPicture = o.productPicture,
                            salesVolume = o.salesVolume == null ? default(int) : salesVolume,
                            storeProductId = o.storeProductId,
                            productDescription = o.productDescription,
                            productPrice = o.productPrice,
                            storeId = o.storeId,
                            productState = o.productState,
                            categoryID = o.categoryID
                        };
            List<FoodProduct> foodproductslist = query.ToList();
            return View(foodproductslist);
        }




        //商品頁面 - 飲品&湯品-類別代號2
        public ActionResult Drinkproducts()
        {
            var query = from o in dbpro.Products
                        where o.categoryID == 2
                        select new FoodProduct
                        {
                            productID = o.productID,
                            productName = o.productName,
                            productPicture = o.productPicture,
                            productDescription = o.productDescription,
                            productPrice = o.productPrice
                        };
            List<FoodProduct> foodproducts = query.ToList();
            return View(foodproducts);

        }

        //商品頁面 - 甜點類-類別代號3
        public ActionResult Dessertproducts()
        {
            var query = from o in dbpro.Products
                        where o.categoryID == 3
                        select new FoodProduct
                        {
                            productID = o.productID,
                            productName = o.productName,
                            productPicture = o.productPicture,
                            productDescription = o.productDescription,
                            productPrice = o.productPrice
                        };
            List<FoodProduct> foodproducts = query.ToList();
            return View(foodproducts);

        }

        //商品頁面 - 麵食類-類別代號4
        public ActionResult Noodleproducts()
        {
            var query = from o in dbpro.Products
                        where o.categoryID == 4
                        select new FoodProduct
                        {
                            productID = o.productID,
                            productName = o.productName,
                            productPicture = o.productPicture,
                            productDescription = o.productDescription,
                            productPrice = o.productPrice
                        };
            List<FoodProduct> foodproducts = query.ToList();
            return View(foodproducts);

        }
        //商品頁面 - 米飯類-類別代號5
        public ActionResult Riceproducts()
        {
            var query = from o in dbpro.Products
                        where o.categoryID == 5
                        select new FoodProduct
                        {
                            productID = o.productID,
                            productName = o.productName,
                            productPicture = o.productPicture,
                            productDescription = o.productDescription,
                            productPrice = o.productPrice
                        };
            List<FoodProduct> foodproducts = query.ToList();
            return View(foodproducts);

        }
        //商品頁面 - 速食類-類別代號6
        public ActionResult Fastfoodproducts()
        {
            var query = from o in dbpro.Products
                        where o.categoryID == 6
                        select new FoodProduct
                        {
                            productID = o.productID,
                            productName = o.productName,
                            productPicture = o.productPicture,
                            productDescription = o.productDescription,
                            productPrice = o.productPrice
                        };
            List<FoodProduct> foodproducts = query.ToList();
            return View(foodproducts);

        }


    }
}
