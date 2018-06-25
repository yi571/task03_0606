using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using task03_0606.Models;

namespace task03_0606.Controllers
{
    public class HomeController : Controller
    {
        FoodCourtDBEntities db = new FoodCourtDBEntities();
        public ActionResult Index()
        {
            return View();
        }
        
        // GET: Home
        public ActionResult Restaurent()
        {
            var restaurentList = (from r in db.Stores
                                  select r).ToList();
            return View(restaurentList);
        }

        public ActionResult RestaurentMemberPage() {
            //return Content(Request["storeId"]);
            string id = Request["storeId"].ToString();
            if (string.IsNullOrEmpty(id) != true)
            {
                var restaurent = (from r in db.Stores
                                  where r.storeId == id
                                  select r).Single();

                //ViewBag.message = "訊息";  //View : @ViewBag.message
                //ViewData["message"] = "訊息";  //View : @ViewData["message"]

                ViewBag.restaurentName = restaurent.storeName.ToString();
                ViewBag.restaurentDescription = restaurent.storeDescription.ToString();
                ViewBag.restaurentIcon = restaurent.UserInfo.userIcon.ToString();

                var menuList = (from m in db.Products
                                where m.Store.storeId == id
                                select m).ToList();


                return View(menuList);
            }
            else {

                return RedirectToAction("Restaurent");
            }
           

        }
        [HttpPost]
        public ActionResult RestaurentMemberPage(int itemId_post) {
            string i = itemId_post.ToString();
            //return Content(i);

            var query = from o in db.Products
                        where o.productID == 4
                        select o;
            Product item = query.Single();

           
            return Json(item, JsonRequestBehavior.AllowGet);
            //return Content(i);
            //return Json(true);
        }


    }
}