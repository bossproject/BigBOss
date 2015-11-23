namespace BigBoss.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class companydonator : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CompanyModels",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        OrganizationName = c.String(nullable: false),
                        MaticniBroj = c.String(nullable: false),
                        Delatnost = c.String(nullable: false),
                        PIB = c.String(nullable: false),
                        usersAplication_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.usersAplication_Id)
                .Index(t => t.usersAplication_Id);
            
            CreateTable(
                "dbo.DonatorModels",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        OrganizationName = c.String(nullable: false),
                        MaticniBroj = c.String(nullable: false),
                        street = c.String(nullable: false),
                        Country = c.String(nullable: false),
                        City = c.String(nullable: false),
                        usersAplication_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.usersAplication_Id)
                .Index(t => t.usersAplication_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DonatorModels", "usersAplication_Id", "dbo.User");
            DropForeignKey("dbo.CompanyModels", "usersAplication_Id", "dbo.User");
            DropIndex("dbo.DonatorModels", new[] { "usersAplication_Id" });
            DropIndex("dbo.CompanyModels", new[] { "usersAplication_Id" });
            DropTable("dbo.DonatorModels");
            DropTable("dbo.CompanyModels");
        }
    }
}
