using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace task03_0606.Hubs {
    public class StoreHub : Hub {
        public void Send(string test) {
            // Call the addNewMessageToPage method to update clients.
            Clients.All.addNewMessageToPage(test);
        }
    }
}