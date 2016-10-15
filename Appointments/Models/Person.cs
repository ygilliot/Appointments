using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Appointments.Api.Models {
    /// <summary>
    /// User Personal information
    /// </summary>
    public class Person {
        /// <summary>
        /// UserName
        /// </summary>
        [ForeignKey("ApplicationUser")]
        public string Id { get; set; }

        /// <summary>
        /// Mr. Ms. Mrs.
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// First Name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last Name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Complex Address
        /// </summary>
        public virtual UserAddress Address { get; set; }

        /// <summary>
        /// Application User (technical)
        /// </summary>
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}