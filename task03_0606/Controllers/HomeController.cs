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
        FoodCourtEntities db = new FoodCourtEntities();

        public ActionResult Index()
        {
            //if (Session["memberType"].ToString() == "Business") {
            //    return View("OrderBusiness");
            //}
            return View();
        }

        // GET: Home
        public ActionResult Restaurent()
        {
            return View();
        }

        public ActionResult RestaurentMemberPage() {
            
            int PageID = Convert.ToInt32(Request["MemberId"]);
            var query = from o in db.Members
                        where o.MemberID == PageID
                        select o;


            Member meb= query.Single();
            
            return View(meb);
        }
  
    }
}