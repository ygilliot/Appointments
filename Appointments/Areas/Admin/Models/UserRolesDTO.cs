using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Appointments.Api.Areas.Admin.Models {
        public class ExpandedUserDTO {
            [Key]
            [Display(Name = "User Name")]
            public string UserName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            [Display(Name = "Lockout End Date Utc")]
            public DateTime? LockoutEndDateUtc { get; set; }
            public int AccessFailedCount { get; set; }
            public string PhoneNumber { get; set; }
            public IEnumerable Roles { get; set; }
        }
        public class UserRolesDTO {
            [Key]
            [Display(Name = "Role Name")]
            public string RoleName { get; set; }
        }
        public class UserRoleDTO {
            [Key]
            [Display(Name = "User Name")]
            public string UserName { get; set; }
            [Display(Name = "Role Name")]
            public string RoleName { get; set; }
        }
        public class RoleDTO {
            [Key]
            public string Id { get; set; }
            [Display(Name = "Role Name")]
            public string RoleName { get; set; }
        }
        public class UserAndRolesDTO {
            [Key]
            [Display(Name = "User Name")]
            public string UserName { get; set; }
            public IEnumerable<UserRoleDTO> colUserRoleDTO { get; set; }
        }
}