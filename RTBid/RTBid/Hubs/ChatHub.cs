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
        public void sendChatMessage(string message)
        {
            // take the message
            // grab the auction
            Clients.All.newChatMessage(message);
        }

        //        public void bidOnItem(int auctionId, decimal bidAmount)
        //        {

        //        }
    }
}