namespace Appointments.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixZipCodeCreatedUTC : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.People", "CreatedUtc", c => c.DateTime(nullable: false));
            AlterColumn("dbo.UserAddresses", "Zipcode", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserAddresses", "Zipcode", c => c.Int(nullable: false));
            DropColumn("dbo.People", "CreatedUtc");
        }
    }
}
