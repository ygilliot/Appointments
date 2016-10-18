using Appointments.Api.Models;
using Appointments.Api.Repositories;
using Appointments.Api.Utils;
using Appointments.Api.Utils.Filters;
using Appointments.Api.Versioning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.Results;

namespace Appointments.Api.Controllers
{
    /// <summary>
    /// Appointments Controller
    /// </summary>
    /// <typeparam name="CalendarHub">SignalR Hub for Calendar events</typeparam>
    [Authorize]
    public class AppointmentsController<CalendarHub> : ApiHubController
    {
        private readonly IRepository<Appointment> appointmentsRepository;

        /// <summary>
        /// Initialize Controller
        /// </summary>
        /// <param name="appointmentsRepository">repository of appointments</param>
        public AppointmentsController(IRepository<Appointment> appointmentsRepository) {
            this.appointmentsRepository = appointmentsRepository;
        }

        [EnableQuery]
        [Authorize(Roles = AppRoles.Admin +","+ AppRoles.Manager)]
        [VersionedRoute("api/{version}/Appointments", "1.0")]
        [VersionedRoute("api/Appointments")]
        public IQueryable<Appointment> Get() {
            return appointmentsRepository.All();
        }

        
        [EnableQuery]
        [VersionedRoute("api/{version}/Collaboraters/{userName}/Appointments", "1.0")]
        [VersionedRoute("api/Collaboraters/{userName}/Appointments")]
        public IQueryable<Appointment> Get(string userName) {
            return appointmentsRepository.All().Where(o=>o.Collaborater != null && o.Collaborater.ApplicationUser.UserName == userName);
        }

        [ValidateModel]
        [VersionedRoute("api/{version}/Collaboraters/{collaboraterId}/Appointments", "1.0")]
        [VersionedRoute("api/Collaboraters/{collaboraterId}/Appointments")]
        public Appointment Post(string collaboraterId, Appointment appointment) {
            
            //if(ModelState.IsValid)
            appointmentsRepository.Add(appointment);


            //var subscribed = Hub.Clients.Group(collaboraterId);
            //subscribed.addItem(appointment);

            //return Ok(appointment);
            return appointment;
        }
    }
}