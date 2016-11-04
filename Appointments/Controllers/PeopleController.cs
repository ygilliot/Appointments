using Appointments.Api.Models;
using Appointments.Api.Models.DTO;
using Appointments.Api.Repositories;
using Appointments.Api.Utils;
using Appointments.Api.Utils.Filters;
using Appointments.Api.Versioning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.OData;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Web;

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
        [Authorize(Roles = AppRoles.Admin + "," + AppRoles.Manager)]
        [VersionedRoute("api/{version}/People", "1.0")]
        [VersionedRoute("api/People")]
        public IQueryable<PersonDTO> Get() {
            return this.peopleRepository.All().Select(p => new PersonDTO() {
                //Id = p.Id,
                UserName = p.ApplicationUser.UserName,
                Email = p.ApplicationUser.Email,
                PhoneNumber = p.ApplicationUser.PhoneNumber,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Gender = p.Gender
            });
        }

        /// <summary>
        /// Get a person by its username
        /// </summary>
        /// <param name="username">email</param>
        /// <returns></returns>
        [Authorize(Roles = AppRoles.Admin + "," + AppRoles.Manager + "," + AppRoles.Collaborater)]
        [VersionedRoute("api/{version}/People/{username}", "1.0")]
        [VersionedRoute("api/People/{username}")]
        public IHttpActionResult Get(string username) {
            Person p = this.peopleRepository.All().FirstOrDefault(o => o.ApplicationUser.UserName == username);
            if (p != null)
                return Ok(new PersonExtendedDTO(p));

            return NotFound();
        }

        /// <summary>
        /// Creates a new Person
        /// </summary>
        /// <param name="person">The person to create</param>
        /// <returns>The newly created person</returns>
        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = AppRoles.Admin)]
        [VersionedRoute("api/{version}/People", "1.0")]
        [VersionedRoute("api/People")]
        public IHttpActionResult Post(PersonExtendedDTO person) {
            //Get user associated
            ApplicationUser user = Request.GetOwinContext().GetUserManager<ApplicationUserManager>().FindByName(person.UserName);

            #region Validation
            if (user == null)
                return NotFound();
            if (user.Person != null)
                return BadRequest("Person you try to create already exists!");
            #endregion

            //Cast for database storage
            Person model = person.ToModel(user);

            //Insert in db
            peopleRepository.Add(model);
            peopleRepository.Save();

            //Cast for transport
            PersonExtendedDTO result = new PersonExtendedDTO(model);

            return Ok(result);
        }


        /// <summary>
        /// Updates an existing Person by replacing ALL values
        /// </summary>
        /// <param name="person">The person with updated properties</param>
        /// <returns>The updated person</returns>
        [HttpPut]
        [ValidateModel]
        [Authorize] //Everyone can update at least himself/herself
        [VersionedRoute("api/{version}/People", "1.0")]
        [VersionedRoute("api/People")]
        public IHttpActionResult Put(PersonExtendedDTO person) {
            //If not admin, user can only edit himself/herself
            if (!User.IsInRole(AppRoles.Admin) && person.UserName != User.Identity.GetUserName())
                return BadRequest("You do not have sufficient rights to edit anyone but yourself");
            
            //Get user associated
            ApplicationUser user = Request.GetOwinContext().GetUserManager<ApplicationUserManager>().FindByName(person.UserName);

            if (user == null)
                return NotFound();

            //Cast for database storage
            Person model = person.ToModel(user);

            //Insert in db
            peopleRepository.Update(model);
            peopleRepository.Save();

            //Cast for transport
            PersonExtendedDTO result = new PersonExtendedDTO(model);

            return Ok(result);
        }

        /// <summary>
        /// Updates an existing Person by replacing only not null properties
        /// </summary>
        /// <param name="person">The person with updated properties</param>
        /// <returns>The updated person</returns>
        [HttpPatch]
        [ValidateModel]
        [Authorize] //Everyone can update at least himself/herself
        [VersionedRoute("api/{version}/People", "1.0")]
        [VersionedRoute("api/People")]
        public IHttpActionResult Patch(PersonExtendedDTO person) {
            //WARNING: from the moment Person has a non-nullable property, replace PersonExtendedDTO by PersonExtendedPatchDTO
            //If not admin, user can only edit himself/herself
            if (!User.IsInRole(AppRoles.Admin) && person.UserName != User.Identity.GetUserName())
                return BadRequest("You do not have sufficient rights to edit anyone but yourself");

            //Get user associated
            ApplicationUser user = Request.GetOwinContext().GetUserManager<ApplicationUserManager>().FindByName(person.UserName);

            if (user == null)
                return NotFound();

            //Cast for database storage
            Person model = person.ToPatchModel(user);

            //Insert in db
            peopleRepository.Update(model);
            peopleRepository.Save();

            //Cast for transport
            PersonExtendedDTO result = new PersonExtendedDTO(model);

            return Ok(result);
        }

    }
}
