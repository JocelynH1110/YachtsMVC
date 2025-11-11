namespace Yachts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRegionToDealer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Dealers", "Region", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Dealers", "Region");
        }
    }
}
