using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Appointments.Api.Models {
    /// <summary>
    /// User Personal information
    /// </summary>
    public class Appointment : TrackedEntityBase {

        /// <summary>
        /// Appointment Identifier
        /// </summary>
        public long Id { get; set; }

        [ForeignKey("Collaborater")]
        public string CollaboraterId { get; set; }
        /// <summary>
        /// Collaborater required for the <see cref="Appointment"/>
        /// </summary>
        public virtual Person Collaborater { get; set; }

        [ForeignKey("Client")]
        public string ClientId { get; set; }
        /// <summary>
        /// Client asking for an appointment
        /// </summary>
        public virtual Person Client { get; set; }
        
        /// <summary>
        /// Appointment Status
        /// </summary>
        public AppointmentStatus Status { get; set; }

        /// <summary>
        /// Appointment start date and time
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Appointment start date and time
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Client can left a note to the Collaborater
        /// </summary>
        public string Note { get; set; }
    }
    
    public enum AppointmentStatus {
        Pending = 0,//Used if app is configured with validation
        Valid = 1,//Default value is app is configured without validation
        Removed = -1
    }
}