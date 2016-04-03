namespace RTBid.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new10 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Auctions", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Auctions", "DeletedDate", c => c.DateTime());
            AlterColumn("dbo.Auctions", "StartTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Auctions", "StartedTime", c => c.DateTime());
            AlterColumn("dbo.Auctions", "ClosedTime", c => c.DateTime());
            AlterColumn("dbo.Auctions", "ActualClosedTime", c => c.DateTime());
            AlterColumn("dbo.Auctions", "RemainingTime", c => c.DateTime());
            AlterColumn("dbo.Bids", "TimeStamp", c => c.DateTime(nullable: false));
            AlterColumn("dbo.RTBidUsers", "DateOfBirth", c => c.DateTime());
            AlterColumn("dbo.RTBidUsers", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.RTBidUsers", "DeletedDate", c => c.DateTime());
            AlterColumn("dbo.Comments", "TimeStamp", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Categories", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Purchases", "CreatedDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Purchases", "CreatedDate", c => c.DateTimeOffset(nullable: false, precision: 7));
            AlterColumn("dbo.Categories", "CreatedDate", c => c.DateTimeOffset(nullable: false, precision: 7));
            AlterColumn("dbo.Comments", "TimeStamp", c => c.DateTimeOffset(nullable: false, precision: 7));
            AlterColumn("dbo.RTBidUsers", "DeletedDate", c => c.DateTimeOffset(precision: 7));
            AlterColumn("dbo.RTBidUsers", "CreatedDate", c => c.DateTimeOffset(nullable: false, precision: 7));
            AlterColumn("dbo.RTBidUsers", "DateOfBirth", c => c.DateTimeOffset(precision: 7));
            AlterColumn("dbo.Bids", "TimeStamp", c => c.DateTimeOffset(nullable: false, precision: 7));
            AlterColumn("dbo.Auctions", "RemainingTime", c => c.DateTimeOffset(precision: 7));
            AlterColumn("dbo.Auctions", "ActualClosedTime", c => c.DateTimeOffset(precision: 7));
            AlterColumn("dbo.Auctions", "ClosedTime", c => c.DateTimeOffset(precision: 7));
            AlterColumn("dbo.Auctions", "StartedTime", c => c.DateTimeOffset(precision: 7));
            AlterColumn("dbo.Auctions", "StartTime", c => c.DateTimeOffset(nullable: false, precision: 7));
            AlterColumn("dbo.Auctions", "DeletedDate", c => c.DateTimeOffset(precision: 7));
            AlterColumn("dbo.Auctions", "CreatedDate", c => c.DateTimeOffset(nullable: false, precision: 7));
        }
    }
}
