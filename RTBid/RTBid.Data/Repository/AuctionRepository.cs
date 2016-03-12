using RTBid.Core.Domain;
using RTBid.Core.Repository;
using RTBid.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTBid.Data.Repository
{
    public class AuctionRepository : Repository<Auction>, IAuctionRepository
    {
        public AuctionRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
