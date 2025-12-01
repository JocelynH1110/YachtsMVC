namespace Yachts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFilePathToNewsItemAndCreateNewsAttachment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NewsAttachments",
                c => new
                    {
                        AttachmentId = c.Int(nullable: false, identity: true),
                        FileName = c.String(maxLength: 255),
                        FilePath = c.String(maxLength: 500),
                        UploadedAt = c.DateTime(nullable: false),
                        NewsId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AttachmentId)
                .ForeignKey("dbo.NewsItems", t => t.NewsId, cascadeDelete: true)
                .Index(t => t.NewsId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NewsAttachments", "NewsId", "dbo.NewsItems");
            DropIndex("dbo.NewsAttachments", new[] { "NewsId" });
            DropTable("dbo.NewsAttachments");
        }
    }
}
