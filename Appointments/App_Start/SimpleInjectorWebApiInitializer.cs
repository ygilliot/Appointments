//[assembly: WebActivator.PostApplicationStartMethod(typeof(Appointments.Api.App_Start.SimpleInjectorWebApiInitializer), "Initialize")]

namespace Appointments.Api {
    using System.Web.Http;
    using SimpleInjector;
    using SimpleInjector.Integration.WebApi;
    using SimpleInjector.Extensions.ExecutionContextScoping;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using Repositories;
    using Controllers;
    using Owin;
    using System.Reflection;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security.DataProtection;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.DataHandler;
    using Microsoft.Owin.Security.DataHandler.Serializer;
    using Microsoft.Owin.Security.DataHandler.Encoder;
    using SimpleInjector.Advanced;
    using Microsoft.Owin;
    using System.Collections.Generic;
    using System.Web;

    public static class SimpleInjectorWebApiInitializer {
        public static void Initialize(IAppBuilder app) {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
            container.Options.AllowOverridingRegistrations = true;

            
            //container.Register<IAuthenticationManager>(() => AdvancedExtensions.IsVerifying(container) ? new OwinContext(new Dictionary<string, object>()).Authentication : HttpContext.Current.GetOwinContext().Authentication);

            ApplicationUserManager um = new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext("DefaultConnection")));
            //ApplicationSignInManager sm = new ApplicationSignInManager(um, container.GetInstance<IAuthenticationManager>());
            SecureDataFormat<AuthenticationTicket> sdt = new SecureDataFormat<AuthenticationTicket>(
                new TicketSerializer(),
                new DpapiDataProtectionProvider().Create("ASP.NET Identity"),
                TextEncodings.Base64);

            container.Register<AccountController>(() => new AccountController(um, sdt), Lifestyle.Scoped);

            //container.Register<ApplicationSignInManager>(() => new ApplicationSignInManager(um, container.GetInstance<IAuthenticationManager>()), Lifestyle.Scoped);
            //container.Register<LoginController>(() => new LoginController(um, sm), Lifestyle.Scoped);
            container.Options.AllowOverridingRegistrations = false;

            container.RegisterWebApiRequest<ApplicationDbContext>(() => new ApplicationDbContext("DefaultConnection"));
            container.Register(typeof(IRepository<>), typeof(GenericRepository<>), Lifestyle.Scoped);

            container.Verify();

            app.Use(async (context, next) => {
                using (container.BeginExecutionContextScope()) {
                    await next();
                }
            });
            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }
    }
}