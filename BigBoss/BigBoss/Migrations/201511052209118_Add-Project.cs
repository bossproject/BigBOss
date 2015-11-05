namespace BigBoss.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProject : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProjectModels",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        nameProject = c.String(nullable: false),
                        descProject = c.String(nullable: false),
                        additionalInfo = c.String(),
                        tagsProject = c.String(nullable: false),
                        money = c.Decimal(nullable: false, precision: 18, scale: 2),
                        moneyWithCommission = c.Decimal(nullable: false, precision: 18, scale: 2),
                        moneyRaised = c.Decimal(nullable: false, precision: 18, scale: 2),
                        numberOfDonations = c.Int(nullable: false),
                        categoryMod_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CategoryModels", t => t.categoryMod_Id)
                .Index(t => t.categoryMod_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProjectModels", "categoryMod_Id", "dbo.CategoryModels");
            DropIndex("dbo.ProjectModels", new[] { "categoryMod_Id" });
            DropTable("dbo.ProjectModels");
        }
    }
}
