using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using RTBid.Core.Domain;

namespace RTBid.Data.Infrastructure
{
    public class RTBidDataContext : DbContext
    {
            public RTBidDataContext() : base("RTBid") // The Database name
            {
                var ensureDllIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
            }

            //SQL Tables
            public IDbSet<Auction> Auctions { get; set; }
            public IDbSet<Bid> Bids { get; set; }
            public IDbSet<Category> Categorys { get; set; }
            public IDbSet<Comment> Comments { get; set; }
            public IDbSet<Product> Products { get; set; }
            public IDbSet<Purchase> Purchases { get; set; }
            public IDbSet<Role> Roles { get; set; }
            public IDbSet<RTBidUser> RTBidUsers { get; set; }
            public IDbSet<UserAuction> UserAuctions { get; set; }
            public IDbSet<UserRole> UserRoles { get; set; }

        //Model Relationships
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
            {

            //Auction-Comments
            modelBuilder.Entity<Auction>()
                            .HasMany(a => a.Comments)
                            .WithRequired(c => c.Auction)
                            .HasForeignKey(c => c.AuctionId)
                            .WillCascadeOnDelete(false);

            //Product-Bids
            modelBuilder.Entity<Product>()
                            .HasMany(p => p.Bids)
                            .WithRequired(b => b.Product)
                            .HasForeignKey(b => b.ProductId)
                            .WillCascadeOnDelete(false);

            //Product-Auctions
            modelBuilder.Entity<Product>()
                            .HasMany(p => p.Auctions)
                            .WithRequired(a => a.Product)
                            .HasForeignKey(a => a.ProductId);

            //Category-Products
            modelBuilder.Entity<Category>()
                            .HasMany(c => c.Products)
                            .WithRequired(p => p.Category)
                            .HasForeignKey(p => p.CategoryId);

            //Product/Address-Purchase/Student////ONE to ONE tables relationship

            // Configure StudentId as PK for StudentAddress
            modelBuilder.Entity<Purchase>()
                        .HasKey(e => e.PurchaseId);

            // Configure StudentId as FK for StudentAddress
            modelBuilder.Entity<Product>()
                        .HasOptional(p => p.Purchase)
                        .WithRequired(pur => pur.Product)
                        .Map(m => m.MapKey("ProductId"));

            //RTBidUser-Products
            modelBuilder.Entity<RTBidUser>()
                        .HasMany(u => u.Products)
                        .WithOptional(p => p.RTBidUser)
                        .HasForeignKey(p => p.UserId);

            //RTBidUser-Purchases
            modelBuilder.Entity<RTBidUser>()
                        .HasMany(u => u.Purchases)
                        .WithRequired(pur => pur.RTBidUser)
                        .HasForeignKey(pur => pur.UserId);

            //RTBidUser-Bids
            modelBuilder.Entity<RTBidUser>()
                        .HasMany(u => u.Bids)
                        .WithRequired(b => b.RTBidUser)
                        .HasForeignKey(b => b.UserId);

            //RTBidUser-Comments
            modelBuilder.Entity<RTBidUser>()
                        .HasMany(u => u.Comments)
                        .WithRequired(c => c.RTBidUser)
                        .HasForeignKey(c => c.UserId);

            // Specify Relationships//RTBidUser-Role-UserAuction
            modelBuilder.Entity<RTBidUser>()
                            .HasMany(u => u.Auctions)
                            .WithRequired(ua => ua.RTBidUser)
                            .HasForeignKey(ua => ua.UserId);

            modelBuilder.Entity<Auction>()
                        .HasMany(a => a.RTBidUsers)
                        .WithRequired(ua => ua.Auction)
                        .HasForeignKey(ua => ua.AuctionId);

            modelBuilder.Entity<UserAuction>().HasKey(ua => new { ua.UserId, ua.AuctionId });
        
            // Specify Relationships//RTBidUser-Role-UserRole
            modelBuilder.Entity<RTBidUser>()
                        .HasMany(u => u.Roles)
                        .WithRequired(ur => ur.RTBidUser)
                        .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<Role>()
                        .HasMany(r => r.RTBidUsers)
                        .WithRequired(ur => ur.Role)
                        .HasForeignKey(ur => ur.RoleId);

            modelBuilder.Entity<UserRole>().HasKey(ur => new { ur.UserId, ur.RoleId });

    
            base.OnModelCreating(modelBuilder);
            }
        }
}