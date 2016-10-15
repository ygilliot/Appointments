using System.Web;
using System.Web.Mvc;

namespace Appointments.Api
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //uncomment to force authorize attribute on all controllers (including api help?)
            //filters.Add(new System.Web.Mvc.AuthorizeAttribute());
            //only https connexions can access controllers
            filters.Add(new RequireHttpsAttribute());
        }
    }
}
