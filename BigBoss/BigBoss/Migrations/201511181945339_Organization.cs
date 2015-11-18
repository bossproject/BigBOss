namespace BigBoss.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Organization : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrganizationModels",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        OrganizationName = c.String(nullable: false),
                        MaticniBroj = c.String(nullable: false),
                        PIB = c.String(nullable: false),
                        usersAplication_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.usersAplication_Id)
                .Index(t => t.usersAplication_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrganizationModels", "usersAplication_Id", "dbo.User");
            DropIndex("dbo.OrganizationModels", new[] { "usersAplication_Id" });
            DropTable("dbo.OrganizationModels");
        }
    }
}
