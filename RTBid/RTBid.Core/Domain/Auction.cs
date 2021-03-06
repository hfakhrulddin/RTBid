﻿using RTBid.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public DateTime? StartedTime { get; set; }
        public DateTime? ClosedTime { get; set; }
        public DateTime? ActualClosedTime { get; set; }
        public DateTime? RemainingTime { get; set; }
        public decimal StartBid { get; set; }
        

        public bool OpenSoon { get; set; }
        public bool InAction { get; set; }
        public bool ItemSold { get; set; }
        public bool Rescheduled { get; set; }


        public virtual Product Product { get; set; }

        public virtual ICollection<UserAuction> RTBidUsers { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Bid> Bids { get; set; }


        public Auction()
        {
            RTBidUsers = new Collection<UserAuction>();
            Comments = new Collection<Comment>();
            Bids = new Collection<Bid>();
        }

        public Auction(AuctionModel model) : this()
        {
            this.Update(model);
            this.CreatedDate = DateTime.Now;
        }

        public void Update(AuctionModel model)
        {
            AuctionId = model.AuctionId;
            ProductId = model.ProductId;
            AuctionTitle = model.AuctionTitle;
            NumberOfBidders = model.NumberOfBidders;
            NumberOfGuests = model.NumberOfGuests;
            StartTime = model.StartTime;
            ClosedTime = model.ClosedTime;
            StartedTime = model.StartedTime;
            StartBid = model.StartBid;

            OpenSoon = model.OpenSoon;
            InAction = model.InAction;
            ItemSold = model.ItemSold;
            Rescheduled = model.Rescheduled;

         
}
    }
}