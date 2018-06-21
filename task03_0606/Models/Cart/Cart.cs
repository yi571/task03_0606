using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace task03_0606.Models.Cart
{
    [Serializable]
    public class Cart
    {
        public Cart() {
            this.cartItemList = new List<CartItem>();
        }

        public List<CartItem> cartItemList;

        public decimal TotalAmount {
            get { 
            decimal totalAmount = 0;
            foreach (var cartItem in cartItemList) {
                    totalAmount += cartItem.amount;
            }
                return totalAmount;
            }
        }
    }
}