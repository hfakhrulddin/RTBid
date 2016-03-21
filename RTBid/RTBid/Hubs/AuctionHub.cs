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

namespace RTBid.Hubs
{
    [HubName("AuctionHub")]
    public class AuctionHub : Hub
    {
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
        public void SendChatMessage(string message)
        {
            // take the message
            // grab the auction
            Clients.All.newChatMessage(message);
        }

        public void BidOnItem(int auctionId, decimal bidAmount)
        {
            var memoryAuction = AuctionsInMemory.FirstOrDefault(a => a.AuctionId == auctionId);

            memoryAuction.Bids.Add(new Bid
            {
                CurrentAmount = bidAmount
            });
            Clients.All.newBid(bidAmount);
        }

        #endregion

        #region broadcast
        public void StartAuction(int id)
        {
            AuctionsInMemory.Add(_auctionRepository.GetById(id));
            Clients.All.auctionStarted(id);
        }

        ////TODO: Research how to implement signalr heartbeats
        public void Heartbeat()
        {
            foreach (var auction in AuctionsInMemory)
            {
                // is this auction supposed to start right now?
                if (auction.StartTime <= DateTime.Now && auction.StartedTime == null)
                {
                    StartAuction(auction.AuctionId);
                }

                // is this auction supposed to end right now?

            }
            Clients.All.heartbeat();
        }

        public void EndAuction(int id)
        {
            var memoryAuction = AuctionsInMemory.FirstOrDefault(a => a.AuctionId == id);

            _auctionRepository.Update(memoryAuction);

            _unitOfWork.Commit();
            Clients.All.auctionFinished(id);
        }

        #endregion      
    } };