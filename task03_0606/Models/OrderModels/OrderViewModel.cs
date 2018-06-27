using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace task03_0606.Models
{
    
    public class OrderDetailViewModel
    {
        public int orderId { get; set; }
        public string orderTime { get; set; }
        public int seatID { get; set; }
        public string customerPhone { get; set; }
        
        public string storeID { get; set; }
        public string storeName { get; set; }
        
        public int productID { get; set; }
        public string productPicture { get; set;}
        public string productName { get; set; }
        public int productCount { get; set; }
        public int? unitPrice { get; set; }
        public string customerNote { get; set; }
        public int productionStatus { get; set; }
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






}