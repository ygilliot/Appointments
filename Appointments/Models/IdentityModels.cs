using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity;

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
        /// <summary>
        /// Constructor
        /// </summary>
        //public ApplicationDbContext()
        //    : base("DefaultConnection", throwIfV1Schema: false)
        //{
        //}
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

        /// <summary>
        /// Database set of Persons
        /// </summary>
        public DbSet<Person> Persons { get; set; }
        /// <summary>
        /// Database set of User Addresses
        /// </summary>
        public DbSet<UserAddress> UserAddresses { get; set; }
    }
}