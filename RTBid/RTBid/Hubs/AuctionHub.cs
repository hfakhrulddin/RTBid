//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using Microsoft.AspNet.SignalR;
//using RTBid.Core.Domain;
//using Microsoft.AspNet.SignalR.Hubs;
//using RTBid.Core.Repository;
//using RTBid.Core.Infrastructure;

//namespace RTBid.Hubs
//{
//    [HubName("AuctionHub")]
//    public class AuctionHub : Hub
//    {
//        private readonly IAuctionRepository _auctionRepository;
//        private readonly IUnitOfWork _unitOfWork;

//        public AuctionHub(IAuctionRepository auctionRepository, IUnitOfWork unitOfWork)
//        {
//            _auctionRepository = auctionRepository;
//            _unitOfWork = unitOfWork;
//        }

//        public HashSet<Auction> AuctionsInMemory = new HashSet<Auction>();

//        public void StartAuction(int id)
//        {
//            AuctionsInMemory.Add(_auctionRepository.GetById(id));
//        }

//        //TODO: Research how to implement signalr heartbeats
//        public void Heartbeat()
//        {
//            foreach (var auction in AuctionsInMemory)
//            {
//                // broadcast the state of the current auction in the loop
//            }
//        }

//        public void sendChatMessage(int auctionId, string message)
//        {
//            var memoryAuction = Auction
//        }

//        public void BidOnItem(int auctionId, decimal bidAmount)
//        {
//            var memoryAuction = AuctionsInMemory.FirstOrDefault(a => a.AuctionId == auctionId);

//            memoryAuction.Bids.Add(new Bid
//            {
//                CurrentAmount = bidAmount
//            });
//        }

//        public void EndAuction(int id)
//        {
//            var memoryAuction = AuctionsInMemory.FirstOrDefault(a => a.AuctionId == id);

//            _auctionRepository.Update(memoryAuction);

//            _unitOfWork.Commit();
//        }

//        public void Hello()
//        {
//            Clients.All.hello();
//        }
//    }
//}