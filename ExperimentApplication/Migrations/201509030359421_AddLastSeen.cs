namespace ExperimentApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLastSeen : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "LastSeen", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "LastSeen");
        }
    }
}
