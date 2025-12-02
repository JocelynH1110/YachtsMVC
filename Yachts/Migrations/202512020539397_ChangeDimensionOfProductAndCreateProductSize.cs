namespace Yachts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeDimensionOfProductAndCreateProductSize : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductSizes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        DimensionValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            DropColumn("dbo.Products", "Dimensions");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Dimensions", c => c.String());
            DropForeignKey("dbo.ProductSizes", "ProductId", "dbo.Products");
            DropIndex("dbo.ProductSizes", new[] { "ProductId" });
            DropTable("dbo.ProductSizes");
        }
    }
}
