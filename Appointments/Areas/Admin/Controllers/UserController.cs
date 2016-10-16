using Appointments.Api.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Appointments.Api.Areas.Admin.Controllers
{
    [Authorize(Roles = AppRoles.Admin)]
    public class UserController : Controller
    {
        // GET: Admin/User
        public ActionResult Index()
        {
            return View();
        }
    }
}