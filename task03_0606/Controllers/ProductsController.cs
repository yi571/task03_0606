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


        FoodCourtDBEntities dbpro = new FoodCourtDBEntities();

        // GET: Products
        public ActionResult Index()
        {
            var query = from o in dbpro.Categories
                        select new FoodCategories {
                            categoryID=o.categoryID,
                            categoryName=o.categoryName,
                            Description=o.Description,
                            categoryPicture=o.categoryPicture

    };
            List<FoodCategories> categorieslist = query.ToList();
            return View(categorieslist);
        }


        public ActionResult ManagerIndex() {


            int salesVolume = 0;
            var query = from o in dbpro.Products
                        select new FoodProducts
                        {
                          productId=o.productID,
                          productName=o.productName ,
                          productPicture=o.productPicture,
                          salesVolume =o.salesVolume== null ? default(int) :salesVolume,
                          storeProductId =o.storeProductId,
                          productDescription=o.productDescription,
                          productPrice=o.productPrice,
                          storeId=o.storeId,
                          productState=o.productState,
                          categoryID=o.categoryID
                        };
            List<FoodProducts> foodproductslist = query.ToList();
            return View(foodproductslist);
        }

        public ActionResult Create() {

            return View();
        }

        public ActionResult Edit() {

            return View();
        }




        public ActionResult Drinkproducts() {
            var query = from o in dbpro.Products
                        where o.categoryID==2
                        select new FoodProducts
                        {
                                productId=o.productID,
                                productName=o.productName,
                                productPicture=o.productPicture,
                                productDescription=o.productDescription,
                                productPrice=o.productPrice
                        };
            List<FoodProducts> foodproducts = query.ToList();
            return View(foodproducts);

        }
     
        public ActionResult Dessertproducts()
        {
            var query = from o in dbpro.Products
                        where o.categoryID == 3
                        select new FoodProducts
                        {
                            productId = o.productID,
                            productName = o.productName,
                            productPicture = o.productPicture,
                            productDescription = o.productDescription,
                            productPrice = o.productPrice
                        };
            List<FoodProducts> foodproducts = query.ToList();
            return View(foodproducts);

        }
        public ActionResult Noodleproducts() {
            var query = from o in dbpro.Products
                        where o.categoryID == 4
                        select new FoodProducts
                        {
                            productId = o.productID,
                            productName = o.productName,
                            productPicture = o.productPicture,
                            productDescription = o.productDescription,
                            productPrice = o.productPrice
                        };
            List<FoodProducts> foodproducts = query.ToList();
            return View(foodproducts);

        }
        public ActionResult Riceproducts() {
            var query = from o in dbpro.Products
                        where o.categoryID == 5
                        select new FoodProducts
                        {
                            productId = o.productID,
                            productName = o.productName,
                            productPicture = o.productPicture,
                            productDescription = o.productDescription,
                            productPrice = o.productPrice
                        };
            List<FoodProducts> foodproducts = query.ToList();
            return View(foodproducts);

        }

        public ActionResult Fastfoodproducts() {
            var query = from o in dbpro.Products
                        where o.categoryID == 6
                        select new FoodProducts
                        {
                            productId = o.productID,
                            productName = o.productName,
                            productPicture = o.productPicture,
                            productDescription = o.productDescription,
                            productPrice = o.productPrice
                        };
            List<FoodProducts> foodproducts = query.ToList();
            return View(foodproducts);

        }


    }
}
