namespace Appointments.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TrackedEntity_Default_datetime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Appointments", "CreatedUtc", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Appointments", "LastUpdateUtc", c => c.DateTime(nullable: false));
            AlterColumn("dbo.People", "CreatedUtc", c => c.DateTime(nullable: false));
            AlterColumn("dbo.People", "LastUpdateUtc", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.People", "LastUpdateUtc", c => c.DateTime());
            AlterColumn("dbo.People", "CreatedUtc", c => c.DateTime());
            AlterColumn("dbo.Appointments", "LastUpdateUtc", c => c.DateTime());
            AlterColumn("dbo.Appointments", "CreatedUtc", c => c.DateTime());
        }
    }
}
