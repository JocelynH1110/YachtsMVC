namespace Yachts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDealers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Dealers",
                c => new
                    {
                        DealerId = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(nullable: false, maxLength: 200),
                        Contact = c.String(nullable: false, maxLength: 100),
                        Address = c.String(),
                        Tel = c.String(nullable: false, maxLength: 30),
                        Fax = c.String(maxLength: 30),
                        Email = c.String(maxLength: 200),
                        Website = c.String(maxLength: 200),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        SortOrder = c.Int(nullable: false),
                        CountryCode = c.String(nullable: false, maxLength: 2),
                    })
                .PrimaryKey(t => t.DealerId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Dealers");
        }
    }
}
