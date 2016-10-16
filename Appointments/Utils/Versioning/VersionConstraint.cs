using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http.Routing;

namespace Appointments.Api.Versioning
{
    public class VersionConstraint : IHttpRouteConstraint
    {
        private readonly string[] _allowedVersions;

        private static string DefaultVersion
        {
            get
            {
                var defaultVersion = ConfigurationManager.AppSettings["DefaultApiVersion"];
                return !string.IsNullOrWhiteSpace( defaultVersion ) ? defaultVersion : "1.0";
            }
        }

        private static string[] SupportedVersions
        {
            get
            {
                var supportedApiVersions = ConfigurationManager.AppSettings["SupportedApiVersions"];
                return !string.IsNullOrWhiteSpace( supportedApiVersions ) ? 
                    supportedApiVersions.Split( new[] {','}, StringSplitOptions.RemoveEmptyEntries ).Select( x => x.Trim() ).ToArray() : 
                    new[] {DefaultVersion};
            }
        }

        public VersionConstraint( string allowedVersions = null )
        {
            // If no allowed versions are specified, the allowed versions are the supported versions
            _allowedVersions = !string.IsNullOrWhiteSpace( allowedVersions ) ? 
                allowedVersions.Split( new[] { ',' }, StringSplitOptions.RemoveEmptyEntries ).Select( x => x.Trim() ).ToArray() : 
                SupportedVersions;
        }

        public bool Match( HttpRequestMessage request, IHttpRoute route, string parameterName, IDictionary<string, object> values, HttpRouteDirection routeDirection )
        {
            if ( routeDirection != HttpRouteDirection.UriResolution ) return true;

            // Get version from route parameters, querystring, accept header or custom header.
            // If no version is found, use default version
            var version = values.ContainsKey( parameterName ) ? ((string) values[parameterName]).ToLower().TrimStart( 'v' ) :
                GetVersionFromQueryString( request, "version" ) ??
                GetVersionFromAcceptHeader( request, "version" ) ??
                GetVersionFromCustomHeader( request, "X-Api-Version" ) ?? 
                DefaultVersion;

            // Add api version to request
            if ( !request.Properties.ContainsKey( "ApiVersion" ) )
                request.Properties.Add( "ApiVersion", version );

            // Disallow unsupported versions
            return MatchVersion( version, SupportedVersions ) && MatchVersion( version, _allowedVersions );
        }

        private static bool MatchVersion( string version, string[] allowedVersions )
        {
            // Return true if version is allowed
            if ( allowedVersions.Contains( version, StringComparer.InvariantCultureIgnoreCase ) ) return true;
            
            // To allow matching 1 to 1.0 and vice versa, try parse versions as doubles and match them
            foreach ( var allowedVersion in from v in allowedVersions 
                                            let fpVersion = version.Count( x => x == '.' ) 
                                            let fpAllowedVersion = v.Count( x => x == '.' ) 
                                            where ( fpVersion == 1 || fpVersion == 0 ) && ( fpAllowedVersion == 1 || fpAllowedVersion == 0 ) select v )
            {
                // Try to parse specified version as double
                double versionNr;
                double.TryParse( version, NumberStyles.Any, CultureInfo.InvariantCulture, out versionNr );

                // Try to parse allowed version as double
                double allowedVersionNr;
                double.TryParse( allowedVersion, NumberStyles.Any, CultureInfo.InvariantCulture, out allowedVersionNr );

                // Check if versions match
                if ( versionNr.Equals( allowedVersionNr ) )
                    return true;
            }

            return false;
        }

        private static string GetVersionFromQueryString( HttpRequestMessage request, string key )
        {
            // Get version parameter value from querystring
            return request.GetQueryNameValuePairs().Any( x => x.Key.Equals( key ) ) ? 
                request.GetQueryNameValuePairs().First( x => x.Key.Equals( key, StringComparison.InvariantCultureIgnoreCase ) ).Value : 
                null;
        }

        private static string GetVersionFromAcceptHeader( HttpRequestMessage request, string paramName )
        {
            if ( !request.Headers.Accept.Any() ) return null;

            foreach ( var acceptHeader in request.Headers.Accept )
            {
                // Support for application/json; [paramName]=[VERSION]
                if ( acceptHeader.Parameters.Any( x => x.Name.Equals( paramName, StringComparison.InvariantCultureIgnoreCase ) ) )
                    return acceptHeader.Parameters.First( x => x.Name.Equals( paramName, StringComparison.InvariantCultureIgnoreCase ) ).Value;
                // Support for application/vnd.yourvendorname-v[VERSION]+json
                if ( !string.IsNullOrEmpty( acceptHeader.MediaType ) && Regex.IsMatch( acceptHeader.MediaType, @"application\/vnd\..*-v([\d]+(\.\d+)*)\+(json|xml)" ) )
                    return Regex.Match( acceptHeader.MediaType, @"(?<=-v)[\d]+(\.\d+)*" ).Value;
            }

            return null;
        }

        private static string GetVersionFromCustomHeader( HttpRequestMessage request, string name )
        {
            // Get version from custom header
            IEnumerable<string> keys;
            return !request.Headers.TryGetValues( name, out keys ) ? null : keys.First();
        }
    }
}