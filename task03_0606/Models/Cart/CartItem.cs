using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace task03_0606.Models.Cart
{
    [Serializable]
    public class CartItem
    {
        public int produtctId { set; get; } 
        public string productName { set; get; }
        public decimal price { set; get; }
        public int quantity { set; get; }
        public decimal amount {
            get {
                return this.price * this.quantity ;
            }
        }
    }
}