namespace Yachts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPhotoPathToDealer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Dealers", "PhotoPath", c => c.String(maxLength: 255));
            AlterColumn("dbo.Dealers", "UpdatedAt", c => c.DateTime());
            AlterColumn("dbo.Dealers", "CountryCode", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Dealers", "CountryCode", c => c.String(nullable: false, maxLength: 2));
            AlterColumn("dbo.Dealers", "UpdatedAt", c => c.DateTime(nullable: false));
            DropColumn("dbo.Dealers", "PhotoPath");
        }
    }
}
