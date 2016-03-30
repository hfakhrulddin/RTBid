namespace RTBid.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new9 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Auctions", "RemainingTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Auctions", "RemainingTime");
        }
    }
}
