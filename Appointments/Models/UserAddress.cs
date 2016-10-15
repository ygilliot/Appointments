using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Appointments.Api.Models {
    /// <summary>
    /// Full Address of a user
    /// </summary>
    public class UserAddress {
        /// <summary>
        /// UserName
        /// </summary>
        [ForeignKey("Person")]
        public string Id { get; set; }

        /// <summary>
        /// Address first line
        /// </summary>
        public string Address1 { get; set; }

        /// <summary>
        /// Address second line
        /// </summary>
        public string Address2 { get; set; }

        /// <summary>
        /// City
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Zip Code
        /// </summary>
        public int Zipcode { get; set; }

        /// <summary>
        /// State of the Country
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Country
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Link to User Information
        /// </summary>
        public virtual Person Person { get; set; }
    }
}