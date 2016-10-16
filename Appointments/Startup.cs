using System;
using System.Linq;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Extensions.ExecutionContextScoping;

[assembly: OwinStartup(typeof(Appointments.Api.Startup))]

namespace Appointments.Api {
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            SimpleInjectorWebApiInitializer.Initialize(app);
            ConfigureAuth(app);
        }
    }
}
