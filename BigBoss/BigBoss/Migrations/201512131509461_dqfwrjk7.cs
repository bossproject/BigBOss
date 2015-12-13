namespace BigBoss.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dqfwrjk7 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CategoryModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        nameCategory = c.String(nullable: false),
                        descriptionCategory = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
                "dbo.User",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FinishedRegistration = c.Boolean(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.UserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserLogin",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserRole",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Role", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.ProjectDeleteModels",
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
                        CategoryID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CategoryModels", t => t.CategoryID)
                .Index(t => t.CategoryID);
            
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
                        TotalDonations = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NumberOfDonations = c.Int(nullable: false),
                        usersAplication_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.usersAplication_Id)
                .Index(t => t.usersAplication_Id);
            
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
                        CategoryID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CategoryModels", t => t.CategoryID)
                .Index(t => t.CategoryID);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRole", "RoleId", "dbo.Role");
            DropForeignKey("dbo.ProjectModels", "CategoryID", "dbo.CategoryModels");
            DropForeignKey("dbo.OrganizationModels", "usersAplication_Id", "dbo.User");
            DropForeignKey("dbo.DonatorModels", "usersAplication_Id", "dbo.User");
            DropForeignKey("dbo.ProjectDeleteModels", "CategoryID", "dbo.CategoryModels");
            DropForeignKey("dbo.CompanyModels", "usersAplication_Id", "dbo.User");
            DropForeignKey("dbo.UserRole", "UserId", "dbo.User");
            DropForeignKey("dbo.UserLogin", "UserId", "dbo.User");
            DropForeignKey("dbo.UserClaim", "UserId", "dbo.User");
            DropIndex("dbo.Role", "RoleNameIndex");
            DropIndex("dbo.ProjectModels", new[] { "CategoryID" });
            DropIndex("dbo.OrganizationModels", new[] { "usersAplication_Id" });
            DropIndex("dbo.DonatorModels", new[] { "usersAplication_Id" });
            DropIndex("dbo.ProjectDeleteModels", new[] { "CategoryID" });
            DropIndex("dbo.UserRole", new[] { "RoleId" });
            DropIndex("dbo.UserRole", new[] { "UserId" });
            DropIndex("dbo.UserLogin", new[] { "UserId" });
            DropIndex("dbo.UserClaim", new[] { "UserId" });
            DropIndex("dbo.User", "UserNameIndex");
            DropIndex("dbo.CompanyModels", new[] { "usersAplication_Id" });
            DropTable("dbo.Role");
            DropTable("dbo.ProjectModels");
            DropTable("dbo.OrganizationModels");
            DropTable("dbo.DonatorModels");
            DropTable("dbo.ProjectDeleteModels");
            DropTable("dbo.UserRole");
            DropTable("dbo.UserLogin");
            DropTable("dbo.UserClaim");
            DropTable("dbo.User");
            DropTable("dbo.CompanyModels");
            DropTable("dbo.CategoryModels");
        }
    }
}
