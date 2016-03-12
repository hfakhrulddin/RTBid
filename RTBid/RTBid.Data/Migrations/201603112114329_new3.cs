namespace RTBid.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RTBidUsers", "DateOfBirth", c => c.DateTime());
            AlterColumn("dbo.RTBidUsers", "DeletedDate", c => c.DateTime());
            DropColumn("dbo.RTBidUsers", "Age");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RTBidUsers", "Age", c => c.Int(nullable: false));
            AlterColumn("dbo.RTBidUsers", "DeletedDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.RTBidUsers", "DateOfBirth");
        }
    }
}
