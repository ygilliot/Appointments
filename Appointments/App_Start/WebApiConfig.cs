using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using Appointments.Api.Versioning;
using System.Net.Http.Formatting;
using System.Configuration;
using System.Net.Http.Headers;
using Appointments.Api.Models;

namespace Appointments.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            // Allow version to be part of the resource url
            config.Routes.MapHttpRoute(
                name: "VersionedApi",
                routeTemplate: "api/{version}/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional },
                constraints: new { version = new VersionConstraint() } // Add version contraint here to force resolving only supported versions
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional },
                constraints: new { version = new VersionConstraint() } // Add version contraint here to force resolving only supported versions
            );
        }

        /// <summary>
        /// Config webapi for the versioning
        /// </summary>
        public static void VersioningConfig() {
            // Only allow json and xml as output
            GlobalConfiguration.Configuration.Formatters.Clear();
            GlobalConfiguration.Configuration.Formatters.Add(new XmlMediaTypeFormatter());
            GlobalConfiguration.Configuration.Formatters.Add(new JsonMediaTypeFormatter());

            // Add custom media type formatters for supported versions
            foreach (var supportedVersion in ConfigurationManager.AppSettings["SupportedApiVersions"].Split(',').Select(x => x.Trim())) {
                GlobalConfiguration.Configuration.Formatters.JsonFormatter.SupportedMediaTypes.Add(
                    new MediaTypeHeaderValue(string.Format("application/vnd.webapiversioning-v{0}+json", supportedVersion)));

                GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Add(
                    new MediaTypeHeaderValue(string.Format("application/vnd.webapiversioning-v{0}+xml", supportedVersion)));
            }

            // Add typed formatters
            GlobalConfiguration.Configuration.Formatters.Add(
                    new TypedJsonMediaTypeFormatter(typeof(Person),
                        new MediaTypeHeaderValue("application/vnd.vendor.person-v1.0+json")));

            GlobalConfiguration.Configuration.Formatters.Add(
                new TypedXmlMediaTypeFormatter(typeof(Person),
                    new MediaTypeHeaderValue("application/vnd.vendor.person-v1.0+xml")));
        }
    }
}
