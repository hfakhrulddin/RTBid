using RTBid.Core.Domain;
using RTBid.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace RTBid.Core.Infrastructure
{
    public interface IAuthorizationRepository : IDisposable
    {
        Task<RTBidUser> FindUser(string username, string password);
        Task<IdentityResult> RegisterUser(RegistrationModel model);
    }
}
