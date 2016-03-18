using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace RTBid.Hubs
{
    public class HeartBeatHub : Hub
    {
        public void Heartbeat()
        {
            Clients.All.Heartbeat();
        }
    }
}