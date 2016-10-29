using Appointments.Api.Repositories;
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

    /// <summary>
    /// Extension methods for AppointmentDTO class
    /// </summary>
    public static class AppointmentDTOExtension {
        #region Transformation to database model
        /// <summary>
        /// Cast a DTO to its model version
        /// </summary>
        /// <param name="dto">object to cast</param>
        /// <param name="personRepo">repository used to convert the DTO</param>
        /// <returns>model version</returns>
        public static Appointment ToModel(this AppointmentDTO dto, IRepository<Person> personRepo) {

            var client = personRepo.All().FirstOrDefault(p => p.ApplicationUser.UserName == dto.Client.UserName);
            var collaborater = personRepo.All().FirstOrDefault(p => p.ApplicationUser.UserName == dto.Collaborater.UserName);

            return new Appointment() {
                Id = dto.Id,
                ClientId = client.Id,
                Client = client,
                CollaboraterId = collaborater.Id,
                Collaborater = collaborater,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Status = dto.Status
            };
        }
        #endregion
    }
}