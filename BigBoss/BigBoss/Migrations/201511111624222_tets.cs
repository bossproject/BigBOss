namespace BigBoss.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tets : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProjectModels", "categoryMod_Id", c => c.Int());
            CreateIndex("dbo.ProjectModels", "categoryMod_Id");
            AddForeignKey("dbo.ProjectModels", "categoryMod_Id", "dbo.CategoryModels", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProjectModels", "categoryMod_Id", "dbo.CategoryModels");
            DropIndex("dbo.ProjectModels", new[] { "categoryMod_Id" });
            DropColumn("dbo.ProjectModels", "categoryMod_Id");
        }
    }
}
