using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using RTBid.Core.Domain;
using Microsoft.AspNet.SignalR.Hubs;
using RTBid.Core.Repository;
using RTBid.Core.Infrastructure;
using System.Reflection;
using RTBid.Infrastructure;
using System.Web.UI;
using System.Threading;
using RTBid.Hubs;
using System.Web.Hosting;
using RTBid.Data.Infrastructure;

namespace RTBid.Hubs
{
    [HubName("AuctionHub")]
    public class AuctionHub : Hub
    {
        #region receive
        public void sendChatMessage(int auctionId, string message)
        {
            if (message == null) { message = ""; }
            
            Clients.All.newChatMessage(auctionId, message);
        }
        #endregion

        #region relay
        // receive from watchdog
        public void tellClientsThatAuctionStarted(int auctionId)
        {
            Clients.All.auctionStarted(auctionId);
        }
        
        // receive from watchdog
        public void tellClientsThatAuctionEnded(int auctionId)
        {
            Clients.All.auctionEnded(auctionId);
        }

        // receive from watchdog
        public void tellClientsAboutNewBid(DateTime timeStamp,decimal currentAmount)
        {
            Clients.All.newBid(timeStamp, currentAmount);
        }
        #endregion
    }
}