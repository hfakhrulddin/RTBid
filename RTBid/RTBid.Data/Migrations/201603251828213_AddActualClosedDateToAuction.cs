namespace RTBid.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddActualClosedDateToAuction : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Auctions", "ActualClosedTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Auctions", "ActualClosedTime");
        }
    }
}
