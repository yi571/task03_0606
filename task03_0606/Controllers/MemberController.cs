using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using task03_0606.Models;
using System.Linq.Expressions;


namespace task03_0606.Controllers {
    public class MemberController : Controller {

        FoodCourtDBEntities db = new FoodCourtDBEntities();
        // GET: Member
        public ActionResult Index() {
            return View();
        }

        public ActionResult Member() {
           
            return View();
        }

        public ActionResult Login() {
            ViewBag.Title = "登入";
            Session["UserAllName"] = "Guest";
            
            return View();
        }

        [HttpPost]
        public ActionResult Login(string phoneNum, string pwd) {  //傳入email和password
            ViewBag.Title = "登入";

            var queryLogin = from o in db.UserInfoes  //比對手機號碼和密碼
                             where (o.phoneNum == phoneNum && o.pwd == pwd)
                             select new { o.UserInfoId, o.lastName, o.firstName, o.userRank };
            var queryStoreLogin = from o in db.Stores //店家登入
                                  where (o.storePhone == phoneNum && o.storePwd == pwd)
                                  select new { o.storeId, o.storeName };
            if (queryLogin.Count() > 0) {
                Session["logState"] = "login";    //將登入狀態設為登入
                Session["identity"] = queryLogin.ToArray()[0].userRank.ToString();
                Session["userInfoId"] = queryLogin.ToArray()[0].UserInfoId;
                Session["UserAllName"] = queryLogin.ToArray()[0].lastName.ToString() + queryLogin.ToArray()[0].firstName.ToString();
                if (String.IsNullOrEmpty((string)Session["lastPage"])) {
                    Session["lastPage"] = "/Member/Member";   //假如最後頁面值為空，則設為/Member/Member(此處應設為首頁)
                }
                return Redirect((string)Session["lastPage"]);  //重導回最後頁面
            } else if(queryStoreLogin.Count() > 0) {
                Session["logState"] = "login";    //將登入狀態設為登入
                Session["identity"] = "store";
                Session["userInfoId"] = queryStoreLogin.ToArray()[0].storeId;
                Session["UserAllName"] = queryStoreLogin.ToArray()[0].storeName.ToString();
                if (String.IsNullOrEmpty((string)Session["lastPage"])) {
                    Session["lastPage"] = "/Member/Member";   //假如最後頁面值為空，則設為/Member/Member(此處應設為首頁)
                }
                return Redirect((string)Session["lastPage"]);  //重導回最後頁面
            } 
            else {
                ViewBag.loginError = "<div class='alert alert-danger' role='alert'><h2>帳號或密碼錯誤</h2></div>";
                return View();
            }
            
            
        }

        public ActionResult Logout() {
            Session.Clear();
            Session["logState"] = "";   //登出則將logState設為空字串
            Session["identity"] = "";
            return RedirectToAction("Member", "Member");  //此處應重導回首頁
        }

        public ActionResult EasyRegister() {  //只需手機號碼的註冊
           

            return View();
        }

        [HttpPost]
        public ActionResult EasyRegister(string LastName, string FirstName, string Password1, string ConfirmPassword, string cellPhone) {  //只需手機號碼的註冊
            

            return View();
        }

        public ActionResult Register() {
            

            return View();
        }

        [HttpPost]
        public ActionResult Register(string LastName, string FirstName, string birthday, string uid, string Email1, string Password1, string ConfirmPassword, string cellPhone, string city, string district, string road, string lane, string alley, string addressNum, string addressF) {  //行政區選單控制
           

            return View();
        }


        public ActionResult ForgotPassword() {  //忘記密碼
            return View();
        }

        public ActionResult OrderTable() {  //所有訂單
           
            return View();
        }

        public ActionResult CustemerOrder() {  //客戶所有訂單
            
            
            return View();
        }

        public ActionResult CustemerOrderDetial() {  //客戶訂單詳細資料
            

            return View();
        }

        public ActionResult OrderDetial() {  //訂單詳細資料
            
            return View();
        }

        public ActionResult OrderCard() {

            

            return View();
        }

        public ActionResult StoreTable() {
            
            return View();
        }

        public ActionResult StoreDetial() {
           
            return View();
        }

        public ActionResult AddStore() {
            
            return View();
        }

        public ActionResult MemberTable() {
           
            return View();
        }

        public ActionResult EditPersonalData() {  //編輯使用者訊息
            
            return View();
        }
        [HttpPost]
        public ActionResult EditPersonalData(string LastName, string FirstName, string uid, string Email1, string Password1, string ConfirmPassword, string cellPhone, string city, string district, string road, string lane, string alley, string addressNum, string addressF) {
            
            return View();
        }

        public ActionResult Detail() {
            return View();
        }
    }
}