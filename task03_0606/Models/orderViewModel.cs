using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace task03_0606.Models
{

   

    public class orderDetailViewModel
    {
        public int OrderId { get; set; }
        public string OrderTime { get; set; }
        public int SeatID { get; set; }
        public string CustomerPhone { get; set; }
        public string FinshTime { get; set; }
        public int? MemberID { get; set; }
        public string MemberName { get; set; }
        public int? OrderCount { get; set; }
        public string Note { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int? UnitPrice { get; set; }

    }


    //public orderDetailViewModel getOrderDetail()
    //{
    //FoodCourtEntities db = new FoodCourtEntities();
    //    int sessionMember = Convert.ToInt32(Session["Member"]);
    //    var query = from o in db.OrderLists
    //                join c in db.OrderDetails on o.OrderId equals c.OrderId into ps
    //                from c in ps.DefaultIfEmpty()
    //                where string.IsNullOrEmpty(c.FinshTime) && c.MemberID == sessionMember
    //                select new orderDetailViewModel
    //                {
    //                    OrderId = c.OrderId,
    //                    OrderTime = o.OrderTime,
    //                    SeatID = o.SeatID,
    //                    CustomerPhone = o.CustomerPhone,
    //                    FinshTime = c.FinshTime,
    //                    MemberID = c.MemberID,
    //                    OrderCount = c.OrderCount,
    //                    Note = c.Note,
    //                    ProductID = c.ProductID,
    //                    ProductName = c.Product.ProductName,
    //                    UnitPrice = c.Product.UnitPrice,
    //                };
    //    List<orderDetailViewModel> ordersDetailList = query.ToList();
    //    return ordersDetailList
    //}




    public class orderSubmitViewModel
    {
        public int OrderId { get; set; }
        public string OrderTime { get; set; }
        public int SeatID { get; set; }
        public string CustomerPhone { get; set; }
        public string FinshTime { get; set; }
       
    }



}