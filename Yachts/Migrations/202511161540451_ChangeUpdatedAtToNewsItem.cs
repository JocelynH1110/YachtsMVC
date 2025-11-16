namespace Yachts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeUpdatedAtToNewsItem : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.NewsItems", "UpdatedAt", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.NewsItems", "UpdatedAt", c => c.DateTime(nullable: false));
        }
    }
}
