using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTBid.Core.Domain
{
    public class Purchase
    {
        public int PurchaseId { get; set; }

        public int UserId { get; set; }

        public string InvoiceNumber { get; set; }
        public decimal? ShippingCost { get; set; }
        public decimal? Price { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual RTBidUser RTBidUser { get; set; }
        public virtual Product Product { get; set; }
    }
}
