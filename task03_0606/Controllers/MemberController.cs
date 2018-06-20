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
            ViewBag.Title = "儀錶板";
            if (String.IsNullOrEmpty((string)Session["logState"])) {  //如未登入，則重導到登入頁面
                Session["lastPage"] = "/Member/Member";      //儲存最後頁面
                return RedirectToAction("Login", "Member");  //重導到登入頁面
            }
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
            //if (pwd == "1") {
            //    Session["identity"] = "superUser";
            //}
            //if (pwd == "2") {
            //    Session["identity"] = "storeUser";
            //}
            //if (pwd == "3") {
            //    Session["identity"] = "normalUser";
            //}

            var queryLogin = from o in db.UserInfoes  //比對手機號碼和密碼
                             where (o.phoneNum == phoneNum && o.pwd == pwd)
                             select new { o.UserInfoId, o.lastName, o.firstName, o.userRank };
            if (queryLogin.Count() > 0) {
                Session["logState"] = "login";    //將登入狀態設為登入
                Session["identity"] = queryLogin.ToArray()[0].userRank.ToString();
                Session["userInfoId"] = queryLogin.ToArray()[0].UserInfoId;
                Session["UserAllName"] = queryLogin.ToArray()[0].lastName.ToString() + queryLogin.ToArray()[0].firstName.ToString();
                if (String.IsNullOrEmpty((string)Session["lastPage"])) {
                    Session["lastPage"] = "/Member/Member";   //假如最後頁面值為空，則設為/Member/Member(此處應設為首頁)
                }
                return Redirect((string)Session["lastPage"]);  //重導回最後頁面
            } else {
                ViewBag.loginError = "<div class='alert alert-danger' role='alert'><h2>帳號或密碼錯誤</h2></div>";
                return View();
            }
        }

        public ActionResult Logout() {
            Session.Clear();
            Session["logState"] = "";   //登出則將logState設為空字串
            return RedirectToAction("Member", "Member");  //此處應重導回首頁
        }

        public ActionResult EasyRegister() {  //只需手機號碼的註冊
            ViewBag.Title = "註冊帳號";

            return View();
        }

        [HttpPost]
        public ActionResult EasyRegister(string LastName, string FirstName, string Password1, string ConfirmPassword, string cellPhone) {  //只需手機號碼的註冊
            ViewBag.Title = "註冊帳號";
            //post時，回傳已填入值
            int checkNum = 0;  //用於檢查必填欄位是否填寫
            if (!string.IsNullOrEmpty(LastName)) { //傳姓氏值
                ViewBag.LastName = LastName;
                checkNum += 1;
            }

            if (!string.IsNullOrEmpty(FirstName)) { //傳名稱值
                ViewBag.FirstName = FirstName;
                checkNum += 1;
            }

            if (!string.IsNullOrEmpty(cellPhone)) { //傳cellPhone值
                ViewBag.cellPhone = cellPhone;
                checkNum += 1;
            }

            //檢查電話是否註冊----------------------------------
            var queryCheckPhone = from o in db.UserInfoes
                                  where o.phoneNum == cellPhone
                                  select cellPhone;
            ViewBag.phoneCheck = "";

            if (queryCheckPhone.Count() > 0) {
                ViewBag.phoneCheck = "號碼已註冊";
                ViewBag.html = "<i class='fa fa-times' style='color: red; '></i> 此號碼已註冊";
            }

            if (ViewBag.phoneCheck == "信箱已註冊") {
                return View();
            } else {
                if (Password1 == ConfirmPassword && checkNum == 3) {
                    UserInfo addMember = new UserInfo() {
                        lastName = LastName,
                        firstName = FirstName,
                        pwd = Password1,
                        phoneNum = cellPhone,
                        userRank = "normalUser"
                    };
                    db.UserInfoes.Add(addMember);
                    //db.SaveChanges();
                    try {
                        db.SaveChanges();
                    } 
                    catch (Exception ex){
                        throw;
                    }
                    return RedirectToAction("Member", "Member");
                }
            }


            return View();
        }

        public ActionResult Register() {
            ViewBag.Title = "註冊帳號";
            var queryCity = from o in db.streetNames //城市
                            group o by o.city into g
                            select g.Key;

            ViewBag.city = queryCity.ToList(); //傳城市list


            //var query = db.streetNames.GroupBy(g => g.city).Select(o => o.Key).ToList();
            //ViewData.Model = query;
            return View();
        }

        [HttpPost]
        public ActionResult Register(string LastName, string FirstName, string birthday, string uid, string Email1, string Password1, string ConfirmPassword, string cellPhone, string city, string district, string road, string lane, string alley, string addressNum, string addressF) {  //行政區選單控制
            ViewBag.Title = "註冊帳號";
            var query = from o in db.streetNames //行政區
                        where o.city == city
                        group o by o.district into g
                        select g.Key;
            var queryCity = from o in db.streetNames //城市
                            group o by o.city into g
                            select g.Key;
            var queryRoad = from o in db.streetNames //路
                            where (o.city == city & o.district == district)
                            group o by o.road into g
                            select g.Key;

            ViewBag.city = queryCity.ToList();  //傳城市list
            ViewBag.cityValue = city;           //傳城市值
            ViewBag.district = query.ToList();  //傳行政區list
            ViewBag.districtValue = district;   //傳行政區值
            ViewBag.road = queryRoad.ToList();  //傳路list
            ViewBag.roadValue = road;           //傳路值


            //post時，回傳已填入值
            int checkNum = 0;  //用於檢查必填欄位是否填寫
            if (!string.IsNullOrEmpty(LastName)) { //傳姓氏值
                ViewBag.LastName = LastName;
                checkNum += 1;
            }

            if (!string.IsNullOrEmpty(FirstName)) { //傳名稱值
                ViewBag.FirstName = FirstName;
                checkNum += 1;
            }

            if (!string.IsNullOrEmpty(birthday)) { //傳身分證值
                ViewBag.birthday = birthday;
                checkNum += 1;
            }

            if (!string.IsNullOrEmpty(uid)) { //傳身分證值
                ViewBag.uid = uid;
                checkNum += 1;
            }

            if (!string.IsNullOrEmpty(Email1)) { //傳Email1值
                ViewBag.Email1 = Email1;
                checkNum += 1;
            }

            if (!string.IsNullOrEmpty(cellPhone)) { //傳cellPhone值
                ViewBag.cellPhone = cellPhone;
                checkNum += 1;
            }

            if (!string.IsNullOrEmpty(lane)) { //傳lane值
                ViewBag.lane = lane;
            }

            if (!string.IsNullOrEmpty(alley)) { //傳alley值
                ViewBag.alley = alley;
            }

            if (!string.IsNullOrEmpty(addressNum)) { //傳addressNum值
                ViewBag.addressNum = addressNum;
                checkNum += 1;
            }

            if (!string.IsNullOrEmpty(addressF)) { //傳addressF值
                ViewBag.addressF = addressF;
            }

            if (!string.IsNullOrEmpty(Password1)) { //傳Password1值
                ViewBag.Password1 = Password1;
                checkNum += 1;
            }

            if (!string.IsNullOrEmpty(ConfirmPassword)) { //傳ConfirmPassword值
                ViewBag.ConfirmPassword = ConfirmPassword;
            }

            if (Password1 != ConfirmPassword) {
                return RedirectToAction("Register", "Member");
            }

            //string emailCheck = queryCheckEmail.ToArray()[0];

            //查詢出AddressPart1
            var queryCheckAddressPart1 = from o in db.streetNames
                                         where (o.city == city & o.district == district & o.road == road)
                                         select o.uid;

            //檢查信箱是否註冊----------------------------------
            var queryCheckEmail = from o in db.userInfoes
                                  where o.email == Email1
                                  select Email1;
            ViewBag.emailCheck = "";


            if (queryCheckEmail.Count() > 0) {
                ViewBag.emailCheck = "信箱已註冊";
                ViewBag.html = "<i class='fa fa-times' style='color: red; '></i> 此信箱已註冊";
            }

            //string catchEx = "";
            //try {
            //    queryCheckEmail.Single();
            //    ViewBag.emailCheck = "信箱已註冊";
            //} catch (Exception ex) {
            //    catchEx = ex.ToString();
            //};


            if (ViewBag.emailCheck == "信箱已註冊") {
                return View();
            } else {
                if (Password1 == ConfirmPassword && checkNum == 8) {
                    userInfo addMember = new userInfo() {
                        lastName = LastName,
                        firstName = FirstName,
                        userId = uid,
                        email = Email1,
                        pwd = Password1,
                        phoneNum = cellPhone,
                        userAddressPart1 = queryCheckAddressPart1.ToArray()[0x0],
                        lane = lane,
                        alley = alley,
                        addressNum = addressNum,
                        addressF = addressF,
                        userRank = "normalUser"
                    };
                    db.userInfoes.Add(addMember);
                    db.SaveChanges();
                    return RedirectToAction("Member", "Member");
                }
            }
            //--------------------------------------




            //var query = db.streetNames.GroupBy(g => g.city).Select(o => o.Key).ToList();
            //ViewData.Model = query;
            return View();
        }


        public ActionResult ForgotPassword() {  //忘記密碼
            return View();
        }

        public ActionResult OrderTable() {  //所有訂單
            Session["lastPage"] = "/Member/OrderTable";
            ViewBag.Title = "所有訂單";
            if (String.IsNullOrEmpty((string)Session["logState"])) {
                return RedirectToAction("Login", "Member");
            }
            if ((string)Session["identity"] != "superUser" && (string)Session["identity"] != "storeUser" && (string)Session["identity"] != "store") {
                return RedirectToAction("Member", "Member");
            };
            return View();
        }

        public ActionResult CustemerOrder() {  //客戶所有訂單
            Session["lastPage"] = "/Member/CustemerOrder";
            ViewBag.Title = "所有訂單";
            if (String.IsNullOrEmpty((string)Session["logState"])) {
                return RedirectToAction("Login", "Member");
            }
            
            return View();
        }

        public ActionResult CustemerOrderDetial() {  //客戶訂單詳細資料
            Session["lastPage"] = "/Member/CustemerOrderDetial";
            ViewBag.Title = "訂單詳細資料";
            if (String.IsNullOrEmpty((string)Session["logState"])) {
                return RedirectToAction("Login", "Member");
            }

            return View();
        }

        public ActionResult OrderDetial() {  //訂單詳細資料
            Session["lastPage"] = "/Member/OrderDetial";
            ViewBag.Title = "訂單詳細資料";
            if (String.IsNullOrEmpty((string)Session["logState"])) {
                return RedirectToAction("Login", "Member");
            }
            if ((string)Session["identity"] != "superUser" && (string)Session["identity"] != "storeUser" && (string)Session["identity"] != "store") {
                return RedirectToAction("Member", "Member");
            };
            return View();
        }

        public ActionResult OrderCard() {

            Session["lastPage"] = "/Member/OrderCard";
            ViewBag.Title = "新訂單";

            if (String.IsNullOrEmpty((string)Session["logState"])) {
                return RedirectToAction("Login", "Member");
            }

            if ((string)Session["identity"] != "superUser" && (string)Session["identity"] != "storeUser" && (string)Session["identity"] != "store") {
                return RedirectToAction("Member", "Member");
            };

            return View();
        }

        public ActionResult StoreTable() {
            Session["lastPage"] = "/Member/StoreTable";
            ViewBag.Title = "商家列表";
            if (String.IsNullOrEmpty((string)Session["logState"])) {
                return RedirectToAction("Login", "Member");
            }
            if ((string)Session["identity"] != "superUser" && (string)Session["identity"] != "storeUser") {
                return RedirectToAction("Member", "Member");
            };
            return View();
        }

        public ActionResult StoreDetial() {
            Session["lastPage"] = "/Member/StoreDetial";
            ViewBag.Title = "商家詳細資料";
            if (String.IsNullOrEmpty((string)Session["logState"])) {
                return RedirectToAction("Login", "Member");
            }
            if ((string)Session["identity"] != "superUser" && (string)Session["identity"] != "storeUser") {
                return RedirectToAction("Member", "Member");
            };
            return View();
        }

        public ActionResult AddStore() {
            Session["lastPage"] = "/Member/AddStore";
            ViewBag.Title = "新增商店";
            if (String.IsNullOrEmpty((string)Session["logState"])) {
                return RedirectToAction("Login", "Member");
            }
            if ((string)Session["identity"] != "superUser" && (string)Session["identity"] != "storeUser") {
                return RedirectToAction("Member", "Member");
            };
            return View();
        }

        public ActionResult MemberTable() {
            ViewBag.Title = "會員列表";
            if ((string)Session["identity"] != "superUser") {
                return RedirectToAction("Member", "Member");
            };
            var queryUserList = from o in db.userInfoes
                            join p in db.streetNames on o.userAddressPart1 equals p.uid
                            select new CUserList { Id = o.id, LastName = o.lastName, FirstName = o.firstName, UserId = o.userId, Email = o.email, PhoneNum = o.phoneNum, Pwd = o.pwd, City = p.city, District = p.district, Road = p.road, Lane = o.lane, Alley = o.alley, AddressNum = o.addressNum, AddressF = o.addressF, UserRank = o.userRank };
            ViewBag.userList = queryUserList.ToList();
            List<CUserList> userLists = queryUserList.ToList();
            


            return View();
        }

        public ActionResult EditPersonalData() {  //編輯使用者訊息
            ViewBag.Title = "編輯個人資訊";
            if (String.IsNullOrEmpty((string)Session["logState"])) {
                return RedirectToAction("Login", "Member");
            }
            int idNum = Convert.ToInt32(Session["userInfoId"]);
            var queryEdit = from o in db.UserInfoes
                            join p in db.UserAddresses on o.phoneNum equals p.phoneNum
                            join q in db.streetNames on p.userAddressPart1 equals q.userAddressPart1
                            where o.UserInfoId == idNum
                            select new { o.lastName, o.firstName, o.userId, o.email, o.phoneNum, o.pwd, q.city, q.district, q.road, p.lane, p.alley, p.addressNum, p.addressF };
            var userInfoEdit = queryEdit.ToArray();

            ViewBag.LastName = userInfoEdit[0].lastName;
            ViewBag.FirstName = userInfoEdit[0].firstName;
            ViewBag.uid = userInfoEdit[0].userId;
            ViewBag.Email1 = userInfoEdit[0].email;
            ViewBag.cellPhone = userInfoEdit[0].phoneNum;
            ViewBag.Password1 = userInfoEdit[0].pwd;
            ViewBag.ConfirmPassword = userInfoEdit[0].pwd;


            //地址選單
            var queryCity = from o in db.streetNames //城市
                            group o by o.city into g
                            select g.Key;

            ViewBag.city = queryCity.ToList(); //傳城市list

            string cityStr = userInfoEdit[0].city;
            string districtStr = userInfoEdit[0].district;
            var query = from o in db.streetNames //行政區
                        where o.city == cityStr
                        group o by o.district into g
                        select g.Key;

            var queryRoad = from o in db.streetNames //路
                            where (o.city == cityStr & o.district == districtStr)
                            group o by o.road into g
                            select g.Key;

            ViewBag.city = queryCity.ToList();  //傳城市list
            ViewBag.cityValue = userInfoEdit[0].city;           //傳城市值
            ViewBag.district = query.ToList();  //傳行政區list
            ViewBag.districtValue = userInfoEdit[0].district;   //傳行政區值
            ViewBag.road = queryRoad.ToList();  //傳路list
            ViewBag.roadValue = userInfoEdit[0].road;           //傳路值
            ViewBag.lane = userInfoEdit[0].lane;
            ViewBag.alley = userInfoEdit[0].alley;
            ViewBag.addressNum = userInfoEdit[0].addressNum;
            ViewBag.addressF = userInfoEdit[0].addressF;

            //return Content(userInfoEdit[0].city.ToString());
            return View();
        }
        [HttpPost]
        public ActionResult EditPersonalData(string LastName, string FirstName, string uid, string Email1, string Password1, string ConfirmPassword, string cellPhone, string city, string district, string road, string lane, string alley, string addressNum, string addressF) {
            ViewBag.Title = "編輯個人資訊";
            if (String.IsNullOrEmpty((string)Session["logState"])) {
                return RedirectToAction("Login", "Member");
            }
            var query = from o in db.streetNames //行政區
                        where o.city == city
                        group o by o.district into g
                        select g.Key;
            var queryCity = from o in db.streetNames //城市
                            group o by o.city into g
                            select g.Key;
            var queryRoad = from o in db.streetNames //路
                            where (o.city == city & o.district == district)
                            group o by o.road into g
                            select g.Key;

            ViewBag.city = queryCity.ToList();  //傳城市list
            ViewBag.cityValue = city;           //傳城市值
            ViewBag.district = query.ToList();  //傳行政區list
            ViewBag.districtValue = district;   //傳行政區值
            ViewBag.road = queryRoad.ToList();  //傳路list
            ViewBag.roadValue = road;           //傳路值


            //post時，回傳已填入值
            int checkNum = 0;  //用於檢查必填欄位是否填寫
            if (!string.IsNullOrEmpty(LastName)) { //傳姓氏值
                ViewBag.LastName = LastName;
                checkNum += 1;
            }

            if (!string.IsNullOrEmpty(FirstName)) { //傳名稱值
                ViewBag.FirstName = FirstName;
                checkNum += 1;
            }

            if (!string.IsNullOrEmpty(uid)) { //傳名稱值
                ViewBag.uid = uid;
                checkNum += 1;
            }

            if (!string.IsNullOrEmpty(Email1)) { //傳Email1值
                ViewBag.Email1 = Email1;
                checkNum += 1;
            }

            if (!string.IsNullOrEmpty(cellPhone)) { //傳cellPhone值
                ViewBag.cellPhone = cellPhone;
                checkNum += 1;
            }

            if (!string.IsNullOrEmpty(lane)) { //傳lane值
                ViewBag.lane = lane;
            }

            if (!string.IsNullOrEmpty(alley)) { //傳alley值
                ViewBag.alley = alley;
            }

            if (!string.IsNullOrEmpty(addressNum)) { //傳addressNum值
                ViewBag.addressNum = addressNum;
                checkNum += 1;
            }

            if (!string.IsNullOrEmpty(addressF)) { //傳addressF值
                ViewBag.addressF = addressF;
            }

            if (!string.IsNullOrEmpty(Password1)) { //傳Password1值
                ViewBag.Password1 = Password1;
                checkNum += 1;
            }

            if (!string.IsNullOrEmpty(ConfirmPassword)) { //傳ConfirmPassword值
                ViewBag.ConfirmPassword = ConfirmPassword;
            }

            if (Password1 != ConfirmPassword) {
                return RedirectToAction("Register", "Member");
            }


            //查詢出AddressPart1
            //var queryCheckAddressPart1 = from o in db.streetNames
            //                             where (o.city == city & o.district == district & o.road == road)
            //                             select o.uid;
            var queryCheckAddressPart1 = from o in db.UserAddresses
                                         join p in db.streetNames on o.userAddressPart1 equals p.userAddressPart1
                                         where (p.city == city & p.district == district & p.road == road)
                                         select o.userAddressPart1;

            //檢查信箱是否註冊----------------------------------
            var queryCheckEmail = from o in db.UserInfoes
                                  where o.email == Email1
                                  select o;
            var queryEmailNotChange = from o in db.UserInfoes
                                      where (o.email == Email1 & o.pwd == Password1)
                                      select o;
            ViewBag.emailCheck = "";
            string checkEmailStr = "";
            try {
                checkEmailStr = queryEmailNotChange.First().email.ToString();
            } catch {

            };
            

            if (Email1.ToString() != checkEmailStr) {
                if (queryCheckEmail.Count() > 0) {
                    ViewBag.emailCheck = "信箱已註冊";
                    ViewBag.html = "<i class='fa fa-times' style='color: red; '></i> 此信箱已註冊";
                    return View();
                }
                return View();
            }



            if (ViewBag.emailCheck == "信箱已註冊") {
                return View();
            } else {
                if (Password1 == ConfirmPassword && checkNum == 7) {
                   
                    UserInfo addMember = new UserInfo() {
                        lastName = LastName,
                        firstName = FirstName,
                        userId = uid,
                        email = Email1,
                        pwd = Password1,
                        phoneNum = cellPhone,
                        userAddressPart1 = queryCheckAddressPart1.ToArray()[0x0],
                        lane = lane,
                        alley = alley,
                        addressNum = addressNum,
                        addressF = addressF,
                        userRank = "normalUser"
                    };
                    int idNum = Convert.ToInt32(Session["userInfoId"]);
                    var queryUserIfo = (from o in db.userInfoes
                                        where o.id == idNum
                                        select o).First();

                    queryUserIfo.lastName = LastName;
                    queryUserIfo.lastName = LastName;
                    queryUserIfo.firstName = FirstName;
                    queryUserIfo.userId = uid;
                    queryUserIfo.email = Email1;
                    queryUserIfo.pwd = Password1;
                    queryUserIfo.phoneNum = cellPhone;
                    queryUserIfo.userAddressPart1 = queryCheckAddressPart1.ToArray()[0x0];
                    queryUserIfo.lane = lane;
                    queryUserIfo.alley = alley;
                    queryUserIfo.addressNum = addressNum;
                    queryUserIfo.addressF = addressF;

                    try {
                        db.SaveChanges();
                    } catch {
                        
                    };
                    //return Content(checkEmailStr);
                    return View();
                }
            }
            //--------------------------------------


            //return Content(checkEmailStr);
            return View();
        }

        public ActionResult Detail() {
            return View();
        }
    }
}