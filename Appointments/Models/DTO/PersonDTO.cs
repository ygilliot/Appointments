using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appointments.Api.Models.DTO {
    /// <summary>
    /// Person Simplified model
    /// </summary>
    public class PersonDTO {
        #region ASP.Net Identity
        // Don't Show Id for public
        ///// <summary>
        ///// Person Identifier
        ///// </summary>
        //public string Id { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Phone Number
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// User Name is also identifier (Email)
        /// </summary>
        public string UserName { get; set; }
        #endregion

        #region Person
        /// <summary>
        /// First Name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last Name
        /// </summary>
        public string LastName { get; set; }
        #endregion

        #region Constructor
        public PersonDTO() { }

        public PersonDTO(Person p) {
            //Id = p.Id;
            UserName = p.ApplicationUser.UserName;
            Email = p.ApplicationUser.Email;
            PhoneNumber = p.ApplicationUser.PhoneNumber;
            FirstName = p.FirstName;
            LastName = p.LastName;
        }
        #endregion
    }
}