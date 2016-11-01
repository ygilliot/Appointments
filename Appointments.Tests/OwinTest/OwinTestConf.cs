using Appointments.Api;
using Appointments.Api.Models;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;


namespace Appointments.Tests.OwinTest {
    public class OwinTestConf {
        public void Configuration(IAppBuilder app) {
            var startup = new Startup();
            startup.ConfigureAuth(app);
            //app.MapSignalR();
            //app.Use(typeof(LogMiddleware));
            //HttpConfiguration config = new HttpConfiguration();
            //config.Services.Replace(typeof(IAssembliesResolver), new TestWebApiResolver());
            //config.MapHttpAttributeRoutes();
            //app.UseWebApi(config);
        }
    }
}
