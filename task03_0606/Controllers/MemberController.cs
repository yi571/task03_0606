using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using task03_0606.Models;
using System.Linq.Expressions;


namespace task03_0606.Controllers {
    public class MemberController : Controller {
        

        // GET: Member
        public ActionResult Index() {
            return View();
        }

        public ActionResult Member() {
           
            return View();
        }

        public ActionResult Login() {
           
            return View();
        }

        [HttpPost]
        public ActionResult Login(string phoneNum, string pwd) {  //傳入email和password
            
                return View();
            
        }

        public ActionResult Logout() {

            return View();
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