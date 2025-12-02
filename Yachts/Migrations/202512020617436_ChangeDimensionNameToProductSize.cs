namespace Yachts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeDimensionNameToProductSize : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductSizes", "DimensionName", c => c.String(maxLength: 30));
            AlterColumn("dbo.ProductSizes", "DimensionValue", c => c.String(maxLength: 30));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ProductSizes", "DimensionValue", c => c.String());
            DropColumn("dbo.ProductSizes", "DimensionName");
        }
    }
}
