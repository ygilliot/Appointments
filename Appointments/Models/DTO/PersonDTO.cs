using Appointments.Api.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Appointments.Api.Models.DTO {
    /// <summary>
    /// Person Simplified model
    /// </summary>
    public class PersonDTO {
        #region ASP.Net Identity
        // Don't Show Id to public
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
        [Required]
        public string UserName { get; set; }
        #endregion

        #region Person
        /// <summary>
        /// First Name
        /// </summary>
        [MaxLength(128)]
        public string FirstName { get; set; }

        /// <summary>
        /// Last Name
        /// </summary>
        [MaxLength(128)]
        public string LastName { get; set; }

        /// <summary>
        /// Mr. Ms. Mrs.
        /// </summary>
        public string Gender { get; set; }
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

    /// <summary>
    /// Extension methods for PersonDTO class
    /// </summary>
    public static class PersonDTOExtension {
        #region Transformation to database model
        /// <summary>
        /// Cast a DTO to its model version
        /// </summary>
        /// <param name="dto">object to cast</param>
        /// <param name="userId">user identifier</param>
        /// <returns>model version</returns>
        public static Person ToModel(this PersonDTO dto, string userId) {
            
            return new Person() {
                Id = userId,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Gender = dto.Gender
            };
        }
        #endregion
    }
}