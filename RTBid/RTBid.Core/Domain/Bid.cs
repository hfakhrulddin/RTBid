using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTBid.Core.Domain
{
    public class Bid
    {
        public int BidId { get; set; }
        public int AuctionId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }

        public decimal CurrentAmount { get; set; }
        public string AttachedText { get; set; }

        public DateTime TimeStamp { get; set; }

        public virtual Auction Auction { get; set; }
        public virtual Product Product { get; set; }
        public virtual RTBidUser RTBidUser { get; set; }
    }
}
