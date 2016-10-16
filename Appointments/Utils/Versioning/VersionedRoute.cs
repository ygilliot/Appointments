using System.Collections.Generic;
using System.Web.Http.Routing;

namespace Appointments.Api.Versioning
{
    /// <summary>
    /// Provides an attribute route that's restricted to a specific version of the api.
    /// </summary>
    public class VersionedRoute : RouteFactoryAttribute
    {
        private readonly string _allowedVersions;
        
        public VersionedRoute( string template, string allowedVersions = null ) : base( template )
        {
            _allowedVersions = allowedVersions;
        }

        public override IDictionary<string, object> Constraints
        {
            get
            {
                var constraints = new HttpRouteValueDictionary {{"version", new VersionConstraint( _allowedVersions )}};
                return constraints;
            }
        }
    }
}