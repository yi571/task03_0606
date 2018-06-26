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

        /*AP0010_ConitionModel condition = new AP0010_ConitionModel();*/ //下拉式選單
        FoodCourtDBEntities db = new FoodCourtDBEntities();


        // GET: Products
        public ActionResult Index(string TableId) {
            if (Session["identity"] == null) {
                Session["identity"] = "Guest";
            };
            if (!String.IsNullOrEmpty(TableId)) {
                Session["TableId"] = TableId;
            }
            var query = from o in db.Categories
                        select new FoodCategories {
                            categoryID = o.categoryID,
                            categoryName = o.categoryName,
                            Description = o.Description,
                            categoryPicture = o.categoryPicture,
                            categoryURL = o.categoryURL

                        };
            List<FoodCategories> categorieslist = query.ToList();
            return View(categorieslist);
        }

        //管理人員-所有商品列表
        public ActionResult ManagerIndex() {

            if (String.IsNullOrEmpty((string)Session["logState"])) {  //如未登入，則重導到登入頁面
                Session["lastPage"] = "/Products/ManagerIndex";      //儲存最後頁面
                return RedirectToAction("Login", "Member");  //重導到登入頁面
            }

            // 假如不是商店管理者，則重導回首頁
            if (Session["identity"].ToString() != "store" || Session["identity"].ToString() != "storeUser") {
                return RedirectToAction("Index", "Products");
            }
            int salesVolume = 0;
            var query = from o in db.Products
                        select new FoodProduct {
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

        public ActionResult Create() {
            if (String.IsNullOrEmpty((string)Session["logState"])) {  //如未登入，則重導到登入頁面
                Session["lastPage"] = "/Create/ManagerIndex";      //儲存最後頁面
                return RedirectToAction("Login", "Member");  //重導到登入頁面
            }

            //如果不是商店管理者，則重導回首頁
            if (Session["identity"].ToString() != "store" || Session["identity"].ToString() != "storeUser") {
                return RedirectToAction("Index", "Products");
            }
            return View();
        }


        [HttpPost]
        public ActionResult Create(Models.Product postback) {
            if (this.ModelState.IsValid) //如果資料驗證成功
            {
                using (Models.FoodCourtDBEntities db = new Models.FoodCourtDBEntities()) {
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






        public ActionResult Edit(int? productID) {

            if (String.IsNullOrEmpty((string)Session["logState"])) {  //如未登入，則重導到登入頁面
                Session["lastPage"] = "/Edit/ManagerIndex";      //儲存最後頁面
                return RedirectToAction("Login", "Member");  //重導到登入頁面
            }

            //如果不是商店管理者，則重導回首頁
            if (Session["identity"].ToString() != "store" || Session["identity"].ToString() != "storeUser") {
                return RedirectToAction("Index", "Products");
            }

            using (Models.FoodCourtDBEntities db = new Models.FoodCourtDBEntities()) {
                //抓取Product.Id等於輸入id的資料
                var result = (from s in db.Products where s.productID == productID select s).FirstOrDefault();
                if (result != default(Models.Product)) //判斷此id是否有資料
                {
                    return View(result); //如果有回傳編輯商品頁面
                } else {   //如果沒有資料則顯示錯誤訊息並導回Index頁面
                    TempData["resultMessage"] = "資料有誤，請重新操作";
                    return RedirectToAction("ManagerIndex");
                }
            }
        }
        //編輯商品頁面 - 資料傳回處理
        [HttpPost]
        public ActionResult Edit(Models.Product postback) {
            if (this.ModelState.IsValid) //判斷使用者輸入資料是否正確
            {
                using (Models.FoodCourtDBEntities db = new Models.FoodCourtDBEntities()) {
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
            } else //如果資料不正確則導向自己(Edit頁面)
              {
                return View(postback);
            }
        }
        // GET: Products/Delete/5
        public ActionResult Delete(int? productID) {
            if (productID == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product pd = db.Products.Find(productID);
            if (pd == null) {
                return HttpNotFound();
            }
            return View(pd);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int productID) {
            Product product = db.Products.Find(productID);
            db.Products.Remove(product);
            db.SaveChanges();
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
        public ActionResult store21354423Index() {
            int salesVolume = 0;
            var query = from o in db.Products
                        where o.storeId == ("21354423")
                        select new FoodProduct {
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
        public ActionResult Drinkproducts() {
            if (Session["identity"] == null) {
                Session["identity"] = "Guest";
            };
            var query = from o in db.Products
                        where o.categoryID == 2
                        select new FoodProduct {
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
        public ActionResult Dessertproducts() {
            if (Session["identity"] == null) {
                Session["identity"] = "Guest";
            };
            var query = from o in db.Products
                        where o.categoryID == 3
                        select new FoodProduct {
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
        public ActionResult Noodleproducts() {
            if (Session["identity"] == null) {
                Session["identity"] = "Guest";
            };
            var query = from o in db.Products
                        where o.categoryID == 4
                        select new FoodProduct {
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
        public ActionResult Riceproducts() {
            if (Session["identity"] == null) {
                Session["identity"] = "Guest";
            };
            var query = from o in db.Products
                        where o.categoryID == 5
                        select new FoodProduct {
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
        public ActionResult Fastfoodproducts() {
            if (Session["identity"] == null) {
                Session["identity"] = "Guest";
            };
            var query = from o in db.Products
                        where o.categoryID == 6
                        select new FoodProduct {
                            productID = o.productID,
                            productName = o.productName,
                            productPicture = o.productPicture,
                            productDescription = o.productDescription,
                            productPrice = o.productPrice
                        };
            List<FoodProduct> foodproducts = query.ToList();
            return View(foodproducts);

        }

        // 下拉式選單ManagerIndex
        //    public class AP0010_ConitionModel
        //{
        //    //我要選取的預設值
        //    public string storeId { get; set; }
        //    //資料來源
        //    public List<FoodProduct> State_ListItem { get; set; }
        //}

        //public class AP0010ViewModel
        //{
        //    public AP0010_ConitionModel condition { get; set; }
        //    //以下可以放其他條件
        //}

        //public SelectList GetEMP_CATEGORY()
        //{

        //    List<FoodProduct> Category = new List<FoodProduct>()
        //{
        //    new FoodProduct() {storeId = "21354423", storeProductId = ""},
        //    new FoodProduct() {storeId = "20035001", storeProductId = ""},
        //    new FoodProduct() {storeId = "15498527", storeProductId = ""},
        //    new FoodProduct() {storeId = "00543689", storeProductId = ""},
        //    new FoodProduct() {storeId = "54123513", storeProductId = ""},
        //    new FoodProduct() {storeId = "68999999", storeProductId = ""},
        //    new FoodProduct() {storeId = "70425874", storeProductId = ""},
        //    new FoodProduct() {storeId = "90856422", storeProductId = ""},

        //};

        //    return new SelectList(Category, dataTextField: "Text", dataValueField: "Value");
        //}
        public class ViewModel {
            public string Name { get; set; }
            public IEnumerable<Product> MyList { get; set; }
        }
        public ActionResult testIndex() {


            List<SelectListItem> mySelectItemList = new List<SelectListItem>();

            foreach (var item in db.Stores) {
                mySelectItemList.Add(new SelectListItem() {
                    Text = item.storeId,
                    Value = item.storeName,
                    Selected = false
                });
            }

            ViewModel model = new ViewModel() //上面的 Model
            {
                //MyList = mySelectItemList
            };

            return View(model);
        }

    }

}
