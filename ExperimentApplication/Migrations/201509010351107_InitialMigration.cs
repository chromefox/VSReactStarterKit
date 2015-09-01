namespace ExperimentApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        ParentCategoryId = c.Long(),
                    })
                .PrimaryKey(t => t.CategoryId)
                .ForeignKey("dbo.Categories", t => t.ParentCategoryId)
                .Index(t => t.ParentCategoryId);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        ItemId = c.Long(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        CreatedDateTime = c.DateTime(nullable: false),
                        CategoryId = c.Long(),
                    })
                .PrimaryKey(t => t.ItemId)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Galleries",
                c => new
                    {
                        GalleryId = c.Long(nullable: false, identity: true),
                        ResourceURI = c.String(),
                        ItemId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.GalleryId)
                .ForeignKey("dbo.Items", t => t.ItemId, cascadeDelete: true)
                .Index(t => t.ItemId);
            
            CreateTable(
                "dbo.ListingItems",
                c => new
                    {
                        ListingId = c.Long(nullable: false),
                        ItemId = c.Long(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ListingId, t.ItemId })
                .ForeignKey("dbo.Items", t => t.ItemId, cascadeDelete: true)
                .ForeignKey("dbo.Listings", t => t.ListingId, cascadeDelete: true)
                .Index(t => t.ListingId)
                .Index(t => t.ItemId);
            
            CreateTable(
                "dbo.Listings",
                c => new
                    {
                        ListingId = c.Long(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        UserId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.ListingId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Listings", "UserId", "dbo.Users");
            DropForeignKey("dbo.ListingItems", "ListingId", "dbo.Listings");
            DropForeignKey("dbo.ListingItems", "ItemId", "dbo.Items");
            DropForeignKey("dbo.Galleries", "ItemId", "dbo.Items");
            DropForeignKey("dbo.Items", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Categories", "ParentCategoryId", "dbo.Categories");
            DropIndex("dbo.Listings", new[] { "UserId" });
            DropIndex("dbo.ListingItems", new[] { "ItemId" });
            DropIndex("dbo.ListingItems", new[] { "ListingId" });
            DropIndex("dbo.Galleries", new[] { "ItemId" });
            DropIndex("dbo.Items", new[] { "CategoryId" });
            DropIndex("dbo.Categories", new[] { "ParentCategoryId" });
            DropTable("dbo.Users");
            DropTable("dbo.Listings");
            DropTable("dbo.ListingItems");
            DropTable("dbo.Galleries");
            DropTable("dbo.Items");
            DropTable("dbo.Categories");
        }
    }
}
