namespace RTBid.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new8 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Auctions", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Auctions", "Status");
        }
    }
}
