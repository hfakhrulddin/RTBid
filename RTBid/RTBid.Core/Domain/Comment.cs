using RTBid.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTBid.Core.Domain
{
    public class Comment
    {
        
        public int CommentId { get; set; }
        public int AuctionId { get; set; }
        public int UserId { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public string AttachmentUrl { get; set; }
        public DateTime TimeStamp { get; set; }


        public virtual RTBidUser RTBidUser { get; set; }
        public virtual Auction Auction { get; set; }


        public Comment()
        {
        }

        public Comment(CommentModel model)
        {
            this.Update(model);
            this.TimeStamp = DateTime.Now;
        }

        public void Update(CommentModel model)
        {
            CommentId = model.CommentId;
            AuctionId = model.AuctionId;
            UserId = model.UserId;
            Title = model.Title;
            Description = model.Description;
            AttachmentUrl = model.AttachmentUrl;
        }



        }
    }
