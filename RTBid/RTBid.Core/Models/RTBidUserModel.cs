using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTBid.Core.Models
{
    public class RTBidUserModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }

        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }

        public string Address { get; set; }



        public DateTime CreatedDate { get; set; }
        public DateTime DeletedDate { get; set; }


        public class Profile : RTBidUserModel
        {
            public string Email { get; set; }
            public string Telephone { get; set; }
            public int Age { get; set; }
            public decimal? AccountBalance { get; set; }
        }
    }
}
