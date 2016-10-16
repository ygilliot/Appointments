using System.Web.Http.Filters;

namespace Appointments.Api.Versioning
{
    public class VersionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted( HttpActionExecutedContext actionExecutedContext )
        {
            // Add current version to response
            if ( actionExecutedContext.Request.Properties.ContainsKey( "ApiVersion" ) )
                actionExecutedContext.Response.Headers.Add( "X-Api-Version", actionExecutedContext.Request.Properties["ApiVersion"].ToString() );
        }
    }
}