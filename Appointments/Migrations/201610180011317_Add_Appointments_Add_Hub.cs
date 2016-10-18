namespace Appointments.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Appointments_Add_Hub : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Appointments",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Status = c.Int(nullable: false),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        CreatedUtc = c.DateTime(nullable: false),
                        Client_Id = c.String(maxLength: 128),
                        Collaborater_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.Client_Id)
                .ForeignKey("dbo.People", t => t.Collaborater_Id)
                .Index(t => t.Client_Id)
                .Index(t => t.Collaborater_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Appointments", "Collaborater_Id", "dbo.People");
            DropForeignKey("dbo.Appointments", "Client_Id", "dbo.People");
            DropIndex("dbo.Appointments", new[] { "Collaborater_Id" });
            DropIndex("dbo.Appointments", new[] { "Client_Id" });
            DropTable("dbo.Appointments");
        }
    }
}
