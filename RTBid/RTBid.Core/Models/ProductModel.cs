using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTBid.Core.Models
{
    public class ProductModel
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        public decimal? ShippingCost { get; set; }
        public decimal? SellingPrice { get; set; }
        public decimal? StartBid { get; set; }
        public decimal? MinimumIncrement { get; set; }
    }
}
