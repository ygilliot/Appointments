namespace Appointments.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Appointment_Note : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointments", "Note", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Appointments", "Note");
        }
    }
}
