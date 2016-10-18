using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Appointments.Api.Models.DTO {
    /// <summary>
    /// Appointment transportation model
    /// </summary>
    public class AppointmentDTO {
        #region Properties
        public long Id { get; set; }

        /// <summary>
        /// Collaborater required for the <see cref="Appointment"/>
        /// </summary>
        public PersonDTO Collaborater { get; set; }

        /// <summary>
        /// Client asking for an appointment
        /// </summary>
        public PersonDTO Client { get; set; }

        /// <summary>
        /// Appointment Status
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public AppointmentStatus Status { get; set; }

        /// <summary>
        /// Appointment start date and time
        /// </summary>
        [Required]
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Appointment start date and time
        /// </summary>
        [Required]
        public DateTime? EndDate { get; set; }
        #endregion

        #region Constructor
        public AppointmentDTO() { }

        public AppointmentDTO(Appointment a) {
            Id = a.Id;
            Collaborater = new PersonDTO(a.Collaborater);
            Client = new PersonDTO(a.Client);
            Status = a.Status;
            StartDate = a.StartDate;
            EndDate = a.EndDate;
        }
        #endregion
    }
}