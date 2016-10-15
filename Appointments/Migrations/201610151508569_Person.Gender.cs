namespace Appointments.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PersonGender : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.People", "Gender", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.People", "Gender");
        }
    }
}
