using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTBid.Core.Domain
{
   public class Auction
    {
        public int AuctionId { get; set; }
        public int ProductId { get; set; }

        public string AuctionTitle { get; set; }
        public int? NumberOfBidders { get; set; }
        public int? NumberOfGuests { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime? ClosedTime { get; set; }

        public virtual Product Product { get; set; }

        public virtual ICollection<UserAuction> RTBidUsers { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Bid> Bids { get; set; }
    }
}
