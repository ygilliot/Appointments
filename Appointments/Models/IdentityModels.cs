using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using EntityFramework.DynamicFilters;
using System.Linq;
using System;
using System.Web;

namespace Appointments.Api.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.

    /// <summary>
    /// Application User
    /// </summary>
    public class ApplicationUser : IdentityUser {
        /// <summary>
        /// Contains detailed user information
        /// </summary>
        public virtual Person Person { get; set; }

        /// <summary>
        /// Generate UserIdentity Asynchronously
        /// </summary>
        /// <param name="manager">user manager</param>
        /// <param name="authenticationType"></param>
        /// <returns></returns>
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }

    /// <summary>
    /// ApplicationDbContext
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        //uncomment only for running Seed method
        public ApplicationDbContext() { }

        /// <summary>
        /// Constructor
        /// </summary>
        public ApplicationDbContext(string connectionstring)
    : base(connectionstring, throwIfV1Schema: false) {
        }

        /// <summary>
        /// Creates Db Context
        /// </summary>
        /// <returns></returns>
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext("DefaultConnection");
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            //Prevent retreiving Soft Deleted items from contxt
            modelBuilder.Filter(nameof(TrackedEntityBase.IsDeleted), (TrackedEntityBase d) => d.IsDeleted, false);
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges() {
            var changeSet = ChangeTracker.Entries<TrackedEntityBase>();

            if (changeSet != null) {
                foreach (var entry in changeSet.Where(c => c.State != EntityState.Unchanged)) {
                    //Update date and User for Tracked Entities
                    entry.Entity.LastUpdateUtc = DateTime.UtcNow;
                    entry.Entity.UpdaterId = HttpContext.Current?.User?.Identity?.Name;

                    //Soft Delete implementation
                    if (entry.State == EntityState.Deleted) {
                        entry.Entity.IsDeleted = true;
                        entry.State = EntityState.Modified;
                    }
                }
            }
            return base.SaveChanges();
        }

        /// <summary>
        /// Database set of Persons
        /// </summary>
        public DbSet<Person> Persons { get; set; }
        /// <summary>
        /// Database set of User Addresses
        /// </summary>
        public DbSet<UserAddress> UserAddresses { get; set; }

        /// <summary>
        /// Database set of Appointments
        /// </summary>
        public DbSet<Appointment> Appointments { get; set; }
    }
}