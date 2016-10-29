using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appointments.Api.Models.DTO {
    /// <summary>
    /// Person with all data
    /// </summary>
    public class PersonExtendedDTO {

        #region ASP.Net Identity
        // Don't Show Id for public
        ///// <summary>
        ///// Person Identifier
        ///// </summary>
        //public string Id { get; set; }

        /// <summary>
        /// Email is also identifier
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Phone Number
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// User Name
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

        /// <summary>
        /// Mr. Ms. Mrs.
        /// </summary>
        public string Gender { get; set; }
        #endregion

        #region UserAddress
        /// <summary>
        /// First line of Address
        /// </summary>
        public string Address1 { get; set; }

        /// <summary>
        /// Second line of Address
        /// </summary>
        public string Address2 { get; set; }

        /// <summary>
        /// City
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Postal Code
        /// </summary>
        public string ZipCode { get; set; }

        /// <summary>
        /// State, if any
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Country
        /// </summary>
        public string Country { get; set; }
        #endregion

        #region Constructor
        public PersonExtendedDTO() { }

        public PersonExtendedDTO(Person p) {
            //Id = p.Id;
            UserName = p.ApplicationUser.UserName;
            Email = p.ApplicationUser.Email;
            PhoneNumber = p.ApplicationUser.PhoneNumber;
            FirstName = p.FirstName;
            LastName = p.LastName;

            //If Address is available fill model
            if (p.Address != null) {
                Address1 = p.Address.Address1;
                Address2 = p.Address.Address2;
                City = p.Address.City;
                ZipCode = p.Address.Zipcode;
                State = p.Address.State;
                Country = p.Address.Country;
            }
        }
        #endregion
    }
}