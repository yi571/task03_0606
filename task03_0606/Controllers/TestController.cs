using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using task03_0606.Models.Interface;
using task03_0606.Models.Repositiry;

namespace task03_0606.Controllers
{
    public class TestController : Controller
    {
        private IOrderRepository orderRepository;

        public TestController()
        {
            this.orderRepository = new OrderRepository();
        }

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

        public ActionResult Index(int id = 3) {


            Models.FoodCourtDBEntities db = new Models.FoodCourtDBEntities();
            Models.OrderModels.OrderViewModel2 model = new Models.OrderModels.OrderViewModel2();
            model.OrderView = db.Orders.Find(id);
                if (model.OrderView == null) {
                    return HttpNotFound();
                }
            //var itemList = (from o in db.OrderDetials
            //                where o.orderId == id
            //                select o).ToList();
            //ViewBag.ItemList = itemList;
            model.OrderDetailView = db.OrderDetials.Where(o => o.orderId == id).ToList();

            return View(model);
            


        }


    }
}