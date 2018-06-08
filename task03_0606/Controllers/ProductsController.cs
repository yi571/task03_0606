using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace task03_0606.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Drinkproducts()
        {
            return View();
        }

        public ActionResult Dessertproducts()
        {
            return View();
        }
        public ActionResult Noodleproducts()
        {
            return View();
        }
        public ActionResult Riceproducts()
        {
            return View();
        }

        public ActionResult Fastfoodproducts()
        {
            return View();
        }
    }
}