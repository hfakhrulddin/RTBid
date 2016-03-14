using RTBid.Core.Models;
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


        public Purchase()
        {
        }

        public Purchase(PurchaseModel model)
        {
            this.Update(model);
            this.CreatedDate = DateTime.Now;
        }

        public void Update(PurchaseModel model)
        {
            PurchaseId = model.PurchaseId;
            UserId = model.UserId;

            InvoiceNumber = model.InvoiceNumber;
            ShippingCost = model.ShippingCost;
            Price = model.Price;
            
        }
    }
}
