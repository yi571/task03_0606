using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using task03_0606.Models;

namespace task03_0606.Controllers
{
    public class MemberController : Controller
    {
        // 新增一個資料庫實體
        private task03UserDBEntities db = new task03UserDBEntities();


        // GET: Member
        public ActionResult Index()
        {
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
            //if(pwd == "1") {
            //    Session["identity"] = "superUser";
            //}
            //if (pwd == "2") {
            //    Session["identity"] = "storeUser";
            //}
            //if (pwd == "3") {
            //    Session["identity"] = "normalUser";
            //}
            //var query = from checkUser in db.members
            //            where 
            //var checkUser = db.members.ToList();
            var query = from o in db.members
                        where o.email == email & o.pwd == pwd
                        select o;

            if () {
                Session["identity"] = query.Single().userRank;
                Session["logState"] = "login";
                if (String.IsNullOrEmpty((string)Session["lastPage"])) {
                    Session["lastPage"] = "/Member/Member";
                }
                return Redirect(Session["lastPage"].ToString());
            }
            if (query.Single() == null) {
                return Content("null");
            }
            return View();


            //if (checkUser.All(e => e.email == email) && checkUser.All(e => e.pwd == pwd)) {
            //    Session["logState"] = "login";
            //    if (String.IsNullOrEmpty((string)Session["lastPage"])) {
            //        Session["lastPage"] = "/Member/Member";
            //    }
            //    return Redirect(Session["lastPage"].ToString());
            //}

            //return Content((checkUser.All(e => e.email == email)).ToString());

            //Session["logState"] = "login";    //將登入狀態設為登入，此處應和資料庫連結
            //if (String.IsNullOrEmpty((string)Session["lastPage"])) {
            //    Session["lastPage"] = "/Member/Member";   //假如最後頁面值為空，則設為/Member/Member(此處應設為首頁)
            //}
            //return Redirect((string)Session["lastPage"]);  //重導回最後頁面
        }

        public ActionResult Logout() {
            Session["logState"] = "";   //登出則將logState設為空字串
            return RedirectToAction("Member", "Member");  //此處應重導回首頁
        }

        public ActionResult Register() {
            return View();
        }
        [HttpPost]
        public ActionResult Register(string LastName, string FirstName, string uid, string Email1, string Password1, string ConfirmPassword, string cellPhone, string county, string district, string zipcode, string Address) {
            member member = new member() {
                lastName = LastName,
                firstName = FirstName,
                uid = uid,
                email = Email1,
                pwd = Password1,
                phoneNum = cellPhone,
                city = county,
                country = district,
                zip = zipcode,
                address = Address,
                userRank = "normalUser"
            };
            db.members.Add(member);
            db.SaveChanges();
            
            return RedirectToAction("Member", "Member");
        }

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
            return View(db.members.ToList());
        }

        public ActionResult EditPersonalData() {
            if (String.IsNullOrEmpty((string)Session["logState"])) {
                return RedirectToAction("Login", "Member");
            }
                return View();
        }


    }
}