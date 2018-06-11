using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using task03_0606.Models;


namespace task03_0606.Controllers {
    public class MemberController : Controller {
        task03LabEntities db = new task03LabEntities();

        // GET: Member
        public ActionResult Index() {
            return View();
        }

        public ActionResult Member() {
            if (String.IsNullOrEmpty((string)Session["logState"])) {  //如未登入，則重導到登入頁面
                Session["lastPage"] = "/Member/Member";      //儲存最後頁面
                return RedirectToAction("Login", "Member");  //重導到登入頁面
            }
            return View();
        }

        public ActionResult Login() {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string pwd) {  //傳入email和password
            if (pwd == "1") {
                Session["identity"] = "superUser";
            }
            if (pwd == "2") {
                Session["identity"] = "storeUser";
            }
            if (pwd == "3") {
                Session["identity"] = "normalUser";
            }
            Session["logState"] = "login";    //將登入狀態設為登入，此處應和資料庫連結
            if (String.IsNullOrEmpty((string)Session["lastPage"])) {
                Session["lastPage"] = "/Member/Member";   //假如最後頁面值為空，則設為/Member/Member(此處應設為首頁)
            }
            return Redirect((string)Session["lastPage"]);  //重導回最後頁面
        }

        public ActionResult Logout() {
            Session["logState"] = "";   //登出則將logState設為空字串
            return RedirectToAction("Member", "Member");  //此處應重導回首頁
        }

        public ActionResult Register() {
            var queryCity = from o in db.streetNames
                            group o by o.city into g
                            select g.Key;

            ViewBag.city = queryCity.ToList();


            //var query = db.streetNames.GroupBy(g => g.city).Select(o => o.Key).ToList();
            //ViewData.Model = query;
            return View();
        }

        [HttpPost]
        public ActionResult Register(string city, string district, string road) {  //行政區選單控制

            var query = from o in db.streetNames
                        where o.city == city
                        group o by o.district into g
                        select g.Key;
            var queryCity = from o in db.streetNames
                            group o by o.city into g
                            select g.Key;
            var queryRoad = from o in db.streetNames
                            where (o.city == city & o.district == district)
                            group o by o.road into g
                            select g.Key;

            ViewBag.city = queryCity.ToList();
            ViewBag.cityValue = city;
            ViewBag.district = query.ToList();
            ViewBag.districtValue = district;
            ViewBag.road = queryRoad.ToList();
            ViewBag.roadValue = road;

            //var query = db.streetNames.GroupBy(g => g.city).Select(o => o.Key).ToList();
            //ViewData.Model = query;
            return View();
        }


        //[HttpPost]
        //public ActionResult Register(string city) {
        //    var query = from o in db.streetNames
        //                group o by o.district into g
        //                select g.Key;

        //    ViewData.Model = query.ToList();

        //    //var query = db.streetNames.GroupBy(g => g.city).Select(o => o.Key).ToList();
        //    //ViewData.Model = query;
        //    return View();
        //}

        public ActionResult ForgotPassword() {
            return View();
        }

        public ActionResult OrderTable() {
            if ((string)Session["identity"] != "superUser" || (string)Session["identity"] != "storeUser") {
                return RedirectToAction("Member", "Member");
            };
            return View();
        }

        public ActionResult MemberTable() {
            if ((string)Session["identity"] != "superUser") {
                return RedirectToAction("Member", "Member");
            };
            return View();
        }

        public ActionResult EditPersonalData() {
            if (String.IsNullOrEmpty((string)Session["logState"])) {
                return RedirectToAction("Login", "Member");
            }
            return View();
        }


    }
}