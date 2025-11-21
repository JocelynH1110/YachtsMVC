namespace Yachts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCoverPhotoPathToNewsItem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NewsItems", "CoverPhotoPath", c => c.String(maxLength: 255));
        }
        
        public override void Down()
        {
            DropColumn("dbo.NewsItems", "CoverPhotoPath");
        }
    }
}
