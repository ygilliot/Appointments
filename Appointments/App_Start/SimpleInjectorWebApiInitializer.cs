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

        //private static void InitializeContainer(Container container) {
        //    container.Options.AllowOverridingRegistrations = true;
        //    ApplicationUserManager um = new ApplicationUserManager(
        //        new UserStore<ApplicationUser>(new ApplicationDbContext()));
        //    SecureDataFormat<AuthenticationTicket> sdt = new SecureDataFormat<AuthenticationTicket>(
        //        new TicketSerializer(),
        //        new DpapiDataProtectionProvider().Create("ASP.NET Identity"),
        //        TextEncodings.Base64);

        //    container.Options.AllowOverridingRegistrations = true;

        //    container.Register<AccountController>(() =>
        //        new AccountController(
        //            new ApplicationUserManager(
        //                new UserStore<ApplicationUser>(new ApplicationDbContext())),
        //            sdt),
        //        Lifestyle.Scoped);

        //    container.Options.AllowOverridingRegistrations = false;

        //    container.Register(typeof(IRepository<>), typeof(GenericRepository<>), Lifestyle.Scoped);
        //}


        ///// <summary>Initialize the container and register it as Web API Dependency Resolver.</summary>
        //public static Container Initialize(IAppBuilder app) {
        //    //var container = new Container();
        //    //container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();

        //    //InitializeContainer(container);
        //    //container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

        //    var container = GetInitializeContainer(app);
        //    container.Verify();

        //    GlobalConfiguration.Configuration.DependencyResolver =
        //        new SimpleInjectorWebApiDependencyResolver(container);

        //    return container;
        //}

        //private static void InitializeContainer(Container container) {

        //    // For instance:
        //    //container.Register<IdentityDbContext<ApplicationUser>, ApplicationDbContext>(Lifestyle.Scoped);
        //    container.Register(typeof(IRepository<>), typeof(GenericRepository<>), Lifestyle.Scoped);
        //    // container.Register<IUserRepository, SqlUserRepository>(Lifestyle.Scoped);
        //}

        //public static Container GetInitializeContainer(IAppBuilder app) {
        //    var container = new Container();
        //    container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();

        //    container.RegisterSingleton<IAppBuilder>(app);
        //    container.RegisterWebApiRequest<ApplicationUserManager>();
        //    container.RegisterWebApiRequest<ApplicationDbContext>(() => new ApplicationDbContext("DefaultConnection"));
        //    container.RegisterWebApiRequest<IUserStore<ApplicationUser>>(() => new UserStore<ApplicationUser>(container.GetInstance<ApplicationDbContext>()));
        //    container.RegisterInitializer<ApplicationUserManager>(manager => InitializeUserManager(manager, app));

        //    container.RegisterWebApiRequest<ISecureDataFormat<AuthenticationTicket>, SecureDataFormat<AuthenticationTicket>>();
        //    container.RegisterWebApiRequest<ITextEncoder, Base64UrlTextEncoder>();
        //    container.RegisterWebApiRequest<IDataSerializer<AuthenticationTicket>, TicketSerializer>();
        //    container.RegisterWebApiRequest<IDataProtector>(() => new Microsoft.Owin.Security.DataProtection.DpapiDataProtectionProvider().Create("ASP.NET Identity"));

        //    container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
        //    //container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

        //    InitializeContainer(container);

        //    return container;
        //}

        //private static void InitializeUserManager(ApplicationUserManager manager, IAppBuilder app) {
        //    manager.UserValidator =
        //     new UserValidator<ApplicationUser>(manager) {
        //         AllowOnlyAlphanumericUserNames = false,
        //         RequireUniqueEmail = true
        //     };

        //    //Configure validation logic for passwords
        //    manager.PasswordValidator = new PasswordValidator() {
        //        RequiredLength = 6,
        //        RequireNonLetterOrDigit = false,
        //        RequireDigit = true,
        //        RequireLowercase = true,
        //        RequireUppercase = true,
        //    };

        //    var dataProtectionProvider = app.GetDataProtectionProvider();

        //    if (dataProtectionProvider != null) {
        //        manager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
        //    }
        //}
    }
}