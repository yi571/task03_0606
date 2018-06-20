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
            var cart = task03_0606.Models.Cart.Operation.GetCurrentCart();
            
            if (cart.cartItemList.Count == 0)
            {
                cart.cartItemList.Add(
                    new Models.Cart.CartItem()
                    {
                        produtctId = 1,
                        productName = "5555",
                        quantity = 20,
                        price = 5,
                    });
            }
            else {
                cart.cartItemList.FirstOrDefault().quantity += 1;
                    }

            return Content (string.Format("目前購物車共： {0} 元", cart.TotalAmount));
            
        }
    }
}