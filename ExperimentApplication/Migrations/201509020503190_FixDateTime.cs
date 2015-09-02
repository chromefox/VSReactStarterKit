namespace ExperimentApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixDateTime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Items", "CreatedDateTime", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.ListingItems", "DateCreated", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ListingItems", "DateCreated", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Items", "CreatedDateTime", c => c.DateTime(nullable: false));
        }
    }
}
