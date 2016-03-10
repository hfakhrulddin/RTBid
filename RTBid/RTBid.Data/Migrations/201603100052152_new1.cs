namespace RTBid.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Auctions",
                c => new
                    {
                        AuctionId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        AuctionTitle = c.String(),
                        NumberOfBidders = c.Int(),
                        NumberOfGuests = c.Int(),
                        CreatedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                        StartTime = c.DateTime(nullable: false),
                        ClosedTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.AuctionId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Bids",
                c => new
                    {
                        BidId = c.Int(nullable: false, identity: true),
                        AuctionId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        CurrentAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AttachedText = c.String(),
                        TimeStamp = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.BidId)
                .ForeignKey("dbo.Auctions", t => t.AuctionId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .ForeignKey("dbo.RTBidUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.AuctionId)
                .Index(t => t.UserId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        UserId = c.Int(),
                        Name = c.String(),
                        Description = c.String(),
                        ImageUrl = c.String(),
                        ShippingCost = c.Decimal(precision: 18, scale: 2),
                        SellingPrice = c.Decimal(precision: 18, scale: 2),
                        StartBid = c.Decimal(precision: 18, scale: 2),
                        MinimumIncrement = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.RTBidUsers", t => t.UserId)
                .Index(t => t.CategoryId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                        CategoryDescription = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Purchases",
                c => new
                    {
                        PurchaseId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        InvoiceNumber = c.String(),
                        ShippingCost = c.Decimal(precision: 18, scale: 2),
                        Price = c.Decimal(precision: 18, scale: 2),
                        CreatedDate = c.DateTime(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PurchaseId)
                .ForeignKey("dbo.RTBidUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .Index(t => t.UserId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.RTBidUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        Address = c.String(),
                        Telephone = c.String(),
                        Email = c.String(),
                        Age = c.Int(nullable: false),
                        AccountBalance = c.Decimal(precision: 18, scale: 2),
                        CreatedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserAuctions",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        AuctionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.AuctionId })
                .ForeignKey("dbo.RTBidUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Auctions", t => t.AuctionId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.AuctionId);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        AuctionId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        Title = c.String(),
                        Description = c.String(),
                        AttachmentUrl = c.String(),
                        TimeStamp = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.RTBidUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Auctions", t => t.AuctionId)
                .Index(t => t.AuctionId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.RTBidUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserAuctions", "AuctionId", "dbo.Auctions");
            DropForeignKey("dbo.Comments", "AuctionId", "dbo.Auctions");
            DropForeignKey("dbo.Purchases", "ProductId", "dbo.Products");
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.RTBidUsers");
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Purchases", "UserId", "dbo.RTBidUsers");
            DropForeignKey("dbo.Products", "UserId", "dbo.RTBidUsers");
            DropForeignKey("dbo.Comments", "UserId", "dbo.RTBidUsers");
            DropForeignKey("dbo.Bids", "UserId", "dbo.RTBidUsers");
            DropForeignKey("dbo.UserAuctions", "UserId", "dbo.RTBidUsers");
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Bids", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Auctions", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Bids", "AuctionId", "dbo.Auctions");
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.Comments", new[] { "UserId" });
            DropIndex("dbo.Comments", new[] { "AuctionId" });
            DropIndex("dbo.UserAuctions", new[] { "AuctionId" });
            DropIndex("dbo.UserAuctions", new[] { "UserId" });
            DropIndex("dbo.Purchases", new[] { "ProductId" });
            DropIndex("dbo.Purchases", new[] { "UserId" });
            DropIndex("dbo.Products", new[] { "UserId" });
            DropIndex("dbo.Products", new[] { "CategoryId" });
            DropIndex("dbo.Bids", new[] { "ProductId" });
            DropIndex("dbo.Bids", new[] { "UserId" });
            DropIndex("dbo.Bids", new[] { "AuctionId" });
            DropIndex("dbo.Auctions", new[] { "ProductId" });
            DropTable("dbo.Roles");
            DropTable("dbo.UserRoles");
            DropTable("dbo.Comments");
            DropTable("dbo.UserAuctions");
            DropTable("dbo.RTBidUsers");
            DropTable("dbo.Purchases");
            DropTable("dbo.Categories");
            DropTable("dbo.Products");
            DropTable("dbo.Bids");
            DropTable("dbo.Auctions");
        }
    }
}
