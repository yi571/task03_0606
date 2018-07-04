using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using task03_0606.Models;
using System.Linq.Expressions;
using System.IO;

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
                             where (o.phoneNum == phoneNum && o.pwd == pwd && o.userState == 1)
                             select new { o.UserInfoId, o.lastName, o.firstName, o.userRank, o.phoneNum };
            var queryStoreLogin = from o in db.Stores //店家登入
                                  where (o.storeId == phoneNum && o.storePwd == pwd && o.storeState == 1)
                                  select new { o.storeId, o.storeName };
            if (queryLogin.Count() > 0) {
                Session["logState"] = "login";    //將登入狀態設為登入
                Session["identity"] = queryLogin.ToArray()[0].userRank.ToString();
                Session["userInfoId"] = queryLogin.ToArray()[0].UserInfoId;
                Session["userPhone"] = queryLogin.ToArray()[0].phoneNum;
                Session["UserAllName"] = queryLogin.ToArray()[0].lastName.ToString() + queryLogin.ToArray()[0].firstName.ToString();
                if (String.IsNullOrEmpty((string)Session["lastPage"])) {
                    if (Session["identity"].ToString() == "superUser") {  //假如是網站管理者，則回Member，如果不是則回顧客首頁
                        Session["lastPage"] = "/Member/StoreTable";   //假如最後頁面值為空，則設為/Member/Member(此處應設為首頁)
                    } else {
                        Session["lastPage"] = "/Products/Index";
                    };
                   
                }
                return Redirect((string)Session["lastPage"]);  //重導回最後頁面
            } else if (queryStoreLogin.Count() > 0) {
                Session["logState"] = "login";    //將登入狀態設為登入
                Session["identity"] = "store";
                Session["userInfoId"] = queryStoreLogin.ToArray()[0].storeId;
                Session["storeId"] = queryStoreLogin.ToArray()[0].storeId;
                Session["UserAllName"] = queryStoreLogin.ToArray()[0].storeName.ToString();
                if (String.IsNullOrEmpty((string)Session["lastPage"])) {
                    Session["lastPage"] = "/Products/Index";   //假如最後頁面值為空，則設為/Member/Member(此處應設為首頁)
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
            Session["identity"] = "Guest";
            return RedirectToAction("Index", "Products");  //此處應重導回首頁
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
                        userRank = "normalUser",
                        userState = 1
                    };
                    db.UserInfoes.Add(addMember);
                    db.SaveChanges();
                    //try {
                    //    db.SaveChanges();
                    //} catch (Exception ex) {
                    //    throw;
                    //}
                    return RedirectToAction("Login", "Member");
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

        //商店列表
        public ActionResult StoreTable(string storeId) {
            if ((string)Session["identity"] != "superUser") {
                return RedirectToAction("Member", "Member");
            };

            var queryStore = from o in db.Stores
                             where o.storeState == 1
                             select o;
            ViewBag.storeList = queryStore;
            ViewBag.Test = "未設定";
            if (!string.IsNullOrEmpty(storeId)) {
                ViewBag.sweetAlertStoreTabl = 1;
                var queryStoreDetial = (from o in db.Stores
                                        join p in db.UserInfoes on o.ownerPhoneNum equals p.phoneNum into q
                                        from p in q.DefaultIfEmpty()
                                        where o.storeId == storeId
                                        select new { o.storeId, o.storeName, o.storePhone, o.ownerPhoneNum, p.lastName, p.firstName }).First();
                ViewBag.storeId = queryStoreDetial.storeId;
                ViewBag.storeName = queryStoreDetial.storeName;
                ViewBag.storePhone = queryStoreDetial.storePhone;
                ViewBag.ownerPhoneNum = queryStoreDetial.ownerPhoneNum;
                ViewBag.lastName = queryStoreDetial.lastName;
                ViewBag.firstName = queryStoreDetial.firstName;


            } else {
                ViewBag.sweetAlertStoreTabl = 0;
            };
            return View();
        }

        public ActionResult StoreDetial() {
            if ((string)Session["identity"] != "superUser") {
                return RedirectToAction("Member", "Member");
            };
            return View();
        }

        //移除商店
        public ActionResult DeleteStore(string storeId) {  
            if ((string)Session["identity"] != "superUser") {
                return RedirectToAction("Member", "Member");
            };

            var queryChangeStoreState = (from o in db.Stores
                                        where o.storeId == storeId
                                        select o).First();

            queryChangeStoreState.storeState = 0;
            db.SaveChanges();

            return RedirectToAction("StoreTable", "Member");
        }

        public ActionResult AddStore() {
            if ((string)Session["identity"] != "superUser") {
                return RedirectToAction("Member", "Member");
            };
            return View();
        }

        //新增商店
        [HttpPost]
        public ActionResult AddStore(string storeName, string storePhone, string storeId, string Password1, string ConfirmPassword, string cellPhone, string storeDescription) {
            var querystoreId = from o in db.Stores
                               where o.storeId == storeId
                               select o;
            //post時，回傳已填入值
            int checkNum = 0;  //用於檢查必填欄位是否填寫
            if (!string.IsNullOrEmpty(storeName)) {
                ViewBag.storeName = storeName;
                checkNum += 1;
            }
            if (!string.IsNullOrEmpty(storePhone)) {
                ViewBag.storePhone = storePhone;
                checkNum += 1;
            }
            if (!string.IsNullOrEmpty(storeId)) {
                ViewBag.storeId = storeId;
                checkNum += 1;
            }
            if (!string.IsNullOrEmpty(Password1)) {
                ViewBag.Password1 = Password1;
                checkNum += 1;
            }
            if (!string.IsNullOrEmpty(ConfirmPassword)) {
                ViewBag.ConfirmPassword = ConfirmPassword;
                checkNum += 1;
            }
            if (!string.IsNullOrEmpty(cellPhone)) {
                ViewBag.cellPhone = cellPhone;
                checkNum += 1;
            }
            if (!string.IsNullOrEmpty(storeDescription)) {
                ViewBag.storeDescription = storeDescription;
                checkNum += 1;
            }
            if (querystoreId.Count() > 0) {
                ViewBag.storeIdCheck = "<i class='fa fa-times' style='color: red; '></i> 統一編號已註冊";
            } else {
                ViewBag.storeIdCheck = "";
                if (Password1 == ConfirmPassword && checkNum == 7) {
                    Store addStore = new Store() {
                        storeId = storeId,
                        storeName = storeName,
                        storePhone = storePhone,
                        storePwd = Password1,
                        storeDescription = storeDescription,
                        ownerPhoneNum = cellPhone,
                        storeState = 1
                    };
                    db.Stores.Add(addStore);
                    db.SaveChanges();
                    return RedirectToAction("storeTable", "Member");
                };
            };

            return View();
        }

        public ActionResult EditStore() {
            if ((string)Session["identity"] != "store") {
                return RedirectToAction("Member", "Member");
            };
            string userInfoId = Session["userInfoId"].ToString();
            var queryEditStore = (from o in db.Stores
                                  where o.storeId == userInfoId
                                  select o).First();
            ViewBag.storeName = queryEditStore.storeName;
            ViewBag.storePhone = queryEditStore.storePhone;
            ViewBag.storeId = queryEditStore.storeId;
            ViewBag.Password1 = queryEditStore.storePwd;
            ViewBag.ConfirmPassword = queryEditStore.storePwd;
            ViewBag.cellPhone = queryEditStore.ownerPhoneNum;
            ViewBag.storeDescription = queryEditStore.storeDescription;

            return View();
        }

        //post編輯商店
        [HttpPost]
        public ActionResult EditStore(string storeName, string storePhone, string storeId, string Password1, string ConfirmPassword, string cellPhone, string storeDescription) {
            if ((string)Session["identity"] != "store") {
                return RedirectToAction("Member", "Member");
            };
            ViewBag.sweetAlert = 0;
            ViewBag.storeName = storeName;
            ViewBag.storePhone = storePhone;
            ViewBag.storeId = storeId;
            ViewBag.Password1 = Password1;
            ViewBag.ConfirmPassword = ConfirmPassword;
            ViewBag.cellPhone = cellPhone;
            ViewBag.storeDescription = storeDescription;

            int checkNum = 0;  //用於檢查必填欄位是否填寫
            if (!string.IsNullOrEmpty(storeName)) {
                ViewBag.storeName = storeName;
                checkNum += 1;
            }
            if (!string.IsNullOrEmpty(storePhone)) {
                ViewBag.storePhone = storePhone;
                checkNum += 1;
            }
            if (!string.IsNullOrEmpty(Password1)) {
                ViewBag.Password1 = Password1;
                checkNum += 1;
            }
            if (!string.IsNullOrEmpty(ConfirmPassword)) {
                ViewBag.ConfirmPassword = ConfirmPassword;
                checkNum += 1;
            }
            if (!string.IsNullOrEmpty(cellPhone)) {
                ViewBag.cellPhone = cellPhone;
                checkNum += 1;
            }
            if (!string.IsNullOrEmpty(storeDescription)) {
                ViewBag.storeDescription = storeDescription;
                checkNum += 1;
            }
            if (Password1 == ConfirmPassword && checkNum == 6) {
                string userInfoId = Session["userInfoId"].ToString();
                var queryUpdateStore = (from o in db.Stores
                                       where o.storeId == userInfoId
                                       select o).First();
                queryUpdateStore.storeName = storeName;
                queryUpdateStore.storePhone = storePhone;
                queryUpdateStore.storePwd = Password1;
                queryUpdateStore.storeDescription = storeDescription;
                queryUpdateStore.ownerPhoneNum = cellPhone;
                db.SaveChanges();
                ViewBag.sweetAlert = 1;
            }

            return View();

        }

        public ActionResult MemberTable(string UserInfoId) {  //會員列表

            if ((string)Session["identity"] != "superUser") {
                return RedirectToAction("Member", "Member");
            };
            var queryUserList = from o in db.UserInfoes
                                where o.userState == 1
                                select o;
            ViewBag.userList = queryUserList.ToList();
            ViewBag.testA = UserInfoId;  //測試用
            if (Convert.ToInt32(UserInfoId) > 0) {
                ViewBag.sweetAlertMemberTabl = 1;
                int UserInfoIdNum = Convert.ToInt32(UserInfoId);
                //var querySingleUser = (from o in db.UserInfoes
                //                       where o.UserInfoId == UserInfoIdNum
                //                       select o).First();
                var querySingleUser = (from o in db.UserInfoes
                                       join p in db.UserAddresses on o.phoneNum equals p.phoneNum into r //left join
                                       from p in r.DefaultIfEmpty()  //如果右邊沒資料則傳出null
                                       join q in db.streetNames on p.userAddressPart1 equals q.userAddressPart1 into s
                                       from q in s.DefaultIfEmpty()
                                       where o.UserInfoId == UserInfoIdNum
                                       select new { o.lastName, o.firstName, o.userId, o.email, o.phoneNum, o.pwd, q.city, q.district, q.road, p.lane, p.alley, p.addressNum, p.addressF, o.userRank }
                                ).First();

                ViewBag.lastName = querySingleUser.lastName;
                ViewBag.firstName = querySingleUser.firstName;
                ViewBag.userId = querySingleUser.userId;
                ViewBag.email = querySingleUser.email;
                ViewBag.phoneNum = querySingleUser.phoneNum;
                ViewBag.userRank = querySingleUser.userRank;
                ViewBag.city = querySingleUser.city;
                ViewBag.district = querySingleUser.district;
                ViewBag.road = querySingleUser.road;
                ViewBag.lane = querySingleUser.lane;
                ViewBag.alley = querySingleUser.alley;
                ViewBag.addressNum = querySingleUser.addressNum;
                ViewBag.addressF = querySingleUser.addressF;


                //querySingleUser.;

                //ViewBag.testA = querySingleUser.phoneNum;
            } else {

                ViewBag.sweetAlertMemberTabl = 0;

                var querySingleUser = (from o in db.UserInfoes
                                       where o.UserInfoId == 3
                                       select o).First();
                ViewBag.SingleUser = querySingleUser;
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
                            where (o.city == cityStr && o.district == districtStr)
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

            ViewBag.sweetAlert = 0;

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

            ViewBag.sweetAlert = "swal({type: 'success', title: '個人資料已修改！', showConfirmButton: false, timer: 1500});";
            return View();
        }

        public ActionResult Detail() {
            return View();
        }

        public ActionResult StoreChart(string polit1) {
            ViewBag.today = DateTime.Today.ToString("yyyy-MM-dd");
            string getDate = polit1;
            if (!string.IsNullOrEmpty(polit1)) {

                ViewBag.today = getDate;
                polit1 = polit1.Replace("-0", "/").Replace("-", "/");

            };

            if (String.IsNullOrEmpty((string)Session["logState"])) {  //如未登入，則重導到登入頁面
                Session["lastPage"] = "/Member/StoreChart";      //儲存最後頁面
                return RedirectToAction("Login", "Member");  //重導到登入頁面
            }
            if ((string)Session["identity"] != "store") {
                return Content("您沒有權限瀏覽此頁");
            }

            if (string.IsNullOrEmpty(polit1)) {
                polit1 = DateTime.Today.ToString("YYYY/MM/DD");
            }
            string storeId = (string)Session["storeId"];
            var queryEdit = from o in db.Orders
                            join p in db.OrderDetials on o.orderId equals p.orderId into r //left join
                            from p in r.DefaultIfEmpty()  //如果右邊沒資料則傳出null
                            join q in db.Products on p.productID equals q.productID into s
                            from q in s.DefaultIfEmpty()
                            where o.orderDate.StartsWith(polit1) && q.storeId == storeId
                            group q by q.productName into g
                            select new CDaySell { Name = g.Key, DayCount = g.Count() };
            var userInfoEdit = queryEdit.ToList();

            ViewBag.test = userInfoEdit;



            //return Content(DateTime.Today.ToString("yyyy-MM-dd"));
            return View();
        }

        public ActionResult AddNewProduct() {
            if (String.IsNullOrEmpty((string)Session["logState"])) {  //如未登入，則重導到登入頁面
                Session["lastPage"] = "/Member/AddNewProduct";      //儲存最後頁面
                return RedirectToAction("Login", "Member");  //重導到登入頁面
            }
            var query = from o in db.Categories
                        select o;
            
            ViewBag.kind = query;
            return View();
        }

        [HttpPost]
        public ActionResult AddNewProduct(HttpPostedFileBase file, string productName, string productKind, string productDescription, string productPrice, string productIdSelf) {
            if (file.ContentLength > 0) {

                var fileName = Path.GetFileName(file.FileName);
                string fileName2 = DateTime.Now.ToString("yyyyMMddhhmmss") + fileName;
                var path = Path.Combine(Server.MapPath("~/photo"), fileName2);

                file.SaveAs(path);

                Product product = new Product {
                    productName = productName,
                    productPicture = fileName2,
                    storeId = Session["storeId"].ToString(),
                    storeProductId = productKind,
                    productDescription = productDescription,
                    categoryID = Convert.ToInt32(productKind),
                    productState = 1,
                    productPrice = Convert.ToInt32(productPrice)

                };
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("ManagerIndex", "Products");
            }

            
            return View();
        }
    }
}