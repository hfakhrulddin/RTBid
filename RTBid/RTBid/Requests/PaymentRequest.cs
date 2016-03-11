using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTBid.Requests
{
    public class PaymentRequest
    {
        public string token { get; set; }
        public int numberOfKeys { get; set; }
    }
}