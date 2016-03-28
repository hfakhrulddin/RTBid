namespace RTBid.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Auctions", "OpenSoon", c => c.Boolean(nullable: false));
            AddColumn("dbo.Auctions", "InAction", c => c.Boolean(nullable: false));
            AddColumn("dbo.Auctions", "ItemSold", c => c.Boolean(nullable: false));
            AddColumn("dbo.Auctions", "Rescheduled", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Auctions", "Rescheduled");
            DropColumn("dbo.Auctions", "ItemSold");
            DropColumn("dbo.Auctions", "InAction");
            DropColumn("dbo.Auctions", "OpenSoon");
        }
    }
}
