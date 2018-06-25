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

        public ActionResult EditCartItem(int ItemId_edit, int quantity_edit, string ItemNoet_edit)
        {
            var currentCart = task03_0606.Models.Cart.Operation.GetCurrentCart();
            currentCart.EditCartProduct(ItemId_edit, quantity_edit , ItemNoet_edit);
            return PartialView("_CartList");
            //return RedirectToAction("Index", "Order");
        }

        public ActionResult RemoveFromCart(int id) {
            var currentCart = task03_0606.Models.Cart.Operation.GetCurrentCart();
            currentCart.RemoveProduct(id);
            return PartialView("_CartList");
        }



        public ActionResult ClearFromCart() {
            var currentCart = task03_0606.Models.Cart.Operation.GetCurrentCart();
            currentCart.ClearCart();
            //return PartialView("_CartList");
            return RedirectToAction("Restaurent", "Home");
        }
    }
}