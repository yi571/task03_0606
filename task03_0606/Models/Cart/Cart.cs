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
                int totalQuantity = 0;
                foreach (var cartItem in cartItemList)
                {
                    totalQuantity += cartItem.quantity;
                }
                return totalQuantity;  
            }
        }
        //加入購物車
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
        //加入購物車
        private bool AddProduct(Product product) {
            var cartItem = new Models.Cart.CartItem() {

                produtctId = product.productID,
                productName = product.productName,
                price = product.productPrice,
                picture = product.productPicture,
                stoteName = product.Store.storeName,
                note = "",
                quantity = 1,
            };
            this.cartItemList.Add(cartItem);
            return true;
        }
        
        //刪除購物車商品
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
        
        //修改購物車數量
        public bool EditCartProductCount(int ItemId_edit, int quantity_edit )
        {
            var findItem = this.cartItemList
                          .Where(s => s.produtctId == ItemId_edit)
                          .Select(s => s)
                          .FirstOrDefault();

            if (findItem == default(Models.Cart.CartItem))
            {
                using (Models.FoodCourtDBEntities db = new FoodCourtDBEntities())
                {
                    var product = (from s in db.Products
                                   where s.productID == ItemId_edit
                                   select s).FirstOrDefault();

                    if (product != default(Models.Product))
                    {
                        this.AddProduct(product);
                    }

                }

            }

            else {
                findItem.quantity = quantity_edit;
            }
            return true;

        }

        //修改購物車備註
        public bool EditCartProductNote(int ItemId_edit, string ItemNoet_edit)
        {
            var findItem = this.cartItemList
                          .Where(s => s.produtctId == ItemId_edit)
                          .Select(s => s)
                          .FirstOrDefault();

            if (findItem == default(Models.Cart.CartItem))
            {
                using (Models.FoodCourtDBEntities db = new FoodCourtDBEntities())
                {
                    var product = (from s in db.Products
                                   where s.productID == ItemId_edit
                                   select s).FirstOrDefault();

                    if (product != default(Models.Product))
                    {
                        this.AddProduct(product);
                    }

                }

            }

            else
            {
                findItem.note = ItemNoet_edit;
            }
            return true;

        }

        //購物明細
        public List<OrderDetial>ToOrderDetail(int id) {
            var findItem = this.cartItemList;

            List<OrderDetial> newOrderDetail = new List<OrderDetial>();
 
            foreach (var item in findItem) {
                newOrderDetail.Add
                    (new OrderDetial()
                {
                    orderId = id,
                    productID = item.produtctId,
                    productCount = item.quantity,
                    productionStatus =0, //訂單產品準備狀態 1-準備中 2-準備ok
                    customerNote = item.note,
                });
                
                    }

            return newOrderDetail ;
        }

        //清空購物車
        public bool ClearCart() {
            this.cartItemList.RemoveAll(it => true);
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