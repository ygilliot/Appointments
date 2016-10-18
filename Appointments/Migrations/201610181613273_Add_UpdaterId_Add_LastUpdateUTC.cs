namespace Appointments.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_UpdaterId_Add_LastUpdateUTC : DbMigration
    {
        public override void Up()
        {
            AlterTableAnnotations(
                "dbo.Appointments",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Status = c.Int(nullable: false),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        CreatedUtc = c.DateTime(nullable: false),
                        LastUpdateUtc = c.DateTime(nullable: false),
                        UpdaterId = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        Client_Id = c.String(maxLength: 128),
                        Collaborater_Id = c.String(maxLength: 128),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_Appointment_IsDeleted",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AlterTableAnnotations(
                "dbo.People",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Gender = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        CreatedUtc = c.DateTime(nullable: false),
                        LastUpdateUtc = c.DateTime(nullable: false),
                        UpdaterId = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_Person_IsDeleted",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AddColumn("dbo.Appointments", "LastUpdateUtc", c => c.DateTime(nullable: false));
            AddColumn("dbo.Appointments", "UpdaterId", c => c.String());
            AddColumn("dbo.Appointments", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.People", "LastUpdateUtc", c => c.DateTime(nullable: false));
            AddColumn("dbo.People", "UpdaterId", c => c.String());
            AddColumn("dbo.People", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.People", "IsDeleted");
            DropColumn("dbo.People", "UpdaterId");
            DropColumn("dbo.People", "LastUpdateUtc");
            DropColumn("dbo.Appointments", "IsDeleted");
            DropColumn("dbo.Appointments", "UpdaterId");
            DropColumn("dbo.Appointments", "LastUpdateUtc");
            AlterTableAnnotations(
                "dbo.People",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Gender = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        CreatedUtc = c.DateTime(nullable: false),
                        LastUpdateUtc = c.DateTime(nullable: false),
                        UpdaterId = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_Person_IsDeleted",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
                "dbo.Appointments",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Status = c.Int(nullable: false),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        CreatedUtc = c.DateTime(nullable: false),
                        LastUpdateUtc = c.DateTime(nullable: false),
                        UpdaterId = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        Client_Id = c.String(maxLength: 128),
                        Collaborater_Id = c.String(maxLength: 128),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_Appointment_IsDeleted",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
        }
    }
}
