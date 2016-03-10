using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTBid.Data.Infrastructure
{
    public class DatabaseFactory
    {
        private readonly RTBidDataContext _dataContext;

        public RTBidDataContext GetDataContext()
        {
            return _dataContext ?? new RTBidDataContext();
        }

        public DatabaseFactory()
        {
            _dataContext = new RTBidDataContext();
        }

        protected override void DisposeCore()
        {
            if (_dataContext != null) _dataContext.Dispose();
        }
    }
}
