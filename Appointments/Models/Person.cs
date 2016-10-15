using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Appointments.Api.Models {
    public class Person {
        [ForeignKey("ApplicationUser")]
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public virtual UserAddress Address { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}