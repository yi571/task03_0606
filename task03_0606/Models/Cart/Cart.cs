using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace task03_0606.Models.Cart
{
    [Serializable]
    public class Cart : IEnumerable<CartItem>
    {
        
        public Cart() {
            this.cartItemList = new List<CartItem>();
        }

        private List<CartItem> cartItemList;

        public decimal TotalAmount {
            get { 
            decimal totalAmount = 0;
            foreach (var cartItem in cartItemList) {
                    totalAmount += cartItem.amount;
            }
                return totalAmount;
            }  
        }

        public int Count {
            get {
                return this.cartItemList.Count;
            }
        }

        public bool AddProduct(int ProductID) {
            var findItem = this.cartItemList
                           .Where(s => s.produtctId == ProductID)
                           .Select(s => s)
                           .FirstOrDefault();

            if (findItem == default(Models.Cart.CartItem))
            {
                using (Models.FoodCourtDBEntities db = new FoodCourtDBEntities())
                {
                    var product = (from s in db.Products
                                   where s.productID == ProductID
                                   select s).FirstOrDefault();

                    if (product != default(Models.Product))
                    {
                        this.AddProduct(product);
                    }

                }

            }

            else { findItem.quantity += 1; }
            return true;
        }

        private bool AddProduct(Product product) {
            var cartItem = new Models.Cart.CartItem() {

                produtctId = product.productID,
                productName = product.productName,
                price = product.productPrice,
                quantity = 1,
            };
            this.cartItemList.Add(cartItem);
            return true;

        }

        public bool RemoveProduct(int ProductId) {
            var findItem = this.cartItemList
                            .Where(s => s.produtctId == ProductId)
                            .Select(s => s)
                            .FirstOrDefault();
            if (findItem == default(Models.Cart.CartItem))
            {

            }
            else { this.cartItemList.Remove(findItem); }
            return true;
        }




        public IEnumerator<CartItem> GetEnumerator()
        {
            return ((IEnumerable<CartItem>)cartItemList).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<CartItem>)cartItemList).GetEnumerator();
        }


        

    }

}