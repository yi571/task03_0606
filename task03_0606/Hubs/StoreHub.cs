using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using task03_0606.Models;

namespace task03_0606.Hubs {
    public class StoreHub : Hub {

        public static List<CStoreSingalr> signalrsList = new List<CStoreSingalr>();
        
        ////資料庫
        //FoodCourtDBEntities db = new FoodCourtDBEntities();
        public void Send(string test, string storeId) {
            //    // Call the addNewMessageToPage method to update clients.
            //    var query = (from o in db.Stores
            //                 where o.storeId == storeId
            //                 select o).First();
            //    query.storeSignalR = Context.ConnectionId;
            //    db.SaveChanges();
            //Clients.All.addNewMessageToPage(Context.ConnectionId, storeId);

            //靜態List
            var queryUser = signalrsList.SingleOrDefault(u => u.StoreId == storeId);
            if(queryUser == null) {
                queryUser = new CStoreSingalr() {
                    ContextID = Context.ConnectionId,
                    StoreId = storeId
                };
                signalrsList.Add(queryUser);
            } else {
                queryUser.ContextID = Context.ConnectionId;
            }
        }
    }
}