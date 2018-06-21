using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace task03_0606.Controllers
{
    public class CartController : Controller
    {
        public ActionResult Index ()
        {
            return View();
        }


        // GET: Cart
        public ActionResult GetCart()
        {
            return PartialView("_CartList");
        }
        
        public ActionResult AddToCart(int id ) {
            var currentCart = task03_0606.Models.Cart.Operation.GetCurrentCart();
            currentCart.AddProduct(id);
            return PartialView("_CartList");
        }

        public ActionResult RemoveFromCart(int id) {
            var currentCart = task03_0606.Models.Cart.Operation.GetCurrentCart();
            currentCart.RemoveProduct(id);
            return PartialView("_CartList");

        }
    }
}