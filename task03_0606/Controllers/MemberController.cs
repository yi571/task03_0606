using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace task03_0606.Controllers
{
    public class MemberController : Controller
    {
        // GET: Member
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Member() {
            //if (String.IsNullOrEmpty((string)Session["logState"])) {
            //    Session["lastPage"] = "/Member/Member";
            //    return RedirectToAction("Login", "Member");
            //}
                return View();
        }

        public ActionResult Login() {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string pwd) {
            Session["logState"] = "login";
            if (String.IsNullOrEmpty((string)Session["lastPage"])) {
                Session["lastPage"] = "/Member/Member";
            }
            return Redirect((string)Session["lastPage"]);
        }

        public ActionResult Logout() {
            Session["logState"] = "";
            return RedirectToAction("Member", "Member");
        }

        public ActionResult Register() {
            return View();
        }

        public ActionResult ForgotPassword() {
            return View();
        }

        
    }
}