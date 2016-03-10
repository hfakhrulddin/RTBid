using RTBid.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTBid.Data.Infrastructure
{
        public class UnitOfWork : IUnitOfWork
    {

            private readonly IDatabaseFactory _databaseFactory;
            private RTBidDataContext _dataContext;


            protected RTBidDataContext DataContext
            {
                get
                {
                    return _dataContext ?? (_dataContext = _databaseFactory.GetDataContext());
                }
            }

            public UnitOfWork(IDatabaseFactory databaseFactory)
            {
                _databaseFactory = databaseFactory;
            }

            public void Commit()
            {

                DataContext.SaveChanges();
            }
    }
}

