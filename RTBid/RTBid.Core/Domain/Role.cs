using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTBid.Core.Domain
{
    public class Role : IRole<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<UserRole> RTBidUsers { get; set; }
    }
}
