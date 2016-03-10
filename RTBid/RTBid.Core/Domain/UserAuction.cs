using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTBid.Core.Domain
{
    public class UserAuction
    {
        public int UserId { get; set; }
        public int AuctionId { get; set; }

        public virtual RTBidUser RTBidUser { get; set; }
        public virtual Auction Auction { get; set; }
    }
}
