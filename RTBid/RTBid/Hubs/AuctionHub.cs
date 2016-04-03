using System;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Web.UI;
using System.Threading;

namespace RTBid.Hubs
{
    [HubName("AuctionHub")]
    public class AuctionHub : Hub
    {
        #region receive
        public void sendChatMessage(int auctionId, string message, string userName)
        {
            if (message == null) { message = ""; }
            
            Clients.All.newChatMessage(auctionId, message, userName);
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
        public void tellClientsAboutNewBid(int auctionId, DateTime timeStamp, decimal currentAmount)
        {
            Clients.All.newBid(auctionId, timeStamp, currentAmount);
        }

        public void tellWinnerThatTheyWon(int auctionId, int userId)
        {
            Clients.All.winnerIs(auctionId, userId);
        }
        #endregion

        #region OnlieUsers
        //List<string> UsersList= new List<string>();

        //public void Join(string userName)
        //{
        //    //Check if the list has been chagned (new join or someone left), if Yes then broadcast the new list
        //    UsersList.Add(userName);
        //    Clients.All.onlineUsers(UsersList);
        //}
        #endregion

        #region pageStartUp
        //public void pageStartUpServer(int auctionId, DateTime ColseTime, decimal currentAmount)
        //{
        //    System.Threading.Timer t = new System.Threading.Timer(pageStartUp, null, 10000, 1000);

        //}

        //private void pageStartUp(object state)
        //{
        //    Clients.All.startUp(auctionId, ColseTime, currentAmount);
        //}
    
    #endregion
}
}