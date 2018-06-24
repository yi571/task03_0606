using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using task03_0606.Models;
using Microsoft.AspNet.SignalR;

namespace task03_0606.Hubs {
    public class EditUserInfoHub : Hub {

        FoodCourtDBEntities db = new FoodCourtDBEntities();
        public void Hello() {
            Clients.All.hello();
        }

        public void EditUserInfo(string city, string district, string road) {
            var query = from o in db.streetNames //行政區
                        where o.city == city
                        group o by o.district into g
                        select g.Key;
            var cityInfo = query.ToList();
            Clients.Client(Context.ConnectionId).sendAddressInfo(cityInfo);
        }
    }
}