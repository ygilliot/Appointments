using Appointments.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Appointments.Api.Controllers
{
    /// <summary>
    /// People Controller
    /// </summary>
    [Authorize]
    public class PeopleController : ApiController
    {
        /// <summary>
        /// Get all people
        /// </summary>
        /// <returns>List of all <see cref="Person"/> registered </returns>
        public IEnumerable<Person> Get() {
            using (ApplicationDbContext ctx = new ApplicationDbContext()) {
                return ctx.Persons.ToList();
            }
        }
    }
}
