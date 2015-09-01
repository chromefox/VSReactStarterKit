namespace ExperimentApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixListingItem : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.ListingItems");
            AddColumn("dbo.ListingItems", "ListingItemId", c => c.Long(nullable: false, identity: true));
            AddPrimaryKey("dbo.ListingItems", "ListingItemId");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.ListingItems");
            DropColumn("dbo.ListingItems", "ListingItemId");
            AddPrimaryKey("dbo.ListingItems", new[] { "ListingId", "ItemId" });
        }
    }
}
