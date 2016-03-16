using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace RTBid.Hubs
{
    public class BiddingHub : Hub
    {
        public void send(string name, string message)
        {
            Clients.All.BroadcastMessage(name, message);
        }
    }
}