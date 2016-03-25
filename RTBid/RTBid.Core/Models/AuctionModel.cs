using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTBid.Core.Models
{
    public class AuctionModel
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
        public DateTime? StartedTime { get; set; }

        public ProductModel Product { get; set; }
    }
}
