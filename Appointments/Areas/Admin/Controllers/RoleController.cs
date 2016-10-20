using Appointments.Api.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X.PagedList;
using Microsoft.AspNet.Identity.Owin;
using Appointments.Api.Areas.Admin.Models;
using System.Net;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Appointments.Api.Areas.Admin.Controllers
{
    [Authorize(Roles = AppRoles.Admin)]
    public class RoleController : Controller
    {
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;
        private int intPageSize = 25;

        // GET: Admin/Role
        public ActionResult Index(int? page)
        {
            int intPage = page ?? 1;

            try {

                //Get Users
                var roles = RoleManager.Roles
                    .OrderBy(x => x.Name)
                    .Select(x => new RoleDTO() {
                        Id = x.Id,
                        RoleName = x.Name
                    });

                ViewBag.Roles = roles.ToPagedList(intPage, intPageSize);
                return View();
            }
            catch (Exception ex) {
                ModelState.AddModelError(string.Empty, "Error: " + ex);
                List<RoleDTO> col_UserDTO = new List<RoleDTO>();
                ViewBag.Roles = col_UserDTO.ToPagedList(1, intPageSize);
                return View();
            }
        }

        // GET: Admin/Role/Add
        public ActionResult Add() {
            RoleDTO role = new RoleDTO();
            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(RoleDTO paramRoleDTO) {
            try {
                if (paramRoleDTO == null) {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var RoleName = paramRoleDTO.RoleName.Trim();
                if (string.IsNullOrEmpty(RoleName)) {
                    throw new Exception("No RoleName");
                }
                // Create Role
                if (!RoleManager.RoleExists(RoleName)) {
                    RoleManager.Create(new IdentityRole(RoleName));
                }
                return Redirect("~/Admin/ViewAllRoles");
            }
            catch (Exception ex) {
                ModelState.AddModelError(string.Empty, "Error: " + ex);
                return View();
            }
        }

        // DELETE: /Admin/Role/Delete?roleName=TestRole
        public ActionResult Delete(string roleName) {
            try {
                if (roleName == null) {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                if (roleName.ToLower() == AppRoles.Admin) {
                    throw new Exception(String.Format("Cannot delete {0} Role.", roleName));
                }

                var UsersInRole = RoleManager.FindByName(roleName).Users.Count();
                if (UsersInRole > 0) {
                    throw new Exception(String.Format("Canot delete {0} Role because it still has users.", roleName));
                }
                var objRoleToDelete = (from objRole in RoleManager.Roles
                                       where objRole.Name == roleName
                                       select objRole).FirstOrDefault();
                if (objRoleToDelete != null) {
                    RoleManager.Delete(objRoleToDelete);
                }
                else {
                    throw new Exception(String.Format("Cannot delete {0} Role does not exist.", roleName));
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError(string.Empty, "Error: " + ex);
                
            }

            return RedirectToAction("Index");
        }

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