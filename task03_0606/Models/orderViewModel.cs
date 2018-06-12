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

    public class orderSubmitViewModel
    {
        public int OrderId { get; set; }
        public string OrderTime { get; set; }
        public int SeatID { get; set; }
        public string CustomerPhone { get; set; }
        public string FinshTime { get; set; }
       
    }

}