using Appointments.Api.Areas.Admin.Models;
using Appointments.Api.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X.PagedList;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Net;
using Appointments.Api.Models;

namespace Appointments.Api.Areas.Admin.Controllers {
    [Authorize(Roles = AppRoles.Admin)]
    public class UserController : Controller {
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;

        #region USER
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

                //Get Users
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

        // GET: /Admin/User/Add
        public ActionResult Add() {
            ExpandedUserDTO objExpandedUserDTO = new ExpandedUserDTO();
            ViewBag.Roles = GetAllRolesAsSelectList();
            return View(objExpandedUserDTO);
        }

        // POST: /Admin/User/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        #region public ActionResult Add(ExpandedUserDTO paramExpandedUserDTO)
        public ActionResult Add(ExpandedUserDTO paramExpandedUserDTO) {
            try {
                if (paramExpandedUserDTO == null) {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var Email = paramExpandedUserDTO.Email.Trim();
                var UserName = paramExpandedUserDTO.Email.Trim();
                var Password = paramExpandedUserDTO.Password.Trim();
                if (Email == "") {
                    throw new Exception("No Email");
                }
                if (Password == "") {
                    throw new Exception("No Password");
                }
                // UserName is LowerCase of the Email
                UserName = Email.ToLower();
                // Create user
                var objNewAdminUser = new ApplicationUser { UserName = UserName, Email = Email };
                var AdminUserCreateResult = UserManager.Create(objNewAdminUser, Password);
                if (AdminUserCreateResult.Succeeded == true) {
                    string strNewRole = Convert.ToString(Request.Form["Roles"]);
                    if (strNewRole != "0") {
                        // Put user in role
                        UserManager.AddToRole(objNewAdminUser.Id, strNewRole);
                    }
                    return RedirectToAction("Index");
                }
                else {
                    ViewBag.Roles = GetAllRolesAsSelectList();
                    ModelState.AddModelError(string.Empty,
                        "Error: Failed to create the user. Check password requirements.");
                    return View(paramExpandedUserDTO);
                }
            }
            catch (Exception ex) {
                ViewBag.Roles = GetAllRolesAsSelectList();
                ModelState.AddModelError(string.Empty, "Error: " + ex);
                return View();
            }
        }
        #endregion

        // GET: /Admin/User/Edit 
        #region public ActionResult Edit(string userName)
        public ActionResult Edit(string userName) {
            if (userName == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpandedUserDTO objExpandedUserDTO = GetUser(userName);
            if (objExpandedUserDTO == null) {
                return HttpNotFound();
            }
            return View(objExpandedUserDTO);
        }
        #endregion

        // POST: /Admin/EditUser
        [HttpPost]
        [ValidateAntiForgeryToken]
        #region public ActionResult Edit(ExpandedUserDTO paramExpandedUserDTO)
        public ActionResult Edit(ExpandedUserDTO paramExpandedUserDTO) {
            try {
                if (paramExpandedUserDTO == null) {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ExpandedUserDTO objExpandedUserDTO = UpdateDTOUser(paramExpandedUserDTO);
                if (objExpandedUserDTO == null) {
                    return HttpNotFound();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex) {
                ModelState.AddModelError(string.Empty, "Error: " + ex);
                return View("Edit", GetUser(paramExpandedUserDTO.UserName));
            }
        }
        #endregion

        // DELETE: /Admin/User/Delete
        #region public ActionResult Delete(string userName)
        public ActionResult Delete(string userName) {
            try {
                if (userName == null) {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                if (userName.ToLower() == this.User.Identity.Name.ToLower()) {
                    ModelState.AddModelError(
                        string.Empty, "Error: Cannot delete the current user");
                    return View("EditUser");
                }
                ExpandedUserDTO objExpandedUserDTO = GetUser(userName);
                if (objExpandedUserDTO == null) {
                    return HttpNotFound();
                }
                else {
                    DeleteUser(objExpandedUserDTO);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex) {
                ModelState.AddModelError(string.Empty, "Error: " + ex);
                return View("Edit", GetUser(userName));
            }
        }
        #endregion
        #endregion

        #region USER_ROLES
        // GET: /Admin/User/EditRoles/TestUser 
        #region ActionResult EditRoles(string userName)
        public ActionResult EditRoles(string userName) {
            if (userName == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            userName = userName.ToLower();
            // Check that we have an actual user
            ExpandedUserDTO objExpandedUserDTO = GetUser(userName);
            if (objExpandedUserDTO == null) {
                return HttpNotFound();
            }
            UserAndRolesDTO objUserAndRolesDTO =
                GetUserAndRoles(userName);
            return View(objUserAndRolesDTO);
        }

        // POST: /Admin/User/EditRoles/TestUse
        [HttpPost]
        [ValidateAntiForgeryToken]
        #region public ActionResult EditRoles(UserAndRolesDTO paramUserAndRolesDTO)
        public ActionResult EditRoles(UserAndRolesDTO paramUserAndRolesDTO) {
            try {
                if (paramUserAndRolesDTO == null) {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                string UserName = paramUserAndRolesDTO.UserName;
                string strNewRole = Convert.ToString(Request.Form["AddRole"]);
                if (strNewRole != "No Roles Found") {
                    // Go get the User
                    ApplicationUser user = UserManager.FindByName(UserName);
                    // Put user in role
                    UserManager.AddToRole(user.Id, strNewRole);
                }
                ViewBag.AddRole = new SelectList(RolesUserIsNotIn(UserName));
                UserAndRolesDTO objUserAndRolesDTO =
                    GetUserAndRoles(UserName);
                return View(objUserAndRolesDTO);
            }
            catch (Exception ex) {
                ModelState.AddModelError(string.Empty, "Error: " + ex);
                return View("EditRoles");
            }
        }
        #endregion
        #endregion

        // DELETE: /Admin/User/DeleteRole?userName="TestUser&roleName=Administrator
        #region public ActionResult DeleteRole(string userName, string roleName)
        public ActionResult DeleteRole(string userName, string roleName) {
            try {
                if ((userName == null) || (roleName == null)) {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                userName = userName.ToLower();
                // Check that we have an actual user
                ExpandedUserDTO objExpandedUserDTO = GetUser(userName);
                if (objExpandedUserDTO == null) {
                    return HttpNotFound();
                }
                if (userName.ToLower() ==
                    this.User.Identity.Name.ToLower() && roleName == "Administrator") {
                    ModelState.AddModelError(string.Empty,
                        "Error: Cannot delete Administrator Role for the current user");
                }
                // Go get the User
                ApplicationUser user = UserManager.FindByName(userName);
                // Remove User from role
                UserManager.RemoveFromRoles(user.Id, roleName);
                UserManager.Update(user);
                ViewBag.AddRole = new SelectList(RolesUserIsNotIn(userName));
                return RedirectToAction("EditRoles", new { userName = userName });
            }
            catch (Exception ex) {
                ModelState.AddModelError(string.Empty, "Error: " + ex);
                ViewBag.AddRole = new SelectList(RolesUserIsNotIn(userName));
                UserAndRolesDTO objUserAndRolesDTO = GetUserAndRoles(userName);
                return View("EditRoles", objUserAndRolesDTO);
            }
        }
        #endregion
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
        #region private List GetAllRolesAsSelectList()
        private List<SelectListItem> GetAllRolesAsSelectList() {
            List<SelectListItem> SelectRoleListItems =
                new List<SelectListItem>();
            //var roleManager =
            //    new RoleManager(
            //        new RoleStore(new ApplicationDbContext()));
            var colRoleSelectList = RoleManager.Roles.OrderBy(x => x.Name).ToList();
            SelectRoleListItems.Add(
                new SelectListItem {
                    Text = "Select",
                    Value = "0"
                });
            foreach (var item in colRoleSelectList) {
                SelectRoleListItems.Add(
                    new SelectListItem {
                        Text = item.Name.ToString(),
                        Value = item.Name.ToString()
                    });
            }
            return SelectRoleListItems;
        }
        #endregion
        #region private ExpandedUserDTO GetUser(string userName)
        private ExpandedUserDTO GetUser(string userName) {
            var user = UserManager.FindByName(userName);
            ExpandedUserDTO extUser = new ExpandedUserDTO() {
                UserName = user.UserName,
                Email = user.Email,
                LockoutEndDateUtc = user.LockoutEndDateUtc,
                AccessFailedCount = user.AccessFailedCount,
                PhoneNumber = user.PhoneNumber,
                Roles = user.Roles
            };
            return extUser;
        }
        #endregion
        #region private ExpandedUserDTO UpdateDTOUser(ExpandedUserDTO objExpandedUserDTO)
        private ExpandedUserDTO UpdateDTOUser(ExpandedUserDTO paramExpandedUserDTO) {
            ApplicationUser result =
                UserManager.FindByName(paramExpandedUserDTO.UserName);
            // If we could not find the user, throw an exception
            if (result == null) {
                throw new Exception("Could not find the User");
            }
            result.Email = paramExpandedUserDTO.Email;
            // Lets check if the account needs to be unlocked
            if (UserManager.IsLockedOut(result.Id)) {
                // Unlock user
                UserManager.ResetAccessFailedCountAsync(result.Id);
            }
            UserManager.Update(result);
            // Was a password sent across?
            if (!string.IsNullOrEmpty(paramExpandedUserDTO.Password)) {
                // Remove current password
                var removePassword = UserManager.RemovePassword(result.Id);
                if (removePassword.Succeeded) {
                    // Add new password
                    var AddPassword =
                        UserManager.AddPassword(
                            result.Id,
                            paramExpandedUserDTO.Password
                            );
                    if (AddPassword.Errors.Count() > 0) {
                        throw new Exception(AddPassword.Errors.FirstOrDefault());
                    }
                }
            }
            return paramExpandedUserDTO;
        }
        #endregion
        #region private void DeleteUser(ExpandedUserDTO paramExpandedUserDTO)
        private void DeleteUser(ExpandedUserDTO paramExpandedUserDTO) {
            ApplicationUser user =
                UserManager.FindByName(paramExpandedUserDTO.UserName);
            // If we could not find the user, throw an exception
            if (user == null) {
                throw new Exception("Could not find the User");
            }
            UserManager.RemoveFromRoles(user.Id, UserManager.GetRoles(user.Id).ToArray());
            UserManager.Update(user);
            UserManager.Delete(user);
        }
        #endregion
        #region private UserAndRolesDTO GetUserAndRoles(string userName)
        private UserAndRolesDTO GetUserAndRoles(string userName) {
            // Go get the User
            ApplicationUser user = UserManager.FindByName(userName);
            List<UserRoleDTO> colUserRoleDTO =
                (from objRole in UserManager.GetRoles(user.Id)
                 select new UserRoleDTO {
                     RoleName = objRole,
                     UserName = userName
                 }).ToList();
            if (colUserRoleDTO.Count() == 0) {
                colUserRoleDTO.Add(new UserRoleDTO { RoleName = "No Roles Found" });
            }
            ViewBag.AddRole = new SelectList(RolesUserIsNotIn(userName));
            // Create UserRolesAndPermissionsDTO
            UserAndRolesDTO objUserAndRolesDTO =
                new UserAndRolesDTO();
            objUserAndRolesDTO.UserName = userName;
            objUserAndRolesDTO.colUserRoleDTO = colUserRoleDTO;
            return objUserAndRolesDTO;
        }
        #endregion
        #region private List<string> RolesUserIsNotIn(string userName)
        private List<string> RolesUserIsNotIn(string userName) {
            // Get roles the user is not in
            var colAllRoles = RoleManager.Roles.Select(x => x.Name).ToList();
            // Go get the roles for an individual
            ApplicationUser user = UserManager.FindByName(userName);
            // If we could not find the user, throw an exception
            if (user == null) {
                throw new Exception("Could not find the User");
            }
            var colRolesForUser = UserManager.GetRoles(user.Id).ToList();
            var colRolesUserInNotIn = (from objRole in colAllRoles
                                       where !colRolesForUser.Contains(objRole)
                                       select objRole).ToList();
            if (colRolesUserInNotIn.Count() == 0) {
                colRolesUserInNotIn.Add("No Roles Found");
            }
            return colRolesUserInNotIn;
        }
        #endregion
    }
}