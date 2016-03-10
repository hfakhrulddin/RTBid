using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RTBid.Core.Domain;
using RTBid.Core.Repository;
using RTBid.Data.Infrastructure;

namespace RTBid.Data.Repository
{
    public class BidRepository : Repository<Bid>, IBidRepository
    {
        public BidRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

    }
}
