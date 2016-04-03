namespace RTBid.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveStatusenum : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Auctions", "Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Auctions", "Status", c => c.Int(nullable: false));
        }
    }
}
