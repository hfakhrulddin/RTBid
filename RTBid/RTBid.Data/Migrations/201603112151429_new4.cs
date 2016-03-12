namespace RTBid.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new4 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Bids", new[] { "ProductId" });
            RenameColumn(table: "dbo.Bids", name: "ProductId", newName: "Product_ProductId");
            AlterColumn("dbo.Bids", "Product_ProductId", c => c.Int());
            CreateIndex("dbo.Bids", "Product_ProductId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Bids", new[] { "Product_ProductId" });
            AlterColumn("dbo.Bids", "Product_ProductId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Bids", name: "Product_ProductId", newName: "ProductId");
            CreateIndex("dbo.Bids", "ProductId");
        }
    }
}
