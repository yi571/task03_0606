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
                            where c.productionStatus == 1 && c.Product.Store.storeId == stroeId && o.orderState == 1
                            select new OrderDetailViewModel {
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

                var orderId = query.First().orderId;
                var orderTime = query.First().orderTime;
                var customerPhone = query.First().customerPhone;
                
                var seatID = query.First().seatID;
                var productID = query.First().productID;
                var productName = query.First().productName;
                var productCount = query.First().productCount;
                var unitPrice = query.First().unitPrice;
                var customerNote = query.First().customerNote;
                var productionStatus = query.First().productionStatus;
                string newOrderData = String.Format("<tr><td data-th='接單時間：'>{0}</td><td data-th='客戶電話：'>{1}</td><td data-th='訂單單號：'>{2}</td><td data-th='座位：'>{3}</td><td data-th='產品編號：' style='display:none'>{4}</td><td data-th='產品編號：'>{5}</td><td data-th='產品名稱：'>{6}</td><td data-th='數量：'>{7}</td><td data-th='單價：'>{8}</td><td data-th='小計：'>{9}</td><td data-th='備註：'>{10}</td><td data-th='產品狀態：'>{11}</td><td> <button id='orderOk' type='button' class='btn btn-primary' onclick='foodDelivery({12}{13})>出餐</button> </td></tr>",
                    query.Last().orderTime, query.Last().customerPhone, query.Last().orderId, query.Last().seatID, query.Last().productID, query.Last().productID, query.Last().productName, query.Last().productName, query.Last().unitPrice, query.Last(), query.Last().unitPrice* query.Last().productCount, query.Last().customerNote, ((query.Last().productionStatus == 1)? "待準備" : "準備完成"), query.Last().productID, query.Last().orderId);
                Clients.All.addNewMessageToPage(test, orderId, orderTime, customerPhone, seatID, productID, productName, productCount, unitPrice, customerNote, productionStatus);
            }
        }
    }
}