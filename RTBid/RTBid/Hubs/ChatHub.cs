using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace RTBid.Hubs
{
    [HubName("ChatHub")]
    public class ChatHub : Hub
    {


        public void clientTOs(string name, string message)
        {
            Clients.All.sTOclients(name, message);
        }
    }
}