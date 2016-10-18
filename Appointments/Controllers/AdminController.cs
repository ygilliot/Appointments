using Appointments.Api.Models.DTO;
using Appointments.Api.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Appointments.Api.Controllers
{
    public class AdminController : Controller
    {
        //private ApplicationUserManager _userManager;
        //private ApplicationRoleManager _roleManager;
        //// Controllers
        //// GET: /Admin/
        //[Authorize(Roles = AppRoles.Admin)]
        //#region public ActionResult Index(string searchStringUserNameOrEmail)
        //public ActionResult Index(string searchStringUserNameOrEmail, string currentFilter, int? page) {
        //    try {
        //        int intPage = 1;
        //        int intPageSize = 5;
        //        int intTotalPageCount = 0;
        //        if (searchStringUserNameOrEmail != null) {
        //            intPage = 1;
        //        }
        //        else {
        //            if (currentFilter != null) {
        //                searchStringUserNameOrEmail = currentFilter;
        //                intPage = page ?? 1;
        //            }
        //            else {
        //                searchStringUserNameOrEmail = "";
        //                intPage = page ?? 1;
        //            }
        //        }
        //        ViewBag.CurrentFilter = searchStringUserNameOrEmail;
        //        List col_UserDTO = new List();
        //        int intSkip = (intPage - 1) * intPageSize;
        //        intTotalPageCount = UserManager.Users
        //            .Where(x => x.UserName.Contains(searchStringUserNameOrEmail))
        //            .Count();
        //        var result = UserManager.Users
        //            .Where(x => x.UserName.Contains(searchStringUserNameOrEmail))
        //            .OrderBy(x => x.UserName)
        //            .Skip(intSkip)
        //            .Take(intPageSize)
        //            .ToList();
        //        foreach (var item in result) {
        //            ExpandedUserDTO objUserDTO = new ExpandedUserDTO();
        //            objUserDTO.UserName = item.UserName;
        //            objUserDTO.Email = item.Email;
        //            objUserDTO.LockoutEndDateUtc = item.LockoutEndDateUtc;
        //            col_UserDTO.Add(objUserDTO);
        //        }
        //        // Set the number of pages
        //        var _UserDTOAsIPagedList =
        //            new StaticPagedList
        //            (
        //                col_UserDTO, intPage, intPageSize, intTotalPageCount
        //                );
        //        return View(_UserDTOAsIPagedList);
        //    }
        //    catch (Exception ex) {
        //        ModelState.AddModelError(string.Empty, "Error: " + ex);
        //        List col_UserDTO = new List();
        //        return View(col_UserDTO.ToPagedList(1, 25));
        //    }
        //}
        //#endregion

        //// Utility
        //#region public ApplicationUserManager UserManager
        //public ApplicationUserManager UserManager {
        //    get {
        //        return _userManager ??
        //            HttpContext.GetOwinContext()
        //            .GetUserManager();
        //    }
        //    private set {
        //        _userManager = value;
        //    }
        //}
        //#endregion
        //#region public ApplicationRoleManager RoleManager
        //public ApplicationRoleManager RoleManager {
        //    get {
        //        return _roleManager ??
        //            HttpContext.GetOwinContext()
        //            .GetUserManager();
        //    }
        //    private set {
        //        _roleManager = value;
        //    }
        //}
        //#endregion
    }
}