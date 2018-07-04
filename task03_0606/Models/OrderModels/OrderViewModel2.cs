using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace task03_0606.Models.OrderModels
{
    public class OrderViewModel2
    {
        public Order OrderView { set; get; }

        public IEnumerable<OrderDetial> OrderDetailView { set; get; }
    }
}