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
                new Microsoft.AspNet.Identity.EntityFramework.IdentityRole(AppRoles.Collaborater),
                new Microsoft.AspNet.Identity.EntityFramework.IdentityRole(AppRoles.Client));

            #region Users
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

            if (!(context.Users.Any(u => u.UserName == "manager@store.com"))) {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                var userToInsert = new ApplicationUser {
                    UserName = "manager@store.com",
                    Email = "manager@store.com",
                    EmailConfirmed = true,
                    PhoneNumber = "0612345678",
                    Person = new Person() {
                        Gender = "Ms.",
                        FirstName = "Manager",
                        LastName = "Test"
                    }
                };
                userManager.Create(userToInsert, "Password@123");

                var currentUser = userManager.FindByName(userToInsert.UserName);
                userManager.AddToRole(currentUser.Id, AppRoles.Manager);
            }

            if (!(context.Users.Any(u => u.UserName == "collaborater@store.com"))) {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                var userToInsert = new ApplicationUser {
                    UserName = "collaborater@store.com",
                    Email = "collaborater@store.com",
                    EmailConfirmed = true,
                    PhoneNumber = "0612345678",
                    Person = new Person() {
                        Gender = "Mr.",
                        FirstName = "Collaborater",
                        LastName = "Test"
                    }
                };
                userManager.Create(userToInsert, "Password@123");

                var currentUser = userManager.FindByName(userToInsert.UserName);
                userManager.AddToRole(currentUser.Id, AppRoles.Collaborater);
            }

            if (!(context.Users.Any(u => u.UserName == "john.doe@client.com"))) {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                var userToInsert = new ApplicationUser {
                    UserName = "john.doe@client.com",
                    Email = "john.doe@client.com",
                    EmailConfirmed = true,
                    PhoneNumber = "0612345678",
                    Person = new Person() {
                        Gender = "Mr.",
                        FirstName = "John",
                        LastName = "Doe",
                        Address = new UserAddress() {
                            Address1 = "CENTER FOR FINANCIAL ASSISTANCE TO DEPOSED NIGERIAN ROYALTY",
                            Address2 = "421 E DRACHMAN",
                            City = "TUCSON",
                            State = "AZ",
                            Zipcode = "85705",
                            Country = "United States"
                        }
                    }
                };
                userManager.Create(userToInsert, "Password@123");

                var currentUser = userManager.FindByName(userToInsert.UserName);
                userManager.AddToRole(currentUser.Id, AppRoles.Collaborater);
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
            #endregion

            #region Appointments
#if DEBUG
            context.Appointments.AddOrUpdate(o => new { o.UpdaterId },
                new Appointment() {
                    Client = context.Users.FirstOrDefault(u => u.UserName == "john.doe@client.com").Person,
                    Collaborater = context.Users.FirstOrDefault(u => u.UserName == "collaborater@store.com").Person,
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddHours(1),
                    UpdaterId = "admin@admin.com"
                },
                new Appointment() {
                    Client = context.Users.FirstOrDefault(u => u.UserName == "john.doe@client.com").Person,
                    Collaborater = context.Users.FirstOrDefault(u => u.UserName == "manager@store.com").Person,
                    StartDate = DateTime.UtcNow.AddDays(1),
                    EndDate = DateTime.UtcNow.AddDays(1).AddHours(1),
                    UpdaterId = "john.doe@client.com"
                });
            #endif
            #endregion
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
