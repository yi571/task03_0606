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

            ViewBag.Title = "註冊帳號";
            return View();
        }

        [HttpPost]
        public ActionResult EasyRegister(string LastName, string FirstName, string Password1, string ConfirmPassword, string cellPhone) {  //只需手機號碼的註冊
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
                    db.SaveChanges();
                    //try {
                    //    db.SaveChanges();
                    //} catch (Exception ex) {
                    //    throw;
                    //}
                    return RedirectToAction("Member", "Member");
                }
            }

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

        public ActionResult MemberTable(string UserInfoId) {  //會員列表

            if ((string)Session["identity"] != "superUser") {
                return RedirectToAction("Member", "Member");
            };
            var queryUserList = from o in db.UserInfoes
                                select o;
            ViewBag.userList = queryUserList.ToList();
            //ViewBag.testA = UserInfoId;
            if (Convert.ToInt32(UserInfoId) > 0) {
                ViewBag.sweetAlertMemberTabl = 1;
                int UserInfoIdNum = Convert.ToInt32(UserInfoId);
                var querySingleUser = (from o in db.UserInfoes
                                       where o.UserInfoId == UserInfoIdNum
                                       select o).First();
                ViewBag.SingleUser = querySingleUser;
                
                //ViewBag.testA = querySingleUser.phoneNum;
            } else {
                ViewBag.sweetAlertMemberTabl = 0;
            }

            

            return View();
        }

        public ActionResult EditPersonalData() {  //編輯使用者訊息
            ViewBag.Title = "編輯個人資訊";
            //return Content(Convert.ToInt32(Session["userInfoId"]).ToString());
            if (String.IsNullOrEmpty((string)Session["logState"])) {
                return RedirectToAction("Login", "Member"); //可能要重導回首頁
            }
            int idNum = Convert.ToInt32(Session["userInfoId"]);
            var queryEdit = from o in db.UserInfoes
                            join p in db.UserAddresses on o.phoneNum equals p.phoneNum into r //left join
                            from p in r.DefaultIfEmpty()  //如果右邊沒資料則傳出null
                            join q in db.streetNames on p.userAddressPart1 equals q.userAddressPart1 into s
                            from q in s.DefaultIfEmpty()
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



            return View();
        }
        [HttpPost]
        public ActionResult EditPersonalData(string LastName, string FirstName, string uid, string Email1, string cellPhone, string city, string district, string road, string lane, string alley, string addressNum, string addressF) {
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

            

            //查詢出AddressPart1
            var queryCheckAddressPart1 = from o in db.streetNames
                                         where (o.city == city & o.district == district & o.road == road)
                                         select o.userAddressPart1;
            //return Content(queryCheckAddressPart1.Count().ToString());
            //修改個人資料
            int idNum = Convert.ToInt32(Session["userInfoId"]);
            var queryUserIfo = (from o in db.UserInfoes
                                where o.UserInfoId == idNum
                                select o).FirstOrDefault();
            var queryUserAddressPhoneCheck = (from o in db.UserInfoes
                            join p in db.UserAddresses on o.phoneNum equals p.phoneNum into r //left join
                            from p in r.DefaultIfEmpty()  //如果右邊沒資料則傳出null
                            where o.UserInfoId == idNum
                            select new { p.phoneNum, o.lastName }).First();
           
            queryUserIfo.lastName = LastName;
            queryUserIfo.lastName = LastName;
            queryUserIfo.firstName = FirstName;
            queryUserIfo.userId = uid;
            queryUserIfo.email = Email1;

            if (queryCheckAddressPart1.Count() == 0) {
                return View();
            }
            //string x = "NOT OK";
            if (queryUserAddressPhoneCheck.phoneNum == null && queryUserIfo.phoneNum != null && queryCheckAddressPart1.Count() > 0) {
                UserAddress userAddress = new UserAddress() {
                    phoneNum = cellPhone,
                    userAddressPart1 = queryCheckAddressPart1.ToArray()[0x0],
                    lane = lane,
                    alley = alley,
                    addressNum = addressNum,
                    addressF = addressF

                };
                db.UserAddresses.Add(userAddress);
                

            } else {
                var queryUserAddress = (from o in db.UserAddresses
                                        where o.phoneNum == cellPhone
                                        select o).First();
                queryUserAddress.userAddressPart1 = queryCheckAddressPart1.ToArray()[0x0];
                queryUserAddress.lane = lane;
                queryUserAddress.alley = alley;
                queryUserAddress.addressNum = addressNum;
                queryUserAddress.addressF = addressF;
                
            }
            

            //if (queryUserAddress.phoneNum)
            




            db.SaveChanges();

            ViewBag.sweetAlert = 1;
            return View();
        }

        public ActionResult Detail() {
            return View();
        }
    }
}