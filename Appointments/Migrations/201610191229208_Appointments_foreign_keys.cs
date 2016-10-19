namespace Appointments.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Appointments_foreign_keys : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Appointments", name: "Client_Id", newName: "ClientId");
            RenameColumn(table: "dbo.Appointments", name: "Collaborater_Id", newName: "CollaboraterId");
            RenameIndex(table: "dbo.Appointments", name: "IX_Collaborater_Id", newName: "IX_CollaboraterId");
            RenameIndex(table: "dbo.Appointments", name: "IX_Client_Id", newName: "IX_ClientId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Appointments", name: "IX_ClientId", newName: "IX_Client_Id");
            RenameIndex(table: "dbo.Appointments", name: "IX_CollaboraterId", newName: "IX_Collaborater_Id");
            RenameColumn(table: "dbo.Appointments", name: "CollaboraterId", newName: "Collaborater_Id");
            RenameColumn(table: "dbo.Appointments", name: "ClientId", newName: "Client_Id");
        }
    }
}
