namespace BigBoss.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kaatata : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProjectDeleteModels", "CategoryID", "dbo.CategoryModels");
            DropForeignKey("dbo.ProjectModels", "CategoryID", "dbo.CategoryModels");
            DropIndex("dbo.ProjectDeleteModels", new[] { "CategoryID" });
            DropIndex("dbo.ProjectModels", new[] { "CategoryID" });
            AlterColumn("dbo.ProjectDeleteModels", "CategoryID", c => c.Int());
            AlterColumn("dbo.ProjectModels", "CategoryID", c => c.Int());
            CreateIndex("dbo.ProjectDeleteModels", "CategoryID");
            CreateIndex("dbo.ProjectModels", "CategoryID");
            AddForeignKey("dbo.ProjectDeleteModels", "CategoryID", "dbo.CategoryModels", "Id");
            AddForeignKey("dbo.ProjectModels", "CategoryID", "dbo.CategoryModels", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProjectModels", "CategoryID", "dbo.CategoryModels");
            DropForeignKey("dbo.ProjectDeleteModels", "CategoryID", "dbo.CategoryModels");
            DropIndex("dbo.ProjectModels", new[] { "CategoryID" });
            DropIndex("dbo.ProjectDeleteModels", new[] { "CategoryID" });
            AlterColumn("dbo.ProjectModels", "CategoryID", c => c.Int(nullable: false));
            AlterColumn("dbo.ProjectDeleteModels", "CategoryID", c => c.Int(nullable: false));
            CreateIndex("dbo.ProjectModels", "CategoryID");
            CreateIndex("dbo.ProjectDeleteModels", "CategoryID");
            AddForeignKey("dbo.ProjectModels", "CategoryID", "dbo.CategoryModels", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ProjectDeleteModels", "CategoryID", "dbo.CategoryModels", "Id", cascadeDelete: true);
        }
    }
}
