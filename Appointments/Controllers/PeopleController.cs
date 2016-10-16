using Appointments.Api.Models;
using Appointments.Api.Repositories;
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
        public IQueryable<Person> Get() {
                return this.peopleRepository.All();
        }

        /// <summary>
        /// Get a person by its username
        /// </summary>
        /// <param name="username">email</param>
        /// <returns></returns>
        public Person Get(string username) {
            return this.peopleRepository.All().FirstOrDefault(o => o.Id == username);
        }
    }
}
