using Appointments.Api.Areas.Admin.Models;
using Appointments.Api.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X.PagedList;
using Microsoft.AspNet.Identity.Owin;

namespace Appointments.Api.Areas.Admin.Controllers {
    [Authorize(Roles = AppRoles.Admin)]
    public class UserController : Controller {
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;

        // Controllers
        // GET: /User/
        #region public ActionResult Index(string searchStringUserNameOrEmail)
        public ActionResult Index(string searchStringUserNameOrEmail, string currentFilter, int? page) {
            int intPage = 1;
            int intPageSize = 25;

            try {
                if (searchStringUserNameOrEmail != null) {
                    intPage = 1;
                }
                else {
                    if (currentFilter != null) {
                        searchStringUserNameOrEmail = currentFilter;
                        intPage = page ?? 1;
                    }
                    else {
                        searchStringUserNameOrEmail = "";
                        intPage = page ?? 1;
                    }
                }
                ViewBag.CurrentFilter = searchStringUserNameOrEmail;

                var users = UserManager.Users
                    .Where(x => x.UserName.Contains(searchStringUserNameOrEmail))
                    .OrderBy(x => x.UserName)
                    .Select( x => new ExpandedUserDTO() {
                        UserName = x.UserName,
                        Email = x.Email,
                        PhoneNumber = x.PhoneNumber,
                        LockoutEndDateUtc = x.LockoutEndDateUtc
                    });
                
                ViewBag.ExpandedUsers = users.ToPagedList(intPage, intPageSize);
                return View();
            }
            catch (Exception ex) {
                ModelState.AddModelError(string.Empty, "Error: " + ex);
                List<ExpandedUserDTO> col_UserDTO = new List<ExpandedUserDTO>();
                ViewBag.ExpandedUsers = col_UserDTO.ToPagedList(1, intPageSize);
                return View();
            }
        }
        #endregion

        // Utility
        #region public ApplicationUserManager UserManager
        public ApplicationUserManager UserManager {
            get {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set {
                _userManager = value;
            }
        }
        #endregion
        #region public ApplicationRoleManager RoleManager
        public ApplicationRoleManager RoleManager {
            get {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set {
                _roleManager = value;
            }
        }
        #endregion
    }
}