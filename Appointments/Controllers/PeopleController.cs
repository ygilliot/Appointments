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
        public PeopleController(IRepository<Person> peopleRepository) {
            this.peopleRepository = peopleRepository;
        }

        /// <summary>
        /// Get all people
        /// </summary>
        /// <returns>List of all <see cref="Person"/> registered </returns>
        [VersionedRoute("api/{version}/People", "1.0")]
        [VersionedRoute("api/People")]
        public IQueryable<PersonDTO> Get() {
            return this.peopleRepository.All().Select(p => new PersonDTO() {
                Id = p.Id,
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
            Person p = this.peopleRepository.All().FirstOrDefault(o => o.Id == username);
            return p != null ? new PersonDTO(p) : null;
        }
    }
}
