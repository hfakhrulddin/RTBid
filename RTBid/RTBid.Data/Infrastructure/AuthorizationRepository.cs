using Microsoft.AspNet.Identity;
using RTBid.Core.Domain;
using RTBid.Core.Infrastructure;
using RTBid.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTBid.Data.Infrastructure
{
    public class AuthorizationRepository : IAuthorizationRepository, IDisposable
    {
            private readonly IUserStore<RTBidUser, int> _userStore;
            private readonly IDatabaseFactory _databaseFactory;
            private readonly UserManager<RTBidUser, int> _userManager;

            private RTBidDataContext db;
            protected RTBidDataContext Db => db ?? (db = _databaseFactory.GetDataContext());

            public AuthorizationRepository(IDatabaseFactory databaseFactory, IUserStore<RTBidUser, int> userStore)
            {
                _userStore = userStore;
                _databaseFactory = databaseFactory;
                _userManager = new UserManager<RTBidUser, int>(userStore);
            }

            ///Register Method -Register
            public async Task<IdentityResult> RegisterUser(RegistrationModel model)
            {
                var wingmanUser = new RTBidUser
                {
                    UserName = model.Username,
                    Email = model.EmailAddress,
                };

                var result = await _userManager.CreateAsync(wingmanUser, model.Password);

                return result;
            }

            ///Find User Method-Login
            public async Task<RTBidUser> FindUser(string username, string password)
            {
                return await _userManager.FindAsync(username, password);
            }

            public void Dispose()
            {
                _userManager.Dispose();
            }
        }
}

