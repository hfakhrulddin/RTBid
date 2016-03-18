namespace RTBid.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedstartedtime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Auctions", "StartedTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Auctions", "StartedTime");
        }
    }
}
