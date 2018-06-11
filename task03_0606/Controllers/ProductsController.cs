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

        public ActionResult Drinkproducts()
        {

            var query = from o in db.myfoodproducts
                        select new Class5
                        {
                            productId = o.productId,
                            title = o.title,
                            price = o.price,
                            picture = o.picture,
                            introduce = o.introduce,
                            addcount = (int)o.addcount

                        };
            List<Class5> prolist = query.ToList();
            return View(prolist);

        }

        public ActionResult Dessertproducts()
        {
            var query = from o in db.myfoodproducts
                        select new Class5
                        {
                            productId = o.productId,
                            title = o.title,
                            price = o.price,
                            picture = o.picture,
                            introduce = o.introduce,
                            addcount = (int)o.addcount

                        };
            List<Class5> prolist = query.ToList();
            return View(prolist);
        }
        public ActionResult Noodleproducts()
        {
            var query = from o in db.myfoodproducts
                        select new Class5
                        {
                            productId = o.productId,
                            title = o.title,
                            price = o.price,
                            picture = o.picture,
                            introduce = o.introduce,
                            addcount = (int)o.addcount

                        };
            List<Class5> prolist = query.ToList();
            return View(prolist);
        }
        public ActionResult Riceproducts()
        {
            var query = from o in db.myfoodproducts
                        select new Class5
                        {
                            productId = o.productId,
                            title = o.title,
                            price = o.price,
                            picture = o.picture,
                            introduce = o.introduce,
                            addcount = (int)o.addcount

                        };
            List<Class5> prolist = query.ToList();
            return View(prolist);
        }

        public ActionResult Fastfoodproducts()

           
        {
            var query = from o in db.myfoodproducts
                        select new Class5
                        {
                            productId = o.productId,
                            title = o.title,
                            price = o.price,
                            picture = o.picture,
                            introduce=o.introduce,
                            addcount=(int)o.addcount
                         
                        };
            List<Class5> prolist = query.ToList();
            return View(prolist);
    }
        
         
        }
    }
