using RTBid.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTBid.Core.Services.Finance
{
    public interface IPaymentService
    {
        void BuyItem(RTBidUser user, string token, int numberOfKeys);
        //Buy Membership
    }
}
