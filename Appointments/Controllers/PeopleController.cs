using Appointments.Api.Models;
using Appointments.Api.Models.DTO;
using Appointments.Api.Repositories;
using Appointments.Api.Versioning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.OData;

namespace Appointments.Api.Controllers {
    /// <summary>
    /// People Controller
    /// </summary>
    [Authorize]
    public class PeopleController : ApiController {
        private readonly IRepository<Person> peopleRepository;

        /// <summary>
        /// Initialize Controller
        /// </summary>
        /// <param name="peopleRepository">people repository</param>
        ///// <param name="userRepository">user repository</param>
        public PeopleController(IRepository<Person> peopleRepository) {
            this.peopleRepository = peopleRepository;
        }

        /// <summary>
        /// Get all people
        /// This method is Queryable using OData format:
        /// $expand : Expands related entities inline.
        /// $filter : Filters the results, based on a Boolean condition.
        /// $inlinecount : Tells the server to include the total count of matching entities in the response. (Useful for server-side paging.)
        /// $orderBy : Sorts the results.
        /// $select : Selects which properties to include in the response.
        /// $skip : Skips the first n results.
        /// $top: Returns only the first n the results.
        /// </summary>
        /// <returns>List of all <see cref="Person"/> registered </returns>
        [EnableQuery]
        [VersionedRoute("api/{version}/People", "1.0")]
        [VersionedRoute("api/People")]
        public IQueryable<PersonDTO> Get() {
            return this.peopleRepository.All().Select(p => new PersonDTO() {
                //Id = p.Id,
                UserName = p.ApplicationUser.UserName,
                Email = p.ApplicationUser.Email,
                PhoneNumber = p.ApplicationUser.PhoneNumber,
                FirstName = p.FirstName,
                LastName = p.LastName
            });
        }

        /// <summary>
        /// Get a person by its username
        /// </summary>
        /// <param name="username">email</param>
        /// <returns></returns>
        [VersionedRoute("api/{version}/People/{username}", "1.0")]
        [VersionedRoute("api/People/{username}")]
        public PersonDTO Get(string username) {
            Person p = this.peopleRepository.All().FirstOrDefault(o => o.ApplicationUser.UserName == username);
            return p != null ? new PersonDTO(p) : null;
        }
    }
}
