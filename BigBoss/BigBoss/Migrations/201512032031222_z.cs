namespace BigBoss.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class z : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProjectModels", "categoryMod_Id", "dbo.CategoryModels");
            DropIndex("dbo.ProjectModels", new[] { "categoryMod_Id" });
            RenameColumn(table: "dbo.ProjectModels", name: "categoryMod_Id", newName: "CategoryID");
            AlterColumn("dbo.ProjectModels", "CategoryID", c => c.Int(nullable: false));
            CreateIndex("dbo.ProjectModels", "CategoryID");
            AddForeignKey("dbo.ProjectModels", "CategoryID", "dbo.CategoryModels", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProjectModels", "CategoryID", "dbo.CategoryModels");
            DropIndex("dbo.ProjectModels", new[] { "CategoryID" });
            AlterColumn("dbo.ProjectModels", "CategoryID", c => c.Int());
            RenameColumn(table: "dbo.ProjectModels", name: "CategoryID", newName: "categoryMod_Id");
            CreateIndex("dbo.ProjectModels", "categoryMod_Id");
            AddForeignKey("dbo.ProjectModels", "categoryMod_Id", "dbo.CategoryModels", "Id");
        }
    }
}
