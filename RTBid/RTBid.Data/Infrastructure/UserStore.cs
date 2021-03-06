﻿using Microsoft.AspNet.Identity;
using RTBid.Core.Domain;
using RTBid.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTBid.Data.Infrastructure
{
    public class UserStore : Disposable,
                             IUserPasswordStore<RTBidUser, int>,
                             IUserSecurityStampStore<RTBidUser, int>,
                             IUserRoleStore<RTBidUser, int>

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

        public UserStore(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }

        #region IUserPasswordStore
        public Task CreateAsync(RTBidUser user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.Factory.StartNew(() => {
                //user.Id = Guid.NewGuid(); // CR
                user.CreatedDate = DateTime.Now;
                DataContext.RTBidUsers.Add(user);
                DataContext.SaveChanges();
            });
        }

        public Task DeleteAsync(RTBidUser user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.Factory.StartNew(() => {
                DataContext.RTBidUsers.Remove(user);
                DataContext.SaveChanges();
            });
        }

        public Task<RTBidUser> FindByIdAsync(int userId)
        {
            return Task.Factory.StartNew(() => {
                return DataContext.RTBidUsers.FirstOrDefault(u => u.Id == userId);
            });
        }

        public Task<RTBidUser> FindByNameAsync(string userName)
        {
            return Task.Factory.StartNew(() =>
            {
                return DataContext.RTBidUsers.FirstOrDefault(u => u.UserName == userName);
            });
        }

        public Task<string> GetPasswordHashAsync(RTBidUser user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.Factory.StartNew(() =>
            {
                return user.PasswordHash;
            });
        }

        public Task<bool> HasPasswordAsync(RTBidUser user)
        {
            return Task.FromResult(!string.IsNullOrEmpty(user.PasswordHash));
        }

        public Task SetPasswordHashAsync(RTBidUser user, string passwordHash)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            user.PasswordHash = passwordHash;

            return Task.FromResult(0);
        }

        public Task UpdateAsync(RTBidUser user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.Factory.StartNew(() =>
            {
                DataContext.RTBidUsers.Attach(user);
                DataContext.Entry(user).State = EntityState.Modified;

                DataContext.SaveChanges();
            });
        }
        #endregion

        #region ISecurityStampStore
        public Task<string> GetSecurityStampAsync(RTBidUser user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.SecurityStamp);
        }

        public Task SetSecurityStampAsync(RTBidUser user, string stamp)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            user.SecurityStamp = stamp;

            return Task.FromResult(0);
        }
        #endregion

        #region IUserRoleStore
        public Task AddToRoleAsync(RTBidUser user, string roleName)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (string.IsNullOrEmpty(roleName))
            {
                throw new ArgumentException("Argument cannot be null or empty: roleName.");
            }

            return Task.Factory.StartNew(() =>
            {
                if (!DataContext.Roles.Any(r => r.Name == roleName))
                {
                    DataContext.Roles.Add(new Role
                    {
                        //Id = Guid.NewGuid().ToString(),
                        Name = roleName
                    });
                }

                DataContext.UserRoles.Add(new UserRole
                {
                    Role = DataContext.Roles.FirstOrDefault(r => r.Name == roleName),
                    RTBidUser = user
                });

                DataContext.SaveChanges();
            });
        }

        public Task RemoveFromRoleAsync(RTBidUser user, string roleName)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (string.IsNullOrEmpty(roleName))
            {
                throw new ArgumentException("Argument cannot be null or empty: roleName.");
            }

            return Task.Factory.StartNew(() =>
            {
                var userRole = user.Roles.FirstOrDefault(r => r.Role.Name == roleName);

                if (userRole == null)
                {
                    throw new InvalidOperationException("User does not have that role");
                }

                DataContext.UserRoles.Remove(userRole);

                DataContext.SaveChanges();
            });
        }

        public Task<IList<string>> GetRolesAsync(RTBidUser user)
        {
            return Task.Factory.StartNew(() =>
            {
                return (IList<string>)user.Roles.Select(ur => ur.Role.Name);
            });
        }

        public Task<bool> IsInRoleAsync(RTBidUser user, string roleName)
        {
            return Task.Factory.StartNew(() =>
            {
                return user.Roles.Any(r => r.Role.Name == roleName);
            });
        }
    }
    #endregion
}
