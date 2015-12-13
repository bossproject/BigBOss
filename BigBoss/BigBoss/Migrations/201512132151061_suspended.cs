namespace BigBoss.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class suspended : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProjectDeleteModels", "suspended", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProjectModels", "suspended", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProjectModels", "suspended");
            DropColumn("dbo.ProjectDeleteModels", "suspended");
        }
    }
}
