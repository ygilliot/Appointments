namespace Appointments.Migrations {
    using Api.Models;
    using Api.Utils;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Model;
    using System.Data.Entity.SqlServer;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Appointments.Api.Models.ApplicationDbContext> {
        public Configuration() {
            AutomaticMigrationsEnabled = false;
            SetSqlGenerator("System.Data.SqlClient", new CustomSqlServerMigrationSqlGenerator());
        }

        protected override void Seed(Appointments.Api.Models.ApplicationDbContext context) {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext("DefaultConnection")));

            context.Roles.AddOrUpdate(o => o.Name,
                new Microsoft.AspNet.Identity.EntityFramework.IdentityRole(AppRoles.Admin),
                new Microsoft.AspNet.Identity.EntityFramework.IdentityRole(AppRoles.Manager),
                new Microsoft.AspNet.Identity.EntityFramework.IdentityRole(AppRoles.Client));

            //Init with super admin account
            #if DEBUG
            if (!(context.Users.Any(u => u.UserName == "admin@admin.com"))) {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                var userToInsert = new ApplicationUser { UserName = "admin@admin.com", Email= "admin@admin.com", EmailConfirmed= true, PhoneNumber = "0612345678",
                    Person = new Person() {
                        Gender = "Mr.",
                        FirstName = "Super",
                        LastName = "Admin"
                    }
                };
                userManager.Create(userToInsert, "Password@123");

                var currentUser = userManager.FindByName(userToInsert.UserName);
                userManager.AddToRole(currentUser.Id, AppRoles.Admin);
            }
#else
            if (!(context.Users.Any(u => u.UserName == "admin@admin.com"))) {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                var userToInsert = new ApplicationUser { UserName = "admin@admin.com", Email= "admin@admin.com", PhoneNumber = "0612345678",
                    Person = new Person() {
                        Gender = "Mr.",
                        FirstName = "Super",
                        LastName = "Admin"
                    } };
                userManager.Create(userToInsert, "Password@123");

                var currentUser = userManager.FindByName(userToInsert.UserName);
                userManager.AddToRole(currentUser.Id, AppRoles.Admin);
            }
#endif

        }
    }

    internal class CustomSqlServerMigrationSqlGenerator : SqlServerMigrationSqlGenerator {
        protected override void Generate(AddColumnOperation addColumnOperation) {
            SetCreatedUtcColumn(addColumnOperation.Column);

            base.Generate(addColumnOperation);
        }

        protected override void Generate(CreateTableOperation createTableOperation) {
            SetCreatedUtcColumn(createTableOperation.Columns);

            base.Generate(createTableOperation);
        }

        private static void SetCreatedUtcColumn(IEnumerable<ColumnModel> columns) {
            foreach (var columnModel in columns) {
                SetCreatedUtcColumn(columnModel);
            }
        }

        private static void SetCreatedUtcColumn(PropertyModel column) {
            if (column.Name == "CreatedUtc" || column.Name == "LastUpdateUtc") {
                column.DefaultValueSql = "GETUTCDATE()";
            }
        }


    }
}
