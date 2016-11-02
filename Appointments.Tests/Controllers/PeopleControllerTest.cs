using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Appointments.Api;
using Appointments.Api.Controllers;
using Appointments.Api.Models;
using Appointments.Api.Repositories;
using Moq;
using Appointments.Tests.Mocks;
using Appointments.Api.Models.DTO;
using System.Web.Http.Results;
using System.Security.Principal;
using System.Security.Claims;
using Microsoft.Owin;
using Microsoft.Owin.Testing;
using Appointments.Tests.OwinTest;
using Microsoft.AspNet.Identity;

namespace Appointments.Api.Tests.Controllers {
    [TestClass]
    public class PeopleControllerTest {
        [TestMethod]
        public void GetPeople() {
            var repo = new Mock<IRepository<Person>>();
            // Arrange
            PeopleRepository rep = new PeopleRepository();
            PeopleController controller = new PeopleController(rep.Repo);

            // Act
            IEnumerable<PersonDTO> result = controller.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(7, result.Count());
            Assert.AreEqual("Escobar Gaviria", result.ElementAt(2).LastName);
            Assert.AreEqual("White", result.ElementAt(3).LastName);
        }

        [TestMethod]
        public void GetPeopleById() {
            var repo = new Mock<IRepository<Person>>();
            // Arrange
            PeopleRepository rep = new PeopleRepository();
            PeopleController controller = new PeopleController(rep.Repo);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act
            IHttpActionResult result = controller.Get("jesse@pink.man");
            var contentResult = result as OkNegotiatedContentResult<PersonExtendedDTO>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual("Albuquerque", contentResult.Content.City);
        }

        [TestMethod]
        public void CreatePerson() {
            #region Owin Context
            var owinMock = new Mock<IOwinContext>();

            var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
            userStoreMock.Setup(s => s.FindByNameAsync("pablo.escobar@coca.in")).ReturnsAsync(new ApplicationUser {
                Id = "ed980470-9c0f-47f7-a967-0adc9eb2325e",
                Email = "pablo.escobar@coca.in",
                UserName = "pablo.escobar@coca.in",
                PhoneNumber = "012345678"
            });
            var applicationUserManager = new ApplicationUserManager(userStoreMock.Object);

            owinMock.Setup(o => o.Get<ApplicationUserManager>(It.IsAny<string>())).Returns(applicationUserManager);
            #endregion

            var repo = new Mock<IRepository<Person>>();
            // Arrange
            PeopleRepository rep = new PeopleRepository();
            PeopleController controller = new PeopleController(rep.Repo);
            controller.Request = new HttpRequestMessage();
            controller.Request.SetOwinContext(owinMock.Object);
            controller.Configuration = new HttpConfiguration();
            controller.User = new ClaimsPrincipal(new GenericPrincipal(new GenericIdentity("admin@admin.com"), new string[] { Utils.AppRoles.Admin }));
            PersonExtendedDTO person = new PersonExtendedDTO() {
                FirstName = "Pablo Emilio",
                LastName = "Escobar Gaviria",
                Gender = "Mr.",
                Address1 = "Hacienda Nápoles",
                City = "Medellín",
                Country = "Colombia",
                UserName = "pablo.escobar@coca.in",
                Email = "pablo.escobar@coca.in",
                PhoneNumber = "012345678"
            };

            // Act
            IHttpActionResult result = controller.Post(person);
            var contentResult = result as OkNegotiatedContentResult<PersonExtendedDTO>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual("Medellín", contentResult.Content.City);
        }

        [TestMethod]
        public void PutPerson() {
            #region Owin Context
            var owinMock = new Mock<IOwinContext>();

            var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
            userStoreMock.Setup(s => s.FindByNameAsync("pablo.escobar@coca.in")).ReturnsAsync(new ApplicationUser {
                Id = "ed980470-9c0f-47f7-a967-0adc9eb2325e",
                Email = "pablo.escobar@coca.in",
                UserName = "pablo.escobar@coca.in",
                PhoneNumber = "012345678"
            });
            var applicationUserManager = new ApplicationUserManager(userStoreMock.Object);

            owinMock.Setup(o => o.Get<ApplicationUserManager>(It.IsAny<string>())).Returns(applicationUserManager);
            #endregion

            var repo = new Mock<IRepository<Person>>();
            // Arrange
            PeopleRepository rep = new PeopleRepository();
            PeopleController controller = new PeopleController(rep.Repo);
            controller.Request = new HttpRequestMessage();
            controller.Request.SetOwinContext(owinMock.Object);
            controller.Configuration = new HttpConfiguration();
            controller.User = new ClaimsPrincipal(new GenericPrincipal(new GenericIdentity("admin@admin.com"), new string[] { Utils.AppRoles.Admin }));

            PersonExtendedDTO person = new PersonExtendedDTO() {
                FirstName = "Pablo Emilio",
                LastName = "Escobar Gaviria",
                //Gender = "Mr.", //Volontary ommit Gender
                Address1 = "Hacienda Nápoles",
                City = "Puerto Triunfo",//base value is: Medellín
                Country = "Colombia",
                UserName = "pablo.escobar@coca.in",
                Email = "pablo.escobar@coca.in",
                PhoneNumber = "012345678"
            };

            // Act
            IHttpActionResult result = controller.Put(person);
            var contentResult = result as OkNegotiatedContentResult<PersonExtendedDTO>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.IsNull(contentResult.Content.Gender);
            Assert.AreEqual("Puerto Triunfo", contentResult.Content.City);
        }

        [TestMethod]
        public void PatchPerson() {
            #region Owin Context
            var owinMock = new Mock<IOwinContext>();

            var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
            userStoreMock.Setup(s => s.FindByNameAsync("pablo.escobar@coca.in")).ReturnsAsync(new ApplicationUser {
                Id = "ed980470-9c0f-47f7-a967-0adc9eb2325e",
                Email = "pablo.escobar@coca.in",
                UserName = "pablo.escobar@coca.in",
                PhoneNumber = "012345678",
                Person = new Person() { Id = "ed980470-9c0f-47f7-a967-0adc9eb2325e", FirstName = "Pablo Emilio", LastName = "Escobar Gaviria", Gender = "Mr.", Address = new UserAddress() { Id = "ed980470-9c0f-47f7-a967-0adc9eb2325e", City = "Medellín", Country = "Colombia", }, ApplicationUser = new ApplicationUser() { Id = "ed980470-9c0f-47f7-a967-0adc9eb2325e", UserName = "pablo.escobar@coca.in", Email = "pablo.escobar@coca.in", PhoneNumber = "012345678" } },
            });
            var applicationUserManager = new ApplicationUserManager(userStoreMock.Object);

            owinMock.Setup(o => o.Get<ApplicationUserManager>(It.IsAny<string>())).Returns(applicationUserManager);
            #endregion

            var repo = new Mock<IRepository<Person>>();
            // Arrange
            PeopleRepository rep = new PeopleRepository();
            PeopleController controller = new PeopleController(rep.Repo);
            controller.Request = new HttpRequestMessage();
            controller.Request.SetOwinContext(owinMock.Object);
            controller.Configuration = new HttpConfiguration();
            controller.User = new ClaimsPrincipal(new GenericPrincipal(new GenericIdentity("admin@admin.com"), new string[] { Utils.AppRoles.Admin }));

            PersonExtendedDTO person = new PersonExtendedDTO() {
                UserName = "pablo.escobar@coca.in",
                Address1 = "Hacienda Nápoles",
                City = "Puerto Triunfo",
            };

            // Act
            IHttpActionResult result = controller.Patch(person);
            var contentResult = result as OkNegotiatedContentResult<PersonExtendedDTO>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual("Puerto Triunfo", contentResult.Content.City);
            Assert.AreEqual("Hacienda Nápoles", contentResult.Content.Address1);
            Assert.AreEqual("Colombia", contentResult.Content.Country);
        }

        //[TestMethod]
        //public void Delete()
        //{
        //    // Arrange
        //    ValuesController controller = new ValuesController();

        //    // Act
        //    controller.Delete(5);

        //    // Assert
        //}
    }
}
