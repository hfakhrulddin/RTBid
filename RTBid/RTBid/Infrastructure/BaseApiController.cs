using RTBid.Core.Domain;
using RTBid.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace RTBid.Infrastructure
{
    public class BaseApiController :ApiController
    {
        protected readonly IRTBidUserRepository _wingmanUserRepository;
        /// ///////////////////
        public BaseApiController(IRTBidUserRepository wingmanUserRepository)
        {
            _wingmanUserRepository = wingmanUserRepository;
        }
        ///
        protected RTBidUser CurrentUser
        {
            get
            {
                return _wingmanUserRepository.GetFirstOrDefault(u => u.UserName == User.Identity.Name);
            }
        }
    }
}