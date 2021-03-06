﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTBid.Core.Models
{
    public class BidModel
    {
        public int BidId { get; set; }
        public int AuctionId { get; set; }
        public int UserId { get; set; }

        public decimal CurrentAmount { get; set; }
        public string AttachedText { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}
