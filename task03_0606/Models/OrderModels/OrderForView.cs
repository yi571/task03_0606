using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace task03_0606.Models.OrderModels
{
    public class OrderForView
    {
        public OrderForView()
        {
            this.orderDetail = new List<OrderDetial>();
        }

        private List<OrderDetial> orderDetail;
        private Order order;

        public int TotalAmount {
            get {
                int totalAmount = 0;
                foreach (var order in orderDetail) {

                    totalAmount += (order.productCount * order.Product.productPrice);
                }
                return totalAmount;
            }
        }
       
        public bool Get0rder(int orderId)
        {
            using (task03_0606.Models.FoodCourtDBEntities db = new FoodCourtDBEntities())
            {
                
                var getorder = (from o in db.Orders
                                  where o.orderId == orderId
                                  select o).FirstOrDefault();

                if (getorder != default(Models.Order))
                {
                    this.GetOrderDetail(getorder);
                }
                this.order = getorder;
               
                return true;
            }
        }

        private bool GetOrderDetail(Order getorder) {
            using (task03_0606.Models.FoodCourtDBEntities db = new FoodCourtDBEntities()) {
                var detailList = (from o in db.OrderDetials
                                  where o.orderId == getorder.orderId
                                  select o).ToList();
                this.orderDetail.AddRange(detailList);
                return true;
            }
        }
    }
}