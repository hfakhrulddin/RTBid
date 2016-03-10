using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTBid.Core.Models
{
    public class CommentModel
    {
        public int CommentId { get; set; }
        public int AuctionId { get; set; }
        public int UserId { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public string AttachmentUrl { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
