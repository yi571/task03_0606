using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using task03_0606.Models;

namespace task03_0606.Hubs {
    public class StoreHub : Hub {
        public void Send(string test, string stroeId) {
            // Call the addNewMessageToPage method to update clients.


            //依結單時間=null &　廠商編號，篩選廠商訂單明細
            using (Models.FoodCourtDBEntities db = new FoodCourtDBEntities()) {
                var query = from o in db.Orders
                            join c in db.OrderDetials on o.orderId equals c.orderId into ps
                            from c in ps.DefaultIfEmpty()
                            where c.productionStatus == 1 && c.Product.Store.storeId == "21354423" && o.orderState == 1
                            select new {
                                orderId = o.orderId,
                                orderTime = o.orderDate,
                                seatID = o.tableId,
                                customerPhone = o.phoneNum,
                                storeID = c.Product.Store.storeId,
                                storeName = c.Product.Store.storeName,
                                productID = c.productID,
                                productName = c.Product.productName,
                                productCount = c.productCount,
                                unitPrice = c.Product.productPrice,
                                customerNote = c.customerNote,
                                productionStatus = c.productionStatus,
                            };

                var newOrderData = query.Last();
                Clients.All.addNewMessageToPage(newOrderData);
            }
        }
    }
}