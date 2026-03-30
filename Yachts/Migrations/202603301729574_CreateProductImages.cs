namespace Yachts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateProductImages : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        ImageUrl = c.String(nullable: false),
                        IsCover = c.Boolean(nullable: false),
                        SortOrder = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => new { t.ProductId, t.SortOrder }, unique: true, name: "IX_Product_SortOrder");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductImages", "ProductId", "dbo.Products");
            DropIndex("dbo.ProductImages", "IX_Product_SortOrder");
            DropTable("dbo.ProductImages");
        }
    }
}
