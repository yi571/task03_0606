using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace task03_0606.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult GetCart()
        {
            var cart = task03_0606.Models.Cart.Operation.GetCurrentCart();
            //if (cart.cartItemList.Count == 0)
            //{

            //    cart.cartItemList.Add(new Models.Cart.CartItem
            //    {
            //        produtctId = 4,
            //        productName = "Test1",
            //        price = 30m,
            //        quantity = 2,
            //    });
            //}
            //else
            //{
            //    cart.cartItemList.First().quantity += 1;

            //}
            cart.AddProduct(1);
            return Content(string.Format("目前購物車{0}元", cart.TotalAmount));



        }
    }
}