using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTBid.Core.Domain
{
    public class RTBidUser : IUser<int>
    {
        public int Id { get; set; }
        public string UserName { get; set; }

        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }

        public string Address { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }

        public DateTime? DateOfBirth { get; set; }
        public decimal? AccountBalance { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }

        public virtual ICollection<UserRole> Roles { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<UserAuction> Auctions { get; set; }
        public virtual ICollection<Bid> Bids { get; set; }
        public virtual ICollection<Purchase> Purchases { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}

