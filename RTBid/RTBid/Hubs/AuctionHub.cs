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

namespace RTBid.Hubs
{
    [HubName("AuctionHub")]
    public class AuctionHub : Hub
    {
        private System.Threading.Timer _timer2;
        public HashSet<Auction> AuctionsInMemory = new HashSet<Auction>();

        #region dependeny Injection
        private readonly IAuctionRepository _auctionRepository;
        private readonly IUnitOfWork _unitOfWork;

        //public AuctionHub(IAuctionRepository auctionRepository, IUnitOfWork unitOfWork)
        //{
        //    _auctionRepository = auctionRepository;
        //    _unitOfWork = unitOfWork;
        //}
        #endregion

        #region receive
        public void sendChatMessage(int auctionId, string message)
        {
            if (message == null) { message = ""; }
            // take the message
            // grab the auction
            Clients.All.newChatMessage(auctionId, message);
        }

        public void bidOnItem(int auctionId, bool bidding)
        {
            //var memoryAuction = AuctionsInMemory.FirstOrDefault(a => a.AuctionId == auctionId);
            //memoryAuction.Bids.Add(new Bid
            //{
            //    CurrentAmount = bidAmount
            //});

            Clients.All.newBid(auctionId, bidding);
            bidding = false;
            CountDownTimer();
        }
        #endregion

        #region broadcast
        public void StartAuction(int id, bool openedBit)
        {
            AuctionsInMemory.Add(_auctionRepository.GetById(id));
            Clients.All.auctionStarted(id, openedBit);
        }

        ////TODO: Research how to implement signalr heartbeats
        public void Heartbeat()
        {
            //foreach (var auction in AuctionsInMemory)
            //{
            //    // is this auction supposed to start right now?
            //    if (auction.StartTime <= DateTime.Now && auction.StartedTime == null)
            //    {
            //        StartAuction(auction.AuctionId);
            //    }

            //    // is this auction supposed to end right now?

            //}
            //Clients.All.heartbeat();
        }

        public void EndAuction(int id, bool closedBit)
        {
            //var memoryAuction = AuctionsInMemory.FirstOrDefault(a => a.AuctionId == id);

            //    _auctionRepository.Update(memoryAuction);

            //    _unitOfWork.Commit();
            Clients.All.auctionFinished(id, closedBit);
        }
        #endregion

        #region countdown
        public void CountDownTimer()
        {
            _timer2 = new System.Threading.Timer(OnTimerElapsed, null, TimeSpan.FromSeconds(20), TimeSpan.FromMilliseconds(0));
            //_timer2.Change(TimeSpan.FromSeconds(20), TimeSpan.FromMilliseconds(0));
        }

        private void OnTimerElapsed(object sender)
        {
             bool closedBit = true;
            EndAuction(7, closedBit);
        }
        #endregion
    }
}